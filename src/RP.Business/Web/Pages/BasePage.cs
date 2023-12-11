global using WebElement = RP.Business.Web.Pages.Elements.WebElement;
using OpenQA.Selenium;
using RP.Business.Web.WebDriver;
using RP.Core.Logger;
using RP.Business.Web.Pages.Elements;
using SeleniumExtras.WaitHelpers;

namespace RP.Business.Web.Pages
{
    public class BasePage
    {
        protected Driver Driver { get; set; }

        private By Notification => By.XPath("//div[contains(@class, 'notificationItem')]/p");

        public SideBar SideBar => new SideBar(Driver, By.XPath("//*[contains(@class, 'layout__sidebar-container')]"), "SideBar");

        public bool IsNotificationText(string text)
        {
            return Driver.Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(Notification, text));
        }

        public BasePage(Driver driver)
        {
            Driver = driver;
            Driver.WaitForPageLoad();
        }

        protected WebElement WaitAndFind(By locator, string name)
        {
            var element = Driver.Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return new WebElement(Driver, element, locator, name);
        }

        protected IEnumerable<WebElement> WaitAndFindMany(By locator)
        {
            var elements = Driver.Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
            return elements.Select(el => new WebElement(Driver, el, locator));
        }

        public bool WaitForElementToBeVisible(By locator)
        {
            try
            {
                return Driver.Wait.Until(ExpectedConditions.ElementIsVisible(locator)).Displayed
                    && Driver.Wait.Until(ExpectedConditions.ElementIsVisible(locator)).Enabled;
            }
            catch(Exception e)
            {
                Logger.Log.Error($"Error occured while waiting for element visibility: {e}");
                return false;
            }
        }
    }
}