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
using VMS_Phase1PortalAT.utls.datas.Products.Categories;

namespace VMS_Phase1PortalAT.Modules.Products.Categories
{
    [TestClass]
    public class ImportCategory
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        public required TestContext TestContext { get; set; }
        setupData setupDatas = new setupData();
        WriteResultToCSV testResult = new WriteResultToCSV();

        private static ExtentReports extent;
        private static ExtentTest test;

        IWebDriver driver;
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
        public void ImportCategorySuccess()
        {

            test = extent.CreateTest("Validating  Import Category Success");

            expectedStatus = "Passed";
            description = "test case to test category import. add valid data ImportCategorySuccess in ImportCategoryData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products1"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        Console.WriteLine(prevoiusdata.Text);
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_upload')]")));
                        IWebElement import = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_upload')]"));
                        if (import != null)
                        {

                            IWebElement fileInput = driver.FindElement(By.XPath("//input[@type='file']"));
                            fileInput.SendKeys(ImportCategoryData.ImportCategorySuccess["inputfile"]);
                            string title = "Category Uploaded";
                            WebDriverWait waitSnackBar = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitSnackBar.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            Thread.Sleep(1000);
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Assert.IsTrue(isSuccess, "Import Category Failed");
                            TestContext.WriteLine("Test passed successfully!");

                            test.Pass();
                        }
                        else
                        {
                            Console.WriteLine("import is null ");

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

                    test.Fail(errorMessage);
                }
                throw;
            }
        }

        [TestMethod]
        [Ignore("Temporarily disabled.")]
        public void ImportCategoryMissingColumns()
        {
            test = extent.CreateTest("Validating Import Category Missing Columns");


            description = "test case to test category import with missing column. add valid data ImportCategoryMissingColumn in ImportCategoryData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products1"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_upload')]")));
                        IWebElement import = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_upload')]"));
                        if (import != null)
                        {
                            IWebElement fileInput = driver.FindElement(By.XPath("//input[@type='file']"));
                            fileInput.SendKeys(ImportCategoryData.ImportCategoryMissingColumn["inputfile"]);
                            string title = "Selected file has coloumn missing";
                            WebDriverWait waitSnackBar = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitSnackBar.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;
                            }
                            Assert.IsTrue(isSuccess, "Import Brand Failed");
                            TestContext.WriteLine("Test passed successfully!");

                            test.Pass();
                        }
                        else
                        {
                            Console.WriteLine("import is null ");

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

                    test.Fail(errorMessage);
                }
                throw;
            }
        }

        [TestMethod]
        [Ignore("Temporarily disabled.")]
        public void ImportCategoryFailure()
        {
            test = extent.CreateTest("Validating  Import Category Failure");


            expectedStatus = "Passed";
            description = "test case to test import failure. add invalid data ImportCategoryFailure in ImportBrandData file";
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
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
            IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
            if (menu != null)
            {
                menu.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products1")));
                IWebElement submenu = driver.FindElement(By.Id("menuItem-Products1"));
                if (submenu != null)
                {
                    submenu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    IWebElement table = driver.FindElement(By.TagName("tbody"));
                    IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
                    IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                    wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_upload')]")));
                    IWebElement import = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_upload')]"));
                    if (import != null)
                    {
                        IWebElement fileInput = driver.FindElement(By.XPath("//input[@type='file']"));
                        fileInput.SendKeys(ImportCategoryData.ImportCategoryFailure["inputfile"]);
                        string title = "Error Uploading Brands";
                        WebDriverWait waitSnackBar = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        waitSnackBar.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                        IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                        bool isSuccess = false;
                        if (snackbar.Text.Contains(title) || snackbar.Text.Contains("Category Partially added due to error"))
                        {
                            isSuccess = true;
                        }
                        Assert.IsTrue(isSuccess, "Import Brand Failed");
                        TestContext.WriteLine("Test passed successfully!");

                        test.Pass();          
                    }
                    else
                    {
                        Console.WriteLine("import is null ");

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
