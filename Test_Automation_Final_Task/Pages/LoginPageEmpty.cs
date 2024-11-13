using OpenQA.Selenium;

namespace Test_Automation_Final_Task.Pages
{
    public class LoginPageEmpty
    {
        private static string Url { get; } = "https://www.saucedemo.com/";

        IWebDriver driver;

        public LoginPageEmpty(IWebDriver driver) => this.driver = driver ?? throw new ArgumentException(nameof(driver));

        public LoginPageEmpty Open()
        {
            driver.Url = Url;
            return this;
        }

        public void Login()
        {
            // Username
            var usernameField = driver.FindElement(By.Id("user-name"));

            usernameField.Clear();

            // Password
            var passwordField = driver.FindElement(By.Id("password"));

            passwordField.Clear();

            // Click Login button
            var loginButton = driver.FindElement(By.Id("login-button"));
            loginButton.Click();
        }
    }
}
