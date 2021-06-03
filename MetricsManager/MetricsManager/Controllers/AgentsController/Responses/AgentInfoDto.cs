using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers.AgentsController.Responses
{
    public class AgentInfoDto
    {
        public int AgentId { get; set; }

        public string AgentUrl { get; set; }

        public bool Enabled { get; set; }
    }
}
