using Models;
using Models.WeatherConditionDetail;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Repository.Interfaces;
using Services;
using Services.Interfaces;
using System.Threading.Tasks;

namespace XtramilesSolutionUnitTest
{
    [TestFixture]
    public class CurrentWeatherServiceTest
    {
        private ICurrentWeatherService _service;
        private Mock<ICurrentWeatherRepository> _mockRepository;
        [OneTimeSetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<ICurrentWeatherRepository>(MockBehavior.Strict);
            _service = new CurrentWeatherService(_mockRepository.Object);   
        }
        [Test]
        [TestCase("212", "100")]
        [TestCase("32", "0")]
        [TestCase("70.69", "21.49")]
        [TestCase("50.32", "10.18")]
        public void ConvertFahrenheitToCelcius_ShouldConvert(string inputTempF, string expectedTempC)
        {
            // Arrange - mock data from API
            var mainWeatherModel = new MainModel()
            {
                Temp = inputTempF
            };
            var weatherConditionModel = new WeatherConditionModel()
            {
                Main = mainWeatherModel
            };
            var stringResult = JsonConvert.SerializeObject(weatherConditionModel);
            var jsonModel = new JsonModel()
            {
                IsError = false,
                ErrorMessage = string.Empty,
                JsonData = stringResult
            };
            _mockRepository.Setup(r => r.RetrieveWeatherFromAPI(It.IsAny<string>(), It.IsAny<string>())).
                Returns(Task.FromResult(jsonModel));

            // Act
            var actualData = _service.GetWeatherConditionByCity("London", "GB");

            // Assert
            var actualCelsius = actualData.Result.DataSingular.Main.TempC;
            Assert.AreEqual(expectedTempC, actualCelsius);
        }

        [Test]
        [TestCase("107.6", "60", "34")]
        [TestCase("89.6", "30.72", "18.14")]
        [TestCase("70.81", "83.28", "18.22")]
        public void CalculateDewPoint_ShouldCalculate(string inputTempF, string inputHumidity, string expectedDewPoint)
        {
            // Arrange - mock data from API
            var mainWeatherModel = new MainModel()
            {
                Temp = inputTempF,
                Humidity = inputHumidity
            };
            var weatherConditionModel = new WeatherConditionModel()
            {
                Main = mainWeatherModel
            };
            var stringResult = JsonConvert.SerializeObject(weatherConditionModel);
            var jsonModel = new JsonModel()
            {
                IsError = false,
                ErrorMessage = string.Empty,
                JsonData = stringResult
            };
            _mockRepository.Setup(r => r.RetrieveWeatherFromAPI(It.IsAny<string>(), It.IsAny<string>())).
                Returns(Task.FromResult(jsonModel));

            // Act
            var actualData = _service.GetWeatherConditionByCity("London", "GB");
            var actualDewPoint = actualData.Result.DataSingular.DewPoint;

            // Assert
            Assert.AreEqual(expectedDewPoint, actualDewPoint);
        }

        [Test]
        [TestCase("")]
        [TestCase("N/A")]
        [TestCase("--")]
        public void ConvertFahrenheitToCelcius_ShouldFail(string inputTempF)
        {
            var mainWeatherModel = new MainModel()
            {
                Temp = inputTempF
            };
            var weatherConditionModel = new WeatherConditionModel()
            {
                Main = mainWeatherModel
            };
            var stringResult = JsonConvert.SerializeObject(weatherConditionModel);
            var jsonModel = new JsonModel()
            {
                IsError = false,
                ErrorMessage = string.Empty,
                JsonData = stringResult
            };
            _mockRepository.Setup(r => r.RetrieveWeatherFromAPI(It.IsAny<string>(), It.IsAny<string>())).
                Returns(Task.FromResult(jsonModel));

            // Act
            var actualData = _service.GetWeatherConditionByCity("London", "GB");

            // Assert
            var actualCelsius = actualData.Result.DataSingular.Main.TempC;
            var actualErrorMessage = actualData.Result.ErrorMessage;
            var actualIsError = actualData.Result.IsError;
            Assert.AreEqual("--", actualCelsius);
            Assert.AreEqual("Temperature is not in the correct format. ", actualErrorMessage);
            Assert.AreEqual(false, actualIsError);
        }

        [TestCase("80.6", "", "Humidity is not in the correct format. ")]
        [TestCase("", "69", "Temperature is not in the correct format. ")]
        [TestCase("--", "", "Temperature is not in the correct format. ")]
        public void CalculateDewPoint_ShouldFail(string inputTempF, string inputHumidity, string expectedErrorMessage)
        {
            // Arrange - mock data from API
            var mainWeatherModel = new MainModel()
            {
                Temp = inputTempF,
                Humidity = inputHumidity
            };
            var weatherConditionModel = new WeatherConditionModel()
            {
                Main = mainWeatherModel
            };
            var stringResult = JsonConvert.SerializeObject(weatherConditionModel);
            var jsonModel = new JsonModel()
            {
                IsError = false,
                ErrorMessage = string.Empty,
                JsonData = stringResult
            };
            _mockRepository.Setup(r => r.RetrieveWeatherFromAPI(It.IsAny<string>(), It.IsAny<string>())).
                Returns(Task.FromResult(jsonModel));

            // Act
            var actualData = _service.GetWeatherConditionByCity("London", "GB");

            // Assert
            var actualDewPoint = actualData.Result.DataSingular.DewPoint;
            var actualErrorMessage = actualData.Result.ErrorMessage;
            var actualIsError = actualData.Result.IsError;
            Assert.AreEqual("--", actualDewPoint);
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
            Assert.AreEqual(false, actualIsError);
        }


    }
}