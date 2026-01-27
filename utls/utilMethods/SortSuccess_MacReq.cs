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
    public class SortSuccess_MacReq
    {
        private static ExtentReports extent;
        private static ExtentTest test;

        public static void Sort(IWebDriver driver, string menuData, string submenuData, string module, string Option)
        {
            extent = ExtentManager.GetInstance();
            test = extent.CreateTest($"Sort Success - {module}");
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Clicking main menu...");
                var menu = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(menuData)));
                ClickElementWithRetries(driver, menu, "Main menu");

                Console.WriteLine("Clicking submenu...");
                var submenu = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(submenuData)));
                ClickElementWithRetries(driver, submenu, "Submenu");

                Console.WriteLine("Clicking submenu...");
                var option = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(Option)));
                ClickElementWithRetries(driver, option, "option");


                var previousRowTexts = GetFirstRowColumnTexts(driver, wait);
                string prevRowStr = string.Join(" | ", previousRowTexts);
                test.Info($"Captured initial first row: {prevRowStr}");
                Console.WriteLine("Initial first row: " + prevRowStr);

                var optionTexts = GetSortOptionTexts(driver, wait);
                Console.WriteLine("Sort options: " + string.Join(", ", optionTexts));

                foreach (var optionText in optionTexts)
                {
                    Console.WriteLine($"Attempting to sort by: '{optionText}'");
                    bool success = ClickOptionAndSort(driver, wait, optionText);

                    if (!success)
                    {
                        test.Fail($"Failed to sort by '{optionText}'.");
                        Console.WriteLine($"Failed to sort by '{optionText}'.");
                        continue;
                    }

                    // Delay after clicking sort to let page update properly
                    Thread.Sleep(1000);

                    var newRowTexts = GetFirstRowColumnTexts(driver, wait);
                    string newRowStr = string.Join(" | ", newRowTexts);
                    Console.WriteLine("New first row: " + newRowStr);

                    if (!AreTextListsEqual(previousRowTexts, newRowTexts))
                    {
                        test.Pass($"Sorted by '{optionText}' successfully.");
                        previousRowTexts = newRowTexts;  // Update to new row texts
                    }
                    else
                    {
                        test.Fail($"Sorting by '{optionText}' did not change the row.");
                        Console.WriteLine($"Sorting by '{optionText}' did not change the row.");
                    }

                    // Small delay before next iteration
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                test.Fail($"Critical error in Sort(): {ex.Message}");
                Console.WriteLine($"Critical error in Sort(): {ex}");
            }
        }

        // Extract only text values from the first row's columns
        private static List<string> GetFirstRowColumnTexts(IWebDriver driver, WebDriverWait wait)
        {
            var table = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
            var row = table.FindElements(By.TagName("tr")).FirstOrDefault();
            if (row == null)
                return new List<string>();

            // Small delay before fetching columns to avoid stale elements
            Thread.Sleep(300);

            var columns = row.FindElements(By.TagName("td"));
            return columns.Select(td => td.Text.Trim()).ToList();
        }

        private static List<string> GetSortOptionTexts(IWebDriver driver, WebDriverWait wait)
        {
            if (EnsureDropdownOpened(driver, wait))
            {
                var options = wait.Until(d =>
                {
                    var list = d.FindElements(By.TagName("mat-option"));
                    return list.Count > 0 ? list : null;
                });

                var texts = options.Select(o => o.Text.Trim()).Where(t => !string.IsNullOrEmpty(t)).Distinct().ToList();
                Console.WriteLine("Dropdown options retrieved: " + string.Join(", ", texts));
                return texts;
            }

            test.Fail("Dropdown did not open; no sort options available.");
            Console.WriteLine("Dropdown did not open; no sort options available.");
            return new List<string>();
        }

        private static bool ClickOptionAndSort(IWebDriver driver, WebDriverWait wait, string optionText)
        {
            if (!EnsureDropdownOpened(driver, wait))
            {
                test.Fail($"Dropdown did not open for option '{optionText}'.");
                Console.WriteLine($"Dropdown did not open for option '{optionText}'.");
                return false;
            }

            try
            {
                var option = wait.Until(d =>
                {
                    var opts = d.FindElements(By.TagName("mat-option"));
                    return opts.FirstOrDefault(o => o.Text.Trim().Equals(optionText, StringComparison.OrdinalIgnoreCase));
                });

                if (option == null)
                {
                    test.Fail($"Option '{optionText}' not found after opening dropdown.");
                    Console.WriteLine($"Option '{optionText}' not found after opening dropdown.");
                    return false;
                }

                ScrollIntoView(driver, option);
                ClickElementWithRetries(driver, option, $"Option '{optionText}'");

                // Wait for overlay (loading) to disappear
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div.overlay")));

                var sortBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("sortButton")));

                // Delay before clicking sort button to ensure UI is ready
                Thread.Sleep(500);

                ScrollIntoView(driver, sortBtn);
                ClickElementWithRetries(driver, sortBtn, "Sort button");

                // Delay after clicking sort button to let sorting process complete
                Thread.Sleep(800);

                return true;
            }
            catch (Exception ex)
            {
                test.Fail($"Exception selecting '{optionText}': {ex.Message}");
                Console.WriteLine($"Exception selecting '{optionText}': {ex}");
                return false;
            }
        }

        private static bool EnsureDropdownOpened(IWebDriver driver, WebDriverWait wait)
        {
            for (int attempt = 1; attempt <= 3; attempt++)
            {
                try
                {
                    var dropdown = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("sortBox")));
                    ScrollIntoView(driver, dropdown);
                    ClickElementWithRetries(driver, dropdown, "Sort dropdown");

                    Console.WriteLine($"Dropdown clicked (attempt {attempt}).");

                    bool optionsVisible = wait.Until(d =>
                    {
                        var list = d.FindElements(By.TagName("mat-option"));
                        return list.Count > 0;
                    });

                    if (optionsVisible) return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Attempt {attempt} to open dropdown failed: {ex.Message}");
                    Thread.Sleep(500);
                }
            }

            return false;
        }

        private static void ScrollIntoView(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);
        }

        private static void ClickElementWithRetries(IWebDriver driver, IWebElement element, string label)
        {
            int tries = 0;
            while (tries < 3)
            {
                try
                {
                    ScrollIntoView(driver, element);
                    element.Click();
                    Console.WriteLine($"Clicked {label}.");
                    return;
                }
                catch (ElementClickInterceptedException)
                {
                    Console.WriteLine($"{label} click intercepted, trying JS click.");
                    ClickElementViaJS(driver, element);
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"{label} is stale, retrying...");
                    tries++;
                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception clicking {label}: {ex.Message}");
                    tries++;
                    Thread.Sleep(300);
                }
            }

            // Last resort JS click
            Console.WriteLine($"Failed to click {label} normally, using JS click as last resort.");
            ClickElementViaJS(driver, element);
        }

        private static void ClickElementViaJS(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            Console.WriteLine("Clicked element via JS.");
        }

        // Compare lists of string texts instead of IWebElements
        private static bool AreTextListsEqual(List<string> a, List<string> b)
        {
            if (a.Count != b.Count) return false;
            return a.SequenceEqual(b);
        }
    }
}
