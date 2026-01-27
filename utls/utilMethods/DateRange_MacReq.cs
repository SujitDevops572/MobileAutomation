using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using VMS_Phase1PortalAT.utls.datas.W.Transactions.Purchase;
using VMS_Phase1PortalAT.Modules.Authentication; 
namespace VMS_Phase1PortalAT.utls.utilMethods
{
    public class DateRange_MacReq
    {
        private static ExtentReports extent;
        private static ExtentTest test;

        public static void DateTimeAction(
            IWebDriver driver,
            string menuData,
            string submenuData,
            string Option,
            Dictionary<string, string> dict,
            string module)
        {
            extent = ExtentManager.GetInstance();
            test = extent.CreateTest($"DateRange - {module}");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Navigating to menu and submenu...");
                ClickMenu(wait, menuData);
                ClickSubMenu(wait, submenuData);
                ClickOption(wait, Option);
                ClickDateIconWithRetry(driver, wait);

                Console.WriteLine("Selecting start and end dates...");
                PickDate(wait, dict["year"], dict["Start Month"], dict["Start Date"]);
                PickDate(wait, dict["year"], dict["End Month"], dict["End Date"]);

                Console.WriteLine("Waiting for table to refresh after date selection...");
                Thread.Sleep(3000);

                Console.WriteLine("Validating table rows...");
                ValidateDateRange(wait, driver);

                test.Pass("Date range filter applied and validated successfully.");
            }
            catch (Exception ex)
            {
                test.Fail($"Exception in date range validation: {ex.Message}");
                Console.WriteLine($"Exception stack: {ex}");
            }
        }

        private static void ClickMenu(WebDriverWait wait, string menuId)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(menuId))).Click();
        }

        private static void ClickSubMenu(WebDriverWait wait, string subMenuId)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(subMenuId))).Click();
        }

        private static void ClickOption(WebDriverWait wait, string Option)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(Option))).Click();
        }

        private static void ClickDateIconWithRetry(IWebDriver driver, WebDriverWait wait, int maxRetries = 3)
        {
            int attempts = 0;
            while (attempts < maxRetries)
            {
                try
                {
                    var dateIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text()='date_range']")));
                    dateIcon.Click();
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    attempts++;
                    Console.WriteLine($"Stale element on date icon click. Retrying {attempts}/{maxRetries}...");
                }
            }

            throw new Exception("Failed to click date icon due to repeated stale element exceptions.");
        }

        private static void PickDate(WebDriverWait wait, string year, string month, string day)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.owl-dt-calendar-control"))).Click();
            Thread.Sleep(300);

            ClickPickerCell(wait, "owl-date-time-multi-year-view", year);
            ClickPickerCell(wait, "owl-date-time-year-view", month, StringComparison.OrdinalIgnoreCase);
            ClickPickerCell(wait, "owl-date-time-month-view", day, StringComparison.OrdinalIgnoreCase);
            Thread.Sleep(300);
        }

        private static void ClickPickerCell(WebDriverWait wait, string viewTag, string textToMatch, StringComparison comparison = StringComparison.Ordinal)
        {
            var view = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName(viewTag)));
            var option = view.FindElements(By.TagName("td"))
                             .FirstOrDefault(td => td.Text.Trim().Equals(textToMatch, comparison));
            if (option == null)
                throw new Exception($"Unable to find '{textToMatch}' in {viewTag} view");

            option.Click();
            Thread.Sleep(200);
        }

        private static void ValidateDateRange(WebDriverWait wait, IWebDriver driver)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//tbody//tr")));

            string pageNumber = "N/A";
            try
            {
                var activePage = driver.FindElement(By.CssSelector("ul.pagination li.active"));
                pageNumber = activePage.Text.Trim();
            }
            catch
            {
                // No pagination
            }

            string FormatMonth(string rawMonth) =>
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(rawMonth.ToLower());

            string startDateStr = $"{PurchaseSearchStockData.DateRange["year"]}-{FormatMonth(PurchaseSearchStockData.DateRange["Start Month"])}-{PurchaseSearchStockData.DateRange["Start Date"]}";
            string endDateStr = $"{PurchaseSearchStockData.DateRange["year"]}-{FormatMonth(PurchaseSearchStockData.DateRange["End Month"])}-{PurchaseSearchStockData.DateRange["End Date"]}";

            DateTime startDate = DateTime.ParseExact(startDateStr, "yyyy-MMM-d", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(endDateStr, "yyyy-MMM-d", CultureInfo.InvariantCulture);

            string[] supportedFormats = new[]
            {
                "yyyy/MM/dd", "yyyy-MM-dd", "dd/MM/yyyy", "dd-MM-yyyy",
                "MM/dd/yyyy", "M/d/yyyy", "d-MMM-yyyy", "dd MMM yyyy",
                "MMM d, yyyy", "d MMM yyyy", "MMM dd yyyy"
            };

            var rows = driver.FindElements(By.XPath("//tbody//tr"));
            Console.WriteLine($"Found {rows.Count} rows to validate on page {pageNumber}...");

            for (int rowIndex = 1; rowIndex <= rows.Count; rowIndex++)
            {
                bool rowValidated = false;
                int retry = 0;

                while (!rowValidated && retry < 2)
                {
                    try
                    {
                        var row = driver.FindElement(By.XPath($"//tbody//tr[{rowIndex}]"));
                        var cells = row.FindElements(By.TagName("td"));

                        for (int colIndex = 0; colIndex < cells.Count; colIndex++)
                        {
                            string text = cells[colIndex].Text.Split(' ')[0].Trim();

                            if (string.IsNullOrWhiteSpace(text) || text.ToLower().Contains("more_vert"))
                                continue;

                            if (DateTime.TryParseExact(text, supportedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                            {
                                Console.WriteLine($"Parsed date '{parsedDate:yyyy-MM-dd}' at Row {rowIndex}, Col {colIndex + 1}, Page {pageNumber}");

                                if (parsedDate < startDate || parsedDate > endDate)
                                {
                                    string errorMsg = $"Row {rowIndex}, Col {colIndex + 1}, Page {pageNumber}: Date '{parsedDate:dd-MM-yyyy}' is outside the range {startDate:dd-MM-yyyy} - {endDate:dd-MM-yyyy}";
                                    Console.WriteLine(errorMsg);
                                    test.Fail(errorMsg);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Skipped non-date or unmatched format '{text}' at Row {rowIndex}, Col {colIndex + 1}");
                            }
                        }

                        rowValidated = true;
                    }
                    catch (StaleElementReferenceException)
                    {
                        retry++;
                        Console.WriteLine($"Stale row {rowIndex} on attempt {retry}, retrying...");
                    }
                }
            }

            Console.WriteLine("Completed date validation for current page.");
        }
    }
}
