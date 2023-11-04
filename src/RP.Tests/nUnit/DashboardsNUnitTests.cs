using FluentAssertions;
using NUnit.Framework;
using RP.Business.Models;
using RP.Core.API.Helpers;
using RP.Core.TestDataProviders;
using System.Net;

namespace RP.Tests.nUnit
{
    public class DashboardsNUnitTests : BaseNUnitTest
    {
        [TestCaseSource(typeof(DashboardProvider), nameof(DashboardProvider.GetDashboardSource))]
        public async Task VerifyDashboardCreation(DashboardDto dashboard)
        {
            var response = await Configuration.DashboardApiClient.CreateDashboard(dashboard);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var dashboardId = response.GetContentAs<DashboardDto>().Id;
            response = await Configuration.DashboardApiClient.GetDashboardById(dashboardId);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdDashboard = response.GetContentAs<DashboardDto>();
            createdDashboard.Name.Should().Be(dashboard.Name);
            createdDashboard.Description.Should().Be(dashboard.Description);
        }

        [Test]
        [NonParallelizable]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task VerifyDashboardGetting(int dashboardAmount)
        {
            await Configuration.DashboardApiClient.DeleteAllCreatedDashboards();
            for (int i = 0; i < dashboardAmount; i++)
            {
                var responseCreation = await Configuration.DashboardApiClient.CreateDashboard(DashboardProvider.GetDashboard());
                responseCreation.StatusCode.Should().Be(HttpStatusCode.Created);
            }
            var response = await Configuration.DashboardApiClient.GetAllDashboards();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var dashboards = response.GetContentAs<DashboardResponceDto>().Dashboards;
            dashboards.Count().Should().Be(dashboardAmount);
        }

        [Test]
        public async Task VerifyDashboardEditing()
        {
            var response = await Configuration.DashboardApiClient.CreateDashboard(DashboardProvider.GetDashboard());
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var dashboardId = response.GetContentAs<DashboardDto>().Id;

            var dashboardToUpdate = DashboardProvider.GetDashboard();
            response = await Configuration.DashboardApiClient.UpdateDashboardById(dashboardId, dashboardToUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response = await Configuration.DashboardApiClient.GetDashboardById( dashboardId);
            var updatedDashboard = response.GetContentAs<DashboardDto>();

            updatedDashboard.Name.Should().Be(dashboardToUpdate.Name);
            updatedDashboard.Description.Should().Be(dashboardToUpdate.Description);
        }
    }
}
