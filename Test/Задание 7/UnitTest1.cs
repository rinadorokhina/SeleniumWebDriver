using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Test2
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
        public void Test7()
        {

            driver.Url = "http://localhost:8080/litecart/";
            var products = driver.FindElements(By.XPath("//li[contains(@class,'product')]"));
            foreach (var product in products)
            {
                try
                {
                    var stickers = product.FindElements(By.CssSelector("div.sticker"));
                    Assert.AreEqual(1, stickers.Count);
                  
                }
                catch (AssertionException)
                {
                    
                    Assert.Fail("Error");
                } 
            }
        }
        [TearDown]
        public void stop()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Quit();
        }
    }
}