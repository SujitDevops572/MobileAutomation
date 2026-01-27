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
using VMS_Phase1PortalAT.utls.datas.W.Transactions.RefillRequest;
using VMS_Phase1PortalAT.utls.datas.W.Transactions.ReturnRequest;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.ReturnRequest
{

    [TestClass]
    public class OptionsReturn
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
        public void A_ViewReturnDetails()
        {
            test = extent.CreateTest("View Return Details");

            expectedStatus = "Passed";
            description = "test case to test View Return Details";
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
                AddReturn(driver);
                Thread.Sleep(2000);

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions"))).Click();

                Thread.Sleep(1000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions3"));
                if (submenu != null)
                {
                    submenu.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                    IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                    Thread.Sleep(1000);
                    select.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                    IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                    searchOptions[1].Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                    IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                    searchInput.Clear();
                    searchInput.SendKeys(ReturnSearch.ReturnSearchSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'View')]")));
                    IWebElement  ViewOpt = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'View')]"));
                    ViewOpt.Click();
                    Thread.Sleep(1000);

                    IWebElement data = null;
                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//mat-dialog-container//div[contains(text(),'Machine')]")));
                        data = driver.FindElement(By.XPath("//mat-dialog-container//div[contains(text(),'Machine')]"));
                        Console.WriteLine(data.Text);
                    }
                    catch (Exception ex)
                    {
                        test.Fail(ex);
                        Assert.Fail(ex.Message);
                    }


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Close')]")));
                    IWebElement Close = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Close')]"));
                    Close.Click();
                                        

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





        public static void AddReturn(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            int maxRetries = 3;

            Action<IWebElement> SafeElementClick = (el) =>
            {
                int retries = 0;
                while (retries < maxRetries)
                {
                    try
                    {
                        wait.Until(_ => el.Displayed && el.Enabled);
                        el.Click();
                        return;
                    }
                    catch (StaleElementReferenceException) { return; }
                    catch (ElementClickInterceptedException) { Thread.Sleep(300); }
                    catch (InvalidOperationException) { }
                    Thread.Sleep(200);
                    retries++;
                }
            };

            Action<By> SafeClick = (locator) =>
            {
                int retries = 0;
                while (retries < maxRetries)
                {
                    try
                    {
                        IWebElement el = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
                        el.Click();
                        return;
                    }
                    catch (StaleElementReferenceException) { }
                    catch (ElementClickInterceptedException) { Thread.Sleep(300); }
                    catch (InvalidOperationException) { }
                    Thread.Sleep(200);
                    retries++;
                }
            };

            Func<By, IList<IWebElement>> SafeGetList = (locator) =>
            {
                int retries = 0;
                while (retries < maxRetries)
                {
                    try
                    {
                        IList<IWebElement> list = driver.FindElements(locator);
                        if (list.Count > 0)
                           return list;
                    }
                    catch (StaleElementReferenceException) { }
                    Thread.Sleep(200);
                    retries++;
                }
                return driver.FindElements(locator);
            };

            SafeClick(By.Id("menuItem-Machines"));
            SafeClick(By.Id("menuItem-Machines2"));
            Thread.Sleep(400);
            SafeClick(By.XPath("//div[contains(@role,'tablist')]//div[contains(text(),'Return')]"));

            SafeClick(By.XPath("//button[contains(@mattooltip,'Add Return request')]"));

            SafeClick(By.XPath("//mat-dialog-container//input[contains(@name,'machineIds')]"));

            IList<IWebElement> Options = SafeGetList(By.XPath("//div[contains(@role,'listbox')]//mat-option"));

            bool optionClicked = false;
            int attempt = 0;

            while (!optionClicked && attempt < maxRetries)
            {
                foreach (IWebElement opt in Options)
                {
                    try
                    {
                        string text = opt.Text;

                        if (text == ReturnSearch.ReturnSearchSuccess["Machine Id"])
                        {
                            SafeElementClick(opt);
                            optionClicked = true;
                            break;
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                        Options = SafeGetList(By.XPath("//div[contains(@role,'listbox')]//mat-option"));
                        break;
                    }
                }
                attempt++;
            }
            Thread.Sleep(1000);

            IList<IWebElement> Qtys = SafeGetList(By.XPath("//mat-dialog-container//input[contains(@type, 'number') and contains(@min, '1')]"));

            bool optionClicked1 = false;
            int attempt1 = 0;

            while (!optionClicked1 && attempt1 < maxRetries)
            {
                for (int i = 0; i < Qtys.Count; i++)
                {
                    try
                    {
                        IWebElement Qt = Qtys[i];

                        Qt.Clear();
                        Qt.SendKeys(ReturnSearch.ReturnSearchSuccess["Qty"]);

                        if (i == Qtys.Count - 1)
                        {
                            optionClicked1 = true;
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                        Qtys = SafeGetList(By.XPath("//div[contains(@role,'listbox')]//mat-option"));
                        break;  
                    }
                }

                attempt1++;
            }

            SafeClick(By.XPath("//mat-dialog-container//span[contains(text(),'Check All')]"));
            SafeClick(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]"));
           
            wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
            Console.WriteLine(snackbar.Text);
            Assert.IsTrue(snackbar.Text.Contains("Return Request Added"), " failed..");

        }





        [TestMethod]
        public void AB_AssignWarehouse()
        {

            test = extent.CreateTest("Assign Warehouse for Return Request");

            expectedStatus = "Passed";
            description = "test case to test Assign Warehouse for Return Request ";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions3"));
                if (submenu != null)
                {
                    submenu.Click();
                    
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();
                    Thread.Sleep(400);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Assign Warehouse')]")));
                    IWebElement Assign = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Assign Warehouse')]"));
                    Assign.Click();
                    Thread.Sleep(400);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]")));
                    IWebElement Save = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]"));
                    Save.Click();

                    wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Warehouse assigned to return request"), " failed..");


                }

            }
            catch (Exception ex){

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
        public void B_ViewReturnSlip()
        {

            test = extent.CreateTest("View Return Request Slip");

            expectedStatus = "Passed";
            description = "test case to test View Return Request Slip ";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions3"));
                if (submenu != null)
                {
                    submenu.Click();
                   
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                    IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                    Thread.Sleep(1000);
                    select.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                    IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                    searchOptions[1].Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                    IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                    searchInput.Clear();
                    searchInput.SendKeys(ReturnSearch.ReturnSearchSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    IWebElement ViewSlip = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'View Return Slip')]")));
                    ViewSlip.Click();
                    Thread.Sleep(3000);

                    IWebElement data = null;
                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//mat-dialog-container//div[contains(text(),'Client')]")));
                        data = driver.FindElement(By.XPath("//mat-dialog-container//div[contains(text(),'Client')]"));
                        Console.WriteLine(data.Text);
                    }
                    catch (Exception ex)
                    {
                        test.Fail(ex);
                        Assert.Fail(ex.Message);
                    }

                    IWebElement Close = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Close')]")));
                    Close.Click();

                    test.Pass();
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
        public void C_PickUpStock()
        {

            test = extent.CreateTest("Pick Up Stock");

            expectedStatus = "Passed";
            description = "test case to test Pick Up Stock ";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions3"));
                if (submenu != null)
                {
                    submenu.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                    IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                    Thread.Sleep(1000);
                    select.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                    IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                    searchOptions[1].Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                    IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                    searchInput.Clear();
                    searchInput.SendKeys(ReturnSearch.ReturnSearchSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    IWebElement PickUp = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Pick Up Stock')]")));
                    PickUp.Click();
                    Thread.Sleep(300);

                    IWebElement pick = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Pick Up')]")));
                    pick.Click();

                    wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Return request updated"), " failed..");

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
        public void D_CompleteReturn()
        {

            test = extent.CreateTest(" Complete Return ");

            expectedStatus = "Passed";
            description = "test case to test Complete Return ";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions3"));
                if (submenu != null)
                {
                    submenu.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                    IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                    Thread.Sleep(1000);
                    select.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                    IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                    searchOptions[1].Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                    IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                    searchInput.Clear();
                    searchInput.SendKeys(RefillSearch.RefillSearchSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    IWebElement CompleteReturn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Complete Return')]")));
                    CompleteReturn.Click();
                    Thread.Sleep(200);

                    IWebElement CheckAll = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Check All')]")));
                    CheckAll.Click();
                    Thread.Sleep(1000);

                    IWebElement Save = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]")));
                    Save.Click();

                    IWebElement snackbar = wait.Until(drv => drv.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    Console.WriteLine(snackbar.Text);
                    //Assert.IsTrue(snackbar.Text.Contains("Refill request updated"), " failed..");

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
        public void E_CancelRequest()
        {

            test = extent.CreateTest("Cancel Request");

            expectedStatus = "Passed";
            description = "test case to test Cancel Request ";
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
                AddReturn(driver);
                Thread.Sleep(2000);

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions"))).Click();
                Thread.Sleep(1000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions3"));
                if (submenu != null)
                {
                    submenu.Click();
                  
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                    IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                    Thread.Sleep(1000);
                    select.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                    IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                    searchOptions[1].Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                    IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                    searchInput.Clear();
                    searchInput.SendKeys(RefillSearch.RefillSearchSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    IWebElement CancelRequest = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Cancel Request')]")));
                    CancelRequest.Click();

                    IWebElement Confirm = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]")));
                    Confirm.Click();

                    IWebElement snackbar = wait.Until(drv => drv.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Request Canceled"), " failed..");

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