using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;

namespace VMS_Phase1PortalAT.Modules.Machines.MachineList
{
    [TestClass]
    public class UpdateOperationStatus
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
        public void UpdateOperationStatusSuccess()
        {
            test = extent.CreateTest("Validating update operation status");

            expectedStatus = "Passed";
            description = "test case to test update operation status";
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
                IWebElement menu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines")));

                if (menu != null)
            {
                menu.Click();
                    IWebElement submenu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines0")));

                    if (submenu != null)
                {
                    submenu.Click();
                        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        Thread.Sleep(1000);
                        select.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        searchOptions[0].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        searchInput.Clear();
                        searchInput.SendKeys(MachineUpdateOperationStatusData.UpdateOperationStatusSuccess["Machine Id"]);
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
                            IWebElement matMenu = wait.Until(drv => driver.FindElement(By.ClassName("mat-menu-panel")));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));


                        foreach (var item in menuItems)
                        {
                            if (item.Text.Contains("Update Operation Status"))
                            {
                                item.Click();
                                break;
                            }
                        }

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-dialog-container")));
                        IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                        IWebElement selectItem = dialogElement.FindElement(By.TagName("mat-select"));
                        selectItem.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("mat-option")));
                        IList<IWebElement> selectOptions = driver.FindElements(By.TagName("mat-option"));
                        Console.WriteLine(selectOptions.Count + " search options count");

                            IWebElement dropdownElement = driver.FindElement(By.Name("status"));
                           
                            
                            String selected = dropdownElement.Text.Trim();
                            Console.WriteLine(" Old => "+selected);
                            if (selected != "Down (Planned)")
                            {
                                for (int i = 0; i < selectOptions.Count; i++)
                                {
                                    if (selectOptions[i].Text.Trim() == UpdateLocationData.updatestatusSuccess["status 1"]) { selectOptions[i].Click(); break; }
                                }
                              
                            
                            }
                            else {
                                for (int i = 0; i < selectOptions.Count; i++)
                                {
                                    if (selectOptions[i].Text.Trim() == UpdateLocationData.updatestatusSuccess["status 2"]) { selectOptions[i].Click(); break; }
                                }
                                
                            }
                            Thread.Sleep(1000);
                            String selected1 = dropdownElement.Text.Trim();
                            Console.WriteLine(" New => " + selected1);
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            submit.Click();
                        string title = "Status updated";
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".mat-snack-bar-container")));
                        IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                        Console.WriteLine($"Snackbar: {snackbar.Text}");

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
        public void UpdateOperationStatusBtnDisable()
        {
            test = extent.CreateTest("Validating update operation status  without required datas");

            expectedStatus = "Passed";
            description = "test case to test update operation status  without required datas";
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
                IWebElement menu = driver.FindElement(By.Id("menuItem-Machines"));
                if (menu != null)
            {
                menu.Click();
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Machines0"));
                    if (submenu != null)
                {
                    submenu.Click();
                        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        Thread.Sleep(1000);
                        select.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        searchOptions[0].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        searchInput.Clear();
                        searchInput.SendKeys(MachineUpdateOperationStatusData.UpdateOperationStatusFailure["Machine Id"]);
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
                            IWebElement matMenu = wait.Until(drv => driver.FindElement(By.ClassName("mat-menu-panel")));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                        foreach (var item in menuItems)
                        {
                            if (item.Text.Contains("Update Operation Status"))
                            {
                                item.Click();
                                break;
                            }
                        }

                        IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                        IList<IWebElement> dialogBtn = dialogElement.FindElements(By.TagName("button"));
                        Console.WriteLine(dialogBtn.Count);
                        foreach (var item in dialogBtn)
                        {
                            if (item.Text.Contains("Save"))
                            {
                                Assert.IsTrue(item.GetAttribute("disabled").Equals("true"));
                                test.Pass();    
                                    break;
                            }
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
