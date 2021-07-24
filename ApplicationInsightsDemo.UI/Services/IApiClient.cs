using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationInsightsDemo.UI.Services
{
    public interface IApiClient
    {
        Task<int> GetRandomNumberAsync();
        Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync();
    }
}