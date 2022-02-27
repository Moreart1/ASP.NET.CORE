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
    public class DotNetControllerUnitTests
    {
        private readonly DotNetMetricsController _controller;
        private readonly Mock<DotNetMetricsRepository> _mock;

        public DotNetControllerUnitTests()
        {
            _mock = new Mock<DotNetMetricsRepository>();
            _controller = new DotNetMetricsController(new Mock<ILogger<DotNetMetricsController>>().Object, _mock.Object);
        }


        [Fact]
        public void GetErrorsCountMetrics_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = _controller.GetErrorsCountMetrics(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
