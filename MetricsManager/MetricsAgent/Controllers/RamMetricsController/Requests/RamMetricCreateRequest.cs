using System;

namespace MetricsAgent.Controllers.RamMetricsController.Requests
{
    /// <summary>
    /// Метрика RAM
    /// </summary>
    public class RamMetricCreateRequest
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
