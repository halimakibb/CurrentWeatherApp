using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICurrentWeatherRepository
    {
        //get json from API, provide JSON to service
        public Task<JsonModel> RetrieveWeatherFromAPI(string cityName, string countryCode);
    }
}
