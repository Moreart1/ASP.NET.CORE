using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Request;

namespace MetricsAgent.DAL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricsDto>();

            CreateMap<DotNetMetric, DotNetMetricsDto>();

            CreateMap<HddMetric, HddMetricsDto>();

            CreateMap<NetworkMetric, NetworkMetricsDto>();

            CreateMap<RamMetric, RamMetricsDto>();
        }
    }
}
