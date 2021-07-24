using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApplicationInsightsDemo.API.Controllers
{
    [ApiController]
    [Route("api/weather-forecast")]
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

        [HttpGet("{secondsToBeSlow:int}")]
        public async Task<IEnumerable<WeatherForecast>> Get(int secondsToBeSlow)
        {
            _logger.LogInformation("Fetching weather with a {DelayInSeconds} delay", secondsToBeSlow);
            await Task.Delay(secondsToBeSlow * 1000);

            _logger.LogInformation("Done waiting for weathre delay. Now generatin forecasts.");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
