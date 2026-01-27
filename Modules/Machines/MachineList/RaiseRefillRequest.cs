using AventStack.ExtentReports;
using FlaUI.Core.Input;
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

namespace VMS_Phase1PortalAT.Modules.Machines.MachineList
{
    [TestClass]
    public class RaiseRefillRequest
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        public required TestContext TestContext { get; set; }
        IWebDriver driver;
        setupData setupDatas = new setupData();
        WriteResultToCSV testResult = new WriteResultToCSV();
        static bool isSuccess1;
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
        public void RaiseRefillRequestSuccess()
        {
            test = extent.CreateTest("Validating raise refill request");

            expectedStatus = "Passed";
            description = "test case to test raise refill request";
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Machines"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines0")));
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
                        searchInput.SendKeys(MachineRaiseRefillRequestData.RaiseRefillRequestSuccess["Machine Id"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);

                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tr")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        if (data.Count > 0)
                        {
                            IWebElement firstRow = data[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            IWebElement lastColumn = columns[columns.Count - 1];
                            lastColumn.Click();
                            //IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                            //IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                            //Thread.Sleep(1000);
                            IList<IWebElement> raise = driver.FindElements(By.XPath("//button[text()=' Raise Refill Request ']"));
                            bool isVisible = raise.Count > 0 && raise[0].Displayed && raise[0].Enabled;

                            if (isVisible)
                            {
                                raise[0].Click();
                                Thread.Sleep(500);

                                string title1 = "Cant create refill request";
                                wait.Until(drv => driver.FindElements(By.CssSelector(".mat-snack-bar-container")));
                                IList<IWebElement> snackbar1 = driver.FindElements(By.CssSelector(".mat-snack-bar-container"));
                                bool isSuccess1 = false;
                                if (snackbar1.Count > 0)
                                {
                                    
                                    IWebElement searchInput2 = driver.FindElement(By.Name("searchText"));
                                    searchInput2.Clear();
                                    searchInput2.SendKeys(Keys.Enter);

                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines")));
                                    wait.Until(driver =>
                                        driver.FindElements(By.CssSelector(".cdk-overlay-backdrop.cdk-overlay-backdrop-showing")).Count == 0);


                                    IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                                        By.Id("menuItem-W. Transactions")));


                                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);


                                    element.Click();

                                    IWebElement submenu1 = driver.FindElement(By.Id("menuItem-W. Transactions2"));
                                    if (submenu1 != null)
                                    {
                                        submenu1.Click();


                                        IWebElement select4 = driver.FindElement(By.ClassName("searchTypeBox"));
                                        Thread.Sleep(1000);
                                        select4.Click();
                                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                                        IList<IWebElement> searchOptions4 = driver.FindElements(By.TagName("mat-option"));
                                        searchOptions4[1].Click();
                                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                                        IWebElement searchInput4 = driver.FindElement(By.Name("searchText"));
                                        searchInput4.Clear();
                                        searchInput4.SendKeys(MachineRaiseRefillRequestData.RaiseRefillRequestSuccess["Machine Id"]);
                                        searchInput4.SendKeys(Keys.Enter);
                                        Thread.Sleep(1000);
                                        IList<IWebElement> search = driver.FindElements(By.XPath("//tr"));
                                        if (search.Count > 1)
                                        {
                                            IWebElement Actions = driver.FindElement(By.XPath("//mat-icon[text() = 'more_vert']"));
                                            Actions.Click();
                                            Thread.Sleep(2000);
                                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), ' Cancel Request ')]"))).Click();
                                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()=' Confirm ']"))).Click();
                                            Thread.Sleep(1500);
                                        }

                                        //returning to Machine
                                        Thread.Sleep(2000);
                                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines"))).Click();
                                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines0"))).Click();
                                        IWebElement select1 = driver.FindElement(By.ClassName("searchTypeBox"));
                                        Thread.Sleep(2000);
                                        select1.Click();
                                        Thread.Sleep(2000);
                                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                                        IList<IWebElement> searchOptions1 = driver.FindElements(By.TagName("mat-option"));
                                        searchOptions1[0].Click();
                                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                                        IWebElement searchInput1 = driver.FindElement(By.Name("searchText"));
                                        searchInput1.Clear();
                                        searchInput1.SendKeys(MachineRaiseRefillRequestData.RaiseRefillRequestSuccess["Machine Id"]);
                                        searchInput1.SendKeys(Keys.Enter);
                                        Thread.Sleep(2000);


                                        if (data.Count > 0)
                                        {
                                            IWebElement Actions1 = driver.FindElement(By.XPath("//mat-icon[text() = 'more_vert']"));
                                           
                                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", Actions1);
                                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", Actions1);
                                            Thread.Sleep(1000);
                                            //IWebElement matMenu1 = driver.FindElement(By.ClassName("mat-menu-panel"));
                                            //IList<IWebElement> menuItems1 = matMenu1.FindElements(By.TagName("button"));
                                            //IList<IWebElement> datas = driver.FindElements(By.XPath("//tr"));
                                          
                                           
                                            IWebElement raiseRefillBtn = wait.Until(driver =>
                                            {
                                                var elements = driver.FindElements(By.XPath("//button[text()=' Raise Refill Request ']"));
                                                if (elements.Count > 0)
                                                {
                                                    var el = elements[0];
                                                    return (el.Displayed && el.Enabled) ? el : null;
                                                }
                                                return null;
                                            });
                                            Thread.Sleep(2000);
                                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(raiseRefillBtn)).Click();
                                            Thread.Sleep(2000);

                                            
                                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-dialog-container")));
                                            IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                                            IWebElement removeQty = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'remove')]")));
                                            removeQty.Click();
                                            IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-raised-button")));
                                            Console.WriteLine(submit.Text);
                                            submit.Click();
                                            wait.Until(drv => driver.FindElements(By.CssSelector(".mat-snack-bar-container")));
                                            IList<IWebElement> snackbar2 = driver.FindElements(By.CssSelector(".mat-snack-bar-container"));
                                            isSuccess1 = true;

                                        }
                                    }
                                               

                                   
                                }


                                else
                                {
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-dialog-container")));
                                    IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                                    IWebElement removeQty = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'remove')]")));
                                    removeQty.Click();
                                    IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-raised-button")));
                                    Console.WriteLine(submit.Text);
                                    submit.Click();
                                    Thread.Sleep(2000);
                                    isSuccess1 = true;
                                }
                                Assert.IsTrue(isSuccess1, " failed..");
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
        public void RaiseRefillRequestAlreadyCreated()
        {
            test = extent.CreateTest("Validating already raised refill request");

            expectedStatus = "Passed";
            description = "test case to test the already raised refill request.check refill request is already raised before run";
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Machines"));
            if (menu != null)
            {
                menu.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines0")));
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
                        searchInput.SendKeys(MachineRaiseRefillRequestData.RaiseRefillRequestFailure["Machine Id"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tr")));
                    IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                    IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                    if (data.Count > 0)
                    {
                        IWebElement firstRow = data[0];
                        IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                        IWebElement lastColumn = columns[columns.Count - 1];
                        lastColumn.Click();
                        IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                        IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                        foreach (var item in menuItems)
                        {
                            if (item.Text.Contains("Raise Refill Request"))
                            {
                                item.Click();
                                break;
                            }
                        }
                        string title = "Cant create refill request due to existing request";
                        IWebElement snackbar = wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
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
