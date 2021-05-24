using AutoMapper;
using MetricsAgent.Controllers.CpuMetricsContoller.Requests;
using MetricsAgent.Controllers.CpuMetricsContoller.Responses;
using MetricsAgent.Controllers.DotNetMetricsController.Requests;
using MetricsAgent.Controllers.DotNetMetricsController.Responses;
using MetricsAgent.Controllers.HddMetricsController.Requests;
using MetricsAgent.Controllers.HddMetricsController.Responses;
using MetricsAgent.Controllers.NetworkMetricsController.Requests;
using MetricsAgent.Controllers.NetworkMetricsController.Responses;
using MetricsAgent.Controllers.RamMetricsController.Requests;
using MetricsAgent.Controllers.RamMetricsController.Responses;
using MetricsAgent.DAL.Models;
using System;

namespace MetricsAgent
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

            CreateMap<CpuMetricCreateRequest, CpuMetric>().ForMember("Time", m => m.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<DotNetMetricCreateRequest, DotNetMetric>().ForMember("Time", m => m.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<HddMetricCreateRequest, HddMetric>().ForMember("Time", m => m.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<NetworkMetricCreateRequest, NetworkMetric>().ForMember("Time", m => m.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<RamMetricCreateRequest, RamMetric>().ForMember("Time", m => m.MapFrom(t => t.Time.ToUnixTimeSeconds()));
        }
    }
}
