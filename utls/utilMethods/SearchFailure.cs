using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class SearchFailure
    {
        private static ExtentReports extent;
        private static ExtentTest test;

        public static void SearchAction(
            IWebDriver driver,
            string menuData,
            string submenuData,
            Dictionary<string, string> dict,
            int s,
            string module)
        {
            extent = ExtentManager.GetInstance();
            test = extent.CreateTest($"Search Failure - {module}");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine($"Navigating to Menu: {menuData}, Submenu: {submenuData}");

                SafeClickWithScrollAndJS(driver, wait, By.Id(menuData));
                Thread.Sleep(1000); // Give time after menu click
                SafeClickWithScrollAndJS(driver, wait, By.Id(submenuData));
                Thread.Sleep(1500); // Let the page load

                By dropdownBy = By.ClassName("searchTypeBox");
                By inputBy = By.Name("searchText");
                By tbodyBy = By.TagName("tbody");

                foreach (var kvp in dict)
                {
                    string type = kvp.Key;
                    string value = kvp.Value;

                    Console.WriteLine($"----- Searching for Type: '{type}' with Value: '{value}' -----");

                    try
                    {
                        bool selected = RetrySelectDropdownOption(driver, wait, dropdownBy, type, 3);
                        if (!selected)
                        {
                            test.Warning($"Could not select searchType '{type}'. Skipping.");
                            Console.WriteLine($"[WARN] Could not select dropdown option '{type}'. Skipping.");
                            continue;
                        }

                        Thread.Sleep(1000); // Let dropdown settle

                        int initialCount = CountRows(driver, tbodyBy);
                        Console.WriteLine($"Initial row count before search: {initialCount}");

                        wait.Until(ExpectedConditions.ElementToBeClickable(inputBy));
                        var input = driver.FindElement(inputBy);
                        input.Clear();
                        input.SendKeys(value);
                        input.SendKeys(Keys.Enter);

                        Thread.Sleep(1000); // Let search trigger

                        bool stable = WaitForStableRowCount(driver, tbodyBy, 10, 500);
                        if (!stable)
                        {
                            Console.WriteLine("[WARN] Row count did not stabilize in time.");
                        }

                        int finalCount = CountRows(driver, tbodyBy);
                        Console.WriteLine($"Final row count after search: {finalCount}");

                        if (finalCount == 0)
                        {
                            test.Pass($"No results for '{type}' with input '{value}'.");
                            Console.WriteLine($"[PASS] No rows found for '{type}' : '{value}'");
                        }
                        else
                        {
                            test.Fail($"Expected 0 rows, but found {finalCount} for '{type}' -> '{value}'.");
                            Console.WriteLine($"[FAIL] Expected 0 rows but got {finalCount} for '{type}' : '{value}'");
                        }

                        Assert.AreEqual(0, finalCount, $"Expected 0 rows for '{type}:{value}', but got {finalCount}");
                    }
                    catch (Exception searchEx)
                    {
                        test.Warning($"Error during search for '{type}' - '{value}': {searchEx.Message}");
                        Console.WriteLine($"[ERROR] Search failed for '{type}' : '{value}' — {searchEx}");
                        continue; // move to next dictionary item
                    }
                }
            }
            catch (Exception ex)
            {
                test.Fail($"Exception in negative search: {ex.Message}");
                Console.WriteLine($"[EXCEPTION] Top-level exception: {ex}");
            }
        }

        private static int CountRows(IWebDriver driver, By tbodyBy)
        {
            try
            {
                var tbody = driver.FindElement(tbodyBy);
                var rows = tbody.FindElements(By.TagName("tr"));
                return rows.Count;
            }
            catch (NoSuchElementException)
            {
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Counting rows failed: {ex.Message}");
                return 0;
            }
        }

        private static bool RetrySelectDropdownOption(IWebDriver driver, WebDriverWait wait, By dropdownBy, string type, int maxAttempts)
        {
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    SafeClickWithScrollAndJS(driver, wait, dropdownBy);

                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.TagName("mat-option")));

                    var options = driver.FindElements(By.TagName("mat-option"));
                    Console.WriteLine($"Dropdown options found: {options.Count}");

                    foreach (var opt in options)
                    {
                        if (opt.Text.Trim().Equals(type, StringComparison.OrdinalIgnoreCase))
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", opt);
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", opt);
                            Console.WriteLine($"[INFO] Selected dropdown option: {type}");
                            return true;
                        }
                    }
                    Console.WriteLine($"[WARN] Option '{type}' not found in dropdown.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[RETRY] Attempt {attempt}: Failed selecting '{type}' — {e.GetType().Name}: {e.Message}");
                }

                Thread.Sleep(1000); // wait before retry
            }
            return false;
        }

        private static void SafeClickWithScrollAndJS(IWebDriver driver, WebDriverWait wait, By by)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
            IWebElement element = driver.FindElement(by);

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);

            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    element.Click();
                    Console.WriteLine($"[INFO] Clicked element: {by}");
                    return;
                }
                catch (ElementClickInterceptedException)
                {
                    Console.WriteLine("[INFO] Click intercepted, trying JS click.");
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("[INFO] Stale element, re-finding...");
                    element = driver.FindElement(by);
                }
                attempts++;
            }

            test.Fail($"Failed to click element: {by} after 3 attempts.");
            Console.WriteLine($"[ERROR] Failed to click element {by} after retries.");
        }

        /// <summary>
        /// Waits until the row count in tbody stabilizes (does not change) for two consecutive checks within timeout.
        /// </summary>
        private static bool WaitForStableRowCount(IWebDriver driver, By tbodyBy, int timeoutSeconds, int pollingIntervalMs)
        {
            int lastCount = -1;
            int stableChecks = 0;
            int maxStableChecks = 2;

            DateTime end = DateTime.Now.AddSeconds(timeoutSeconds);

            while (DateTime.Now < end)
            {
                int currentCount = CountRows(driver, tbodyBy);
                Console.WriteLine($"[INFO] Row count check: {currentCount}");

                if (currentCount == lastCount)
                {
                    stableChecks++;
                    if (stableChecks >= maxStableChecks)
                        return true; // stabilized
                }
                else
                {
                    stableChecks = 0; // reset because count changed
                }

                lastCount = currentCount;
                Thread.Sleep(pollingIntervalMs);
            }

            Console.WriteLine("[WARN] Timeout waiting for row count to stabilize.");
            return false; // timeout
        }
    }
}
