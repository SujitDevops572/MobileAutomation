using AventStack.ExtentReports;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.MaintenanceHistory

{
    
    public class FlowMaintainanceRequestSuccess
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
        public void FlowMaintainanceRequest()
        {
            test = extent.CreateTest("Check the flow of Maintainance Request");

            expectedStatus = "Passed";
            description = "test case to Check the functionlity flow of Maintainance Request";
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-W. Transactions"));
                menu.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions4")));
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions4"));
                submenu.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//td//button)[1]")));
                IWebElement ActionsButton = driver.FindElement(By.XPath("(//td//button)[1]"));
                ActionsButton.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='View']")));
                IWebElement Viewclick = driver.FindElement(By.XPath("//button[text()=\"View\"]"));
                Viewclick.Click();
                Thread.Sleep(3000);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Close']")));
                IWebElement ViewCloseclick = driver.FindElement(By.XPath("//span[text()='Close']"));
                ViewCloseclick.Click();
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("(//td//button)[1]")).Click();
                
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()= \" Update Issue \"]")));
                IWebElement UpdateIssueclick = driver.FindElement(By.XPath("//button[text()= \" Update Issue \"]"));
                UpdateIssueclick.Click();
                Thread.Sleep(3000);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Close']")));
                IWebElement UpdateIssueCloseclick = driver.FindElement(By.XPath("//span[text()='Close']"));
                UpdateIssueCloseclick.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text()='refresh']")));
                IWebElement Refresh = driver.FindElement(By.XPath("//mat-icon[text()='refresh']"));
                Refresh.Click();
                Thread.Sleep(3000);
                ActionsButton.Click();
                Thread.Sleep(2000);
                IList<IWebElement> AddIssue = driver.FindElements(By.XPath("//button[contains(text(), 'Issue')]"));
                Thread.Sleep(2000);
                AddIssue[1].Click();
                Thread.Sleep(1000);
                UpdateIssueCloseclick.Click();
                ActionsButton.Click();
                IWebElement ViewIssueclick = driver.FindElement(By.XPath("//button[text()=\" View History \"]"));
                ViewIssueclick.Click();
                Thread.Sleep(1000);
                driver.Navigate().Back();
                Thread.Sleep(1000);


                
                
                
                /*IList<IWebElement> AddIssue = driver.FindElements(By.XPath("//button[contains(text(), 'Issue')]"));
               Thread.Sleep(2000);
               AddIssue[1].Click();

               IWebElement AddIssueCloseclick = driver.FindElement(By.XPath("//span[text()='Close']"));
               Thread.Sleep(2000);
               AddIssueCloseclick.Click();
               Actionclick.Click();*/

                // Actions.Click();
                //IWebElement CancelRequestclick = driver.FindElement(By.XPath("//button[text()=' Cancel Request ']"));
                //Thread.Sleep(3000);
                //CancelRequestclick.Click();
                //IWebElement ConfirmtocancelRequestclick = driver.FindElement(By.XPath("//span[text()=' Confirm ']"));
                //Thread.Sleep(3000);
                // ConfirmtocancelRequestclick.Click();


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
            string formatedTime = $"{timeTaken.TotalSeconds:F2}";
            driver.Quit();
            testResult.WriteTestResults(TestContext, formatedTime, expectedStatus, errorMessage, description);

            extent.Flush();
        }

    }
}

