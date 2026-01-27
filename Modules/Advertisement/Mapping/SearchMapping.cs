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
using VMS_Phase1PortalAT.utls.datas.Advertisement.Mapping;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Advertisement.Mapping
{
    [TestClass]
    public class SearchMapping
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


        [TestInitialize]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;

            extent = ExtentManager.GetInstance();

        }


        [TestMethod]
        public void SearchMappingSuccess()
        {

            expectedStatus = "Passed";
            description = "test case to test search Mapping. add valid data searchMappingSuccess in SearchMappingData file";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));

            try
            {
                Console.WriteLine("Logging in...");

                LoginSuccess.LoginSuccessCompanyAdmin();

                wait.Until(d =>
                    d.FindElements(By.Id("menuItem-W. Transactions")).Any()
                );

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

                SearchSuccess.SearchAction(driver, "menuItem-Advertisements", "menuItem-Advertisements3", SearchMappingData.SearchMappingSuccess, 1, "Mapping");


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
        public void SearchMappingFailure()
        {
            expectedStatus = "Failed";
            description = "test case to test search Mapping. add valid data searchMappingFailure in SearchMappingData file";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
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

                SearchFailure.SearchAction(driver, "menuItem-Advertisements", "menuItem-Advertisements3", SearchMappingData.SearchMappingFailure, 0, "Mapping");


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
