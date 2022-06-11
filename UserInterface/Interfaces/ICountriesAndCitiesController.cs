using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace XtramilesSolution.Interfaces
{
    public interface ICountriesAndCitiesController
    {
        Task<IActionResult> GetCountries();
        Task<IActionResult> GetCities(string countryCode);
    }
}
