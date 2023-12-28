using Microsoft.Extensions.DependencyInjection;
using RP.Business.API.Services;
using RP.Business.Config;
using RP.Business.Models;
using RP.Tests.Services;
using RP.Tests.Services.Jira;

namespace RP.Tests
{
    public static class Configuration
    {
        private static readonly Lazy<ServiceProvider> Provider = new(() => new Startup().Build());

        private static ServiceProvider Instance => Provider.Value;

        public static DashboardApiService DashboardApiService => Instance.GetService<DashboardApiService>();

        public static MSTeamsService MSTeamsService => Instance.GetService<MSTeamsService>();

        public static JiraService JiraService => Instance.GetService<JiraService>();

        public static string ProjectName => Instance.GetService<ApiConfig>().ProjectName;

        public static WebBrowserConfig WebBrowserConfig => Instance.GetService<WebBrowserConfig>();

        public static WebConfig WebConfig => Instance.GetService<WebConfig>();

        public static UserConfig UserConfig => Instance.GetService<UserConfig>();
    }
}