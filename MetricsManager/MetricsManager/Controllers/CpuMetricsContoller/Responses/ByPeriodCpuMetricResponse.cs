using System.Collections.Generic;


namespace MetricsManager.Controllers.CpuMetricsContoller.Responses
{
    public class ByPeriodCpuMetricResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
