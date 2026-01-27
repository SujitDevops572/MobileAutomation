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
using VMS_Phase1PortalAT.utls.datas.Advertisement.Mapping;

namespace VMS_Phase1PortalAT.Modules.Advertisement.MachineSettings
{
    [TestClass]
    public class DeleteMachineSettings
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;

        public required TestContext TestContext { get; set; }

        private IWebDriver driver;
        private setupData setupDatas = new setupData();
        private WriteResultToCSV testResult = new WriteResultToCSV();

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
        public void DeleteMachineSettingsSuccess()
        {
            test = extent.CreateTest("Check the Delete Machine Settings in Machine Settings Option");

            expectedStatus = "Passed";
            description = "Test case to check the Delete Machine Settings in Machine Settings Option";

            // Initialize WebDriver
            Login loginHelper = new Login();
            driver = loginHelper.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                loginHelper.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                Console.WriteLine("Navigating to Advertisement...");
                IWebElement menuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements")));
                menuBtn.Click();
                IWebElement submenuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements2")));
                submenuBtn.Click();
                Thread.Sleep(2000);
                              
                IWebElement lastcol = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                lastcol.Click();

                IWebElement delete = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Delete')]")));
                delete.Click();
                Thread.Sleep(200);
                                
                IWebElement Confirm = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]")));
                Confirm.Click();
                Thread.Sleep(400);

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Advertisement Setting Deleted"), " failed..");


            }


            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                }
                Console.WriteLine("Test failed with error: " + errorMessage);
                test.Fail(errorMessage);
                expectedStatus = "Failed";
                throw;
            }
        }




        [TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;
            string formattedTime = $"{timeTaken.TotalSeconds:F2}";
            try
            {
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error quitting driver: " + e.Message);
            }

            Console.WriteLine($"Test finished. Time taken: {formattedTime} seconds. Status: {expectedStatus}");

            testResult.WriteTestResults(TestContext, formattedTime, expectedStatus, errorMessage, description);
            extent.Flush();
        }



    }
 }
