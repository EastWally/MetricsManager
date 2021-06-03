using System.Collections.Generic;

namespace MetricsManager.Controllers.RamMetricsController.Responses
{
    public class ByPeriodRamMetricResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
