using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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
using VMS_Phase1PortalAT.utls.datas.W.Transactions.RefillRequest;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.RefillRequest
{

    [TestClass]
    public class SearchRefillRequest
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
        public void RefillRequestSearchSuccess()
        {
            expectedStatus = "Passed";
            
            description = "test case to Refill Request Success";
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
                SearchSuccess.SearchAction(driver , "menuItem-W. Transactions" , "menuItem-W. Transactions2" , WTransactionRefillRequestSearch.searchwtransactioRefillRequestSuccess , 1  , "Refill Request");



                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions")));
                //IWebElement menu = driver.FindElement(By.Id("menuItem-W. Transactions"));
                //if (menu != null)
                //{
                //    menu.Click();
                //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions2")));
                //    IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions2"));
                //    if (submenu != null)
                //    {
                //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions2")));
                //        submenu.Click();
                //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                //        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                //        select.Click();
                //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                //        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                //        foreach (var item in searchOptions)
                //        {
                //            Thread.Sleep(1000);
                //            int index = searchOptions.IndexOf(item);
                //            searchOptions[index].Click();
                //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                //            IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                //            searchInput.Clear();


                //            bool clicked = false;
                //            int attempts = 0;

                //            while (!clicked && attempts < 3)
                //            {
                //                try
                //                {
                //                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Name("searchText"))));
                //                    Console.WriteLine(item.Text);
                //                    searchInput.SendKeys(WTransactionRefillRequestSearch.searchwtransactioRefillRequestSuccess[item.Text]);
                //                    searchInput.SendKeys(Keys.Enter);
                //                    Thread.Sleep(1000);
                //                    clicked = true;
                //                }
                //                catch (StaleElementReferenceException)
                //                {
                //                    attempts++;
                //                    Thread.Sleep(1000);
                //                }
                //            }
                //            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
                //            IWebElement table = wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                //            IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
                //            Assert.AreNotEqual(0, datas.Count, "search Success");
                //            IWebElement searchInput1 = driver.FindElement(By.Name("searchText"));
                //            Thread.Sleep(1000);
                //             string  s = searchInput1.Text;
                //            Console.WriteLine(s);
                          
                //            if (searchInput1.Text.Equals(WTransactionRefillRequestSearch.searchwtransactioRefillRequestSuccess[item.Text]))
                //            { test.Pass(); }
                //            else { test.Fail("Not Matched the searchText"); }
                //            select.Click();
                            
                //        }
                       
                //    }
                //    else
                //    {
                //        Console.WriteLine("doesnt find sub menu");
                //        test.Fail();
                //    }
                //}
                //else
                //{
                //    Console.WriteLine("doesnt find  menu");
                //    test.Fail();
                //}
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
        public void RefillRequestSearchFailure()
        {
            expectedStatus = "Passed";
           
            description = "test case tow.Transaction Refill Request Search Stock Failure";
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
                SearchFailure.SearchAction(driver, "menuItem-W. Transactions", "menuItem-W. Transactions2", WTransactionRefillRequestSearch.searchwtransactioRefillRequestFailure,0, "Refill Request");

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
            string formatedTime = $"{timeTaken.TotalSeconds:F2}";
            driver.Quit();
            testResult.WriteTestResults(TestContext, formatedTime, expectedStatus, errorMessage, description);

            extent.Flush();
        }
    }
}