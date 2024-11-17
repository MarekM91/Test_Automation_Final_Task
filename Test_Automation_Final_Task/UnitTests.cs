using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using Test_Automation_Final_Task.Pages;
using Newtonsoft.Json;
using log4net;
using OpenQA.Selenium.Support.UI;

namespace Test_Automation_Final_Task
{
    public class Tests
    {
        private static readonly ILog log = Logger.Log;
        private IWebDriver driver;
        private readonly Dictionary<string, string> browserSettings;

        public Tests()
        {
            browserSettings = LoadBrowserSettings();
        }

        private Dictionary<string, string> LoadBrowserSettings()
        {
            string settingPath = "C:\\Users\\mozi1\\source\\repos\\Test_Automation_Final_Task\\Test_Automation_Final_Task\\settings\\browserSettings.json";
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(settingPath));
        }

        public static IEnumerable<string> GetBrowsersFromJson()
        {
            var browserConfig = new Tests().LoadBrowserSettings();
            foreach (var browserName in browserConfig.Keys)
            {
                yield return browserName;
            }
        }

        private IWebDriver CreateDriver(string browserName)
        {
            return browserName switch
            {
                "Edge" => new EdgeDriver(),
                "Firefox" => new FirefoxDriver(),
                "Chrome" => new ChromeDriver(),
                _ => throw new Exception("Unsupported browser name"),
            };
        }

        public void Setup(string browserName)
        {
            log.Info($"Setting up driver for browser: {browserName}");
            driver = CreateDriver(browserName);
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Window.Maximize();
        }

        private static void AssertErrorMessage(string actual, string expected)
        {
            log.Debug($"Running assertion for error message, actual: {actual}, expected: {expected}");
            Assert.That(actual, Is.EqualTo(expected), "Error message does not match.");
        }

        [Test]
        [TestCaseSource(nameof(GetBrowsersFromJson))]
        public void TestLoginFormWithEmptyCredentials(string browserName)
        {
            log.Info($"Running test Login form with empty credentials in {browserName}");
            Setup(browserName);

            var loginPage = new LoginPage(driver);
            loginPage.Open();

            // Type any credentials into "Username" and "Password" fields
            loginPage.TypeInUsername("test_user");
            loginPage.TypeInPassword("test_password");

            // Clear the inputs
            loginPage.ClearUsername();
            loginPage.ClearPassword();

            // Click login button
            loginPage.ClickLoginButton();

            string actualErrorMessage = driver.FindElement(By.CssSelector("h3[data-test='error']")).Text;
            string expectedErrorMessage = "Epic sadface: Username is required";

            AssertErrorMessage(actualErrorMessage, expectedErrorMessage);
        }

        [Test]
        [TestCaseSource(nameof(GetBrowsersFromJson))]
        public void TestLoginFormWithUsernameNoPassword(string browserName)
        {
            log.Info($"Running test Login form with username and no password in {browserName}");
            Setup(browserName);

            var loginPage = new LoginPage(driver);
            loginPage.Open();

            // Type any credentials in username
            loginPage.TypeInUsername("test_user");

            // Enter password
            loginPage.TypeInPassword("secret_sauce");

            // Clear the "Password" input
            loginPage.ClearPassword();

            // Click login button
            loginPage.ClickLoginButton();

            string actualErrorMessage = driver.FindElement(By.CssSelector("h3[data-test='error']")).Text;
            string expectedErrorMessage = "Epic sadface: Password is required";

            AssertErrorMessage(actualErrorMessage, expectedErrorMessage);
        }

        [Test]
        [TestCaseSource(nameof(GetBrowsersFromJson))]
        public void TestLoginFormWithCredentials(string browserName)
        {
            log.Info($"Running test Login form with credentials in {browserName}");
            Setup(browserName);

            var loginPage = new LoginPage(driver);

            // Enter Username and Password and Login
            loginPage.Open().Login("standard_user", "secret_sauce");

            string actualLogoMessage = driver.FindElement(By.ClassName("app_logo")).Text;
            string expectedLogoMessage = "Swag Labs";

            AssertErrorMessage(actualLogoMessage, expectedLogoMessage);
        }

        [TearDown]
        public void CloseBrowser()
        {
            log.Info("Closing browser.");
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
