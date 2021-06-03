using System;

namespace MetricsManager.Client.Responses
{
    public class NetworkMetricApiResponse
    {
        public int value { get; set; }

        public DateTimeOffset time { get; set; }
    }
}
