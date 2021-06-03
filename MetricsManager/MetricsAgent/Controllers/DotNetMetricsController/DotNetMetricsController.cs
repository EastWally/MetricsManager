using MetricsAgent.Controllers.DotNetMetricsController.Requests;
using MetricsAgent.Controllers.DotNetMetricsController.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL;
using AutoMapper;

namespace MetricsAgent.Controllers.DotNetMetricsController
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
        /// Получает метрики DotNet в заданном интервале времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/dotnet/errors-count/from/2020-01-01/to/2022-01-01
        ///
        /// </remarks>
        /// <param name="fromTime">Начальная метка времени</param>
        /// <param name="toTime">Конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не правильные параметры</response>
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCount([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("DotNetController FromTime:{0} ToTime {1}", fromTime, toTime);

            var metrics = _repository.GetByPeriod(new PeriodArgs() { FromTime = fromTime, ToTime = toTime });
            var response = new ByPeriodDotNetMetricResponse()
            {
                Metrics = _mapper.Map<IEnumerable<DotNetMetric>, List<DotNetMetricDto>>((IEnumerable<DotNetMetric>)metrics)
            };

            return Ok(response);
        }

        /// <summary>
        /// Записывает метрику DotNet
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST api/metrics/dotnet/create
        ///
        /// </remarks>
        /// <param name="request">Метрика</param>
        /// <returns></returns>
        /// <response code="400">Переданы не правильные параметры</response>
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _repository.Create(_mapper.Map<DotNetMetric>(request));
            return Ok();
        }
    }
}
