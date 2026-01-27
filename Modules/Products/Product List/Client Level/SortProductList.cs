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

namespace VMS_Phase1PortalAT.Modules.Products.Product_List.Client_Level
{
    [TestClass]
    public class SortProductList
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
        [Ignore("Temporarily disabled.")]
        public void SortProductListSuccess()
        {
            test = extent.CreateTest("Validating Sort Product List Success");

            expectedStatus = "Passed";
            description = "test case to test sort client level product with available options. make sure client level product has multiple datas before run";
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
            IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
            if (menu != null)
            {
                menu.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products3")));
                IWebElement submenu = driver.FindElement(By.Id("menuItem-Products3"));
                if (submenu != null)
                {
                    submenu.Click();
                    IWebElement tabList = wait.Until(drv => driver.FindElement(By.ClassName("mat-tab-list")));
                    Console.WriteLine(tabList.Text);
                    IWebElement tabLabels = tabList.FindElement(By.ClassName("mat-tab-labels"));
                    IWebElement clientTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'Client Level')]")));
                    clientTab.Click();
                    Thread.Sleep(1000);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    IWebElement previousTable = driver.FindElement(By.TagName("tbody"));
                    IList<IWebElement> previousDatas = previousTable.FindElements(By.TagName("tr"));
                    IList<IWebElement> previousColumns = previousDatas[0].FindElements(By.TagName("td"));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("sortBox")));
                    IWebElement select = driver.FindElement(By.ClassName("sortBox"));
                    select.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                    IList<IWebElement> sortOptions = driver.FindElements(By.TagName("mat-option"));
                    foreach (var item in sortOptions)
                    {
                        int index = sortOptions.IndexOf(item);
                        sortOptions[index].Click();
                        Thread.Sleep(4000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("sortButton")));
                        IWebElement sortBtn = driver.FindElement(By.ClassName("sortButton"));
                        sortBtn.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
                        IWebElement firstRow = datas[0];
                        IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                        Assert.AreNotEqual(previousColumns, columns, "Error in sorting");
                        select.Click();

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
            }
        }

        [TestMethod]
        [Ignore("Temporarily disabled.")]
        public void SortProductListFailure()
        {
            test = extent.CreateTest("Validating Sort Product List Failure");


            expectedStatus = "Failed";
            description = "test case to test sort client level product with available options. make sure client level product has multiple datas before run. it pass only if there is an error in sorting";
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
            IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
            if (menu != null)
            {
                menu.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products3")));
                IWebElement submenu = driver.FindElement(By.Id("menuItem-Products3"));
                if (submenu != null)
                {
                    submenu.Click();
                    IWebElement tabList = wait.Until(drv => driver.FindElement(By.ClassName("mat-tab-list")));
                    Console.WriteLine(tabList.Text);
                    IWebElement tabLabels = tabList.FindElement(By.ClassName("mat-tab-labels"));
                    IWebElement clientTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'Client Level')]")));
                    clientTab.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));

                    IWebElement previousTable = driver.FindElement(By.TagName("tbody"));
                    IList<IWebElement> previousDatas = previousTable.FindElements(By.TagName("tr"));
                    IList<IWebElement> previousColumns = previousDatas[0].FindElements(By.TagName("td"));

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("sortBox")));

                    IWebElement select = driver.FindElement(By.ClassName("sortBox"));
                    select.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                    IList<IWebElement> sortOptions = driver.FindElements(By.TagName("mat-option"));
                    foreach (var item in sortOptions)
                    {
                        int index = sortOptions.IndexOf(item);
                        sortOptions[index].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("sortButton")));

                        IWebElement sortBtn = driver.FindElement(By.ClassName("sortButton"));
                        sortBtn.Click();
                        Thread.Sleep(4000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
                        IWebElement firstRow = datas[0];
                        IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                        Assert.AreEqual(previousColumns, columns, "Error in sorting");
                        select.Click();

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
