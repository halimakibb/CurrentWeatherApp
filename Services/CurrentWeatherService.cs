using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CurrentWeatherService : ICurrentWeatherService
    {
        private ICurrentWeatherRepository _repository;
        public CurrentWeatherService(ICurrentWeatherRepository repository)
        {
            _repository = repository;
        }
        private decimal CalculateDewPoint(decimal celcius, decimal humidity)
        {
            // calculate dew point with simple dew point calculation formula: Td = T - ((100 - RH)/5)
            return Math.Round(celcius - Decimal.Divide((100 - humidity), 5), 2);
        }
        private decimal ConvertFahrenheitToCelcius(decimal fahrenheit)
        {   
            // standard fahrenheit to celcius conversion formula. (F - 32) / 1.8 = C
            return Math.Round(Decimal.Divide((fahrenheit - 32), 1.8m), 2);
        }
        private DateTime GetLocalTime(int timezone)
        {
            return DateTime.Now.ToUniversalTime().AddSeconds(timezone);
        }


        public async Task<ResponseModel<WeatherConditionModel>> GetWeatherConditionByCity(string cityName, string countryCode)
        {
            using (var client = new HttpClient())
            {
                // initialise weather condition response object
                var weatherConditionResponse = new ResponseModel<WeatherConditionModel>();
                weatherConditionResponse.IsError = false;
                weatherConditionResponse.ErrorMessage = String.Empty;

                var weatherCondition = new WeatherConditionModel();

                try
                {
                    //get data from Repository/API
                    var jsonModel = await _repository.RetrieveWeatherFromAPI(cityName, countryCode);

                    if (!jsonModel.IsError)
                    {
                        weatherCondition = JsonConvert.DeserializeObject<WeatherConditionModel>(jsonModel.JsonData);

                        // convert fahrenheit to celcius and calculate dewpoint
                        
                        weatherCondition.Main.TempC = "--";
                        weatherCondition.DewPoint = "--";

                        decimal tempF = 0;
                        if (Decimal.TryParse(weatherCondition.Main.Temp, out tempF))
                        {
                            // convert fahrenheit to celcius
                            var tempC = ConvertFahrenheitToCelcius(tempF);
                            weatherCondition.Main.TempC = tempC.ToString();

                            // calculate dewpoint from celcius and humidity
                            decimal humidity = 0;
                            
                            if (Decimal.TryParse(weatherCondition.Main.Humidity, out humidity))
                            {
                                var dewPoint = CalculateDewPoint(tempC, humidity);
                                weatherCondition.DewPoint = dewPoint.ToString();
                            }
                            else
                            {
                                //continue process even if the dew point cannot be calculated
                                weatherConditionResponse.IsError = false;
                                weatherConditionResponse.ErrorMessage += "Humidity is not in the correct format. ";
                            }
                        }
                        else
                        {
                            //continue process even if the temperature cannot be calculated
                            weatherConditionResponse.IsError = false;
                            weatherConditionResponse.ErrorMessage += "Temperature is not in the correct format. ";
                        }

                        // get local time based on timezone
                        weatherCondition.Time = GetLocalTime(weatherCondition.Timezone);

                        weatherConditionResponse.DataSingular = weatherCondition;
                    }
                    else
                    {
                        weatherConditionResponse.IsError = true;
                        weatherConditionResponse.ErrorMessage = jsonModel.ErrorMessage;
                    }
                    return weatherConditionResponse;
                }
                catch (JsonSerializationException)
                {
                    weatherConditionResponse.IsError = true;
                    weatherConditionResponse.ErrorMessage += "Error when converting from Weather Condition API. ";
                    return weatherConditionResponse;
                }
                catch (Exception ex)
                {
                    if (ex is OverflowException)
                    {
                        weatherConditionResponse.IsError = true;
                        weatherConditionResponse.ErrorMessage += "Error when calculating temperature/humidity. ";
                    }
                    else
                    {
                        weatherConditionResponse.IsError = true;
                        weatherConditionResponse.ErrorMessage += "Error when processing Weather API data. ";
                    }
                    return weatherConditionResponse;
                }
            }
        }
    }
}
