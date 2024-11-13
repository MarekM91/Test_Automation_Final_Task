using OpenQA.Selenium;

namespace Test_Automation_Final_Task.Pages
{
    internal class LoginPageUsername
    {
        private static string Url { get; } = "https://www.saucedemo.com/";

        IWebDriver driver;

        public LoginPageUsername(IWebDriver driver) => this.driver = driver ?? throw new ArgumentException(nameof(driver));

        public LoginPageUsername Open()
        {
            driver.Url = Url;
            return this;
        }

        public void Login(string username)
        {
            // Username
            var usernameField = driver.FindElement(By.Id("user-name"));

            usernameField.Clear();
            usernameField.SendKeys(username);

            // Password
            var passwordField = driver.FindElement(By.Id("password"));

            passwordField.Clear();

            // Click Login button
            var loginButton = driver.FindElement(By.Id("login-button"));
            loginButton.Click();
        }
    }
}
