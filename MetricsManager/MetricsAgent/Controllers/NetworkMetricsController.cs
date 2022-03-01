using AutoMapper;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using MetricsAgent.Request;
using Microsoft.AspNetCore.Mvc;
using static MetricsAgent.Responses.AllMetricsResponses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        private readonly INetworkMetricsRepository _networkMetricsRepository;

        private readonly IMapper _mapper;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _networkMetricsRepository = repository;
            _mapper = mapper;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics(
           [FromRoute] TimeSpan fromTime,
           [FromRoute] TimeSpan toTime)
        {
            IList<NetworkMetric> metrics = _networkMetricsRepository.GetAll(fromTime, toTime);

            var response = new NetworkMetricResponse()
            {
                Metrics = new List<NetworkMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricsDto>(metric));
            }

            _logger.LogInformation($"Получение метрик за период: {fromTime}, \t {toTime}",
                fromTime.ToString(),
                toTime.ToString());
            return Ok(response);
        }
    }
}
