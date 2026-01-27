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
using VMS_Phase1PortalAT.utls.datas.Company.Branch;

namespace VMS_Phase1PortalAT.Modules.Company.Branch
{
    [TestClass]
    public class ImportBranch
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
        public void ImportBranchSuccess()
        {
            test = extent.CreateTest("test case to test branch import. add valid data ImportBranchSuccess in ImportBranchData file");


            expectedStatus = "Passed";
            description = "test case to test branch import. add valid data ImportBranchSuccess in ImportBranchData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        Thread.Sleep(3000);
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_upload')]")));
                        IWebElement import = driver.FindElement(By.XPath("//mat-icon[contains(text(),'cloud_upload')]"));
                        if (import != null)
                        {
                            IWebElement fileInput = driver.FindElement(By.XPath("//input[@type='file']"));
                            fileInput.SendKeys(importBranchData.ImportBranchSuccess["inputfile"]);
                            Thread.Sleep(3000);
                            string title = "Branchs Uploaded";
                            WebDriverWait waitSnackBar = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitSnackBar.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Assert.IsTrue(isSuccess, "Import Branch Failed");
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
        public void ImportBranchMissingColumns()
        {
            test = extent.CreateTest("test case to test import branch. add valid data ImportBranchMissingColumn in ImportBranchData file");

            expectedStatus = "Passed";
            description = "test case to test import branch. add valid data ImportBranchMissingColumn in ImportBranchData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company0"));
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
                            fileInput.SendKeys(importBranchData.ImportBranchMissingColumn["inputfile"]);
                            string title = "Selected file has coloumn missing";
                            WebDriverWait waitSnackBar = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitSnackBar.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;
                            }
                            Assert.IsTrue(isSuccess, "Import Branch missing column not Failed");
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
        public void ImportBranchFailure()
        {
            test = extent.CreateTest("test case to test branch. add valid data ImportBranchFailure in ImportBranchData file");


            expectedStatus = "Passed";
            description = "test case to test branch. add valid data ImportBranchFailure in ImportBranchData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company0"));
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
                            fileInput.SendKeys(importBranchData.ImportBranchFailure["inputfile"]);
                            string title = "Selected file has malformed data";
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
