namespace MetricsAgent.Request
{
    public class CpuMetricsDto
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
