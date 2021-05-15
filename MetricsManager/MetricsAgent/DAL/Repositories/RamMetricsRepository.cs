using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;
using Dapper;
using System.Linq;

namespace MetricsAgent.DAL.Repositories
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private string _connectionString;

        public RamMetricsRepository(IDBConfig dBConfig)
        {
            _connectionString = dBConfig.ConnectionString;
        }

        public void Create(RamMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public IList<RamMetric> GetByPeriod(PeriodArgs args)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<RamMetric>("SELECT * FROM rammetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        fromTime = args.FromTime.ToUnixTimeSeconds(),
                        toTime = args.ToTime.ToUnixTimeSeconds()
                    }).ToList();
            }
        }
    }

}
