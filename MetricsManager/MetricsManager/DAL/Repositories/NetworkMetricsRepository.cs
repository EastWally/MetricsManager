using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Interfaces;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace MetricsManager.DAL.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<NetworkMetricsRepository> _logger;

        public NetworkMetricsRepository(IDBConfig dBConfig, ILogger<NetworkMetricsRepository> logger)
        {
            _connectionString = dBConfig.ConnectionString;
            _logger = logger;
        }

        public void Create(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            try
            {
                connection.Execute("INSERT INTO networkmetrics(value, time, agentId) VALUES(@value, @time, @agentId)",
                    new
                    {
                        value = item.Value,
                        time = item.Time,
                        agentId = item.AgentId
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public IList<NetworkMetric> GetByPeriodFromAgent(PeriodAgentArgs args)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (agentId = @agentId) AND (time BETWEEN @fromTime AND @toTime)",
                new
                {
                    agentId = args.AgentId,
                    fromTime = args.FromTime.ToUnixTimeSeconds(),
                    toTime = args.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public IList<NetworkMetric> GetByPeriod(PeriodArgs args)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE time BETWEEN @fromTime AND @toTime",
                new
                {
                    fromTime = args.FromTime.ToUnixTimeSeconds(),
                    toTime = args.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public DateTimeOffset GetLastDate(int agentId)
        {
            using var connection = new SQLiteConnection(_connectionString);
            var metric = connection.QueryFirstOrDefault<NetworkMetric>("SELECT * FROM networkmetrics WHERE agentId = @agentId ORDER BY time DESC LIMIT 1",
                new 
                {
                    agentId = agentId
                });
            if (metric != null)
                return DateTimeOffset.FromUnixTimeSeconds(metric.Time);
            else
                return DateTimeOffset.FromUnixTimeSeconds(1);
        }
    }

}
