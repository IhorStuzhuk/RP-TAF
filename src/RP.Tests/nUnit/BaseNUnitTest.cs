using FluentAssertions;
using NUnit.Framework;
using RP.Business.API.Models;
using RP.Core.Helpers;
using System.Net;

[assembly: LevelOfParallelism(3)]
[assembly: Parallelizable(ParallelScope.Children)]
namespace RP.Tests.nUnit
{
    [TestFixture]
    public class BaseNUnitTest
    {
        protected const string PROJECT_NAME = "default_personal";

        public BaseNUnitTest() { }

        [OneTimeTearDown]
        public async Task TearDownAsync()
        {
            await Configuration.DashboardApiService.DeleteAllCreatedDashboards();
        }
    }
}
