using RP.Business.API.ApiClients;

namespace RP.Business
{
    public interface IHttpClientAsync
    {
        Task<HttpResponse> GetAsync(string url);
        Task<HttpResponse> PostAsync<T>(string url, T data) where T : class;
        Task<HttpResponse> PutAsync<T>(string url, T data) where T : class;
        Task<HttpResponse> DeleteAsync(string url);
    }
}