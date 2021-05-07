using System.Collections.Generic;

namespace MetricsAgent.Controllers.NetworkMetricsController.Responses
{
    public class ByPeriodNetworkMetricResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
