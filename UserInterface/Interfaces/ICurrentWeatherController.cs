using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace XtramilesSolution.Interfaces
{
    public interface ICurrentWeatherController
    {
        Task<IActionResult> GetWeatherCondition(string cityName, string countryCode);
    }
}
