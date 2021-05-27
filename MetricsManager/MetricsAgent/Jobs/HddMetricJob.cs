using MetricsAgent.DAL.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _repository;
        private readonly PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("LogicalDisk", "% Free Space", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddLeft = Convert.ToInt32(_hddCounter.RawValue);
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
           _repository.Create(new DAL.Models.HddMetric { Time = time, Value = hddLeft });

            return Task.CompletedTask;
        }
    }
}
