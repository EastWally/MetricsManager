using System.Collections;
using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetricApiResponse> metrics { get; set; }

        public AllCpuMetricsApiResponse()
        {
            metrics = new List<CpuMetricApiResponse>();
        }

        public IEnumerator<CpuMetricApiResponse> GetEnumerator()
        {
            return metrics.GetEnumerator();
        }
    }
}
