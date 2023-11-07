namespace RP.Business.API
{
    public class RPHttpClient
    {
        private readonly HttpClient _httpClient;

        public RPHttpClient(ApiSettings apiSettings)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri($"{apiSettings.Host}/api/v1/")};
        }
    }
}
