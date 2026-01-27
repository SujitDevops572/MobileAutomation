using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
    public class AddCurrentPurchaseOrder
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        public TestContext TestContext { get; set; }
        IWebDriver driver;
        setupData setupDatas = new setupData();
        WriteResultToCSV testResult = new WriteResultToCSV();
        private static ExtentReports extent;
        private static ExtentTest test;

        [TestInitialize]
        public void Setup()
        {
            stopwatch = Stopwatch.StartNew();
            errorMessage = string.Empty;
            extent = ExtentManager.GetInstance();
        }

        [TestMethod]
        public void AddCurrentPurchaseOrderSuccess()
        {
            {
                test = extent.CreateTest("Add Current Purchase Order Success");
                expectedStatus = "Passed";
                description = "Check functionality of Add Current Purchase Order Success";

                Console.WriteLine("Starting test:Add Current Purchase Order Success");

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

                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                    Console.WriteLine("Navigating to Transfer menu...");
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Purchase Order")));
                    driver.FindElement(By.Id("menuItem-Purchase Order")).Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Purchase Order0")));
                    driver.FindElement(By.Id("menuItem-Purchase Order0")).Click();
                    Console.WriteLine("Sub-menu clicked.");
                    Thread.Sleep(2000);

                    Console.WriteLine("Opening Add button...");
                    IWebElement addButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                    addButton.Click();

                    DateSelect.DateTimeActions(driver, AddCurrentPurchaseOrderData.addCurrentPurchaseOrderBill,1);

                    // Wait for options to appear
                    var addClient = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementToBeClickable(By.XPath("//input[@name='client']")));
                    Console.WriteLine("Clicking client");
                    addClient.Click();

                    var addClientOption = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                      .ElementToBeClickable(By.XPath("//span[text()=' IndiQube ']")));
                    addClientOption.Click();    
                    Thread.Sleep(500); // Small pause if needed
                    Console.WriteLine("Selecting product...");

                    var upload = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(By.XPath("//mat-icon[text()='cloud_upload']")));
                    upload.Click();
                    Thread.Sleep(500); // Small pause if needed

                    var sim = new InputSimulator();
                    sim.Keyboard.TextEntry(@"C:\Users\Sujit-PC\Downloads\doc-10281756885754083.pdf");
                    sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN); // RETURN is same as ENTER
                    Console.WriteLine("Uploaded a file");


                   
                   DateSelect.DateTimeActions(driver, AddCurrentPurchaseOrderData.addCurrentPurchaseOrderExpiry,2);
                    Console.WriteLine("Expiry Date");


                    var machineId = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementToBeClickable(By.XPath("//input[@name='machineId']")));
                    machineId.Click();
                    var machineIdOption = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                       .ElementToBeClickable(By.XPath("//span[text()=' 2VE0000110 ']")));
                    machineIdOption.Click();
                    Thread.Sleep(1000);
                    var addBtn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                       .ElementToBeClickable(By.XPath("(//mat-icon[text()='add'])[2]")));
                    addBtn.Click();

                    Thread.Sleep(3000);

                    Console.WriteLine("Clicking Save...");
                    IWebElement saveBtn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()=' Save ']")));
                    saveBtn.Click();

                    Console.WriteLine("Transfer request added successfully.");
                    test.Pass("Add PO completed successfully.");
                }
                catch (Exception ex)
                {
                    errorMessage = ex.InnerException?.Message ?? ex.Message;
                    Console.WriteLine("Test failed: " + errorMessage);
                    test.Fail(errorMessage);
                    throw;
                }
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
