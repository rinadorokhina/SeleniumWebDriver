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

        [Test]
        public void FirstTest()
        {
            driver.Url = "https://software-testing.ru/";
            Thread.Sleep(1000);
        }

        [TearDown]
        public void stop()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Quit();
        }
    }
}