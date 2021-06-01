using MetricsManager.Client.Responses;
using MetricsManager.Client.Requests;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);        

        AllHddMetricsApiResponse GetHddMetrics(GetAllHddMetricsApiRequest request);

        AllDotNetMetricsApiResponse GetDotNetMetrics(GetAllDotNetMetricsApiRequest request);

        AllNetworkMetricsApiResponse GetNetworkMetrics(GetAllNetworkMetricsApiRequest request);

        AllRamMetricsApiResponse GetRamMetrics(GetAllRamMetricsApiRequest request);
    }
}
