using AutoMapper;
using MetricsManager.Model;
using MetricsManager.Response.Responses;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {          
            CreateMap<AgentInfo, AgentsResponse>();

            CreateMap<CpuMetric, CpuMetricResponse>();

            CreateMap<DotNetMetric, DotNetMetricResponse>();

            CreateMap<HddMetric, HddMetricResponse>();

            CreateMap<NetworkMetric, NetworkMetricResponse>();

            CreateMap<RamMetric, RamMetricResponse>();          
           
        }
    }
}
