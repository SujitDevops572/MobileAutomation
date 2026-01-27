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
using VMS_Phase1PortalAT.utls.datas.Company.CompanyUser;
using VMS_Phase1PortalAT.utls.datas.Products.Brand;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Products.Brand
{
    [TestClass]
    public class SearchBrand
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
        public void SearchBrandSuccess()
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

                SearchSuccess.SearchAction(driver, "menuItem-Products", "menuItem-Products0", SearchBrandData.searchBrandSuccess, 1, "Brand");


            }





            //test = extent.CreateTest("Validating Search Brand Success");

            //expectedStatus = "Passed";
            //description = "test case to test brand list displayed. add valid data in searchBrandSuccess in SearchBrandData file";
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
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
            //    IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
            //    if (menu != null)
            //    {
            //        menu.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products0")));
            //        IWebElement submenu = driver.FindElement(By.Id("menuItem-Products0"));
            //        if (submenu != null)
            //        {
            //            submenu.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
            //            IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
            //            Thread.Sleep(1000);
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
            //                searchInput.SendKeys(SearchBrandData.searchBrandSuccess[item.Text]);
            //                searchInput.SendKeys(Keys.Enter);
            //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
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
        public void SearchBrandFailure()
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

                SearchFailure.SearchAction(driver, "menuItem-Products", "menuItem-Products0", SearchBrandData.searchBrandFailure, 0, "Brand");


            }




            //test = extent.CreateTest("Validating Search Brand Failure");


            //expectedStatus = "Passed";
            //description = "test case to test brand search with invalid data. add invalid data in searchBrandFailure in SearchBrandData file";
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
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
            //    IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
            //    if (menu != null)
            //    {
            //        menu.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products0")));
            //        IWebElement submenu = driver.FindElement(By.Id("menuItem-Products0"));
            //        if (submenu != null)
            //        {
            //            submenu.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
            //            IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
            //            Thread.Sleep(1000);
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
            //                searchInput.SendKeys(SearchBrandData.searchBrandFailure[item.Text]);
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
