using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RP.Business.Web.WebDriver;
using RP.Business.Web.WebDriver.Utils;
using RP.Core.Helpers;

namespace RP.Tests.WebTest
{
    public class BaseWebTest
    {
        protected Driver Driver;
        private ScreenshotTaker ScreenshotTaker;

        [SetUp]
        public void WebTestSetup()
        {
            Driver = new(BrowserFactories
                .GetFactory((Browser)Enum.Parse(typeof(Browser), Configuration.WebBrowserConfig.BrowserName, ignoreCase: true))
                .CreateBrowser(Configuration.WebBrowserConfig), Configuration.WebBrowserConfig.ImplicitWaitTimeOutSec);

            if(Configuration.WebBrowserConfig.MakeScreenshot)
                ScreenshotTaker = new(Driver);

            Driver.NavigateTo(Configuration.WebConfig.UrlClient);
        }

        [TearDown]
        public async Task WebTestTearDown()
        {
            if(TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                ScreenshotTaker?.TakeScreenshot(TestContext.CurrentContext.Test.Name + StringHelper.RandomString(10) + ".png");
            }
            Driver.Close();

            await Configuration.DashboardApiService.DeleteAllCreatedDashboards();
        }
    }
}