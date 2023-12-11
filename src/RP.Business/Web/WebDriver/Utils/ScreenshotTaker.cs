using OpenQA.Selenium;
using RP.Core.Logger;

namespace RP.Business.Web.WebDriver.Utils
{
    public class ScreenshotTaker
    {
        private Driver Driver { get; set; }

        private DirectoryInfo ScreenshotsFolder = new DirectoryInfo(Path.Combine(Directory.GetParent(AppContext.BaseDirectory)?.FullName, "screenshots"));

        public ScreenshotTaker(Driver driver)
        {
            Driver = driver;
            if(!ScreenshotsFolder.Exists)
                ScreenshotsFolder.Create();
        }

        public void TakeScreenshot(string fileName)
        {
            var filePath = Path.Combine(ScreenshotsFolder.FullName, fileName);
            ((ITakesScreenshot)Driver._driver).GetScreenshot().SaveAsFile(filePath);
            Logger.Log.Info($"Screenshot added: {fileName}");
        }
    }
}
