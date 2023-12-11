using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RP.Business.Web.Models;
using RP.Business.Web.Pages.Elements;
using RP.Business.Web.WebDriver;
using System;
using System.Reflection;

namespace RP.Business.Web.Pages
{
    public class DashboardsPage : BasePage
    {
        private WebElement AddDashboardButton => new WebElement(Driver, By.XPath("//*[contains(@class, 'addDashboardButton')]/button"), "AddDashboardButton");

        private AddDashboardPopup AddDashboardPopup => new AddDashboardPopup(WaitAndFind(By.XPath("//*[contains(@class, 'window-animation-enter-done')]"), "AddDashboardPopup"));

        public DashboardTable DashboardTable => new DashboardTable(WaitAndFind(By.XPath("//div[contains(@class, 'dashboard-table')]"), "DashboardTable"));

        public EmptyDashboardsContainer EmptyDashboardsContainer => new EmptyDashboardsContainer(WaitAndFind(By.XPath("//p[contains(@class, 'emptyDashboards')]/.."), "EmptyDashboardsContainer"));

        private DeleteDashboardPopup DeleteDashboardPopup => new DeleteDashboardPopup(WaitAndFind(By.XPath("//*[contains(@class, 'window-animation-enter-done')]"), "DeleteDashboardPopup"));
        
        private EditDashboardPopup EditDashboardPopup => new EditDashboardPopup(WaitAndFind(By.XPath("//*[contains(@class, 'window-animation-enter-done')]"), "EditDashboardPopup"));

        private DashboardDetailedContainer DashboardDetailedContainer => new DashboardDetailedContainer(Driver, By.XPath("//div[contains(@class, 'page-layout')]"), "DashboardDetailedContainer");
        
        public DashboardsPage(Driver driver) : base(driver)
        {
        }

        public void AddDashboard(DashboardModel dashboard)
        {
            AddDashboardButton.Click();

            AddDashboardPopup.EnterName(dashboard.Name);
            AddDashboardPopup.EnterDescription(dashboard.Description);
            AddDashboardPopup.Add();
        }

        public List<DashboardModel> GetDashboards()
        {
            return DashboardTable.Rows.Select(r => r.ToModel()).ToList();
        }

        public List<Widget> GetWidgets()
        {
            return DashboardDetailedContainer.Widgets;
        }

        public void DeleteDashboard(int index = 0)
        {
            DashboardTable.Rows[index].Delete();
            DeleteDashboardPopup.Delete();
        }

        public void EditDashboard(string name, DashboardModel updatedDashboard)
        {
            DashboardTable.Rows.First(r => r.Name.Text == name).Edit();
            EditDashboardPopup.EnterName(updatedDashboard.Name);
            EditDashboardPopup.EnterDescription(updatedDashboard.Description);
            EditDashboardPopup.Update();
        }

        public void AddWidget(string name, string description, int dashboardIndex = 0)
        {
            DashboardTable.Rows[dashboardIndex].Name.Click();
            DashboardDetailedContainer.AddWidget(name, description);
        }
    }
}
