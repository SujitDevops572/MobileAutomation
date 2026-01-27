using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using Docker.DotNet.Models;
using DocumentFormat.OpenXml.Bibliography;
using FlaUI.Core.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.UserManagement.ClientUserList;

namespace VMS_Phase1PortalAT.Modules.UserManagement.ClientUserLst
{
    [TestClass]
    public class VMSCredits
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
        public void SetValue()
        {
            test = extent.CreateTest("Check the Set Value Option in Client User List");

            expectedStatus = "Passed";
            description = "Test case to check the  Set Value Option in Client User List";

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
                ClickElementWhenReady(wait, By.Id("menuItem-User Management1"));

                WaitForTableToLoad(wait);
                ClickElementWhenReady(wait, By.XPath("(//td)[14]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[6]"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Update ']"));
                ClickElementWhenReady(wait, By.XPath("//mat-select[@name='valueOption']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Set Value ']"));
                SendKeysWhenReady(wait, By.XPath("//input[@name='valueOption']"), UpdateVMSCreditInfo.UpdateVMSCreditInfoSuccess["Set Value"]);
                ClickElementWhenReady(wait, By.XPath("(//span[text()=' Update '])[2]"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()='Close']"));
                Thread.Sleep(3000);

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
        }


        [TestMethod]
        public void CreditValue()
        {
            test = extent.CreateTest("Check the Credit Value Option in Client User List");

            expectedStatus = "Passed";
            description = "Test case to check the  Credit Value Option in Client User List";

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
                ClickElementWhenReady(wait, By.Id("menuItem-User Management1"));

                WaitForTableToLoad(wait);

                ClickElementWhenReady(wait, By.XPath("(//tbody/tr)[1]/td[last()]"));
                Thread.Sleep(3000);
                ClickElementWhenReady(wait, By.XPath("//div[contains(@class,'mat-menu-panel')]//button[contains(text(),'View VMS Credit Info')]"));

                ClickElementWhenReady(wait, By.XPath("//mat-dialog-container//span[contains(text(),'Update')]"));

                ClickElementWhenReady(wait, By.XPath("//mat-dialog-container//mat-select[contains(@name,'valueOption')]"));
                ClickElementWhenReady(wait, By.XPath("//div[contains(@id,'mat-select')]//mat-option//span[contains(text(),'Credit Value')]"));
                SendKeysWhenReady(wait, By.XPath("//div[contains(@class,'mat-form-field')]//div[contains(@id,'mat-select')]//input[@name='valueOption']"), UpdateVMSCreditInfo.UpdateVMSCreditInfoSuccess["Credit Value"]);
                ClickElementWhenReady(wait, By.XPath("(//span[text()=' Update '])[2]"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()='Close']"));


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
        }


        [TestMethod]
        public void DebitValue()
        {
            test = extent.CreateTest("Check the Debit Value Option in Client User List");

            expectedStatus = "Passed";
            description = "Test case to check the  Debit Value Option in Client User List";

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
                ClickElementWhenReady(wait, By.Id("menuItem-User Management1"));

                WaitForTableToLoad(wait);

                WaitForTableToLoad(wait);
                ClickElementWhenReady(wait, By.XPath("(//td)[14]"));
                Thread.Sleep(3000);
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[6]"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Update ']"));
                ClickElementWhenReady(wait, By.XPath("//mat-select[@name='valueOption']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Debit Value ']"));
                SendKeysWhenReady(wait, By.XPath("//input[@name='valueOption']"), UpdateVMSCreditInfo.UpdateVMSCreditInfoSuccess["Debit Value"]);
                ClickElementWhenReady(wait, By.XPath("(//span[text()=' Update '])[2]"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()='Close']"));

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
        }









        [TestCleanup]
            public void Cleanup()
            {
                stopwatch.Stop();
                TimeSpan timeTaken = stopwatch.Elapsed;
                string formattedTime = $"{timeTaken.TotalSeconds:F2}";
                try
                {
                    driver.Quit();
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
             private void SendKeysWhenReady(WebDriverWait wait, By locator, string textToEnter)
        {
            wait.Until(driver =>
            {
                try
                {
                    // Wait for overlays to disappear
                    var overlays = driver.FindElements(By.CssSelector("div.overlay"));
                    if (overlays.Any(o => o.Displayed))
                    {
                        Console.WriteLine("Overlay detected; waiting...");
                        return false;
                    }

                    var element = driver.FindElement(locator);
                    if (element.Displayed && element.Enabled)
                    {
                        element.Clear();
                        element.SendKeys(textToEnter);
                        Console.WriteLine($"Sent keys to element {locator}: {textToEnter}");
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
            });
        }
    }
    }