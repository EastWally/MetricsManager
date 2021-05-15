using System;

namespace MetricsAgent.Controllers.CpuMetricsContoller.Responses
{
    public class CpuMetricDto
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
