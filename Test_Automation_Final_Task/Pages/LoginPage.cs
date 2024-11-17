using OpenQA.Selenium;
using log4net;

namespace Test_Automation_Final_Task.Pages
{
    internal class LoginPage
    {
        private static readonly ILog log = Logger.Log;
        private readonly IWebDriver driver;
        private readonly string url;

        public LoginPage(IWebDriver driver, string url = "https://www.saucedemo.com/")
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            this.url = url ?? throw new ArgumentNullException(nameof(url));
        }

        private IWebElement UsernameField => driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordField => driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => driver.FindElement(By.Id("login-button"));

        public LoginPage Open()
        {
            log.Info("Opening Login Page.");
            driver.Url = url;
            return this;
        }

        public void ClearUsername()
        {
            log.Debug($"Username field before clearing: '{UsernameField.GetAttribute("value")}'");

            UsernameField.SendKeys(Keys.Control + "a"); 
            UsernameField.SendKeys(Keys.Backspace);

            log.Debug($"Username field after clearing: '{UsernameField.GetAttribute("value")}'");
        }

        public void ClearPassword()
        {
            log.Debug($"Password field before clearing: '{PasswordField.GetAttribute("value")}'");

            PasswordField.SendKeys(Keys.Control + "a");
            PasswordField.SendKeys(Keys.Backspace);

            log.Debug($"Password field after clearing: '{PasswordField.GetAttribute("value")}'");
        }

        public void TypeInUsername(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                log.Debug($"Entering username: {username}");
                UsernameField.SendKeys(username);
            }
        }

        public void TypeInPassword(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                log.Debug($"Entering password: {password}");
                PasswordField.SendKeys(password);
            }
        }

        public void ClickLoginButton()
        {
            log.Info("Clicking Login button.");
            LoginButton.Click();
        }

        public void Login(string? username = null, string? password = null)
        {
            log.Info("Logging in.");
            ClearUsername();
            ClearPassword();

            if (!string.IsNullOrEmpty(username))
            {
                ClearUsername();
                TypeInUsername(username);
            }

            if (!string.IsNullOrEmpty(password))
            {
                ClearPassword();
                TypeInPassword(password);
            }

            ClickLoginButton();
        }
    }
}
