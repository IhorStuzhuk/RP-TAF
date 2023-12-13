using OpenQA.Selenium;
using RP.Business.Web.Models;

namespace RP.Business.Web.Pages.Elements
{
    public class DashboardRow : WebElement
    {
        private WebElement Name => Find(By.XPath("//a[contains(@class, 'dashboardTable')]"), "Dashboard Name");

        private WebElement Description => Find(By.XPath("//div[contains(@class, 'description')]"), "Dashboard Description");

        private WebElement Owner => Find(By.XPath("//div[contains(@class, 'owner')]"), "Dashboard Owner");

        private WebElement EditIcon => Find(By.XPath("//i[contains(@class, 'icon-pencil')]"), "Edit Icon");

        private WebElement DeleteIcon => Find(By.XPath("//i[contains(@class, 'icon-delete')]"), "Delete Icon");

        public string GetName => Name.Text;

        public void Open() => Name.Click();

        public void Delete() => DeleteIcon.Click();

        public void Edit() => EditIcon.Click();


        public DashboardModel ToModel() =>
            new DashboardModel { Name = Name.Text, Description = Description.Text, Owner = Owner.Text };

        public DashboardRow(WebElement element) : base(element)
        {
        }
    }
}
