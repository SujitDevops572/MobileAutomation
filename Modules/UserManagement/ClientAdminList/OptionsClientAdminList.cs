using AventStack.ExtentReports;
using FlaUI.Core.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.UserManagement.ClientAdminList;

namespace VMS_Phase1PortalAT.Modules.UserManagement.ClientAdminList
{
    [TestClass]
    public class OptionsClientAdminList
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
        public void ResetPasswordClientAdmin()
        {
            test = extent.CreateTest("Reset the password for Client Admin List");

            expectedStatus = "Passed";
            description = "Test case to check the functionality reset the password of Client Admin List";
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

                WaitForTableToLoad(wait);

                Console.WriteLine("Clicking Actions button...");
                ClickElementWhenReady(wait, By.XPath("(//tbody/tr)[1]/td[last()]"));

                Console.WriteLine("Clicking Reset Password...");
                ClickElementWhenReady(wait, By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Reset Password')]"));

                Console.WriteLine("Entering new password...");
                By passwordInputLocator = By.XPath("//input[@name='newPassword']");
                IWebElement resetBox = WaitForElementVisible(wait, passwordInputLocator);
                resetBox.Clear();
                resetBox.SendKeys(SearchClientAdminListData.ResetClientAdminList["reset"]);

                Console.WriteLine("Saving new password...");
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Save ']"));

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Client admin password updated"), " failed..");

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
        public void MenuAccess()
        {
            test = extent.CreateTest("Menu Access for Client Admin List");

            expectedStatus = "Passed";
            description = "Test case to check the functionality of Menu Access for Client Admin List";
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

                WaitForTableToLoad(wait);

                Console.WriteLine("Clicking Actions button again after DOM update...");
                ClickElementWhenReady(wait, By.XPath("(//tbody/tr)[1]/td[last()]"));

                Console.WriteLine("Opening Menu Access...");
                ClickElementWhenReady(wait, By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Menu Access Info')]"));

                Console.WriteLine("-----Selecting menu access checkboxes-----");

                Console.WriteLine("Clicking Products...");
                for(int i=0;i < AccessData.MenuArrowClientAdminListSuccess.Count(); i++) {

                    String a = AccessData.MenuArrowClientAdminListSuccess[i];
                    test.Info($"Clicking {i}  {"-"}  {a}");
                    ClickElementWhenReady(wait, By.XPath($"//mat-dialog-container//span[contains(text(),'{a}')]/preceding::button[1]"));

                }

                Console.WriteLine("Selecting Product checkboxes...");
                for (int i = 0; i < AccessData.MenuCheckboxClientAdminListSuccess.Count(); i++)
                {

                    String a = AccessData.MenuCheckboxClientAdminListSuccess[i];
                    test.Info($"Clicking {a}");
                    ClickElementWhenReady(wait, By.XPath($"//mat-dialog-container//span[contains(text(),'{a}')]"));

                }

                //ClickElementWhenReady(wait, By.XPath("//mat-dialog-container//span[contains(text(),'Brands')]"));
                //ClickElementWhenReady(wait, By.XPath("//mat-dialog-container//span[contains(text(),'Categories')]"));

                Console.WriteLine("Saving menu access changes...");
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Save ']"));

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Access Info Updated"), " failed..");

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
        public void FeatureAccess()
        {
            test = extent.CreateTest("Feature Access for Client Admin");

            expectedStatus = "Passed";
            description = "Test case to check the functionality of Feature Access for Client Admin List";
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

                WaitForTableToLoad(wait);

                Console.WriteLine("Clicking Actions button again for feature access...");
                ClickElementWhenReady(wait, By.XPath("(//tbody/tr)[1]/td[last()]"));

                Console.WriteLine("Opening Feature Access...");
                ClickElementWhenReady(wait, By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Feature Access Info')]"));


                Console.WriteLine("Clicking Feature Access Arrows and checkboxes...");
                for (int i = 0; i < AccessData.FeatureArrowClientAdminListSuccess.Count(); i++)
                {                   
                    String a = AccessData.FeatureArrowClientAdminListSuccess[i];
                    test.Info($"Clicking {i}  {"-"}  {a}");
                    ClickElementWhenReady(wait, By.XPath($"//mat-dialog-container//span[contains(text(),'{a}')]/preceding::button[1]"));
                    for (int j = 0; j < AccessData.FeatureCheckboxClientAdminListSuccess.Count(); j++) {
                        String b = AccessData.FeatureCheckboxClientAdminListSuccess[i][j];
                        test.Info($"Clicking {b}");
                        ClickElementWhenReady(wait, By.XPath($"//mat-dialog-container//span[contains(text(),'{b}')]"));

                    }
                    Thread.Sleep(400);
                }


                //Console.WriteLine("Expanding feature - brands Page...");
                //ClickElementWhenReady(wait, By.XPath("//mat-dialog-container//span[contains(text(),'Brands Page')]/preceding::button[1]"));

                Console.WriteLine("Saving feature access...");
                ClickElementWhenReady(wait, By.XPath("//span[contains(text(),'Save')]"));

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Feature Access Info Updated"), " failed..");

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
        public void Block()
        {
            test = extent.CreateTest("Blocking Client Admin");

            expectedStatus = "Passed";
            description = "Test case to check the functionality of Blocking Client Admin";
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

                WaitForTableToLoad(wait);

                Console.WriteLine("Opening Block Option...");
                ClickElementWhenReady(wait, By.XPath("(//tbody/tr)[1]/td[last()]"));

                ClickElementWhenReady(wait, By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Block')]"));
                ClickElementWhenReady(wait, By.XPath("//span[contains(text(),'Confirm')]"));

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Client Admin Blocked"), " failed..");
             

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
        public void UnBlock()
        {
            test = extent.CreateTest("UnBlocking Client Admin");

            expectedStatus = "Passed";
            description = "Test case to check the functionality of UnBlocking Client Admin";
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

                WaitForTableToLoad(wait);

                Console.WriteLine("Opening UnBlock Option...");
                ClickElementWhenReady(wait, By.XPath("(//tbody/tr)[1]/td[last()]"));

                ClickElementWhenReady(wait, By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Unblock')]"));
                ClickElementWhenReady(wait, By.XPath("//span[contains(text(),'Confirm')]"));

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Client Admin Unblocked"), " failed..");


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
            driver.Quit();

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
                    IList<IWebElement> overlayElements = null;

                     overlayElements = driver.FindElements(By.CssSelector("div.overlay"));

                    IWebElement overlayElement = overlayElements.FirstOrDefault(o => o.Displayed);

                    if (overlayElement != null)
                    {
                        wait.Until(d =>
                    {
                        try
                        {
                            return !overlayElement.Displayed;
                        }
                        catch (StaleElementReferenceException)
                        {
                            return true;
                        }
                    });
                    }
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

        private IWebElement WaitForElementVisible(WebDriverWait wait, By locator)
        {
            return wait.Until(driver =>
            {
                var element = driver.FindElement(locator);

                if (!element.Displayed)
                    throw new NoSuchElementException($"Element {locator} found but not visible.");

                return element; 
            });
        }


        private void WaitForTableToLoad(WebDriverWait wait)
        {
            Console.WriteLine("Waiting for table to load...");

            wait.Until(driver =>
            {
                try
                {
                    var tableRows = driver.FindElements(By.CssSelector("table tbody tr"));
                    return tableRows.Count > 0 && tableRows.All(row =>
                    {
                        try
                        {
                            return row.Displayed;
                        }
                        catch (StaleElementReferenceException)
                        {
                            return false;
                        }
                    });
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });

            Console.WriteLine("Table loaded.");
        }
    }
}
