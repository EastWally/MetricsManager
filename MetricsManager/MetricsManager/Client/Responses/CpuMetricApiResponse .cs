using System;

namespace MetricsManager.Client.Responses
{
    public class CpuMetricApiResponse
    {
        public int value { get; set; }

        public DateTimeOffset time { get; set; }
    }
}
