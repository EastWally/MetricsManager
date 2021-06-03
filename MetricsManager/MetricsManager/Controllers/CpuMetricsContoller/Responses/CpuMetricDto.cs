using System;

namespace MetricsManager.Controllers.CpuMetricsContoller.Responses
{
    public class CpuMetricDto
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }

        public int AgentId { get; set; }
    }
}
