using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class CartPage
{
    private IWebDriver driver;
    private WebDriverWait wait;

    public CartPage(IWebDriver driver, WebDriverWait wait)
    {
        this.driver = driver;
        this.wait = wait;
    }

    public void OpenCart()
    {
        var checkoutLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.link[href*='checkout']")));
        checkoutLink.Click();
    }

    public void RemoveAllProducts()
    {
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
}
