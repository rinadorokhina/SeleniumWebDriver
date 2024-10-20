using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

using System.Drawing;
using OpenQA.Selenium.Edge;
using System.Globalization;
using SeleniumExtras.WaitHelpers;
using System;


namespace Test2
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string url = "http://localhost:8080/litecart/admin/";

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = url;
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("remember_me")).Click();
            driver.FindElement(By.Name("login")).Click();
            string title = "My Store";
            wait.Until(d => d.Title.Equals(title));
        }

        [Test]
        public void WorkWithPage()
        {
            driver.Navigate().GoToUrl("http://localhost:8080/litecart/admin/?app=countries&doc=countries");
            var firstCountryLink = wait.Until(drv => drv.FindElement(By.CssSelector("i.fa.fa-pencil"))); 
            firstCountryLink.Click();
            var externalLinks = driver.FindElements(By.CssSelector("i.fa-external-link")); 
            foreach (var link in externalLinks)
            {
                string originalWindow = driver.CurrentWindowHandle; 
                link.Click();
                wait.Until(drv => drv.WindowHandles.Count == 2);
                foreach (var window in driver.WindowHandles)
                {
                    if (window != originalWindow)
                    {
                        driver.SwitchTo().Window(window);
                        break;
                    }
                }
                driver.Close();
                driver.SwitchTo().Window(originalWindow);
            }
        }

        [TearDown]
        public void Stop()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Quit();
        }
    }
}