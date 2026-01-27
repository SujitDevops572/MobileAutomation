using AventStack.ExtentReports;
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
using VMS_Phase1PortalAT.utls.datas.UserManagement.ValidationGroupList;

namespace VMS_Phase1PortalAT.Modules.UserManagement.ValidationGroupList
{
    [TestClass]
    public class AddValidationGroupList
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
        public void AddValidationGroupListSuccess()
        {
            test = extent.CreateTest("Check the Validation Group List");

            expectedStatus = "Passed";
            description = "Test case to check the functionality flow of Validation Group List";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                LoginSuccess.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                Console.WriteLine("Navigating to User Management > Client Admin List...");
                ClickElementWhenReady(wait, By.Id("menuItem-User Management"));
                ClickElementWhenReady(wait, By.Id("menuItem-User Management2"));

                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='add'])[1]"));
                ClickElementWhenReady(wait, By.Name("name"));

                SendKeysWhenReady(wait, By.Name("name"), AddValidationGroupListData.AddValidationGroupListSuccess["Name"]);
                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Client']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' IndiQube ']"));

                //is Default
                ClickElementWhenReady(wait, By.XPath("(//mat-slide-toggle//div)[1]"));

                //WhiteListing Machine
                ClickElementWhenReady(wait, By.XPath("(//label[@class='mat-radio-label'])[1]"));

                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Machine ID']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' STE0000082 ']"));

                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Reset Duration']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Every-Week ']"));

                ClickElementWhenReady(wait, By.XPath("//mat-icon[text()='date_range']"));
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[1]"), AddValidationGroupListData.AddValidationGroupListSuccess["hours"]);
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[2]"), AddValidationGroupListData.AddValidationGroupListSuccess["mins"]);

                Thread.Sleep(1000);
                ClickElementWhenReady(wait, By.XPath("//span[text()='Set']"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                Thread.Sleep(2000);
                //add-icon
                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='add'])[2]"));

                //WhiteListing Product
                ClickElementWhenReady(wait, By.XPath("(//label[@class='mat-radio-label'])[3]"));
                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Products']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Lays Spanish Tomato Tango ']"));

                SendKeysWhenReady(wait, By.XPath("//input[@name='purLimitQty1']"), AddValidationGroupListData.AddValidationGroupListSuccess["qty"]);

                ClickElementWhenReady(wait, By.XPath("(//input[@placeholder='Reset Duration'])[2]"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Every-Week ']"));

                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='date_range'])[2]"));
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[1]"), AddValidationGroupListData.AddValidationGroupListSuccess["rhours"]);
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[2]"), AddValidationGroupListData.AddValidationGroupListSuccess["rmins"]);

                Thread.Sleep(1000);
                ClickElementWhenReady(wait, By.XPath("//span[text()='Set']"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                Thread.Sleep(2000);
                //add-icon
                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='add'])[3]"));

                Thread.Sleep(1000);
                //ClickElementWhenReady(wait, By.XPath("//span[text()=' Save ']"));
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                }

                Console.WriteLine("Test failed with error: " + errorMessage);
                test.Fail(errorMessage);
                expectedStatus = "Failed";
                throw;
            }
        }
            [TestCleanup]
            public void Cleanup()
            {
                stopwatch.Stop();
                TimeSpan timeTaken = stopwatch.Elapsed;
                string formattedTime = $"{timeTaken.TotalSeconds:F2}";
                try
                {
                    driver.Quit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error quitting driver: " + e.Message);
                }

                Console.WriteLine($"Test finished. Time taken: {formattedTime} seconds. Status: {expectedStatus}");

                testResult.WriteTestResults(TestContext, formattedTime, expectedStatus, errorMessage, description);
                extent.Flush();
            }
        
        private void ClickElementWhenReady(WebDriverWait wait, By locator)
        {
            wait.Until(driver =>
            {
                try
                {
                    var overlays = driver.FindElements(By.CssSelector("div.overlay"));
                    if (overlays.Any(o => o.Displayed))
                        return false;

                    var element = driver.FindElement(locator);
                    if (element.Displayed && element.Enabled)
                    {
                        element.Click();
                        return true;
                    }
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
                catch (ElementClickInterceptedException)
                {
                    return false;
                }
            });
        }
            private void SendKeysWhenReady(WebDriverWait wait, By locator, string textToEnter)
        {
            wait.Until(driver =>
            {
                try
                {
                    // Wait for overlays to disappear
                    var overlays = driver.FindElements(By.CssSelector("div.overlay"));
                    if (overlays.Any(o => o.Displayed))
                    {
                        Console.WriteLine("Overlay detected; waiting...");
                        return false;
                    }

                    var element = driver.FindElement(locator);
                    if (element.Displayed && element.Enabled)
                    {
                        element.Clear();
                        element.SendKeys(textToEnter);
                        Console.WriteLine($"Sent keys to element {locator}: {textToEnter}");
                        return true;
                    }

                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"StaleElementReferenceException for {locator}; retrying...");
                    return false;
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"NoSuchElementException for {locator}; retrying...");
                    return false;
                }
            });
        }

    }
}
