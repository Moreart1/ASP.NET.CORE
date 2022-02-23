using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lesson2.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        [HttpGet("agent{agentID}/available")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentID)
        {
            return Ok();
        }

        [HttpGet("cluster/available")]
        public IActionResult GetMetricsFromAllCluster()
        {
            return Ok();
        }
    }
}
