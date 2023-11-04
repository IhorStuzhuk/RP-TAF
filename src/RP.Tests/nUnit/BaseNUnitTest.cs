using NUnit.Framework;

[assembly: LevelOfParallelism(3)]
[assembly: Parallelizable(ParallelScope.Children)]
namespace RP.Tests.nUnit
{
    [TestFixture]
    public class BaseNUnitTest
    {
        public BaseNUnitTest() { }

        [OneTimeTearDown]
        public async Task TearDownAsync()
        {
            await Configuration.DashboardApiClient.DeleteAllCreatedDashboards();
        }
    }
}
