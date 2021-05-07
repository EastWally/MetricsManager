using MetricsAgent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public class DBConfig: IDBConfig
    {
        public string ConnectionString { get; set; } = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    }
}
