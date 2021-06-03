namespace MetricsManager.DAL.Models
{
    public class CpuMetric
    {
        public int Value { get; set; }

        public long Time { get; set; }

        public int AgentId { get; set; }
    }
}
