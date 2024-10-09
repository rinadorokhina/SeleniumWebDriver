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
        public void SortCountries()
        {
            driver.Url = url + "?app=countries&doc=countries";
            List<string> countriesName = [];
            var countries = driver.FindElements(By.CssSelector(".row a[href]:not([title])"));
            foreach (var country in countries)
            {
                countriesName.Add(country.GetAttribute("text"));
            }
            List<string> sortCountriesNames = new List<string>(countriesName);
            sortCountriesNames.Sort();
            try
            {
                Assert.That(sortCountriesNames, Is.EqualTo(countriesName));
            }
            catch (Exception)
            {
                Assert.Fail("Страны не отсортированы");
            }
        }
        [Test]
        public void CountriesZone()
        {
            driver.Url = url+"?app=countries&doc=countries";
            var countryElements = driver.FindElements(By.CssSelector("tr.row"));
            for (int i = 0; i < countryElements.Count; i++)
            {
                countryElements = driver.FindElements(By.CssSelector("tr.row"));
                int zoneCount = int.Parse(countryElements[i].FindElement(By.CssSelector("td:nth-child(6)")).GetAttribute("textContent"));
                if (zoneCount > 0)
                {
                    countryElements[i].FindElement(By.CssSelector("i.fa.fa-pencil")).Click();
                    var geoZoneElements = driver.FindElements(By.CssSelector("td:nth-child(3)"));
                    List<string> geoZoneNames = new List<string>();
                    for (int j = 0; j < geoZoneElements.Count - 1; j++)
                    {
                        string geoZoneName = geoZoneElements[j].GetAttribute("textContent");
                        geoZoneNames.Add(geoZoneName);
                    }
                    List<string> sortedGeoZoneNames = new List<string>(geoZoneNames);
                    sortedGeoZoneNames.Sort();
                    Assert.That(geoZoneNames, Is.EqualTo(sortedGeoZoneNames));
                    driver.Navigate().Back();
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