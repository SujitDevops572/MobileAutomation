using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Security.Policy;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Warehouse.CurrentStock;

namespace VMS_Phase1PortalAT.Modules.Warehouse.CurrentStock
{
    [TestClass]
    public class MergeCurrentStock
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
        private string errormessage;

        [TestInitialize]
       
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;
            extent = ExtentManager.GetInstance();
           
        }

        [TestMethod]
      
        public void MergeCurentStockSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("Merge current stock");
            description = "test case to test merge current stock. add valid data in MergeCurrentStocSuccess in MergeCurrentStockData file";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse2")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse2"));
                    if (submenu != null)
                    {
                        submenu.Click();
                      
                        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                        //IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        //select.Click();
                        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        //IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        //searchOptions[0].Click();
                        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        //IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        //searchInput.Clear();
                        //searchInput.SendKeys(MergeCurrentStockData.MergeCurrentStocSuccess["Stock Id"]);
                        //searchInput.SendKeys(Keys.Enter);
                        //Thread.Sleep(1000);
                        //IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        //IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        //IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        //if (data.Count > 0)
                        //{
                        //    IWebElement firstRow = data[0];
                        //    IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                        //    IWebElement lastColumn = columns[columns.Count - 1];
                        //    lastColumn.Click();

                        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                        //IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                        //IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                        //foreach (var item in menuItems)
                        //{
                        //    if (item.Text.Contains("Merge"))
                        //    {
                        //        item.Click();
                        //        break;
                        //    }
                        //}
                        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-focused")));
                        //IWebElement select2 = driver.FindElement(By.ClassName("mat-focused"));
                        //select2.Click();
                        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        //IList<IWebElement> searchOptions2 = driver.FindElements(By.TagName("mat-option"));
                        //searchOptions2[0].Click();
                        //IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                        //submit.Click();


                        
                        void WaitForAngular()
                        {
                            try
                            {
                                var js = (IJavaScriptExecutor)driver;

                                wait.Until(d => js.ExecuteScript("return document.readyState").ToString() == "complete");

                                wait.Until(d =>
                                    (bool)js.ExecuteScript(@"
                if (window.getAllAngularRootElements) {
                    var roots = window.getAllAngularRootElements();
                    if (!roots || roots.length === 0) return true;
                    var el = roots[0];
                    var injector = angular.element(el).injector();
                    if (!injector) return true;
                    var $browser = injector.get('$browser');
                    return $browser.$$completeOutstandingRequestCount === 0;
                }
                return true;
            ")
                                );

                                // Wait for table to be present and visible
                                wait.Until(d =>
                                {
                                    var table = d.FindElement(By.XPath("//table"));
                                    return table.Displayed;
                                });

                                // Wait for 'Next' button to be present and enabled or not visible (meaning no next page)
                                wait.Until(d =>
                                {
                                    var nextButtons = d.FindElements(By.XPath("//button[contains(@aria-label,'Next page')]"));
                                    if (nextButtons.Count == 0)
                                        return true; // no next button means no next page to wait for
                                    return nextButtons[0].Enabled;
                                });
                            }
                            catch { }
                        }


                        bool a = false;
                        IWebElement snackbar = null;

                        while (a == false)
                        {
                            WaitForAngular();

                            var actionButtons = wait.Until(d =>
                            {
                                var els = d.FindElements(By.XPath("//table//tr/td[last()]//button"));
                                return els.Count > 0 ? els : null;
                            });

                            Console.WriteLine(actionButtons.Count);

                            bool mergeClickedOnThisPage = false;

                            for (int i = 0; i < actionButtons.Count; i++)  // change to 0 if you want all buttons; use 3 if intentional
                            {
                                // Scroll element into view and wait clickable before clicking
                                var js = (IJavaScriptExecutor)driver;
                                js.ExecuteScript("arguments[0].scrollIntoView(true);", actionButtons[i]);
                                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(actionButtons[i]));
                                actionButtons[i].Click();
                                WaitForAngular();
                                Thread.Sleep(1000);

                                var dropdownTrigger = wait.Until(d =>
                                    d.FindElement(By.XPath("//button[contains(text(),'Merge')]"))
                                );
                                dropdownTrigger.Click();
                                WaitForAngular();

                                var clickingMatselect = wait.Until(d =>
                                   d.FindElement(By.XPath("//form//mat-select[@role='listbox']"))
                                );
                                clickingMatselect.Click();

                                var dropdownItems = driver.FindElements(By.XPath("//mat-option[@role='option']"));

                                if (dropdownItems.Count > 0)
                                {
                                    wait.Until(d => dropdownItems[0].Displayed && dropdownItems[0].Enabled);
                                    dropdownItems[0].Click();
                                    WaitForAngular();

                                    var mergeButton = wait.Until(d =>
                                        d.FindElement(By.XPath("//button[normalize-space()='Save']"))
                                    );
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(mergeButton));
                                    mergeButton.Click();

                                    // Wait for snackbar and assign
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".mat-snack-bar-container")));
                                    snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                                    Console.WriteLine(snackbar.Text);
                                    mergeClickedOnThisPage = true;   
                                    break;
                                }
                                else
                                {
                                    var closeButton = wait.Until(d =>
                                        d.FindElement(By.XPath("//button[normalize-space()='Close']"))
                                    );
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(closeButton));
                                    closeButton.Click();
                                }

                                WaitForAngular();

                                // Refresh action buttons list after possible DOM changes
                                actionButtons = driver.FindElements(By.XPath("//table//tr/td[last()]//button"));
                            }

                            // Only click Next page if NO merge happened on this page
                            if (!mergeClickedOnThisPage)
                            {
                                var nextButtons = driver.FindElements(By.XPath("//button[contains(@aria-label,'Next page')]"));

                                if (nextButtons.Count == 0 || !nextButtons[0].Enabled)
                                    break;

                                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(nextButtons[0]));
                                nextButtons[0].Click();
                            }
                            else
                            {
                                break; // merge happened, so stop pagination
                            }

                            // Check snackbar text safely
                            string title = "Stock Merged successfully";
                            bool isSuccess = false;
                            if (snackbar != null && snackbar.Text.Contains(title))
                            {
                                isSuccess = true;
                            }
                            Assert.IsTrue(isSuccess, "Merge failed: snackbar message not found.");
                            test.Pass();
                        }

                    }
                    //        else
                    //        {
                    //            Console.WriteLine("doesnt find add null");
                    //            test.Skip();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("doesnt find sub menu");
                    //        test.Skip();
                    //    }

                    //else
                    //{
                    //    Console.WriteLine("doesnt find menu");
                    //    test.Skip();
                    //}
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
        public void MergeCurentStockFailure()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("Merge current stock");
            description = "test case to test merge current stock. add valid data in MergeCurrentStocFailure in MergeCurrentStockData file";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse2")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse2"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        select.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        searchOptions[0].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        searchInput.Clear();
                        searchInput.SendKeys(MergeCurrentStockData.MergeCurrentStocFailure["Stock Id"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        if (data.Count > 0)
                        {
                            IWebElement firstRow = data[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            IWebElement lastColumn = columns[columns.Count - 1];
                            lastColumn.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                            IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));

                            foreach (var item in menuItems)
                            {
                                if (item.Text.Contains("Merge"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-focused")));
                            IWebElement select2 = driver.FindElement(By.ClassName("mat-focused"));
                            select2.Click();
                            IList<IWebElement> searchOptions2 = driver.FindElements(By.TagName("mat-option"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            bool isDisable = submit.GetAttribute("disabled").Equals("true");
                            bool isSuccess = false;
                            if (searchOptions2.Count == 0 && isDisable)
                            {
                                isSuccess = true;
                            }
                            Assert.IsTrue(isSuccess, " failed..");
                            test.Pass();
                        }
                        else
                        {
                            Console.WriteLine("doesnt find sub menu");
                            test.Skip();
                        }
                    }
                    else
                    {
                        Console.WriteLine("doesnt find menu");
                        test.Skip();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                    test.Fail(errormessage);
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
