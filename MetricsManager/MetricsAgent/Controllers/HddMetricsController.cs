using AutoMapper;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using MetricsAgent.Request;
using Microsoft.AspNetCore.Mvc;
using static MetricsAgent.Responses.AllMetricsResponses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;

        private readonly IHddMetricsRepository _hddMetricsRepository;

        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _hddMetricsRepository = repository;
            _mapper = mapper;
        }


        [HttpGet("left")]
        public IActionResult GetRemainingFreeDiskSpaceMetrics(
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            IList<HddMetric> metrics = _hddMetricsRepository.GetAll(fromTime, toTime);

            var response = new HddMetricResponse()
            {
                Metrics = new List<HddMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricsDto>(metric));
            }

            _logger.LogInformation($"Получение свободного места  HDD за период : {fromTime}, \t {toTime}",
               fromTime.ToString(),
               toTime.ToString());
            return Ok(response);
        }
    }
}
