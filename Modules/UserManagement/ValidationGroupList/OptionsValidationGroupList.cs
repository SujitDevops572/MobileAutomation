using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;

namespace VMS_Phase1PortalAT.Modules.UserManagement.ValidationGroupList
{
    [TestClass]
    public class OptionsValidationGroupList
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;

        public required TestContext TestContext { get; set; }

        private IWebDriver driver;
        private setupData setupDatas = new setupData();
        private WriteResultToCSV testResult = new WriteResultToCSV();

        private static ExtentReports extent;
        private static ExtentTest test;

        [TestInitialize]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;
            extent = ExtentManager.GetInstance();
        }

        [TestMethod]
        public void FlowValidationGroupList()
        {
            test = extent.CreateTest("Check the Validation Group List");

            expectedStatus = "Passed";
            description = "Test case to check the functionality flow of Validation Group List";

            // Initialize WebDriver
            Login loginHelper = new Login();
            driver = loginHelper.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                loginHelper.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                // Navigate to Validation Group List
                Console.WriteLine("Navigating to Validation Group List...");
                ClickElementWhenReady(wait, By.Id("menuItem-User Management"));
                ClickElementWhenReady(wait, By.Id("menuItem-User Management2"));

                WaitForTableToLoad(wait);

                // View option
                Console.WriteLine("Clicking Actions -> View...");
                ClickElementWhenReady(wait, By.XPath("(//td)[6]"));  // Actions cell
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[1]"));  // View
                ClickElementWhenReady(wait, By.XPath("//span[text()='Close']"));

                WaitForTableToLoad(wait);

                // Make Default
                Console.WriteLine("Clicking Actions -> Make Default...");
                ClickElementWhenReady(wait, By.XPath("(//td)[6]"));
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[3]"));
                Thread.Sleep(1000);  // If needed; try to replace with wait

                ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));

                WaitForTableToLoad(wait);

                //// Delete option
                //Console.WriteLine("Clicking Actions -> Delete...");
                //ClickElementWhenReady(wait, By.XPath("(//td)[6]"));
                //ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[4]"));
                //Thread.Sleep(1000);
                //ClickElementWhenReady(wait, By.XPath("//span[text()=' Confirm ']"));
                ////ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                //Thread.Sleep(3000);
                //Console.WriteLine("Flow completed successfully.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                }
                Console.WriteLine("Test failed with error: " + errorMessage);
                test.Fail(errorMessage);
                expectedStatus = "Failed";
                throw;
            }
            finally
            {
                // Some cleanup if needed (especially around HTTP or shared objects)
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;
            string formattedTime = $"{timeTaken.TotalSeconds:F2}";
            try
            {
                //driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error quitting driver: " + e.Message);
            }

            Console.WriteLine($"Test finished. Time taken: {formattedTime} seconds. Status: {expectedStatus}");

            testResult.WriteTestResults(TestContext, formattedTime, expectedStatus, errorMessage, description);
            extent.Flush();
        }

        private void ClickElementWhenReady(WebDriverWait wait, By locator)
        {
            wait.Until(driver =>
            {
                try
                {
                    // Wait until no overlay that blocks clicks
                    var overlays = driver.FindElements(By.CssSelector("div.overlay"));
                    if (overlays.Any(o => o.Displayed))
                    {
                        Console.WriteLine("Overlay present; waiting...");
                        return false;
                    }

                    var element = driver.FindElement(locator);

                    if (element != null && element.Displayed && element.Enabled)
                    {
                        element.Click();
                        Console.WriteLine($"Clicked element: {locator}");
                        return true;
                    }

                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"StaleElementReferenceException for {locator}; retrying...");
                    return false;
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"NoSuchElementException for {locator}; retrying...");
                    return false;
                }
                catch (ElementClickInterceptedException)
                {
                    Console.WriteLine($"ElementClickInterceptedException for {locator}; retrying...");
                    return false;
                }
            });
        }

        private IWebElement WaitForElementVisible(WebDriverWait wait, By locator)
        {
            return wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    if (element.Displayed)
                    {
                        Console.WriteLine($"Element visible: {locator}");
                        return element;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"StaleElementReferenceException in WaitForElementVisible for {locator}; retrying...");
                    return null;
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"NoSuchElementException in WaitForElementVisible for {locator}; retrying...");
                    return null;
                }
            });
        }

        private void WaitForTableToLoad(WebDriverWait wait)
        {
            Console.WriteLine("Waiting for table to load...");

            wait.Until(driver =>
            {
                try
                {
                    var tableRows = driver.FindElements(By.CssSelector("table tbody tr"));
                    if (tableRows.Count == 0)
                    {
                        Console.WriteLine("No rows found yet; waiting...");
                        return false;
                    }

                    // All rows must be displayed
                    foreach (var row in tableRows.ToList())
                    {
                        try
                        {
                            if (!row.Displayed)
                            {
                                Console.WriteLine("A row not displayed yet; waiting...");
                                return false;
                            }
                        }
                        catch (StaleElementReferenceException)
                        {
                            Console.WriteLine("Row became stale; will retry waiting for table...");
                            return false;
                        }
                    }

                    Console.WriteLine("Table loaded with rows displayed.");
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("StaleElementReferenceException while getting rows; retrying table load...");
                    return false;
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Table or rows not found; retrying table load...");
                    return false;
                }
            });
        }
    }
}
