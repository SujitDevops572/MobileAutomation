using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

namespace VMS_Phase1PortalAT.Modules.Advertisement.Runtime
{
    [TestClass]
    public class DeleteRuntime
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        private readonly string systemName = Environment.UserName;
        private string downloadPath;
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
            downloadPath = setupDatas.downloadPath;
            downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", true);

            extent = ExtentManager.GetInstance();


        }

        [TestMethod]
        public void DeleteRuntimeSuccess()
        {
            test = extent.CreateTest("test case to test Delete Runtime");

            expectedStatus = "Passed";
            description = "test case to test Delete Runtime";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Advertisements"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Advertisements1"));

                    // Helper: Wait until element is clickable, refetching each time
                    IWebElement WaitUntilClickable(By locator, int timeoutSeconds = 10)
                    {
                        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                        return wait.Until(drv =>
                        {
                            try
                            {
                                var el = drv.FindElement(locator);

                                if (el.Displayed && el.Enabled)
                                {
                                    return el; // element is ready
                                }

                                // element exists but not interactable yet
                                throw new ElementNotInteractableException($"Element found but not clickable: {locator}");
                            }
                            catch (StaleElementReferenceException)
                            {
                                // element went stale, retry
                                throw;
                            }
                            catch (NoSuchElementException)
                            {
                                // element not yet in DOM, retry
                                throw;
                            }
                        });
                    }



                    void SafeClick(IWebElement element)
                    {
                        try
                        {
                            element.Click();
                        }
                        catch (ElementClickInterceptedException)
                        {

                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
                        }
                    }

                    //Test
                    if (submenu != null)
                    {
                        SafeClick(submenu);


                        wait.Until(driver =>
                        {
                            var rows = driver.FindElements(By.TagName("tr"));
                            return rows.Count > 0 ? true : throw new NoSuchElementException("No table rows found yet");
                        });


                        var lastrow = WaitUntilClickable(By.XPath("(//tbody/tr)[1]/td[last()]"));
                        SafeClick(lastrow);

                        var DeleteButton = WaitUntilClickable(By.XPath("//div[@role='menu']//button[contains(text(),'Delete')]"));
                        SafeClick(DeleteButton);

                        var Confirm = WaitUntilClickable(By.XPath("//span[contains(text(),'Confirm')]"));
                        SafeClick(Confirm);

                        wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                        IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                        Console.WriteLine(snackbar.Text);
                        Assert.IsTrue(snackbar.Text.Contains("Error Deleting Advertisement"), " failed..");


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
