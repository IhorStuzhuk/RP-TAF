using Microsoft.Extensions.DependencyInjection;
using RP.Business.API;

namespace RP.Tests
{
    public static class Configuration
    {
        private static readonly Lazy<ServiceProvider> Provider = new(() => new Startup().Build());

        private static ServiceProvider Instance => Provider.Value;

        public static DashboardApiClient DashboardApiClient => Instance.GetService<DashboardApiClient>();
    }
}
