using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class BasePopup : WebElement
    {
        protected virtual WebElement ConfirmButton => Find(By.XPath("//button[text()='Confirm']"), "Confirm button");

        protected virtual WebElement CancelButton => Find(By.XPath("//button[text()='Cancel']"), "Cancel button");

        public BasePopup(WebElement element) : base(element)
        {
        }

        public void Confirm() => ConfirmButton.Click();

        public void Cancel() => CancelButton.Click();
    }
}
