using MetricsManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL
{
    public class DBConfig: IDBConfig
    {
        public string ConnectionString { get; set; } = "Data Source=metricsManager.db;Version=3;Pooling=true;Max Pool Size=100;";
    }
}
