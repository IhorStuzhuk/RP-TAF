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
            httpClient.BaseAddress = new Uri(apiSettings.Value.Host + $"/api/v1/{apiSettings.Value.ProjectName}/dashboard/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiSettings.Value.BearerToken);
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreateDashboard(DashboardDto dashboard)
        {
            return await _httpClient.PostJson(_httpClient.BaseAddress.ToString(), dashboard);
        }

        public async Task<HttpResponseMessage> GetDashboardById(int id)
        {
            return await _httpClient.GetAsync(id.ToString());
        }

        public async Task<HttpResponseMessage> GetAllDashboards()
        {
            return await _httpClient.GetAsync(_httpClient.BaseAddress.ToString());
        }

        public async Task<HttpResponseMessage> DeleteDashboardById(int id)
        {
            return await _httpClient.DeleteAsync(id.ToString());
        }

        public async Task<HttpResponseMessage> UpdateDashboardById(int id, DashboardDto dashboardToUpdate)
        {
            return await _httpClient.PutJson(id.ToString(), dashboardToUpdate);
        }

        public async Task DeleteAllCreatedDashboards()
        {
            var response = await GetAllDashboards();
            var dashboards = response.GetContentAs<DashboardResponceDto>().Dashboards;
            if(dashboards.Count > 0)
                foreach(var db in dashboards)
                {
                    response = await DeleteDashboardById(db.Id);
                    if(!response.IsSuccessStatusCode)
                        throw new HttpRequestException($"Dashboard with id: {db.Id} was not deleted");
                }
        }
    }
}
