using AutoMapper;
using ClassLibrary1;
using MetricsAgent.Controllers;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using MetricsAgent.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTest
{
    public class CpuControllerUnitTests
    {
        private readonly CpuMetricsController _controller;

        private readonly Mock<ICpuMetricsRepository> _mockRepository;

        private readonly Mock<ILogger<CpuMetricsController>> _mockLogger;

        private readonly Mock<IMapper> _mockMapper;

        public CpuControllerUnitTests()
        {
            _mockRepository = new Mock<ICpuMetricsRepository>();
            _mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CpuMetricsController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }

        [Theory]
        [InlineData(0, 100)]
        public void GetMetrics_ReturnsOk(int start, int end)
        {
            var fromTime = TimeSpan.FromSeconds(start);
            var toTime = TimeSpan.FromSeconds(end);

            _mockRepository.Setup(_cpuMetricsRepository => _cpuMetricsRepository.GetAll(It.IsAny<TimeSpan>(),
                It.IsAny<TimeSpan>())).Returns(new List<CpuMetric>());

            var result = _controller.GetMetrics(fromTime, toTime);

            _mockRepository.Verify(_cpuMetricsRepository => _cpuMetricsRepository.GetAll(
                It.IsAny<TimeSpan>(),
                It.IsAny<TimeSpan>()),
                Times.AtMostOnce());
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