using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.W.Transactions.Purchase;
using VMS_Phase1PortalAT.utls.utilMethods;

namespace VMS_Phase1PortalAT.Modules.Warehouse.ReturnedStock
{
    [TestClass]
    public class DateFilter
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        public required TestContext TestContext { get; set; }
        IWebDriver driver;
        setupData setupDatas = new setupData();
        WriteResultToCSV testResult = new WriteResultToCSV();
        string downloadPath = "";

        private static ExtentReports extent;
        private static ExtentTest test;



        [TestInitialize]
        public void Setup()
        {
            downloadPath = setupDatas.downloadPath;
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;

            extent = ExtentManager.GetInstance();

        }
        [TestMethod]
        public void DateRangeSuccess()
        {


            expectedStatus = "Passed";
            description = "test case to test date filter in Maintainance request with valid dates ranges.";
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
                DateRange.DateTimeAction(driver, "menuItem-W. Transactions", "menuItem-W. Transactions4", PurchaseSearchStockData.DateRange, "Maintenance Request");

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




            //test = extent.CreateTest("Validating  Date Range Success");


            //Login LoginSuccess = new Login();
            //driver = LoginSuccess.getdriver();
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //driver.Manage().Window.Maximize();
            //LoginSuccess.LoginSuccessCompanyAdmin();
            //var filesBeforeDownload = Directory.GetFiles(downloadPath).ToList();

            //IWebElement menu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Warehouse")));
            //if (menu != null)
            //{
            //    menu.Click();

            //    IWebElement submenu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Warehouse4")));
            //    if (submenu != null)
            //    {
            //        submenu.Click();
            //        IWebElement dateRangeBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'date_range')]")));
            //        dateRangeBtn.Click();
            //        Thread.Sleep(3000);
            //        IWebElement calender = driver.FindElement(By.ClassName("owl-dt-container-inner"));
            //        IWebElement calender1 = driver.FindElement(By.TagName("owl-date-time-month-view"));

            //        IWebElement dateElement = calender1.FindElement(By.ClassName("owl-dt-calendar-body"));
            //        IList<IWebElement> dateElements = dateElement.FindElements(By.TagName("td"));

            //        foreach (IWebElement datElement in dateElements)
            //        {
            //            if (datElement.Text.Equals("8"))
            //            {
            //                datElement.Click();
            //            }
            //            if (datElement.Text.Equals("17"))
            //            {
            //                datElement.Click();
            //                break;
            //            }
            //        }
            //        Thread.Sleep(3000);
            //        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
            //        IList<IWebElement> datas = table.FindElements(By.TagName("tr"));

            //        IList<IWebElement> prevoiusdata = datas[0].FindElements(By.TagName("td"));
            //        IWebElement inputValue = driver.FindElement(By.Name("drange"));
            //        string inputValueText = inputValue.GetAttribute("value");

            //        string[] dates = inputValueText.Split('~');
            //        string startDateString = dates[0].Trim();
            //        string endDateString = dates[1].Trim();
            //        int spaceIndex = prevoiusdata[10].Text.IndexOf(' ');
            //        string datePart = spaceIndex >= 0 ? prevoiusdata[10].Text.Substring(0, spaceIndex) : prevoiusdata[10].Text;
            //        DateTime startDate = DateTime.ParseExact(startDateString, "d/M/yyyy", CultureInfo.InvariantCulture);
            //        DateTime endDate = DateTime.ParseExact(endDateString, "d/M/yyyy", CultureInfo.InvariantCulture);
            //        DateTime testDate = DateTime.ParseExact(datePart, "yyyy/M/dd", CultureInfo.InvariantCulture);
            //        bool isBetween = testDate >= startDate && testDate <= endDate;
            //        Assert.IsTrue(isBetween, "failed");

            //        test.Pass();    
            //    }
            //    else
            //    {
            //        Console.WriteLine("doesnt find sub menu");

            //        test.Skip();   
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("doesnt find menu");

            //    test.Skip();
            //}
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
