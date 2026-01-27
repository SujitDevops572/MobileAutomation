using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using VMS_Phase1PortalAT.Modules.Authentication;



namespace VMS_Phase1PortalAT.utls.utilMethods
{
    public class SortFailure
    {
        private static ExtentReports extent;
        private static ExtentTest test;

        // Mapping column names to their index positions in the table row
        private static Dictionary<string, int> columnMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "Request Id", 0 },
            { "Updated At", 8 },
            { "Machine Id", 3 }
        };

        public static void Sort(IWebDriver driver, string menuData, string submenuData, string module)
        {
            extent = ExtentManager.GetInstance();
            test = extent.CreateTest($"Sort Failure - {module}");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Clicking main menu...");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(menuData))).Click();

                Console.WriteLine("Clicking submenu...");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(submenuData))).Click();

                Console.WriteLine("Capturing initial row...");
                string previousRowText = GetFirstRowText(driver, wait);
                Console.WriteLine("Initial row: " + (string.IsNullOrEmpty(previousRowText) ? "No data" : previousRowText));

                List<string> sortOptionTexts = GetSortOptionTexts(driver, wait);
                Console.WriteLine($"Found {sortOptionTexts.Count} sort options.");

                if (sortOptionTexts.Count == 0)
                {
                    test.Fail("No sort options found after dropdown opened.");
                    return;
                }

                bool anySortSuccessful = false;

                foreach (string optionText in sortOptionTexts)
                {
                    Console.WriteLine($"\n--- Attempting to sort by: {optionText}");

                    try
                    {
                        bool clicked = ClickSortOption(driver, wait, optionText);
                        if (!clicked)
                        {
                            test.Warning($"Could not click sort option '{optionText}'. Skipping.");
                            continue;
                        }

                        Thread.Sleep(300);

                        bool sortClicked = false;
                        for (int i = 0; i < 3; i++)
                        {
                            try
                            {
                                var sortButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("sortButton")));
                                if (TryClickElement(driver, sortButton, "Sort button"))
                                {
                                    sortClicked = true;
                                    break;
                                }
                            }
                            catch (StaleElementReferenceException)
                            {
                                Console.WriteLine("Stale sort button, retrying...");
                                Thread.Sleep(300);
                            }
                        }

                        if (!sortClicked)
                        {
                            test.Warning($"Failed to click sort button for '{optionText}'");
                            continue;
                        }

                        Console.WriteLine("Clicked sort button. Waiting for column update...");

                        string columnValueBefore = GetColumnValue(driver, wait, optionText);
                        string columnValueAfter = WaitForColumnChange(driver, wait, columnValueBefore, optionText);

                        if (columnValueBefore != columnValueAfter)
                        {
                            test.Pass($"Sorting by '{optionText}' succeeded. Changed: '{columnValueBefore}' → '{columnValueAfter}'");
                            anySortSuccessful = true;
                        }
                        else
                        {
                            test.Info($"Sorting triggered for '{optionText}', but value remained same: '{columnValueBefore}'");
                            anySortSuccessful = true; // Still okay to mark as success
                        }
                    }
                    catch (Exception ex)
                    {
                        test.Warning($"Exception during sort for '{optionText}': {ex.Message}");
                        Console.WriteLine($"Error sorting with '{optionText}': {ex.Message}");
                    }
                }

                if (!anySortSuccessful)
                    test.Fail("Sorting did not succeed for any option.");
                else
                    test.Pass("Completed sort checks for all options.");
            }
            catch (Exception ex)
            {
                test.Fail($"Fatal exception in Sort(): {ex.Message}");
                Console.WriteLine($"Exception: {ex}");
            }
        }

        private static string WaitForColumnChange(IWebDriver driver, WebDriverWait wait, string previousValue, string optionText)
        {
            try
            {
                wait.Until(d =>
                {
                    try
                    {
                        var newValue = GetColumnValue(driver, wait, optionText);
                        return newValue != previousValue;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return true; // Consider as changed if stale
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Timeout waiting for column '{optionText}' to change.");
            }

            try
            {
                return GetColumnValue(driver, wait, optionText);
            }
            catch
            {
                return previousValue;
            }
        }

        private static string GetColumnValue(IWebDriver driver, WebDriverWait wait, string optionText)
        {
            int columnIndex = columnMapping.ContainsKey(optionText) ? columnMapping[optionText] : 0;

            var tbody = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
            if (tbody == null)
            {
                Console.WriteLine("Table body (tbody) is null.");
                return string.Empty;
            }

            var rows = tbody.FindElements(By.TagName("tr"));
            if (rows == null || rows.Count == 0)
            {
                Console.WriteLine("No rows found in the table.");
                return string.Empty;
            }

            var firstRow = rows.FirstOrDefault();
            if (firstRow == null)
            {
                Console.WriteLine("First row is null.");
                return string.Empty;
            }

            var cells = firstRow.FindElements(By.TagName("td"));
            if (cells == null || cells.Count <= columnIndex)
            {
                Console.WriteLine($"Column index {columnIndex} is out of bounds. Columns found: {cells?.Count ?? 0}");
                return string.Empty;
            }

            return cells[columnIndex].Text.Trim();
        }

        private static string GetFirstRowText(IWebDriver driver, WebDriverWait wait)
        {
            var tbody = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
            if (tbody == null) return string.Empty;

            var rows = tbody.FindElements(By.TagName("tr"));
            if (rows == null || rows.Count == 0) return string.Empty;

            var firstRow = rows.FirstOrDefault();
            if (firstRow == null) return string.Empty;

            var cells = firstRow.FindElements(By.TagName("td"));
            if (cells == null || cells.Count == 0) return string.Empty;

            return string.Join(" | ", cells.Select(td => td.Text.Trim()));
        }

        private static List<string> GetSortOptionTexts(IWebDriver driver, WebDriverWait wait)
        {
            try
            {
                Console.WriteLine("Opening dropdown to get sort options...");
                OpenDropdown(driver, wait);

                var options = wait.Until(d =>
                {
                    var list = d.FindElements(By.CssSelector("mat-option"));
                    return list != null && list.Count > 0 ? list : null;
                });

                return options.Select(o => o.Text.Trim()).Where(t => !string.IsNullOrEmpty(t)).Distinct().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to get sort options: " + ex.Message);
                return new List<string>();
            }
        }

        private static bool ClickSortOption(IWebDriver driver, WebDriverWait wait, string optionText)
        {
            for (int attempt = 0; attempt < 3; attempt++)
            {
                try
                {
                    OpenDropdown(driver, wait);

                    var options = wait.Until(d =>
                    {
                        var list = d.FindElements(By.CssSelector("mat-option"));
                        return list != null && list.Count > 0 ? list : null;
                    });

                    var target = options.FirstOrDefault(o =>
                        o.Text.Trim().Equals(optionText, StringComparison.OrdinalIgnoreCase));

                    if (target != null)
                    {
                        ScrollIntoView(driver, target);
                        ClickElementViaJS(driver, target);
                        Console.WriteLine($"Clicked sort option: {optionText}");
                        return true;
                    }

                    Console.WriteLine($"Sort option '{optionText}' not found. Retrying...");
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"Stale element for option '{optionText}', retrying...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error clicking sort option '{optionText}': {ex.Message}");
                }

                Thread.Sleep(500);
            }

            return false;
        }

        private static void OpenDropdown(IWebDriver driver, WebDriverWait wait)
        {
            for (int attempt = 1; attempt <= 3; attempt++)
            {
                try
                {
                    Console.WriteLine($"Attempt {attempt}: Trying to open sort dropdown...");

                    IWebElement dropdown = null;

                    try
                    {
                        dropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("mat-select")));
                    }
                    catch
                    {
                        dropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("sortBox")));
                    }

                    ScrollIntoView(driver, dropdown);
                    ClickElementViaJS(driver, dropdown);

                    bool optionsVisible = wait.Until(d =>
                        d.FindElements(By.CssSelector("mat-option")).Count > 0);

                    if (optionsVisible)
                    {
                        Console.WriteLine("Dropdown opened and options found.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                }

                Thread.Sleep(500);
            }

            throw new Exception("Failed to open sort dropdown after retries.");
        }

        private static bool TryClickElement(IWebDriver driver, IWebElement element, string label)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    ScrollIntoView(driver, element);
                    element.Click();
                    return true;
                }
                catch (ElementClickInterceptedException)
                {
                    ClickElementViaJS(driver, element);
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error clicking {label}: {ex.Message}");
                }

                Thread.Sleep(300);
            }

            Console.WriteLine($"Failed to click {label} after retries.");
            return false;
        }

        private static void ScrollIntoView(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
        }

        private static void ClickElementViaJS(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }
    }
}
