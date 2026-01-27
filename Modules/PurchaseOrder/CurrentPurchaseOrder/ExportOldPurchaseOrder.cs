using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;

namespace VMS_Phase1PortalAT.Modules.PurchaseOrder.OldPurchaseOrder
{
    [TestClass]
    public class ExportOldPurchaseOrder
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
            options.AddUserProfilePreference("download.default_directory", downloadPath);
        }

        [TestMethod]
        public void ExportOldPurchaseOrderSuccess()
        {
            test = extent.CreateTest("Export Current Purchase Order");
            expectedStatus = "Passed";
            description = "Test case to verify export functionality in Old Purchase Order";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    LoginSuccess.LoginSuccessCompanyAdmin();
                    return driver.FindElements(By.Id("menuItem-Purchase Order")).Any();
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
                Console.WriteLine("Navigating to W. Transactions > Return Request...");
                IWebElement menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("menuItem-Purchase Order")));
                ClickElementWithRetries(driver, menu, "Main Menu");

                IWebElement submenu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("menuItem-Purchase Order1")));
                ClickElementWithRetries(driver, submenu, "Submenu - Return Request");
                // Ensure table is visible before proceeding
                wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
                Console.WriteLine("Return Request table loaded.");

                // Capture pre-download file list
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

                // Try finding the Export PO button
                By exportIconBy = By.XPath("(//td)[9]");
                IWebElement exportIcon = RetryFindElement(exportIconBy, wait);

                if (exportIcon != null)
                {
                    ScrollToElement(exportIcon);
                    exportIcon.Click();
                    Console.WriteLine("Clicked export icon.");

                    // Wait for file to download
                    bool fileDownloaded = WaitForDownloadToComplete(filesBeforeDownload);
                    Assert.IsTrue(fileDownloaded, "File was not downloaded successfully.");

                    Console.WriteLine("Download verified.");
                    test.Pass("Export completed and file downloaded successfully.");
                }
                else
                {
                    throw new Exception("Export icon not found or not clickable.");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("Test failed: " + errorMessage);
                test.Fail("Test failed: " + errorMessage);
                throw;
            }
        }

        // Helper: Scroll to element using JS
        private void ScrollToElement(IWebElement element)
        {
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'});", element);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ScrollToElement failed: " + ex.Message);
            }
        }

        // Helper: Retry finding element multiple times
        private IWebElement RetryFindElement(By by, WebDriverWait wait, int retries = 3)
        {
            for (int attempt = 0; attempt < retries; attempt++)
            {
                try
                {
                    var element = wait.Until(ExpectedConditions.ElementToBeClickable(by));
                    if (element.Displayed)
                        return element;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Retry {attempt + 1} failed: {ex.Message}");
                    Thread.Sleep(1000);
                }
            }
            return null!;
        }

        // Helper: Wait for file download completion
        private bool WaitForDownloadToComplete(List<string> filesBeforeDownload)
        {
            const int maxAttempts = 15;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                var filesAfterDownload = Directory.GetFiles(downloadPath).ToList();
                var newFiles = filesAfterDownload.Except(filesBeforeDownload).ToList();

                if (newFiles.Count >= 1)
                {
                    Console.WriteLine("New file(s) downloaded: " + string.Join(", ", newFiles));
                    return true;
                }

                Thread.Sleep(1000); // wait 1 sec
                attempts++;
            }

            Console.WriteLine("File download did not complete within timeout.");
            return false;
        }

        private static void ClickElementWithRetries(IWebDriver driver, IWebElement element, string label)
        {
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);
                    element.Click();
                    Console.WriteLine($"Clicked: {label}");
                    return;
                }
                catch (ElementClickInterceptedException)
                {
                    Console.WriteLine($"{label} click intercepted. Trying JS click.");
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"{label} became stale. Retrying...");
                    Thread.Sleep(500);
                    attempts++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Click failed on {label}: {ex.Message}");
                    Thread.Sleep(500);
                    attempts++;
                }
            }

            Console.WriteLine($"Failed to click {label} after retries.");
        }


        [TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;
            string formattedTime = $"{timeTaken.TotalSeconds:F2}";
            driver.Quit();
            testResult.WriteTestResults(TestContext, formattedTime, expectedStatus, errorMessage, description);
            extent.Flush();
        }

    }
}
