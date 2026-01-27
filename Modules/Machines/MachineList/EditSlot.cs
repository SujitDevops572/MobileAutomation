using AventStack.ExtentReports;
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
    public class EditSlot
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
        public void EditSlotSuccess()
        {

            test = extent.CreateTest("Validating edit Slot");

            expectedStatus = "Passed";
            description = "test case to test editSlot. add valid data in editSlotSuccess in EditSlotData file before run";
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
                        IWebElement matMenu = wait.Until(drv => driver.FindElement(By.Id("mat-menu-panel-0")));
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
                        IWebElement rowHolder = wait.Until(condition: SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("rowHolder2")));
                        IList<IWebElement> buttons = rowHolder.FindElements(By.TagName("button"));
                        foreach (var item in buttons)
                        {
                            if (item.Text.Contains("edit"))
                            {
                                item.Click();
                                Thread.Sleep(3000);
                                break;
                            }
                        }
                            //String s = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("slotColumnCount"))).GetAttribute("value");
                            //Console.WriteLine(" Old slot Count "+s);
                            

                                //IWebElement TotalCount = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("slotColumnCount")));
                                var TotalSlotRowCount = wait.Until(driver => driver.FindElement(By.XPath("//mat-label[contains(text(),'Total Slot Row Count')]/ancestor::mat-form-field//input ")));                                                
                            TotalSlotRowCount.Clear();
                            TotalSlotRowCount.SendKeys(EditSlotData.EditSlotSuccess["TotalSlotRowCount"]);

                            var TotalSlotColumnCount = wait.Until(driver => driver.FindElement(By.XPath("//mat-label[contains(text(),'Total Slot Column Count')]/ancestor::mat-form-field//input ")));                                 
                            TotalSlotColumnCount.Clear();
                            TotalSlotColumnCount.SendKeys(EditSlotData.EditSlotSuccess["TotalColumnRowCountCount"]);

                            IWebElement StartingRow = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//mat-form-field[.//mat-label[contains(text(),'Starting Row')]]//input)[1]")));
                            StartingRow.Clear();
                            StartingRow.SendKeys(EditSlotData.EditSlotSuccess["StartingRow"]);

                            IWebElement StartingColumn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//mat-form-field[.//mat-label[contains(text(),'Starting Column')]]//input)[1]")));
                            StartingColumn.Clear();
                            StartingColumn.SendKeys(EditSlotData.EditSlotSuccess["StartingColumn"]);

                            IWebElement EndingRow = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//mat-form-field[.//mat-label[contains(text(),'Ending Row')]]//input)[1]")));
                            EndingRow.Clear();
                            EndingRow.SendKeys(EditSlotData.EditSlotSuccess["EndingRow"]);

                            IWebElement EndingColumn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//mat-form-field[.//mat-label[contains(text(),'Ending Column')]]//input)[1]")));
                            EndingColumn.Clear();
                            EndingColumn.SendKeys(EditSlotData.EditSlotSuccess["EndingColumn"]);

                           

                            //String s1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("slotColumnCount"))).GetAttribute("value");
                            //Console.WriteLine(" New Slot Count " + s1);
                            //IWebElement containerFlex = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("containerFlex")));
                            //IWebElement row = containerFlex.FindElement(By.ClassName("row"));
                            //IList<IWebElement> divInput = row.FindElements(By.TagName("mat-form-field"));
                            //int i = 1;
                            //foreach (var item in divInput)
                            //{

                            //    IWebElement inputEle = item.FindElement(By.ClassName("mat-input-element"));
                            //    inputEle.Clear();
                            //    if (i == 1 || i == 2 || item.Text.Contains("Starting Row *"))
                            //    {
                            //        inputEle.SendKeys("1");
                            //    }
                            //    else if (i == 3)
                            //    {
                            //        inputEle.SendKeys("7");
                            //    }
                            //    else
                            //    {
                            //        inputEle.SendKeys("5");
                            //    }
                            //    i++;
                            //}
                            IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Save')]")));
                        submit.Click();
                        string title = "Slot Count Updated";
                            Thread.Sleep(3000);
                           
                                
                                
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
        public void EditSlotBtnDisable()
        {
            test = extent.CreateTest("Validating editSlot button disable");

            expectedStatus = "Passed";
            description = "test case to test editSlot button disable";
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
                        IWebElement matMenu = wait.Until(drv => driver.FindElement(By.Id("mat-menu-panel-0")));
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
                        IWebElement rowHolder = wait.Until(condition: SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("rowHolder2")));
                        IList<IWebElement> buttons = rowHolder.FindElements(By.TagName("button"));
                        foreach (var item in buttons)
                        {
                            if (item.Text.Contains("edit"))
                            {
                                item.Click();
                                break;
                            }
                        }
                        IWebElement slotRowCount = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("slotRowCount")));
                        IWebElement slotColumnCount = driver.FindElement(By.Name("slotColumnCount"));
                        slotRowCount.Clear();
                        slotRowCount.SendKeys(EditSlotData.EditSlotSuccess1["TotalCount"]);
                        slotColumnCount.Clear();
                        slotColumnCount.SendKeys(EditSlotData.EditSlotSuccess1["UnitColumnCount"]);

                        IWebElement containerFlex = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("containerFlex")));
                        IWebElement row = containerFlex.FindElement(By.ClassName("row"));
                        IList<IWebElement> divInput = row.FindElements(By.TagName("mat-form-field"));
                        foreach (var item in divInput)
                        {

                            IWebElement inputEle = item.FindElement(By.ClassName("mat-input-element"));
                            inputEle.Clear();
                            if (item.Text.Contains("Starting Row *"))
                            {
                                inputEle.SendKeys("1");
                            }
                            else
                            {
                                inputEle.SendKeys("1");
                            }
                        }
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-dialog-container")));
                        IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                        IWebElement submit = dialogElement.FindElement(By.ClassName("mat-raised-button"));
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
