using System;
namespace MetricsManager.Controllers.AgentsController.Requests
{
    public class RegisterAgentRequest
    {
        /// <summary>
        /// Идентификатор агента
        /// </summary>
        public int AgentId { get; set; }

        /// <summary>
        /// Адрес агента
        /// </summary>
        public Uri AgentAddress { get; set; }
    }
}
