using System;

namespace MetricsManager.DAL
{
    public class PeriodArgs
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }
    }
}
