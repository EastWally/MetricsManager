using MetricsAgent.Controllers.NetworkMetricsController.Requests;
using MetricsAgent.Controllers.NetworkMetricsController.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL;
using AutoMapper;

namespace MetricsAgent.Controllers.NetworkMetricsController
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает метрики Network в заданном интервале времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/network/from/2020-01-01/to/2022-01-01
        ///
        /// </remarks>
        /// <param name="fromTime">Начальная метка времени</param>
        /// <param name="toTime">Конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не правильные параметры</response>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("NetworkController FromTime:{0} ToTime {1}", fromTime, toTime);

            var metrics = _repository.GetByPeriod(new PeriodArgs() { FromTime = fromTime, ToTime = toTime });

            var response = new ByPeriodNetworkMetricResponse()
            {
                Metrics = _mapper.Map<IEnumerable<NetworkMetric>, List<NetworkMetricDto>>((IEnumerable<NetworkMetric>)metrics)
            };

            return Ok(response);
        }

        /// <summary>
        /// Записывает метрику Network
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST api/metrics/network/create
        ///
        /// </remarks>
        /// <param name="request">Метрика</param>
        /// <returns></returns>
        /// <response code="400">Переданы не правильные параметры</response>
        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _repository.Create(_mapper.Map<NetworkMetric>(request));

            return Ok();
        }
    }
}
