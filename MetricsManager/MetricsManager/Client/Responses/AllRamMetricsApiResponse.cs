using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetricApiResponse> metrics { get; set; }

        public AllRamMetricsApiResponse()
        {
            metrics = new List<RamMetricApiResponse>();
        }

        public IEnumerator<RamMetricApiResponse> GetEnumerator()
        {
            return metrics.GetEnumerator();
        }
    }
}
