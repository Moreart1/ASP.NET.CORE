using Lesson2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTest
{
    public class RamMetricsControllerUnitTest
    {
        private readonly RamMetricsController _controller;

        public RamMetricsControllerUnitTest()
        {
            _controller = new RamMetricsController();
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;

            var result = _controller.GetMetricsFromAgent(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            var result = _controller.GetMetricsFromAllCluster();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
