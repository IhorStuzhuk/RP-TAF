using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class AddDashboardPopup : WebElement
    {
        private InputField NameField => new(Find(By.XPath("//*[contains(@class, 'modal-field-content')]//input"), "NameField"));

        private InputField DescriptionField => new(Find(By.XPath("//textarea"), "DescriptionField"));

        private WebElement AddButton => Find(By.XPath("//button[text() = 'Add']"), "AddButton");

        public AddDashboardPopup(WebElement element) : base(element)
        {
        }

        public void EnterName(string name) => NameField.Enter(name);

        public void EnterDescription(string description) => DescriptionField.Enter(description);

        public void Add() => AddButton.Click();
    }
}
