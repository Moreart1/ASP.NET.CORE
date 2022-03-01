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
    public class DotNetControllerUnitTests
    {
        private readonly DotNetMetricsController _controller;

        private readonly Mock<IDotNetMetricsRepository> _mockRepository;

        private readonly Mock<ILogger<DotNetMetricsController>> _mockLogger;

        private readonly Mock<IMapper> _mockMapper;

        public DotNetControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            _mockRepository = new Mock<IDotNetMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new DotNetMetricsController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }


        [Theory]
        [InlineData(0, 100)]
        public void GetErrorsCountMetrics_ReturnsOk(int start, int end)
        {
            var fromTime = TimeSpan.FromSeconds(start);
            var toTime = TimeSpan.FromSeconds(end);

            _mockRepository.Setup(_dotnetMetricsRepository => _dotnetMetricsRepository.GetAll(
                It.IsAny<TimeSpan>(),
                It.IsAny<TimeSpan>())).Returns(new List<DotNetMetric>());

            var result = _controller.GetErrorsCountMetricsDto(fromTime, toTime);

            _mockRepository.Verify(_cpuMetricsRepository => _cpuMetricsRepository.GetAll(It.IsAny<TimeSpan>(),
                It.IsAny<TimeSpan>()),
                Times.AtMostOnce());
        }
    }
}
