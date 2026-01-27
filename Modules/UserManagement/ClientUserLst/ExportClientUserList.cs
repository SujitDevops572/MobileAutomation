using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;

namespace VMS_Phase1PortalAT.Modules.UserManagement.ClientUserLst
{
    [TestClass]
    public class ExportClientUserList
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;

        private readonly string systemName = Environment.UserName;
        private string downloadPath;
        private IWebDriver driver;

        public required TestContext TestContext { get; set; }

        private static ExtentReports extent;
        private static ExtentTest test;

        private readonly setupData setupDatas = new setupData();
        private readonly WriteResultToCSV testResult = new WriteResultToCSV();

        [TestInitialize]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;

            extent = ExtentManager.GetInstance();

            // Set up download path
            downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", true);

            test = ExtentManager.GetInstance().CreateTest(TestContext.TestName);

            stopwatch = Stopwatch.StartNew();

        }

        [TestMethod]
        public void ExportClientUserListSuccess()
        {
            test = extent.CreateTest("Export Client User List");
            expectedStatus = "Passed";
            description = "Test case to validate exporting of Client User List.";

            try
            {
                Login LoginSuccess = new Login();
                driver = LoginSuccess.getdriver();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                Console.WriteLine("🔐 Logging in...");
                LoginSuccess.LoginSuccessCompanyAdmin();

                wait.Until(d => d.FindElements(By.Id("menuItem-W. Transactions")).Any());
                Console.WriteLine("✅ Login successful and main menu is visible.");

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-User Management")));
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

                driver.FindElement(By.Id("menuItem-User Management")).Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-User Management1")));
                driver.FindElement(By.Id("menuItem-User Management1")).Click();

                Thread.Sleep(3000); // Wait for table to load
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));

                // Export button click
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'cloud_download')]")));
                driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]")).Click();

                bool fileDownloaded = WaitForDownloadToComplete(filesBeforeDownload);
                Assert.IsTrue(fileDownloaded, "File was not downloaded successfully.");

                test.Pass("File downloaded successfully.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("❌ Test failed: " + errorMessage);
                throw;
            }
        }

        [TestMethod]
        public void ExportCreditReport_ClientUserListSuccess()
        {
            test = extent.CreateTest("Export Credit Report in Client User List");
            expectedStatus = "Passed";
            description = "Test case to validate exporting of credit report from Client User List.";

            try
            {
                Login LoginSuccess = new Login();
                driver = LoginSuccess.getdriver();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                Console.WriteLine("🔐 Logging in...");
                LoginSuccess.LoginSuccessCompanyAdmin();

                wait.Until(d => d.FindElements(By.Id("menuItem-W. Transactions")).Any());
                Console.WriteLine("✅ Login successful and main menu is visible.");

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-User Management")));
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

                driver.FindElement(By.Id("menuItem-User Management")).Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-User Management1")));
                driver.FindElement(By.Id("menuItem-User Management1")).Click();

                Thread.Sleep(3000);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));

                // Credit report export button
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'request_quote')]")));
                driver.FindElement(By.XPath("//mat-icon[contains(text(),'request_quote')]")).Click();

                bool fileDownloaded = WaitForDownloadToComplete(filesBeforeDownload);
                Assert.IsTrue(fileDownloaded, "File was not downloaded successfully.");

                test.Pass("File downloaded successfully.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Test failed: " + errorMessage);
                throw;
            }
        }

        /// <summary>
        /// Waits up to 10 seconds for a new file to appear in the Downloads folder that is not a .crdownload.
        /// </summary>
        private bool WaitForDownloadToComplete(List<string> filesBeforeDownload)
        {
            const int maxAttempts = 10;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                var filesAfter = Directory.GetFiles(downloadPath).ToList();
                var newFiles = filesAfter.Except(filesBeforeDownload).ToList();

                if (newFiles.Any())
                {
                    bool allCompleted = newFiles.All(f => !f.EndsWith(".crdownload", StringComparison.OrdinalIgnoreCase));
                    if (allCompleted)
                        return true;
                }

                Thread.Sleep(1000);
                attempts++;
            }

            return false;
        }

        [TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            string formattedTime = $"{stopwatch.Elapsed.TotalSeconds:F2}";

            //ExtentManager.LogDuration(test, stopwatch.ElapsedMilliseconds);
            
            driver?.Quit();
            extent.Flush();
        }
    }
}
