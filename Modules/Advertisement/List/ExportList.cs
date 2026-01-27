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

namespace VMS_Phase1PortalAT.Modules.Advertisement.List
{
    [TestClass]
    public class ExportList
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        private readonly string systemName = Environment.UserName;
        private string downloadPath;
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
            downloadPath = setupDatas.downloadPath;
            downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", true);

            extent = ExtentManager.GetInstance();


        }


        [TestMethod]
        public void ExportListSuccess()
        {
            test = extent.CreateTest("test case to test export List");

            expectedStatus = "Passed";
            description = "test case to test export List";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements")));
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();
                IWebElement menu = driver.FindElement(By.Id("menuItem-Advertisements"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Advertisements0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]")));
                        IWebElement export = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_download')]"));
                        export.Click();
                        Thread.Sleep(3000);
                        var filesAfterDownload = Directory.GetFiles(downloadPath).ToList();
                        Console.WriteLine(filesAfterDownload.Count + " After Count");
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
