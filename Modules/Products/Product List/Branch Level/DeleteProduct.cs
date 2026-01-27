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
using VMS_Phase1PortalAT.utls.datas.Products.ProductList;

namespace VMS_Phase1PortalAT.Modules.Products.Product_List.Branch_Level
{
    [TestClass]
    public class DeleteProduct
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
        public void ProductDeleteSuccess()
        {
            test = extent.CreateTest("Validating Product Delete Success");


            expectedStatus = "Passed";
            description = "test case to test delete product. add valid data in addCategorySuccess in AddCategoryData file";
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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products3")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products3"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement branch = driver.FindElement(By.Name("branch"));
                        Thread.Sleep(2000);
                        branch.Clear();
                        branch.SendKeys(DeleteBranchLevelProductData.DeleteProductSuccess["search"]);
                        branch.SendKeys(Keys.Enter);
                        Thread.Sleep(4000);

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
                        searchInput.SendKeys(DeleteBranchLevelProductData.DeleteProductSuccess["name"]);
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
                            Thread.Sleep(2000);
                            string title = "No Data";
                            IWebElement noDataFound = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),' No Data ')]")));
                           
                            Assert.AreEqual(title, noDataFound.Text, "no data found failed");

                            test.Pass();

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

                    test.Fail(errorMessage);
                }
                throw;
            }
        }

        [TestMethod]
        [Ignore("Temporarily disabled.")]
        public void ProductDeleteFailure()
        {
            test = extent.CreateTest("Validating  Product Delete Failure");


            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            LoginSuccess.LoginSuccessCompanyAdmin();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
            IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
            if (menu != null)
            {
                menu.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products3")));
                IWebElement submenu = driver.FindElement(By.Id("menuItem-Products3"));
                if (submenu != null)
                {
                    submenu.Click();
                    IWebElement branch = driver.FindElement(By.Name("branch"));
                    Thread.Sleep(2000);
                    branch.Clear();
                    branch.SendKeys("trivandrum");
                    branch.SendKeys(Keys.Enter);
                    Thread.Sleep(4000);

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
                    searchInput.SendKeys(DeleteBranchLevelProductData.DeleteProductFailure["productId"]);
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
                            if (item.Text.Contains("Delete"))
                            {
                                item.Click();
                                break;
                            }
                        }

                        IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Confirm')]")));
                        Console.WriteLine(submit.Text);
                        submit.Click();
                        string title = "Error Deleting Product";
                        IWebElement snackbar = wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));

                        bool isSuccess = false;
                        if (snackbar.Text.Contains(title))
                        {
                            isSuccess = true;

                        }
                        Assert.IsTrue(isSuccess, " failed..");

                        test.Pass();
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
