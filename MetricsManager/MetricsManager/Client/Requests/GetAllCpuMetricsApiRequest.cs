using System;

namespace MetricsManager.Client.Requests
{
    public class GetAllCpuMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public string ClientBaseAddress { get; set; }
    }
}
