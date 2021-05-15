using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public class PeriodArgs
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }
    }
}
