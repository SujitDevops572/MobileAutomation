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

namespace VMS_Phase1PortalAT.Modules.Company.CompanyUser
{
    [TestClass]
    public class GetCompanyUserList
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
        public void GetCompanyUserListSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("To display the company user list");
            description = "test case to test company user list displayed";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Company"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company3")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company3"));
                    if (submenu != null)
                    {
                        submenu.Click();

                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));

                        IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
                        Console.WriteLine(datas.Count + " table data count");
                        IWebElement paginatorRange = driver.FindElement(By.ClassName("mat-paginator-range-label"));
                        string totalText = paginatorRange.Text.Split(new string[] { "of" }, StringSplitOptions.None)[1].Trim();
                        int totalNumber = int.Parse(totalText);
                        bool flag = false;
                        if (totalNumber > 0)
                        {
                            if (datas.Count > 0)
                            {
                                flag = true;
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                        Assert.IsTrue(flag);
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

        [TestMethod]
        public void GetCompanyUserRoleListFailure()
        {
            expectedStatus = "Failed";
            test = extent.CreateTest(" Error message if the company user list is not display");
            description = "test case to test company user list is not displayed. this test case pass only if there is any error on getting datas or no data present in database";
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
                IWebElement menu = driver.FindElement(By.Id("menuItem-Company"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company3")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company3"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        if (table != null)
                        {
                            //string title = "No Data";
                            //IWebElement noDataFound = driver.FindElement(By.XPath("//div[contains(text(),' No Data ')]"));
                            //Assert.AreEqual(title, noDataFound.Text, "no data found failed");
                            test.Pass();
                        }
                        else
                        {
                            Console.WriteLine("Table not found");
                            string title = "No Data";
                            IWebElement noDataFound = driver.FindElement(By.XPath("//div[contains(text(),' No Data ')]"));
                            Assert.AreEqual(title, noDataFound.Text, "no data found failed");
                            test.Skip();
                        }
                    }
                    else
                    {
                        Console.WriteLine("doesnt find add null");
                        test.Skip();
                    }
                }
                else
                {
                    Console.WriteLine("menu not found..");
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
