using MetricsAgent.Controllers.CpuMetricsContoller;
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
    public class CpuControllerUnitTests
    {
        private CpuMetricsController controller;
        private Mock<ILogger<CpuMetricsController>> mockLogger;
        private Mock<ICpuMetricsRepository> mockRepository;

        public CpuControllerUnitTests()
        {
            mockRepository = new Mock<ICpuMetricsRepository>();
            mockLogger = new Mock<ILogger<CpuMetricsController>>();

            controller = new CpuMetricsController(mockLogger.Object, mockRepository.Object);
        }

        [Fact]
        public void GetMetrics_ReturnOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.Now.AddDays(-4);
            var toTime = DateTimeOffset.Now;

            mockRepository.Setup(repository => repository.GetByPeriod(It.IsAny<PeriodArgs>())).Returns(new List<CpuMetric>() 
            { new CpuMetric() { Time = fromTime.ToUnixTimeSeconds(), Value = 1 } });

            //Act
            var result = controller.GetMetrics(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            //Arrange
            mockRepository.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            //Act
            var result = controller.Create(new MetricsAgent.Controllers.CpuMetricsContoller.Requests.CpuMetricCreateRequest { Time = DateTimeOffset.Now, Value = 50 });

            //Assert
            mockRepository.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

    }
}

