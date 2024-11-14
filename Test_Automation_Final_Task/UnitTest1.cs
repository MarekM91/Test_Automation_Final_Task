using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using Test_Automation_Final_Task.Pages;
using Newtonsoft.Json;
using System.IO;

namespace Test_Automation_Final_Task
{
    public class Tests
    {
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
            driver = CreateDriver(browserName);
        }

        private void AssertErrorMessage(string actual, string expected)
        {
            Assert.That(actual, Is.EqualTo(expected), "Error message does not match.");
        }

        [Test]
        [TestCaseSource(nameof(GetBrowsersFromJson))]
        public void TestLoginFormWithEmptyCredentials(string browserName)
        {
            Setup(browserName);

            var loginPage = new LoginPage(driver);
            loginPage.Open().Login();

            string actualErrorMessage = driver.FindElement(By.CssSelector("h3[data-test='error']")).Text;
            string expectedErrorMessage = "Epic sadface: Username is required";

            AssertErrorMessage(actualErrorMessage, expectedErrorMessage);
        }

        [Test]
        [TestCaseSource(nameof(GetBrowsersFromJson))]
        public void TestLoginFormWithUsernameNoPassword(string browserName)
        {
            Setup(browserName);

            var loginPage = new LoginPageUsername(driver);
            loginPage.Open().Login("standard_user");

            string actualErrorMessage = driver.FindElement(By.CssSelector("h3[data-test='error']")).Text;
            string expectedErrorMessage = "Epic sadface: Password is required";

            AssertErrorMessage(actualErrorMessage, expectedErrorMessage);
        }

        [Test]
        [TestCaseSource(nameof(GetBrowsersFromJson))]
        public void TestLoginFormWithCredentials(string browserName)
        {
            Setup(browserName);

            var loginPage = new LoginPage(driver);
            loginPage.Open().Login("standard_user", "secret_sauce");

            string actualLogoMessage = driver.FindElement(By.ClassName("app_logo")).Text;
            string expectedLogoMessage = "Swag Labs";

            AssertErrorMessage(actualLogoMessage, expectedLogoMessage);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
