using AventStack.ExtentReports;
using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

namespace VMS_Phase1PortalAT.Modules.Machines.MachineList
{
    [TestClass]
    public class Merge
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
        public void MergeSuccess()
        {

            test = extent.CreateTest("Validating merge the slots");

            expectedStatus = "Passed";
            description = "test case to test merge the slots.add the valid data in mergeSlotSuccess in MergeSlotData file before run";
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
                IWebElement submenu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines0")));
                if (submenu != null)
                {
                    submenu.Click();
                    IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    IList<IWebElement> data = table.FindElements(By.TagName("tr"));
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
                            if (item.Text.Contains("View Details"))
                            {
                                item.Click();
                                break;
                            }
                        }
                        Thread.Sleep(1000);
                        IWebElement rowHolder = wait.Until(ExpectedConditions.ElementExists(By.ClassName("rowHolder2")));
                        IList<IWebElement> buttons = rowHolder.FindElements(By.TagName("button"));
                        buttons[1].Click();
                        IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                        IWebElement selectRow = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("selectedSlotRowData")));
                        Thread.Sleep(1000);
                        selectRow.Click();
                        wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("mat-option")));
                        IList<IWebElement> selectOptionsRow = driver.FindElements(By.TagName("mat-option"));
                            foreach (IWebElement a in selectOptionsRow) { a.Click();break; }
                            
                            
                            //selectOptionsRow[1].Click();

                        IWebElement selectSlot = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("slotTobemergerdto")));
                        if (selectSlot.Enabled)
                        {
                            Thread.Sleep(3000);
                            selectSlot.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("mat-option")));
                            IList<IWebElement> selectOptionsSlot = driver.FindElements(By.TagName("mat-option"));
                            selectOptionsSlot[0].Click();
                                Thread.Sleep(1000);
                            IWebElement width = driver.FindElement(By.Name("width"));
                            width.Clear();
                            width.SendKeys(MergeSlotData.MergeSlotSuccess["width"]);
                                Thread.Sleep(1000);
                                IWebElement submit = dialogElement.FindElement(By.ClassName("mat-raised-button"));
                            submit.Click();
                            string title = "Merged";
                            IWebElement snackbar = wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            bool isSuccess = false;
                                
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                                Console.WriteLine(snackbar.Text);
                                Assert.IsTrue(isSuccess, " failed..");

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
        public void MergeBtnDisable()
        {
            test = extent.CreateTest("Validating merge slot button disable with invalid data");


            expectedStatus = "Passed";
            description = "test case to test merge slot button disable with invalid data";
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
                IWebElement submenu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines0")));
                if (submenu != null)
                {
                    submenu.Click();
                    IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    IList<IWebElement> data = table.FindElements(By.TagName("tr"));
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
                            if (item.Text.Contains("View Details"))
                            {
                                item.Click();
                                break;
                            }
                        }
                        Thread.Sleep(1000);
                        IWebElement rowHolder = wait.Until(ExpectedConditions.ElementExists(By.ClassName("rowHolder2")));
                        IList<IWebElement> buttons = rowHolder.FindElements(By.TagName("button"));
                        buttons[1].Click();
                        IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                        IWebElement submit = dialogElement.FindElement(By.ClassName("mat-raised-button"));
                            Thread.Sleep(2000);
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
