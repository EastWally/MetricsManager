using System;

namespace MetricsManager.Client.Responses
{
    public class RamMetricApiResponse
    {
        public int value { get; set; }

        public DateTimeOffset time { get; set; }
    }
}
