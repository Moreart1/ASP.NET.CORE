using AutoMapper;
using MetricsManager.DAL.Interface;
using MetricsManager.Model;
using MetricsManager.Response.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentsRepository _repository;
        private readonly ILogger<AgentsController> _logger;
        private readonly IMapper _mapper;

        public AgentsController(ILogger<AgentsController> logger, IAgentsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _repository.Create(_mapper.Map<AgentInfo>(agentInfo));

            _logger.LogInformation("Регистрация агента");

            return Ok();

        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {

            var agent = _repository.GetById(agentId);
            if (agent is null)

            {
                return NotFound();
            }
            agent.IsEnabled = true;

            _repository.Update(agent);

            return Ok();

        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {

            var agent = _repository.GetById(agentId);

            if (agent is null)
            {
                return NotFound();
            }

            agent.IsEnabled = false;

            _repository.Update(agent);

            return Ok();


        }

        [HttpGet("getregistagents")]
        public IActionResult GetRegisterAgents()
        {
            var agents = _repository.Get();

            return Ok(new GetRegisteredAgentsResponse()
            {
                Agents = agents.Select(_mapper.Map<AgentsResponse>)
            });
        }
    }
}
