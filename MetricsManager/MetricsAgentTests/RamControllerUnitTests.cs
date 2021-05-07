using MetricsAgent.Controllers.RamMetricsController;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;

namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> mockLogger;
        private Mock<IRamMetricsRepository> mockRepository;

        public RamControllerUnitTests()
        {
            mockRepository = new Mock<IRamMetricsRepository>();
            mockLogger = new Mock<ILogger<RamMetricsController>>();

            controller = new RamMetricsController(mockLogger.Object, mockRepository.Object);
        }

        [Fact]
        public void GetErrorsCount_ReturnOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.Now.AddDays(-4);
            var toTime = DateTimeOffset.Now;
            mockRepository.Setup(repository => repository.GetByPeriod(It.IsAny<PeriodArgs>())).Returns(new List<RamMetric>()
            { new RamMetric() { Time = fromTime.ToUnixTimeSeconds(), Value = 1 } });

            //Act
            var result = controller.GetAvailableSpace(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            //Arrange
            mockRepository.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            //Act
            var result = controller.Create(new MetricsAgent.Controllers.RamMetricsController.Requests.RamMetricCreateRequest { Time = DateTimeOffset.Now, Value = 50 });

            //Assert
            mockRepository.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }
    }
}

