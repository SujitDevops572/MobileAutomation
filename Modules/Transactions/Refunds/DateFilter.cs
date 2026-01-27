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

namespace VMS_Phase1PortalAT.Modules.Transaction.Refunds
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
        public void DateRangeSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest(" Data Filter for refund");
            description = "test case to test date filter in refund with valid dates ranges";
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
                IWebElement menu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Transactions")));
                if (menu != null)
                {
                    menu.Click();

                    IWebElement submenu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Transactions1")));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement dateRangeBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'date_range')]")));
                        dateRangeBtn.Click();
                        Thread.Sleep(3000);


                        IWebElement nextButton = driver.FindElement(By.XPath("//button[@aria-label='Next month']"));
                        IWebElement prevButton = driver.FindElement(By.XPath("//button[@aria-label='Previous month']"));

                        IWebElement currentMonthLabel = driver.FindElement(By.ClassName("owl-dt-calendar-control-content"));

                        while (!currentMonthLabel.Text.Contains("Jan 2024"))
                        {
                            prevButton.Click();
                            Thread.Sleep(500);
                            currentMonthLabel = driver.FindElement(By.ClassName("owl-dt-calendar-control-content"));
                        }

                        IWebElement calender = driver.FindElement(By.ClassName("owl-dt-container-inner"));
                        IWebElement calender1 = driver.FindElement(By.TagName("owl-date-time-month-view"));

                        IWebElement dateElement = calender1.FindElement(By.ClassName("owl-dt-calendar-body"));
                        IList<IWebElement> dateElements = dateElement.FindElements(By.TagName("td"));
                        Console.WriteLine(currentMonthLabel.Text + " current month");
                        foreach (IWebElement datElement in dateElements)
                        {
                            if (datElement.Text.Equals("4"))
                            {
                                datElement.Click();
                            }
                            if (datElement.Text.Equals("29"))
                            {
                                datElement.Click();
                                break;
                            }
                        }
                        Thread.Sleep(3000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> datas = table.FindElements(By.TagName("tr"));

                        IList<IWebElement> prevoiusdata = datas[0].FindElements(By.TagName("td"));
                        IWebElement inputValue = driver.FindElement(By.Name("drange"));
                        string inputValueText = inputValue.GetAttribute("value");

                        string[] dates = inputValueText.Split('~');
                        string startDateString = dates[0].Trim();
                        string endDateString = dates[1].Trim();
                        int spaceIndex = prevoiusdata[8].Text.IndexOf(' ');
                        string datePart = spaceIndex >= 0 ? prevoiusdata[8].Text.Substring(0, spaceIndex) : prevoiusdata[8].Text;


                        DateTime startDate = DateTime.ParseExact(startDateString, "d/M/yyyy", CultureInfo.InvariantCulture);
                        DateTime endDate = DateTime.ParseExact(endDateString, "d/M/yyyy", CultureInfo.InvariantCulture);
                        DateTime testDate = DateTime.ParseExact(datePart, "yyyy/M/dd", CultureInfo.InvariantCulture);

                        bool isBetween = testDate >= startDate && testDate <= endDate;

                        // Assert
                        Assert.IsTrue(isBetween, "failed");
                        test.Pass();
                    }
                    else
                    {
                        Console.WriteLine("doesnt find sub menu");
                        test.Fail();
                    }
                }
                else
                {
                    Console.WriteLine("doesnt find menu");
                    test.Fail();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                    test.Fail(errorMessage );
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
