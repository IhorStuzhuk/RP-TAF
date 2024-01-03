using FluentAssertions;
using NUnit.Framework;
using RP.Business;
using RP.Business.Web.Models;
using RP.Business.Web.Pages;
using RP.Core.Helpers;
using RP.Tests.Services.Jira;
using RP.Tests.TestDataProviders;

namespace RP.Tests.WebTest
{
    [NonParallelizable]
    public class DashboardWebTests : BaseWebTest
    {
        private DashboardsPage _dashboardsPage;

        [SetUp]
        public void SetUp()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Login(Configuration.UserConfig);
            loginPage.IsNotificationText(Messages.SignedInSuccessfully).Should().BeTrue();

            _dashboardsPage = new DashboardsPage(Driver);
        }

        [Test]
        [TestCaseId("RPTEST-1")]
        public void CreateDashboard()
        {
            var dashboard = new DashboardModel()
            {
                Name = StringHelper.RandomString(5),
                Description = StringHelper.RandomString(5),
                Owner = Configuration.UserConfig.User
            };

            _dashboardsPage.AddDashboard(dashboard);
            _dashboardsPage.IsNotificationText(Messages.DashboardHasBeenAdded).Should().BeTrue();
            _dashboardsPage.SideBar.GoToDashboards().GetDashboards().Should().Contain(dashboard);
        }

        [Test]
        [TestCaseId("RPTEST-2")]
        public async Task RemoveDashboard()
        {
            await Configuration.DashboardApiService.CreateDashboard(DashboardProvider.GetDashboard());
            Driver.Refresh();

            _dashboardsPage.DeleteDashboard();
            _dashboardsPage.IsNotificationText(Messages.DashboardHasBeenDeleted).Should().BeTrue();
            _dashboardsPage.EmptyDashboardsContainer.YouHaveNoDashboardsLabelText.Should().Be(Messages.YouHaveNoDashboards);
        }

        [Test]
        [TestCaseId("RPTEST-3")]
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

            _dashboardsPage.EditDashboard(addedDashboard.Name, updatedDashboard);
            _dashboardsPage.GetDashboards().Should().Contain(updatedDashboard);
        }

        [Test]
        [TestCaseId("RPTEST-4")]
        public async Task AddWidget()
        {
            await Configuration.DashboardApiService.CreateDashboard(DashboardProvider.GetDashboard());
            Driver.Refresh();

            var widgetName = "newWidget";
            var widgetDescription = "widgetDescription";

            AddWidget(1, widgetName, widgetDescription);
            _dashboardsPage.GetWidgetsNames().Contains("newWidget").Should().BeTrue();
        }

        [Test]
        [TestCaseId("RPTEST-5")]
        public async Task RemoveWidget()
        {
            await Configuration.DashboardApiService.CreateDashboard(DashboardProvider.GetDashboard());
            Driver.Refresh();

            AddWidget(1);
            _dashboardsPage.RemoveWidget();
            _dashboardsPage.IsNotificationText(Messages.WidgetHasBeenDeleted).Should().BeTrue();
            _dashboardsPage.DashboardDetailedContainer.EmptyWidgetContainer.ThereAreNoWidgetsOnDashboardLabelText.Should().Be(Messages.ThereAreNoWidgetsOnDashboard);
        }

        [Test]
        [TestCaseId("RPTEST-6")]
        public async Task ChangeWidgetsOrder()
        {
            await Configuration.DashboardApiService.CreateDashboard(DashboardProvider.GetDashboard());
            Driver.Refresh();

            AddWidget(2);
            _dashboardsPage.WaitTillNotificationBeHidden();
            var widgetNames = _dashboardsPage.GetWidgetsNames();
            _dashboardsPage.ChangeWidgetsOrder(1, 2);
            _dashboardsPage.GetWidgetsNames().First().Should().Be(widgetNames.Last());
        }

        private void AddWidget(int amount, string? widgetName = null, string? widgetDescription = null, int dashboard = 0)
        {
            for(int i = 1; i <= amount; i++)
                _dashboardsPage.AddWidget(widgetName ?? StringHelper.RandomString(5), widgetDescription ?? StringHelper.RandomString(5), dashboard, i == 1);
            _dashboardsPage.IsNotificationText(Messages.WidgetHasBeenAdded).Should().BeTrue();
        }
    }
}
