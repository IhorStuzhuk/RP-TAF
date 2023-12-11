using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class ChooseFilterForm : WebElement
    {
        public List<WebElement> Filters => FindElements(By.XPath("//div[contains(@class, 'filtersItem')]")).ToList();

        public ChooseFilterForm(WebElement element) : base(element)
        {
        }

        public void ChooseFilter(int index = 0)
        {
            Filters[index].Click();
        }
    }
}
