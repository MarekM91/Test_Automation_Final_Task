using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Test_Automation_Final_Task.Pages;

namespace Test_Automation_Final_Task
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestLoginFormWithEmptyCredentials()
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                var loginPageEmpty = new LoginPageEmpty(driver);
                loginPageEmpty.Open().Login();

                string actualErrorMessage = driver.FindElement(By.CssSelector("h3[data-test='error']")).Text;
                string expectedErrorMessage = "Epic sadface: Username is required";

                Assert.That(actualErrorMessage, Is.EqualTo(expectedErrorMessage), "Error message does not match.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                driver.Quit();
            }
        }

        [Test]
        public void TestLoginFormWithUsernameNoPassword()
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                var loginPageUsername = new LoginPageUsername(driver);
                loginPageUsername.Open().Login("standard_user");

                string actualErrorMessage = driver.FindElement(By.CssSelector("h3[data-test='error']")).Text;
                string expectedErrorMessage = "Epic sadface: Password is required";

                Assert.That(actualErrorMessage, Is.EqualTo(expectedErrorMessage), "Error message does not match.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                driver.Quit();
            }
        }

        [Test]
        public void TestLoginFormWithCredentials()
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                var loginPageCorrect = new LoginPageCorrect(driver);
                loginPageCorrect.Open().Login("standard_user", "secret_sauce");

                string actualLogoMessage = driver.FindElement(By.ClassName("app_logo")).Text;
                string expectedLogoMessage = "Swag Labs";

                Assert.That(actualLogoMessage, Is.EqualTo(expectedLogoMessage), "Error message does not match.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}