using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class DashboardDetailedContainer : WebElement
    {
        private WebElement AddNewWidgetButton => new(Find(By.XPath("//div[contains(@class, 'buttons-block')][1]/button"), "AddNewWidgetButton"));

        private AddNewWidgetPopup AddNewWidgetPopup => new(WaitAndFind(By.XPath("//*[contains(@class, 'widgetWizardContent')]"), "AddNewWidgetPopup"));

        public List<Widget> Widgets => FindElements(By.XPath(".//*[contains(@class, 'widgetsGrid')]")).Select(e => new Widget(e)).ToList();

        public EmptyWidgetContainer EmptyWidgetContainer => new(Find(By.XPath("//div[@class = 'container']"), "EmptyWidgetContainer"));

        public DashboardDetailedContainer(WebElement element) : base(element)
        {
        }

        public void AddWidget(string name, string description)
        {
            AddNewWidgetButton.Click();

            AddNewWidgetPopup.SelectWidgetTypeForm.SelectType();
            AddNewWidgetPopup.NextStep();

            AddNewWidgetPopup.ChooseFilterForm.ChooseFilter();
            AddNewWidgetPopup.NextStep();

            AddNewWidgetPopup.WidgetDataForm.EnterWidgetData(name, description);
            AddNewWidgetPopup.Add();
        }

        public void RemoveWidget(int widgetIndex) => ((Widget)Widgets[widgetIndex].ScrollToElementByJS().Hover()).DeleteWidget();
    }
}
