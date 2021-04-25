using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder _holder;

        public CrudController(ValuesHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int value)
        {
            _holder.Values.Add(new Weather(date, value));
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_holder.Values);
        }

        [HttpGet("readPeriod")]
        public IActionResult Read([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var result = _holder.Values.Where(w => w.Date >= fromDate && w.Date <= toDate).ToList(); ;
            return Ok(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int newValue)
        {
            for (int i = 0; i < _holder.Values.Count; i++)
            {
                if (_holder.Values[i].Date == date)
                    _holder.Values[i].Temperature = newValue;
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            _holder.Values = _holder.Values.Where(w => w.Date < fromDate || w.Date > toDate).ToList();
            return Ok();
        }
    }
}
