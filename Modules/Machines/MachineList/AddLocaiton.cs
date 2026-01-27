using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;

namespace VMS_Phase1PortalAT.Modules.Machines.MachineList
{
    [TestClass]
    public class AddLocation  // fixed spelling
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
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;

            extent = ExtentManager.GetInstance();
        }

        [TestMethod]
        public void AddLocationSuccess()
        {
            test = extent.CreateTest("Validating add location");

            expectedStatus = "Passed";
            description = "Test case to test add location with valid data. Populate AddLocationData.addLocationSuccess before running.";

            // Login flow
            Login login = new Login();
            driver = login.getdriver();
            try
            {
                login.LoginSuccessCompanyAdmin();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                test.Fail("Login failed: " + errorMessage);
                // Fail immediately
                Assert.Fail("Login failed: " + errorMessage);
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));

                // Navigate via menu
                IWebElement menu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines")));
                menu.Click();

                IWebElement submenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines0")));
                submenu.Click();

                // Wait for the table body
                IWebElement tableBody = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
                IList<IWebElement> rows = tableBody.FindElements(By.TagName("tr"));

                if (rows == null || rows.Count == 0)
                {
                    errorMessage = "No machine rows found in listing.";
                    test.Fail(errorMessage);
                    Assert.Fail(errorMessage);
                }

                IWebElement firstRow = rows[0];
                IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                if (columns == null || columns.Count == 0)
                {
                    errorMessage = "First row has no columns.";
                    test.Fail(errorMessage);
                    Assert.Fail(errorMessage);
                }

                // Click the last column cell (assuming action menu)
                IWebElement lastCell = columns[columns.Count - 1];
                lastCell.Click();

                IWebElement matMenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-menu-panel")));
                IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));

                bool clickedViewDetails = false;
                foreach (var item in menuItems)
                {
                    if (item.Text.Contains("View Details"))
                    {
                        item.Click();
                        clickedViewDetails = true;
                        break;
                    }
                }
                if (!clickedViewDetails)
                {
                    errorMessage = "Did not find 'View Details' menu item.";
                    test.Fail(errorMessage);
                    Assert.Fail(errorMessage);
                }

                // Wait and click Location card
                IWebElement locationCard = wait.Until(driver => driver.FindElement(By.XPath("//small[contains(text(),'Location')]")));
                // Optionally: wait until clickable
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//small[contains(text(),'Location')]")));
                locationCard.Click();

                Thread.Sleep(200);
                // After clicking, wait for pluscode field to be present / visible
                IWebElement pluscode1 = wait.Until(driver => driver.FindElement(By.Name("pluscode")));
                string s = pluscode1.GetAttribute("value");
                Console.WriteLine("pluscode1 value: " + s);

                Thread.Sleep(200);
                // Determine which data set to use
                bool useFirstSet = (s == AddLocationData.addLocationSuccess1["pluscode"]);

                IWebElement cityField = wait.Until(driver => driver.FindElement(By.Name("City")));
                IWebElement stateField = wait.Until(driver => driver.FindElement(By.Name("State")));
                IWebElement pluscodeField = wait.Until(driver => driver.FindElement(By.Name("pluscode")));

                if (useFirstSet)
                {
                    cityField.Clear();
                    cityField.SendKeys(AddLocationData.addLocationSuccess["City"]);

                    stateField.Clear();
                    stateField.SendKeys(AddLocationData.addLocationSuccess["State"]);

                    pluscodeField.Clear();
                    pluscodeField.SendKeys(AddLocationData.addLocationSuccess["pluscode"]);
                }
                else
                {
                    cityField.Clear();
                    cityField.SendKeys(AddLocationData.addLocationSuccess1["City"]);

                    stateField.Clear();
                    stateField.SendKeys(AddLocationData.addLocationSuccess1["State"]);

                    pluscodeField.Clear();
                    pluscodeField.SendKeys(AddLocationData.addLocationSuccess1["pluscode"]);
                }

                // Wait until pluscodeField’s value matches what you typed (to ensure UI updated)
                wait.Until(drv =>
                {
                    var val = pluscodeField.GetAttribute("value");
                    return !string.IsNullOrEmpty(val);
                });

                string inputValue = pluscodeField.GetAttribute("value");
                Console.WriteLine("pluscodeField after input: " + inputValue);

                if (string.IsNullOrEmpty(inputValue))
                {
                    errorMessage = "pluscode input value is empty after typing.";
                    test.Fail(errorMessage);
                    Assert.Fail(errorMessage);
                }

                IWebElement validatePlusCode = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    By.XPath("//span[contains(text(),'Validate Plus Code')]")));
                validatePlusCode.Click();

                // Wait until Save becomes enabled
                wait.Until(drv =>
                {
                    var saveBtn = drv.FindElement(By.XPath("//span[contains(text(),'Save')]/ancestor::button"));
                    return saveBtn.Enabled;
                });

                IWebElement submitButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    By.XPath("//span[contains(text(),'Save')]/ancestor::button")));

                // Scroll into view in case it’s out of visible area
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);

                // Click the submit button
                submitButton.Click();
                Console.WriteLine("Clicked Save button");
                Thread.Sleep(2000);

                // Optionally, wait for a confirmation, e.g. snackbar or success message
                // IWebElement snack = wait.Until(drv => drv.FindElement(By.CssSelector(".mat-snack-bar-container")));
                // Console.WriteLine("Snackbar text: " + snack.Text);

                test.Pass();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                }
                test.Fail("Exception in test: " + errorMessage);
                Assert.Fail("Exception in test: " + errorMessage);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            string formatted = $"{elapsed.TotalSeconds:F2}";
            if (driver != null)
            {
                driver.Quit();
            }
            testResult.WriteTestResults(TestContext, formatted, expectedStatus, errorMessage, description);
            extent.Flush();
        }
    }
}
