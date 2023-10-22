using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RP.Core.API;
using RP.Core.Models;

namespace RP.Tests
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            Configuration = builder;
        }

        public ServiceProvider Build()
        {
            var services = new ServiceCollection();
            RegisterHttpClients(services, Configuration.GetSection("ApiSettings"));
            return services.BuildServiceProvider();
        }

        private void RegisterHttpClients(ServiceCollection services, IConfigurationSection apiSettingsSection)
        {
            services.Configure<ApiSettings>(apiSettingsSection);
            services.AddSingleton(p => p.GetRequiredService<IOptions<ApiSettings>>().Value);

            services.AddHttpClient<DashboardApiClient>();
        }
    }
}