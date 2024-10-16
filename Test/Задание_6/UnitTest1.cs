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
        public void Test6()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("remember_me")).Click();
            driver.FindElement(By.Name("login")).Click();
            string title = "My Store";
            wait.Until(d => d.Title.Equals(title));
            ClickAllElements();
        }
        
        public void ClickAllElements() 
        {
            var menuItem = driver.FindElements(By.Id("app-"));
            for (int i = 0; i < menuItem.Count; i++)
            {
                try
                {
                    menuItem = driver.FindElements(By.Id("app-"));
                    menuItem[i].Click();
                    Assert.IsTrue(driver.FindElement(By.CssSelector("h1")).Displayed);
                    var subMenuItem = driver.FindElements(By.XPath("//*[@id=\"app-\"]/ul"));
                    for (int j = 0; j < subMenuItem.Count; j++)
                    {
                        subMenuItem = driver.FindElements(By.CssSelector("ul.docs li"));
                        subMenuItem[j].Click();
                        Assert.IsTrue(driver.FindElement(By.CssSelector("h1")).Displayed);
                    }
                }
                catch (Exception)
                {
                    throw;
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