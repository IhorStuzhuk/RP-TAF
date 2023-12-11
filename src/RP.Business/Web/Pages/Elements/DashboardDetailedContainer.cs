using OpenQA.Selenium;
using RP.Business.Web.WebDriver;

namespace RP.Business.Web.Pages.Elements
{
    public class DashboardDetailedContainer : WebElement
    {
        private WebElement AddWidgetButton => Find(By.XPath("//*[contains(@class, 'emptyWidgetGrid')]/button"), "AddWidgetButton");

        private AddNewWidgetPopup AddNewWidgetPopup => new(WaitAndFind(By.XPath("//*[contains(@class, 'widgetWizardContent')]"), "AddNewWidgetPopup"));

        public List<Widget> Widgets => FindElements(By.XPath("//*[contains(@class, 'widget-container')]")).Select(e => new Widget(e)).ToList();

        public DashboardDetailedContainer(Driver driver, By locator, string name) : base(driver, locator, name)
        {
        }

        public void AddWidget(string name, string description)
        {
            AddWidgetButton.Click();

            AddNewWidgetPopup.SelectWidgetTypeForm.SelectType();
            AddNewWidgetPopup.NextStep();

            AddNewWidgetPopup.ChooseFilterForm.ChooseFilter();
            AddNewWidgetPopup.NextStep();

            AddNewWidgetPopup.WidgetDataForm.EnterWidgetData(name, description);
            AddNewWidgetPopup.Add();
        }
    }
}
