using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;

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
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<CpuMetric> GetByPeriod(PeriodArgs args)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime";
            cmd.Parameters.AddWithValue("@fromTime", args.FromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@toTime", args.ToTime.ToUnixTimeSeconds());
            cmd.Prepare();
            var returnList = new List<CpuMetric>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new CpuMetric
                    {
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt64(2)
                    });
                }
            }
            return returnList;
        }
    }

}
