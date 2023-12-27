using FluentAssertions;
using NUnit.Framework;
using RP.Business.API.Models;
using RP.Business.API.Extensions;
using RP.Core.Helpers;
using RP.Tests.TestDataProviders;
using System.Net;
using RP.Business;

namespace RP.Tests.nUnit
{
    public class DashboardsNUnitTests : BaseTest
    {
        [TestCaseSource(typeof(DashboardProvider), nameof(DashboardProvider.GetDashboardSource))]
        public async Task VerifyDashboardCreation(DashboardDto dashboard)
        {
            var response = await Configuration.DashboardApiService.CreateDashboard(dashboard);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var dashboardId = response.GetContentAs<DashboardDto>().Id;
            response = await Configuration.DashboardApiService.GetDashboardById(dashboardId);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdDashboard = response.GetContentAs<DashboardDto>();
            createdDashboard.Name.Should().Be(dashboard.Name);
            createdDashboard.Description.Should().Be(dashboard.Description);
        }

        [Test]
        public async Task NegativeDashboardCreation()
        {
            var dashboardToCreate = new DashboardDto { Description = "DB without name" };
            var response = await Configuration.DashboardApiService.CreateDashboard(dashboardToCreate);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var error = response.GetContentAs<ResponseException>();
            error.ErrorCode.Should().Be(4001);
            error.Message.Should().Be(Messages.IncorrectRequestFieldNull("name"));
        }

        [Test]
        public async Task NegativeVerifyCreationAlreadyExistedDashboard()
        {
            var dashboardToCreate = DashboardProvider.GetDashboard();
            var response = await Configuration.DashboardApiService.CreateDashboard(dashboardToCreate);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            response = await Configuration.DashboardApiService.CreateDashboard(dashboardToCreate);
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);

            var error = response.GetContentAs<ResponseException>();
            error.ErrorCode.Should().Be(4091);
            error.Message.Should().Be(Messages.ResourceAlreadyExists(dashboardToCreate.Name));
        }

        [Test]
        public async Task NegativeVerifyGettingNonCreatedDashboard()
        {
            var dashboardId = 999; 
            var response = await Configuration.DashboardApiService.GetDashboardById(dashboardId);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var error = response.GetContentAs<ResponseException>();
            error.ErrorCode.Should().Be(40422);
            error.Message.Should().Be(Messages.DashboardWithIdNotFoundOnProject(dashboardId, Configuration.ProjectName));
        }

        [Test]
        [NonParallelizable]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task VerifyDashboardGetting(int dashboardAmount)
        {
            await Configuration.DashboardApiService.DeleteAllCreatedDashboards();
            for (int i = 0; i < dashboardAmount; i++)
            {
                var responseCreation = await Configuration.DashboardApiService.CreateDashboard(DashboardProvider.GetDashboard());
                responseCreation.StatusCode.Should().Be(HttpStatusCode.Created);
            }
            var response = await Configuration.DashboardApiService.GetAllDashboards();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var dashboards = response.GetContentAs<DashboardResponceDto>().Dashboards;
            dashboards.Count().Should().Be(dashboardAmount);
        }

        [Test]
        public async Task VerifyDashboardEditing()
        {
            var response = await Configuration.DashboardApiService.CreateDashboard(DashboardProvider.GetDashboard());
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var dashboardId = response.GetContentAs<DashboardDto>().Id;

            var dashboardToUpdate = DashboardProvider.GetDashboard();
            response = await Configuration.DashboardApiService.UpdateDashboardById(dashboardId, dashboardToUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response = await Configuration.DashboardApiService.GetDashboardById( dashboardId);
            var updatedDashboard = response.GetContentAs<DashboardDto>();

            updatedDashboard.Name.Should().Be(dashboardToUpdate.Name);
            updatedDashboard.Description.Should().Be(dashboardToUpdate.Description);
        }

        [Test]
        public async Task NegativeVerifyEditingNonCreatedDB()
        {
            var dashboardToUpdateId = 999;
            var dashboardToUpdate = DashboardProvider.GetDashboard();
            var response = await Configuration.DashboardApiService.UpdateDashboardById(dashboardToUpdateId, dashboardToUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var error = response.GetContentAs<ResponseException>();
            error.ErrorCode.Should().Be(40422);
            error.Message.Should().Be(Messages.DashboardWithIdNotFoundOnProject(dashboardToUpdateId, Configuration.ProjectName));
        }

        [Test]
        public async Task NegativeVerifyEditingWithHugeFieldSize()
        {
            var response = await Configuration.DashboardApiService.CreateDashboard(DashboardProvider.GetDashboard());
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var dashboardId = response.GetContentAs<DashboardDto>().Id;

            var dashboardToUpdate = new DashboardDto { Name = StringHelper.RandomString(1000)};
            response = await Configuration.DashboardApiService.UpdateDashboardById(dashboardId, dashboardToUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var error = response.GetContentAs<ResponseException>();
            error.ErrorCode.Should().Be(4001);
            error.Message.Should().Be(Messages.IncorrectRequestFieldSize("name"));
        }


        [Test]
        public async Task VerifyDeletingDashboard()
        {
            var response = await Configuration.DashboardApiService.CreateDashboard(DashboardProvider.GetDashboard());
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var dashboardId = response.GetContentAs<DashboardDto>().Id;

            response = await Configuration.DashboardApiService.DeleteDashboardById(dashboardId);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task NegativeVerifyDeletingNonExistedDashboard()
        {
            var dashboardId = 999;
            var response = await Configuration.DashboardApiService.DeleteDashboardById(dashboardId);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var error = response.GetContentAs<ResponseException>();
            error.ErrorCode.Should().Be(40422);
            error.Message.Should().Be(Messages.DashboardWithIdNotFoundOnProject(dashboardId, Configuration.ProjectName));
        }
    }
}
