using MetricsAgent.Controllers;
using MetricsAgent.Interface;
using MetricsAgent.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsAgentTest
{
    public class HddControllerUnitTests
    {
        private readonly HddMetricsController _controller;
        private readonly Mock<HddMetricsRepository> _mock;

        public HddControllerUnitTests()
        {
            _mock = new Mock<HddMetricsRepository>();
            _controller = new HddMetricsController(new Mock<ILogger<HddMetricsController>>().Object, _mock.Object);
        }

        [Fact]
        public void GetRemainingFreeDiskSpaceMetrics_ReturnsOk()
        {
            var result = _controller.GetRemainingFreeDiskSpaceMetrics();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}