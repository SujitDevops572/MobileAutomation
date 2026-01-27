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
using VMS_Phase1PortalAT.utls.datas.Company.Client;

namespace VMS_Phase1PortalAT.Modules.Company.Client
{
    [TestClass]
    public class ImportClient
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        public required TestContext TestContext { get; set; }
        setupData setupDatas = new setupData();
        WriteResultToCSV testResult = new WriteResultToCSV();
        
        IWebDriver driver;

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
        public void ImportClientSuccess()

        {

            test = extent.CreateTest("test case to test import client");

            expectedStatus = "Passed";
            description = "test case to test import client";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company1"));
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
                            fileInput.SendKeys(ImportClientData.ImportClientSuccess["inputfile"]);

                            string title = "Clients Uploaded";
                            WebDriverWait waitSnackBar = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitSnackBar.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));

                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Assert.IsTrue(isSuccess, "Import Client Failed");
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
        public void ImportClientMissingColumns()
        {
            test = extent.CreateTest("test case to test import client with missing column. add invalid data with missing columns in ImportClientFailure in ImportClientData");

            expectedStatus = "Passed";
            description = "test case to test import client with missing column. add invalid data with missing columns in ImportClientFailure in ImportClientData";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company1"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_upload')]")));
                        IWebElement import = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_upload')]"));
                        if (import != null)
                        {
                            IWebElement fileInput = driver.FindElement(By.XPath("//input[@type='file']"));
                            fileInput.SendKeys(ImportClientData.ImportClientMissingColumn["inputfile"]);
                            string title = "Selected file has coloumn missing";
                            WebDriverWait waitSnackBar = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitSnackBar.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Assert.IsTrue(isSuccess, "Import Client Failed");
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
        public void ImportClientFailure()
        {
            test = extent.CreateTest("test case to test import client with invalid data. add invalid data in ImportClientFailure in ImportClientData");

            expectedStatus = "Passed";
            description = "test case to test import client with invalid data. add invalid data in ImportClientFailure in ImportClientData";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company1"));
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
                            fileInput.SendKeys(ImportClientData.ImportClientFailure["inputfile"]);
                            string title = "Malformed Client Data";
                            WebDriverWait waitSnackBar = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitSnackBar.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Assert.IsTrue(isSuccess, "Import Client Failed");
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
