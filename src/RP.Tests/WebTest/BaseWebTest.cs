using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RP.Business.Web.Pages;
using RP.Business.Web.WebDriver;
using RP.Business.Web.WebDriver.Utils;
using RP.Core.Helpers;
using RP.Tests.nUnit;

namespace RP.Tests.WebTest
{
    public class BaseWebTest : BaseNUnitTest
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
            var loginPage = new LoginPage(Driver);
            loginPage.Login(Configuration.UserConfig);
            loginPage.IsNotificationText(Messages.SignedInSuccessfully).Should().BeTrue();
        }

        [TearDown]
        public void WebTestTearDown()
        {
            if(TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                ScreenshotTaker?.TakeScreenshot(TestContext.CurrentContext.Test.Name + StringHelper.RandomString(10) + ".png");
            }
            Driver.Close();
        }
    }
}