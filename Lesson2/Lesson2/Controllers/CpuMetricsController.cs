﻿using Lesson2.Percentiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lesson2.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        [HttpGet("agent/{agentID}/from{fromTime}/to{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentID,
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

        [HttpGet("agent/{agentID}/from{fromTime}/to{toTime}/percentiles{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent(
            [FromRoute] int agentID,
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)

        {
            return Ok();
        }

        [HttpGet("cluster/from{fromTime}/to{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

        [HttpGet("cluster/from{fromTime}/to{toTime}/percentiles{percentile}")]
        public IActionResult GetMetricsByPercentileFromAllCluster(
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)
        {
            return Ok();
        }
    }
}
