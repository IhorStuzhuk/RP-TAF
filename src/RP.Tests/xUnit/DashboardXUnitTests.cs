using FluentAssertions;
using RP.Business.API.Models;
using RP.Core.Helpers;
using RP.Tests.TestDataProviders;
using System.Net;
using Xunit;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace RP.Tests.xUnit
{
    public class DashboardXUnitTests : BaseXUnitTest
    {
        [Theory]
        [MemberData(nameof(DashboardProvider.GetDashboardSource), MemberType = typeof(DashboardProvider))]
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

        [Theory]
        [InlineData(5)]
        [InlineData(3)]
        [InlineData(1)]
        public async Task VerifyDashboardGetting(int dashboardAmount)
        {
            for(int i = 0; i < dashboardAmount; i++)
            {
                var responseCreation = await Configuration.DashboardApiClient.CreateDashboard(DashboardProvider.GetDashboard());
                responseCreation.StatusCode.Should().Be(HttpStatusCode.Created);
            }
            var response = await Configuration.DashboardApiClient.GetAllDashboards();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var dashboards = response.GetContentAs<DashboardResponceDto>().Dashboards;
            dashboards.Count().Should().Be(dashboardAmount);
        }

        [Fact]
        public async Task VerifyDashboardEditing()
        {
            var response = await Configuration.DashboardApiClient.CreateDashboard(DashboardProvider.GetDashboard());
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var dashboardId = response.GetContentAs<DashboardDto>().Id;

            var dashboardToUpdate = DashboardProvider.GetDashboard();
            response = await Configuration.DashboardApiClient.UpdateDashboardById(dashboardId, dashboardToUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response = await Configuration.DashboardApiClient.GetDashboardById(dashboardId);
            var updatedDashboard = response.GetContentAs<DashboardDto>();

            updatedDashboard.Name.Should().Be(dashboardToUpdate.Name);
            updatedDashboard.Description.Should().Be(dashboardToUpdate.Description);
        }
    }
}
