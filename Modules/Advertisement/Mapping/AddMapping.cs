using AventStack.ExtentReports;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
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


namespace VMS_Phase1PortalAT.Modules.Advertisement.Mapping
{
    [TestClass]
    public class AddMapping
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

        public void AddMappingSuccess()
        {
            test = extent.CreateTest("Check the Add Mapping in Mapping Option");

            expectedStatus = "Passed";
            description = "Test case to check the Add Mapping in Mapping Option";

            // Initialize WebDriver
            Login loginHelper = new Login();
            driver = loginHelper.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                loginHelper.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                // Navigate to Validation Group List
                Console.WriteLine("Navigating to Client User List...");
                IWebElement menuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements")));
                menuBtn.Click();
                IWebElement submenuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements3")));
                submenuBtn.Click();
                Thread.Sleep(2000);
                IWebElement addbtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'add')]")));
                addbtn.Click();
                IWebElement Client = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container[contains(@role,'dialog')]//input[contains(@placeholder,'Client')]")));
                Client.Click();
                Thread.Sleep(400);
                IList<IWebElement> Clientopt = driver.FindElements(By.XPath("//mat-option"));
                foreach (IWebElement a in Clientopt) {
                    if (a.Text.ToLower().Contains(AddMappingData.AddMappingSuccess["Client"])) { a.Click(); break; }

                }
                Thread.Sleep(400);
                IWebElement Machine = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container[contains(@role,'dialog')]//input[contains(@placeholder,'Machine ID')]")));
                Machine.Click();
                Thread.Sleep(400);
                IList<IWebElement> Machineopt = driver.FindElements(By.XPath("//mat-option"));
                foreach (IWebElement a in Machineopt)
                {
                    if (a.Text.Contains(AddMappingData.AddMappingSuccess["Machine"])) { a.Click(); break; }

                }
                Thread.Sleep(400);
                IWebElement advId = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container[contains(@role,'dialog')]//mat-select[contains(@name,'advertisementId')]")));
                advId.Click();
                IList<IWebElement> Advopt = driver.FindElements(By.XPath("//mat-option"));
                foreach (IWebElement a in Advopt)
                {
                    if (a.Text.Contains(AddMappingData.AddMappingSuccess["adv"])) { a.Click(); break; }

                }
                Thread.Sleep(400);
                IWebElement save = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Save')]")));
                save.Click();

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Advertisement Machine Mapping Added"), " failed..");
            
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
