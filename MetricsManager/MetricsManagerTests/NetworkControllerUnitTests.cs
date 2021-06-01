using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.Controllers.NetworkMetricsController;
using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager;

namespace MetricsManagerTests
{
    public class NetworkControllerUnitTests
    {
        private readonly NetworkMetricsController controller;
        private readonly Mock<ILogger<NetworkMetricsController>> mockLogger;
        private readonly Mock<INetworkMetricsRepository> mockRepository;
        private static IMapper _mapper;

        public NetworkControllerUnitTests()
        {
            mockRepository = new Mock<INetworkMetricsRepository>();
            mockLogger = new Mock<ILogger<NetworkMetricsController>>();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            controller = new NetworkMetricsController(mockLogger.Object, mockRepository.Object, _mapper);
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
