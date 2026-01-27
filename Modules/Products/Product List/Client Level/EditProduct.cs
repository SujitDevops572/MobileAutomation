using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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

namespace VMS_Phase1PortalAT.Modules.Products.Product_List.Client_Level
{
    [TestClass]
    public class EditProduct
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
        [Ignore("Temporarily disabled.")]
        public void EditProductSuccess()
        {

            test = extent.CreateTest("Validating Edit Product Success");

            expectedStatus = "Passed";
            description = "test case to test edit client level product. add valid data in EditProductSuccess in EditClientLevelProductData file";
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
                    IWebElement tabList = wait.Until(drv => driver.FindElement(By.ClassName("mat-tab-list")));
                    Console.WriteLine(tabList.Text);
                    IWebElement tabLabels = tabList.FindElement(By.ClassName("mat-tab-labels"));
                    IWebElement clientTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'Client Level')]")));
                    clientTab.Click();
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
                            if (item.Text.Contains("Edit"))
                            {

                                item.Click();
                                break;
                            }
                        }
                        wait.Until(drv => driver.FindElement(By.Name("name")));
                        IWebElement name = driver.FindElement(By.Name("name"));
                        wait.Until(drv => driver.FindElement(By.Name("mrp")));
                        IWebElement mrp = driver.FindElement(By.Name("mrp"));
                        IWebElement taxablePriceCovS = driver.FindElement(By.Name("taxablePriceCovS"));
                        IWebElement taxablePriceCovUT = driver.FindElement(By.Name("taxablePriceCovUT"));
                        IWebElement utCost = driver.FindElement(By.Name("utCost"));

                        name.Clear();
                        name.SendKeys(EditClientLevelProductData.EditProductSuccess["name"]);
                        mrp.Clear();
                        mrp.SendKeys(EditClientLevelProductData.EditProductSuccess["mrp"]);
                        taxablePriceCovS.Clear();
                        taxablePriceCovS.SendKeys(EditClientLevelProductData.EditProductSuccess["taxablePriceCovS"]);
                        taxablePriceCovUT.Clear();
                        taxablePriceCovUT.SendKeys(EditClientLevelProductData.EditProductSuccess["taxablePriceCovUT"]);
                        taxablePriceCovUT.Clear();
                        taxablePriceCovUT.SendKeys(EditClientLevelProductData.EditProductSuccess["taxablePriceCovUT"]);
                        utCost.Clear();
                        utCost.SendKeys(EditClientLevelProductData.EditProductSuccess["utCost"]);
                        //taxablePriceCovUT.SendKeys(Keys.Enter);
                        //  submit = wait.Until(drv => driver.FindElement(By.ClassName("mat-raised-button")));
                        Thread.Sleep(4000);
                       IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-raised-button")));
                       submit.Click();
                        Thread.Sleep(1000);
                        string title = "Product Updated";
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".mat-snack-bar-container")));
                        IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
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
        public void EditProductBtnDisable()
        {
            test = extent.CreateTest("Validating Edit Product BtnDisable");


            expectedStatus = "Passed";
            description = "test case to test edit client level product with invalid datas";
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
                    IWebElement tabList = wait.Until(drv => driver.FindElement(By.ClassName("mat-tab-list")));
                    Console.WriteLine(tabList.Text);
                    IWebElement tabLabels = tabList.FindElement(By.ClassName("mat-tab-labels"));
                    IWebElement clientTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'Client Level')]")));
                    clientTab.Click();
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
                            if (item.Text.Contains("Edit"))
                            {

                                item.Click();
                                break;
                            }
                        }

                        IWebElement submit = wait.Until(drv => driver.FindElement(By.ClassName("mat-raised-button")));

                        Assert.IsTrue(submit.GetAttribute("disabled").Equals("true"));

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
