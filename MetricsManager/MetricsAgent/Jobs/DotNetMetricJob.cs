using MetricsAgent.DAL.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly PerformanceCounter _dotnetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;            
            _dotnetCounter = new PerformanceCounter("ASP.NET", "Error Events Raised");
            

        }

        public Task Execute(IJobExecutionContext context)
        {
            var dotNetErrors = Convert.ToInt32(_dotnetCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new DAL.Models.DotNetMetric { Time = time, Value = dotNetErrors });

            return Task.CompletedTask;
        }
    }
}
