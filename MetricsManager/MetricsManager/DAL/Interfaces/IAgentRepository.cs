using MetricsManager.DAL.Models;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgentRepository
    {
        IList<Agents> GetAgents();

        void RegisterAgent(Agents item);

        void EnableAgentById(int id);

        void DisableAgentById(int id);
    }
}
