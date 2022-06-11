using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XtramilesSolution.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesAndCitiesController : ControllerBase
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
                    return Ok(countryResponseModel);
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

        [HttpGet("[action]/{countryName}")]
        public async Task<IActionResult> GetCities(string countryName)
        {

            try
            {
                var countryResponseModel = await _countriesAndCitiesService.GetCityByCountryName(countryName);
                if (!countryResponseModel.IsError)
                {
                    return Ok(countryResponseModel);
                }
                else
                {
                    return BadRequest(countryResponseModel.ErrorMessage);
                }
            }
            catch (Exception)
            {
                return BadRequest(String.Format("Error getting cities"));
            }

        }
    }
}
