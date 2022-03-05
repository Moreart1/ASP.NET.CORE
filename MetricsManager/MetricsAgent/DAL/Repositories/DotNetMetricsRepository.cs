using Dapper;
using MetricsAgent.DAL;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public IList<DotNetMetric> GetByTimePeriod(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT id, value, time FROM dotnetmetrics WHERE time BETWEEN @fromTime AND @toTime",
                            new
                            {
                                fromTime = fromTime,
                                toTime = toTime
                            }).ToList();
            }
        }

    }
}

