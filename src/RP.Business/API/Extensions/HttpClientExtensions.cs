using RP.Core.Helpers;
using System.Text;

namespace RP.Business.API.Extension
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

        public static T Get<T>(this HttpClient client, string uri)
        {
            var response = client.GetAsync(uri).Result.Content.ReadAsStringAsync();
            return JsonConvertExtension.Map<T>(response.Result);
        }

        private static StringContent GetContent<T>(T body)
        {
            return new StringContent(JsonConvertExtension.Map(body), Encoding.UTF8, "application/json");
        }
    }
}
