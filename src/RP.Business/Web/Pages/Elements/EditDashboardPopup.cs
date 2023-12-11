using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace RP.Business.Web.Pages.Elements
{
    public class EditDashboardPopup : AddDashboardPopup
    {
        private WebElement UpdateButton => Find(By.XPath("//button[text()='Update']"), "Update button");

        private WebElement CancelButton => Find(By.XPath("//button[text()='Cancel']"), "Cancel button");

        public void Update()
        {
            UpdateButton.Click();
            Driver.Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(Locator));
        }

        public void Cancel() => CancelButton.Click();

        public EditDashboardPopup(WebElement element) : base(element)
        {
        }
    }
}
