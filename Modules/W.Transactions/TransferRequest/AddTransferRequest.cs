using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.TransferRequest
{
    [TestClass]
    public class AddTransferRequest
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
        public void AddTransfer()
        {
            test = extent.CreateTest("Add Transfer Request");
            expectedStatus = "Passed";
            description = "Check functionality of add Transfer Request";

            Console.WriteLine("Starting test: AddTransfer");

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
                    return driver.FindElements(By.Id("menuItem-W. Transactions")).Any();
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions")));
                driver.FindElement(By.Id("menuItem-W. Transactions")).Click();

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions1")));
                driver.FindElement(By.Id("menuItem-W. Transactions1")).Click();
                Console.WriteLine("Sub-menu clicked.");

                Console.WriteLine("Opening Add button...");
                IWebElement addButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                addButton.Click();

                Console.WriteLine("Selecting warehouse...");
                IWebElement warehouseSelect = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("warehouse")));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", warehouseSelect);
                warehouseSelect.Click();

                // Wait for options to appear
                var warehouseOption = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(By.XPath("//span[contains(text(), 'JP Hyderabad')]")));
                Console.WriteLine("Clicking warehouse option: JP Hyderabad");
                warehouseOption.Click();

                Thread.Sleep(500); // Small pause if needed
                Console.WriteLine("Selecting product...");
                IWebElement productSelect = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("productId")));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", productSelect);
                productSelect.Click();
                var productOption = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(By.XPath("//span[contains(text(), 'masala')]")));
                productOption.Click();

                Console.WriteLine("Entering quantity...");
                IWebElement qty = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("qty")));
                qty.SendKeys(AddTransferRequestData.ExpiryDate["qty"]);

                Console.WriteLine("Clicking add icon...");
                IList<IWebElement> addIcons = driver.FindElements(By.XPath("//mat-icon[text()='add']"));
                addIcons[1].Click();

                Thread.Sleep(1000);
                Console.WriteLine("Clicking Save...");
                IWebElement saveBtn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()=' Save ']")));
                saveBtn.Click();

                Console.WriteLine("Transfer request added successfully.");
                test.Pass("AddTransfer completed successfully.");
            }
            catch (Exception ex)
            {
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
