using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Test
{
    [TestFixture]
    public class Test
    {
        private ChromeDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        public string GenerateEmail()
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            return $"email{randomNumber}@mail.com";
        }
        [Test]
        public void CreateUser()
        {
            driver.Url = "http://localhost:8080/litecart/";
            string email = GenerateEmail();
            string password = "password123";
            driver.FindElement(By.CssSelector("a[href*='create_account']")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys("Rina");
            driver.FindElement(By.Name("lastname")).SendKeys("Doro");
            driver.FindElement(By.Name("address1")).SendKeys("Test Adress");
            driver.FindElement(By.Name("postcode")).SendKeys("30010");
            driver.FindElement(By.Name("city")).SendKeys("Kansas");
            driver.FindElement(By.CssSelector(".select2-selection")).Click();
            driver.FindElement(By.CssSelector(".select2-search__field")).SendKeys("United States" + Keys.Enter);
            var zone = new SelectElement(driver.FindElement(By.CssSelector("select[name='zone_code")));
            zone.SelectByText("New York");
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys("+88005553535");
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(password);
            driver.FindElement(By.Name("create_account")).Click();
            driver.FindElement(By.CssSelector("a[href*='logout']")).Click();
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.CssSelector("a[href*='logout']")).Click();
        }

        [TearDown]
        public void stop()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Quit();
        }
    }
}