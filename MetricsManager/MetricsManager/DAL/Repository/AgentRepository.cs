using AutoMapper;
using Dapper;
using MetricsManager.DAL.Interface;
using MetricsManager.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;

namespace MetricsManager.DAL.Repository
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly ILogger<AgentsRepository> _logger;
        public AgentsRepository(ILogger<AgentsRepository> logger)
        {
            _logger = logger;
        }
        public void Create(AgentInfo agent)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                var count = connection.ExecuteScalar<int>($"SELECT Count(*) FROM agents WHERE uri=@uri;", new { uri = agent.Url });
                if (count > 0)
                {
                    throw new ArgumentException("Агент уже существует");
                }
                var result = connection.Execute(
                $"INSERT INTO agents (uri,isenabled) VALUES (@uri,@isenabled);",
                new
                {
                    uri = agent.Url,
                    isenabled = agent.IsEnabled
                }
                );
                if (result <= 0) throw new InvalidOperationException("Не удалось добавить агента.");
            }
        }

        public IList<AgentInfo> Get()
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<AgentInfo>($"SELECT * FROM agents").ToList();
            }
        }

        public AgentInfo GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.QuerySingle<AgentInfo>($"SELECT * FROM agents WHERE id=@id",
                    new { id });
            }
        }

        public void Update(AgentInfo agent)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                var count = connection.ExecuteScalar<int>($"SELECT Count(*) FROM agents WHERE uri=@uri;", new { uri = agent.Url });
                if (count <= 0)
                {
                    throw new ArgumentException("Агент не существует");
                }
                var result = connection.Execute($"UPDATE agents SET uri=@uri, isenabled=@isenabled where id=@id;",
                new
                {
                    uri = agent.Url,
                    isenabled = agent.IsEnabled,
                    id = agent.Id
                }
                );
                if (result <= 0) throw new InvalidOperationException("Не удалось обновить агента.");
            }
        }
    }
}
