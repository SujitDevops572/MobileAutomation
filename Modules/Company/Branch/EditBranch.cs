using AventStack.ExtentReports;
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
using VMS_Phase1PortalAT.utls.datas.Company.Branch;

namespace VMS_Phase1PortalAT.Modules.Company.Branch
{
    [TestClass]
    public class EditBranch
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
        public void EditBranchSuccess()
        {
            test = extent.CreateTest("test case to test edit branch. add valid data in EditBranchSuccess in EditBranchData file");

            expectedStatus = "Passed";
            description = "test case to test edit branch. add valid data in EditBranchSuccess in EditBranchData file";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Company"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        Thread.Sleep(1000);
                        select.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        searchOptions[0].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        searchInput.Clear();
                        searchInput.SendKeys(EditBranchData.EditBranchSuccess["branchId"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
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
                                if (item.Text.Contains("Edit"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.Clear();
                            name.SendKeys(EditBranchData.EditBranchSuccess["name"]);
                            System.Threading.Thread.Sleep(1000);
                            submit.Click();
                            string title = "Branch Updated";
                            wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
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
        public void EditBranchFailure()
        {
            test = extent.CreateTest("test case to test edit branch. add invalid data in EditBranchFailure in EditBranchData file");

            expectedStatus = "Passed";
            description = "test case to test edit branch. add invalid data in EditBranchFailure in EditBranchData file";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Company"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        Thread.Sleep(1000);
                        select.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        searchOptions[0].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        searchInput.Clear();
                        searchInput.SendKeys(EditBranchData.EditBranchFailure["branchId"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
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
                                if (item.Text.Contains("Edit"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.Clear();
                            name.SendKeys(EditBranchData.EditBranchFailure["name"]);
                            submit.Click();
                            string title = "Error updating Branch";
                            wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
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
        public void EditBranchBtnDisable()
        {
            test = extent.CreateTest("test case to test edit branch");

            expectedStatus = "Passed";
            description = "test case to test edit branch";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company")));

                IWebElement menu = driver.FindElement(By.Id("menuItem-Company"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
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
                                if (item.Text.Contains("Edit"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            Assert.IsTrue(submit.GetAttribute("disabled").Equals("true"));

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
