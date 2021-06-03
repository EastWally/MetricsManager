using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.DotNetMetricsController.Requests
{
    /// <summary>
    /// Метрика DotNet
    /// </summary>
    public class DotNetMetricCreateRequest
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
