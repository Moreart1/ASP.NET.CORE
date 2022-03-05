using Dapper;
using MetricsAgent.DAL;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositories
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        public void Create(HddMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                connection.Execute("INSERT INTO hddmetrics (value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public IList<HddMetric> GetByTimePeriod(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT id, value, time FROM hddmetrics WHERE time BETWEEN @fromTime AND @toTime",
                            new
                            {
                                fromTime = fromTime,
                                toTime = toTime
                            }).ToList();
            }
        }
    }
}
