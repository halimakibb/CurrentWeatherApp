using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountriesAndCitiesService : ICountriesAndCitiesService
    {
        private ICountriesAndCitiesRepository _repository;
        public CountriesAndCitiesService(ICountriesAndCitiesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<CityModel>> GetCityByCountryCode(string countryCode)
        {
            using (var client = new HttpClient())
            {
                // initialize city list of object
                List<CityModel> city = new List<CityModel>();
                var cityResponse = new ResponseModel<CityModel>()
                {
                    IsError = false,
                    ErrorMessage = string.Empty
                };

                try
                {
                    //get data from Repository/API
                    var jsonModel = await _repository.RetrieveCitiesFromAPI(countryCode);
                    if (!jsonModel.IsError)
                    {
                        var cityList = JsonConvert.DeserializeObject<List<CityModel>>(jsonModel.JsonData);

                        //assign country code to each city
                        foreach (var cityResult in cityList)
                        {
                            cityResult.CountryCode = countryCode;
                        }
                        cityResponse.Data = cityList;
                    }
                    else
                    {
                        cityResponse.IsError = true;
                        cityResponse.ErrorMessage = jsonModel.ErrorMessage;
                    }
                    return cityResponse;
                }
                catch (JsonSerializationException)
                {
                    cityResponse.IsError = true;
                    cityResponse.ErrorMessage = "Error when converting from City API data";
                    return cityResponse;
                }
                catch (Exception ex)
                {
                    cityResponse.IsError = true;
                    cityResponse.ErrorMessage = "Error when processing City API";
                    return cityResponse;
                }
            }
        }

        public async Task<CountryResponseModel> GetCountries()
        {
            using (var client = new HttpClient())
            {
                // initiate country list of object
                List<CountryModel> countries = new List<CountryModel>();
                var countriesResponse = new CountryResponseModel();
                try
                {
                    // get data from repository/API
                    var jsonModel = await _repository.RetrieveCountriesFromAPI();
                    if (!jsonModel.IsError)
                    {
                        countriesResponse = JsonConvert.DeserializeObject<CountryResponseModel>(jsonModel.JsonData);
                    }
                    else
                    {
                        countriesResponse.IsError = true;
                        countriesResponse.ErrorMessage = jsonModel.ErrorMessage;
                    }
                    return countriesResponse;
                }

                catch (JsonSerializationException)
                {
                    countriesResponse.IsError = true;
                    countriesResponse.ErrorMessage = "Error when converting from Country API data";
                    return countriesResponse;
                }
                catch (Exception ex)
                {
                    countriesResponse.IsError = true;
                    countriesResponse.ErrorMessage = "Error when processing Country API ";
                    return countriesResponse;
                }
            }
        }

    }
}
