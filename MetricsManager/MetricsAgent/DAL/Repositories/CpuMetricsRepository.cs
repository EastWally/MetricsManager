using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;
using Dapper;
using System.Linq;

namespace MetricsAgent.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private string _connectionString;

        public CpuMetricsRepository(IDBConfig dBConfig)
        {
            _connectionString = dBConfig.ConnectionString;
        }

        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",                    
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }
        }

        public IList<CpuMetric> GetByPeriod(PeriodArgs args)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new 
                    { 
                        fromTime = args.FromTime.ToUnixTimeSeconds(),
                        toTime = args.ToTime.ToUnixTimeSeconds()
                    }).ToList();
            }
        }
    }

}
