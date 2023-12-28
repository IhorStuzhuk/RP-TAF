namespace RP.Business.Config
{
    public class WebBrowserConfig
    {
        public string BrowserName { get; set; }

        public bool IsRemote { get; set; }

        public int ImplicitWaitTimeOutSec { get; set; }

        public bool MakeScreenshot { get; set; }

        public string GridHubUrl { get; set; }
    }
}
