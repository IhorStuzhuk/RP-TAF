using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class Widget : WebElement
    {
        private WebElement Name => Find(By.XPath("//*[contains(@class, 'widget-name-block')]"), "Widget name");

        public Widget(WebElement element) : base(element)
        {
        }

        public string GetName => Name.Text; 
    }
}
