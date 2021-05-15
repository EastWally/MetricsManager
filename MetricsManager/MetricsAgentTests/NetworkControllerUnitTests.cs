using MetricsAgent.Controllers.NetworkMetricsController;
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
    public class NetworkControllerUnitTests
    {
        private NetworkMetricsController controller;
        private Mock<ILogger<NetworkMetricsController>> mockLogger;
        private Mock<INetworkMetricsRepository> mockRepository;

        public NetworkControllerUnitTests()
        {
            mockRepository = new Mock<INetworkMetricsRepository>();
            mockLogger = new Mock<ILogger<NetworkMetricsController>>();

            controller = new NetworkMetricsController(mockLogger.Object, mockRepository.Object);
        }

        [Fact]
        public void GetErrorsCount_ReturnOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.Now.AddDays(-4);
            var toTime = DateTimeOffset.Now;
            mockRepository.Setup(repository => repository.GetByPeriod(It.IsAny<PeriodArgs>())).Returns(new List<NetworkMetric>()
            { new NetworkMetric() { Time = fromTime.ToUnixTimeSeconds(), Value = 1 } });

            //Act
            var result = controller.GetMetrics(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            //Arrange
            mockRepository.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

            //Act
            var result = controller.Create(new MetricsAgent.Controllers.NetworkMetricsController.Requests.NetworkMetricCreateRequest { Time = DateTimeOffset.Now, Value = 50 });

            //Assert
            mockRepository.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
        }
    }
}

