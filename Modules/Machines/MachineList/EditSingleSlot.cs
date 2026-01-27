using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
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
using VMS_Phase1PortalAT.utls.datas.Warehouse.WarehouseList;

namespace VMS_Phase1PortalAT.Modules.Machines.MachineList
{
    [TestClass]
    public class EditSingleSlot
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
        private string s1;

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
            test = extent.CreateTest("Validating edit Single Slot");

            expectedStatus = "Passed";
            description = "Test case for editing a single slot. Update EditSingleSlotData before running.";

            Login loginObj = new Login();
            driver = loginObj.getdriver();

            try
            {
                loginObj.LoginSuccessCompanyAdmin();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                throw;
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                // Navigate to Machines menu
                SafeClick(By.Id("menuItem-Machines"), wait);
                SafeClick(By.Id("menuItem-Machines0"), wait);

                // Select search type
                SafeClick(By.ClassName("searchTypeBox"), wait);
                SafeClick(By.TagName("mat-option"), wait);

                // Enter search value
                IWebElement searchInput = wait.Until(d => d.FindElement(By.Name("searchText")));
                SafeClear(searchInput);
                searchInput.SendKeys(EditSingleSlotData.EditSingleSlotSuccess["machine id"]);
                searchInput.SendKeys(Keys.Enter);

                // Wait for table to load
                IWebElement table = WaitForTableToLoad(wait);
                IList<IWebElement> rows = table.FindElements(By.TagName("tr"));

                if (rows.Count == 0)
                {
                    Console.WriteLine("No rows in table.");
                    test.Skip();
                    return;
                }

                // Click last column of first row
                SafeClick(By.XPath("//tbody/tr[1]/td[last()]"), wait);

                // Click "View Details"
                SafeClick(By.XPath("//button[contains(.,'View Details')]"), wait);

                // Wait for slotRow to appear (dialog fully loaded)
                IWebElement slotRow = WaitForSlotRow(wait);
                Console.WriteLine("Waited for slot row count");
                // Click edit icon
                
                By editSlotBtn = By.XPath("(//app-slot-info[1]//mat-card)[1]");
                SafeHoverClick(editSlotBtn, wait, driver);

                // Now dialog appears, continue with actions...

                // Dialog
                // IWebElement dialog = wait.Until(d => d.FindElement(By.TagName("mat-dialog-container")));

                // Fields
                Func<IWebElement> UserLimitField = () => wait.Until(d => d.FindElement(By.Name("purchaseLimitPerUser")));
                Func<IWebElement> TransactionLimitField = () => wait.Until(d => d.FindElement(By.Name("purchaseLimitPerTransaction")));

                Thread.Sleep(500);
                string oldValue = UserLimitField().GetAttribute("value");
                Console.WriteLine("Old Value of Purchase Limit user = " + oldValue);

                string newUserLimit;
                string newTxnLimit;

                if (oldValue == EditSingleSlotData.EditSingleSlotSuccess1["purchaseLimitPerUser"])
                {
                    newUserLimit = EditSingleSlotData.EditSingleSlotSuccess["purchaseLimitPerUser"];
                    newTxnLimit = EditSingleSlotData.EditSingleSlotSuccess["purchaseLimitPerTransaction"];
                    
                }
                else
                {
                    newUserLimit = EditSingleSlotData.EditSingleSlotSuccess1["purchaseLimitPerUser"];
                    newTxnLimit = EditSingleSlotData.EditSingleSlotSuccess1["purchaseLimitPerTransaction"];
                }

                // Clear + Type
                SafeClear(UserLimitField());
                UserLimitField().SendKeys(newUserLimit);
                Console.WriteLine("1");
                SafeClear(TransactionLimitField());
                TransactionLimitField().SendKeys(newTxnLimit);
                Console.WriteLine("10");
                string updatedUserValue = UserLimitField().GetAttribute("value");

                // Click submit button
                // SafeClick(By.ClassName("mat-raised-button"), wait);
                IWebElement submit = wait.Until(d => d.FindElement(By.XPath("//mat-dialog-container//button[.//span[contains(text(),'Save')]]")));
                submit.Click();
                // Verify snackbar
                IWebElement snackbar = wait.Until(d => d.FindElement(By.CssSelector(".mat-snack-bar-container")));
                bool isSuccess = snackbar.Text.Contains("Slot Info Updated");

                Console.WriteLine("New Value of Purchase Limit user = " + updatedUserValue);
                Assert.IsTrue(isSuccess, "Slot Update Failed");

                test.Pass();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail(errorMessage);
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









        // ------------------ SUPPORT METHODS ------------------

        private void SafeClick(By locator, WebDriverWait wait)
        {
            wait.Until(driver =>
            {
                try
                {
                    IWebElement element = null;

                    // Try finding the element first
                    try
                    {
                        element = driver.FindElement(locator);
                        if (element.Displayed && element.Enabled)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript(
                                "arguments[0].scrollIntoView({block: 'center'});", element);
                            try { element.Click(); return true; }
                            catch { ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element); return true; }
                        }
                    }
                    catch { /* element not found yet */ }

                    // If element not ready, wait for overlays to disappear
                    var overlays = driver.FindElements(By.CssSelector(".cdk-overlay-backdrop"));
                    if (overlays.Any(o => o.Displayed)) return false; // retry

                    // Re-find element after overlays gone
                    element = driver.FindElement(locator);
                    if (element.Displayed && element.Enabled)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript(
                            "arguments[0].scrollIntoView({block: 'center'});", element);
                        try { element.Click(); return true; }
                        catch { ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element); return true; }
                    }

                    return false; // retry
                }
                catch { return false; } // retry
            });
        }


        public static void SafeHoverClick(By locator, WebDriverWait wait, IWebDriver driver)
        {
            const int retries = 3;  // Retry in case of stale element or intercept
            for (int i = 0; i < retries; i++)
            {
                try
                {
                    // Wait until element is visible and clickable
                    IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
                    Console.WriteLine("Hover element located");
                    // Scroll into view
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);
                    Console.WriteLine("scrolled to Hover element");
                    // Hover using Actions
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(element)
                           .Pause(TimeSpan.FromMilliseconds(500)).Build()
                           .Perform();

                    // Optional: wait a tiny bit to avoid overlay issues
                    Thread.Sleep(400);
                   
                    // Click the element
                    IWebElement edit1 = wait.Until(d => d.FindElement(By.XPath("(//button[contains(@class,'edit_slot')])[1]")));
                    edit1.Click();
                    return; // success, exit
                }
                catch (StaleElementReferenceException)
                {
                    // Retry
                }
                catch (ElementClickInterceptedException)
                {
                    Thread.Sleep(500); // wait and retry
                }
            }

            throw new Exception("SafeHoverClick failed after multiple attempts for locator: " + locator);
        }

        private IWebElement WaitForTableToLoad(WebDriverWait wait)
        {
            return wait.Until(driver =>
            {
                try
                {
                    IWebElement table = driver.FindElement(By.TagName("tbody"));
                    var rows = table.FindElements(By.TagName("tr"));
                    return rows.Count > 0 ? table : null;
                }
                catch
                {
                    return null;
                }
            });
        }

        private IWebElement WaitForSlotRow(WebDriverWait wait)
        {
            return wait.Until(driver =>
            {
                try
                {
                    // Wait for overlay to disappear
                    var overlays = driver.FindElements(By.CssSelector(".cdk-overlay-backdrop"));
                    foreach (var overlay in overlays)
                    {
                        if (overlay.Displayed)
                            return null;
                    }

                    var elements = driver.FindElements(By.XPath("//div[contains(@class,'slotRow')]"));
                    return elements.Count > 0 && elements[0].Displayed ? elements[0] : null;
                }
                catch
                {
                    return null;
                }
            });
        }

        private void SafeClear(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].value='';", element);
            Thread.Sleep(100); // small delay to ensure clearing
        }
    }
}