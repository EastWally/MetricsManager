using MetricsAgent.Controllers.CpuMetricsContoller.Requests;
using MetricsAgent.Controllers.CpuMetricsContoller.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL;
using AutoMapper;

namespace MetricsAgent.Controllers.CpuMetricsContoller
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает метрики CPU в заданном интервале времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/cpu/from/2020-01-01/to/2022-01-01
        ///
        /// </remarks>
        /// <param name="fromTime">Начальная метка времени</param>
        /// <param name="toTime">Конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не правильные параметры</response>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("CpuController FromTime:{0} ToTime {1}", fromTime, toTime);

            var metrics = _repository.GetByPeriod(new PeriodArgs() { FromTime = fromTime, ToTime = toTime });

            var response = new ByPeriodCpuMetricResponse()
            {
                Metrics = _mapper.Map<IEnumerable<CpuMetric>, List<CpuMetricDto>>((IEnumerable<CpuMetric>)metrics)
            };

            return Ok(response);
        }

        /// <summary>
        /// Записывает метрику CPU
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST api/metrics/cpu/create
        ///
        /// </remarks>
        /// <param name="request">Метрика</param>
        /// <returns></returns>
        /// <response code="400">Переданы не правильные параметры</response>
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _repository.Create(_mapper.Map<CpuMetric>(request));

            return Ok();
        }
    }
}
