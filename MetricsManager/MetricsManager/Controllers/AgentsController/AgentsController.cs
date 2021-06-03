using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsManager.Controllers.AgentsController.Requests;
using MetricsManager.Controllers.AgentsController.Responses;
using System;
using MetricsManager.DAL.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using MetricsManager.DAL.Models;

namespace MetricsManager.Controllers.AgentsController
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentRepository _agents;
        private readonly IMapper _mapper;

        public AgentsController(IAgentRepository agents, ILogger<AgentsController> logger, IMapper mapper)
        {
            _agents = agents;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] RegisterAgentRequest agentInfo)
        {
            _logger.LogInformation("RegisterAgent AgentAddress:{0} AgentId {1}", agentInfo.AgentAddress, agentInfo.AgentId);

            _agents.RegisterAgent(new Agents() { AgentId = agentInfo.AgentId, AgentUrl = agentInfo.AgentAddress.AbsoluteUri, Enabled = true }) ;
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("EnableAgentById: {0}", agentId);
            _agents.EnableAgentById(agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("DisableAgentById: {0}", agentId);
            _agents.DisableAgentById(agentId);
            return Ok();
        }

        [HttpGet("get")]
        public IActionResult GetRegisteredAgents()
        {
            _logger.LogInformation("GetRegisteredAgents");
            var agents = _agents.GetAgents();

            var response = new GetRegisteredAgentsResponse()
            {
                Agents = _mapper.Map<IEnumerable<Agents>, List<AgentInfoDto>>((IEnumerable<Agents>)agents)
            };

            return Ok(response);
        }
    }
}
