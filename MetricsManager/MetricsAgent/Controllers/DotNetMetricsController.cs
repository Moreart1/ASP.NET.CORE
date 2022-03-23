using AutoMapper;
using MetricsAgent.Interface;
using MetricsAgent.Request;
using Microsoft.AspNetCore.Mvc;
using static MetricsAgent.Responses.AllMetricsResponses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet/errors-count")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;

        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;

        private readonly IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _dotNetMetricsRepository = repository;
            _mapper = mapper;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запуск DotNetMetricsController.GetMetrics с параметрами: {fromTime}, {toTime}.");
            var metrics = _dotNetMetricsRepository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());
            var response = new DotNetMetricResponse()
            {
                Metrics = new List<DotNetMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricsDto>(metric));
            }
            return Ok(response);
        }
    }
}
