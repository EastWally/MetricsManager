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

        /// <summary>
        /// Получает метрики RAM от заданного агента в заданном интервале времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/ram/agent/1/from/2020-01-01/to/2022-01-01
        ///
        /// </remarks>
        /// <param name="agentId">Идентификатор агента</param>
        /// <param name="fromTime">Начальная метка времени</param>
        /// <param name="toTime">Конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не правильные параметры</response>
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

        /// <summary>
        /// Получает метрики Ram от всех агентов в заданном интервале времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/ram/cluster/from/2020-01-01/to/2022-01-01
        ///
        /// </remarks>
        /// <param name="fromTime">Начальная метка времени</param>
        /// <param name="toTime">Конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не правильные параметры</response>
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
