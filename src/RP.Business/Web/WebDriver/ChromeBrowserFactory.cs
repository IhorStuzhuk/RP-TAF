using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using RP.Business.Config;

namespace RP.Business.Web.WebDriver
{
    public class ChromeBrowserFactory : IBrowserFactory
    {
        public IWebDriver CreateBrowser(WebBrowserConfig config)
        {
            var options = new ChromeOptions();
            options.SetBrowserStartupConfig();
            options.AddArguments("--incognito");

            IWebDriver driver;
            if(config.IsRemote)
                driver = new RemoteWebDriver(new Uri(config.GridHubUrl), options.ToCapabilities());
            else
                driver = new ChromeDriver(options);

            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}