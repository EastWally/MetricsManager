﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Models
{
    public class CpuMetric
    {
        public int Value { get; set; }

        public long Time { get; set; }
    }
}
