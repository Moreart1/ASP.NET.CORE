using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTest
{
    public class HddControllerUnitTests
    {
        private readonly HddMetricsController _controller;

        private readonly Mock<IHddMetricsRepository> _mockRepository;

        private readonly Mock<ILogger<HddMetricsController>> _mockLogger;

        private readonly Mock<IMapper> _mockMapper;

        public HddControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<HddMetricsController>>();
            _mockRepository = new Mock<IHddMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new HddMetricsController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }

        [Theory]
        [InlineData(0, 100)]
        public void GetRemainingFreeDiskSpaceMetrics_ReturnsOk(int start, int end)
        {
            var fromTime = TimeSpan.FromSeconds(start);
            var toTime = TimeSpan.FromSeconds(end);

            _mockRepository.Setup(_hddMetricsRepository => _hddMetricsRepository.GetAll(
                It.IsAny<TimeSpan>(),
                It.IsAny<TimeSpan>())).Returns(new List<HddMetric>());

            var result = _controller.GetRemainingFreeDiskSpaceMetrics(fromTime, toTime);

            _mockRepository.Verify(_hddMetricsRepository => _hddMetricsRepository.GetAll(
                It.IsAny<TimeSpan>(),
                It.IsAny<TimeSpan>()),
                Times.AtMostOnce());
        }
    }
}