using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Model.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ScheduledTasks.ModbusTcp
{
    public class Initialize : BackgroundService
    {

        private readonly IServiceProvider _services;

        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ILogger<Initialize> _logger;

        public Initialize(IServiceProvider services, IServiceScopeFactory scopeFactory, ILogger<Initialize> logger)
        {
            _services = services;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var modbus = serviceProvider.GetRequiredService<IModbusTCP>();
                    if (await modbus.InitializeAsync())
                    {
                        _logger.LogInformation("数据库初始化成功");
                    }
                    else
                    {
                        _logger.LogError("数据库初始化失败");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "后台服务启动失败");
                //throw; // 根据需求决定是否终止应用
            }
        }


        public override Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}