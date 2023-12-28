using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using RP.Business.Web.WebDriver;
using RP.Business.Web.WebDriver.Utils;
using RP.Core.Helpers;
using RP.Tests.nUnit;
using RP.Tests.Services.Jira;
using System.Reflection;

namespace RP.Tests.WebTest
{
    [Category("UI")]
    public class BaseWebTest : BaseTest
    {
        protected Driver Driver;
        private ScreenshotTaker ScreenshotTaker;
        private string TestCaseId;

        [SetUp]
        public void WebTestSetup()
        {
            TestCaseId = Type.GetType(TestContext.CurrentContext.Test.ClassName)
                .GetMethod(TestContext.CurrentContext.Test.MethodName).GetCustomAttribute<TestCaseIdAttribute>().Id;

            Configuration.JiraService.UpdateTestCaseStatus(TestCaseId, JiraStatus.InProgress);

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
            JiraStatus testStatus = JiraStatus.Passed;
            if(TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                ScreenshotTaker?.TakeScreenshot(TestContext.CurrentContext.Test.Name + StringHelper.RandomString(10) + ".png");
                testStatus = JiraStatus.Failed;
                Configuration.JiraService.AddCommentToTestCase(TestCaseId, TestContext.CurrentContext.Result.Message);
            }
            Driver.Close();

            Configuration.JiraService.UpdateTestCaseStatus(TestCaseId, testStatus);
        }
    }
}