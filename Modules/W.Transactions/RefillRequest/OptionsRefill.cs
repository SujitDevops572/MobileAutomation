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
using VMS_Phase1PortalAT.utls.datas.W.Transactions.RefillRequest;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.RefillRequest
{

    [TestClass]
    public class OptionsRefill
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
        public void A_ViewRefill()
        {
            test = extent.CreateTest("View Refill Request");
            expectedStatus = "Passed";
            description = "test case to test View Refill Request";
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
                AddRefill(driver);
                Thread.Sleep(2000);

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions"))).Click();

                Thread.Sleep(3000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions2"));
                if (submenu != null)
                {
                    submenu.Click();
                       
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();
                    
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'View')]")));
                    IWebElement View = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'View')]"));
                    View.Click();
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


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Close']")));
                    IWebElement ViewCancel = driver.FindElement(By.XPath("//span[text()='Close']"));
                    ViewCancel.Click();

                               
                    /*wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[text() = \" Complete Return \"]")));
                    IWebElement CompleteReturn = driver.FindElement(By.XPath("//button[text() = \" Complete Return \"]"));
                    CompleteReturn.Click();
                    Thread.Sleep(1000);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button//span[text() = 'Close']")));
                    driver.FindElement(By.XPath("//button//span[text() = 'Close']")).Click();
                    Thread.Sleep(2000);*/

                    // driver.FindElement(By.XPath("//span[text() = ' Save ']"));
                    //driver.FindElement(By.XPath("//button//span[text() = ' Check All ']"));
                    //driver.FindElement(By.XPath("//button//span[text() = ' Clear All ']"));
                    //IWebElement IsComplete = driver.FindElement(By.XPath("//input[@type='checkbox']"));
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



        public static void AddRefill(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            int maxRetries = 10;

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

            SafeClick(By.XPath("//button[contains(@mattooltip,'Add Refill request')]"));

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

                        if (text == RefillSearch.RefillSearchSuccess["Machine Id"])
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

            SafeClick(By.XPath("//mat-dialog-container//span[contains(text(),'Next')]"));

            Thread.Sleep(1500);

            SafeClick(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]"));
        }




        [TestMethod]
        public void Ab_AssignWarehouse()
        {

            test = extent.CreateTest("Assign Warehouse Refill Request");

            expectedStatus = "Passed";
            description = "test case to test Assign Warehouse Refill Request";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions2"));
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
                    Thread.Sleep(200);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Assign Warehouse')]")));
                    IWebElement AssignWarehouse = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Assign Warehouse')]"));
                    AssignWarehouse.Click();
                    Thread.Sleep(500);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]")));
                    IWebElement CloseAssignwarehouse = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]"));
                    CloseAssignwarehouse.Click();

                    wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Warehouse assigned to refill request"), " failed..");

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
        public void B_SelectStock()
        {
            test = extent.CreateTest("Select Stock Refill");

            expectedStatus = "Passed";
            description = "test case to test Select Stock Refill";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

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
                // Click main menu
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions"))).Click();

                // Click submenu
                IWebElement submenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("menuItem-W. Transactions2")));
                submenu.Click();

                // Search type selection
                IWebElement select = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                select.Click();

                // Click the second mat-option safely
                IList<IWebElement> searchOptions = null;
                int retries = 0;
                while (retries < 3)
                {
                    try
                    {
                        searchOptions = driver.FindElements(By.TagName("mat-option"));
                        if (searchOptions.Count > 1)
                        {
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(searchOptions[1]));
                            searchOptions[1].Click();
                            break;
                        }
                        else
                        {
                            throw new Exception("Expected at least 2 mat-option elements, but found " + searchOptions.Count);
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                        retries++;
                    }
                }

                // Enter search text
                IWebElement searchInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("searchText")));
                searchInput.Clear();
                searchInput.SendKeys(RefillSearch.RefillSearchSuccess["Machine Id"]);
                searchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);

                // Click Actions
                IWebElement Actions = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                Actions.Click();


                // Click Select Stock button safely
                IWebElement SelectStock = null;
                retries = 0;
                while (retries < 3)
                {
                    try
                    {
                        SelectStock = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Select Stock')]")));
                        SelectStock.Click();
                        break;
                    }
                    catch (StaleElementReferenceException)
                    {
                        retries++;
                    }
                }
                Thread.Sleep(1000);
                              
                    // Wait for input fields in the dialog
                    IList<IWebElement> datas = [];
                    retries = 0;
                    int maxRetries = 3; // adjust timeout as needed
                try
                {
                    while (datas.Count == 0 && retries < maxRetries)
                    {

                        datas = driver.FindElements(By.XPath("//mat-dialog-container//input[@min='0' and @type='number']"))
                                     .Where(e => e.Displayed && e.Enabled)
                                     .ToList();

                        if (datas.Count > 0)
                            break; // found elements, exit loop
                    }
                }
                catch (StaleElementReferenceException)
                {
                    // Ignore and retry
                }


                    Thread.Sleep(500); // short wait before retrying
                    retries++;
                

                // After this loop, datas is guaranteed to have 0 or more non-stale, visible, enabled elements
                Console.WriteLine($"Found {datas.Count} input elements.");


                Console.WriteLine(datas.Count);

                // Fill all input fields safely
                for (int i = 0; i < datas.Count; i++)
                {
                    retries = 0;
                    while (retries < 3)
                    {
                        try
                        {
                            datas = driver.FindElements(By.XPath("//mat-dialog-container//input[@min='0' and @type='number']"));
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(datas[i]));
                            datas[i].Clear();
                            datas[i].SendKeys(RefillSearch.RefillSearchSuccess["Stock"]);
                            break;
                        }
                        catch (StaleElementReferenceException)
                        {
                            retries++;
                        }
                    }
                }

                // Click Save safely
                IWebElement Save = null;
                retries = 0;
                while (retries < 3)
                {
                    try
                    {
                        Save = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]")));
                        Save.Click();
                        break;
                    }
                    catch (StaleElementReferenceException)
                    {
                        retries++;
                    }
                }

                // Verify snackbar message
                IWebElement snackbar = wait.Until(drv => drv.FindElement(By.CssSelector(".mat-snack-bar-container")));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Refill request updated"), " failed..");
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
        public void B_ViewRefillSlip()
        {

            test = extent.CreateTest("View Refill Request");

            expectedStatus = "Passed";
            description = "test case to test View Refill Request";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions2"));
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

                    IWebElement ViewSlip = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'View Refill Slip')]")));
                    ViewSlip.Click();
                    Thread.Sleep(2000);

                    IWebElement data = null;
                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//mat-dialog-container//div[contains(text(),'Assigned Warehouse Name')]")));
                        data = driver.FindElement(By.XPath("//mat-dialog-container//div[contains(text(),'Assigned Warehouse Name')]"));
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
        public void C_CompleteRefill()
        {

            test = extent.CreateTest("Complete Refill Request");

            expectedStatus = "Passed";
            description = "test case to test Complete Refill Request";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions2"));
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
                    
                    IWebElement CompleteRefill = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Complete Refill')]")));
                    CompleteRefill.Click();

                    //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-content//input[@type='checkbox' and not (@role='switch')]")));
                    //IList<IWebElement> Checkboxes = driver.FindElements(By.XPath("//mat-dialog-content//input[@type='checkbox' and not (@role='switch')]"));
                    //foreach (IWebElement checkbox in Checkboxes) { 
                    //    checkbox.Click();
                    //}

                    IWebElement CheckAll = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Check All')]")));
                    CheckAll.Click();
                    Thread.Sleep(1000);

                    IWebElement Save = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]")));
                    Save.Click();

                    IWebElement snackbar = wait.Until(drv => drv.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Refill request updated"), " failed..");

                }
            }
            catch(Exception ex) {

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
        public void D_CancelRequest()
        {

            test = extent.CreateTest("Cancel Refill Request");

            expectedStatus = "Passed";
            description = "test case to test Cancel Refill Request";
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
                AddRefill(driver);
                Thread.Sleep(3000);
                
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions"))).Click();
                Thread.Sleep(1000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions2"));
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
            catch(Exception ex){

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