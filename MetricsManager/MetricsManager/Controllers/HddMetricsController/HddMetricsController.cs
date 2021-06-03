using AutoMapper;
using MetricsManager.Controllers.HddMetricsController.Responses;
using MetricsManager.DAL;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.HddMetricsController
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _repository;
        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает метрики Hdd от заданного агента в заданном интервале времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/hdd/agent/1/from/2020-01-01/to/2022-01-01
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
            _logger.LogInformation("HddControllerAgent FromTime:{0} ToTime {1}", fromTime, toTime);
            var metrics = _repository.GetByPeriodFromAgent(new PeriodAgentArgs() { FromTime = fromTime, ToTime = toTime, AgentId = agentId });

            var response = new ByPeriodHddMetricResponse()
            {
                Metrics = _mapper.Map<IEnumerable<HddMetric>, List<HddMetricDto>>((IEnumerable<HddMetric>)metrics)
            };

            return Ok(response);
        }

        /// <summary>
        /// Получает метрики Hdd от всех агентов в заданном интервале времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/hdd/cluster/from/2020-01-01/to/2022-01-01
        ///
        /// </remarks>
        /// <param name="fromTime">Начальная метка времени</param>
        /// <param name="toTime">Конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не правильные параметры</response>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("HddControllerCluster FromTime:{0} ToTime {1}", fromTime, toTime);
            var metrics = _repository.GetByPeriod(new PeriodArgs() { FromTime = fromTime, ToTime = toTime });

            var response = new ByPeriodHddMetricResponse()
            {
                Metrics = _mapper.Map<IEnumerable<HddMetric>, List<HddMetricDto>>((IEnumerable<HddMetric>)metrics)
            };

            return Ok(response);
        }
    }
}
