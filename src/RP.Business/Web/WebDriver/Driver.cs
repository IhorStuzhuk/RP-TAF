using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RP.Core.Logger;
using SeleniumExtras.WaitHelpers;

namespace RP.Business.Web.WebDriver
{
    public class Driver
    {
        public IWebDriver _driver { get; private set; }

        public WebDriverWait Wait { get; private set; }

        public Driver(IWebDriver driver, int implicitWaitTimeOutSec)
        {
            _driver = driver;
            Wait = new(driver, new TimeSpan(0, 0, implicitWaitTimeOutSec));
        }

        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void WaitForPageLoad()
        {
            Wait.Until(isLoaded =>
            {
                try
                {
                    return Convert.ToBoolean(((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState === 'complete'"));
                }
                catch(WebDriverTimeoutException)
                {
                    Logger.Log.Error("Page is not loaded");
                    throw;
                }
            });

        }

        public void Refresh()
        {
            _driver.Navigate().Refresh();
            WaitForPageLoad();
        }

        public void Close()
        {
            _driver?.Quit();
            _driver = null;
        }
    }
}
