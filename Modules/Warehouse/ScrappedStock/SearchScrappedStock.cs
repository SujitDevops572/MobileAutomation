using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.PurchaseOrder.ScrappedPurchaseOrder;
using VMS_Phase1PortalAT.utls.datas.Warehouse.CurrentStock;
using VMS_Phase1PortalAT.utls.datas.Warehouse.ScrappedStock;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Warehouse.ScrappedStock
{
    [TestClass]
    public class SearchScrappedStock
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
        public void SearchScrappedStockSuccess()
        {

            expectedStatus = "Passed";

            description = "test case to Search Client Admin List Success";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

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

                SearchSuccess.SearchAction(driver, "menuItem-Warehouse", "menuItem-Warehouse5", SearchScrappedStockeData.searchScrappedStockSuccess, 1, "ScrappedStock");


            }




            //test = extent.CreateTest("Validating  Search Scrapped Stock Success");

            //expectedStatus = "Passed";
            //description = "test case to search scrapped stock datas with valid datas so it return datas. add valid data in searchScrappedStockSuccess in SearchScrappedStockeData file before run";
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
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse")));
            //    IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
            //    if (menu != null)
            //    {
            //        menu.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse5")));
            //        IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse5"));
            //        if (submenu != null)
            //        {
            //            submenu.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
            //            IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
            //            select.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
            //            IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
            //            foreach (var item in searchOptions)
            //            {
            //                int index = searchOptions.IndexOf(item);
            //                searchOptions[index].Click();
            //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
            //                IWebElement searchInput = driver.FindElement(By.Name("searchText"));
            //                searchInput.Clear();
            //                searchInput.SendKeys(SearchScrappedStockeData.searchScrappedStockSuccess[item.Text]);
            //                searchInput.SendKeys(Keys.Enter);
            //                Thread.Sleep(1000);
            //                IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
            //                IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
            //                Assert.AreNotEqual(0, datas.Count, "search failed");
            //                select.Click();

            //                test.Pass();
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("doesnt find sub menu");

            //            test.Skip();
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("doesnt find  menu");

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
        public void SearchScrappedStockFailure()
        {

            expectedStatus = "Passed";

            description = "test case to Purchase Search Current Purchase Order Failure";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    LoginSuccess.LoginSuccessCompanyAdmin();
                    return driver.FindElements(By.Id("menuItem-Purchase Order")).Any();
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

                SearchFailure.SearchAction(driver, "menuItem-Warehouse", "menuItem-Warehouse5", SearchScrappedStockeData.searchScrappedStockFailure, 0, "Scrapped Stock");

            }




            //test = extent.CreateTest("Validating Search Scrapped Stock Failure");


            //expectedStatus = "Passed";
            //description = "test case to search scrapped stock datas with valid datas so it return datas. add valid data in searchScrappedStockFailure in SearchScrappedStockeData file before run";
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
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse")));
            //    IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
            //    if (menu != null)
            //    {
            //        menu.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse5")));
            //        IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse5"));
            //        if (submenu != null)
            //        {
            //            submenu.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
            //            IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
            //            select.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
            //            IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
            //            Console.WriteLine(searchOptions.Count);
            //            foreach (var item in searchOptions)
            //            {
            //                int index = searchOptions.IndexOf(item);
            //                searchOptions[index].Click();
            //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
            //                IWebElement searchInput = driver.FindElement(By.Name("searchText"));
            //                searchInput.Clear();
            //                searchInput.SendKeys(SearchScrappedStockeData.searchScrappedStockFailure[item.Text]);
            //                searchInput.SendKeys(Keys.Enter);
            //                Thread.Sleep(2000);
            //                IWebElement table = driver.FindElement(By.TagName("tbody"));
            //                IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
            //                Assert.AreEqual(0, datas.Count, "search failed");
            //                select.Click();

            //                test.Pass();   
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("doesnt find sub menu");

            //            test.Skip();
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("doesnt find menu");

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
