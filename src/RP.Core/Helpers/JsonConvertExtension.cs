using Newtonsoft.Json;

namespace RP.Core.Helpers
{
    internal class JsonConvertExtension
    {
        public static T Map<T>(string objString)
        {
            return JsonConvert.DeserializeObject<T>(objString);
        }

        public static string Map<T>(T bodyObject)
        {
            return JsonConvert.SerializeObject(bodyObject);
        }
    }
}
