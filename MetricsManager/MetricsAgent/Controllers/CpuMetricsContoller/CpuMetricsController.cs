using MetricsAgent.Controllers.CpuMetricsContoller.Requests;
using MetricsAgent.Controllers.CpuMetricsContoller.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL;

namespace MetricsAgent.Controllers.CpuMetricsContoller
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private ICpuMetricsRepository _repository;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("CpuController FromTime:{0} ToTime {1}", fromTime, toTime);

            var response = new ByPeriodCpuMetricResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            var metrics = _repository.GetByPeriod(new PeriodArgs() { FromTime = fromTime, ToTime = toTime});
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto { Time = DateTimeOffset.FromUnixTimeSeconds(metric.Time), Value = metric.Value });
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _repository.Create(new CpuMetric
            {
                Time = request.Time.ToUnixTimeSeconds(),
                Value = request.Value
            });

            return Ok();
        }
    }
}
