using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XtramilesSolution.Interfaces;

namespace XtramilesSolution.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesAndCitiesController : ControllerBase, ICountriesAndCitiesController
    {
        private ICountriesAndCitiesService _countriesAndCitiesService;

        public CountriesAndCitiesController(ICountriesAndCitiesService countriesAndCitiesService)
        {
            _countriesAndCitiesService = countriesAndCitiesService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCountries()
        {

            try
            {
                var countryResponseModel = await _countriesAndCitiesService.GetCountries();
                if (!countryResponseModel.IsError)
                {
                    return Ok(countryResponseModel.Data);
                }
                else
                {
                    return BadRequest(countryResponseModel.ErrorMessage);
                }
            }
            catch (Exception)
            {
                return BadRequest(String.Format("Error getting countries"));
            }

        }

        [HttpGet("[action]/{countryCode}")]
        public async Task<IActionResult> GetCities(string countryCode)
        {

            try
            {
                var cityResponseModel = await _countriesAndCitiesService.GetCityByCountryCode(countryCode);
                if (!cityResponseModel.IsError)
                {
                    return Ok(cityResponseModel.Data);
                }
                else
                {
                    return BadRequest(cityResponseModel.ErrorMessage);
                }
            }
            catch (Exception)
            {
                return BadRequest(String.Format("Error getting cities"));
            }

        }
    }
}
