using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class InputField : WebElement
    {
        public InputField(WebElement element) : base(element)
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
