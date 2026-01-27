using AventStack.ExtentReports;
using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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

namespace VMS_Phase1PortalAT.Modules.Machines.MachineList
{
    [TestClass]
    public class ViewDetails
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
        public void ViewDetailsSuccess()
        {

            test = extent.CreateTest("Validating redirecting to view details page");

            expectedStatus = "Passed";
            description = "test case to test redirecting to view details page";
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                IWebElement menu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines")));
            if (menu != null)
            {
                menu.Click();
                    IWebElement submenu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines0")));
                    if (submenu != null)
                {
                        submenu.Click();
                        Thread.Sleep(1000);
                        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        select.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        searchOptions[0].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        searchInput.Clear();
                        searchInput.SendKeys(ViewDetailsData.ViewDetailsSuccess["Machine Id"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                    IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                    if (data.Count > 0)
                    {
                        IWebElement firstRow = data[0];
                        IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                        IWebElement lastColumn = columns[columns.Count - 1];
                        lastColumn.Click();
                        IWebElement matMenu = wait.Until(drv => driver.FindElement(By.ClassName("mat-menu-panel")));
                        IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));


                        foreach (var item in menuItems)
                        {
                            if (item.Text.Contains("View Details"))
                            {
                                item.Click();
                                break;
                            }
                        }
                        bool isFound = false;
                        if (driver.Url.Contains("portal/machine-info"))
                        {
                            isFound = true;
                        }

                        Console.WriteLine(driver.Url.ToString());
                        Assert.IsTrue(isFound, "not found the page");

                            test.Pass();
                    }
                    else
                    {
                        Console.WriteLine("doesnt find add null");

                            test.Skip();
                    }
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

                    test.Fail();
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

