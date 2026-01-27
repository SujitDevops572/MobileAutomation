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

namespace VMS_Phase1PortalAT.Modules.Products.Categories
{
    [TestClass]
    public class ExportCategories
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        public required TestContext TestContext { get; set; }
        IWebDriver driver;
        setupData setupDatas = new setupData();
        WriteResultToCSV testResult = new WriteResultToCSV();

        static string systemName = Environment.UserName;
        private string downloadPath = $"C:\\Users\\{systemName}\\Downloads";

        private static ExtentReports extent;
        private static ExtentTest test;



        //private Stopwatch stopwatch;
        //private string expectedStatus;
        //private string errorMessage;
        //private string description;
        //private string downloadPath = @"C:\Users\Babu Office\Downloads"; // Specify your download path
        //public required TestContext TestContext { get; set; }
        //IWebDriver driver;
        //setupData setupDatas = new setupData();
        //WriteResultToCSV testResult = new WriteResultToCSV();

        //private static ExtentReports extent;
        //private static ExtentTest test;

        [TestInitialize]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;
            var options = new ChromeOptions();
            string systemName = Environment.UserName;
            Console.WriteLine(systemName);
            options.AddUserProfilePreference("download.default_directory", downloadPath);

            extent = ExtentManager.GetInstance();


            //    stopwatch = new Stopwatch();
            //    stopwatch.Start();
            //    errorMessage = string.Empty;


            //    extent = ExtentManager.GetInstance();

        }

        [TestMethod]
        public void ExportCategoriesSuccess()
        {

            test = extent.CreateTest("Validating  Export Categories Success");

            expectedStatus = "Passed";
            description = "test case to test export category";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
                Thread.Sleep(3000);
                var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();
                IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products1"));
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
