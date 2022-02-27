using MetricsAgent.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _hddNetMetricsRepository;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository)
        {
            _logger = logger;
            _hddNetMetricsRepository = repository;
        }


        [HttpGet("left")]
        public IActionResult GetRemainingFreeDiskSpaceMetrics()
        {
            _logger.LogInformation("Получение свободного места  HDD");
            return Ok();
        }
    }
}
