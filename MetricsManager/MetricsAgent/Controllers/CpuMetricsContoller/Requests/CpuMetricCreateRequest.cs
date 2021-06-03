using System;

namespace MetricsAgent.Controllers.CpuMetricsContoller.Requests
{
    /// <summary>
    /// Метрика CPU
    /// </summary>
    public class CpuMetricCreateRequest
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
