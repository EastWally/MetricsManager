using System;

namespace MetricsAgent.Controllers.NetworkMetricsController.Requests
{
    public class NetworkMetricCreateRequest
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
