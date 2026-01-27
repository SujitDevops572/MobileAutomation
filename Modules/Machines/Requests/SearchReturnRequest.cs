using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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
using VMS_Phase1PortalAT.utls.datas.W.Transactions.RefillRequest;
using VMS_Phase1PortalAT.utls.datas.W.Transactions.ReturnRequest;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Machines.Requests
{
    [TestClass]
    public class SearchReturnRequest
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
        public void SearchReturnRequestSuccess()
        {

            test = extent.CreateTest("Validating search return request datas with valid datas");

            expectedStatus = "Passed";
            description = "test case to search return request datas with valid datas so it return datas. add valid data in searchReturnRequstSuccess in SearchReturnRequestData file before run";
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

                SearchSuccess_MacReq.SearchAction(driver, "menuItem-Machines", "menuItem-Machines2", "(//div[contains(text(),'Return')])[1]", SearchReturnRequestData.searchReturnRequestSuccess, 1, "Return Request");

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
        public void SearchReturnRequestFailure()
        {
            test = extent.CreateTest("Validating  search return request datas with wrong datas ");

            expectedStatus = "Passed";
            description = "test case to search return request datas with wrong datas so it returns no data. add invalid data in searchReturnRequestFailure in SearchReturnRequestData file before run";
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

                SearchFailure_MacReq.SearchAction(driver, "menuItem-Machines", "menuItem-Machines2", "(//div[contains(text(),'Return')])[1]", SearchReturnRequestData.searchReturnRequestFailure, 0, "Return Request");

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
