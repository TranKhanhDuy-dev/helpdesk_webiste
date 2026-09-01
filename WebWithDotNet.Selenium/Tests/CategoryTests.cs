using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace WebWithDotNet.Selenium.Tests;

public class CategoryTests
{
    public IWebDriver CreateDriver()
    {
        IWebDriver driver = new ChromeDriver();

        driver.Navigate().GoToUrl("http://localhost:5274");

        return driver;
    }

    [Fact]
    public void CreateNewCategory()
    {
        using IWebDriver driver = CreateDriver();

        WebDriverWait wait = new WebDriverWait(
            driver,
            TimeSpan.FromSeconds(10)
        );

        wait.Until(
            d => d.FindElement(By.Id("NewCategory"))
        ).Click();

        wait.Until(d =>
        {
            IWebElement element = d.FindElement(By.Id("InputName"));

            if (element.Displayed && element.Enabled)
            {
                return element;
            }

            return null;
        }).SendKeys("Hardware");
        Thread.Sleep(1000);

        string Description = "Issues related to computers, laptops, monitors, printers, keyboards, mice, and other physical equipment.";
        driver.FindElement(By.Id("InputDescription")).SendKeys(Description);
        Thread.Sleep(1000);

        SelectElement status = new SelectElement(driver.FindElement(By.Id("InputStatus")));
        status.SelectByText("Active");
        Thread.Sleep(1000);

        driver.FindElement(By.Id("SubmitButton")).Click();
        Thread.Sleep(2000);
    }
}