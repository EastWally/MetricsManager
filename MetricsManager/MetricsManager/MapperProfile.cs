using AutoMapper;
using MetricsManager.Controllers.AgentsController.Responses;
using MetricsManager.Controllers.CpuMetricsContoller.Responses;
using MetricsManager.Controllers.DotNetMetricsController.Responses;
using MetricsManager.Controllers.HddMetricsController.Responses;
using MetricsManager.Controllers.NetworkMetricsController.Responses;
using MetricsManager.Controllers.RamMetricsController.Responses;
using MetricsManager.DAL.Models;
using System;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>().ForMember("Time", m => m.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));
            CreateMap<DotNetMetric, DotNetMetricDto>().ForMember("Time", m => m.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));
            CreateMap<HddMetric, HddMetricDto>().ForMember("Time", m => m.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));
            CreateMap<NetworkMetric, NetworkMetricDto>().ForMember("Time", m => m.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));
            CreateMap<RamMetric, RamMetricDto>().ForMember("Time", m => m.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));
            CreateMap<Agents, AgentInfoDto>();
        }
    }
}
