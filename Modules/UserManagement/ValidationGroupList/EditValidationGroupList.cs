using AventStack.ExtentReports;
using FlaUI.Core.WindowsAPI;
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
    public class EditValidationGroupList
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
        public void EditValidationGroupListSuccess()
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

                //Riotachittoor Client

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                Thread.Sleep(1000);
                select.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                searchOptions[1].Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                searchInput.Clear();
                searchInput.SendKeys(SearchValidationGroupListData.EditValidationGroupList["Name"]);
                searchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);


                WaitForTableToLoad(wait);
                ClickElementWhenReady(wait, By.XPath("(//td)[6]"));
                ClickElementWhenReady(wait, By.XPath("(//button[@role='menuitem'])[2]"));
                              
                SendKeysWhenReady(wait, By.XPath("//input[@name='name']"), SearchValidationGroupListData.EditValidationGroupList["Name1"]);
                SendKeysWhenReady(wait, By.XPath("//input[@name='purLimitQty']"), SearchValidationGroupListData.EditValidationGroupList["qty"]);
                
                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Machine ID']"));
                ClickElementWhenReady(wait, By.XPath("(//mat-option[@role='option'])[1]"));
                 
                //is Default
                //ClickElementWhenReady(wait, By.XPath("//input[@role='switch']"));
                
                ClickElementWhenReady(wait, By.XPath("//input[@placeholder='Reset Duration']"));
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Every-Week ']"));
                Thread.Sleep(2000);

                ClickElementWhenReady(wait, By.XPath("//mat-icon[text()='date_range']"));
                
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[1]"), SearchValidationGroupListData.EditValidationGroupList["hour"]);
                SendKeysWhenReady(wait, By.XPath("(//input[@class='owl-dt-timer-input'])[2]"), SearchValidationGroupListData.EditValidationGroupList["mins"]);

                Thread.Sleep(1000);
                ClickElementWhenReady(wait, By.XPath("//span[text()='Set']"));
                //ClickElementWhenReady(wait, By.XPath("//span[text()='Cancel']"));
                Thread.Sleep(2000);
                //add-icon
                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='add'])[2]"));
                Thread.Sleep(1000);
                ClickElementWhenReady(wait, By.XPath("(//mat-icon[text()='delete'])[1]"));

                ClickElementWhenReady(wait, By.XPath("//span[text()=' Save ']"));

                var snackbarText = wait.Until(drv =>
                {
                    var el = drv.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    return el.Displayed && !string.IsNullOrWhiteSpace(el.Text)
                           ? el.Text
                           : null;
                });

                Console.WriteLine(snackbarText);

                //Assert.IsTrue(snackbarText.Contains("Validation group added"), "Not Deleted!!!");


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
            String formatedTime = $"{timeTaken.TotalSeconds:F2}";
            driver.Quit();
            testResult.WriteTestResults(TestContext, formatedTime, expectedStatus, errorMessage, description);
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

        private IWebElement WaitForElementVisible(WebDriverWait wait, By locator)
        {
            return wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch
                {
                    return null;
                }
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

    } }