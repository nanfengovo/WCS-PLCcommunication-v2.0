using Microsoft.AspNetCore.Mvc;

namespace PLCCommunicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogTrace("Trace级别：测试ASP.NET Core自带的日志系统！！");
            _logger.LogDebug("Debug级别：测试ASP.NET Core自带的日志系统！！");
            _logger.LogInformation("Info级别：测试ASP.NET Core自带的日志系统！！");
            _logger.LogWarning("Warning级别：测试ASP.NET Core自带的日志系统！！");
            _logger.LogError("Error级别：测试ASP.NET Core自带的日志系统！！ ");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
