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
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Company.Client
{
    [TestClass]
    public class SortClient
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
        public void SortClientSuccess()
        {
            
            
            expectedStatus = "Passed";
            description = "test case sort client. make sure you have multiple datas";
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
                SortSuccess.Sort(driver, "menuItem-Company", "menuItem-Company1", "Client");

            }



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
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company1")));
            //        IWebElement submenu = driver.FindElement(By.Id("menuItem-Company1"));
            //        if (submenu != null)
            //        {
            //            submenu.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
            //            IWebElement previousTable = driver.FindElement(By.TagName("tbody"));
            //            IList<IWebElement> previousDatas = previousTable.FindElements(By.TagName("tr"));
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
            //            IList<IWebElement> previousColumns = previousDatas[0].FindElements(By.TagName("td"));
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("sortBox")));

            //            IWebElement select = driver.FindElement(By.ClassName("sortBox"));
            //            select.Click();
            //            IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
            //            searchOptions[1].Click();

            //            IWebElement sortBtn = driver.FindElement(By.ClassName("sortButton"));
            //            Console.WriteLine(sortBtn.Text + " sorted");
            //            sortBtn.Click();
            //            Thread.Sleep(2000);
            //            IWebElement table = driver.FindElement(By.TagName("tbody"));
            //            IList<IWebElement> datas = table.FindElements(By.TagName("tr"));

            //            IWebElement firstRow = datas[0];
            //            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));


            //            Assert.AreNotEqual(previousColumns, columns, "Error in sorting");

            //            test.Pass();
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
        public void SortClientFailure()
        {
            

            expectedStatus = "Failed";
            description = "test case sort client failure. make sure you have multiple datas";
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
                SortFailure.Sort(driver, "menuItem-Company", "menuItem-Company1", "Client");

            }


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
            //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company1")));
            //        IWebElement submenu = driver.FindElement(By.Id("menuItem-Company1"));
            //        if (submenu != null)
            //        {
            //            submenu.Click();
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
            //            IWebElement previousTable = driver.FindElement(By.TagName("tbody"));
            //            IList<IWebElement> previousDatas = previousTable.FindElements(By.TagName("tr"));
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
            //            IList<IWebElement> previousColumns = previousDatas[0].FindElements(By.TagName("td"));
            //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("sortBox")));

            //            IWebElement select = driver.FindElement(By.ClassName("sortBox"));
            //            select.Click();
            //            IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
            //            searchOptions[1].Click();

            //            IWebElement sortBtn = driver.FindElement(By.ClassName("sortButton"));
            //            Console.WriteLine(sortBtn.Text + " sorted");
            //            sortBtn.Click();
            //            Thread.Sleep(2000);
            //            IWebElement table = driver.FindElement(By.TagName("tbody"));
            //            IList<IWebElement> datas = table.FindElements(By.TagName("tr"));

            //            IWebElement firstRow = datas[0];
            //            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));


            //            Assert.AreEqual(previousColumns, columns, "Error in sorting");

            //            test.Pass();  
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
