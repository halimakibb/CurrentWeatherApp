using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICountriesAndCitiesService
    {
        // get all countries
        Task<CountryResponseModel> GetCountries();
        // get cities by country's two letter code
        Task<ResponseModel<CityModel>> GetCityByCountryCode(string countryCode);
       
        

    }
}
