using FluentAssertions;
using NUnit.Framework;
using RP.Business.Models;
using RP.Core.API.Helpers;
using System.Net;

[assembly: LevelOfParallelism(3)]
[assembly: Parallelizable(ParallelScope.All)]
namespace RP.Tests.nUnit
{
    [TestFixture]
    public class BaseNUnitTest
    {
        protected const string PROJECT_NAME = "default_personal";

        public BaseNUnitTest() { }

        [TearDown]
        public void TearDown()
        {
            DeleteAllCreatedDashboards();
        }

        protected async void DeleteAllCreatedDashboards()
        {
            var response = await Configuration.DashboardApiClient.GetAllDashboards(PROJECT_NAME);
            var dashboards = response.GetContentAs<DashboardResponceDto>().Dashboards;
            if(dashboards.Count > 0)
                foreach(var db in dashboards)
                {
                    response = await Configuration.DashboardApiClient.DeleteDashboardById(PROJECT_NAME, db.Id);
                    response.StatusCode.Should().Be(HttpStatusCode.OK);
                }
        }
    }
}
