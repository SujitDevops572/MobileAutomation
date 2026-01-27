using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
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

namespace VMS_Phase1PortalAT.Modules.Transaction.Sales
{
    [TestClass]
    public class ExportSales
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
        public void ExportSalesSummarySuccess()
        {
            expectedStatus = "Passed";
            description = "Test case to test export sales data";

            test = extent.CreateTest("ExportSalesSummarySuccess");

            Login loginPage = new Login();
            driver = loginPage.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                // Login
                loginPage.LoginSuccessCompanyAdmin();
                test.Pass("Login successful and dashboard loaded.");

                // Capture files before download
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

                // Navigate to Transactions menu
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Transactions")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Transactions"));
                menu.Click();

                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Transactions0")));
                IWebElement submenu = driver.FindElement(By.Id("menuItem-Transactions0"));
                submenu.Click();

                // Wait for table to load
                wait.Until(ExpectedConditions.ElementExists(By.TagName("tbody")));
                IWebElement table = driver.FindElement(By.TagName("tbody"));
                IList<IWebElement> rows = table.FindElements(By.TagName("tr"));

                // Wait for overlay to disappear
                By overlayBy = By.CssSelector("div.overlay.ng-animating, div.overlay");
                try
                {
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(overlayBy));
                    Console.WriteLine("[INFO] Overlay disappeared.");
                }
                catch
                {
                    Console.WriteLine("[WARN] Overlay did not disappear in time. Trying JS click anyway...");
                }

                // Click export icon safely
                By exportIconBy = By.XPath("//mat-icon[contains(text(),'cloud_download')]");

                wait.Until(ExpectedConditions.ElementExists(exportIconBy));
                IWebElement export = wait.Until(ExpectedConditions.ElementToBeClickable(exportIconBy));

                // Use JS click to bypass Angular overlay issues
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", export);

                Console.WriteLine("[INFO] Export clicked successfully.");


                // Select radio button and checkbox
                wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("mat-radio-button")));
                IList<IWebElement> radioButtons = driver.FindElements(By.ClassName("mat-radio-button"));
                radioButtons[0].Click();

                wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("mat-checkbox")));
                IList<IWebElement> checkBoxes = driver.FindElements(By.ClassName("mat-checkbox"));
                checkBoxes[0].Click();

                // Click options: Invoice Document + Session Log
                IWebElement invoiceDoc = driver.FindElement(By.XPath("//span[contains(text(),'Invoice Document')]"));
                invoiceDoc.Click();

                IWebElement sessionLog = driver.FindElement(By.XPath("//span[contains(text(),'Session Log')]"));
                sessionLog.Click();

                // Wait until loading overlay disappears before clicking download
                WaitUntilOverlayDisappears(wait);

                // Locate download button and click
                wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("rowReverseHolder")));
                IWebElement downloadContainer = driver.FindElement(By.ClassName("rowReverseHolder"));
                IWebElement downloadButton = downloadContainer.FindElement(By.ClassName("mat-primary"));

                // Use JavaScript click to avoid interception issues
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", downloadButton);

                test.Info("Clicked download button.");

                Thread.Sleep(1500);
                // Wait for download to complete
                string downloadedFile = WaitForDownload(downloadPath, filesBeforeDownload, 60);
                test.Pass("Downloaded file: " + downloadedFile);

                stopwatch.Stop();
                test.Info($"Test duration: {stopwatch.Elapsed.TotalSeconds:F2} sec");

                Assert.IsTrue(File.Exists(downloadedFile), "Downloaded file does not exist.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                    errorMessage = ex.InnerException.Message;

                test.Fail("Test failed: " + errorMessage);
                throw;
            }
        }

        // Wait until overlay is gone
        private void WaitUntilOverlayDisappears(WebDriverWait wait, string overlaySelector = "div.overlay")
        {
            wait.Until(drv =>
            {
                var overlays = drv.FindElements(By.CssSelector(overlaySelector));
                return overlays.Count == 0 || overlays.All(o => !o.Displayed);
            });
        }

        // Wait for download to complete
        private string WaitForDownload(string downloadPath, List<string> filesBeforeDownload, int timeoutInSeconds = 60)
        {
            Stopwatch watch = Stopwatch.StartNew();
            while (watch.Elapsed.TotalSeconds < timeoutInSeconds)
            {
                var filesAfter = Directory.GetFiles(downloadPath).ToList();
                var newFiles = filesAfter.Except(filesBeforeDownload).ToList();

                if (newFiles.Count > 0)
                {
                    // Skip temporary files
                    var actualFile = newFiles.FirstOrDefault(f => !f.EndsWith(".crdownload") && !f.EndsWith(".part") && !f.EndsWith(".tmp"));
                    if (actualFile != null)
                        return actualFile;
                }

                Thread.Sleep(500);
            }

            throw new Exception("Download did not complete within timeout.");
        }
       


        //public static string WaitForDownload(string downloadPath, List<string> filesBefore, int timeoutInSeconds = 15)
        //{
        //    Stopwatch watch = Stopwatch.StartNew();
        //    while (watch.Elapsed.TotalSeconds < timeoutInSeconds)
        //    {
        //        var filesAfter = Directory.GetFiles(downloadPath).ToList();
        //        var newFiles = filesAfter.Except(filesBefore).ToList();

        //        if (newFiles.Count > 0 && !newFiles.Any(f => f.EndsWith(".crdownload") || f.EndsWith(".part")))
        //            return newFiles[0];

        //        Thread.Sleep(500);
        //    }

        //    throw new Exception("Download not completed within timeout.");
        //}




        [TestMethod]
        public void ExportSalesCartSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest(" Export sales data ");
            description = "test case to test export sales cart data. add your path in setupData before run";
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
                Thread.Sleep(3000);
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();
                IWebElement menu = driver.FindElement(By.Id("menuItem-Transactions"));
                if (menu != null)
                {
                    menu.Click();
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Transactions0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));

                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]")));
                        IWebElement export = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]"));

                        export.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-radio-button")));

                        IList<IWebElement> radioButtons = driver.FindElements(By.ClassName("mat-radio-button"));
                        radioButtons[1].Click();

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-checkbox")));
                        IList<IWebElement> checkBoxes = driver.FindElements(By.ClassName("mat-checkbox"));
                        checkBoxes[0].Click();

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("rowReverseHolder")));
                        IWebElement downloadContainer = driver.FindElement(By.ClassName("rowReverseHolder"));

                        IWebElement download = downloadContainer.FindElement(By.ClassName("mat-primary"));

                        download.Click();

                        Thread.Sleep(3000);
                        var filesAfterDownload = Directory.GetFiles(downloadPath).ToList();
                        var newFiles = filesAfterDownload.Except(filesBeforeDownload).ToList();
                        Assert.AreEqual(1, newFiles.Count, "Expected one new file to be downloaded.");
                        test.Pass();
                    }
                    else
                    {
                        Console.WriteLine("doesnt find sub menu");
                        test.Skip();
                    }
                }
                else
                {
                    Console.WriteLine("doesnt find menu");
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

        [TestMethod]
        public void ExportSalesPaymentSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("export sales payment");
            description = "test case to test export sales payment data. add your path in setupData before run";
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
                Thread.Sleep(3000);
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();
                IWebElement menu = driver.FindElement(By.Id("menuItem-Transactions"));
                if (menu != null)
                {
                    menu.Click();
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Transactions0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));

                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]")));
                        IWebElement export = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]"));

                        export.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-radio-button")));

                        IList<IWebElement> radioButtons = driver.FindElements(By.ClassName("mat-radio-button"));
                        radioButtons[2].Click();

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-checkbox")));
                        IList<IWebElement> checkBoxes = driver.FindElements(By.ClassName("mat-checkbox"));
                        checkBoxes[0].Click();

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("rowReverseHolder")));
                        IWebElement downloadContainer = driver.FindElement(By.ClassName("rowReverseHolder"));

                        IWebElement download = downloadContainer.FindElement(By.ClassName("mat-primary"));

                        download.Click();

                        Thread.Sleep(3000);
                        var filesAfterDownload = Directory.GetFiles(downloadPath).ToList();
                        var newFiles = filesAfterDownload.Except(filesBeforeDownload).ToList();
                        Assert.AreEqual(1, newFiles.Count, "Expected one new file to be downloaded.");
                        test.Pass();

                    }
                    else
                    {
                        Console.WriteLine("doesnt find sub menu");
                        test.Skip();
                    }
                }
                else
                {
                    Console.WriteLine("doesnt find menu");
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

        [TestMethod]
        public void ExportRefundTransactionSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("Refund Transaction data");
            description = "test case to test export refund transaction data. add your path in setupData before run";
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
                Thread.Sleep(3000);
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

                IWebElement menu = driver.FindElement(By.Id("menuItem-Transactions"));
                if (menu != null)
                {
                    menu.Click();
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Transactions0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));

                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]")));
                        IWebElement export = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]"));

                        export.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-radio-button")));

                        IList<IWebElement> radioButtons = driver.FindElements(By.ClassName("mat-radio-button"));
                        radioButtons[3].Click();

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-checkbox")));
                        IList<IWebElement> checkBoxes = driver.FindElements(By.ClassName("mat-checkbox"));
                        checkBoxes[0].Click();

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("rowReverseHolder")));
                        IWebElement downloadContainer = driver.FindElement(By.ClassName("rowReverseHolder"));

                        IWebElement download = downloadContainer.FindElement(By.ClassName("mat-primary"));

                        download.Click();

                        Thread.Sleep(3000);
                        var filesAfterDownload = Directory.GetFiles(downloadPath).ToList();
                        var newFiles = filesAfterDownload.Except(filesBeforeDownload).ToList();
                        Assert.AreEqual(1, newFiles.Count, "Expected one new file to be downloaded.");
                        test.Pass();
                    }
                    else
                    {
                        Console.WriteLine("doesnt find sub menu");
                        test.Skip();
                    }
                }
                else
                {
                    Console.WriteLine("doesnt find menu");
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
