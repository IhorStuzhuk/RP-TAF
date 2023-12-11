using OpenQA.Selenium;
using RP.Business.Web.WebDriver;

namespace RP.Business.Web.Pages.Elements
{
    public class SideBar : WebElement
    {
        private WebElement DashboardsButton => Find(By.XPath("//a[contains(@href, 'dashboard')]"), "DashboardsButton");

        public DashboardsPage GoToDashboards()
        {
            DashboardsButton.Click();
            return new DashboardsPage(Driver);
        }

        public SideBar(Driver driver, By locator, string name) : base(driver, locator, name)
        {
        }
    }
}
