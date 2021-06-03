using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class AllHddMetricsApiResponse
    {
        public List<HddMetricApiResponse> metrics { get;set;}

        public AllHddMetricsApiResponse()
        {
            metrics = new List<HddMetricApiResponse>();
        }

        public IEnumerator<HddMetricApiResponse> GetEnumerator()
        {
            return metrics.GetEnumerator();
        }

    }
}
