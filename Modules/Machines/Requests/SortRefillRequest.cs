using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Machines.Requests
{
    [TestClass]
    public class SortRefillRequest
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
        public void SortRefillRequestSuccess()
        {

           

            expectedStatus = "Passed";
            description = "test case to test sort refill request with available options. make sure refill request has multiple datas before run";
            Login login = new Login();
            driver = login.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    login.LoginSuccessCompanyAdmin();
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
                SortSuccess_MacReq.Sort(driver, "menuItem-Machines", "menuItem-Machines2", "Refill Request", "(//div[contains(text(),'Refill')])[1]");
            
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;

                    test.Fail(errorMessage);    
                }
            }
        }

        [TestMethod]
        public void SortRefillRequestFailure()
        {
           

            expectedStatus = "Failed";
            description = "test case to test sort refill request with available options. make sure refill request has multiple datas before run. it pass only if there is an error in sorting";
            Login login = new Login();
            driver = login.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    login.LoginSuccessCompanyAdmin();
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
                SortFailure_MacReq.Sort(driver, "menuItem-Machines", "menuItem-Machines2", "Refill Request", "(//div[contains(text(),'Refill')])[1]");
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
