using System.Text;

namespace RP.Core.Helpers
{
    public static class StringHelper
    {
        public static string RandomString(int length)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for(int i = 0; i < length; i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))));
            }
            return builder.ToString();
        }
    }
}
