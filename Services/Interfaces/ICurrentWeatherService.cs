using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICurrentWeatherService
    {
        // get city's weather data
        Task<ResponseModel<WeatherConditionModel>> GetWeatherConditionByCity(string cityName, string cityCode);
    }
}
