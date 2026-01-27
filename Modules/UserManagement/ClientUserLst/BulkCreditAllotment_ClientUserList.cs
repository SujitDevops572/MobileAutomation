using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
using WindowsInput;
using WindowsInput.Native;

namespace VMS_Phase1PortalAT.Modules.UserManagement.ClientUserLst
{
    [TestClass]
    [Ignore("Temporarily disabled.")]
    public class BulkCreditAllotment_ClientUserList
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        static string systemName = Environment.UserName;
        private string downloadPath = $"C:\\Users\\{systemName}\\Downloads";
        public required TestContext TestContext { get; set; }
        IWebDriver driver;
        setupData setupDatas = new setupData();
        WriteResultToCSV testResult = new WriteResultToCSV();

        private static ExtentReports extent;
        private static ExtentTest test;

        [TestInitialize]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;

            extent = ExtentManager.GetInstance();
            var options = new ChromeOptions();
            string systemName = Environment.UserName;
            Console.WriteLine(systemName);
            options.AddUserProfilePreference("download.default_directory", downloadPath);
        }

        [TestMethod]
        public void BulkCredit_ClientUserListSuccess()
        {
            test = extent.CreateTest("Bulk Credit Allotment");

            expectedStatus = "Passed";
            description = "test case to test export Bulk Credit Allotment ";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    LoginSuccess.LoginSuccessCompanyAdmin();
                    return driver.FindElements(By.Id("menuItem-W. Transactions")).Any();
                });

                Console.WriteLine("Login successful and main menu is visible.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Login failed: " + errorMessage);
                throw;
            }

            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-User Management")));
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

                IWebElement menu = driver.FindElement(By.Id("menuItem-User Management"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-User Management1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-User Management1"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        Thread.Sleep(3000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        ClickElementWhenReady(wait, By.XPath("//mat-icon[text()='payments']"));
                        

                        // Wait for the export button to be clickable
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='Client']")));
                        IWebElement bulkButton = driver.FindElement(By.XPath("//input[@placeholder='Client']"));
                        bulkButton.Click();
                        Thread.Sleep(3000);

                        ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Client']"));
                        ClickElementWhenReady(wait, By.XPath("//span[text()=' A2B Test Client ']"));
                        Thread.Sleep(1000);
                        ClickElementWhenReady(wait, By.XPath("//mat-select[@name='ignoreOtherExisting']"));
                        ClickElementWhenReady(wait, By.XPath("//span[text()=' Ignore ']"));
                        ClickElementWhenReady(wait, By.XPath("//span[text()=' Select File ']"));
                        Thread.Sleep(4000);
                        
                        var sim = new InputSimulator();
                        sim.Keyboard.TextEntry(@"C:\Users\Sujit-PC\Downloads\Sample Bulk Recharge_at_2025-09-20 04_38_58.xlsx");
                        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN); // RETURN is same as ENTER
                        Console.WriteLine("Uploaded a file");
                        Thread.Sleep(2000);

                        ClickElementWhenReady(wait, By.XPath("//span[text()=' Validate ']"));
                        
                        ClickElementWhenReady(wait, By.XPath("//span[text()=' Save ']"));
                        Thread.Sleep(3000);
                        //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));

                        //ClickElementWhenReady(wait, By.XPath("//span[text()=' Remove ']"));
                    }
                }
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
        public void BulkCredit_GetSample_ClientUserList()
        {
            test = extent.CreateTest("Bulk Credit_Get Sample in Client User List");

            expectedStatus = "Passed";
            description = "Test case to test Bulk Credit_Get Sample functionality";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            try
            {
                Console.WriteLine("Logging in...");

                wait.Until(driver =>
                {
                    LoginSuccess.LoginSuccessCompanyAdmin();
                    return driver.FindElements(By.Id("menuItem-W. Transactions")).Any();
                });

                Console.WriteLine("Login successful and main menu is visible.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Login failed: " + errorMessage);
                throw;
            }

            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-User Management")));

                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

                // Open User Management > Client User List
                IWebElement menu = driver.FindElement(By.Id("menuItem-User Management"));
                menu?.Click();

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-User Management1")));
                IWebElement submenu = driver.FindElement(By.Id("menuItem-User Management1"));
                submenu?.Click();

                Thread.Sleep(3000); // Replace with better wait if possible

                // Click on Bulk Credit (payments icon)
                IWebElement BulkCredit = driver.FindElement(By.XPath("//mat-icon[text()='payments']"));
                BulkCredit?.Click();

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("tbody")));

                // Wait for 'Get Sample' button to be present and scroll into view
                By getSampleButtonLocator = By.XPath("//span[text()=' Get Sample ']");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(getSampleButtonLocator));

                IWebElement export = driver.FindElement(getSampleButtonLocator);

                // Scroll to the element to ensure visibility
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", export);
                Thread.Sleep(500); // Allow scroll to finish

                // Wait until it's clickable and no other elements are overlapping
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(getSampleButtonLocator));

                try
                {
                    export.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    // Fallback: use JavaScript click if intercepted
                    Console.WriteLine("Click intercepted, using JavaScript click as fallback.");
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", export);
                }

                Thread.Sleep(6000); // Wait for download to complete (improve with dynamic wait)

                bool fileDownloaded = WaitForDownloadToComplete(filesBeforeDownload);
                Assert.IsTrue(fileDownloaded, "File was not downloaded successfully.");

                test.Pass();
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
        private bool WaitForDownloadToComplete(List<string> filesBeforeDownload)
        {
            const int maxAttempts = 10;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                var filesAfterDownload = Directory.GetFiles(downloadPath).ToList();
                var newFiles = filesAfterDownload.Except(filesBeforeDownload).ToList();

                if (newFiles.Count == 1) // Check that exactly one new file has been downloaded
                {
                    return true;
                }

                Thread.Sleep(1000); // Wait 1 second before checking again
                attempts++;
            }

            return false; // Return false if the file was not downloaded within the max attempts
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
    }
}
        
        
