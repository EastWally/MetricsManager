using System;
using System.Collections.Generic;
using MetricsAgent.DAL;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByPeriod(PeriodArgs args);

        void Create(T item);
    }

}
