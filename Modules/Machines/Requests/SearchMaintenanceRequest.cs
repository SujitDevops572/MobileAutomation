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
using VMS_Phase1PortalAT.utls.datas.W.Transactions.MaintenanceRequest;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Machines.Requests
{
    [TestClass]
    public class SearchMaintenanceRequest
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        public required TestContext TestContext { get; set; }
        IWebDriver driver;
        setupData setupDatas = new setupData();
        WriteResultToCSV testResult = new WriteResultToCSV();
        string downloadPath = "";

        private static ExtentReports extent;
        private static ExtentTest test;

        [TestInitialize]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;
            downloadPath = setupDatas.downloadPath;

            extent = ExtentManager.GetInstance();


        }
        [TestMethod]
        public void SearchMaintenanceRequestSuccess()
        {
          


            expectedStatus = "Passed";
            description = "test case to search maintenance request datas with valid datas so it return datas. add valid data in searchMaintenanceRequestSuccess in SearchMaintenanceRequestData file before run";
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

                SearchSuccess_MacReq.SearchAction(driver, "menuItem-Machines", "menuItem-Machines2", "(//div[contains(text(),'Maintenance')])[1]", SearchMaintenanceRequestData.searchMaintenanceRequestSuccess, 1, "Maintenance Request");

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
        public void SearchMaintenanceRequestFailure()
        {

            test = extent.CreateTest("Validating search maintenance request datas with wrong datas ");

            expectedStatus = "Passed";
            description = "test case to search maintenance request datas with wrong datas so it returns no data. add invalid data in searchMaintenanceRequestFailure in SearchMaintenanceRequestData file before run";
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

                SearchFailure_MacReq.SearchAction(driver, "menuItem-Machines", "menuItem-Machines2", "(//div[contains(text(),'Maintenance')])[1]", SearchMaintenanceRequestData.searchMaintenanceRequestFailure, 0, "Maintenance Request");

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
