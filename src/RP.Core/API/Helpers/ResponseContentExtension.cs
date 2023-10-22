﻿using RP.Core.Helpers;

namespace RP.Core.API.Helpers
{
    public static class ResponseContentExtension
    {
        public static T GetContentAs<T>(this HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvertExtension.Map<T>(content);
        }
    }
}
