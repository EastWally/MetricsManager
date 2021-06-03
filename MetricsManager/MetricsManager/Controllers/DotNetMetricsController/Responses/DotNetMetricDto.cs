using System;

namespace MetricsManager.Controllers.DotNetMetricsController.Responses
{
    public class DotNetMetricDto
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }

        public int AgentId { get; set; }
    }
}
