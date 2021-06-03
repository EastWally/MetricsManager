using System;

namespace MetricsManager.Controllers.NetworkMetricsController.Responses
{
    public class NetworkMetricDto
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }

        public int AgentId { get; set; }
    }
}
