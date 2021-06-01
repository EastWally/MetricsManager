using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.Controllers.DotNetMetricsController;
using MetricsManager.DAL.Interfaces;
using AutoMapper;
using MetricsManager;

namespace MetricsManagerTests
{
    public class DotNetControllerUnitTests
    {
        private readonly DotNetMetricsController controller;
        private readonly Mock<ILogger<DotNetMetricsController>> mockLogger;
        private readonly Mock<IDotNetMetricsRepository> mockRepository;
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
        public void GetMetricsFromAgent_ReturnOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.Now.AddDays(-4);
            var toTime = DateTimeOffset.Now;

            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsAllCluster_ReturnOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.Now.AddDays(-4);
            var toTime = DateTimeOffset.Now;

            //Act
            var result = controller.GetMetricsAllCluster(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
