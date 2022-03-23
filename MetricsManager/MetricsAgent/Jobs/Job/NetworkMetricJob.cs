using MetricsAgent.Interface;
using MetricsAgent.Models;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs.Job
{
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _cpuCounter;
        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("IPsec Connections", "Total Bytes Out since start");
        }

        public Task Execute(IJobExecutionContext context)
        {
            _repository.Create(new NetworkMetric
            {
                Value = Convert.ToInt32(_cpuCounter.NextValue()),
                Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });
            return Task.CompletedTask;

        }
    }
}
