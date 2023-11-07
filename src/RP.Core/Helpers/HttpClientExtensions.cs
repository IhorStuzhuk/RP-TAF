using System.Text;

namespace RP.Core.Helpers
{
    internal static class HttpClientExtensions
    {
        internal static Task<HttpResponseMessage> PostJson<T>(this HttpClient client, string uri, T request)
        {
            return client.PostAsync(uri, GetContent(request));
        }

        internal static Task<HttpResponseMessage> PutJson<T>(this HttpClient client, string uri, T request)
        {
            return client.PutAsync(uri, GetContent(request));
        }

        internal static Task<HttpResponseMessage> PatchJson<T>(this HttpClient client, string uri, T request)
        {
            return client.PatchAsync(uri, GetContent(request));
        }

        internal static Task<HttpResponseMessage> Get<T>(this HttpClient client, string uri)
        {
            return client.GetAsync(uri);
        }

        private static StringContent GetContent<T>(T body)
        {
            return new StringContent(JsonConvertExtension.Map(body), Encoding.UTF8, "application/json");
        }
    }
}
