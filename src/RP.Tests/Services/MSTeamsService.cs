using RP.Business.API.Extension;
using RP.Core;

namespace RP.Tests.Services
{
    public class MSTeamsService
    {
        private readonly HttpClient _httpClient;

        public MSTeamsService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(AppConfig.Instance.GetSection("MSTeamsWebHookUrl").Value);
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostNotification(string message)
        {
            return await _httpClient.PostJson(_httpClient.BaseAddress.AbsoluteUri, new { text = message });
        }
    }
}