using MetricsAgent.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkNetMetricsRepository;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository)
        {
            _logger = logger;
            _networkNetMetricsRepository = repository;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics(
           [FromRoute] TimeSpan fromTime,
           [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Получение метрик за период: {fromTime}, \t {toTime}",
                fromTime.ToString(),
                toTime.ToString());
            return Ok();
        }
    }
}
