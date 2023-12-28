using RestSharp;
using RP.Business.Models;

namespace RP.Business.API.ApiClients
{
    public class RestSharpClient : IHttpClientAsync
    {
        private readonly RestClient _restClient;

        public RestSharpClient(ApiSettings apiSettings) 
        {
            _restClient = new RestClient(apiSettings.Host);
            _restClient.AddDefaultHeader("Content-Type", "application/json");
            _restClient.AddDefaultHeader("Authorization", $"Bearer {apiSettings.BearerToken}");
        }

        public async Task<HttpResponse> GetAsync(string url)
        {
            var request = new RestRequest(_restClient.Options.BaseHost + url, Method.Get);
            var response = await _restClient.ExecuteAsync(request);
            return new HttpResponse { StatusCode = response.StatusCode, Content = response.Content };
        }

        public async Task<HttpResponse> PostAsync<T>(string url, T data) where T : class 
        {
            var request = new RestRequest(_restClient.Options.BaseHost + url, Method.Post);
            request.AddJsonBody(data);
            var response = await _restClient.ExecuteAsync(request);
            return new HttpResponse { StatusCode = response.StatusCode, Content = response.Content };
        }

        public async Task<HttpResponse> PutAsync<T>(string url, T data) where T : class
        {
            var request = new RestRequest(_restClient.Options.BaseHost + url, Method.Put);
            request.AddJsonBody(data);
            var response = await _restClient.ExecuteAsync(request);
            return new HttpResponse { StatusCode = response.StatusCode, Content = response.Content };
        }

        public async Task<HttpResponse> DeleteAsync(string url)
        {
            var request = new RestRequest(_restClient.Options.BaseHost + url, Method.Delete);
            var response = await _restClient.ExecuteAsync(request);
            return new HttpResponse { StatusCode = response.StatusCode, Content = response.Content };
        }
    }
}
