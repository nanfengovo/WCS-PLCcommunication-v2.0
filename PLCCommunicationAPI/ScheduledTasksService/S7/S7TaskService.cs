using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Infrastructure.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledTasksService.S7
{
    public class S7TaskService : BackgroundService
    {
        private readonly IServiceProvider _services;

        //private readonly S7ProxyToTask _s7ProxyToTask;

        //private readonly S7ReadTaskResposity _s7ReadTaskResposity;

        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ILogger<S7TaskService> _logger;

        public S7TaskService(IServiceProvider services, IServiceScopeFactory scopeFactory, ILogger<S7TaskService> logger)
        {
            _services = services;
            //_s7ProxyToTask = s7ProxyToTask;
            //_s7ReadTaskResposity = s7ReadTaskResposity;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        /// <summary>
        /// 开启后台任务初始化执行一次
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var proxyToTask = serviceProvider.GetRequiredService<S7ProxyToTask>();

                    bool isInitialized = await proxyToTask.InitializeDatabaseAsync();
                    if (isInitialized)
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
            await base.StartAsync(cancellationToken);
        }


        /// <summary>
        /// 循环执行任务
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 长期运行的任务（例如每2秒执行一次）
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetRequiredService<IS7ReadTaskResposity>();
                        var result = await repository.ReadAsync();
                        Console.WriteLine("后台任务S7读取ing");
                    }
                    await Task.Delay(2000, stoppingToken); // 间隔2秒
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "后台任务执行失败");
                }
            }
        }






        public override Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
