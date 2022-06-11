using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICountriesAndCitiesRepository
    {
        // Get All countries on initialisation
        public Task<JsonModel> RetrieveCountriesFromAPI();
        // Get cities belonging to one country
        public Task<JsonModel> RetrieveCitiesFromAPI(string countryCode);
    }
}
