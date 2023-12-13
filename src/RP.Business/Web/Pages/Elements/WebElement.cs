using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RP.Business.Web.WebDriver;
using RP.Core.Logger;
using SeleniumExtras.WaitHelpers;

namespace RP.Business.Web.Pages.Elements
{
    public class WebElement
    {
        protected Driver Driver { get; private set; }

        protected IWebElement Element { get; private set; }

        protected By Locator { get; private set; }

        private string Name { get; set; }

        public string Text => Element.Text;

        public WebElement(Driver driver, IWebElement element, By locator, string name = "")
        {
            Driver = driver;
            Element = element;
            Locator = locator;
            Name = name;
        }

        public WebElement(WebElement element) : this(element.Driver, element.Element, element.Locator, element.Name)
        {
        }

        protected WebElement Find(By locator, string name) => new WebElement(Driver, Element.FindElement(locator), locator, name);

        protected IEnumerable<WebElement> FindElements(By locator)
        {
            Driver.Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            return Element.FindElements(locator).Select(e => new WebElement(Driver, e, locator));
        }

        protected WebElement WaitAndFind(By locator, string name)
        {
            var element = Driver.Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            return new WebElement(Driver, element, locator, name);
        }

        public void Click()
        {
            Driver.Wait.Until(ExpectedConditions.ElementToBeClickable(Element)).Click();
            Logger.Log.Info($"Clicked on '{Name}'");
            Driver.WaitForPageLoad();
        }

        #region JSExecutor
        public void ClickByJS()
        {
            ((IJavaScriptExecutor)Driver._driver).ExecuteScript("arguments[0].click();", Element);
        }

        public WebElement ScrollToElementByJS()
        {
            ((IJavaScriptExecutor)Driver._driver).ExecuteScript("arguments[0].scrollIntoView(true);", Element);
            return this;
        }

        public bool IsElementScrolledIntoView()
        {
            return (bool)((IJavaScriptExecutor)Driver._driver).ExecuteScript(
                "var elemTop = arguments[0].getBoundingClientRect().top; " +
                "var elemBottom = arguments[0].getBoundingClientRect().bottom; " +
                "var isVisible = (elemTop >= 0) && (elemBottom <= window.innerHeight); " +
                "return isVisible;", Element);
        }
        #endregion

        #region Actions
        public void ResizeElement(int xOffset, int yOffset)
        {
            new Actions(Driver._driver).MoveToElement(Element).ClickAndHold().MoveByOffset(xOffset, yOffset).Release().Perform();
        }

        public void DragAndDrop(WebElement dropTo)
        {
            new Actions(Driver._driver).MoveToElement(Element).ClickAndHold().MoveToElement(dropTo.Element).Release().Perform();
        }

        public WebElement Hover()
        {
            new Actions(Driver._driver).MoveToElement(Element).Build().Perform();
            return this;
        }
        #endregion
    }
}
