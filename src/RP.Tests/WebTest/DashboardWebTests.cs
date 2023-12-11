using FluentAssertions;
using NUnit.Framework;
using RP.Business;
using RP.Business.Web.Models;
using RP.Business.Web.Pages;
using RP.Core.Helpers;
using RP.Tests.TestDataProviders;

namespace RP.Tests.WebTest
{
    public class DashboardWebTests : BaseWebTest
    {
        [Test]
        public void CreateDashboard()
        {
            var dashboard = new DashboardModel()
            {
                Name = StringHelper.RandomString(5),
                Description = StringHelper.RandomString(5),
                Owner = Configuration.UserConfig.User
            };

            var dashboardsPage = new DashboardsPage(Driver);
            dashboardsPage.AddDashboard(dashboard);
            dashboardsPage.IsNotificationText(Messages.DashboardHasBeenAdded).Should().BeTrue();

            dashboardsPage.SideBar.GoToDashboards().GetDashboards().Should().Contain(dashboard);
        }

        [Test]
        public async Task RemoveDashboard()
        {
            await Configuration.DashboardApiService.CreateDashboard(DashboardProvider.GetDashboard());
            Driver.Refresh();
            var dashboardsPage = new DashboardsPage(Driver);
            dashboardsPage.DeleteDashboard();
            dashboardsPage.IsNotificationText(Messages.DashboardHasBeenDeleted).Should().BeTrue();
            dashboardsPage.EmptyDashboardsContainer.YouHaveNoDashboardsLabelText.Should().Be(Messages.YouHaveNoDashboards);
        }

        [Test]
        public async Task EditDashboard()
        {
            var addedDashboard = DashboardProvider.GetDashboard();
            await Configuration.DashboardApiService.CreateDashboard(addedDashboard);
            Driver.Refresh();

            var updatedDashboard = new DashboardModel
            {
                Name = "updatedName",
                Description = "UpdatedDescription",
                Owner = Configuration.UserConfig.User
            };

            var dashboardsPage = new DashboardsPage(Driver);
            dashboardsPage.EditDashboard(addedDashboard.Name, updatedDashboard);
            dashboardsPage.GetDashboards().Should().Contain(updatedDashboard);
        }

        [Test]
        public async Task AddWidget()
        {
            var addedDashboard = DashboardProvider.GetDashboard();
            await Configuration.DashboardApiService.CreateDashboard(addedDashboard);
            Driver.Refresh();

            var widgetName = "newWidget";
            var widgetDescription = "widgetDescription";
            var dashboardsPage = new DashboardsPage(Driver);
            dashboardsPage.AddWidget(widgetName, widgetDescription);
            dashboardsPage.IsNotificationText(Messages.WidgetHasBeenAdded).Should().BeTrue();
            dashboardsPage.GetWidgets().Select(w => w.GetName).Contains("newWidget").Should().BeTrue();
        }
    }
}
