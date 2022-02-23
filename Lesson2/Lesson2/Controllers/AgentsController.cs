using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lesson2.Model;


namespace Lesson2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly AgentsModel _agentsModel;

        public AgentsController(AgentsModel agentsModel)
        {
            _agentsModel = agentsModel;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            return Ok();
        }

        [HttpDelete("unregister")]
        public IActionResult UnRegisterAgent([FromBody] AgentInfo agentInfo)
        {
            return Ok();
        }

        [HttpPut("enable/{agentID}")]
        public IActionResult EnableAgentById([FromRoute] int agentID)
        {
            return Ok();
        }

        [HttpPut("disable/{agentID}")]
        public IActionResult DisableAgentById([FromRoute] int agentID)
        {
            return Ok();
        }
        [HttpGet("get_agents")]
        public IActionResult GetRegisterAgent()
        {
            return Ok(_agentsModel.GetAgentsinfo());
        }
    }
}
