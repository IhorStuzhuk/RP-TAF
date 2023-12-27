using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RP.Business;
using RP.Business.API.ApiClients;
using RP.Business.API.Services;
using RP.Business.Config;
using RP.Business.Models;
using RP.Tests.Services;
using RP.Tests.Services.Jira;

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
            RegisterHttpClients(services, Configuration.GetSection("ApiConfig"));
            RegisterApiServices(services);
            ConfigureDriver(services);
            ConfigureUser(services);
            return services.BuildServiceProvider();
        }

        private void RegisterHttpClients(ServiceCollection services, IConfigurationSection apiSettingsSection)
        {
            services.Configure<ApiConfig>(apiSettingsSection);
            services.AddSingleton<ApiConfig>(p => p.GetRequiredService<IOptions<ApiConfig>>().Value);

            services.AddSingleton<IHttpClientAsync>(sp => { return new RestSharpClient(sp.GetRequiredService<ApiConfig>()); });//possible to switch to BaseHttpClient

        }
        private void RegisterApiServices(ServiceCollection services)
        {
            services.Configure<JiraConfig>(Configuration.GetSection("JiraConfig"));
            services.AddSingleton<JiraConfig>(p => p.GetRequiredService<IOptions<JiraConfig>>().Value);

            services.AddTransient<DashboardApiService>(sp => new DashboardApiService(sp.GetRequiredService<IHttpClientAsync>(), sp.GetRequiredService<ApiConfig>()));
            services.AddHttpClient<MSTeamsService>();
            services.AddSingleton<JiraService>(sp => new JiraService(new HttpClient(), sp.GetRequiredService<JiraConfig>()));
        }

        private void ConfigureDriver(ServiceCollection services)
        {
            services.Configure<WebBrowserConfig>(Configuration.GetSection("WebBrowserConfig"));
            services.Configure<WebConfig>(Configuration.GetSection("WebConfig"));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<WebBrowserConfig>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<WebConfig>>().Value);
        }

        private void ConfigureUser(ServiceCollection services)
        {
            services.Configure<UserConfig>(Configuration.GetSection("UserConfig"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<UserConfig>>().Value);
        }
    }
}