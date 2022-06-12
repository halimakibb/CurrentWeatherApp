using Models;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Repository.Interfaces;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XtramilesSolutionUnitTest.Services
{
    [TestFixture]
    public class CountriesAndCitiesTest
    {
        private ICountriesAndCitiesService _service;
        private Mock<ICountriesAndCitiesRepository> _mockRepository;
        [OneTimeSetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<ICountriesAndCitiesRepository>(MockBehavior.Strict);
            _service = new CountriesAndCitiesService(_mockRepository.Object);
        }
        [Test]
        [TestCase("GB")]
        [TestCase("ID")]
        [TestCase("AU")]
        [TestCase("JP")]
        public void GetCities_ShouldGetCountryCodeInResponse(string countryCode)
        {
            // Arrange - mock data from API

            var cityModel1 = new CityModel()
            {
                CityName = "London"
            };
            var cityModel2 = new CityModel()
            {
                CityName = "Manchester"
            };
            var cityModel3 = new CityModel()
            {
                CityName = "Leichester"
            };

            

            var cityModels = new List<CityModel>();
            cityModels.Add(cityModel1);
            cityModels.Add(cityModel2);     
            cityModels.Add(cityModel3);


            var jsonString = JsonConvert.SerializeObject(cityModels);
            var jsonModel = new JsonModel()
            {
                IsError = false,
                ErrorMessage = String.Empty,
                JsonData = jsonString
            };
            _mockRepository.Setup(r => r.RetrieveCitiesFromAPI(It.IsAny<string>())).
                Returns(Task.FromResult(jsonModel));

            var cityModelExpected1 = new CityModel()
            {
                CityName = "London",
                CountryCode = countryCode
            };
            var cityModelExpected2 = new CityModel()
            {
                CityName = "Manchester",
                CountryCode = countryCode
            };
            var cityModelExpected3 = new CityModel()
            {
                CityName = "Leichester",
                CountryCode = countryCode
            };

            var expectedCityModels = new List<CityModel>();
            expectedCityModels.Add(cityModelExpected1);
            expectedCityModels.Add(cityModelExpected2);
            expectedCityModels.Add(cityModelExpected3);

            var responseModel = new ResponseModel<CityModel>()
            {
                IsError = false,
                ErrorMessage = string.Empty,
                Data = expectedCityModels
            };

            // Act
            var actualData = _service.GetCityByCountryCode(countryCode).Result;

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(responseModel), JsonConvert.SerializeObject(actualData));
        }


        [Test]
        public void ExceptionFromAPI_ShouldReturnErrorMessage()
        {
            // Arrange
            var cityModelResponse = new ResponseModel<CityModel>()
            {
                IsError = true,
                ErrorMessage = "Error when processing City API. "
            };
            // any other exception beside HttpRequestException, as this particular exception is handled inside the repository layer.
            _mockRepository.Setup(r => r.RetrieveCitiesFromAPI(It.IsAny<string>())).
              Throws(new System.Exception());
            // Act
            var actualData = _service.GetCityByCountryCode("GB");

            // Assert

            Assert.AreEqual(JsonConvert.SerializeObject(actualData.Result), JsonConvert.SerializeObject(cityModelResponse));


        }
    }
}
