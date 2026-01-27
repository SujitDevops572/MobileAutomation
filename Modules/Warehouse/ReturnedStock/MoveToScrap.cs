using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Warehouse.CurrentStock;

namespace VMS_Phase1PortalAT.Modules.Warehouse.ReturnedStock
{
    [TestClass]
    public class MoveToScrap
    {

        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description; public required TestContext TestContext { get; set; }
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
        public void MoveToScrapSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("move the stock to Scrap");
            description = "MoveToScrapSuccess test";

            Login login = new Login();
            driver = login.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            try
            {
                login.LoginSuccessCompanyAdmin();

                // Click Warehouse
                SafeClick(By.Id("menuItem-Warehouse"), wait);

                // Click submenu
                SafeClick(By.Id("menuItem-Warehouse4"), wait);

                // Select search type
                SafeClick(By.ClassName("searchTypeBox"), wait);
                SafeClick(By.TagName("mat-option"), wait);   // first option

                // Search input
                var searchBox = SafeFind(By.Name("searchText"), wait);
                searchBox.Clear();
                searchBox.SendKeys(MoveToScrapData.MoveToScrapSuccess["Stock Id"]);
                searchBox.SendKeys(Keys.Enter);

                // Wait for table rows
                wait.Until(d => d.FindElements(By.TagName("td")).Count > 0);

                // Click menu on last column
                //var table = SafeFind(By.TagName("tbody"), wait);
                //var row = table.FindElements(By.TagName("tr")).First();
                //var lastColumn = row.FindElements(By.TagName("td")).Last();
                SafeClick(By.XPath("(//tbody/tr)[1]/td[last()]"), wait);

                // Open popup menu
                var matMenu = SafeFind(By.ClassName("mat-menu-panel"), wait);
                var items = matMenu.FindElements(By.TagName("button"));

                foreach (var item in items)
                {
                    if (item.Text.Contains("Move To Scrap"))
                    {
                        SafeClick(item, wait);
                        break;
                    }
                }

                // Submit button
                SafeClick(By.ClassName("mat-raised-button"), wait);

                // Snackbar wait
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".mat-snack-bar-container")));
                var snackbar = SafeFind(By.CssSelector(".mat-snack-bar-container"), wait);

                string expected = "Returned Stock scrapped";
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains(expected), "Snackbar did not show expected success message");
                test.Pass();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail(errorMessage);
                throw;
            }
        }




        [TestMethod]
        public void MoveToScrapFailure()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("move to scrap with 0 stock");
            description = "test case to test move to scrap list displayed with 0 qty. add valid data in MoveToScrapSuccess in MoveToScrapData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse4")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse4"));
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
                        searchInput.SendKeys(MoveToScrapData.MoveToScrapFailure["Stock Id"]);
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
                                if (item.Text.Contains("Move To Scrap"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("qtyToScrap")));
                            IWebElement qty = driver.FindElement(By.Name("qtyToScrap"));
                            qty.Clear();
                            qty.SendKeys("0");
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            bool isDisable = submit.GetAttribute("disabled").Equals("true");
                            bool isSuccess = false;
                            if (isDisable)
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




        public IWebElement SafeFind(By locator, WebDriverWait wait)
        {
            IWebElement element = null;

            wait.Until(driver =>
            {
                try
                {
                    element = driver.FindElement(locator);

                    if (element != null && element.Displayed)
                        return true; 
                    return false; 
                }
                catch
                {
                    return false; 
                }
            });

            return element;
        }





        public void SafeClick(By locator, WebDriverWait wait)
        {
            wait.Until(driver =>
            {
                try
                {
                    var el = driver.FindElement(locator);

                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", el);

                    wait.Until(ExpectedConditions.ElementToBeClickable(locator));

                    el.Click();
                    return true;
                }
                catch (ElementClickInterceptedException)
                {
                    Thread.Sleep(200);
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }



        public void SafeClick(IWebElement element, WebDriverWait wait)
        {
            wait.Until(driver =>
            {
                try
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);

                    element.Click();
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false; // will retry
                }
                catch (ElementClickInterceptedException)
                {
                    return false; // will retry
                }
            });
        }









        public void MoveToScrapSuccess1()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("move the stock to Scrap");
            description = "test case to test move to scrap list displayed. add valid data in MoveToScrapSuccess in MoveToScrapData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse4")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse4"));
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
                        searchInput.SendKeys(MoveToScrapData.MoveToScrapSuccess["Stock Id"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        wait.Until(drv => driver.FindElements(By.TagName("td")).Count > 0);
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
                                if (item.Text.Contains("Move To Scrap"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            submit.Click();
                            string title = "Returned Stock scrapped";
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            bool isSuccess = false;
                            Console.WriteLine(snackbar.Text);
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Assert.IsTrue(isSuccess, " failed..");
                            test.Pass();
                        }
                        else
                        {
                            Console.WriteLine("doesnt find add null");
                            test.Skip();
                        }
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

               
    }

}