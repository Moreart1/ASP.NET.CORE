using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lesson2.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        [HttpGet("agent{agentId}/left")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpGet("cluster/left")]
        public IActionResult GetMetricsFromAllCluster()
        {
            return Ok();
        }
    }
}
