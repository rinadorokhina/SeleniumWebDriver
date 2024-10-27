using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class MainPage
{
    private IWebDriver driver;
    private WebDriverWait wait;

    public MainPage(IWebDriver driver, WebDriverWait wait)
    {
        this.driver = driver;
        this.wait = wait;
    }

    public void Open()
    {
        driver.Url = "http://localhost:8080/litecart/";
    }

    public void SelectFirstProduct()
    {
        var firstProduct = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".product a.link")));
        firstProduct.Click();
    }
}