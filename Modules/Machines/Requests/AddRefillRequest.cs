using AventStack.ExtentReports;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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

namespace VMS_Phase1PortalAT.Modules.Machines.Requests
{
    [TestClass]
    public class AddRefillRequest
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
        public static int colindex;

        [TestInitialize]
        public void Setup()
        {
            //added
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;

            extent = ExtentManager.GetInstance();

        }

        [TestMethod]
        [TestCategory("UI")]
        [TestProperty("TestCaseId", "4401")]


        public void AddRefillRequestSuccess()
        {
            test = extent.CreateTest("Validating add refill request with valid data");
            expectedStatus = "Passed";
            description = "Test case to test add refill request with valid data.";

            Login loginHelper = new Login();
            driver = loginHelper.getdriver();

            try
            {
                loginHelper.LoginSuccessCompanyAdmin();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                IWebElement menu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines")));
                if (menu != null)
                {
                    menu.Click();

                    IWebElement submenu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines2")));
                    if (submenu != null)
                    {
                        submenu.Click();

                        IWebElement tabList = wait.Until(drv => driver.FindElement(By.ClassName("mat-tab-list")));
                        Console.WriteLine(tabList.Text);
                        IWebElement tabLabels = tabList.FindElement(By.ClassName("mat-tab-labels"));
                        IWebElement returnTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'Refill')]")));
                        returnTab.Click();
                        Thread.Sleep(1000);
                        IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-fab")));
                        if (add != null)
                        {
                            Thread.Sleep(1000);
                            add.Click();
                            Thread.Sleep(1000);
                            IWebElement machineId = driver.FindElement(By.Name("machineIds"));
                            machineId.Click();
                           // machineId.SendKeys(AddReturnRequestData.addReturnRequestSuccess["machineIds"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
                            Thread.Sleep(1000);
                            foreach (IWebElement a in options) {
                                if (a.Text.Contains(AddRefillRequestData.addRequestSuccess["machineIds"]))
                                {
                                    a.Click();
                                    break;
                                }
                            }
                            IWebElement next = wait.Until(drv => driver.FindElement(By.XPath("//span[contains(text(),'Next')]")));
                            next.Click();

                            Thread.Sleep(2000);
                            //IWebElement add1 = wait.Until(drv => driver.FindElement(By.XPath("(//mat-form-field//mat-icon[contains(text(),'add')])[1]")));
                            //add1.Click();

                            //IWebElement add2 = wait.Until(drv => driver.FindElement(By.XPath("(//mat-form-field//mat-icon[contains(text(),'add')])[2]")));
                            //add2.Click();

                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            submit.Click();
                            string title = "Refill Request Added";
                            IWebElement snackbar = wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            bool isSuccess = false;
                            Console.WriteLine(snackbar.Text);
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Assert.IsTrue(isSuccess, " failed..");

                            Thread.Sleep(2000);

                            wait.Until(d => { try { var e = d.FindElement(By.Id("menuItem-W. Transactions")); return (e.Displayed && e.Enabled) ? e : null; } catch { return null; } });
                            driver.FindElement(By.Id("menuItem-W. Transactions")).Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions2")));
                            driver.FindElement(By.Id("menuItem-W. Transactions2")).Click();
                            Thread.Sleep(1000);
                            IWebElement Actions = driver.FindElement(By.XPath("(//mat-icon[text() = 'more_vert'])[1]"));
                            Actions.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),' Cancel Request ')]")));
                            driver.FindElement(By.XPath("//button[contains(text(),' Cancel Request ')]")).Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),' Confirm ')]")));
                            Thread.Sleep(1000);
                            driver.FindElement(By.XPath("//span[contains(text(),' Confirm ')]")).Click();
                            Thread.Sleep(1000);




                            //    // Go back to Machines menu
                            //    SafeClick(SafeFindElement(By.Id("menuItem-Machines"), wait), wait);
                            //    SafeClick(SafeFindElement(By.Id("menuItem-Machines2"), wait), wait);
                            //}

                            //    // Proceed to add refill request
                            //    var addBtn = SafeFindElement(By.ClassName("mat-fab"), wait);
                            //    SafeClick(addBtn, wait);

                            //    var machineIdDropdown = SafeFindElement(By.Name("machineIds"), wait);
                            //    SafeClick(machineIdDropdown, wait);

                            //    foreach (var o in SafeFindElements(By.TagName("mat-option"), wait))
                            //    {
                            //        if (o.Text.Contains(AddRefillRequestData.addRequestSuccess["machineIds"]))
                            //        {
                            //            SafeClick(o, wait);
                            //            break;
                            //        }
                            //    }

                            //    var nextBtn = SafeFindElement(By.ClassName("mat-raised-button"), wait);
                            //    if (nextBtn.Enabled)
                            //    {
                            //        SafeClick(nextBtn, wait);

                            //        var removeBtn = SafeFindElement(By.XPath("//mat-icon[contains(text(),'remove')]"), wait);
                            //        SafeClick(removeBtn, wait);

                            //        var submitBtn = SafeFindElement(By.ClassName("mat-raised-button"), wait);
                            //        SafeClick(submitBtn, wait);

                            //        var snackbar = wait.Until(d => d.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            //        string msg = snackbar.Text;

                            //        if (!msg.Contains("Refill Request Added"))
                            //        {
                            //            test.Fail("Snackbar did not confirm success: " + msg);
                            //            Assert.Fail("Refill request addition failed");
                            //        }

                            //        test.Pass("Refill request added successfully: " + msg);
                            //        return;
                            //    }
                            //}

                            //test.Fail("No matching 'Created' status row found or form could not be submitted.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Exception in test: " + errorMessage);
                throw;
            }
        }

        private IWebElement SafeFindElement(By by, WebDriverWait wait, int retryCount = 2)
        {
            for (int attempt = 0; attempt <= retryCount; attempt++)
            {
                try
                {
                    return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(300);
                }
            }
            throw new NoSuchElementException($"Element not found or stale for locator: {by}");
        }

        private IList<IWebElement> SafeFindElements(By by, WebDriverWait wait, int retryCount = 2)
        {
            for (int attempt = 0; attempt <= retryCount; attempt++)
            {
                try
                {
                    // Note: using FindElements not in WaitHelpers, so use wait.Until + FindElements
                    return wait.Until(drv => drv.FindElements(by));
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(300);
                }
            }
            throw new NoSuchElementException($"Elements not found or stale for locator: {by}");
        }

        private void SafeClick(By by, WebDriverWait wait, int retryCount = 3)
        {
            for (int attempt = 0; attempt <= retryCount; attempt++)
            {
                try
                {
                    // Wait until element is visible
                    IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));

                    // Wait until it's enabled (using polling)
                    wait.Until(driver =>
                    {
                        try
                        {
                            IWebElement e = driver.FindElement(by);
                            return e.Enabled;
                        }
                        catch
                        {
                            return false;
                        }
                    });

                    // Wait until element is clickable and then click
                    IWebElement clickableElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
                    clickableElement.Click();
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(300);
                }
                catch (ElementClickInterceptedException)
                {
                    try
                    {
                        IWebElement jsElement = wait.Until(driver => driver.FindElement(by));
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", jsElement);
                        return;
                    }
                    catch
                    {
                        Thread.Sleep(300);
                    }
                }
                catch (WebDriverException)
                {
                    Thread.Sleep(300);
                }
            }

            throw new Exception($"SafeClick failed after {retryCount + 1} attempts for selector: {by}");
        }


        public void SafeClick(IWebElement element, WebDriverWait wait)
        {
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            }
        }

        public IWebElement SafeFindElement(By by, WebDriverWait wait)
        {
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        private string GetCellText(IWebDriver driver, WebDriverWait wait, int rowIndex, int columnIndex)
        {
            for (int retry = 0; retry < 2; retry++)
            {
                try
                {
                    IWebElement tableBody = wait.Until(drv => drv.FindElement(By.TagName("tbody")));
                    IList<IWebElement> rows = tableBody.FindElements(By.TagName("tr"));

                    if (rowIndex >= rows.Count)
                        throw new IndexOutOfRangeException("Row index out of range.");

                    IList<IWebElement> columns = rows[rowIndex].FindElements(By.TagName("td"));

                    if (columnIndex >= columns.Count)
                        throw new IndexOutOfRangeException("Column index out of range.");

                    return columns[columnIndex].Text.Trim();
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(500); // short pause before retry
                }
            }

            throw new Exception("Failed to get cell text due to stale element reference.");
        }




        

        

        [TestMethod]
        public void AddRefillRequestBtnDisable()
        {

            test = extent.CreateTest("Validating add refill request without required datas");
           // associated 
            expectedStatus = "Passed";
            description = "test case to test add refill request without required datas";
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Machines"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines2")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Machines2"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        if (add != null)
                        {
                            add.Click();
                            Thread.Sleep(1000);
                            IWebElement machineId = driver.FindElement(By.Name("machineIds"));
                            machineId.SendKeys(AddRefillRequestData.addRequestBtnDisable["machineIds"]);
                            Thread.Sleep(2000);
                            
                            
                            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()=' Save ']"))).Click();
                            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            //IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));

                            //if (options[0].Text.Contains(AddRefillRequestData.addRequestBtnDisable["machineIds"]))
                            //{
                            //    options[0].Click();
                            //}

                            
                            IWebElement next = driver.FindElement(By.ClassName("mat-raised-button"));
                            if (next.Enabled)
                            {
                                next.Click();
                                Thread.Sleep(1000);

                                IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                                Assert.IsTrue(submit.GetAttribute("disabled").Equals("true"));

                                test.Pass();
                            }



                            //}
                            //else
                            //{
                            //    Console.WriteLine("doesnt find add null");

                            //        test.Skip();
                            //}
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
