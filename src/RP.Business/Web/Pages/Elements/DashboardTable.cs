using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class DashboardTable : WebElement
    {
        public List<DashboardRow> Rows => FindElements(By.XPath("//div[contains(@class, 'Row') and not(contains(@class, 'wrapper'))]"))
            .Select(elem => new DashboardRow(elem)).ToList();

        public DashboardTable(WebElement element) : base(element)
        {
        }
    }
}
