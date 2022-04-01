using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerClient.DTO
{
    public class MetricDTO : IMetricDTO
    {
        public int Value { get; set; }

        public DateTime Date { get; set; }

        public int Id { get; set; }
    }
}
