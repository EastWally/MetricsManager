using System.Collections.Generic;

namespace MetricsManager.Controllers.NetworkMetricsController.Responses
{
    public class ByPeriodNetworkMetricResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
