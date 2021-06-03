using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class AllNetworkMetricsApiResponse
    {
        public List<NetworkMetricApiResponse> metrics { get; set; }

        public AllNetworkMetricsApiResponse()
        {
            metrics = new List<NetworkMetricApiResponse>();
        }

        public IEnumerator<NetworkMetricApiResponse> GetEnumerator()
        {
            return metrics.GetEnumerator();
        }
    }
}
