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
using VMS_Phase1PortalAT.utls.datas.Products.SubCategories;

namespace VMS_Phase1PortalAT.Modules.Products.SubCategories
{
    [TestClass]
    public class ImportSubCategory
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
        public void ImportSubCategorySuccess()
        {

            test = extent.CreateTest("Validating  Import SubCategory Success");

            expectedStatus = "Passed";
            description = "test case to test import sub categories with valid data. add valid data in ImportSubCategorySuccess in ImportSubCategoryData";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products2")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products2"));
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
                            fileInput.SendKeys(ImportSubCategoryData.ImportSubCategorySuccess["inputfile"]);
                            string title = "Subcategory Uploaded";
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
        public void ImportSubCategoryMissingColumns()
        {
            test = extent.CreateTest("Validating Import SubCategory Missing Columns");


            expectedStatus = "Passed";
            description = "test case to test import sub categories with missing columns. add valid data in ImportSubCategoryMissingColumn in ImportSubCategoryData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products2")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products2"));
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
                            fileInput.SendKeys(ImportSubCategoryData.ImportSubCategoryMissingColumn["inputfile"]);
                            string title = "Selected file has coloumn missing";
                            WebDriverWait waitSnackBar = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitSnackBar.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Assert.IsTrue(isSuccess, "Import Subcategory Failed");
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
        public void ImportSubCategoryFailure()
        {
            test = extent.CreateTest("Validating Import SubCategory Failure");


            expectedStatus = "Passed";
            description = "test case to test import sub categories with invalid data. add invalid data in ImportSubCategoryFailure in ImportSubCategoryData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products2")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products2"));
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
                        Console.WriteLine(import.Text + " add clicked ....");
                        if (import != null)
                        {
                            IWebElement fileInput = driver.FindElement(By.XPath("//input[@type='file']"));
                            fileInput.SendKeys(ImportSubCategoryData.ImportSubCategoryFailure["inputfile"]);
                            string title = "Error Uploading Subcategory";
                            WebDriverWait waitSnackBar = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitSnackBar.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title) || snackbar.Text.Contains("Subcategory Partially added due to erro"))
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
