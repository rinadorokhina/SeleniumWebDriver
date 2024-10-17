using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Diagnostics.Metrics;

namespace Test2
{
    [TestFixture]
    public class Test
    {
        private ChromeDriver driver;
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
        public void AddProduct()
        {
            // Переход в меню "Catalog"
            driver.FindElement(By.CssSelector("a[href*='catalog']")).Click();

            // Нажимаем кнопку "Add New Product"
            driver.FindElement(By.CssSelector("a[href*='edit_product']")).Click();

            // Вкладка "General"
            driver.FindElement(By.CssSelector("input[name=status][value='1']")).Click(); // Видимость
            driver.FindElement(By.Name("name[en]")).SendKeys("Cute Kitty"); //Наименование
            driver.FindElement(By.Name("code")).SendKeys("30010"); // Код продукта
            driver.FindElement(By.CssSelector("input[data-name='Rubber Ducks']")).Click(); // Категория
            var defaultCategory = new SelectElement(driver.FindElement(By.CssSelector("select[name='default_category_id"))); // Категория по умолчанию
            defaultCategory.SelectByText("Rubber Ducks");
            driver.FindElement(By.CssSelector("input[value='1-3']")).Click(); // Пол
            driver.FindElement(By.CssSelector("input[value='1-1']")).Click();
            driver.FindElement(By.CssSelector("input[value='1-2']")).Click();
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "cat.jpg"); // Gуть к картинке
            driver.FindElement(By.Name("new_images[]")).SendKeys(imagePath);
            driver.FindElement(By.Name("quantity")).Clear();
            driver.FindElement(By.Name("quantity")).SendKeys("10"); // Количество
            var soldOutStatus = new SelectElement(driver.FindElement(By.CssSelector("select[name='sold_out_status_id"))); // Солдаут статус
            soldOutStatus.SelectByText("Temporary sold out");
            driver.FindElement(By.CssSelector("input[name='date_valid_from']")).Clear();
            driver.FindElement(By.CssSelector("input[name='date_valid_from']")).SendKeys("08.10.2023"); // Дата действия с
            driver.FindElement(By.CssSelector("input[name='date_valid_to']")).Clear();
            driver.FindElement(By.CssSelector("input[name='date_valid_to']")).SendKeys("08.10.2025"); // Дата действия по

            // Вкладка "Information"
            driver.FindElement(By.CssSelector("a[href='#tab-information']")).Click();
            Thread.Sleep(1000);
            var manufacturerSelect = new SelectElement(driver.FindElement(By.Name("manufacturer_id"))); // Производитель
            manufacturerSelect.SelectByText("ACME Corp.");
            driver.FindElement(By.Name("keywords")).SendKeys("Cat, kitty, cute"); // Ключевые слова
            driver.FindElement(By.Name("short_description[en]")).SendKeys("This is a cute kitty."); // Краткое описание
            driver.FindElement(By.CssSelector(".trumbowyg-editor")).SendKeys("This is a cute kitty with a full description."); // Полное описание
            driver.FindElement(By.Name("head_title[en]")).SendKeys("Kitty"); // Заголовок
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("Cute"); // Метаописание

            // Вкладка "Prices"
            driver.FindElement(By.CssSelector("a[href='#tab-prices']")).Click();
            Thread.Sleep(1000); 
            driver.FindElement(By.Name("purchase_price")).Clear();
            driver.FindElement(By.Name("purchase_price")).SendKeys("38"); // Закупочная цена
            var currencySelect = new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code"))); // Валюта
            currencySelect.SelectByText("Euros");
            driver.FindElement(By.Name("prices[USD]")).SendKeys("45"); // Цена
            driver.FindElement(By.Name("prices[EUR]")).SendKeys("58");
            driver.FindElement(By.Name("save")).Click(); // Сохранение
            driver.FindElement(By.CssSelector("a[href*='catalog']")).Click(); // Назад в каталог
            Thread.Sleep(1000); 
            Assert.IsTrue(driver.PageSource.Contains("Cute Kitty")); //Проверка, что есть такой товар          
        }

        [TearDown]
        public void Stop()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Quit();
        }
    }
}