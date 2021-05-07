using System;

namespace MetricsAgent.Controllers.RamMetricsController.Requests
{
    public class RamMetricCreateRequest
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
