using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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

namespace VMS_Phase1PortalAT.Modules.UserManagement.ClientUserLst
{
    [TestClass]
    public class EditClientUserList
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
        public void EditClientUserListSuccess()
        {
            test = extent.CreateTest("Edit the Client user");
            expectedStatus = "Passed";
            description = "Test case to check the functionality flow of Edit Client User List";

            Login login = new Login();
            driver = login.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            try
            {
                Console.WriteLine("Starting login process...");
                login.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                Console.WriteLine("Navigating to User Management > Client User List...");

                ClickWhenReady(By.Id("menuItem-User Management"), 40);
                ClickWhenReady(By.Id("menuItem-User Management1"), 40);

                WaitForTableToLoad(wait);
                WaitForOverlayToDisappear(wait, By.ClassName("searchTypeBox"));

                // Search for client
                ClickWhenReady(By.ClassName("searchTypeBox"));

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(By.TagName("mat-option")));

                driver.FindElements(By.TagName("mat-option"))[0].Click();

                SendKeysWhenReady(wait, By.Name("searchText"), EditClientUserListData.EditClientUserList["search edit"]);
                driver.FindElement(By.Name("searchText")).SendKeys(Keys.Enter);

                WaitForTableToLoad(wait);

                // Actions Menu
                ClickWhenReady(By.XPath("(//td)[14]"));
                ClickWhenReady(By.XPath("(//button[@role='menuitem'])[3]"));

                WaitForOverlayToDisappear(wait, By.XPath("//input[@name='name']"));

                Console.WriteLine("Editing Client User...");

                // Edit fields
                SendKeysWhenReady(wait, By.XPath("//input[@name='name']"), EditClientUserListData.EditClientUserList["Name"]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='email']"), EditClientUserListData.EditClientUserList["Email"]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='mobile']"), EditClientUserListData.EditClientUserList["Mobile"]);

                // Whitelisting Machine
                ClickWhenReady(By.XPath("(//label[@class='mat-radio-label'])[1]"));
                ClickWhenReady(By.XPath("//input[@placeholder='Machine ID']"));
                ClickWhenReady(By.XPath("(//mat-option[@role='option'])[1]"));

                ClickWhenReady(By.XPath("//input[@placeholder='Reset Duration']"));
                ClickWhenReady(By.XPath("//span[text()=' Every-Week ']"));

                ClickWhenReady(By.XPath("//mat-icon[text()='date_range']"));
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[1]"),
                    AddClientUserListData.AddNewValidationGroupList["hours"]);
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[2]"),
                    AddClientUserListData.AddNewValidationGroupList["mins"]);
                ClickWhenReady(By.XPath("//span[text()='Set']"));

                ClickWhenReady(By.XPath("(//mat-icon[text()='add'])[2]"));

                // Whitelisting Product
                ClickWhenReady(By.XPath("(//label[@class='mat-radio-label'])[3]"));
                ClickWhenReady(By.XPath("//input[@placeholder='Products']"));
                ClickWhenReady(By.XPath("(//mat-option[@role='option'])[1]"));

                SendKeysWhenReady(wait, By.XPath("//input[@name='purLimitQty1']"),
                    EditClientUserListData.EditClientUserList["qty"]);

                ClickWhenReady(By.XPath("(//input[@placeholder='Reset Duration'])[2]"));
                ClickWhenReady(By.XPath("(//mat-option[@role='option'])[1]"));

                ClickWhenReady(By.XPath("(//mat-icon[text()='date_range'])[2]"));

                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[1]"),
                    EditClientUserListData.EditClientUserList["ehours"]);
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[2]"),
                    EditClientUserListData.EditClientUserList["emins"]);

                ClickWhenReady(By.XPath("//span[text()='Set']"));

                // Add
                ClickWhenReady(By.XPath("(//mat-icon[text()='add'])[3]"));

                // Delete
                ClickWhenReady(By.XPath("(//mat-icon[text()='delete'])[1]"));

                // Save
                ClickWhenReady(By.XPath("//span[text()=' Save ']"));

                // Snackbar
                var snackbarText = wait.Until(d =>
                {
                    var els = d.FindElements(By.CssSelector(".mat-snack-bar-container"));
                    if (els.Count == 0) return null;
                    var text = els[0].Text?.Trim();
                    return string.IsNullOrWhiteSpace(text) ? null : text;
                });

                Console.WriteLine(snackbarText);
                Assert.IsTrue(snackbarText.Contains("Client User Updated"), "Not Edited!!!");

                test.Pass("Client User edited successfully.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;

                Console.WriteLine("Test failed with error: " + errorMessage);
                test.Fail(errorMessage);
                expectedStatus = "Failed";
                throw;
            }
        }

        // ---------------------------------------------------------
        // WAIT FOR OVERLAY – DYNAMIC CUTOFF BASED ON NEXT ELEMENT
        // ---------------------------------------------------------
        private void WaitForOverlayToDisappear(WebDriverWait wait, By nextLocator, int timeoutInSeconds = 20)
        {
            try
            {
                // Function to detect whether any blocking overlay is visible
                Func<IWebDriver, bool> IsOverlayVisible = (d) =>
                {
                    try
                    {
                        var overlays = d.FindElements(By.XPath(
                            "//*[contains(@class,'cdk-overlay-backdrop') or " +
                            "contains(@class,'loading') or contains(@class,'loader') or " +
                            "contains(@class,'overlay') or contains(@class,'spinner') or " +
                            "@aria-busy='true']"
                        ));

                        foreach (var o in overlays)
                        {
                            if (!o.Displayed) continue;

                            string cls = o.GetAttribute("class") ?? "";
                            string style = o.GetAttribute("style") ?? "";

                            // Ignore Angular Mat Dialog Containers
                            if (cls.Contains("mat-dialog-container")) continue;

                            // If visible & not transparent → treat as real overlay
                            if (!style.Contains("opacity: 0") && !style.Contains("visibility: hidden"))
                                return true;
                        }
                    }
                    catch { }

                    return false;
                };


                // STEP 1 — No overlay → return immediately
                if (!IsOverlayVisible(driver))
                    return;


                // STEP 2 — Overlay detected → dynamic waiting
                var overlayWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(250)
                };

                overlayWait.Until(d =>
                {
                    bool overlayStillVisible = IsOverlayVisible(d);

                    try
                    {
                        var el = d.FindElement(nextLocator);

                        // If the next element becomes ready → cut off overlay wait immediately
                        if (el.Displayed && el.Enabled)
                            return true;
                    }
                    catch
                    {
                        // Ignore and keep waiting
                    }

                    // Continue waiting while overlay visible
                    return !overlayStillVisible;
                });
            }
            catch
            {
                return; // never fail test due to overlay detection issues
            }
        }



        // ---------------------------------------------------------
        // SEND KEYS WHEN READY — SAFE INTERACTION + AUTO OVERLAY HANDLING
        // ---------------------------------------------------------
        private void SendKeysWhenReady(WebDriverWait wait, By locator, string textToEnter, int timeoutInSeconds = 30)
        {
            // Overlay handled dynamically with locator awareness
            WaitForOverlayToDisappear(wait, locator);

            var localWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(300)
            };

            bool success = localWait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);

                    if (element.Displayed && element.Enabled)
                    {
                        ((IJavaScriptExecutor)d).ExecuteScript(
                            "arguments[0].scrollIntoView({block:'center'});", element);

                        ((IJavaScriptExecutor)d).ExecuteScript("arguments[0].focus();", element);

                        Thread.Sleep(150); // stabilize

                        element.Clear();
                        element.SendKeys(textToEnter);

                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            });

            if (!success)
                throw new Exception($"Failed to send keys to element: {locator}");
        }




        private void WaitForTableToLoad(WebDriverWait wait, int timeoutInSeconds = 20)
        {
            var tableWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(300)
            };

            tableWait.Until(d =>
            {
                try
                {
                    // Find all table rows
                    var rows = d.FindElements(By.XPath("//table//tbody//tr"));

                    // Return true if at least one row is displayed
                    return rows.Any(r => r.Displayed);
                }
                catch
                {
                    return false; // Retry if DOM changes
                }
            });
        }





        // ---------------------------------------------------------
        // CLICK WHEN READY — NORMAL CLICK ONLY + AUTO OVERLAY HANDLING
        // ---------------------------------------------------------
        private void ClickWhenReady(By locator, int timeoutInSeconds = 40)
        {
            // Wait for overlay ONLY if it exists – dynamic exit when next element becomes ready
            WaitForOverlayToDisappear(new WebDriverWait(driver, TimeSpan.FromSeconds(5)), locator);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(300)
            };

            bool clicked = wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);

                    if (element.Displayed && element.Enabled)
                    {
                        ((IJavaScriptExecutor)d).ExecuteScript(
                            "arguments[0].scrollIntoView({block:'center'});", element);

                        ((IJavaScriptExecutor)d).ExecuteScript("arguments[0].focus();", element);

                        Thread.Sleep(100);

                        element.Click(); // ***NORMAL CLICK ONLY***
                        return true;
                    }

                    return false;
                }
                catch
                {
                    return false; // retry
                }
            });

            if (!clicked)
                throw new Exception($"Failed to click element: {locator}");
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





       




    }
}

