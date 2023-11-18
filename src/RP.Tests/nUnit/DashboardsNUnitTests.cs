using FluentAssertions;
using NUnit.Framework;
using RP.Business.API.Models;
using RP.Core.Helpers;
using RP.Tests.TestDataProviders;
using System.Net;

namespace RP.Tests.nUnit
{
    public class DashboardsNUnitTests : BaseNUnitTest
    {
        [TestCaseSource(typeof(DashboardProvider), nameof(DashboardProvider.GetDashboardSource))]
        public async Task VerifyDashboardCreation(DashboardDto dashboard)
        {
            var response = await Configuration.DashboardApiClient.CreateDashboard(PROJECT_NAME, dashboard);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var dashboardId = response.GetContentAs<DashboardDto>().Id;
            response = await Configuration.DashboardApiClient.GetDashboardById(PROJECT_NAME, dashboardId);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdDashboard = response.GetContentAs<DashboardDto>();
            createdDashboard.Name.Should().Be(dashboard.Name);
            createdDashboard.Description.Should().Be(dashboard.Description);
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task VerifyDashboardGetting(int dashboardAmount)
        {
            for (int i = 0; i < dashboardAmount; i++)
            {
                var responseCreation = await Configuration.DashboardApiClient.CreateDashboard(PROJECT_NAME, DashboardProvider.GetDashboard());
                responseCreation.StatusCode.Should().Be(HttpStatusCode.Created);
            }
            var response = await Configuration.DashboardApiClient.GetAllDashboards(PROJECT_NAME);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var dashboards = response.GetContentAs<DashboardResponceDto>().Dashboards;
            dashboards.Count().Should().Be(dashboardAmount);
        }

        [Test]
        public async Task VerifyDashboardEditing()
        {
            var response = await Configuration.DashboardApiClient.CreateDashboard(PROJECT_NAME, DashboardProvider.GetDashboard());
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var dashboardId = response.GetContentAs<DashboardDto>().Id;

            var dashboardToUpdate = DashboardProvider.GetDashboard();
            response = await Configuration.DashboardApiClient.UpdateDashboardById(PROJECT_NAME, dashboardId, dashboardToUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response = await Configuration.DashboardApiClient.GetDashboardById(PROJECT_NAME, dashboardId);
            var updatedDashboard = response.GetContentAs<DashboardDto>();

            updatedDashboard.Name.Should().Be(dashboardToUpdate.Name);
            updatedDashboard.Description.Should().Be(dashboardToUpdate.Description);
        }
    }
}
