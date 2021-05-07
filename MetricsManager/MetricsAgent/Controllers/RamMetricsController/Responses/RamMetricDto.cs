using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.RamMetricsController.Responses
{
    public class RamMetricDto
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
