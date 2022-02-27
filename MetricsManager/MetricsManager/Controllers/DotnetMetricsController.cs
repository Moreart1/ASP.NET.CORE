﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("errors-count/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult ErrorsCount
            ([FromRoute] int agentId,
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime
            )
        {
            _logger.LogInformation($"Получение количества ошибок за период: {fromTime}, {toTime} от {agentId}",
                agentId,
                fromTime.ToString(),
                toTime.ToString());
            return Ok();
        }


        [HttpGet("errors-count/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster
            (
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime
            )
        {
            _logger.LogInformation($"Получение количества ошибок за период: {fromTime}, {toTime}",
                fromTime.ToString(),
                toTime.ToString());
            return Ok();
        }
    }
}