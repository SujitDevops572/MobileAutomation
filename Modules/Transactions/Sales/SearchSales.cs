using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Products.SubCategories;
using VMS_Phase1PortalAT.utls.datas.Transactions.Sales;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Transaction.Sales
{
    [TestClass]
    public class SearchSales
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        public required TestContext TestContext { get; set; }
        IWebDriver driver;
        paginator pagin = new paginator();
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
        public void SearchSalesSuccess()
        {

            expectedStatus = "Passed";
            description = "test case to search company user datas with valid datas so it return datas. add valid data in searchCompanyUserSuccess in SearchCompanyUserData file before run";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    LoginSuccess.LoginSuccessCompanyAdmin();
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
               
                Search_Sales.SearchAction(driver, "menuItem-Transactions", "menuItem-Transactions0", SearchSalesData.searchSalesSuccess, 1, "Sales");
            }


            //expectedStatus = "Passed";
            //test = extent.CreateTest(" search sales ");
            //description = "test case to search sales datas with valid datas so it return datas. add valid data in searchSalesSuccess in SearchSalesData file before run";
            //Login LoginSuccess = new Login();
            //driver = LoginSuccess.getdriver();
            //try
            //{
            //    LoginSuccess.LoginSuccessCompanyAdmin();
            //}
            //catch (Exception ex)
            //{
            //    if (ex.InnerException != null)
            //    {
            //        errorMessage = ex.InnerException.Message;
            //    }
            //}
            //try
            //{
            //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Transactions")));
            //IWebElement menu = driver.FindElement(By.Id("menuItem-Transactions"));
            //if (menu != null)
            //{
            //    menu.Click();
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Transactions0")));
            //    IWebElement submenu = driver.FindElement(By.Id("menuItem-Transactions0"));
            //    if (submenu != null)
            //    {
            //        submenu.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
            //        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
            //        Thread.Sleep(1000);
            //        select.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
            //        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));

            //        foreach (var item in searchOptions)
            //        {
            //            int index = searchOptions.IndexOf(item);
            //            searchOptions[index].Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
            //            IWebElement searchInput = driver.FindElement(By.Name("searchText"));
            //            searchInput.Clear();
            //            searchInput.SendKeys(SearchSalesData.searchSalesSuccess[item.Text]);
            //            searchInput.SendKeys(Keys.Enter);
            //            Thread.Sleep(4000);
            //            int index1 = index + 1;
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//mat-chip-list)["+index1+"]//button")));
            //            IWebElement close = driver.FindElement(By.XPath("(//mat-chip-list)["+index1+"]//button"));
            //            close.Click();
            //            Thread.Sleep(4000);
            //            IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
            //            IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
            //            Assert.AreNotEqual(0, datas.Count, "search failed");
            //            select.Click();
            //                test.Pass();
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("doesnt find sub menu");
            //            test.Skip();
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("doesnt find  menu");
            //        test.Skip();
            //    }
            //}
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
        public void SearchSalesFailure()
        {

            expectedStatus = "Passed";

            description = "test case to search company user datas with wrong datas so it returns no data. add invalid data in searchCompanyUserFailure in SearchCompanyUserData file before run";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    LoginSuccess.LoginSuccessCompanyAdmin();
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

                Search_SalesFailure.SearchAction(driver, "menuItem-Transactions", "menuItem-Transactions0", SearchSalesData.searchSalesFailure, 0, "Sales");


            }



            //expectedStatus = "Passed";
            //test = extent.CreateTest("search sales data");
            //description = "test case to search sales datas with wrong datas so it returns no data. add invalid data in searchSalesFailure in SearchSalesData file before run";
            //Login LoginSuccess = new Login();
            //driver = LoginSuccess.getdriver();
            //try
            //{
            //    LoginSuccess.LoginSuccessCompanyAdmin();
            //}
            //catch (Exception ex)
            //{
            //    if (ex.InnerException != null)
            //    {
            //        errorMessage = ex.InnerException.Message;
            //    }
            //}
            //try
            //{
            //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Transactions")));
            //IWebElement menu = driver.FindElement(By.Id("menuItem-Transactions"));
            //if (menu != null)
            //{
            //    menu.Click();
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Transactions0")));
            //    IWebElement submenu = driver.FindElement(By.Id("menuItem-Transactions0"));
            //    if (submenu != null)
            //    {
            //        submenu.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
            //        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
            //        Thread.Sleep(1000);
            //        select.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
            //        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));

            //        foreach (var item in searchOptions)
            //        {
            //                int index = searchOptions.IndexOf(item);
            //                searchOptions[index].Click();
            //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
            //                IWebElement searchInput = driver.FindElement(By.Name("searchText"));
            //                searchInput.Clear();
            //                searchInput.SendKeys(SearchSalesData.searchSalesFailure[item.Text]);
            //                searchInput.SendKeys(Keys.Enter);
            //                Thread.Sleep(4000);
            //                int index1 = index + 1;

            //                IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
            //                IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
            //                Console.WriteLine(datas.Count);
            //                Assert.AreEqual(0, datas.Count, "search failed");

            //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//mat-chip-list)[" + index1 + "]//button")));
            //                IWebElement close = driver.FindElement(By.XPath("(//mat-chip-list)[" + index1 + "]//button"));
            //                close.Click();
            //                select.Click();
            //                test.Pass();
            //            }
            //    }
            //    else
            //    {
            //        Console.WriteLine("doesnt find sub menu");
            //            test.Skip();
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("doesnt find  menu");
            //        test.Skip();
            //    }
            //}
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
