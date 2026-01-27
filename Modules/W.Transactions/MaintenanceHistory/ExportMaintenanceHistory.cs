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

namespace VMS_Phase1PortalAT.Modules.W.Transactions.MaintenanceHistory
{
    [TestClass]
    public class ExportMaintenanceHistory
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
        public void ExportMaintenanceHistorySuccess()
        {
            test = extent.CreateTest("Export Maitenanace History Request");

            expectedStatus = "Passed";
            description = "test case to test export Maitenanace History Request ";

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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions")));
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

                IWebElement menu = driver.FindElement(By.Id("menuItem-W. Transactions"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions5")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions5"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));

                        // Wait for the export button to be clickable
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'cloud_download')]")));

                        IWebElement export = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]"));
                        export.Click();

                        // Use a dynamic wait for the file to appear in the Downloads folder
                        bool fileDownloaded = WaitForDownloadToComplete(filesBeforeDownload);
                        Assert.IsTrue(fileDownloaded, "File was not downloaded successfully.");

                        test.Pass();
                    }
                    else
                    {
                        Console.WriteLine("Submenu not found.");
                        test.Skip();
                    }
                }
                else
                {
                    Console.WriteLine("Menu item not found.");
                    test.Skip();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                    test.Fail(errorMessage);
                }
                throw;
            }
        }

        // Helper method to wait for the file download to complete
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

        
        public void InfoButtonmaintenanceHistory()
        {

           test = extent.CreateTest("Export Maintenance History Success");

            expectedStatus = "Passed";
            description = "test case to test Export Maintenance History ";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions")));
                Thread.Sleep(3000);
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();
                IWebElement menu = driver.FindElement(By.Id("menuItem-W. Transactions"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions5")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions5"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text() = 'more_vert']")));
                        IWebElement InfoOptions = driver.FindElement(By.XPath("//mat-icon[text() = 'more_vert']"));
                        InfoOptions.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()=\" Info \"]")));
                        IWebElement Info = driver.FindElement(By.XPath("//button[text()=\" Info \"]"));
                        Info.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Close']")));
                        IWebElement close = driver.FindElement(By.XPath("//span[text()='Close']"));
                        Thread.Sleep(1000);
                        close.Click();
                        Thread.Sleep(1000);
                    }
                }
            }

            catch(Exception ex) { string error =ex.InnerException.StackTrace;Console.WriteLine(error); }   
            
            }
                    [TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;
            String formatedTime = $"{timeTaken.TotalSeconds:F2}";
            driver.Quit();
            testResult.WriteTestResults(TestContext, formatedTime, expectedStatus, errorMessage, description);

            extent.Flush();
        }
    }
}

