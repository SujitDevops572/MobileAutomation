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

namespace VMS_Phase1PortalAT.Modules.Machines.Requests
{
    [TestClass]
    public class AddReturnRequest
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
        public void AddReturnRequestSuccess()
        {
            test = extent.CreateTest("Validating add return request with valid datas");

            expectedStatus = "Passed";
            description = "test case to test add return request with valid datas. add valid data in addReturnRequestSuccess in AddReturnRequestData file before run";
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

                    IWebElement tabList =  wait.Until(drv => driver.FindElement(By.ClassName("mat-tab-list")));
                    Console.WriteLine(tabList.Text);
                    IWebElement tabLabels = tabList.FindElement(By.ClassName("mat-tab-labels"));
                    IWebElement returnTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'Return')]")));
                    returnTab.Click();
                    Thread.Sleep(1000);
                    IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-fab")));
                    if (add != null)
                    {
                        Thread.Sleep(1000);
                        add.Click();
                        Thread.Sleep(1000);
                        IWebElement machineId = driver.FindElement(By.Name("machineIds"));
                        machineId.SendKeys(AddReturnRequestData.addReturnRequestSuccess["machineIds"]);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));

                        if (options[0].Text.Contains(AddReturnRequestData.addReturnRequestSuccess["machineIds"]))
                        {
                            options[0].Click();
                        }
                        IWebElement checkbox = wait.Until(drv => driver.FindElement(By.TagName("mat-checkbox")));
                        checkbox.Click();

                        IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                        submit.Click();
                        string title = "Return Request Added";
                        IWebElement snackbar = wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                        bool isSuccess = false;
                        if (snackbar.Text.Contains(title))
                        {
                            isSuccess = true;

                        }
                        Assert.IsTrue(isSuccess, " failed..");

                            test.Pass();

                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions")));
                            driver.FindElement(By.Id("menuItem-W. Transactions")).Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions3")));
                            driver.FindElement(By.Id("menuItem-W. Transactions3")).Click();
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
        public void AddReturnRequestBtnDisable()
        {
            test = extent.CreateTest("Validating add return request without required datas BtnDisable");

            expectedStatus = "Passed";
            description = "test case to test add return request without required datas";
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
                    IWebElement returnTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'Return')]")));
                    returnTab.Click();
                    Thread.Sleep(1000);
                    IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-fab")));
                    if (add != null)
                    {
                        Thread.Sleep(1000);
                        add.Click();
                        Thread.Sleep(1000);
                        IWebElement machineId = driver.FindElement(By.Name("machineIds"));
                        machineId.SendKeys(AddReturnRequestData.addReturnRequestFailure["machineIds"]);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));

                        if (options[0].Text.Contains(AddReturnRequestData.addReturnRequestFailure["machineIds"]))
                        {
                            options[0].Click();
                        }
                        
                        IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                        Assert.IsTrue(submit.GetAttribute("disabled").Equals("true"));

                            test.Pass();
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
