using System.Collections.Generic;


namespace MetricsAgent.Controllers.CpuMetricsContoller.Responses
{
    public class ByPeriodCpuMetricResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
