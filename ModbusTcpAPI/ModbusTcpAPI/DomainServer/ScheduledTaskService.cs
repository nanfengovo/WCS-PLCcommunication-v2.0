using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class ScheduledTaskService : BackgroundService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Uri _apiUrl;

    public ScheduledTaskService(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
    {
        _httpClientFactory = httpClientFactory;
        // 你可以从配置或DI中获取API的URL
        _apiUrl = new Uri("http://localhost:5057/api/ModbusTCP/ReadHoldingRegisters");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(_apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // 处理结果
                }
            }
            catch (Exception ex)
            {
                // 日志记录异常
            }

            // 等待一段时间后再次执行
            await Task.Delay(TimeSpan.FromMilliseconds(1000), stoppingToken);
        }
    }
}

