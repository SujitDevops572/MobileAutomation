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
using VMS_Phase1PortalAT.utls.datas.UserManagement.ClientUserList;
using VMS_Phase1PortalAT.utls.datas.UserManagement.ValidationGroupList;
using WindowsInput;
using WindowsInput.Native;

namespace VMS_Phase1PortalAT.Modules.UserManagement.ClientUserLst
{
    [TestClass]
    public class AddClientUserList
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
        public void AddClientUserListSuccess()
        {
            test = extent.CreateTest("Adding the Client user");

            expectedStatus = "Passed";
            description = "Test case to check the Add functionality flow of Client User List";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                LoginSuccess.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                Console.WriteLine("Navigating to User Management > Client User List...");
                ClickElementWhenReady(wait, By.Id("menuItem-User Management"));
                ClickElementWhenReady(wait, By.Id("menuItem-User Management1"));

                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='add'])[1]"));
                SendKeysWhenReady(wait, By.XPath("//input[@name='name']"), AddClientUserListData.AddClientUserListSuccess["Name"]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='email']"), AddClientUserListData.AddClientUserListSuccess["Email"]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='mobile']"), AddClientUserListData.AddClientUserListSuccess["Mobile"]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='emplyeeId']"), AddClientUserListData.AddClientUserListSuccess["Employee Id"]);
                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Client']"));

                ClickElementWhenReady(wait, By.XPath($"//span[contains(text(),'{AddClientUserListData.AddClientUserListSuccess["client"]}')]"));
                ClickElementWhenReady(wait, By.XPath("//span[text()='publish']"));
                Thread.Sleep(1000);
                var sim = new InputSimulator();
                sim.Keyboard.TextEntry(@"C:\Users\Sujit-PC\Downloads\Shooting-in-RAW-TIFF-JPEG-5682ac103df78ccc15bfef42.jpg");
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN); // RETURN is same as ENTER
                Console.WriteLine("Uploaded a file");
                Thread.Sleep(1000);
                SendKeysWhenReady(wait, By.XPath("//input[@name='accessId']"), AddClientUserListData.AddClientUserListSuccess["Access Id"]);
                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Validation Group']"));
                
                ClickElementWhenReady(wait, By.XPath($"//span[contains(text(),'{AddClientUserListData.AddClientUserListSuccess["Group"]}')]"));
                Thread.Sleep(2000);
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Save ']"));
                Thread.Sleep(2000);

                var snackbarText = wait.Until(drv =>
                {
                    var el = drv.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    return el.Displayed && !string.IsNullOrWhiteSpace(el.Text)
                           ? el.Text
                           : null;
                });

                Console.WriteLine(snackbarText);
                Assert.IsTrue(snackbarText.Contains("Added"), "Not Added!!!");


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

        [TestMethod]
        public void AddClientUserListFailure()
        {
            test = extent.CreateTest("Adding the Client user with invalid inputs");

            expectedStatus = "Passed";
            description = "Test case to check the add functionality flow of Client User List";
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
                ClickElementWhenReady(wait, By.Id("menuItem-User Management1"));

                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='add'])[1]"));
                SendKeysWhenReady(wait, By.XPath("//input[@name='name']"), AddClientUserListData.AddClientUserListSuccess[""]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='email']"), AddClientUserListData.AddClientUserListSuccess[""]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='mobile']"), AddClientUserListData.AddClientUserListSuccess["IMobile"]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='emplyeeId']"), AddClientUserListData.AddClientUserListSuccess[""]);
                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Client']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Riota ']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()='publish']"));
                Thread.Sleep(1000);
                var sim = new InputSimulator();
                sim.Keyboard.TextEntry(@"C:\Users\Sujit-PC\Downloads\Shooting-in-RAW-TIFF-JPEG-5682ac103df78ccc15bfef42.jpg");
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN); // RETURN is same as ENTER
                Console.WriteLine("Uploaded a file");
                Thread.Sleep(1000);
                SendKeysWhenReady(wait, By.XPath("//input[@name='accessId']"), AddClientUserListData.AddClientUserListSuccess["Access Id"]);
                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Validation Group']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' bhavya ']"));
                Thread.Sleep(2000);
                IWebElement saveButton = driver.FindElement(By.XPath("//span[text()=' Save ']"));
                Assert.IsTrue(saveButton.Enabled,"Save Button Is Diasbled");
                Thread.Sleep(2000);
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

        [TestMethod]
        public void AddClientUserList_NewValidationGroup()
        {
            test = extent.CreateTest("Adding the Client user with new Validation Group");

            expectedStatus = "Passed";
            description = "Test case to check the add functionality flow of Client User List";
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
                ClickElementWhenReady(wait, By.Id("menuItem-User Management1"));

                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='add'])[1]"));
                SendKeysWhenReady(wait, By.XPath("//input[@name='name']"), AddClientUserListData.AddClientUserListSuccess["VName"]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='email']"), AddClientUserListData.AddClientUserListSuccess["Email"]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='mobile']"), AddClientUserListData.AddClientUserListSuccess["Mobile"]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='emplyeeId']"), AddClientUserListData.AddClientUserListSuccess["Employee Id"]);
                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Client']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Riota ']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()='publish']"));
                Thread.Sleep(1000);
                var sim = new InputSimulator();
                sim.Keyboard.TextEntry(@"C:\Users\Sujit-PC\Downloads\Shooting-in-RAW-TIFF-JPEG-5682ac103df78ccc15bfef42.jpg");
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN); // RETURN is same as ENTER
                Console.WriteLine("Uploaded a file");
                Thread.Sleep(1000);
                SendKeysWhenReady(wait, By.XPath("//input[@name='accessId']"), AddClientUserListData.AddClientUserListSuccess["Access Id"]);


                ClickElementWhenReady(wait, By.XPath("//span[text()=' Add New Validation Group ']"));

                ClickElementWhenReady(wait, By.XPath("(//input[@name='name'])[2]"));

                SendKeysWhenReady(wait, By.XPath("(//input[@name='name'])[2]"), AddClientUserListData.AddNewValidationGroupList["Name"]);
                ClickElementWhenReady(wait, By.XPath("(//input[@placeholder='Client'])[2]"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Riota ']"));

                //is Default
               // ClickElementWhenReady(wait, By.XPath("//input[@role='switch']"));

                //WhiteListing Machine
                ClickElementWhenReady(wait, By.XPath("(//label[@class='mat-radio-label'])[5]"));

                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Machine ID']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' 2VE0000001 ']"));

                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Reset Duration']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Every-Week ']"));

                ClickElementWhenReady(wait, By.XPath("//mat-icon[text()='date_range']"));
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[1]"), AddClientUserListData.AddNewValidationGroupList["hours"]);
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[2]"), AddClientUserListData.AddNewValidationGroupList["mins"]);

                Thread.Sleep(1000);
                ClickElementWhenReady(wait, By.XPath("//span[text()='Set']"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                Thread.Sleep(2000);
                //add-icon
                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='add'])[2]"));

                //WhiteListing Product
                ClickElementWhenReady(wait, By.XPath("(//label[@class='mat-radio-label'])[7]"));
                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Products']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Lays - Tangy Tomato Masala - 20 ']"));

                SendKeysWhenReady(wait, By.XPath("//input[@name='purLimitQty1']"), AddClientUserListData.AddNewValidationGroupList["qty"]);

                ClickElementWhenReady(wait, By.XPath("(//input[@placeholder='Reset Duration'])[2]"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Every-Week ']"));

                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='date_range'])[2]"));
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[1]"), AddClientUserListData.AddNewValidationGroupList["rhours"]);
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[2]"), AddClientUserListData.AddNewValidationGroupList["rmins"]);

                Thread.Sleep(1000);
                ClickElementWhenReady(wait, By.XPath("//span[text()='Set']"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                Thread.Sleep(2000);
                //add-icon
                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='add'])[3]"));

                Thread.Sleep(1000);

                ClickElementWhenReady(wait, By.XPath("(//span[text()=' Save '])[2]"));
                Thread.Sleep(3000);

                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Validation Group']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' kavin groups ']"));
                Thread.Sleep(5000);


               // ClickElementWhenReady(wait, By.XPath("(//span[text()=' Save '])[1]"));
               // Thread.Sleep(3000);
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));

                ClickElementWhenReady(wait, By.XPath("(//span[text()=' Save '])[1]"));

                var snackbarText = wait.Until(drv =>
                {
                    var el = drv.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    return el.Displayed && !string.IsNullOrWhiteSpace(el.Text)
                           ? el.Text
                           : null;
                });

                Console.WriteLine(snackbarText);

                Assert.IsTrue(snackbarText.Contains("Client User Added"), "Not Added!!!");

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
