using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class EditDashboardPopup : AddDashboardPopup
    {
        protected override WebElement ConfirmButton => Find(By.XPath("//button[text()='Update']"), "Update button");

        public EditDashboardPopup(WebElement element) : base(element)
        {
        }
    }
}
