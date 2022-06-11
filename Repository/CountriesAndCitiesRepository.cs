using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public class CountriesAndCitiesRepository : ICountriesAndCitiesRepository
    {
        private IConfiguration _configuration;
        public CountriesAndCitiesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<JsonModel> RetrieveCitiesFromAPI(string countryCode)
        {
            using (var client = new HttpClient())
            {
                var jsonModel = new JsonModel()
                {
                    IsError = false,
                    ErrorMessage = string.Empty,
                    JsonData = string.Empty
                };
                try
                {
                    //get uri + key from appsettings.json
                    var baseUri = _configuration["CitiesAPIUri"];
                    client.DefaultRequestHeaders.Add("apikey", _configuration["CityAPIKey"]);
                    client.BaseAddress = new Uri(baseUri);
                    var response = await client.GetAsync(countryCode);

                    response.EnsureSuccessStatusCode();

                    // if request was successful, return json as string
                    var stringResult = await response.Content.ReadAsStringAsync();
                    jsonModel.JsonData = stringResult;
                    return jsonModel;
                }
                catch (HttpRequestException)
                {
                    jsonModel.IsError = true;
                    jsonModel.ErrorMessage = "Error connecting to API";
                    return jsonModel;
                }
            }
        }

        public async Task<JsonModel> RetrieveCountriesFromAPI()
        {
            using (var client = new HttpClient())
            {
                var jsonModel = new JsonModel()
                {
                    IsError = false,
                    ErrorMessage = string.Empty,
                    JsonData = string.Empty
                };
                try
                {
                    //get uri from appsettings.json
                    var baseUri = _configuration["CountriesAPIUri"];
                    client.BaseAddress = new Uri(baseUri);

                    var response = await client.GetAsync(String.Empty);

                    response.EnsureSuccessStatusCode();

                    // if request was successful, convert to JSON
                    var stringResult = await response.Content.ReadAsStringAsync();

                    jsonModel.JsonData = stringResult;
                    return jsonModel;
                }
                catch (HttpRequestException)
                {
                    jsonModel.IsError = true;
                    jsonModel.ErrorMessage = "Error connecting to API";
                    return jsonModel;
                }
            }
        }

    }
}
