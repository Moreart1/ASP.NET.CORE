using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lesson2.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        [HttpGet("agent{agentId}/available")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
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
