using MetricsAgent.Controllers.HddMetricsController;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using MetricsAgent.DAL;

namespace MetricsAgentTests
{
    public class HddControllerUnitTests
    {
        private HddMetricsController controller;
        private Mock<ILogger<HddMetricsController>> mockLogger;
        private Mock<IHddMetricsRepository> mockRepository;

        public HddControllerUnitTests()
        {
            mockRepository = new Mock<IHddMetricsRepository>();
            mockLogger = new Mock<ILogger<HddMetricsController>>();

            controller = new HddMetricsController(mockLogger.Object, mockRepository.Object);
        }

        [Fact]
        public void GetErrorsCount_ReturnOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.Now.AddDays(-4);
            var toTime = DateTimeOffset.Now;
            mockRepository.Setup(repository => repository.GetByPeriod(It.IsAny<PeriodArgs>())).Returns(new List<HddMetric>()
            { new HddMetric() { Time = fromTime.ToUnixTimeSeconds(), Value = 1 } });

            //Act
            var result = controller.GetLeftSpace(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            //Arrange
            mockRepository.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();

            //Act
            var result = controller.Create(new MetricsAgent.Controllers.HddMetricsController.Requests.HddMetricCreateRequest { Time = DateTimeOffset.Now, Value = 50 });

            //Assert
            mockRepository.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
        }
    }
}

