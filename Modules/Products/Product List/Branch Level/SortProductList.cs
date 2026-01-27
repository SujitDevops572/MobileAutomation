using AventStack.ExtentReports;
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
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Products.Product_List.Branch_Level
{
    [TestClass]
    public class SortProductList
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
        public void SortProductListSuccess()
        {
            expectedStatus = "Passed";
            description = "test case to test sort refill request with available options. make sure refill request has multiple datas before run";
            Login login = new Login();
            driver = login.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    login.LoginSuccessCompanyAdmin();
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
                SortSuccess_MacReq.Sort(driver, "menuItem-Products", "menuItem-Products3", "Branch Level", "//div[contains(text(),'Branch')]");

            }





            //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
            //    IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
            //    if (menu != null)
            //    {
            //        menu.Click();
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products3")));
            //        IWebElement submenu = driver.FindElement(By.Id("menuItem-Products3"));
            //        if (submenu != null)
            //        {
            //            submenu.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));

            //            IWebElement previousTable = driver.FindElement(By.TagName("tbody"));
            //            IList<IWebElement> previousDatas = previousTable.FindElements(By.TagName("tr"));
            //            IList<IWebElement> previousColumns = previousDatas[0].FindElements(By.TagName("td"));

            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("sortBox")));

            //            IWebElement select = driver.FindElement(By.ClassName("sortBox"));
            //            select.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
            //            IList<IWebElement> sortOptions = driver.FindElements(By.TagName("mat-option"));
            //            foreach (var item in sortOptions)
            //            {
            //                int index = sortOptions.IndexOf(item);
            //                sortOptions[index].Click();
            //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("sortButton")));

            //                IWebElement sortBtn = driver.FindElement(By.ClassName("sortButton"));
            //                sortBtn.Click();
            //                IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
            //                IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
            //                IWebElement firstRow = datas[0];
            //                IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
            //                Assert.AreNotEqual(previousColumns, columns, "Error in sorting");
            //                select.Click();

            //                test.Pass();
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("doesnt find add null");

            //            test.Skip();    
            //        }
            //    }
            //    else
            //    {

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
            }
        }

        [TestMethod]
        public void SortProductListFailure()
        {

            expectedStatus = "Failed";
            description = "test case to test sort refill request with available options. make sure refill request has multiple datas before run. it pass only if there is an error in sorting";
            Login login = new Login();
            driver = login.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    login.LoginSuccessCompanyAdmin();
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
                SortFailure_MacReq.Sort(driver, "menuItem-Products", "menuItem-Products3", "Branch Level", "//div[contains(text(),'Branch')]");
            }


            //test = extent.CreateTest("Validating Sort Product List Failure");


            //expectedStatus = "Failed";
            //description = "test case to test sort branch level product with available options. make sure branch level product has multiple datas before run. it pass only if there is an error in sorting";
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
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products3")));
            //        IWebElement submenu = driver.FindElement(By.Id("menuItem-Products3"));
            //        if (submenu != null)
            //        {
            //            submenu.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));

            //            IWebElement previousTable = driver.FindElement(By.TagName("tbody"));
            //            IList<IWebElement> previousDatas = previousTable.FindElements(By.TagName("tr"));
            //            IList<IWebElement> previousColumns = previousDatas[0].FindElements(By.TagName("td"));
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("sortBox")));
            //            IWebElement select = driver.FindElement(By.ClassName("sortBox"));
            //            select.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
            //            IList<IWebElement> sortOptions = driver.FindElements(By.TagName("mat-option"));
            //            foreach (var item in sortOptions)
            //            {
            //                int index = sortOptions.IndexOf(item);
            //                sortOptions[index].Click();
            //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("sortButton")));
            //                IWebElement sortBtn = driver.FindElement(By.ClassName("sortButton"));
            //                sortBtn.Click();
            //                Thread.Sleep(1000);
            //                IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
            //                IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
            //                IWebElement firstRow = datas[0];
            //                IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
            //                Assert.AreEqual(previousColumns, columns, "Error in sorting");
            //                select.Click();

            //                test.Pass();
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("doesnt find add null");

            //            test.Skip();
            //        }
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
