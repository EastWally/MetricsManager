using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Interfaces;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace MetricsManager.DAL.Repositories
{
    public class AgentsRepository : IAgentRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<AgentsRepository> _logger;

        public AgentsRepository(IDBConfig dBConfig, ILogger<AgentsRepository> logger)
        {
            _connectionString = dBConfig.ConnectionString;
            _logger = logger;
        }

        public void RegisterAgent(Agents item)
        {
            try
            {
                using var connection = new SQLiteConnection(_connectionString);
                connection.Execute("INSERT INTO agents(agentId, agentUrl, enabled) VALUES(@agentId, @agentUrl, @enabled)",
                    new
                    {
                        agentId = item.AgentId,
                        agentUrl = item.AgentUrl,
                        enabled = item.Enabled
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public IList<Agents> GetAgents()
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection.Query<Agents>("SELECT * FROM agents").ToList();
        }

        public void EnableAgentById(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            try
            {
                connection.Execute("UPDATE agents SET enabled = true WHERE agentId=@agentId",
                    new
                    {
                        agentId = id
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void DisableAgentById(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            try
            {
                connection.Execute("UPDATE agents SET enabled = false WHERE agentId=@agentId",
                    new
                    {
                        agentId = id
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }

}
