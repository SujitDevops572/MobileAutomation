using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RazorEngine.Compilation.ImpromptuInterface;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Machines.MaintenanceRequest;

namespace VMS_Phase1PortalAT.Modules.Machines.Requests
{
    [TestClass]
    public class AddMaintenanceRequest
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
        public void AddMaintenanceRequestSuccess()
        {

            test = extent.CreateTest("Validating add maintenance request with valid datas");

            expectedStatus = "Passed";
            description = "test case to test add maintenance request with valid datas. add valid data in addMaintenanceRequestSuccess in AddMaintenanceRequestData file before run";
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                IWebElement menu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines")));
            if (menu != null)
            {
                menu.Click();

                IWebElement submenu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines2")));
                if (submenu != null)
                {
                    submenu.Click();
                    IWebElement tabList = wait.Until(drv => driver.FindElement(By.ClassName("mat-tab-list")));
                    Console.WriteLine(tabList.Text);
                    IWebElement tabLabels = tabList.FindElement(By.ClassName("mat-tab-labels"));
                    IWebElement returnTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'Maintenance')]")));
                    returnTab.Click();
                    Thread.Sleep(1000);
                    IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-fab")));
                    if (add != null)
                    {
                        Thread.Sleep(1000);
                        add.Click();
                        wait.Until(drv => driver.FindElement(By.Name("machineIds"))).Click();
                            Thread.Sleep(2000);
                        wait.Until(drv => driver.FindElement(By.XPath($"//span[contains(text(),'{AddMaintenanceRequestData.addMaintenacnRequestSuccess["machineIds"]}')]"))).Click();
                                              
                        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        //IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));

                        //if (options[0].Text.Contains(AddMaintenanceRequestData.addMaintenacnRequestSuccess["machineIds"]))
                        //{
                        //    options[0].Click();
                        //}
                        IWebElement issues = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("mat-chip-input"))); 
                        issues.SendKeys(AddMaintenanceRequestData.addMaintenacnRequestSuccess["issues"]);

                        IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                        submit.Click();

                            string title = "Maintenance Request Added";
                            IWebElement snackbar = wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Console.WriteLine(snackbar.Text);
                            Thread.Sleep(2000);
                            Assert.IsTrue(isSuccess, " failed..");

                            test.Pass();


                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions")));
                            driver.FindElement(By.Id("menuItem-W. Transactions")).Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions4")));
                            driver.FindElement(By.Id("menuItem-W. Transactions4")).Click();
                            Thread.Sleep(1000);
                            IWebElement Actions = driver.FindElement(By.XPath("(//mat-icon[text() = 'more_vert'])[1]"));
                            Actions.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),' Cancel Request ')]")));
                            driver.FindElement(By.XPath("//button[contains(text(),' Cancel Request ')]")).Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),' Confirm ')]")));
                            Thread.Sleep(1000);
                            driver.FindElement(By.XPath("//span[contains(text(),' Confirm ')]")).Click();
                            Thread.Sleep(1000);

                        }
                }
                else
                {
                    Console.WriteLine("doesnt find add null");

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

        [TestMethod]
        public void AddMaintenanceRequestFailure()
        {
            test = extent.CreateTest("Validating add maintenance request with duplicate datas");

            expectedStatus = "Passed";
            description = "Test case to add maintenance request with duplicate data. Ensure 'AddMaintenanceRequestData.addMaintenacnRequestFailure' is set properly.";
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
                          
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                // Navigate to menu
                IWebElement menu = wait.Until(drv => drv.FindElement(By.Id("menuItem-Machines")));
                menu.Click();

                IWebElement submenu = wait.Until(drv => drv.FindElement(By.Id("menuItem-Machines2")));
                submenu.Click();

                IWebElement tabList = wait.Until(drv => drv.FindElement(By.ClassName("mat-tab-list")));
                Console.WriteLine("Tab List: " + tabList.Text);

                IWebElement maintenanceTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'Maintenance')]")));
                maintenanceTab.Click();

                Thread.Sleep(1000);

                IWebElement addButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-fab")));
                addButton.Click();
                Thread.Sleep(1000);

                IWebElement machineId = driver.FindElement(By.Name("machineIds"));
                machineId.SendKeys(AddMaintenanceRequestData.addMaintenacnRequestFailure["machineIds"]);

                IWebElement issues = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("mat-chip-input")));
                issues.SendKeys(AddMaintenanceRequestData.addMaintenacnRequestFailure["issues"]);

                Thread.Sleep(2000);
                IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                if (submit.Enabled) {
                    
                    Assert.IsTrue(false, "Expected to be disabled");
                }




                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                //IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
                //if (options.Count > 0 && options[0].Text.Contains(AddMaintenanceRequestData.addMaintenacnRequestFailure["machineIds"]))
                //{
                //    options[0].Click();
                //}



                //string expectedErrorMessage = "Error adding Maintenance Request";
                //IWebElement snackbar = wait.Until(drv => drv.FindElement(By.CssSelector(".mat-snack-bar-container")));

                //bool isFailureMessageShown = snackbar.Text.Contains(expectedErrorMessage);
                //Assert.IsTrue(isFailureMessageShown, "Expected failure message was not displayed.");

                test.Pass("Duplicate maintenance request was handled correctly.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Test failed: " + errorMessage);
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
