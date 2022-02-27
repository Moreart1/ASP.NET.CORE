using MetricsAgent.Controllers;
using MetricsAgent.Interface;
using MetricsAgent.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgentTest
{
    public class NetworkControllerUnitTests
    {
        private readonly NetworkMetricsController _controller;
        private readonly Mock<NetworkMetricsRepository> _mock;

        public NetworkControllerUnitTests()
        {
            _mock = new Mock<NetworkMetricsRepository>();
            _controller = new NetworkMetricsController(new Mock<ILogger<NetworkMetricsController>>().Object, _mock.Object);
        }


        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = _controller.GetMetrics(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
