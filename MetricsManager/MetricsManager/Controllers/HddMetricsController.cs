using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("left/agent/{agentId}")]
        public IActionResult Left([FromRoute] int agentId)                             
        {
            _logger.LogInformation($"Получение свободного места на HDD у  \t {agentId}",
               agentId);
            return Ok();
        }

        [HttpGet("left/cluster")]
        public IActionResult GetMetricsFromAllCluster()           
        {
            _logger.LogInformation("Получение свободного места на всех HDD");
            return Ok();
        }


    }
}