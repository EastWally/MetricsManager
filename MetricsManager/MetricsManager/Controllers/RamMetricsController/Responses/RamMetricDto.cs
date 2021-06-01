﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers.RamMetricsController.Responses
{
    public class RamMetricDto
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }

        public int AgentId { get; set; }
    }
}
