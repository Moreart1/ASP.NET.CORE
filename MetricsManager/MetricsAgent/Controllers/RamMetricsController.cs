using MetricsAgent.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository)
        {
            _logger = logger;
            _ramMetricsRepository = repository;
        }


        [HttpGet("available")]
        public IActionResult GetFreeRamSizeMetrics()
        {
            _logger.LogInformation("Получение RAM");
            return Ok();
        }
    }
}
