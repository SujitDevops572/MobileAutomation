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
using VMS_Phase1PortalAT.utls.datas.Company.CompanyAdmin;
using VMS_Phase1PortalAT.utls.datas.Company.CompanyUserRole;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Company.CompanyUserRole
{
    [TestClass]
    public class SearchCompanyUserRole
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
        public void SearchCompanyUserRoleListSuccess()
        {
            expectedStatus = "Passed";
           
            description = "test case to search company user role datas with valid datas so it return datas. add valid data in searchCompanyUserRoleSuccess in SearchCompanyUserRoleData file before run";

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

                SearchSuccess.SearchAction(driver, "menuItem-Company", "menuItem-Company4", SearchCompanyUserRoleData.searchCompanyUserRoleSuccess, 1, "Company User Role");


            }



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
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company")));
            //    IWebElement menu = driver.FindElement(By.Id("menuItem-Company"));
            //    if (menu != null)
            //    {
            //        menu.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company4")));
            //        IWebElement submenu = driver.FindElement(By.Id("menuItem-Company4"));
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
            //                searchInput.SendKeys(SearchCompanyUserRoleData.searchCompanyUserRoleSuccess[item.Text]);
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
        public void SearchCompanyUserRoleListFailure()
        {
            expectedStatus = "Passed";
           
            description = "test case to search company user role datas with wrong datas so it returns no data. add invalid data in searchCompanyUserRoleFailure in SearchCompanyUserRoleData file before run";

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

                SearchFailure.SearchAction(driver, "menuItem-Company", "menuItem-Company4", SearchCompanyUserRoleData.searchCompanyUserRoleFailure, 0, "Company User Role");


            }



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
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company")));
            //    IWebElement menu = driver.FindElement(By.Id("menuItem-Company"));
            //    if (menu != null)
            //    {
            //        menu.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company4")));
            //        IWebElement submenu = driver.FindElement(By.Id("menuItem-Company4"));
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
            //                searchInput.SendKeys(SearchCompanyUserRoleData.searchCompanyUserRoleFailure[item.Text]);
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
