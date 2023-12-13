using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class Widget : WebElement
    {
        public WebElement NameLabel => Find(By.XPath(".//*[contains(@class, 'widget-name-block')]"), "Widget name");

        private WebElement CrossIcon => Find(By.XPath(".//*[contains(@class,'widgetHeader__control-')][3]"), "Cross Icon");

        public Widget(WebElement element) : base(element)
        {
        }

        public string GetName => NameLabel.Text; 

        public void DeleteWidget() => CrossIcon.Click();
    }
}
