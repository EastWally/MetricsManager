using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.HddMetricsController.Responses
{
    public class ByPeriodHddMetricResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
