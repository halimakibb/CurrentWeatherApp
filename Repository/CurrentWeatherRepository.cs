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
    public class CurrentWeatherRepository : ICurrentWeatherRepository
    {
        private IConfiguration _configuration;
        public CurrentWeatherRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    
        public async Task<JsonModel> RetrieveWeatherFromAPI(string cityName, string countryCode)
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
                    //get uri and api key from appsettings.json
                    var baseUri = _configuration["DefaultAPIUri"];
                    var apiKey = _configuration["APIKey"];

                    // city name format = {city name}, {country two letter code}
                    var fullCityName = string.Format("{0}, {1}", cityName, countryCode);

                    client.BaseAddress = new Uri(baseUri);

                    //parameter for the API, use imperial units for Fahrenheit
                    var apiParameter = string.Format("/data/2.5/weather?q={0}&appid={1}&units=imperial", fullCityName, apiKey);
                    var response = await client.GetAsync(apiParameter);

                    response.EnsureSuccessStatusCode();

                    // if request was successful, return json as string
                    var stringResult = await response.Content.ReadAsStringAsync();
                    jsonModel.JsonData = stringResult; 
                    return jsonModel;
                }
                catch(HttpRequestException)
                {
                    jsonModel.IsError = true;
                    jsonModel.ErrorMessage = "Error connecting to API";
                    return jsonModel;
                }
            }
        }
    }
}
