using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Linq;

public class ProductPage
{
    private IWebDriver driver;
    private WebDriverWait wait;

    public ProductPage(IWebDriver driver, WebDriverWait wait)
    {
        this.driver = driver;
        this.wait = wait;
    }

    public void AddProductToCart(int expectedCount)
    {
        List<IWebElement> select = driver.FindElements(By.CssSelector("select[name='options[Size]']")).ToList();
        if (select.Count > 0)
        {
            var element = new SelectElement(select[0]);
            element.SelectByIndex(2); 
        }
        var addToCartButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("add_cart_product")));
        addToCartButton.Click();
        var cartCounter = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("span.quantity")));
        wait.Until(ExpectedConditions.TextToBePresentInElement(cartCounter, expectedCount.ToString()));
    }
}