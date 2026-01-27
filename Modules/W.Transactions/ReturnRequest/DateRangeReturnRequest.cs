using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas.W.Transactions.Purchase;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.ReturnRequest
{
    [TestClass]
    public class DateRangeReturnRequest
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        public required TestContext TestContext { get; set; }
        IWebDriver driver;
        WriteResultToCSV testResult = new WriteResultToCSV();
        private static ExtentReports extent;
        private static ExtentTest test;

        [TestInitialize]
        public void Setup()
        {
            stopwatch = Stopwatch.StartNew();
            errorMessage = string.Empty;
            extent = ExtentManager.GetInstance();
        }

        [TestMethod]
        public void DateRangeReturnRequestSuccess()
        {
           
            expectedStatus = "Passed";
            description = "Test for date filter in Return request with valid date range";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

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
                DateRange.DateTimeAction(driver, "menuItem-W. Transactions", "menuItem-W. Transactions3", PurchaseSearchStockData.DateRange,  "Return Request");

            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail(errorMessage);
                throw;
            }
        }

       

        [TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            driver.Quit();
            testResult.WriteTestResults(TestContext,
                                       stopwatch.Elapsed.TotalSeconds.ToString("F2"),
                                       expectedStatus,
                                       errorMessage,
                                       description);
            extent.Flush();
        }
    }
}
