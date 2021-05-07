using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.HddMetricsController.Responses
{
    public class HddMetricDto
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
