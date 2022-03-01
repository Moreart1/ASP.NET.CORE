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
    public class RamControllerUnitTests
    {
        private readonly RamMetricsController _controller;

        private readonly Mock<IRamMetricsRepository> _mockRepository;

        private readonly Mock<ILogger<RamMetricsController>> _mockLogger;

        private readonly Mock<IMapper> _mockMapper;

        public RamControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<RamMetricsController>>();
            _mockRepository = new Mock<IRamMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new RamMetricsController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }


        [Theory]
        [InlineData(0, 100)]
        public void GetFreeRamSizeMetrics_ReturnsOk(int start, int end)
        {
            var fromTime = TimeSpan.FromSeconds(start);
            var toTime = TimeSpan.FromSeconds(end);

            _mockRepository.Setup(_ramMetricsRepository => _ramMetricsRepository.GetAll(
                It.IsAny<TimeSpan>(),
                It.IsAny<TimeSpan>())).Returns(new List<RamMetric>());

            var result = _controller.GetFreeRamSizeMetrics(fromTime, toTime);

            _mockRepository.Verify(_ramMetricsRepository => _ramMetricsRepository.GetAll(
                It.IsAny<TimeSpan>(),
                It.IsAny<TimeSpan>()),
                Times.AtMostOnce());
        }
    }
}
