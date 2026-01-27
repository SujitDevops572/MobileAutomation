using AventStack.ExtentReports;
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
using VMS_Phase1PortalAT.utls.datas.UserManagement.ValidationGroupList;

namespace VMS_Phase1PortalAT.Modules.UserManagement.ClientUserLst
{
    [TestClass]
    public class OptionsClientUserList
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
        public void BlockClientUserList()
        {
            test = extent.CreateTest("Check the Block Client User List Option");

            expectedStatus = "Passed";
            description = "Test case to check the Block client of Client User List";

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
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[2]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Confirm ']"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                Thread.Sleep(2000);
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
        public void UnBlockClientUserList()
        {
            test = extent.CreateTest("Check the UnBlock Client User List Option");

            expectedStatus = "Passed";
            description = "Test case to check the UnBlock Client User List Option";

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
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[2]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Confirm ']"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                Thread.Sleep(2000);
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
        public void ViewClientUserList()
        {
            test = extent.CreateTest("Check the View Client User List Option");

            expectedStatus = "Passed";
            description = "Test case to check the  View Client User List Option";

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
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[5]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("//span[text()='Close']"));
                Thread.Sleep(2000);
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
        public void DeleteClientUserList()
        {
            test = extent.CreateTest("Check the Delete Client User List Option");

            expectedStatus = "Passed";
            description = "Test case to check the Delete Client User List";

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

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                Thread.Sleep(1000);
                select.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                searchOptions[0].Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                searchInput.Clear();
                searchInput.SendKeys(AddClientUserListData.AddClientUserListSuccess["Name"]);
                searchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);


                ClickElementWhenReady(wait, By.XPath("(//tbody/tr)[1]/td[last()]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[4]"));
                Thread.Sleep(2000);

                ClickElementWhenReady(wait, By.XPath("//span[text()=' Confirm ']"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));

                var snackbarText = wait.Until(drv =>
                {
                    var el = drv.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    return el.Displayed && !string.IsNullOrWhiteSpace(el.Text)
                           ? el.Text
                           : null;
                });

                Console.WriteLine(snackbarText);

                Assert.IsTrue(snackbarText.Contains("Client User Deleted"), "Not Deleted!!!");
                Thread.Sleep(1500);        

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                IWebElement vselect = driver.FindElement(By.ClassName("searchTypeBox"));
                Thread.Sleep(1000);
                vselect.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                IList<IWebElement> vsearchOptions = driver.FindElements(By.TagName("mat-option"));
                vsearchOptions[0].Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                IWebElement vsearchInput = driver.FindElement(By.Name("searchText"));
                vsearchInput.Clear();
                vsearchInput.SendKeys(AddClientUserListData.AddClientUserListSuccess["VName"]);
                vsearchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);


                ClickElementWhenReady(wait, By.XPath("(//tbody/tr)[1]/td[last()]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[4]"));
                Thread.Sleep(2000);

                ClickElementWhenReady(wait, By.XPath("//span[text()=' Confirm ']"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                Thread.Sleep(2000);

                var snackbarText1 = wait.Until(drv =>
                {
                    var el = drv.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    return el.Displayed && !string.IsNullOrWhiteSpace(el.Text)
                           ? el.Text
                           : null;
                });

                Console.WriteLine(snackbarText1);
                Assert.IsTrue(snackbarText1.Contains("Client User Deleted"), "Not Deleted!!!");

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


     
        public void DeleteValidationClientUserList()
        {
            test = extent.CreateTest("Check the Delete Validation Client User List Option");

            expectedStatus = "Passed";
            description = "Test case to check the Delete Validation Client User List";

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

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                Thread.Sleep(1000);
                select.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                searchOptions[0].Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                searchInput.Clear();
                searchInput.SendKeys(AddClientUserListData.AddClientUserListSuccess["VName"]);
                searchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);


                ClickElementWhenReady(wait, By.XPath("(//tbody/tr)[1]/td[last()]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[4]"));
                Thread.Sleep(2000);

                ClickElementWhenReady(wait, By.XPath("//span[text()=' Confirm ']"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));

                var snackbarText = wait.Until(drv =>
                {
                    var el = drv.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    return el.Displayed && !string.IsNullOrWhiteSpace(el.Text)
                           ? el.Text
                           : null;
                });

                Console.WriteLine(snackbarText);

                Assert.IsTrue(snackbarText.Contains("Client User Deleted"), "Not Deleted!!!");

                Thread.Sleep(2000);
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
        public void VMSCreditInfo_ClientUserList()
        {
            test = extent.CreateTest("Check the VMS Credit Info in Client User List");

            expectedStatus = "Passed";
            description = "Test case to check the VMS Credit Info in Client User List";

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
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("//button[contains(text(),'View VMS Credit Info')]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("//span[text()='Close']"));

                Thread.Sleep(2000);
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
        public void VMSLoyaltyPoint_ClientUserList()
        {
            test = extent.CreateTest("Check the VMS Loyalty Point in Client User List");

            expectedStatus = "Passed";
            description = "Test case to check the VMS Loyalty Point in Client User List";

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
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("//button[contains(text(),'Loyalty Point Info')]"));
                Thread.Sleep(2000);

                ClickElementWhenReady(wait, By.XPath("//span[text()='Close']"));

                Thread.Sleep(2000);
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
        public void EditAccessId_ClientUserList()
        {
            test = extent.CreateTest("Check the Edit Access Id in Client User List Option");

            expectedStatus = "Passed";
            description = "Test case to check the Edit Access Id in Client User List Option";

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
                Console.WriteLine("Navigating to Client User List...");
                ClickElementWhenReady(wait, By.Id("menuItem-User Management"));
                ClickElementWhenReady(wait, By.Id("menuItem-User Management1"));

                WaitForTableToLoad(wait);

                ClickElementWhenReady(wait, By.XPath("(//td)[14]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[1]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("//textarea[@name='accessId']"));
                SendKeysWhenReady(wait, By.XPath("//textarea[@name='accessId']"), SearchClientUserListData.EditAccessId_ClientUserListFailure["Access id"]);
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Save ']"));
                Thread.Sleep(100);
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));

                var snackbarText = wait.Until(drv =>
                {
                    var el = drv.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    return el.Displayed && !string.IsNullOrWhiteSpace(el.Text)
                           ? el.Text
                           : null;
                });

                Console.WriteLine(snackbarText);

                Assert.IsTrue(snackbarText.Contains("AccessId updated"), "Not Edited!!!");

                Thread.Sleep(500);

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

