using MetricsManager.Controllers;
using MetricsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTest
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;

        public AgentsControllerUnitTests()
        {
            controller = new AgentsController();
        }

        [Fact]
        public void RegisterAgent_ReturnOk()
        {
            //Arrange
            var agent = new AgentInfo();

            //act
            var result = controller.RegisterAgent(agent);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnOk()
        {
            //Arrange
            var agentId = 1;

            //act
            var result = controller.EnableAgentById(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnOk()
        {
            //Arrange
            var agentId = 1;

            //act
            var result = controller.DisableAgentById(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

    }
}