using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Read_Configuration02.Model;

namespace Read_Configuration02.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IOptionsSnapshot<DbSettings> optDbsettings;
        private readonly IOptionsSnapshot<SmtpSettings> optSmtpSettings;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IOptionsSnapshot<DbSettings> optDbsettings, IOptionsSnapshot<SmtpSettings> optSmtpSettings, ILogger<WeatherForecastController> logger)
        {
            this.optDbsettings = optDbsettings;
            this.optSmtpSettings = optSmtpSettings;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public string GetConfig()
        {
            return optSmtpSettings.Value.UserName.ToString();
        }
    }
}
