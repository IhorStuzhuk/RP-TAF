using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class WidgetDataForm : WebElement
    {
        private InputField WidgetNameField => new(Find(By.XPath("//input"), "WidgetNameField"));

        private InputField WidgetDescriptionField => new(Find(By.XPath("//textarea"), "WidgetDescriptionField"));

        public WidgetDataForm(WebElement element) : base(element)
        {
        }

        public void EnterWidgetData(string name, string description)
        {
            WidgetNameField.Enter(name);
            WidgetDescriptionField.Enter(description);
        }
    }
}
