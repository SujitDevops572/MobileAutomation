using AventStack.ExtentReports;
using FlaUI.Core.AutomationElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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
using VMS_Phase1PortalAT.utls.datas.W.Transactions.ReturnRequest;
using VMS_Phase1PortalAT.utls.datas.W.Transactions.TransferRequest;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.TransferRequest
{
    [TestClass]
    public class OptionsTransferRequest
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
        public void A_ViewTransferDetails()
        {
            test = extent.CreateTest("View Transfer Details");

            expectedStatus = "Passed";
            description = "test case to test View Transfer Details";
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

                Thread.Sleep(1000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions1"));
                if (submenu != null)
                {
                    submenu.Click();

                    AddTransferRequest(driver);
                    Thread.Sleep(3000);

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
                    searchInput.SendKeys(TransferOptions.TransferRequestSuccess["Req Id Warehouse"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@role,'menu')]//button[contains(text(),'Info')]")));
                    IWebElement Info = driver.FindElement(By.XPath("//div[contains(@role,'menu')]//button[contains(text(),'Info')]"));
                    Info.Click();
                    Thread.Sleep(1000);

                    IWebElement data = null;
                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(),'Requestor Warehouse')]")));
                        data = driver.FindElement(By.XPath("//div[contains(text(),'Requestor Warehouse')]"));
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





        public static void AddTransferRequest(IWebDriver driver) {

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
                        
           
            SafeClick(By.XPath("//button[contains(@mattooltip,'Add Transfer request')]"));

            SafeClick(By.XPath("//mat-dialog-container//input[contains(@name,'warehouse')]"));

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

                        if (text.Contains(TransferOptions.TransferRequestSuccess["Add Transfer"]))
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
            Thread.Sleep(1500);

            SafeClick(By.XPath("//mat-dialog-container//input[contains(@placeholder,'Products')]"));

            IList<IWebElement> Options1 = SafeGetList(By.XPath("//div[contains(@role,'listbox')]//mat-option"));

            bool optionClicked1 = false;
            int attempt1 = 0;

            while (!optionClicked1 && attempt1 < maxRetries)
            {
                foreach (IWebElement opt in Options)
                {
                    try
                    {
                        string text = opt.Text;

                        if (text.Contains(TransferOptions.TransferRequestSuccess["Add Product"]))
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

                attempt1++;
            }


            IWebElement Qty = null;
            int retries = 5;

            for (int i = 0; i < retries; i++)
            {
                try
                {
                    Qty = driver.FindElement(By.XPath("//mat-dialog-container//input[contains(@name,'qty')]"));

                    if (Qty.Displayed && Qty.Enabled)
                    {
                        try { Qty.Click(); } catch { }

                        Qty.Clear();
                        Qty.SendKeys(TransferOptions.TransferRequestSuccess["Qty"]);
                        break;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(150);
                }
                catch (ElementClickInterceptedException)
                {
                    Thread.Sleep(150);
                }

                Thread.Sleep(100);
            }


            SafeClick(By.XPath("//mat-dialog-container//mat-icon[contains(text(),'add')]"));
                       
            SafeClick(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]"));

            wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
            Console.WriteLine(snackbar.Text);
            Assert.IsTrue(snackbar.Text.Contains("Transfer Request Added"), " failed..");

        }






        [TestMethod]
        public void B_AssignProWarehouse()
        {
            test = extent.CreateTest("Assigning Provider Warehouse");

            expectedStatus = "Passed";
            description = "test case to test Assigning Provider Warehouse";
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

                Thread.Sleep(1000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions1"));
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
                    searchInput.SendKeys(TransferOptions.TransferRequestSuccess["Req Id Warehouse"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@role,'menu')]//button[contains(text(),'Assign Pro. Warehouse')]")));
                    IWebElement Assign = driver.FindElement(By.XPath("//div[contains(@role,'menu')]//button[contains(text(),'Assign Pro. Warehouse')]"));
                    Assign.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@role,'listbox')]//mat-option")));
                    IList<IWebElement> Options = driver.FindElements(By.XPath("//div[contains(@role,'listbox')]//mat-option"));
                    foreach (IWebElement Opt in Options)
                    {
                        if (Opt.Text.Contains(TransferOptions.TransferRequestSuccess["Provider Warehouse"]))
                        {
                            Opt.Click();
                            break;
                        }
                    }
                        Thread.Sleep(600);

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]")));
                        IWebElement Save = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]"));
                        Save.Click();

                        wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                        IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                        Console.WriteLine(snackbar.Text);
                        Assert.IsTrue(snackbar.Text.Contains("Warehouse assigned to transfer request"), " failed..");



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
        public void C_PickUp()
        {
            test = extent.CreateTest("Pick Up Stock");

            expectedStatus = "Passed";
            description = "test case to test Pick Up Stock";
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

                Thread.Sleep(1000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions1"));
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
                    searchInput.SendKeys(TransferOptions.TransferRequestSuccess["Req Id Warehouse"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@role,'menu')]//button[contains(text(),'PickUp')]")));
                    IWebElement PickUp = driver.FindElement(By.XPath("//div[contains(@role,'menu')]//button[contains(text(),'PickUp')]"));
                    PickUp.Click();
                    Thread.Sleep(1000);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Stock and Proceed')]")));
                    IWebElement Proceed = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Stock and Proceed')]"));
                    Proceed.Click();
                                        
                    wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Transfer request updated"), " failed..");



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
        public void D_Complete()
        {
            test = extent.CreateTest("Assigning Provider Warehouse");

            expectedStatus = "Passed";
            description = "test case to test Assigning Provider Warehouse";
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

                Thread.Sleep(1000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions1"));
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
                    searchInput.SendKeys(TransferOptions.TransferRequestSuccess["Req Id Warehouse"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-panel')]//button[contains(text(),'Complete')]")));
                    IWebElement PickUp = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-panel')]//button[contains(text(),'Complete')]"));
                    PickUp.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]")));
                    IWebElement Save = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]"));
                    Save.Click();

                    wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Transfer request Completed"), " failed..");



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
            test = extent.CreateTest("Cancelling Transfer Request");
            expectedStatus = "Passed";
            description = "test case to test Cancelling Transfer Request";

            Login login = new Login();
            driver = login.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            login.LoginSuccessCompanyAdmin();

            SeleniumSafeActions.ClickSafe(driver, wait, By.Id("menuItem-W. Transactions"));
            SeleniumSafeActions.ClickSafe(driver, wait, By.Id("menuItem-W. Transactions1")); 

            AddTransferRequest(driver);
            Thread.Sleep(3000);
            SeleniumSafeActions.ClickSafe(driver, wait, By.ClassName("searchTypeBox"));

            wait.Until(d => driver.FindElements(By.TagName("mat-option")).Count > 1);
            SeleniumSafeActions.ClickSafe(driver, wait, By.XPath("(//mat-option)[2]"));

            SeleniumSafeActions.SendKeysSafe(
                driver,
                wait,
                By.Name("searchText"),
                TransferOptions.TransferRequestSuccess["Req Id Warehouse"] + Keys.Enter
            );

            Thread.Sleep(1000);
            By actionCell = By.XPath("(//tbody/tr)[1]/td[last()]");
            SeleniumSafeActions.ClickSafe(driver, wait, actionCell);

            By cancelBtn = By.XPath("//div[contains(@class,'mat-menu-panel')]//button[contains(text(),'Cancel')]");
            SeleniumSafeActions.ClickSafe(driver, wait, cancelBtn);

            By confirmBtn = By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]");
            SeleniumSafeActions.ClickSafe(driver, wait, confirmBtn);

            By snackBar = By.CssSelector(".mat-snack-bar-container");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(snackBar));

            string snackText = driver.FindElement(snackBar).Text;
            Console.WriteLine(snackText);

            Assert.IsTrue(snackText.Contains("Request Canceled"), "Request cancel failed");
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




        public static class SeleniumSafeActions
        {
            public static void ClickSafe(IWebDriver driver, WebDriverWait wait, By locator, int retries = 3)
            {
                for (int i = 0; i < retries; i++)
                {
                    try
                    {
                        wait.Until(d =>
                        {
                            try
                            {
                                var el = d.FindElement(locator);
                                return el.Displayed && el.Enabled;
                            }
                            catch
                            {
                                return false;
                            }
                        });

                        IWebElement element = driver.FindElement(locator);

                        try
                        {
                            element.Click();
                        }
                        catch
                        {
                            ((IJavaScriptExecutor)driver)
                                .ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);

                            ((IJavaScriptExecutor)driver)
                                .ExecuteScript("arguments[0].click();", element);
                        }

                        return;
                    }
                    catch
                    {
                        if (i == retries - 1)
                        {
                            return;
                        }
                    }
                }
            }


            public static void SendKeysSafe(IWebDriver driver, WebDriverWait wait, By locator, string text)
            {
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        wait.Until(d =>
                        {
                            try
                            {
                                var el = d.FindElement(locator);
                                return el.Displayed && el.Enabled;
                            }
                            catch
                            {
                                return false;
                            }
                        });

                        IWebElement element = driver.FindElement(locator);
                        element.Clear();
                        element.SendKeys(text);
                        return;
                    }
                    catch
                    {
                        if (i == 2)
                        {
                            return;
                        }
                    }
                }
            }
        }










    }
}
