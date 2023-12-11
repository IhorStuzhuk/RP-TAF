using Microsoft.Extensions.Configuration;

namespace RP.Core
{
    internal sealed class AppConfig
    {
        private IConfiguration _configuration;
        private static readonly Lazy<AppConfig> lazy = new Lazy<AppConfig>(() => new AppConfig());

        public static IConfiguration Instance { get { return lazy.Value._configuration; } }

        private AppConfig()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
