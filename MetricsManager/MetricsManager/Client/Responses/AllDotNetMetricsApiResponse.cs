using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class AllDotNetMetricsApiResponse
    {
        public List<DotNetMetricApiResponse> metrics { get; set; }

        public AllDotNetMetricsApiResponse()
        {
            metrics = new List<DotNetMetricApiResponse>();
        }

        public IEnumerator<DotNetMetricApiResponse> GetEnumerator()
        {
            return metrics.GetEnumerator();
        }
    }
}
