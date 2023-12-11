using OpenQA.Selenium;
using RP.Business.Web.Models;

namespace RP.Business.Web.Pages.Elements
{
    public class DashboardRow : WebElement
    {
        public WebElement Name => Find(By.XPath("//a[contains(@class, 'dashboardTable')]"), "Dashboard Name");

        public WebElement Description => Find(By.XPath("//div[contains(@class, 'description')]"), "Dashboard Description");

        public WebElement Owner => Find(By.XPath("//div[contains(@class, 'owner')]"), "Dashboard Owner");

        public WebElement EditIcon => Find(By.XPath("//i[contains(@class, 'icon-pencil')]"), "Edit Icon");

        public WebElement DeleteIcon => Find(By.XPath("//i[contains(@class, 'icon-delete')]"), "Delete Icon");

        public void Delete() => DeleteIcon.Click();

        public void Edit() => EditIcon.Click();

        public DashboardModel ToModel() =>
            new DashboardModel { Name = Name.Text, Description = Description.Text, Owner = Owner.Text };

        public DashboardRow(WebElement element) : base(element)
        {
        }
    }
}
