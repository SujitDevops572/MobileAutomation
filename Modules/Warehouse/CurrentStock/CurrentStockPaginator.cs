using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Warehouse.CurrentStock
{
    [TestClass]
    public class CurrentStockPaginator
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        public required TestContext TestContext { get; set; }
        IWebDriver driver;
        paginator pagin = new paginator();
        setupData setupDatas = new setupData();
        WriteResultToCSV testResult = new WriteResultToCSV();
        private static ExtentReports extent;
        private static ExtentTest test;
        private string errormessage;

        [TestInitialize]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;
            extent = ExtentManager.GetInstance();

        }

        [TestMethod]
        public void CurrentStockPaginatorSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest(" current stock paginator");
            description = "test case to test current stock paginator.";
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
                pagin.PaginatorSuccess(driver, "menuItem-Warehouse", "menuItem-Warehouse2");
                test.Pass();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                    test.Fail(errormessage);
                }
                throw;
            }
        }

        [TestMethod]
        public void CurrentStockPaginatorFailure()
        {
            expectedStatus = "Failed";
            test = extent.CreateTest("Warehouse paginator");
            description = "test case to test warehouse paginator if it has any error";
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
                pagin.PaginatorSuccess(driver, "menuItem-Warehouse", "menuItem-Warehouse2");
                test.Pass();
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
