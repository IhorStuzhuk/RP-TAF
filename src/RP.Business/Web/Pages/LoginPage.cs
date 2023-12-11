using OpenQA.Selenium;
using RP.Business.Models;
using RP.Business.Web.WebDriver;
using SeleniumExtras.WaitHelpers;

namespace RP.Business.Web.Pages
{
    public class LoginPage : BasePage
    {
        private WebElement LoginField => new WebElement(Driver, By.XPath("//div[contains(@class, 'login-field')]//input"), "Login field");

        private WebElement PasswordField => new WebElement(Driver, By.XPath("//div[contains(@class, 'password-field')]//input"), "Password field");

        private WebElement LoginButton => new WebElement(Driver, By.XPath("//button[@type = 'submit']"), "Login button");

        public LoginPage(Driver driver) : base(driver)
        {
        }

        public void Login(UserConfig user)
        {
            LoginField.SendKeys(user.User);
            PasswordField.SendKeys(user.Password);
            LoginButton.Click();
        }
    }
}
