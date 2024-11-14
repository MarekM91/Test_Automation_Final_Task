using OpenQA.Selenium;

namespace Test_Automation_Final_Task.Pages
{
    internal class LoginPage
    {
        private static string Url { get; } = "https://www.saucedemo.com/";
        readonly IWebDriver driver;

        public LoginPage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentException(nameof(driver));

        public LoginPage Open()
        {
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
                usernameField.SendKeys(username);
            }

            // Password
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.Clear();
            if (!string.IsNullOrEmpty(password))
            {
                passwordField.SendKeys(password);
            }

            // Click Login button
            var loginButton = driver.FindElement(By.Id("login-button"));
            loginButton.Click();
        }
    }
}
