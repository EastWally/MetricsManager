using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;
using Dapper;
using System.Linq;

namespace MetricsAgent.DAL.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly string _connectionString;

        public NetworkMetricsRepository(IDBConfig dBConfig)
        {
            _connectionString = dBConfig.ConnectionString;
        }

        public void Create(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
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
    }

}
