using System;

namespace MetricsAgent.Controllers.NetworkMetricsController.Requests
{
    /// <summary>
    /// Метрика Network
    /// </summary>
    public class NetworkMetricCreateRequest
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
