using AventStack.ExtentReports;
using DocumentFormat.OpenXml.Wordprocessing;
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
using VMS_Phase1PortalAT.utls.datas.PurchaseOrder.CurrentPurchaseOrder;
using VMS_Phase1PortalAT.utls.utilMethods;
using WindowsInput;
using WindowsInput.Native;

namespace VMS_Phase1PortalAT.Modules.PurchaseOrder.CurrentPurchaseOrder
{
    [TestClass]
    public class FlowTest
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
        public void FlowCurrentPurchaseOrder()
        {
            test = extent.CreateTest("Export Current Purchase Order");
            expectedStatus = "Passed";
            description = "Test case to verify export functionality in Current Purchase Order";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    LoginSuccess.LoginSuccessCompanyAdmin();
                    return driver.FindElements(By.Id("menuItem-Purchase Order")).Any();
                });

                Console.WriteLine("Login successful and main menu is visible.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Login failed: " + errorMessage);
                throw;
            }
            try
            {
                Console.WriteLine("Navigating to PurchaseOrder...");
                IWebElement menu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("menuItem-Purchase Order")));
                menu.Click();

                Console.WriteLine("Navigating to PurchaseOrder - CurrentPurchaseOrder...");
                IWebElement subMenu = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("menuItem-Purchase Order0")));
                subMenu.Click();

                Console.WriteLine("Opening Options...");
                IWebElement Options = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//td)[13]")));
                Options.Click();

                Console.WriteLine("Opening View...");
                IWebElement View = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//button[@role='menuitem'])[1]")));
                View.Click();
                Thread.Sleep(1000);

                Console.WriteLine("Closing View...");
                IWebElement Close = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Close']")));
                Close.Click();

                Thread.Sleep(2000);
                Options.Click();
                Thread.Sleep(2000);

                //Console.WriteLine("Opening Move To Scrap...");
                //IWebElement MoveToScrap = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//button[@role='menuitem'])[2]")));
                //MoveToScrap.Click();
                //Thread.Sleep(1000);
                ////Console.WriteLine("Navigating to PurchaseOrder - CurrentPurchaseOrder...");
                ////IWebElement Confirm = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()=' Confirm ']")));
                ////Confirm.Click();

                //Console.WriteLine("Navigating to PurchaseOrder - CurrentPurchaseOrder...");
                //IWebElement Cancel = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Cancel']")));
                //Cancel.Click();

                //Thread.Sleep(2000);
                //Options.Click();
                //Thread.Sleep(2000);


                Console.WriteLine("Opening Renew...");
                IWebElement Renew = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//button[@role='menuitem'])[3]")));
                Renew.Click();
                Console.WriteLine("Renew clicked...");

                DateSelect.DateTimeActions(driver, RenewCurrentPurchaseOrder.RenewCurrentPurchaseOrderBill, 1);

                DateSelect.DateTimeActions(driver,RenewCurrentPurchaseOrder.RenewCurrentPurchaseOrderExpiry, 2);

                var upload = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                   .ElementToBeClickable(By.XPath("//mat-icon[text()='cloud_upload']")));
                upload.Click();
                Thread.Sleep(500); // Small pause if needed
                var sim = new InputSimulator();
                sim.Keyboard.TextEntry(@"C:\Users\Sujit-PC\Downloads\doc-15561756989552535.pdf");
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN); // RETURN is same as ENTER
                Console.WriteLine("Uploaded a file");
                Thread.Sleep(1000);

                IWebElement Save = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div//span[text()=' Save ']")));
                Save.Click();

                // Snackbar --> Purchase Added
            }
            catch (Exception ex) {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("Test failed: " + errorMessage);
                test.Fail(errorMessage);
                throw;
            }
        }
            [TestCleanup]
            public void Cleanup()
            {
                stopwatch.Stop();
                driver?.Quit();
                string timeTaken = $"{stopwatch.Elapsed.TotalSeconds:F2}";
                testResult.WriteTestResults(TestContext, timeTaken, expectedStatus, errorMessage, description);
                extent.Flush();
                Console.WriteLine("Test finished. Time taken: " + timeTaken + "s");
            }
        }
    } 
