using MetricsAgent.Controllers.RamMetricsController.Requests;
using MetricsAgent.Controllers.RamMetricsController.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL;
using AutoMapper;

namespace MetricsAgent.Controllers.RamMetricsController
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
        /// Получает метрики RAM в заданном интервале времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/ram/available/from/2020-01-01/to/2022-01-01
        ///
        /// </remarks>
        /// <param name="fromTime">Начальная метка времени</param>
        /// <param name="toTime">Конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не правильные параметры</response>
        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableSpace([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("RamController FromTime:{0} ToTime {1}", fromTime, toTime);

            var metrics = _repository.GetByPeriod(new PeriodArgs() { FromTime = fromTime, ToTime = toTime });

            var response = new ByPeriodRamMetricResponse()
            {
                Metrics = _mapper.Map<IEnumerable<RamMetric>, List<RamMetricDto>>((IEnumerable<RamMetric>)metrics)
            };

            return Ok(response);
        }

        /// <summary>
        /// Записывает метрику RAM
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST api/metrics/ram/create
        ///
        /// </remarks>
        /// <param name="request">Метрика</param>
        /// <returns></returns>
        /// <response code="400">Переданы не правильные параметры</response>
        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _repository.Create(_mapper.Map<RamMetric>(request));

            return Ok();
        }
    }
}
