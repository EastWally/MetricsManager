using MetricsAgent.Controllers.NetworkMetricsController.Requests;
using MetricsAgent.Controllers.NetworkMetricsController.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL;

namespace MetricsAgent.Controllers.NetworkMetricsController
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private INetworkMetricsRepository _repository;
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("NetworkController FromTime:{0} ToTime {1}", fromTime, toTime);

            var metrics = _repository.GetByPeriod(new PeriodArgs() { FromTime = fromTime, ToTime = toTime });
            var response = new ByPeriodNetworkMetricResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto { Time = DateTimeOffset.FromUnixTimeSeconds(metric.Time), Value = metric.Value });
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _repository.Create(new NetworkMetric
            {
                Time = request.Time.ToUnixTimeSeconds(),
                Value = request.Value
            });

            return Ok();
        }
    }
}
