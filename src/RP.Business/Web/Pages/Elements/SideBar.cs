using OpenQA.Selenium;

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

        public SideBar(WebElement element) : base(element)
        {
        }
    }
}
