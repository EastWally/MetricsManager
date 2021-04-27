using MetricsAgent.Controllers;
using MetricsAgent.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController controller;

        public RamControllerUnitTests()
        {
            controller = new RamMetricsController();
        }

        [Fact]
        public void GetErrorsCount_ReturnOk()
        {
            //Act
            var result = controller.GetAvailableSpace();

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}

