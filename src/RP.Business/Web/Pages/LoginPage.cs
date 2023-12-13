using OpenQA.Selenium;
using RP.Business.Models;
using RP.Business.Web.Pages.Elements;
using RP.Business.Web.WebDriver;

namespace RP.Business.Web.Pages
{
    public class LoginPage : BasePage
    {
        private InputField LoginField => new(Find(By.XPath("//div[contains(@class, 'login-field')]//input"), "Login field"));

        private InputField PasswordField => new(Find(By.XPath("//div[contains(@class, 'password-field')]//input"), "Password field"));

        private WebElement LoginButton => Find(By.XPath("//button[@type = 'submit']"), "Login button");

        public LoginPage(Driver driver) : base(driver)
        {
        }

        public void Login(UserConfig user)
        {
            LoginField.Enter(user.User);
            PasswordField.Enter(user.Password);
            LoginButton.Click();
        }
    }
}
