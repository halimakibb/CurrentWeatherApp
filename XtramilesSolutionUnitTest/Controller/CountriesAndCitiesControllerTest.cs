using Models;
using Models.WeatherConditionDetail;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Repository.Interfaces;
using Services;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using XtramilesSolution.Controllers;
using XtramilesSolution.Interfaces;

namespace XtramilesSolutionUnitTest
{
    [TestFixture]
    public class CountriesAndCitiesControllerTest
    {
        private ICountriesAndCitiesController _controller;
        private Mock<ICountriesAndCitiesService> _mockService;
        [OneTimeSetUp]
        public void SetUp()
        {
            _mockService = new Mock<ICountriesAndCitiesService>(MockBehavior.Strict);
            _controller = new CountriesAndCitiesController(_mockService.Object);   
        }
        [Test]
        public void CityDataRetrieved_ShouldReturnOK()
        {
            // Arrange - mock data from API
            var cityModel = new CityModel()
            {
                CityName = "Yogyakarta",
                CountryCode = "ID"
            };
            var response = new ResponseModel<CityModel>()
            {
                IsError = false,
                ErrorMessage = string.Empty,
                DataSingular = cityModel
            };
           
            _mockService.Setup(r => r.GetCityByCountryCode(It.IsAny<string>())).
                Returns(Task.FromResult(response));

            // Act
            var actualResponse = _controller.GetCities("GB");
            // Assert
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>(actualResponse.Result);
        }
        [Test]
        public void CityDataNotRetrieved_ShouldReturnBadRequest()
        {
            // Arrange - mock data from API
            var cityModel = new CityModel()
            {
                CityName = "Yogyakarta",
                CountryCode = "ID"
            };
            var response = new ResponseModel<CityModel>()
            {
                IsError = true,
                ErrorMessage = "Error.",
                DataSingular = cityModel
            };

            _mockService.Setup(r => r.GetCityByCountryCode(It.IsAny<string>())).
                Returns(Task.FromResult(response));

            // Act
            var actualResponse = _controller.GetCities("GB");
            // Assert
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actualResponse.Result);
        }

        [Test]
        public void CountryDataRetrieved_ShouldReturnOK()
        {
            // Arrange - mock data from API
            var countryModel = new CountryModel()
            {
                CountryName = "Indonesia",
                CountryCode = "ID"
            };

            var countryModelList = new List<CountryModel>();
            var response = new CountryResponseModel()
            {
                IsError = false,
                ErrorMessage = string.Empty,
                Data = countryModelList
            };

            _mockService.Setup(r => r.GetCountries()).
                Returns(Task.FromResult(response));

            // Act
            var actualResponse = _controller.GetCountries();
            // Assert
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>(actualResponse.Result);
        }
        [Test]
        public void CountryDataNotRetrieved_ShouldReturnBadRequest()
        {
            // Arrange - mock data from API
            var countryModel = new CountryModel()
            {
                CountryName = "Indonesia",
                CountryCode = "ID"
            };

            var countryModelList = new List<CountryModel>();
            var response = new CountryResponseModel()
            {
                IsError = true,
                ErrorMessage = "Error",
                Data = countryModelList
            };

            _mockService.Setup(r => r.GetCountries()).
                Returns(Task.FromResult(response));

            // Act
            var actualResponse = _controller.GetCountries();
            // Assert
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actualResponse.Result);
        }
    }
}