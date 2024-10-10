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
        public void Sort()
        {
            driver.Url = url + "?app=geo_zones&doc=geo_zones";
            var countryElements = driver.FindElements(By.CssSelector(".dataTable .row"));
            for (int i = 0; i < countryElements.Count; i++)
            {
                countryElements = driver.FindElements(By.CssSelector(".dataTable .row"));
                countryElements[i].FindElement(By.CssSelector("i.fa.fa-pencil")).Click();
                var zoneElements = driver.FindElements(By.CssSelector("#table-zones td:nth-child(3) option:checked"));
                List<string> zoneNames = new List<string>();
                
                for (int j = 0; j < zoneElements.Count - 1; j++)
                {
                    string geoZoneName = zoneElements[j].GetAttribute("textContent");
                    zoneNames.Add(geoZoneName);
                }
                List<string> sortedZoneNames = new List<string>(zoneNames);
                sortedZoneNames.Sort();
                try
                {
                    Assert.That(zoneNames, Is.EqualTo(sortedZoneNames));
                    driver.Navigate().Back();
                }
                catch (AssertionException)
                {
                    Assert.Fail("Error");
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