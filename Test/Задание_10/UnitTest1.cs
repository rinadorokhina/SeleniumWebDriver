using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

using System.Drawing;
using OpenQA.Selenium.Edge;


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
          //driver = new ChromeDriver();
          driver = new FirefoxDriver();
          //driver = new EdgeDriver();
        }
        [Test]
        public void Test()
        {
            driver.Url = "http://localhost:8080/litecart/";
            //Главная страница
            string productNameMainPageText = driver.FindElement(By.CssSelector("#box-campaigns .name")).Text; //название на главной
            string regularPriceMainPageText = driver.FindElement(By.CssSelector("#box-campaigns .regular-price")).Text; //обычная цена на главной
            string campaignPriceMainPageText = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price")).Text; // акционная цена на главной
            string campaignPriceMainPageStrong = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price")).GetAttribute("tagName"); //тег жирности у акционной цена
            string campaignPriceMainPageColor = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price")).GetCssValue("color"); // цвет акционной цены
            Size regularPriceMainPageSize = driver.FindElement(By.CssSelector("#box-campaigns .regular-price")).Size; // размер обычной цены
            Size campaignPriceMainPageSize = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price")).Size; // размер акционной цены
            
            //проверка акционная цена жирная и красная на главной странице
            var match = Regex.Match(campaignPriceMainPageColor, @"rgba?\((\d+),\s+(\d+),\s+(\d+)(?:,\s+\d+)?\)");
            string [] firstThreeNumbers = (match.Groups[1].Value + "," + match.Groups[2].Value + "," + match.Groups[3].Value).Split(',');
            Assert.Multiple(() =>
            {
                Assert.That(0, Is.EqualTo(int.Parse(firstThreeNumbers[1])).And.EqualTo(int.Parse(firstThreeNumbers[2])));
                Assert.That(driver.FindElement(By.CssSelector(campaignPriceMainPageStrong)).GetAttribute("tagName"), Is.EqualTo("STRONG"));
            });
            
            //проверка, что акционная цена больше, чем обычная
            Assert.Multiple(() =>
            {
                Assert.That(campaignPriceMainPageSize.Width, Is.GreaterThan(regularPriceMainPageSize.Width));
                Assert.That(campaignPriceMainPageSize.Height, Is.GreaterThan(regularPriceMainPageSize.Height));
            });

            //Страница товара
            driver.FindElement(By.CssSelector("#box-campaigns a.link")).Click();
            string productNameProductPageText = driver.FindElement(By.CssSelector("h1.title")).Text; //название на странице товара
            string regularPriceProductPageText = driver.FindElement(By.CssSelector("#box-product .regular-price")).Text; //обычная цена на странице товара
            string campaignPriceProductPageText = driver.FindElement(By.CssSelector("#box-product .campaign-price")).Text; // акционная цена на странице товара
            string regularPriceProductPageStrong = driver.FindElement(By.CssSelector("#box-product .regular-price")).GetAttribute("tagName"); //тег зачеркнутости у акционной цена
            string campaignPriceProductPageColor = driver.FindElement(By.CssSelector("#box-product .campaign-price")).GetCssValue("color"); // цвет акционной цены
            string regularPriceProductPageColor = driver.FindElement(By.CssSelector("#box-product .regular-price")).GetCssValue("color"); // цвет обычной цены
            Size regularPriceProductPageSize = driver.FindElement(By.CssSelector("#box-product .regular-price")).Size; // размер обычной цены
            Size campaignPriceProductPageSize = driver.FindElement(By.CssSelector("#box-product .campaign-price")).Size; // размер акционной цены

            //проверка, что текст на главной равен тексту на странице товара
            Assert.That(productNameProductPageText, Is.EqualTo(productNameMainPageText));

            //проверка, что цены на главной равны ценам на странице товара
            Assert.Multiple(() =>
            {
                Assert.That(regularPriceProductPageText, Is.EqualTo(regularPriceMainPageText));
                Assert.That(campaignPriceProductPageText, Is.EqualTo(campaignPriceMainPageText));
            });

            //проверка, что обычная цена зачеркнутая и серая
            var match2 = Regex.Match(regularPriceProductPageColor, @"rgba?\((\d+),\s+(\d+),\s+(\d+)(?:,\s+\d+)?\)");
            string[] firstThreeNumbers2 = (match2.Groups[1].Value + "," + match2.Groups[2].Value + "," + match2.Groups[3].Value).Split(',');
            Assert.Multiple(() =>
            {
                Assert.That(driver.FindElement(By.CssSelector(regularPriceProductPageStrong)).GetAttribute("tagName"), Is.EqualTo("S"));
                Assert.That(int.Parse(firstThreeNumbers2[0]), Is.EqualTo(int.Parse(firstThreeNumbers2[1])).And.EqualTo(int.Parse(firstThreeNumbers2[2])));
            });

            //проверка акционная цена жирная и красная на странице товара
            var match1 = Regex.Match(campaignPriceProductPageColor, @"rgba?\((\d+),\s+(\d+),\s+(\d+)(?:,\s+\d+)?\)");
            string[] firstThreeNumbers1 = (match1.Groups[1].Value + "," + match1.Groups[2].Value + "," + match1.Groups[3].Value).Split(',');
            Assert.Multiple(() =>
            {
                Assert.That(0, Is.EqualTo(int.Parse(firstThreeNumbers1[1])).And.EqualTo(int.Parse(firstThreeNumbers1[2])));
                Assert.That(driver.FindElement(By.CssSelector(campaignPriceMainPageStrong)).GetAttribute("tagName"), Is.EqualTo("STRONG"));
            });

            //проверка, что акционная цена больше, чем обычная
            Assert.Multiple(() =>
            {
                Assert.That(campaignPriceProductPageSize.Width, Is.GreaterThan(regularPriceProductPageSize.Width));
                Assert.That(campaignPriceProductPageSize.Height, Is.GreaterThan(regularPriceProductPageSize.Height));
            });
        }

        [TearDown]
        public void Stop()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Quit();
        }
    }
}