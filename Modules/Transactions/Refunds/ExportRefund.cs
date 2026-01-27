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

namespace VMS_Phase1PortalAT.Modules.Transaction.Refunds
{
    [TestClass]
    public class ExportRefund
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
        public void ExportRefundSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest(" Export Refund Data");
            description = "test case to test export refund data. add your path in setupData before run";
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
                IWebElement menu = driver.FindElement(By.Id("menuItem-Transactions"));
                if (menu != null)
                {
                    menu.Click();
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Transactions1"));
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
