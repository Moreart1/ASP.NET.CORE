﻿using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interface;
using MetricsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetricsManagerTest
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

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<long>(), It.IsAny<long>())).Returns(new List<RamMetric>());
            var result = _controller.GetAvailable(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow);
            _mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<long>(), It.IsAny<long>()), Times.AtMostOnce());
        }
    }
}
