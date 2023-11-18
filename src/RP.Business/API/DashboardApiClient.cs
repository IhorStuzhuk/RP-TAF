using Microsoft.Extensions.Options;
using RP.Business.API.Models;
using RP.Core.Helpers;
using System.Net.Http.Headers;

namespace RP.Business.API
{
    public class DashboardApiClient
    {
        private readonly HttpClient _httpClient;

        public DashboardApiClient(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            httpClient.BaseAddress = new Uri(apiSettings.Value.Host + "/api/v1/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "name_pgbM9iU3SmG-7DiTmNnKRwSJZd5zH85RqfUtkuJMhX08CkRcMc5jlrkPdMIpCtA6");
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreateDashboard(string projectName, DashboardDto dashboard)
        {
            return await _httpClient.PostJson($"{projectName}/dashboard", dashboard);
        }

        public async Task<HttpResponseMessage> GetDashboardById(string projectName, int id)
        {
            return await _httpClient.GetAsync($"{projectName}/dashboard/{id}");
        }

        public async Task<HttpResponseMessage> GetAllDashboards(string projectName)
        {
            return await _httpClient.GetAsync($"{projectName}/dashboard");
        }

        public async Task<HttpResponseMessage> DeleteDashboardById(string projectName, int id)
        {
            return await _httpClient.DeleteAsync($"{projectName}/dashboard/{id}");
        }


        public async Task<HttpResponseMessage> UpdateDashboardById(string projectName, int id, DashboardDto dashboardToUpdate)
        {
            return await _httpClient.PutJson($"{projectName}/dashboard/{id}", dashboardToUpdate);
        }
    }
}
