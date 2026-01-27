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

namespace VMS_Phase1PortalAT.Modules.Machines.CurrentStock
{
    [TestClass]
    public class ExportCurrentStock
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;

        private static ExtentReports extent;
        private static ExtentTest test;
        private readonly string systemName = Environment.UserName;
        private string downloadPath;
       
        public required TestContext TestContext { get; set; }
        private IWebDriver driver;
        private readonly setupData setupDatas = new setupData();
        private readonly WriteResultToCSV testResult = new WriteResultToCSV();
      

        [TestInitialize]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;

            downloadPath = setupDatas.downloadPath;

            downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", true);

            extent = ExtentManager.GetInstance();

          
        }

        [TestMethod]
        public void ExportCurrentStockSuccess()
        {
            test = extent.CreateTest("Validating test export currentStock data");
            expectedStatus = "Passed";
            description = "Test case to export currentStock data. Make sure downloadPath is set correctly in setupData.";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            try
            {
                LoginSuccess.LoginSuccessCompanyAdmin();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                }
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

                IWebElement menu = wait.Until(drv => drv.FindElement(By.Id("menuItem-Machines")));
                menu.Click();

                IWebElement submenu = wait.Until(drv => drv.FindElement(By.Id("menuItem-Machines1")));
                submenu.Click();

                Thread.Sleep(3000);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'cloud_download')]")));
                IWebElement exportButton = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]"));
                exportButton.Click();

                bool fileDownloaded = WaitForDownloadToComplete(filesBeforeDownload);
                Assert.IsTrue(fileDownloaded, "File was not downloaded successfully.");

                test.Pass("File was downloaded successfully.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Export failed: " + errorMessage);
                throw;
            }
        }

        private bool WaitForDownloadToComplete(List<string> filesBeforeDownload)
        {
            const int maxAttempts = 20; // increased attempts for safety
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                var filesAfterDownload = Directory.GetFiles(downloadPath).ToList();
                var newFiles = filesAfterDownload.Except(filesBeforeDownload).ToList();

                // Filter out incomplete files (.crdownload)
                var completedFiles = newFiles.Where(f => !f.EndsWith(".crdownload", StringComparison.OrdinalIgnoreCase)).ToList();

                if (completedFiles.Count >= 1)
                {
                    // Double-check if file exists physically before returning true
                    foreach (var file in completedFiles)
                    {
                        if (File.Exists(file))
                        {
                            return true;
                        }
                    }
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
            driver?.Quit();

            testResult.WriteTestResults(TestContext, formattedTime, expectedStatus, errorMessage, description);
            extent.Flush();
        }
    }
}
