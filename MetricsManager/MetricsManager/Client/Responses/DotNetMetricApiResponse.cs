using System;

namespace MetricsManager.Client.Responses
{
    public class DotNetMetricApiResponse
    {
        public int value { get; set; }

        public DateTimeOffset time { get; set; }
    }
}
