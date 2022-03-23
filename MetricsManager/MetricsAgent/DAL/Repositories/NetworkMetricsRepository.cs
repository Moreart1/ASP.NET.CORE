using Dapper;
using MetricsAgent.DAL;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                connection.Execute("INSERT INTO networkmetrics (value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public IList<NetworkMetric> GetByTimePeriod(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<NetworkMetric>("SELECT id, value, time FROM networkmetrics WHERE time BETWEEN @fromTime AND @toTime",
                            new
                            {
                                fromTime = fromTime,
                                toTime = toTime
                            }).ToList();
            }
        }
    }
}
