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

namespace VMS_Phase1PortalAT.Modules.Products.Product_List.Branch_Level
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
        public void EditProductSuccess()
        {

            test = extent.CreateTest("Validating Edit Product Success");


            expectedStatus = "Passed";
            description = "test case to test edit branch level product. add valid data in EditProductSuccess in EditBranchLevelProductData file";
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
                            Thread.Sleep(1500);

                            char s=(char)new Random().Next('A', 'Z' + 1);
                        wait.Until(drv => driver.FindElement(By.Name("name")));
                        IWebElement name = driver.FindElement(By.Name("name"));
                        IWebElement hsnCode = driver.FindElement(By.Name("hsnCode"));
                        IWebElement customProductId = driver.FindElement(By.Name("customProductId"));
                        //wait.Until(drv => driver.FindElement(By.XPath("//span[contains(text(),'Add GST Information')]")));
                        //driver.FindElement(By.XPath("//span[contains(text(),'Add GST Information')]")).Click();
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-form-field[.//mat-label[contains(text(),'MRP')]]//input")));
                        IWebElement mrp = driver.FindElement(By.XPath("//mat-form-field[.//mat-label[contains(text(),'MRP')]]//input"));
                        IWebElement cgst = driver.FindElement(By.XPath("//mat-form-field[.//mat-label[contains(text(),'CGST')]]//input"));
                        IWebElement sgst = driver.FindElement(By.XPath("//mat-form-field[.//mat-label[contains(text(),'SGST')]]//input"));
                        IWebElement utgst = driver.FindElement(By.XPath("//mat-form-field[.//mat-label[contains(text(),'UTGST')]]//input"));
                        IWebElement cess = driver.FindElement(By.XPath("//mat-form-field[.//mat-label[contains(text(),'Cess')]]//input"));
                        IWebElement taxablePriceCovS = driver.FindElement(By.Name("taxablePriceCovS"));
                        IWebElement taxablePriceCovUT = driver.FindElement(By.Name("taxablePriceCovUT"));
                        IWebElement updateClientLevel = driver.FindElement(By.Name("applytoAll"));
                        IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                        name.Clear();
                        name.SendKeys(EditBranchLevelProductData.EditProductSuccess["name"]+s);
                        hsnCode.Clear();
                        hsnCode.SendKeys(EditBranchLevelProductData.EditProductSuccess["hsnCode"]);
                        customProductId.Clear();
                        customProductId.SendKeys(EditBranchLevelProductData.EditProductSuccess["customProductId"]);
                        mrp.Clear();
                        mrp.SendKeys(EditBranchLevelProductData.EditProductSuccess["mrp"]);
                        cgst.Clear();
                        cgst.SendKeys(EditBranchLevelProductData.EditProductSuccess["cgst"]);
                        sgst.Clear();
                        sgst.SendKeys(EditBranchLevelProductData.EditProductSuccess["sgst"]);
                        utgst.Clear();
                        utgst.SendKeys(EditBranchLevelProductData.EditProductSuccess["utgst"]);
                        cess.Clear();
                        cess.SendKeys(EditBranchLevelProductData.EditProductSuccess["cess"]);
                        taxablePriceCovS.Clear();
                        taxablePriceCovS.SendKeys(EditBranchLevelProductData.EditProductSuccess["taxablePriceCovS"]);
                        taxablePriceCovUT.Clear();
                        taxablePriceCovUT.SendKeys(EditBranchLevelProductData.EditProductSuccess["taxablePriceCovUT"]);
                        updateClientLevel.Click();
                        submit.Click();
                        string title = "Product Updated";
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".mat-snack-bar-container")));
                        IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                        bool isSuccess = false;
                            Console.WriteLine(snackbar.Text);
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
        public void EditProductBtnDisable()
        {
            test = extent.CreateTest("Validating Edit Product BtnDisable");


            expectedStatus = "Passed";
            description = "test case to test edit product with invalid datas. add invalid data in EditProductBtnDisable in EditBranchLevelProductData file before run";
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
