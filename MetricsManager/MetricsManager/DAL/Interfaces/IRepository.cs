using System;
using System.Collections.Generic;
using MetricsManager.DAL;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByPeriodFromAgent(PeriodAgentArgs args);

        IList<T> GetByPeriod(PeriodArgs args);

        DateTimeOffset GetLastDate(int agentId);

        void Create(T item);
    }

}
