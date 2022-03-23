using Dapper;
using MetricsAgent.DAL;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositories
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        public void Create(RamMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                connection.Execute("INSERT INTO rammetrics (value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public IList<RamMetric> GetByTimePeriod(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT id, value, time FROM rammetrics WHERE time BETWEEN @fromTime AND @toTime",
                            new
                            {
                                fromTime = fromTime,
                                toTime = toTime
                            }).ToList();
            }
        }
    }
}
