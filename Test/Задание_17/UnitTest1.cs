using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Задание_17
{
    [TestFixture]
    public class Test
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("http://localhost:8080/litecart/admin/");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }

        [Test]
        public void CheckProductPageLogs()
        {
            driver.Navigate().GoToUrl("http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1");
            var pagesWithLogs = new List<string>();
            IList<IWebElement> productLinks = driver.FindElements(By.CssSelector("a[href*='product_id']:not([title='Edit'])"));
            int productCount = productLinks.Count;
            for (int i = 0; i < productCount; i++)
            {
                productLinks = driver.FindElements(By.CssSelector("a[href*='product_id']:not([title='Edit'])"));
                productLinks[i].Click();
                var logs = driver.Manage().Logs.GetLog(LogType.Browser);
                if (logs.Count > 0)
                {
                    pagesWithLogs.Add($"Страница товара с логами: {driver.Url}");
                }
                driver.Navigate().Back();
            }
            if (pagesWithLogs.Count > 0)
            {
                Assert.Fail(string.Join("\n", pagesWithLogs));
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
