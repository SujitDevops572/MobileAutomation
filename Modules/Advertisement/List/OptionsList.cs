using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Advertisement.List;

namespace VMS_Phase1PortalAT.Modules.Advertisement.List
{
    [TestClass]
    public class OptionsList
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
        public void ViewOption()
        {
            test = extent.CreateTest("test case to test View List");

            expectedStatus = "Passed";
            description = "test case to test View List";
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
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Advertisements0"));
                    
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
                        Thread.Sleep(500);

                        var viewButton = WaitUntilClickable(By.XPath("//div[@role='menu']//button[contains(text(),'View')]"));
                        SafeClick(viewButton);

                        
                        var video = WaitUntilClickable(By.XPath("//mat-dialog-container//video"));
                        bool isViewOpened = video.Displayed && video.Enabled;

                        var titleElement = WaitUntilClickable(By.XPath("//div[contains(text(),'Title')]/following-sibling::div[1]"));

                        
                        bool isTitle = false;

                        
                        if (titleElement.Text.Contains("N/A"))
                        {
                            isTitle = titleElement.Displayed && titleElement.Enabled;
                        }
                        else
                        {
                            
                            isTitle = titleElement.Displayed && titleElement.Enabled;
                        }

                        
                        var closeButton = WaitUntilClickable(By.XPath("//mat-dialog-container[@role='dialog']//span[contains(text(),'Close')]"));
                        SafeClick(closeButton);

                        Console.WriteLine("Button Enabled: " + isViewOpened);
                        Console.WriteLine("Title Visible : " + isTitle);

                        
                        Assert.IsTrue(isViewOpened && isTitle, "View not opened");

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



        [TestMethod]
        public void SetAsClientDefaultAdv()
        {

            test = extent.CreateTest("Check the Set As Client Default Adv in List Option");

            expectedStatus = "Passed";
            description = "Test case to check the Set As Client Default Adv in List Option";

            Login loginHelper = new Login();
            driver = loginHelper.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                loginHelper.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                Console.WriteLine("Navigating to Client User List...");
                IWebElement menuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements")));
                menuBtn.Click();
                IWebElement submenuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements0")));
                submenuBtn.Click();

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                Thread.Sleep(1000);
                select.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                searchOptions[3].Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                searchInput.Clear();
                searchInput.SendKeys(SearchListData.SetasDefaultsearch["Client Id"]);
                searchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);

                //clicking set As default client
                int rowCount = driver.FindElements(By.XPath("//tbody/tr")).Count;
                Console.WriteLine("Total Rows: " + rowCount);

                for (int i = 1; i <= rowCount; i++)
                {
                    WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                    try
                    {
                        // Click last column of current row
                        IWebElement lastCol = wait1.Until(
                            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                                By.XPath($"(//tbody/tr)[{i}]/td[last()]")
                            ));
                        Console.WriteLine($"Row {i}: Last column clicked.");
                        lastCol.Click();

                        // Look for "Set as default"
                        IWebElement setAsDefault = wait1.Until(
                            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                                By.XPath("//button[contains(text(),'Set as Client Default')]")
                            ));

                        // Found → click & exit
                        setAsDefault.Click();
                        Console.WriteLine(" Set as default clicked. Exiting loop.");
                        break;
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine($" 'Set as default' not visible in row {i}. Closing menu & moving to next row...");

                        //Close the mat-menu using ESC
                        Actions a = new Actions(driver);
                        a.SendKeys(Keys.Escape).Perform();

                        // Give menu time to close
                        Thread.Sleep(400);

                        continue;
                    }
                    catch (StaleElementReferenceException)
                    {
                        Console.WriteLine($" Stale element in row {i}, retrying...");
                        i--; // retry the same row
                        continue;
                    }
                }

                Thread.Sleep(500);
                IWebElement confirm = driver.FindElement(By.XPath("//span[contains(text(),'Confirm')]"));
                confirm.Click();
                
                
                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Advertisement is set as client default"), " failed..");

                Thread.Sleep(500);
                IWebElement Ignore = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Ignore')]"));
                Ignore.Click();



            }
            catch (Exception ex){
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
        public void DeleteAdv()
        {

            test = extent.CreateTest("Check the Delete Adv in List Option");

            expectedStatus = "Passed";
            description = "Test case to check the Delete Adv in List Option";

            // Initialize WebDriver
            Login loginHelper = new Login();
            driver = loginHelper.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                loginHelper.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                Console.WriteLine("Navigating to Client User List...");
                IWebElement menuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements")));
                menuBtn.Click();
                IWebElement submenuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements0")));
                submenuBtn.Click();

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
                searchInput.SendKeys(SearchListData.Deletesearch["Title"]);
                searchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);


                IWebElement lastcol = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//tbody/tr)[last()]/td[last()]")));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", lastcol);
                wait.Until(ExpectedConditions.ElementToBeClickable(lastcol)).Click();

                IWebElement delete = wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        By.XPath("//div[contains(@class,'mat-menu-panel')]//button[contains(text(),'Delete')]")
                    )
                );
                delete.Click();

                IWebElement confirm = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]")
                    )
                );
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", confirm);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", confirm);

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Advertisement Deleted"), " failed..");

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
