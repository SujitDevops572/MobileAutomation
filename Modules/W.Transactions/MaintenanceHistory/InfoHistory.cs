using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
using VMS_Phase1PortalAT.utls.datas.W.Transactions.MaintenanceRequest;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.MaintenanceHistory
{
    [TestClass]
    public class OptionsMaintenanceHistory
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
        public void InfoHistory()
        {
            test = extent.CreateTest("View Maintenance Request");
            expectedStatus = "Passed";
            description = "test case to test View Maintenance Request";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions"))).Click();

                Thread.Sleep(3000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions5"));
                if (submenu != null)
                {
                    submenu.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                    IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                    Thread.Sleep(1000);
                    select.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                    IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                    searchOptions[2].Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                    IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                    searchInput.Clear();
                    searchInput.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Info')]")));
                    IWebElement Info = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Info')]"));
                    Info.Click();
                    Thread.Sleep(1000);


                    IWebElement data = null;
                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//mat-dialog-container//th[contains(text(),'Machine')]/following::td[1]")));
                        data = driver.FindElement(By.XPath("//mat-dialog-container//th[contains(text(),'Machine')]/following::td[1]"));
                        Console.WriteLine(data.Text);
                        Assert.IsTrue(data.Text.Contains(AddMaintenanceData.searchMaintenanceRequestSuccess["Machine Id"]));
                        
                    }
                    catch (Exception ex)
                    {
                        test.Fail(ex);
                        Assert.Fail(ex.Message);
                    }


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Close']")));
                    IWebElement ViewCancel = driver.FindElement(By.XPath("//span[text()='Close']"));
                    ViewCancel.Click();

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
