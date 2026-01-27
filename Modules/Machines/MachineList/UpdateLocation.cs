// namespaces...
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
    public class UpdateLocation
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
        public void UpdateLocationSuccess()
        {
            test = extent.CreateTest("Validating update location");

            expectedStatus = "Passed";
            description = "Test case to validate location update. Add valid data in UpdateLocationSuccess before running.";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            try
            {
                LoginSuccess.LoginSuccessCompanyAdmin();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                IWebElement menu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines")));
                menu.Click();

                IWebElement submenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines0")));
                submenu.Click();

                IWebElement select = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                select.Click();

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                searchOptions[0].Click();

                IWebElement searchInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                searchInput.Clear();
                searchInput.SendKeys(MachineUpdateLocation.UpdateLocationSuccess["Machine Id"]);
                searchInput.SendKeys(Keys.Enter);

                IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("tbody")));

                Thread.Sleep(1000); // Give time for results to render

                IList<IWebElement> rows = table.FindElements(By.TagName("tr")).Where(r => r.Displayed).ToList();

                if (rows == null || rows.Count == 0)
                {
                    Console.WriteLine("No rows found for the given Machine ID.");
                    test.Skip("No results returned for Machine ID search. Skipping test.");
                    return;
                }

                IList<IWebElement> columns = rows[0].FindElements(By.TagName("td"));
                if (columns == null || columns.Count == 0)
                {
                    Console.WriteLine("No columns found in the first result row.");
                    test.Skip("First result row contains no columns. Skipping test.");
                    return;
                }

                IWebElement lastColumn = columns.Last();
                RetryUntilSuccess(() => lastColumn.Click());

                IWebElement matMenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-menu-panel")));
                IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));

                foreach (var item in menuItems)
                {
                    if (item.Text.Contains("Update Location"))
                    {
                        item.Click();
                        break;
                    }
                }

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-dialog-container")));

                string oldPluscode = driver.FindElement(By.Name("pluscode")).GetAttribute("value");
                Console.WriteLine("Old Plus Code: " + oldPluscode);

                Dictionary<string, string> dataToUse = oldPluscode.Equals(UpdateLocationData.updateLocationSuccess1["pluscode"])
                    ? UpdateLocationData.updateLocationSuccess
                    : UpdateLocationData.updateLocationSuccess1;

                IWebElement City = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("City")));
                IWebElement State = driver.FindElement(By.Name("State"));
                IWebElement pluscode = driver.FindElement(By.Name("pluscode"));
                IWebElement validatePlusCode = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Validate Plus Code')]")));

                City.Clear();
                City.SendKeys(dataToUse["City"]);

                State.Clear();
                State.SendKeys(dataToUse["State"]);

                pluscode.Clear();
                pluscode.SendKeys(dataToUse["pluscode"]);

                validatePlusCode.Click();

                string newPluscode = driver.FindElement(By.Name("pluscode")).GetAttribute("value");
                Console.WriteLine("New Plus Code: " + newPluscode);
                Thread.Sleep(1000);

                IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Save')]")));
                submit.Click();
                Thread.Sleep(1500);

                IWebElement snackbar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".mat-snack-bar-container")));
                Console.WriteLine("Snackbar: " + snackbar.Text);

                Assert.IsTrue(snackbar.Text.Contains("Location updated"), "Location update failed.");
                test.Pass("Location updated successfully.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail(errorMessage);
                throw;
            }
        }


        [TestMethod]
        public void UpdateLocationBtnDisable()
        {
            test = extent.CreateTest("Validating update location without required data");

            expectedStatus = "Passed";
            description = "Test to ensure Save button is disabled without required data";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            try
            {
                LoginSuccess.LoginSuccessCompanyAdmin();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                IWebElement menu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines")));
                menu.Click();

                IWebElement submenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Machines0")));
                submenu.Click();

                IWebElement select = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                select.Click();

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                searchOptions[0].Click();

                IWebElement searchInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                searchInput.Clear();
                searchInput.SendKeys(MachineUpdateLocation.UpdateLocationFailure["Machine Id"]);
                searchInput.SendKeys(Keys.Enter);

                // Wait for the table to load
                IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("tbody")));

                Thread.Sleep(1000); // optional small wait for table rendering
                IList<IWebElement> rows = table.FindElements(By.TagName("tr"));

                if (rows == null || rows.Count == 0)
                {
                    Console.WriteLine("No rows found for the given Machine ID.");
                    test.Skip("No results returned for Machine ID search. Skipping test.");
                    return;
                }

                IList<IWebElement> columns = rows[0].FindElements(By.TagName("td"));
                if (columns == null || columns.Count == 0)
                {
                    Console.WriteLine("No columns found in the first result row.");
                    test.Skip("First result row contains no columns. Skipping test.");
                    return;
                }

                IWebElement lastColumn = columns.Last();
                RetryUntilSuccess(() => lastColumn.Click());

                IWebElement matMenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-menu-panel")));
                IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));

                foreach (var item in menuItems)
                {
                    if (item.Text.Contains("Update Location"))
                    {
                        item.Click();
                        break;
                    }
                }

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-dialog-container")));
                IWebElement dialog = driver.FindElement(By.TagName("mat-dialog-container"));
                IWebElement saveButton = dialog.FindElements(By.TagName("button")).FirstOrDefault(b => b.Text.Contains("Save"));

                Assert.IsNotNull(saveButton, "Save button not found in the dialog.");
                Assert.IsTrue(saveButton.GetAttribute("disabled") == "true", "Save button should be disabled when required data is missing.");

                test.Pass("Save button correctly disabled when fields are empty.");
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
            string formattedTime = $"{stopwatch.Elapsed.TotalSeconds:F2}";
            driver.Quit();
            testResult.WriteTestResults(TestContext, formattedTime, expectedStatus, errorMessage, description);
            extent.Flush();
        }

        private void RetryUntilSuccess(Action action, int maxRetries = 2)
        {
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    action.Invoke();
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(1000);
                }
            }
            throw new StaleElementReferenceException("Element remained stale after retries.");
        }

    }
}