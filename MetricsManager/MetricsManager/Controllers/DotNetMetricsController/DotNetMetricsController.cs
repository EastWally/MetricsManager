using AutoMapper;
using MetricsManager.Controllers.DotNetMetricsController.Responses;
using MetricsManager.DAL;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.DotNetMetricsController
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает метрики DotNet от заданного агента в заданном интервале времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/dotnet/agent/1/from/2020-01-01/to/2022-01-01
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
            _logger.LogInformation("DotNetControllerAgent FromTime:{0} ToTime {1}", fromTime, toTime);
            var metrics = _repository.GetByPeriodFromAgent(new PeriodAgentArgs() { FromTime = fromTime, ToTime = toTime, AgentId = agentId });

            var response = new ByPeriodDotNetMetricResponse()
            {
                Metrics = _mapper.Map<IEnumerable<DotNetMetric>, List<DotNetMetricDto>>((IEnumerable<DotNetMetric>)metrics)
            };

            return Ok(response);
        }

        /// <summary>
        /// Получает метрики DotNet от всех агентов в заданном интервале времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/dotnet/cluster/from/2020-01-01/to/2022-01-01
        ///
        /// </remarks>
        /// <param name="fromTime">Начальная метка времени</param>
        /// <param name="toTime">Конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не правильные параметры</response>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("DotNetControllerCluster FromTime:{0} ToTime {1}", fromTime, toTime);
            var metrics = _repository.GetByPeriod(new PeriodArgs() { FromTime = fromTime, ToTime = toTime });

            var response = new ByPeriodDotNetMetricResponse()
            {
                Metrics = _mapper.Map<IEnumerable<DotNetMetric>, List<DotNetMetricDto>>((IEnumerable<DotNetMetric>)metrics)
            };

            return Ok(response);
        }
    }
}
