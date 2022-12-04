namespace MetricsManager.Model
{
    public class CpuMetric
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public long Time { get; set; }
        public int Value { get; set; }
    }
}
