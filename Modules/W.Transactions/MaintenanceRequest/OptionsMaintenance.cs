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
using VMS_Phase1PortalAT.utls.datas.W.Transactions.MaintenanceRequest;
using VMS_Phase1PortalAT.utls.datas.W.Transactions.RefillRequest;
using VMS_Phase1PortalAT.utls.datas.W.Transactions.ReturnRequest;
using WindowsInput;
using WindowsInput.Native;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.MaintenanceRequest
{
    [TestClass]
    public class OptionsMaintenance
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;

        public required TestContext TestContext { get; set; }
        IWebDriver driver;
        setupData setupDatas = new setupData();
        WriteResultToCSV testResult = new WriteResultToCSV();

        static string systemName = Environment.UserName;
        private string downloadPath = $"C:\\Users\\{systemName}\\Downloads";

        private static ExtentReports extent;
        private static ExtentTest test;

        [TestInitialize]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;

            var options = new ChromeOptions();
            string systemName = Environment.UserName;
            Console.WriteLine(systemName);
            options.AddUserProfilePreference("download.default_directory", downloadPath);


            extent = ExtentManager.GetInstance();
        }

        [TestMethod]
        public void A_ViewMaintenance()
        {
            test = extent.CreateTest("View Maintenance Request");
            expectedStatus = "Passed";
            description = "test case to test View Maintenance Request";
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
                AddMaintenance(driver);
                Thread.Sleep(2000);

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions"))).Click();

                Thread.Sleep(3000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions4"));
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
                    searchInput.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


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
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//mat-dialog-container//th[contains(text(),'Machine')]")));
                        data = driver.FindElement(By.XPath("//mat-dialog-container//th[contains(text(),'Machine')]"));
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



        public static void AddMaintenance(IWebDriver driver)
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
            Thread.Sleep(400);
            SafeClick(By.XPath("//div[contains(@role,'tablist')]//div[contains(text(),'Maintenance')]"));

            SafeClick(By.XPath("//button[contains(@mattooltip,'Add Maintanance request')]"));

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

                        if (text == AddMaintenanceData.searchMaintenanceRequestSuccess["Machine Id"])
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


            IWebElement NoteBox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//mat-dialog-container//mat-label[contains(text(),'Note')]/preceding::input[1]")));
            NoteBox.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["Note"]);

            IWebElement Issues = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//mat-dialog-container//mat-label[contains(text(),'Issues')]/preceding::input[1]")));
            Issues.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["Issues"]);

            Thread.Sleep(500);

            SafeClick(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]"));

            wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
            Console.WriteLine(snackbar.Text);
            Assert.IsTrue(snackbar.Text.Contains("Maintenance Request Added"), " failed..");

        }





        [TestMethod]
        public void B_UpdateIssues()
        {
            test = extent.CreateTest("Update Issues - Maintenance Request");
            expectedStatus = "Passed";
            description = "test case to test Update Issues";
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

                Thread.Sleep(500);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions4"));
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
                    searchInput.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Update Issues')]")));
                    IWebElement Update = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Update Issues')]"));
                    Update.Click();
                    Thread.Sleep(500);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//mat-select[contains(@name,'selectedCheckList')]")));
                    IWebElement Checklist = driver.FindElement(By.XPath("//mat-dialog-container//mat-select[contains(@name,'selectedCheckList')]"));
                    Checklist.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'mat-select-panel')]")));
                    var optionLocator = By.XPath("//div[contains(@class,'mat-select-panel')]//mat-option");

                    int count = driver.FindElements(optionLocator).Count;
                    for (int i = 0; i < count; i++)
                    {
                        IWebElement option = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(optionLocator));

                        try
                        {
                            option.Click();
                            break;
                        }
                        catch (ElementClickInterceptedException)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", option);
                            option.Click();
                        }
                        Thread.Sleep(150);
                    }

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//mat-select[contains(@name,'selectedCheckList')]/following::mat-icon[text()='add'][1]")));
                    IWebElement ChecklistAdd = driver.FindElement(By.XPath("//mat-dialog-container//mat-select[contains(@name,'selectedCheckList')]/following::mat-icon[text()='add'][1]"));
                    ChecklistAdd.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//mat-dialog-container//input[contains(@name,'newIssue')]")));
                    IWebElement New = driver.FindElement(By.XPath("//mat-dialog-container//input[contains(@name,'newIssue')]"));
                    New.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["New Issue"]);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//input[contains(@name,'newIssue')]/following::mat-icon[text()='add'][1]")));
                    IWebElement NewAdd = driver.FindElement(By.XPath("//mat-dialog-container//input[contains(@name,'newIssue')]/following::mat-icon[text()='add'][1]"));
                    NewAdd.Click();
                    
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Update')]")));
                    IWebElement Confirm = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Update')]"));
                    Confirm.Click();

                    wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Request Issue Updated"), " failed..");


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
        public void C_ApproveIssues()
        {
            test = extent.CreateTest("Approve Issues - Maintenance Request");
            expectedStatus = "Passed";
            description = "test case to test Approve Issues";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions4"));
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
                    searchInput.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Approve Issues')]")));
                    IWebElement Approve = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Approve Issues')]"));
                    Approve.Click();
                    Thread.Sleep(1000);
                          
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]")));
                    IWebElement Confirm = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]"));
                    Confirm.Click();

                    wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Request status Changed to Approved"), " failed..");


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
        public void D_AssignToRandD()
        {
            test = extent.CreateTest("Assign To R&D");
            expectedStatus = "Passed";
            description = "test case to test Assign To R&D";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions4"));
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
                    searchInput.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Assign to R&D')]")));
                    IWebElement AssignTo = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Assign to R&D')]"));
                    AssignTo.Click();
                    Thread.Sleep(1000);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]")));
                    IWebElement Confirm = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]"));
                    Confirm.Click();

                    wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Request status Changed to R&D"), " failed..");

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
        public void E_CompleteRandD()
        {
            test = extent.CreateTest("Complete R&D");
            expectedStatus = "Passed";
            description = "test case to test Complete R&D";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions4"));
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
                    searchInput.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Complete R&D')]")));
                    IWebElement Complete = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Complete R&D')]"));
                    Complete.Click();
                    Thread.Sleep(1000);

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Select File')]")));
                    IWebElement SelectFile = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Select File')]"));
                    SelectFile.Click();
                    Thread.Sleep(2000);

                    var sim = new InputSimulator();
                    sim.Keyboard.TextEntry(AddMaintenanceData.searchMaintenanceRequestSuccess["Path"]);
                    sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN); 
                    Console.WriteLine("Typed Path");
                    Thread.Sleep(2000);
                    
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]")));
                    IWebElement Save = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]"));
                    Save.Click();

                    wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Request Updated"), " failed..");

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
        public void F_DownloadRandDReport()
        {
            test = extent.CreateTest("Download R&D Report File");
            expectedStatus = "Passed";
            description = "test case to test Download R&D Report File";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions4"));
                if (submenu != null)
                {
                    submenu.Click();
                    var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

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
                    searchInput.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Download R&D Report File')]")));
                    IWebElement DownloadReport = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'Download R&D Report File')]"));
                    DownloadReport.Click();
                    
                    Thread.Sleep(4000);
                    var filesAfterDownload = Directory.GetFiles(downloadPath).ToList();
                    var newFiles = filesAfterDownload.Except(filesBeforeDownload).ToList();
                    Assert.AreEqual(1, newFiles.Count, "Expected one new file to be downloaded.");

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
        public void G_ViewHistory()
        {
            test = extent.CreateTest("View History");
            expectedStatus = "Passed";
            description = "test case to test View History";
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
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions4"));
                if (submenu != null)
                {
                    submenu.Click();
                    var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

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
                    searchInput.SendKeys(AddMaintenanceData.searchMaintenanceRequestSuccess["Machine Id"]);
                    searchInput.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);


                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Actions = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Actions.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'View History')]")));
                    IWebElement View = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(), 'View History')]"));
                    View.Click();
                    Thread.Sleep(3000);
                                        
                    try
                    {
                        string data = driver.Title;
                        Console.WriteLine(data);
                        Assert.IsTrue(data.Contains("maintenance history"));
                    }
                    catch (Exception ex)
                    {
                        test.Fail(ex);
                        
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
        public void H_CancelRequest()
        {

            test = extent.CreateTest("Cancel Maintenance Request");

            expectedStatus = "Passed";
            description = "test case to test Cancel Maintenance Request";
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
                AddMaintenance(driver);
                Thread.Sleep(3000);

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions"))).Click();
                Thread.Sleep(1000);
                IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions4"));
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
