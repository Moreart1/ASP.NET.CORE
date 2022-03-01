using AutoMapper;
using ClassLibrary1;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using MetricsAgent.Request;
using Microsoft.AspNetCore.Mvc;
using static MetricsAgent.Responses.AllMetricsResponses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        private readonly ICpuMetricsRepository _cpuMetricsRepository;

        private readonly IMapper _mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _cpuMetricsRepository = repository;
            _mapper = mapper;
        }

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentile(
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Получение показателей ЦП за период: {fromTime},\t {toTime} c процентилем {percentile}",
                fromTime.ToString(),
                toTime.ToString());
            return Ok();
        }

        /// <summary>
        /// Data Transfer Object (DTO)
        /// </summary>
        /// <returns></returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics(
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            IList<CpuMetric> metrics = _cpuMetricsRepository.GetAll(fromTime, toTime);

            var response = new CpuMetricResponse()
            {
                Metrics = new List<CpuMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricsDto>(metric));
            }

            _logger.LogInformation($"Получение показателей ЦП за период: {fromTime},\t {toTime}",
                fromTime.ToString(),
                toTime.ToString());

            return Ok(response);
        }
    }
}
