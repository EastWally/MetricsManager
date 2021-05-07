using System;

namespace MetricsAgent.Controllers.NetworkMetricsController.Responses
{
    public class NetworkMetricDto
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
