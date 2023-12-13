using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class DeletePopup : BasePopup
    {
        protected override WebElement ConfirmButton => Find(By.XPath("//button[text()='Delete']"), "Delete button");

        public DeletePopup(WebElement element) : base(element)
        {
        }
    }
}
