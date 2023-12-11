using OpenQA.Selenium;
using RP.Business.Web.WebDriver;

namespace RP.Business.Web.Pages.Elements
{
    public class InputField : WebElement
    {
        public InputField(WebElement element) : base(element)
        {
        }

        public InputField(Driver driver, By locator, string name) : base(driver, locator, name)
        {
        }

        public void Enter(string text)
        {
            Clear();
            Element.SendKeys(text);
        }

        public void Clear()
        {
            Element.SendKeys(Keys.LeftShift + Keys.Home);
        }
    }
}
