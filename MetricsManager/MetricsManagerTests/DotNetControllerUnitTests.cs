using MetricsManager.Controllers;
using MetricsManager.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController controller;

        public DotNetControllerUnitTests()
        {
            controller = new DotNetMetricsController();
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
