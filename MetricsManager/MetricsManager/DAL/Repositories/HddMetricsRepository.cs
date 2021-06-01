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
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<HddMetricsRepository> _logger;

        public HddMetricsRepository(IDBConfig dBConfig, ILogger<HddMetricsRepository> logger)
        {
            _connectionString = dBConfig.ConnectionString;
            _logger = logger;
        }

        public void Create(HddMetric item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            try
            {
                connection.Execute("INSERT INTO hddmetrics(value, time, agentId) VALUES(@value, @time, @agentId)",
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

        public IList<HddMetric> GetByPeriodFromAgent(PeriodAgentArgs args)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE (agentId = @agentId) AND (time BETWEEN @fromTime AND @toTime)",
                new
                {
                    agentId = args.AgentId,
                    fromTime = args.FromTime.ToUnixTimeSeconds(),
                    toTime = args.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public IList<HddMetric> GetByPeriod(PeriodArgs args)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE time BETWEEN @fromTime AND @toTime",
                new
                {
                    fromTime = args.FromTime.ToUnixTimeSeconds(),
                    toTime = args.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }


        public DateTimeOffset GetLastDate(int agentId)
        {
            using var connection = new SQLiteConnection(_connectionString);
            var metric = connection.QueryFirstOrDefault<HddMetric>("SELECT * FROM hddmetrics WHERE agentId = @agentId ORDER BY time DESC LIMIT 1",
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
