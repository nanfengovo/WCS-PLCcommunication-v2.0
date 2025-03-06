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
            _logger.LogTrace("Trace���𣺲���ASP.NET Core�Դ�����־ϵͳ����");
            _logger.LogDebug("Debug���𣺲���ASP.NET Core�Դ�����־ϵͳ����");
            _logger.LogInformation("Info���𣺲���ASP.NET Core�Դ�����־ϵͳ����");
            _logger.LogWarning("Warning���𣺲���ASP.NET Core�Դ�����־ϵͳ����");
            _logger.LogError("Error���𣺲���ASP.NET Core�Դ�����־ϵͳ���� ");
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
