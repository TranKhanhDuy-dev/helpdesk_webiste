using CsvHelper;
using ClosedXML.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace WebWithDotNet.Selenium.Tests;

public class CategoryAutomation
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public CategoryAutomation(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(
            _driver,
            TimeSpan.FromSeconds(10)
        );
    }

    public List<CategoryData> ReadCsv(string filePath)
    {
        using var reader = new StreamReader(filePath);

        using var csv = new CsvReader(
            reader,
            CultureInfo.InvariantCulture
        );

        return csv.GetRecords<CategoryData>().ToList();
    }

    public List<CategoryData> ReadXlsx(string filePath)
    {
        using var workbook = new XLWorkbook(filePath);

        var worksheet = workbook.Worksheet(1);

        var categories = new List<CategoryData>();

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            categories.Add(new CategoryData
            {
                CategoryId = row.Cell(2).GetString(),
                Name = row.Cell(3).GetString(),
                Description = row.Cell(4).GetString(),
                Status = row.Cell(5).GetString()
            });
        }

        return categories;
    }

    public List<CategoryData> ReadCategory(string filePath)
    {
        string basePath = AppContext.BaseDirectory;

        string? csvFile = Directory.GetFiles(
            basePath,
            "*.csv"
        ).FirstOrDefault();

        if (csvFile != null)
        {
            return ReadCsv(csvFile);
        }

        string? xlsxFile = Directory.GetFiles(
            basePath,
            "*.xlsx"
        ).FirstOrDefault();

        if (xlsxFile != null)
        {
            return ReadXlsx(xlsxFile);
        }

        throw new FileNotFoundException(
            "No CSV or XLSX file was found."
        );
    }

    private void CreateCategory(CategoryData category)
    {
        WebDriverWait wait = new WebDriverWait(
            _driver,
            TimeSpan.FromSeconds(10)
        );

        wait.Until(
            d => d.FindElement(By.Id("newcategory"))
        ).Click();

        wait.Until(d =>
        {
            IWebElement element = d.FindElement(By.Id("inputcategoryid"));

            if (element.Displayed && element.Enabled)
            {
                return element;
            }

            return null;
        }).SendKeys(category.CategoryId);

        _driver.FindElement(By.Id("inputname")).SendKeys(category.Name);

        _driver.FindElement(By.Id("inputdescription")).SendKeys(category.Description);

        SelectElement status = new SelectElement(_driver.FindElement(By.Id("inputstatus")));
        status.SelectByValue(category.Status.ToLower() == "active" ? "true" : "false");

        _driver.FindElement(By.Id("submitbutton")).Click();
    }

    public void CreateCategories(string filePath)
    {
        List<CategoryData> categories = ReadCategory(filePath);
        _driver.Navigate().GoToUrl("http://localhost:5274");

        foreach(var category in categories)
        {
            CreateCategory(category);
        }
    }
}