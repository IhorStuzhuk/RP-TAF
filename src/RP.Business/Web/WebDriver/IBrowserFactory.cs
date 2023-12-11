using OpenQA.Selenium;
using RP.Business.Config;

namespace RP.Business.Web.WebDriver
{
    public interface IBrowserFactory
    {
        public IWebDriver CreateBrowser(WebBrowserConfig config);
    }
}