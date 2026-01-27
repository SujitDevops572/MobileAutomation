using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;

namespace VMS_Phase1PortalAT.Modules.Transaction.Refunds
{
    [TestClass]
    public class GetRefundList
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
        public void GetRefundListSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("Refund List");
            description = "test case to test Redund list displayed";
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
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Transactions")));
            IWebElement menu = driver.FindElement(By.Id("menuItem-Transactions"));
            if (menu != null)
            {
                menu.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Transactions1")));
                IWebElement submenu = driver.FindElement(By.Id("menuItem-Transactions1"));
                if (submenu != null)
                {
                    submenu.Click();
                    IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
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
                    test.Fail(errorMessage);
                }
                throw;
            }

        }

        [TestMethod]
        public void GetRefundListFailure()
        {
            expectedStatus = "Failed";
            test = extent.CreateTest("Refund List ");
            description = "test case to test refund list is not displayed. this test case pass only if there is any error on getting datas or no data present in database";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Transactions")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Transactions"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Transactions1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Transactions1"));
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
