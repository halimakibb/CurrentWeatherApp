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
    public class CurrentWeatherController : ControllerBase, ICurrentWeatherController
    {
        private ICurrentWeatherService _currentWeatherService;

        public CurrentWeatherController(ICurrentWeatherService currentWeatherService)
        {
            _currentWeatherService = currentWeatherService;
        }

        [HttpGet("[action]/{cityName}/{countryCode}")]
        public async Task<IActionResult> GetWeatherCondition(string cityName, string countryCode)
        {

            try
            {
                var weatherConditionModel = await _currentWeatherService.GetWeatherConditionByCity(cityName, countryCode);
                if (!weatherConditionModel.IsError)
                {
                    return Ok(weatherConditionModel.DataSingular);
                }
                else
                {
                    return BadRequest(weatherConditionModel.ErrorMessage);
                }
            }
            catch (Exception)
            {
                return BadRequest(String.Format("Error getting weather condition"));
            }

        }

    }
}
