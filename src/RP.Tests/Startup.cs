using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RP.Business;
using RP.Business.API;
using RP.Business.API.ApiClients;
using RP.Business.API.Services;

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
            RegisterApiServices(services);
            return services.BuildServiceProvider();
        }

        private void RegisterHttpClients(ServiceCollection services, IConfigurationSection apiSettingsSection)
        {
            services.Configure<ApiSettings>(apiSettingsSection);
            services.AddSingleton<ApiSettings>(p => p.GetRequiredService<IOptions<ApiSettings>>().Value);

            services.AddSingleton<IHttpClientAsync>(sp => { return new RestSharpClient(sp.GetRequiredService<ApiSettings>()); });//possible to switch to BaseHttpClient

        }
        private void RegisterApiServices(ServiceCollection services)
        {
            services.AddTransient<DashboardApiService>(sp => new DashboardApiService(sp.GetRequiredService<IHttpClientAsync>(), sp.GetRequiredService<ApiSettings>()));
        }
    }
}