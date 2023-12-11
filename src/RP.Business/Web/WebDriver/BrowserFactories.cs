namespace RP.Business.Web.WebDriver
{
    public static class BrowserFactories
    {
        public static IBrowserFactory GetFactory(Browser browser)
        {
            return browser switch
            {
                Browser.Chrome => new ChromeBrowserFactory(),
                Browser.Edge => new EdgeBrowserFactory(),
                _ => throw new NotSupportedException($"Browser {browser} is not supported!"),
            };
        }
    }
}