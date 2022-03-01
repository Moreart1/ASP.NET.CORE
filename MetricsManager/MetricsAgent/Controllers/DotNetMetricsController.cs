using AutoMapper;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using MetricsAgent.Request;
using Microsoft.AspNetCore.Mvc;
using static MetricsAgent.Responses.AllMetricsResponses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
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

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountMetricsDto(
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            IList<DotNetMetric> metrics = _dotNetMetricsRepository.GetAll(fromTime, toTime);

            var response = new DotNetMetricResponse()
            {
                Metrics = new List<DotNetMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricsDto>(metric));
            }

            _logger.LogInformation($"Получение количества ошибок за период: {fromTime}, \t {toTime} ",
                fromTime.ToString(),
                toTime.ToString());
            return Ok(response);
        }
    }
}
