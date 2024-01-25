using NUnit.Framework;
using NUnit.Framework.Internal;

[assembly: LevelOfParallelism(3)]
[assembly: Parallelizable(ParallelScope.Children)]
namespace RP.Tests.nUnit
{
    [TestFixture]
    public class BaseTest
    {
        [OneTimeSetUp]
        public async Task SetUpAsync()
        {
            await Configuration.MSTeamsService.PostNotification($"'{TestExecutionContext.CurrentContext.TestObject.GetType().Name}' fixture run has been started!");
        }

        [OneTimeTearDown]
        public async Task TearDownAsync()
        {
            var testsResults = TestContext.CurrentContext.Result;
            await Configuration.MSTeamsService.PostNotification($"'{TestExecutionContext.CurrentContext.TestObject.GetType().Name}' fixture run finished!\n" +
                                                                $"Passed: {testsResults.PassCount}, " +
                                                                $"Failed: {testsResults.FailCount}, " +
                                                                $"Skipped: {testsResults.SkipCount}, " +
                                                                $"Total: {testsResults.PassCount + testsResults.FailCount + testsResults.SkipCount}, " +
                                                                $"Duration: {TestExecutionContext.CurrentContext.Duration}");
            await Configuration.DashboardApiService.DeleteAllCreatedDashboards();
        }
    }
}