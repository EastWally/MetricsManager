using System;

namespace MetricsManager.Client.Responses
{
    public class HddMetricApiResponse
    {
        public int value { get; set; }

        public DateTimeOffset time { get; set; }
    }
}
