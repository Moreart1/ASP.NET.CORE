using Lesson2.Model;
using Lesson2.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTest
{
    public class AgentsControllerUnitTest
    {
        private readonly AgentsController _controller;

        public AgentsControllerUnitTest()
        {
            var agentsModel = new AgentsModel();
            _controller = new AgentsController(agentsModel);   
        }


        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            var agentInfo = new AgentInfo();

            var result = _controller.RegisterAgent(agentInfo);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void UnRegisterAgent_ReturnsOk()
        {
            var agentInfo = new AgentInfo();

            var result = _controller.UnRegisterAgent(agentInfo);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = _controller.EnableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = _controller.DisableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetRegisterAgents_ReturnsOk()
        {

            var result = _controller.GetRegisterAgent();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
