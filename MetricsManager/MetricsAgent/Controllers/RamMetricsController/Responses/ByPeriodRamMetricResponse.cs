using System.Collections.Generic;

namespace MetricsAgent.Controllers.RamMetricsController.Responses
{
    public class ByPeriodRamMetricResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
