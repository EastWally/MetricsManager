using MetricsAgent.DAL.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly PerformanceCounter _cpuCounter;

        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new DAL.Models.CpuMetric { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
