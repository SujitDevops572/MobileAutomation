using AventStack.ExtentReports;
using Docker.DotNet.Models;
using DocumentFormat.OpenXml.Bibliography;
using FlaUI.Core.Input;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls.datas.Transactions.Sales;

namespace VMS_Phase1PortalAT.utls.utilMethods
{
    public class Search_Sales
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

                WaitUntilOverlayDisappears(driver);

                Dateselection(driver);

                By searchTypeBoxBy = By.ClassName("searchTypeBox");
                By searchInputBy = By.Name("searchText");
                By closeMarkBy = By.XPath("//mat-chip//mat-icon[contains(text(),'cancel')]"); // adjust selector as needed

                foreach (var kvp in dict)
                {
                    string searchType = kvp.Key;
                    string expectedValue = kvp.Value;

                    Console.WriteLine($"----- Searching for Type: '{searchType}' with Value: '{expectedValue}' -----");

                    try
                    {
                        // --- Click the close mark before entering new search ---
                        var closeElements = driver.FindElements(closeMarkBy);
                        if (closeElements.Any(e => e.Displayed))
                        {
                            SafeClickJS(driver, closeElements.First(e => e.Displayed));
                            Console.WriteLine("[INFO] Clicked close mark to clear previous search.");
                            Thread.Sleep(500);
                        }

                        // --- Select search type ---
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


                        Console.WriteLine($"[INFO] Waiting for search input to be clickable...");
                        RetryFind(driver, searchInputBy, wait);

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

                        WaitUntilOverlayDisappears(driver);

                        Thread.Sleep(1000);
                        var td1 = driver.FindElement(By.XPath("(//td)[1]"));
                        td1.Click();
                        Thread.Sleep(200);

                        var td2 = driver.FindElement(By.XPath("(//td)[2]"));
                        td2.Click();
                        Thread.Sleep(1000);

                        // ========== GET MATCHING HEADER COLUMN ==========

                        var thead = driver.FindElement(By.TagName("thead"));
                        var headerColumns = thead.FindElements(By.TagName("th"));

                        int matchingColumnIndex = -1;

                        // Find the TH whose text matches the search keyword
                        for (int i = 0; i < headerColumns.Count; i++)
                        {
                            string headerText = headerColumns[i].Text.Trim();

                            if (headerText.Equals(searchType, StringComparison.OrdinalIgnoreCase))
                            {
                                matchingColumnIndex = i;
                                Console.WriteLine($"[INFO] Matching header found: '{headerText}' at column {i}");
                                break;
                            }
                        }

                        if (matchingColumnIndex == -1)
                        {
                            test.Fail($"No header found matching search type '{searchType}'");
                            Console.WriteLine($"[FAIL] No matching header for '{searchType}'");
                            continue;
                        }

                        // ========== VALIDATE ROW DATA FROM THAT COLUMN ==========

                        IWebElement tbody = driver.FindElement(By.TagName("tbody"));
                        var rows = tbody.FindElements(By.TagName("tr"));

                        bool keywordFound = false;

                        foreach (var row in rows)
                        {
                            var cells = row.FindElements(By.TagName("td"));

                            if (cells.Count <= matchingColumnIndex)
                                continue; 

                            string cellText = cells[matchingColumnIndex].Text.Trim();
                            Console.WriteLine($"Checking row cell: {cellText}");

                            if (cellText.Contains(expectedValue, StringComparison.OrdinalIgnoreCase))
                            {
                                keywordFound = true;
                                Console.WriteLine(cellText+ " = " +expectedValue);
                                break;
                            }
                        }

                        if (keywordFound)
                        {
                            test.Pass($"Value '{expectedValue}' found under column '{searchType}'");
                            Console.WriteLine($"[PASS] '{expectedValue}' found in matched column.");
                        }
                        else
                        {
                            test.Fail($"Value '{expectedValue}' NOT found under column '{searchType}'");
                            Console.WriteLine($"[FAIL] '{expectedValue}' not found in matched column.");
                        }






                        //By tbodyBy = By.TagName("tbody");
                        //wait.Until(ExpectedConditions.ElementIsVisible(tbodyBy));

                        //bool stable = WaitForStableRowCount(driver, tbodyBy, 10, 500);
                        //if (!stable)
                        //{
                        //    Console.WriteLine("[WARN] Row count did not stabilize after search.");
                        //}

                        //IWebElement tbody = RetryFind(driver, tbodyBy, wait);
                        //var rows = tbody.FindElements(By.TagName("tr"));
                        //Console.WriteLine($"[INFO] Row count after search: {rows.Count}");

                        //if (rows.Count == 0)
                        //{
                        //    test.Fail($"No results found for '{searchType}' with value '{expectedValue}'");
                        //    Console.WriteLine($"[FAIL] No results found for '{searchType}' = '{expectedValue}'");
                        //    continue;
                        //}

                        //string actualValue = driver.FindElement(searchInputBy).GetAttribute("value") ?? "";
                        //if (actualValue == expectedValue)
                        //{
                        //    test.Pass($"Search successful for '{searchType}' with value '{expectedValue}'");
                        //    Console.WriteLine($"[PASS] Search matched for '{searchType}' = '{actualValue}'");
                        //}
                        //else
                        //{
                        //    test.Fail($"Search value mismatch for '{searchType}'. Expected: '{expectedValue}', Found: '{actualValue}'");
                        //    Console.WriteLine($"[FAIL] Mismatch for '{searchType}'. Expected: '{expectedValue}', Found: '{actualValue}'");
                        //}


                        // --- Click close mark after search ---
                        closeElements = driver.FindElements(closeMarkBy);
                        if (closeElements.Any(e => e.Displayed))
                        {
                            SafeClickJS(driver, closeElements.First(e => e.Displayed));
                            Console.WriteLine("[INFO] Clicked close mark after search to clear input.");
                            Thread.Sleep(300);
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
        private static void Dateselection(IWebDriver driver) {

            IWebElement dateRangeBtn = driver.FindElement(By.XPath("//mat-icon[contains(text(),'date_range')]"));
            dateRangeBtn.Click();
            Thread.Sleep(2000);

            //DateRange Selection
            IWebElement calenderclick = driver.FindElement(By.XPath("(//button[contains(@class,'owl-dt-control')])[2]"));
            calenderclick.Click();
            Thread.Sleep(400);
            IWebElement yearselect = driver.FindElement(By.XPath("(//span[contains(text(),'2025')])[2]"));
            yearselect.Click();
            Thread.Sleep(400);
            IWebElement monthselect = driver.FindElement(By.XPath("//span[contains(text(),'Oct')]"));
            monthselect.Click();
            Thread.Sleep(400);

            IWebElement month1 = driver.FindElement(By.TagName("owl-date-time-month-view"));

            IWebElement dateElement = month1.FindElement(By.ClassName("owl-dt-calendar-body"));
            IList<IWebElement> dateElements = dateElement.FindElements(By.TagName("td"));

            foreach (IWebElement datElement in dateElements)
            {
                if (datElement.Text.Equals(SearchSalesData.searchDate["startDate"]))
                {
                    datElement.Click();

                }
                if (datElement.Text.Equals(SearchSalesData.searchDate["endDate"]))
                {
                    datElement.Click();
                    break;
                }
            }
            Thread.Sleep(1000);

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
        private static void WaitUntilOverlayDisappears(IWebDriver driver, int maxAttempts = 5)
        {
            // Angular CDK Overlay XPaths
            By overlayBy = By.XPath("//div[contains(@class, 'cdk-overlay-backdrop') or contains(@class,'cdk-overlay-pane') or contains(@class,'cdk-overlay-container')]");

            IWebElement overlayElement = null;
            int attempt = 0;

            while (attempt < maxAttempts)
            {
                attempt++;

                try
                {
                    // Try to locate Angular CDK overlay element
                    overlayElement = driver.FindElement(overlayBy);

                    if (overlayElement != null && overlayElement.Displayed)
                    {
                        Console.WriteLine($"[INFO] Angular overlay detected (attempt {attempt}). Waiting for disappearance...");

                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));

                        try
                        {
                            // Wait up to 4 seconds for the overlay to disappear
                            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(overlayBy));
                            Console.WriteLine("[INFO] Overlay disappeared.");
                            return;
                        }
                        catch (WebDriverTimeoutException)
                        {
                            Console.WriteLine("[INFO] Overlay still visible. Retrying...");
                        }
                    }
                }
                catch (NoSuchElementException)
                {
                    // Overlay not present → safe to continue
                    Console.WriteLine("[INFO] No overlay found. Continuing execution...");
                    return;
                }
            }

            // After max attempts overlay still exists
            Console.WriteLine("[WARN] Overlay did not disappear after max attempts. Moving on...");
        }






    }
}
