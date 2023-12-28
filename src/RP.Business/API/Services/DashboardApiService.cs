using RP.Business.API.ApiClients;
using RP.Business.API.Extensions;
using RP.Business.API.Models;
using RP.Business.Config;

namespace RP.Business.API.Services
{
    public class DashboardApiService
    {
        private readonly IHttpClientAsync _httpClient;
        private readonly string _url;

        public DashboardApiService(IHttpClientAsync httpClient, ApiConfig apiSettings)
        {
            _httpClient = httpClient;
            _url = $"/{apiSettings.ProjectName}/dashboard";
        }

        public async Task<HttpResponse> CreateDashboard(DashboardDto dashboard)
        {
            return await _httpClient.PostAsync(_url, dashboard);
        }

        public async Task<HttpResponse> GetDashboardById(int id)
        {
            return await _httpClient.GetAsync($"{_url}/{id}".ToString());
        }

        public async Task<HttpResponse> GetAllDashboards()
        {
            return await _httpClient.GetAsync(_url);
        }

        public async Task<HttpResponse> DeleteDashboardById(int id)
        {
            return await _httpClient.DeleteAsync($"{_url}/{id}");
        }

        public async Task<HttpResponse> UpdateDashboardById(int id, DashboardDto dashboardToUpdate)
        {
            return await _httpClient.PutAsync($"{_url}/{id}", dashboardToUpdate);
        }

        public async Task DeleteAllCreatedDashboards()
        {
            var response = await GetAllDashboards();
            var dashboards = response.GetContentAs<DashboardResponceDto>().Dashboards;
            foreach(var db in dashboards)
                {
                    response = await DeleteDashboardById(db.Id);
                if(!response.IsSuccessStatusCode())
                        throw new HttpRequestException($"Dashboard with id: {db.Id} was not deleted");
                }
        }
    }
}
