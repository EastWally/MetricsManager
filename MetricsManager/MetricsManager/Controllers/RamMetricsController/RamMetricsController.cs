using AutoMapper;
using MetricsManager.Controllers.RamMetricsController.Responses;
using MetricsManager.DAL;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.RamMetricsController
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("RamControllerAgent FromTime:{0} ToTime {1}", fromTime, toTime);
            var metrics = _repository.GetByPeriodFromAgent(new PeriodAgentArgs() { FromTime = fromTime, ToTime = toTime, AgentId = agentId });

            var response = new ByPeriodRamMetricResponse()
            {
                Metrics = _mapper.Map<IEnumerable<RamMetric>, List<RamMetricDto>>((IEnumerable<RamMetric>)metrics)
            };

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("RamControllerCluster FromTime:{0} ToTime {1}", fromTime, toTime);
            var metrics = _repository.GetByPeriod(new PeriodArgs() { FromTime = fromTime, ToTime = toTime });

            var response = new ByPeriodRamMetricResponse()
            {
                Metrics = _mapper.Map<IEnumerable<RamMetric>, List<RamMetricDto>>((IEnumerable<RamMetric>)metrics)
            };

            return Ok(response);
        }
    }
}
