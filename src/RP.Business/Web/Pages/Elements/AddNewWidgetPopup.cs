using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class AddNewWidgetPopup : WebElement
    {
        public SelectWidgetTypeForm SelectWidgetTypeForm => new(Find(By.XPath("//form"), "SelectWidgetTypeForm"));

        public ChooseFilterForm ChooseFilterForm => new(WaitAndFind(By.XPath("//form"), "ChooseFilterForm"));

        public WidgetDataForm WidgetDataForm => new(Find(By.XPath("//form"), "SelectWidgetTypeForm"));

        private WebElement NextStepButton => Find(By.XPath("(//button)[last()]"), "NextStepButton");

        private WebElement AddButton => Find(By.XPath("//button[contains(@class, 'bigButton')]"), "AddButton");

        public AddNewWidgetPopup(WebElement element) : base(element)
        {
            ScrollToElementByJS();
        }

        public void NextStep()
        {
            NextStepButton.ScrollToElementByJS().Click();
        }

        public void Add()
        {
            AddButton.Click();
        }
    }
}
