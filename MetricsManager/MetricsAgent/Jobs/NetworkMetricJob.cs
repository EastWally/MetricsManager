using MetricsAgent.DAL.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository; 
            
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
            var instancename = category.GetInstanceNames();
            if (instancename.Length > 0)
                _networkCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec", instancename[0]);
            
        }

        public Task Execute(IJobExecutionContext context)
        {
            var networkUsage = Convert.ToInt32(_networkCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new DAL.Models.NetworkMetric { Time = time, Value = networkUsage });

            return Task.CompletedTask;
        }
    }
}
