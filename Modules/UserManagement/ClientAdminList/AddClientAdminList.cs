using AventStack.ExtentReports;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RazorEngine.Compilation.ImpromptuInterface.Dynamic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.UserManagement.ClientAdminList;
using VMS_Phase1PortalAT.utls.datas.UserManagement.ClientUserList;
using WindowsInput;
using WindowsInput.Native;

namespace VMS_Phase1PortalAT.Modules.UserManagement.ClientAdminList
{
    [TestClass]
    public class AddClientAdminList
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        private int a;
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
        public void AddClientAdminListSuccess()
        {
            test = extent.CreateTest("Adding the Client Admin");

            expectedStatus = "Passed";
            description = "Test case to check the Add functionality flow of Client Admin List";
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
                ClickElementWhenReady(wait, By.Id("menuItem-User Management0"));

                ClickElementWhenReady(wait, By.XPath("//span[contains(@class,'mat-button-wrapper')]/mat-icon[text()='add']"));
                
                SendKeysWhenReady(wait, By.XPath("//mat-dialog-container//input[@name='name']"), AddClientAdminData.AddClientAdminSuccess["Name"]);
                SendKeysWhenReady(wait, By.XPath("//mat-dialog-container//input[contains(@name,'username')]"), AddClientAdminData.AddClientAdminSuccess["Username"]);
                SendKeysWhenReady(wait, By.XPath("//mat-dialog-container//input[@name='email']"), AddClientAdminData.AddClientAdminSuccess["Email"]);
                SendKeysWhenReady(wait, By.XPath("//mat-dialog-container//input[@name='mobile']"), AddClientAdminData.AddClientAdminSuccess["Mobile"]);
                SendKeysWhenReady(wait, By.XPath("//mat-dialog-container//input[@name='password']"), AddClientAdminData.AddClientAdminSuccess["Password"]);
               
                ClickElementWhenReady(wait, By.XPath("//mat-dialog-container//input[contains(@placeholder,'Client')]"));
                ClickElementWhenReady(wait, By.XPath($"//mat-option//span[contains(text(),'{AddClientAdminData.AddClientAdminSuccess["Client"]}')]"));
                Thread.Sleep(1000);
                
                ClickElementWhenReady(wait, By.XPath("//mat-dialog-container//span[contains(text(),'publish')]"));
                Thread.Sleep(1000);

                var sim = new InputSimulator();
                sim.Keyboard.TextEntry(AddClientAdminData.AddClientAdminSuccess["upload"]);
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN);
                Console.WriteLine("Uploaded a file");
                Thread.Sleep(1000);

                ClickElementWhenReady(wait, By.XPath("//span[text()=' Save ']"));
                
                var snackbarText = wait.Until(drv =>
                {
                    var el = drv.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    return el.Displayed && !string.IsNullOrWhiteSpace(el.Text)
                           ? el.Text
                           : null;
                });

                Console.WriteLine(snackbarText);
                Assert.IsTrue(snackbarText.Contains("Client Admin Added"), "Not Added!!!");


                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));

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
