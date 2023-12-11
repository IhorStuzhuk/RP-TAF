using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using RP.Business.Config;

namespace RP.Business.Web.WebDriver
{
    public class EdgeBrowserFactory : IBrowserFactory
    {
        public IWebDriver CreateBrowser(WebBrowserConfig config)
        {
            var options = new EdgeOptions();
            options.SetBrowserStartupConfig();
            options.AddArgument("--inprivate");

            IWebDriver driver;
            if(config.IsRemote)
                driver = new RemoteWebDriver(new Uri(config.GridHubUrl), options.ToCapabilities());
            else
                driver = new EdgeDriver(options);

            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}