using System;

namespace MetricsManager
{
    public class AgentInfo
    {
        public AgentInfo()
        { 
        }


        public AgentInfo(int agentId, Uri agentAddress)
        {
            AgentId = agentId;
            AgentAddress = agentAddress;
        }

        public int AgentId { get; }

        public Uri AgentAddress { get; }

    }
}