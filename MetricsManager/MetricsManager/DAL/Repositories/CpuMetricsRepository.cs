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
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CpuMetricsRepository> _logger;

        public CpuMetricsRepository(IDBConfig dBConfig, ILogger<CpuMetricsRepository> logger)
        {
            _connectionString = dBConfig.ConnectionString;
            _logger = logger;
        }

        public void Create(CpuMetric item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            try
            {
                connection.Execute("INSERT INTO cpumetrics(value, time, agentId) VALUES(@value, @time, @agentId)",
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

        public IList<CpuMetric> GetByPeriodFromAgent(PeriodAgentArgs args)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE (agentId = @agentId) AND (time BETWEEN @fromTime AND @toTime)",
                new
                {
                    agentId = args.AgentId,
                    fromTime = args.FromTime.ToUnixTimeSeconds(),
                    toTime = args.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public IList<CpuMetric> GetByPeriod(PeriodArgs args)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
                new
                {
                    fromTime = args.FromTime.ToUnixTimeSeconds(),
                    toTime = args.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public DateTimeOffset GetLastDate(int agentId)
        {
            using var connection = new SQLiteConnection(_connectionString);
            var metric = connection.QueryFirstOrDefault<CpuMetric>("SELECT * FROM cpumetrics ORDER BY time DESC LIMIT 1",
                new 
                {
                    agentId
                });
            if (metric != null)
                return DateTimeOffset.FromUnixTimeSeconds(metric.Time);
            else
                return DateTimeOffset.FromUnixTimeSeconds(1);
        }
    }

}
