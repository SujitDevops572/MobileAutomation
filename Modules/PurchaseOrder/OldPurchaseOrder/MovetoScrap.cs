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

namespace VMS_Phase1PortalAT.Modules.PurchaseOrder.OldPurchaseOrder
{

    [TestClass]
    public class MovetoScrap
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;

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
        }

        [TestMethod]
        public void Movetoscrap()
        {

            test = extent.CreateTest("move the purchase order to scrap");

            expectedStatus = "Passed";
            description = "test case to move the purchaseorder to scrap  ";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Purchase Order"))).Click();

                Thread.Sleep(3000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-Purchase Order1"));
                if (submenu != null)
                {
                    submenu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text() = 'more_vert']")));
                    IWebElement Actions = driver.FindElement(By.XPath("//mat-icon[text() = 'more_vert']"));
                    Actions.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()=' Move to scrap ']")));
                    IWebElement MovetoScrapClick= driver.FindElement(By.XPath("//button[text()=' Move to scrap ']"));
                    Thread.Sleep(2000);
                    MovetoScrapClick.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()=' Confirm ']")));
                    IWebElement Confirm = driver.FindElement(By.XPath("//span[text()=' Confirm ']"));
                    Thread.Sleep(2000);
                   // Confirm.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()=' Confirm ']")));
                    IWebElement Cancel= driver.FindElement(By.XPath("//span[text()='Cancel']"));
                    Thread.Sleep(2000);
                    Cancel.Click();


                }

            }
            catch (Exception ex) { 
                errorMessage = ex.InnerException.StackTrace; 
                Console.WriteLine(errorMessage); 
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
