using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class EmptyWidgetContainer : WebElement
    {
        private WebElement AddWidgetButton => Find(By.XPath("//*[contains(@class, 'emptyWidgetGrid')]/button"), "AddWidgetButton");

        private WebElement ThereAreNoWidgetsOnDashboardLabel => Find(By.XPath("//p[contains(@class, 'empty-widget-headline')]"), "ThereAreNoWidgetsOnDashboardLabel");

        public string ThereAreNoWidgetsOnDashboardLabelText => ThereAreNoWidgetsOnDashboardLabel.Text;

        public void AddWidget() => AddWidgetButton.Click();

        public EmptyWidgetContainer(WebElement element) : base(element)
        {
        }
    }
}