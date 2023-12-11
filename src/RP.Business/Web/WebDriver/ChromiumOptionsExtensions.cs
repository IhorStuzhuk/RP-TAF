using OpenQA.Selenium.Chromium;

namespace RP.Business.Web.WebDriver
{
    internal static class ChromiumOptionsExtensions
    {
        internal static void SetBrowserStartupConfig(this ChromiumOptions options)
        {
            options.AddArguments("--start-maximized", "--disable-extensions");
            options.AddArgument("use-fake-device-for-media-stream");
            options.AddArgument("use-fake-ui-for-media-stream");
            options.AddArgument("--ignore-certificate-errors");
        }
    }
}
