﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.HddMetricsController.Requests
{
    public class HddMetricCreateRequest
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
