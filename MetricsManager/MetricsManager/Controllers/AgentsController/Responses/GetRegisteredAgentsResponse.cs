using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers.AgentsController.Responses
{
    public class GetRegisteredAgentsResponse
    {
        public List<AgentInfoDto> Agents { get; set; }
    }
}
