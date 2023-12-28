using OpenQA.Selenium;

namespace RP.Business.Web.Pages.Elements
{
    public class SelectWidgetTypeForm : WebElement
    {
        private List<WebElement> TypeOptions => FindElements(By.XPath("//div[contains(@class, 'widget-type-item-name')]")).ToList();

        public SelectWidgetTypeForm(WebElement element) : base(element)
        {
        }

        public void SelectType(int index = 0)
        {
            TypeOptions[index].Click();
        }
    }
}
