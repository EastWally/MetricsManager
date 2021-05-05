using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableSpace([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("RamController FromTime:{0} ToTime {1}", fromTime, toTime);
            return Ok();
        }
    }
}
