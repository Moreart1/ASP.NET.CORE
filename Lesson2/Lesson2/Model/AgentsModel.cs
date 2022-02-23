using System.Collections.Generic;


namespace Lesson2.Model

{
    public class AgentsModel
    {
        private readonly List<AgentInfo> _data;

        public AgentsModel()
        {
            _data = new List<AgentInfo>();
        }

        public IReadOnlyList<AgentInfo> GetAgentsinfo()
        {
            return _data;
        }

    }
}
