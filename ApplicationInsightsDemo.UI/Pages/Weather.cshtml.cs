using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationInsightsDemo.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApplicationInsightsDemo.UI.Pages
{
    public class WeatherModel : PageModel
    {
        public IEnumerable<WeatherForecast> weatherForecasts;

        private readonly IApiClient _apiClient;

        public WeatherModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task OnGet()
        {
            weatherForecasts = await _apiClient.GetWeatherForecastAsync();
        }
    }
}
