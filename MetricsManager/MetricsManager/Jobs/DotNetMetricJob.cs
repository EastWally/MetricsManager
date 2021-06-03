using MetricsManager.Client;
using MetricsManager.Client.Requests;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IAgentRepository _agentsRepository;

        public DotNetMetricJob(IDotNetMetricsRepository repository, IAgentRepository agentsRepository, IMetricsAgentClient metricsAgentClient)
        {
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
            _agentsRepository = agentsRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            IList<Agents> _agents = _agentsRepository.GetAgents();
            foreach (var agent in _agents)
            {
                var fromTime = _repository.GetLastDate(agent.AgentId);
                var toTime = DateTimeOffset.Now;

                var metrics = _metricsAgentClient.GetDotNetMetrics(new GetAllDotNetMetricsApiRequest
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    ClientBaseAddress = agent.AgentUrl
                });

                if (metrics != null)
                {
                    foreach (var metric in metrics)
                    {
                        _repository.Create(new DotNetMetric { Time = metric.time.ToUnixTimeSeconds(), Value = metric.value, AgentId = agent.AgentId });
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
