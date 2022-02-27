using ClassLibrary1;
using MetricsAgent.Controllers;
using MetricsAgent.Interface;
using MetricsAgent.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTest
{
    public class CpuControllerUnitTests
    {

        private readonly CpuMetricsController _controller;
        private readonly Mock<CpuMetricsRepository> _mock;

        public CpuControllerUnitTests()
        {
            _mock = new Mock<CpuMetricsRepository>();
            _controller = new CpuMetricsController(new Mock<ILogger<CpuMetricsController>>().Object, _mock.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = _controller.GetMetrics(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Theory]
        [InlineData(0, 100, Percentile.Median)]
        [InlineData(0, 100, Percentile.P75)]
        [InlineData(0, 100, Percentile.P90)]
        [InlineData(0, 100, Percentile.P95)]
        [InlineData(0, 100, Percentile.P99)]
        public void GetMetricsByPercentile_ReturnsOk(
            int start,
            int end,
            Percentile percentile)
        {
            var fromTime = TimeSpan.FromSeconds(start);
            var toTime = TimeSpan.FromSeconds(end);

            var result = _controller.GetMetricsByPercentile(fromTime, toTime, percentile);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}