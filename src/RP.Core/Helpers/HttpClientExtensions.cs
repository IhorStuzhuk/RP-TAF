using System.Text;
using RP.Core.Helpers;

namespace RP.Core.Helpers
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostJson<T>(this HttpClient client, string uri, T request)
        {
            return client.PostAsync(uri, GetContent(request));
        }

        public static Task<HttpResponseMessage> PutJson<T>(this HttpClient client, string uri, T request)
        {
            return client.PutAsync(uri, GetContent(request));
        }

        public static Task<HttpResponseMessage> PatchJson<T>(this HttpClient client, string uri, T request)
        {
            return client.PatchAsync(uri, GetContent(request));
        }

        public static Task<HttpResponseMessage> Get<T>(this HttpClient client, string uri)
        {
            return client.GetAsync(uri);
        }

        private static StringContent GetContent<T>(T body)
        {
            return new StringContent(JsonConvertExtension.Map(body), Encoding.UTF8, "application/json");
        }
    }
}
