using RP.Business.API.ApiClients;
using RP.Core.Helpers;

namespace RP.Business.API.Extensions
{
    public static class HttpResponseExtensions
    {
        public static T GetContentAs<T>(this HttpResponse response)
        {
            return JsonConvertExtension.Map<T>(response.Content);
        }

        public static bool IsSuccessStatusCode(this HttpResponse response)
        {
            return ((int)response.StatusCode >= 200) && ((int)response.StatusCode <= 299);
        }
    }
}
