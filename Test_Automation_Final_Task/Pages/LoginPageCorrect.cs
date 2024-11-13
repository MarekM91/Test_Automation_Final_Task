using OpenQA.Selenium;

namespace Test_Automation_Final_Task.Pages
{
    internal class LoginPageCorrect
    {
        private static string Url { get; } = "https://www.saucedemo.com/";

        IWebDriver driver;

        public LoginPageCorrect(IWebDriver driver) => this.driver = driver ?? throw new ArgumentException(nameof(driver));

        public LoginPageCorrect Open()
        {
            driver.Url = Url;
            return this;
        }

        public void Login(string username, string password)
        {
            // Username
            var usernameField = driver.FindElement(By.Id("user-name"));

            usernameField.Clear();
            usernameField.SendKeys(username);

            // Password
            var passwordField = driver.FindElement(By.Id("password"));

            passwordField.Clear();
            passwordField.SendKeys(password);

            // Click Login button
            var loginButton = driver.FindElement(By.Id("login-button"));
            loginButton.Click();
        }
    }
}
