using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RP.Business.Web.WebDriver;
using RP.Core.Logger;
using SeleniumExtras.WaitHelpers;

namespace RP.Business.Web.Pages.Elements
{
    public class WebElement
    {
        public Driver Driver { get; private set; }

        public IWebElement Element { get; private set; }

        protected string Name { get; set; }

        public By Locator { get; private set; }

        public WebElement(Driver driver, By locator, string name)
        {
            Driver = driver;
            Name = name;
            try
            {
                Element = Driver._driver.FindElement(locator);
            }
            catch(NullReferenceException ex)
            {
                Logger.Log.Error($"The '{name}' was not found by locator {locator}");
                throw ex;
            }
        }

        public WebElement(WebElement element)
        {
            Driver = element.Driver;
            Element = element.Element;
            Name = element.Name;
            Locator = element.Locator;
        }

        public WebElement(Driver driver, IWebElement element, By locator, string name = "")
        {
            Driver = driver;
            Element = element;
            Locator = locator;
            Name = name;
        }

        protected WebElement Find(By locator, string name)
        {
            return new WebElement(Driver, Element.FindElement(locator), locator, name);
        }

        protected IReadOnlyCollection<WebElement> FindElements(By locator)
        {
            var collection = new List<WebElement>();
            foreach(var item in Element.FindElements(locator))
            {
                var elem = Driver.Wait.Until(ExpectedConditions.ElementToBeClickable(item));
                collection.Add(new WebElement(Driver, elem, locator));
            }
            return collection;
        }

        protected WebElement WaitAndFind(By locator, string name)
        {
            var element = Driver.Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            return new WebElement(Driver, element, locator, name);
        }

        public WebElement(By locator, string name)
        {
            Name = name;
            try
            {
                Element = Element.FindElement(locator);
            }
            catch(NullReferenceException ex)
            {
                Logger.Log.Error($"The '{name}' was not found by locator {locator}");
                throw ex;
            }
        }

        public string Text => Element.Text;

        public void Click()
        {
            Driver.Wait.Until(ExpectedConditions.ElementToBeClickable(Element)).Click();
            Logger.Log.Info($"Clicked on '{Name}'");
            Driver.WaitForPageLoad();
        }

        public void SendKeys(string text)
        {
            Element.Click();
            Element.SendKeys(text);
            Logger.Log.Info($"Typing '{text}' into '{Name}'");
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

        public void DragAndDrop(IWebElement dropTo)
        {
            new Actions(Driver._driver).DragAndDrop(Element, dropTo).Perform();
        }

        public void DragAndDrop(int xOffset, int yOffset)
        {
            new Actions(Driver._driver).DragAndDropToOffset(Element, xOffset, yOffset).Perform();
        }
        #endregion
    }
}
