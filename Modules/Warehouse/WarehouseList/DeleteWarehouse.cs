using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Warehouse.WarehouseList;

namespace VMS_Phase1PortalAT.Modules.Warehouse.WarehouseList
{
    [TestClass]
    public class DeleteWarehouse
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
        public void DeleteWarehouseSuccess()
        {
            test = extent.CreateTest("Validating  Delete Warehouse Success");

            expectedStatus = "Passed";
            description = "test case to test delete warehouse which is not associated to anything. add valid data in DeleteWarehouseSuccess in DeleteWarehouseData file before run";
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
                IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        Thread.Sleep(1000);
                        select.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        searchOptions[1].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        searchInput.Clear();
                        searchInput.SendKeys(DeleteWarehouseData.DeleteWarehouseSuccess["name"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        if (data.Count > 0)
                        {
                            IWebElement firstRow = data[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            IWebElement lastColumn = columns[columns.Count - 1];
                            lastColumn.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                            IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                            foreach (var item in menuItems)
                            {
                                if (item.Text.Contains("Delete"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                            IList<IWebElement> dialogBtns = dialogElement.FindElements(By.TagName("button"));
                            foreach (var item in dialogBtns)
                            {
                                if (item.Text.Contains("Confirm"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            Thread.Sleep(1000);
                            string title = "No Data";
                            IWebElement noDataFound = driver.FindElement(By.XPath("//div[contains(text(),' No Data ')]"));
                            Assert.AreEqual(title, noDataFound.Text, "no data found failed");

                            test.Pass();
                        }
                        else
                        {
                            Console.WriteLine("No rows found in the table.");

                            test.Skip();
                        }
                    }
                    else
                    {
                        Console.WriteLine("doesnt find add null");

                        test.Skip();
                    }
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
        public void DeleteWarehouseFailure()
        {

            test = extent.CreateTest("Validating Delete Warehouse Failure");

            expectedStatus = "Passed";
            description = "test case to test delete warehouse which is associated to something which will not get deleted. add valid data in DeleteWarehouseFailure in DeleteWarehouseData file before run";
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
                IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        Thread.Sleep(1000);
                        select.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        searchOptions[0].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        searchInput.Clear();
                        searchInput.SendKeys(DeleteWarehouseData.DeleteWarehouseFailure["warehouseId"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("td")));
                        IWebElement previousData = driver.FindElement(By.TagName("td"));
                        if (data.Count > 0)
                        {
                            IWebElement firstRow = data[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            IWebElement lastColumn = columns[columns.Count - 1];
                            lastColumn.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                            IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                            foreach (var item in menuItems)
                            {
                                if (item.Text.Contains("Delete"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                            IList<IWebElement> dialogBtns = dialogElement.FindElements(By.TagName("button"));
                            foreach (var item in dialogBtns)
                            {
                                if (item.Text.Contains("Confirm"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            Thread.Sleep(1000);
                            IWebElement table1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            IList<IWebElement> tableDatas = table1.FindElements(By.TagName("tr"));
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("td")));
                            IWebElement currentdata = driver.FindElement(By.TagName("td"));
                            Assert.AreEqual(currentdata, previousData, "no data get deleted");

                            test.Pass();
                        }
                        else
                        {
                            Console.WriteLine("No rows found in the table.");

                            test.Skip();
                        }
                    }
                    else
                    {
                        Console.WriteLine("doesnt find add null");

                        test.Skip();
                    }
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
