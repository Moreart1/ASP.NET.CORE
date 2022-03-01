using AutoMapper;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using MetricsAgent.Request;
using Microsoft.AspNetCore.Mvc;
using static MetricsAgent.Responses.AllMetricsResponses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;

        private readonly IRamMetricsRepository _ramMetricsRepository;

        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _ramMetricsRepository = repository;
            _mapper = mapper;
        }


        [HttpGet("available")]
        public IActionResult GetFreeRamSizeMetrics(
           [FromRoute] TimeSpan fromTime,
           [FromRoute] TimeSpan toTime)
        {
            IList<RamMetric> metrics = _ramMetricsRepository.GetAll(fromTime, toTime);

            var response = new RamMetricResponse()
            {
                Metrics = new List<RamMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricsDto>(metric));
            }
            _logger.LogInformation($"Получение RAM за период: {fromTime}, \t {toTime}",
                fromTime.ToString(),
                toTime.ToString());
            return Ok(response);
        }
    }
}
