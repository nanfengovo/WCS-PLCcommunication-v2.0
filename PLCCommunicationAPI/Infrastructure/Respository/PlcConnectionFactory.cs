using Microsoft.Extensions.Logging;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.BcakServiceModel;
using Polly;
using S7.Net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.Respository
{
    // 实现类
    public class PlcConnectionFactory : IPlcConnectionFactory, IDisposable
    {
        private readonly ConcurrentDictionary<string, Lazy<Plc>> _connections;
        private readonly ConcurrentDictionary<string, PlcConnectionInfo> _connectionInfos;
        private readonly Timer _cleanupTimer;
        private readonly ILogger<PlcConnectionFactory> _logger;
        private readonly object _syncRoot = new object();

        // 配置参数
        private readonly TimeSpan _maxIdleTime = TimeSpan.FromMinutes(5);
        private readonly TimeSpan _heartbeatInterval = TimeSpan.FromSeconds(30);
        private const int MaxRetryCount = 3;

        public PlcConnectionFactory(ILogger<PlcConnectionFactory> logger)
        {
            _logger = logger;
            _connections = new ConcurrentDictionary<string, Lazy<Plc>>();
            _connectionInfos = new ConcurrentDictionary<string, PlcConnectionInfo>();

            // 启动定时清理任务
            _cleanupTimer = new Timer(CleanupConnections, null,
                TimeSpan.FromMinutes(1),
                TimeSpan.FromMinutes(1));
        }

        public Plc GetConnection(string ip, int port)
        {
            var key = GenerateKey(ip, port);

            var lazyPlc = _connections.GetOrAdd(key, k =>
                new Lazy<Plc>(() => CreatePlcConnection(ip, port)));

            UpdateConnectionInfo(key, true);

            // 检查并维持连接状态
            MaintainConnection(lazyPlc.Value, key);

            return lazyPlc.Value;
        }

        // Existing code...

        private Plc CreatePlcConnection(string ip, int port)
        {
            var plc = new Plc(CpuType.S71200, ip, port, 0, 0);
            var key = GenerateKey(ip, port);

            var policy = Policy
                .Handle<PlcException>()
                .WaitAndRetry(MaxRetryCount,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(ex,
                            $"PLC连接失败 ({key})，正在第 {retryCount} 次重试");
                    });

            policy.Execute(() =>
            {
                if (!plc.IsConnected)
                {
                    plc.Open();
                    StartHeartbeat(plc, key);
                    _logger.LogInformation($"成功建立PLC连接：{key}");
                }
            });

            return plc;
        }

        private void MaintainConnection(Plc plc, string key)
        {
            lock (_syncRoot)
            {
                if (!plc.IsConnected)
                {
                    try
                    {
                        plc.Close();
                        plc.Open();
                        _logger.LogWarning($"重新建立中断的PLC连接：{key}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"无法恢复PLC连接：{key}");
                        _connections.TryRemove(key, out _);
                        throw new PlcConnectionException($"无法连接到PLC：{key}");
                    }
                }
            }
        }

        private void StartHeartbeat(Plc plc, string key)
        {
            var timer = new Timer(x=>
            {
                try
                {
                    if (plc.IsConnected)
                    {
                        plc.Write("HeartbeatAddress", 1);
                        _logger.LogDebug($"心跳检测成功：{key}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, $"心跳检测失败：{key}");
                    _connections.TryRemove(key, out _);
                }
            }, null, _heartbeatInterval, _heartbeatInterval);
        }

        public void ReleaseConnection(Plc plc)
        {
            var key = GenerateKey(plc.IP, plc.Port);
            UpdateConnectionInfo(key, false);
        }

        private void UpdateConnectionInfo(string key, bool isUsing)
        {
            _connectionInfos.AddOrUpdate(key,
                addValueFactory: k => new PlcConnectionInfo
                {
                    Key = k,
                    LastUsedTime = DateTime.UtcNow,
                    UsageCount = 1,
                    IsConnected = true
                },
                updateValueFactory: (k, existing) =>
                {
                    existing.LastUsedTime = DateTime.UtcNow;
                    if (isUsing) existing.UsageCount++;
                    return existing;
                });
        }

        private void CleanupConnections(object state)
        {
            var cutoff = DateTime.UtcNow - _maxIdleTime;
            var oldKeys = _connectionInfos
                .Where(kvp => kvp.Value.LastUsedTime < cutoff)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in oldKeys)
            {
                if (_connections.TryRemove(key, out var lazyPlc))
                {
                    try
                    {
                        var plc = lazyPlc.Value;
                        if (plc.IsConnected)
                        {
                            plc.Close();
                            _logger.LogInformation($"清理空闲连接：{key}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"清理连接时发生错误：{key}");
                    }
                    _connectionInfos.TryRemove(key, out _);
                }
            }
        }

        public IEnumerable<PlcConnectionInfo> GetConnectionStatus()
        {
            return _connectionInfos.Values.ToList();
        }

        private static string GenerateKey(string ip, int port) => $"{ip}:{port}";

        public void Dispose()
        {
            _cleanupTimer?.Dispose();
            foreach (var connection in _connections.Values)
            {
                try
                {
                    if (connection.IsValueCreated && connection.Value.IsConnected)
                    {
                        connection.Value.Close();
                    }
                }
                catch { /* 忽略关闭异常 */ }
            }
            _connections.Clear();
            _connectionInfos.Clear();
        }

        IEnumerable<PlcConnectionInfo> IPlcConnectionFactory.GetConnectionStatus()
        {
            throw new NotImplementedException();
        }
    }
}
