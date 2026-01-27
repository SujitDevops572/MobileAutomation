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
    public class SearchSuccess
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
            test = extent.CreateTest($"Search Success - {module}");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine($"Navigating to Menu: {menuData}, Submenu: {submenuData}");

                SafeClickWithScrollAndJS(driver, wait, By.Id(menuData));
                Thread.Sleep(1000);

                SafeClickWithScrollAndJS(driver, wait, By.Id(submenuData));
                Thread.Sleep(1500);

                By searchTypeBoxBy = By.ClassName("searchTypeBox");

                foreach (var kvp in dict)
                {
                    string searchType = kvp.Key;
                    string expectedValue = kvp.Value;

                    Console.WriteLine($"----- Searching for Type: '{searchType}' with Value: '{expectedValue}' -----");

                    try
                    {
                        SafeClickWithScrollAndJS(driver, wait, searchTypeBoxBy);
                        Thread.Sleep(500);

                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.TagName("mat-option")));
                        IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));

                        bool found = false;
                        foreach (var option in options)
                        {
                            if (option.Text.Trim().Equals(searchType, StringComparison.OrdinalIgnoreCase))
                            {
                                RetryAction(() => SafeClickJS(driver, option), () =>
                                    driver.FindElement(By.TagName("mat-option")), wait);
                                Console.WriteLine($"[INFO] Selected dropdown option: {searchType}");
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            test.Warning($"Search type '{searchType}' not found in dropdown.");
                            Console.WriteLine($"[WARN] Option '{searchType}' not found in dropdown.");
                            continue;
                        }

                        Thread.Sleep(1000);

                        By searchInputBy = By.Name("searchText");

                        Console.WriteLine($"[INFO] Waiting for search input to be clickable...");
                        RetryFind(driver, searchInputBy, wait);  // Ensure element is located

                        IWebElement searchInput = wait.Until(ExpectedConditions.ElementIsVisible(searchInputBy));
                        wait.Until(ExpectedConditions.ElementToBeClickable(searchInputBy));
                        Console.WriteLine($"[INFO] Search input ready.");

                        RetryAction(() =>
                        {
                            searchInput.Clear();
                            Thread.Sleep(100);
                            searchInput.SendKeys(expectedValue);
                            Thread.Sleep(200);
                            searchInput.SendKeys(Keys.Enter);
                        }, () => driver.FindElement(searchInputBy), wait);

                        Thread.Sleep(1000);

                        By tbodyBy = By.TagName("tbody");
                        wait.Until(ExpectedConditions.ElementIsVisible(tbodyBy));

                        bool stable = WaitForStableRowCount(driver, tbodyBy, 10, 500);
                        if (!stable)
                        {
                            Console.WriteLine("[WARN] Row count did not stabilize after search.");
                        }

                        IWebElement tbody = RetryFind(driver, tbodyBy, wait);
                        var rows = tbody.FindElements(By.TagName("tr"));
                        Console.WriteLine($"[INFO] Row count after search: {rows.Count}");

                        if (rows.Count == 0)
                        {
                            test.Fail($"No results found for '{searchType}' with value '{expectedValue}'");
                            Console.WriteLine($"[FAIL] No results found for '{searchType}' = '{expectedValue}'");
                            continue;
                        }

                        string actualValue = driver.FindElement(searchInputBy).GetAttribute("value") ?? "";
                        if (actualValue == expectedValue)
                        {
                            test.Pass($"Search successful for '{searchType}' with value '{expectedValue}'");
                            Console.WriteLine($"[PASS] Search matched for '{searchType}' = '{actualValue}'");
                        }
                        else
                        {
                            test.Fail($"Search value mismatch for '{searchType}'. Expected: '{expectedValue}', Found: '{actualValue}'");
                            Console.WriteLine($"[FAIL] Mismatch for '{searchType}'. Expected: '{expectedValue}', Found: '{actualValue}'");
                        }
                    }
                    catch (Exception searchEx)
                    {
                        test.Warning($"Error during search for '{searchType}' - '{expectedValue}': {searchEx.Message}");
                        Console.WriteLine($"[ERROR] Search failed for '{searchType}' = '{expectedValue}' — {searchEx}");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                test.Fail($"Unhandled exception during search: {ex.Message}");
                Console.WriteLine($"[EXCEPTION] Top-level exception: {ex}");
            }
        }

        private static void SafeClickWithScrollAndJS(IWebDriver driver, WebDriverWait wait, By by)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
            IWebElement element = RetryFind(driver, by, wait);

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView({block: 'center'});", element);

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
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
                    Console.WriteLine($"[INFO] Click intercepted, used JS click on: {by}");
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("[INFO] Stale element, re-finding...");
                    element = driver.FindElement(by);
                }
                attempts++;
            }
            test.Fail($"Unable to click element: {by} after 3 attempts.");
            Console.WriteLine($"[ERROR] Failed to click element {by} after retries.");
        }

        private static void SafeClickJS(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }

        private static void RetryAction(Action action, Func<IWebElement> elementFactory, WebDriverWait wait, int maxAttempts = 3)
        {
            int attempts = 0;
            while (attempts < maxAttempts)
            {
                try
                {
                    action();
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    wait.Until(ExpectedConditions.StalenessOf(elementFactory()));
                }
                catch (WebDriverTimeoutException e)
                {
                    Console.WriteLine($"[WARN] Timeout in RetryAction: {e.Message}");
                }
                attempts++;
            }
            throw new WebDriverTimeoutException("Element remained stale or unclickable after retries.");
        }

        private static IWebElement RetryFind(IWebDriver driver, By by, WebDriverWait wait, int maxAttempts = 3)
        {
            int attempts = 0;
            while (attempts < maxAttempts)
            {
                try
                {
                    return driver.FindElement(by);
                }
                catch (StaleElementReferenceException)
                {
                    wait.Until(ExpectedConditions.ElementExists(by));
                }
            }
            return driver.FindElement(by);
        }

        private static bool WaitForStableRowCount(IWebDriver driver, By tbodyBy, int timeoutSeconds, int pollingIntervalMs)
        {
            int lastCount = -1;
            int stableChecks = 0;
            int maxStableChecks = 2;

            DateTime end = DateTime.Now.AddSeconds(timeoutSeconds);

            while (DateTime.Now < end)
            {
                int currentCount = 0;
                try
                {
                    currentCount = driver.FindElement(tbodyBy).FindElements(By.TagName("tr")).Count;
                }
                catch
                {
                    currentCount = 0;
                }

                Console.WriteLine($"[INFO] Row count check: {currentCount}");

                if (currentCount == lastCount)
                {
                    stableChecks++;
                    if (stableChecks >= maxStableChecks)
                        return true;
                }
                else
                {
                    stableChecks = 0;
                }

                lastCount = currentCount;
                Thread.Sleep(pollingIntervalMs);
            }

            return false;
        }
    }
}
