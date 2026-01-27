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
using VMS_Phase1PortalAT.utls.datas.Advertisement.MachineSettings;
using VMS_Phase1PortalAT.utls.datas.Advertisement.Mapping;

namespace VMS_Phase1PortalAT.Modules.Advertisement.MachineSettings
{
    [TestClass]
    public class AddMachineSettings
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
        public void AddMachineSettingsSuccess()
        {
            test = extent.CreateTest("test case to test Add Machine Settings");
            expectedStatus = "Passed";
            description = "test case to test Add Machine Settings";

            Login loginSuccess = new Login();
            driver = loginSuccess.getdriver();

            try
            {
                loginSuccess.LoginSuccessCompanyAdmin();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            try
            {
                
                Action<Func<IWebElement>> RetryClick = (getElement) =>
                {
                    int attempts = 0;
                    while (attempts < 5)
                    {
                        try
                        {
                            IWebElement el = getElement();
                            js.ExecuteScript("arguments[0].scrollIntoView({block:'center'});", el);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(el));
                            el.Click();
                            return;
                        }
                        catch (StaleElementReferenceException)
                        {
                            attempts++;
                            Thread.Sleep(200);
                        }
                        catch (ElementClickInterceptedException)
                        {
                            js.ExecuteScript("arguments[0].click();", getElement());
                            return;
                        }
                    }
                    throw new Exception("Element could not be clicked after retries.");
                };

               
                void WaitForOverlay()
                {
                    wait.Until(driver =>
                        driver.FindElements(By.CssSelector(".cdk-overlay-backdrop.cdk-overlay-backdrop-showing")).Count == 0
                    );
                }

               
                RetryClick(() => driver.FindElement(By.Id("menuItem-Advertisements")));
                //WaitForOverlay();

                RetryClick(() => driver.FindElement(By.Id("menuItem-Advertisements2")));
                //WaitForOverlay();

                wait.Until(d => d.FindElements(By.TagName("tr")).Count > 0);

               
                RetryClick(() => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                //WaitForOverlay();

                
                RetryClick(() => driver.FindElement(By.XPath("//mat-dialog-container//input[contains(@name,'machineIds')]")));
                //WaitForOverlay();

                Thread.Sleep(1000);
                IList<IWebElement> options = wait.Until(d => d.FindElements(By.XPath("//mat-option")));

                if (options.Count == 0)
                    throw new Exception("No mat-option elements found.");

                RetryClick(() => driver.FindElement(By.XPath("(//mat-option)[1]")));


                RetryClick(() => driver.FindElement(By.XPath("//mat-dialog-container//input[contains(@name,'playbackStartTimeout')]")));

                IWebElement playback = driver.FindElement(By.XPath("//mat-dialog-container//input[contains(@name,'playbackStartTimeout')]"));
                playback.Clear();
                playback.SendKeys(AddMAchineSettingsdata.AddMAchineSettingSuccess["playback"]);

                Thread.Sleep(200);

                
                RetryClick(() => driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Save')]")));

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                //Assert.IsTrue(snackbar.Text.Contains("Advertisement Machine Mapping Added"), " failed..");

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
            TimeSpan timeTaken = stopwatch.Elapsed;
            String formatedTime = $"{timeTaken.TotalSeconds:F2}";
            driver.Quit();
            testResult.WriteTestResults(TestContext, formatedTime, expectedStatus, errorMessage, description);

            extent.Flush();
        }
    }
}