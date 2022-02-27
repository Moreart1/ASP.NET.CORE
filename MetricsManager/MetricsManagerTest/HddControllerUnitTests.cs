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
    public class HddControllerUnitTests
    {
        private HddMetricsController controller;

        private Mock<ILogger<HddMetricsController>> mockLogger;

        public HddControllerUnitTests()
        {
            mockLogger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(mockLogger.Object);
        }

        [Fact]
        public void Left_ReturnOk()
        {
            //Arrange
            var agentId = 1;

            //act
            var result = controller.Left(agentId);

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
