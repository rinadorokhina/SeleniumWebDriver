using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Задание_19
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private MainPage mainPage;
        private ProductPage productPage;
        private CartPage cartPage;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            mainPage = new MainPage(driver, wait);
            productPage = new ProductPage(driver, wait);
            cartPage = new CartPage(driver, wait);
        }

        [Test]
        public void WorkWithCart()
        {
            mainPage.Open();
            for (int i = 1; i <= 3; i++)
            {
                mainPage.SelectFirstProduct();
                productPage.AddProductToCart(i);
                mainPage.Open();
            }
            cartPage.OpenCart();
            cartPage.RemoveAllProducts();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}