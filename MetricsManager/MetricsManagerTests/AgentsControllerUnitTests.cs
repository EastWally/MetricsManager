using MetricsManager;
using MetricsManager.Controllers;
using MetricsManager.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController agentsController;

        public AgentsControllerUnitTests()
        {
            AgentsHolder holder = new AgentsHolder();
            agentsController = new AgentsController(holder);
        }


        [Fact]
        public void RegisterAgent_ReturnOk()
        {
            //Arrange
            var agentInfo = new AgentInfo(1, new Uri("http://localhost:51684/api/agent"));            

            //Act
            var result = agentsController.RegisterAgent(agentInfo);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void EnableAgentById_ReturnOk()
        {
            //Arrange
            var agentId = 1;

            //Act
            var result = agentsController.EnableAgentById(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void DisableAgentById_ReturnOk()
        {
            //Arrange
            var agentId = 1;

            //Act
            var result = agentsController.DisableAgentById(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetRegisteredAgents_ReturnOk()
        {
            //Act
            var result = agentsController.GetRegisteredAgents();

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
