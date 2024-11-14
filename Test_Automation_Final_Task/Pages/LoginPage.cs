using OpenQA.Selenium;
using log4net;

namespace Test_Automation_Final_Task.Pages
{
    internal class LoginPage
    {
        private static readonly ILog log = Logger.Log;
        private static string Url { get; } = "https://www.saucedemo.com/";
        readonly IWebDriver driver;

        public LoginPage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentException(nameof(driver));

        public LoginPage Open()
        {
            log.Info("Opening Login Page.");
            driver.Url = Url;
            return this;
        }

        public void Login(string? username = null, string? password = null)
        {
            // Username
            var usernameField = driver.FindElement(By.Id("user-name"));
            usernameField.Clear();
            if (!string.IsNullOrEmpty(username))
            {
                log.Debug($"Entering username: {username}");
                usernameField.SendKeys(username);
            }

            // Password
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.Clear();
            if (!string.IsNullOrEmpty(password))
            {
                log.Debug($"Entering password: {password}");
                passwordField.SendKeys(password);
            }

            // Click Login button
            var loginButton = driver.FindElement(By.Id("login-button"));
            log.Info("Clicking Login button.");
            loginButton.Click();
        }
    }
}
