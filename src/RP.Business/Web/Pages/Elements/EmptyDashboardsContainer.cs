using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class EmptyDashboardsContainer : WebElement
    {
        private WebElement YouHaveNoDashboardsLabel => Find(By.XPath("//p[contains(@class, 'emptyDashboards')]"), "YouHaveNoDashboardsLabel");

        public string YouHaveNoDashboardsLabelText => YouHaveNoDashboardsLabel.Text;

        public EmptyDashboardsContainer(WebElement element) : base(element)
        {
        }
    }
}
