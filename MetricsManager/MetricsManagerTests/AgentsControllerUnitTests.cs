using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.Controllers.AgentsController;
using MetricsManager.DAL.Interfaces;
using AutoMapper;
using MetricsManager.Controllers.AgentsController.Requests;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private readonly AgentsController agentsController;
        private readonly Mock<ILogger<AgentsController>> mockLogger;
        private readonly Mock<IAgentRepository> mockRepository;
        private static IMapper _mapper;

        public AgentsControllerUnitTests()
        {
            mockRepository = new Mock<IAgentRepository>();
            mockLogger = new Mock<ILogger<AgentsController>>();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            agentsController = new AgentsController(mockRepository.Object, mockLogger.Object, _mapper);
        }


        [Fact]
        public void RegisterAgent_ReturnOk()
        {
            //Arrange
            var agentInfo = new RegisterAgentRequest() { AgentId = 1, AgentAddress = new Uri("http://localhost:51684/api/agent") };            

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
