using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApplicationInsightsDemo.UI.Services
{
    public class ApiClient : IApiClient
    {
        private static Random random = new Random();
        private const int DefaultNumber = 123;

        private readonly HttpClient _client;
        private readonly ILogger<ApiClient> _logger;

        public ApiClient(HttpClient client, ILogger<ApiClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<int> GetRandomNumberAsync()
        {
            var result = DefaultNumber;
            try
            {
                _logger.LogInformation("Calling random number API get get new random number");

                var response = await _client.GetAsync("/api/values");

                _logger.LogInformation("Response from random number API was {ResponseCode}", response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (int.TryParse(responseContent, out var paresedContent))
                    {
                        _logger.LogInformation("Successfully parsed response content into result {Result}", paresedContent);
                        result = paresedContent;
                    }
                    else
                    {
                        _logger.LogWarning("Could not parse respone content of {ResponseContent}", responseContent);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while calling the random number API");
            }

            return result;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
        {
            var secondsToBeSlow = random.Next(1, 5);

            try
            {
                _logger.LogInformation("Calling weather forecast api with {Seconds} second(s) delay", secondsToBeSlow);

                var response = await _client.GetAsync($"/api/weather-forecast/{secondsToBeSlow}");
                response.EnsureSuccessStatusCode();

                _logger.LogInformation("Received successful response for weather forecast api");

                var responseContent = await response.Content.ReadAsStringAsync();
                var forecast = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(responseContent);

                return forecast;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching weather forecast");

                return Enumerable.Empty<WeatherForecast>();
            }
        }
    }
}
