using System;

namespace MetricsAgent.Controllers.CpuMetricsContoller.Requests
{
    public class CpuMetricCreateRequest
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
