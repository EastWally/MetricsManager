using System;
namespace MetricsManager.Controllers.AgentsController.Requests
{
    public class RegisterAgentRequest
    {
        public int AgentId { get; set; }

        public Uri AgentAddress { get; set; }
    }
}
