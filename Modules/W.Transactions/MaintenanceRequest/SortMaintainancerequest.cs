using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.MaintenanceRequest
{
    [TestClass]
    public class SortMaintainancerequest
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
        public void SortMaintenanceRequestSuccess()
        {
            expectedStatus = "Passed";
          
            description = "Test case to sort Maintenance Request Request";
            Login login = new Login();
            driver = login.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    login.LoginSuccessCompanyAdmin();
                    return driver.FindElements(By.Id("menuItem-W. Transactions")).Any();
                });

                Console.WriteLine("Login successful and main menu is visible.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Login failed: " + errorMessage);
                throw;
            }

            try
            {
                SortSuccess.Sort(driver, "menuItem-W. Transactions", "menuItem-W. Transactions4", "Maintenance Request");
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

        [TestMethod]
        public void SortMaintenanceRequestFailure()
        {
            expectedStatus = "Failed";

            description = "Verify all sort options are attempted, and handle UI changes gracefully.";

            Login login = new Login();
            driver = login.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    login.LoginSuccessCompanyAdmin();
                    return driver.FindElements(By.Id("menuItem-W. Transactions")).Any();
                });

                Console.WriteLine("Login successful and main menu is visible.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Login failed: " + errorMessage);
                throw;
            }

            try
            {
                SortFailure.Sort(driver, "menuItem-W. Transactions", "menuItem-W. Transactions4", "Maintenance Request");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Test encountered an exception: " + errorMessage);
                throw;
            }
        }

        private int GetSortOptionsCount(WebDriverWait wait)
        {
            SafeClickSortDropdown(wait);
            IList<IWebElement> options = wait.Until(d =>
            {
                var list = d.FindElements(By.TagName("mat-option"));
                return list.Count > 0 ? list : null;
            });
            return options.Count;
        }

        private string GetFirstRowTextSafe(WebDriverWait wait)
        {
            for (int attempt = 0; attempt < 3; attempt++)
            {
                try
                {
                    var body = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
                    var firstRow = body.FindElements(By.TagName("tr")).FirstOrDefault();
                    if (firstRow == null) return string.Empty;

                    return string.Join(" | ",
                        firstRow.FindElements(By.TagName("td"))
                                .Select(td => td.Text.Trim()));
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(500); // Wait and retry
                }
            }
            return string.Empty;
        }

        private void SafeClickSortDropdown(WebDriverWait wait)
        {
            IWebElement dropdown = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("sortBox")));
            SafeClick(wait, dropdown);
        }

        private void SafeClickSortOption(WebDriverWait wait, int index)
        {
            SafeClickSortDropdown(wait);
            Thread.Sleep(300); // Short delay for UI render

            IList<IWebElement> options = wait.Until(d =>
            {
                var list = d.FindElements(By.TagName("mat-option"));
                return list.Count > 0 ? list : null;
            });

            if (index >= options.Count)
                throw new IndexOutOfRangeException("Sort option index out of bounds");

            SafeClick(wait, options[index]);

            IWebElement sortButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("sortButton")));
            SafeClick(wait, sortButton);
        }

        private void SafeClick(WebDriverWait wait, IWebElement element)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
                element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            }
            catch (StaleElementReferenceException)
            {
                // Retry once after re-fetching the element context if needed
                Thread.Sleep(300);
            }
        }

        private bool WaitForRowUpdate(WebDriverWait wait, string oldRow, int retries = 3)
        {
            for (int i = 0; i < retries; i++)
            {
                try
                {
                    return wait.Until(d =>
                    {
                        string current = GetFirstRowTextSafe(wait);
                        return !string.IsNullOrEmpty(current) && !current.Equals(oldRow);
                    });
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(500);
                }
            }
            return false;
        }


        [TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;
            string formatedTime = $"{timeTaken.TotalSeconds:F2}";
            driver.Quit();
            testResult.WriteTestResults(TestContext, formatedTime, expectedStatus, errorMessage, description);
            extent.Flush();
        }

    }
}
