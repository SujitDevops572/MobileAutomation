using AventStack.ExtentReports;
using Docker.DotNet.Models;
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
using VMS_Phase1PortalAT.utls.datas.Company.CompanyUserRole;

namespace VMS_Phase1PortalAT.Modules.Company.CompanyUserRole
{
    [TestClass]
    public class AddCompanyUserRole
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
        [Priority(0)]
        public void AddCompanyUserRoleSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("Add user role");
            description = "test case to test add company user role with valid datas. add valid data in addCompanyUserRoleSuccess in AddCompanyUserRoleData  file before run";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company4")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company4"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement role = driver.FindElement(By.Name("role"));
                            var role1 =wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("role")));
                            role.SendKeys(AddCompanyUserRoleData.addCompanyUserRoleSuccess["role"]);
                     
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            submit.Click();
                            Console.WriteLine(submit.Text + "submitText");
                            Thread.Sleep(4000);
                            IWebElement table1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
                            IWebElement currentdata = driver.FindElement(By.TagName("td"));
                            Console.WriteLine(currentdata.Text);
                           
                            Assert.AreNotEqual(currentdata, prevoiusdata, "no new data added");
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
                    test.Fail(errorMessage);
                }
                throw;
            }
        }

        [TestMethod]
        public void AddCompanyUserRoleDuplicateEntry()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("Add company user with duplicate data");
            description = "test case to test add company user role with duplicate datas. add duplicate data in addCompanyUserRoleDuplicate in AddCompanyUserRoleData file before run";
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
                IWebElement menu = driver.FindElement(By.Id("menuItem-Company"));
                if (menu != null)
                {
                    menu.Click();
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company4"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        Thread.Sleep(3000);
                        IWebElement table = driver.FindElement(By.TagName("tbody"));

                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        Console.WriteLine(prevoiusdata.Text);
                        Thread.Sleep(3000);
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement role = driver.FindElement(By.Name("role"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));

                            role.SendKeys(AddCompanyUserRoleData.addCompanyUserRoleDuplicate["role"]);
                            submit.Click();
                            IWebElement table1 = driver.FindElement(By.TagName("tbody"));

                            IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
                            IWebElement currentdata = driver.FindElement(By.TagName("td"));
                            Console.WriteLine(currentdata.Text);
                            Thread.Sleep(3000);
                            Assert.AreEqual(currentdata, prevoiusdata, "new data added");
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
                    test.Fail(errorMessage);
                }
                throw;
            }
        }


        [TestMethod]
        public void AddCompanyUserRoleBtnDisable()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("Add company user role with invalid datas");
            description = "test case to test add company user role with invalid datas.";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company4")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company4"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            Thread.Sleep(4000);
                            Assert.IsTrue(submit.GetAttribute("disabled").Equals("true"));
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
