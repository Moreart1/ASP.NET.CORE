using MetricsManager.Controllers;
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
        private RamMetricsController controller;

        private Mock<ILogger<RamMetricsController>> mockLogger;

        public RamControllerUnitTests()
        {
            mockLogger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(mockLogger.Object);
        }

        [Fact]
        public void Available_ReturnsOk()
        {
            //Arrange
            var agentId = 1;

            //act
            var result = controller.Available(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {        
            //act
            var result = controller.GetMetricsFromAllCluster();

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


    }
}
