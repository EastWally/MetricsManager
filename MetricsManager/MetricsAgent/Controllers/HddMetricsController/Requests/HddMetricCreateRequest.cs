using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.HddMetricsController.Requests
{
    /// <summary>
    /// Метрика Hdd
    /// </summary>
    public class HddMetricCreateRequest
    {
        /// <summary>
        /// Значение метрики
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Дата метрики
        /// </summary>
        public DateTimeOffset Time { get; set; }
    }
}
