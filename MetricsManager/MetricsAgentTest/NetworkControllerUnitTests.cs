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
    public class NetworkControllerUnitTests
    {
        private readonly NetworkMetricsController _controller;

        private readonly Mock<INetworkMetricsRepository> _mockRepository;

        private readonly Mock<ILogger<NetworkMetricsController>> _mockLogger;

        private readonly Mock<IMapper> _mockMapper;

        public NetworkControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            _mockRepository = new Mock<INetworkMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new NetworkMetricsController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }


        [Theory]
        [InlineData(0, 100)]
        public void GetMetrics_ReturnsOk(int start, int end)
        {
            var fromTime = TimeSpan.FromSeconds(start);
            var toTime = TimeSpan.FromSeconds(end);


            _mockRepository.Setup(_networkMetricsRepository => _networkMetricsRepository.GetAll(
                It.IsAny<TimeSpan>(),
                It.IsAny<TimeSpan>())).Returns(new List<NetworkMetric>());

            var result = _controller.GetMetrics(fromTime, toTime);

            _mockRepository.Verify(_networkMetricsRepository => _networkMetricsRepository.GetAll(It.IsAny<TimeSpan>(),
                It.IsAny<TimeSpan>()),
                Times.AtMostOnce());
        }
    }
}
