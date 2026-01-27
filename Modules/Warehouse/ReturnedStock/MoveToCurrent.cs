using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Warehouse.ReturnedStock;

namespace VMS_Phase1PortalAT.Modules.Warehouse.ReturnedStock
{
    [TestClass]
    public class MoveToCurrent
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
        public void MoveToCurentStockSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("Move to current stock");
            description = "test case to test move to current. add valid data in MergeCurrentStockSuccess in MoveToCurrentStockData file";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse4")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse4"));
                    if (submenu != null)
                    {
                        submenu.Click();
                       
                        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                        //IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        //select.Click();
                        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        //IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        //searchOptions[0].Click();
                        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        //IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        //searchInput.Clear();
                        //searchInput.SendKeys(MoveToCurrentStockData.MergeCurrentStockSuccess["Stock Id"]);
                        //searchInput.SendKeys(Keys.Enter);
                        //Thread.Sleep(1000);
                        
                        wait.Until(drv => drv.FindElement(By.XPath("//tr")));
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        if (data.Count > 0)
                        {
                            IWebElement firstRow = data[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            IWebElement lastColumn = columns[columns.Count - 1];
                            lastColumn.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                            IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                            foreach (var item in menuItems)
                            {
                                if (item.Text.Contains("Move To Current"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            submit.Click();
                            string title = "Returned Stock moved to Current";
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Assert.IsTrue(isSuccess, " failed..");
                            test.Pass();
                        }
                        else
                        {
                            Console.WriteLine("doesnt find add null");
                            test.Skip();
                        }
                    }
                    else
                    {
                        Console.WriteLine("doesnt find sub menu");
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
        public void MoveToCurentStockFailure()
        {
            expectedStatus = "Failed";
            test = extent.CreateTest("Move current stock to zero");
            description = "test case to test move to current with 0 qty. add valid data in MergeCurrentStockFailure in MoveToCurrentStockData file";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse4")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse4"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        select.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        searchOptions[0].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        searchInput.Clear();
                        searchInput.SendKeys(MoveToCurrentStockData.MergeCurrentStockFailure["Stock Id"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        if (data.Count > 0)
                        {
                            IWebElement firstRow = data[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            IWebElement lastColumn = columns[columns.Count - 1];
                            lastColumn.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                            IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                            foreach (var item in menuItems)
                            {
                                if (item.Text.Contains("Move To Current"))
                                {
                                    item.Click();
                                    break;
                                }
                            }

                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("qtyToScrap")));
                            IWebElement qty = driver.FindElement(By.Name("qtyToScrap"));
                            qty.Clear();
                            qty.SendKeys("0");
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            bool isDisable = submit.GetAttribute("disabled").Equals("true");
                            bool isSuccess = false;
                            if (isDisable)
                            {
                                isSuccess = true;
                            }
                            Assert.IsTrue(isSuccess, " failed..");
                            test.Pass();
                        }
                        else
                        {
                            Console.WriteLine("doesnt find sub menu");
                            test.Skip();
                        }
                    }
                    else
                    {
                        Console.WriteLine("doesnt find menu");
                        test.Skip();
                    }
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

    internal class setupData
    {
        internal string downloadPath;
    }
}
