using NUnit.Framework.Internal;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace RP.Tests.BDD.Hooks
{
    [Binding]
    public class Hooks
    {
        [AfterFeature]
        public static async Task AfterFeature()
        {
            await Configuration.DashboardApiService.DeleteAllCreatedDashboards();
        }
    }
}