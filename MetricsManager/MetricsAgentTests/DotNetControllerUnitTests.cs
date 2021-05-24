using MetricsAgent.Controllers.DotNetMetricsController;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using MetricsAgent.DAL;
using MetricsAgent;
using AutoMapper;

namespace MetricsAgentTests
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> mockLogger;
        private Mock<IDotNetMetricsRepository> mockRepository;
        private static IMapper _mapper;

        public DotNetControllerUnitTests()
        {
            mockRepository = new Mock<IDotNetMetricsRepository>();
            mockLogger = new Mock<ILogger<DotNetMetricsController>>();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            controller = new DotNetMetricsController(mockLogger.Object, mockRepository.Object, _mapper);
        }

        [Fact]
        public void GetErrorsCount_ReturnOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.Now.AddDays(-4);
            var toTime = DateTimeOffset.Now;
            mockRepository.Setup(repository => repository.GetByPeriod(It.IsAny<PeriodArgs>())).Returns(new List<DotNetMetric>()
            { new DotNetMetric() { Time = fromTime.ToUnixTimeSeconds(), Value = 1 } });

            //Act
            var result = controller.GetErrorsCount(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            //Arrange
            mockRepository.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            //Act
            var result = controller.Create(new MetricsAgent.Controllers.DotNetMetricsController.Requests.DotNetMetricCreateRequest { Time = DateTimeOffset.Now, Value = 50 });

            //Assert
            mockRepository.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }
    }
}

