using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers.DotNetMetricsController.Responses
{
    public class ByPeriodDotNetMetricResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}
