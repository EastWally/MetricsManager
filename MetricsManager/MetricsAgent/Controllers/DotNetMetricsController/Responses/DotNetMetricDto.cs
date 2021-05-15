using System;

namespace MetricsAgent.Controllers.DotNetMetricsController.Responses
{
    public class DotNetMetricDto
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
