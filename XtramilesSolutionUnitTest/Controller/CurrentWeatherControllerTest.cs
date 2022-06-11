using Models;
using Models.WeatherConditionDetail;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Repository.Interfaces;
using Services;
using Services.Interfaces;
using System.Threading.Tasks;
using XtramilesSolution.Controllers;
using XtramilesSolution.Interfaces;

namespace XtramilesSolutionUnitTest
{
    [TestFixture]
    public class CurrentWeatherControllerTest
    {
        private ICurrentWeatherController _controller;
        private Mock<ICurrentWeatherService> _mockService;
        [OneTimeSetUp]
        public void SetUp()
        {
            _mockService = new Mock<ICurrentWeatherService>(MockBehavior.Strict);
            _controller = new CurrentWeatherController(_mockService.Object);   
        }
        [Test]
        public void WeatherDataRetrieved_ShouldReturnOK()
        {
            // Arrange - mock data from API
            var mainWeatherModel = new MainModel()
            {
                Temp = "26"
            };
            var weatherConditionModel = new WeatherConditionModel()
            {
                Main = mainWeatherModel
            };
            var response = new ResponseModel<WeatherConditionModel>()
            {
                IsError = false,
                ErrorMessage = string.Empty,
                DataSingular = weatherConditionModel
            };
           
            _mockService.Setup(r => r.GetWeatherConditionByCity(It.IsAny<string>(), It.IsAny<string>())).
                Returns(Task.FromResult(response));

            // Act
            var actualResponse = _controller.GetWeatherCondition("London", "GB");
            // Assert
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>(actualResponse.Result);
        }
        [Test]
        public void WeatherDataNotRetrieved_ShouldReturnBadRequest()
        {
            // Arrange - mock data from API
            var mainWeatherModel = new MainModel()
            {
                Temp = "26"
            };
            var weatherConditionModel = new WeatherConditionModel()
            {
                Main = mainWeatherModel
            };
            var response = new ResponseModel<WeatherConditionModel>()
            {
                IsError = true,
                ErrorMessage = "Error.",
                DataSingular = weatherConditionModel
            };

            _mockService.Setup(r => r.GetWeatherConditionByCity(It.IsAny<string>(), It.IsAny<string>())).
                Returns(Task.FromResult(response));

            // Act
            var actualResponse = _controller.GetWeatherCondition("London", "GB");
            // Assert
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actualResponse.Result);
        }
    }
}