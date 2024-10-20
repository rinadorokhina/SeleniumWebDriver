using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

using System.Drawing;
using OpenQA.Selenium.Edge;
using System.Globalization;
using SeleniumExtras.WaitHelpers;


namespace Test2
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
       
        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void WorkWithCard()
        {
            driver.Url="http://localhost:8080/litecart/";
            for (int i = 0; i < 3; i++)
            {
                var firstProduct = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".product a.link")));
                firstProduct.Click();
                List<IWebElement> select = driver.FindElements(By.CssSelector("select[name='options[Size]")).ToList();
                if (select.Count > 0)
                {
                    var element = new SelectElement(select[0]);
                    element.SelectByIndex(2);
                }
                var addToCartButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("add_cart_product")));
                addToCartButton.Click();
                var cartCounter = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("span.quantity")));
                int expectedCount = int.Parse(cartCounter.Text) + 1;
                wait.Until(ExpectedConditions.TextToBePresentInElement(cartCounter, expectedCount.ToString()));
                driver.Navigate().GoToUrl("http://localhost:8080/litecart/");
            }
            var checkoutLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.link[href*='checkout']")));
            checkoutLink.Click();
            while (true)
            {
                var removeButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("remove_cart_item")));
                removeButton.Click();
                wait.Until(ExpectedConditions.StalenessOf(removeButton));
                var productsTable = driver.FindElements(By.CssSelector("table.dataTable td.item"));
                if (productsTable.Count == 0)
                {
                    break;
                }
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