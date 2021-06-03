using System;

namespace MetricsManager.DAL
{
    public class PeriodAgentArgs
    {
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }
    }
}
