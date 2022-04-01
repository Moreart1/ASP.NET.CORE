using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerClient.DTO
{
    public interface IMetricDTO
    {
        int Id { get; set; }

        int Value { get; set; }

        DateTime Date { get; set; }
    }
}
