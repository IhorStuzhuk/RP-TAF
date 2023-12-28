global using WebElement = RP.Business.Web.Pages.Elements.WebElement;
using OpenQA.Selenium;
using RP.Business.Web.WebDriver;
using RP.Business.Web.Pages.Elements;
using SeleniumExtras.WaitHelpers;

namespace RP.Business.Web.Pages
{
    public class BasePage
    {
        protected Driver Driver { get; set; }

        private By Notification => By.XPath("//div[contains(@class, 'notificationItem')]/p");

        protected By PopupLocator => By.XPath("//*[contains(@class, 'window-animation-enter-done')]");

        public SideBar SideBar => new(Find(By.XPath("//*[contains(@class, 'layout__sidebar-container')]"), "SideBar"));

        public bool IsNotificationText(string text)
        {
            return Driver.Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(Notification, text));
        }

        public bool WaitTillNotificationBeHidden()
        {
            return Driver.Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(Notification));
        }

        public BasePage(Driver driver)
        {
            Driver = driver;
            Driver.WaitForPageLoad();
        }

        protected WebElement Find(By locator, string name)
        {
            return new WebElement(Driver, Driver._driver.FindElement(locator), locator, name);
        }

        protected WebElement WaitAndFind(By locator, string name)
        {
            var element = Driver.Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return new WebElement(Driver, element, locator, name);
        }
    }
}