using MetricsAgent.Controllers;
using MetricsAgent.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddControllerUnitTests
    {
        private HddMetricsController controller;

        public HddControllerUnitTests()
        {
            controller = new HddMetricsController();
        }

        [Fact]
        public void GetErrorsCount_ReturnOk()
        {
            //Act
            var result = controller.GetLeftSpace();

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}

