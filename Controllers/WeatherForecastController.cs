using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Holamundo.Controllers
{
    [ApiController] //Controler que se encarga de tocas las peticiones a la ruta https://localhost:7051/WeatherForecast

    [Route("[controller]")]
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
        // Método GET, 
        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Administrator, User")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogTrace($"{nameof(WeatherForecastController)} - {nameof(Get)} - Trace Log Level");
            _logger.LogDebug($"{nameof(WeatherForecastController)} - {nameof(Get)} - Debug log Level");
            _logger.LogInformation($"{nameof(WeatherForecastController)} - {nameof(Get)} - Information Log Level");
            _logger.LogWarning($"{nameof(WeatherForecastController)} - {nameof(Get)} - Warning Log Level");
            _logger.LogError($"{nameof(WeatherForecastController)} - {nameof(Get)} - Error Log Level");
            _logger.LogCritical($"{nameof(WeatherForecastController)} - {nameof(Get)} - Critical Log Level");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}