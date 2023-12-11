using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class DeleteDashboardPopup : WebElement
    {
        private WebElement DeleteButton => Find(By.XPath("//button[text()='Delete']"), "Delete button");

        private WebElement CancelButton => Find(By.XPath("//button[text()='Cancel']"), "Cancel button");


        public void Delete() => DeleteButton.Click();

        public void Cancel() => CancelButton.Click();

        public DeleteDashboardPopup(WebElement element) : base(element)
        {
        }
    }
}
