using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IDBConfig
    {
        public string ConnectionString { get; set; }
    }
}
