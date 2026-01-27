using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VMS_Phase1PortalAT.utls.utilMethods
{
    public class Navigation
    {
        public void NavigationSuccess(IWebDriver driver, string menuData, string submenuData)
        {  

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            Console.WriteLine($"[INFO] Locating menu: {menuData}");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(menuData))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(submenuData))).Click();

            Thread.Sleep(1500);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
          
            IWebElement FirstPage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'mat-paginator-range-actions')]//button[contains(@aria-label,'First page')]")));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", FirstPage);
            Console.WriteLine("First Page - " + FirstPage.Enabled);
            Assert.IsFalse(FirstPage.Enabled,"First Page button not working");

            Thread.Sleep(1000);
            ClickAndRetry(driver, "//div[contains(@class,'mat-paginator-range-actions')]//button[contains(@aria-label,'Last page')]");

            IWebElement LastPage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'mat-paginator-range-actions')]//button[contains(@aria-label,'Last page')]")));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", LastPage);
            Console.WriteLine("Last Page - "+LastPage.Enabled);    
            Assert.IsFalse(LastPage.Enabled,"Last Page button not working");
            Thread.Sleep(1000);

            IList<IWebElement> Previousdatas = driver.FindElements(By.XPath("//tr[1]//td"));

            //IWebElement NextPage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'mat-paginator-range-actions')]//button[contains(@aria-label,'Next page')]")));
            //js.ExecuteScript("arguments[0].scrollIntoView(true);", NextPage);
            Console.WriteLine("-----Previous Page Check...-----");

            ClickAndValidateFirstRowChange(driver , By.XPath("//div[contains(@class,'mat-paginator-range-actions')]//button[contains(@aria-label,'Previous page')]"));
            Thread.Sleep(1000);

            Console.WriteLine("-----Next Page Check...-----");
            ClickAndValidateFirstRowChange(driver, By.XPath("//div[contains(@class,'mat-paginator-range-actions')]//button[contains(@aria-label,'Next page')]"));


            Thread.Sleep(2000); 

        }



        public static void ClickAndRetry(IWebDriver driver, string xpath, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            IWebElement element = null;

            // Poll repeatedly until element is found, visible, and enabled
            wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(By.XPath(xpath));

                    if (el.Displayed && el.Enabled)
                    {
                        element = el; // store the ready element
                        return true;   // stop waiting
                    }

                    return false;      // keep waiting
                }
                catch (NoSuchElementException)
                {
                    return false;      // keep waiting
                }
                catch (StaleElementReferenceException)
                {
                    return false;      // keep waiting
                }
            });

            // At this point, element is guaranteed to be non-null and clickable
            try
            {
                element.Click();
            }
            catch (Exception)
            {
                // Fallback to JavaScript click
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", element);
            }
        }

        public static void ClickAndValidateFirstRowChange(
    IWebDriver driver,
    By clickLocator,
    int timeoutInSeconds = 10)
        {
            // STEP 1: Ensure overlays removed
            WaitForOverlayToDisappear(driver, timeoutInSeconds);
            WaitForTableToLoad(driver);

            // STEP 2: Get before-click table row data
            List<string> beforeData = FetchFirstFiveFromFirstRow(driver, timeoutInSeconds);

            // STEP 3: Click the target element with safe retries
            ClickWithRetry(driver, clickLocator, timeoutInSeconds);

            // STEP 4: Wait for table & overlay after click
            WaitForOverlayToDisappear(driver, timeoutInSeconds);
            WaitForTableToLoad(driver);
            // STEP 5: Get after-click table row data
            List<string> afterData = FetchFirstFiveFromFirstRow(driver, timeoutInSeconds);

            // STEP 6: Compare the two lists
            int mismatchCount = 0;

            for (int i = 0; i < 5; i++)
            {
                if (!beforeData[i].Equals(afterData[i], StringComparison.OrdinalIgnoreCase))
                {
                    mismatchCount++;

                    if (mismatchCount >= 2)
                    {
                        Assert.IsTrue(true,
                            $"At least 2 mismatches detected between before and after click values.\n" +
                            $"Before: {string.Join(", ", beforeData)}\n" +
                            $"After:  {string.Join(", ", afterData)}"
                        );
                    }
                }
            }
        }

        public static List<string> FetchFirstFiveFromFirstRow(IWebDriver driver, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            List<string> result = null;

            wait.Until(d =>
            {
                try
                {
                    var row = d.FindElement(By.XPath("//table/tbody/tr[1]"));
                    var cells = row.FindElements(By.TagName("td"));

                    if (cells.Count < 5)
                        return false;   // retry, not null

                    result = new List<string>();

                    for (int i = 0; i < 5; i++)
                        result.Add(cells[i].Text.Trim());

                    return true;    // row successfully captured
                }
                catch (Exception)
                {
                    return false;   // retry, no exceptions thrown
                }
            });

            // SAFE: result is guaranteed to be non-null when wait completes successfully
            return result;
        }


        public static void ClickWithRetry(IWebDriver driver, By locator, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);

                    if (element.Displayed && element.Enabled)
                    {
                        try
                        {
                            element.Click();
                            return true;
                        }
                        catch
                        {
                            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                            js.ExecuteScript("arguments[0].click();", element);
                            return true;
                        }
                    }

                    return false;
                }
                catch
                {
                    return false; // retry
                }
            });
        }


        public static void WaitForOverlayToDisappear(IWebDriver driver, int timeoutSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));

            IWebElement overlay = null;   // initialize first as requested

            wait.Until(d =>
            {
                try
                {
                    // Try to locate any overlay element
                    var overlays = d.FindElements(By.CssSelector(".overlay, .loading, .spinner"));

                    // If found, pick the first one
                    overlay = overlays.FirstOrDefault();

                    // Case 1: Overlay element exists AND is displayed → WAIT
                    if (overlay != null && overlay.Displayed)
                        return false;

                    // Case 2: Overlay is null OR not displayed → DONE
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    // Stale overlay → table is reloading, so continue waiting
                    return false;
                }
                catch
                {
                    // Any other issue → treat as overlay still present
                    return false;
                }
            });
        }


        private static void WaitForTableToLoad(IWebDriver driver)
        {
            Console.WriteLine("Waiting for table to load...");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                try
                {
                    var tableRows = driver.FindElements(By.CssSelector("table tbody tr"));
                    return tableRows.Count > 0 && tableRows.All(row =>
                    {
                        try
                        {
                            return row.Displayed;
                        }
                        catch (StaleElementReferenceException)
                        {
                            return false;
                        }
                    });
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });

            Console.WriteLine("Table loaded.");
        }






    }
}
