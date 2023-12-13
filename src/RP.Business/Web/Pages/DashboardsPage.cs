using OpenQA.Selenium;
using RP.Business.Web.Models;
using RP.Business.Web.Pages.Elements;
using RP.Business.Web.WebDriver;

namespace RP.Business.Web.Pages
{
    public class DashboardsPage : BasePage
    {
        private WebElement AddDashboardButton => Find(By.XPath("//*[contains(@class, 'addDashboardButton')]/button"), "AddDashboardButton");

        private AddDashboardPopup AddDashboardPopup => new AddDashboardPopup(WaitAndFind(PopupLocator, "AddDashboardPopup"));

        public DashboardTable DashboardTable => new DashboardTable(WaitAndFind(By.XPath("//div[contains(@class, 'dashboard-table')]"), "DashboardTable"));

        public EmptyDashboardsContainer EmptyDashboardsContainer => new EmptyDashboardsContainer(WaitAndFind(By.XPath("//p[contains(@class, 'emptyDashboards')]/.."), "EmptyDashboardsContainer"));

        private DeletePopup DeleteDashboardPopup => new DeletePopup(WaitAndFind(PopupLocator, "DeleteDashboardPopup"));

        private EditDashboardPopup EditDashboardPopup => new EditDashboardPopup(WaitAndFind(PopupLocator, "EditDashboardPopup"));

        public DashboardDetailedContainer DashboardDetailedContainer => new DashboardDetailedContainer(WaitAndFind(By.XPath("//div[contains(@class, 'page-layout')]"), "DashboardDetailedContainer"));

        private DeletePopup DeleteWidgetPopup => new DeletePopup(WaitAndFind(PopupLocator, "DeleteWidgetPopup"));

        public DashboardsPage(Driver driver) : base(driver)
        {
        }

        public void AddDashboard(DashboardModel dashboard)
        {
            AddDashboardButton.Click();

            AddDashboardPopup.EnterName(dashboard.Name);
            AddDashboardPopup.EnterDescription(dashboard.Description);
            AddDashboardPopup.Confirm();
        }

        public List<DashboardModel> GetDashboards()
        {
            return DashboardTable.Rows.Select(r => r.ToModel()).ToList();
        }

        public List<string> GetWidgetsNames()
        {
            return DashboardDetailedContainer.Widgets.Select(w => w.GetName).ToList();
        }

        public void DeleteDashboard(int index = 0)
        {
            DashboardTable.Rows[index].Delete();
            DeleteDashboardPopup.Confirm();
        }

        public void EditDashboard(string name, DashboardModel updatedDashboard)
        {
            DashboardTable.Rows.First(r => r.GetName == name).Edit();
            EditDashboardPopup.EnterName(updatedDashboard.Name);
            EditDashboardPopup.EnterDescription(updatedDashboard.Description);
            EditDashboardPopup.Confirm();
        }

        public void AddWidget(string name, string description, int dashboardIndex = 0, bool isWidgetFirst = true)
        {
            if(isWidgetFirst)
                DashboardTable.Rows[dashboardIndex].Open();
            DashboardDetailedContainer.AddWidget(name, description);
        }

        public void RemoveWidget(int widgetIndex = 0)
        {
            DashboardDetailedContainer.RemoveWidget(widgetIndex);
            DeleteWidgetPopup.Confirm();
        }

        public void ChangeWidgetsOrder(int draggedWidgetIndex, int droppedWidgetIndex)
        {
            var widgets = DashboardDetailedContainer.Widgets;
            var draggedWidget = widgets[--draggedWidgetIndex].NameLabel;
            var droppedWidget = widgets[--droppedWidgetIndex].NameLabel;
            draggedWidget.DragAndDrop(droppedWidget);
            Driver.Refresh();
        }
    }
}
