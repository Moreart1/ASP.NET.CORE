using Dapper;
using MetricsAgent.DAL;
using MetricsAgent.Interface;
using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                connection.Execute("INSERT INTO cpumetrics (value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public IList<CpuMetric> GetByTimePeriod(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                return connection.Query<CpuMetric>("SELECT id, value, time FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
                            new
                            {
                                fromTime = fromTime,
                                toTime = toTime
                            }).ToList();
            }
        }
    }
}

