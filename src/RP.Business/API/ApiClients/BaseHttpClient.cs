using RP.Business.API.Extension;
using RP.Business.Config;
using System.Net.Http.Headers;

namespace RP.Business.API.ApiClients
{
    public class BaseHttpClient : IHttpClientAsync
    {
        private readonly HttpClient _httpClient;

        public BaseHttpClient(ApiConfig apiSettings) 
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiSettings.BearerToken);
        }

        public async Task<HttpResponse> GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + url);
            return new HttpResponse { StatusCode = response.StatusCode, Content = await response.Content.ReadAsStringAsync()};
        }

        public async Task<HttpResponse> PostAsync<T>(string url, T data) where T : class
        {
            var response = await _httpClient.PostJson(_httpClient.BaseAddress + url, data);
            return new HttpResponse { StatusCode = response.StatusCode, Content = await response.Content.ReadAsStringAsync() };
        }

        public async Task<HttpResponse> PutAsync<T>(string url, T data) where T : class
        {
            var response = await _httpClient.PutJson(_httpClient.BaseAddress + url, data);
            return new HttpResponse { StatusCode = response.StatusCode, Content = await response.Content.ReadAsStringAsync() };
        }

        public async Task<HttpResponse> DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + url);
            return new HttpResponse { StatusCode = response.StatusCode, Content = await response.Content.ReadAsStringAsync() };
        }
    }
}