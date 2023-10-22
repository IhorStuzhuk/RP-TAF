using FluentAssertions;
using RP.Business.Models;
using RP.Core.API.Helpers;
using System.Net;
using Xunit;

namespace RP.Tests.xUnit
{
    public class BaseXUnitTest : IDisposable
    {
        protected const string PROJECT_NAME = "default_personal";

        public BaseXUnitTest() { }

        public void Dispose()
        {
            DeleteAllCreatedDashboards();
        }

        private async void DeleteAllCreatedDashboards()
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

