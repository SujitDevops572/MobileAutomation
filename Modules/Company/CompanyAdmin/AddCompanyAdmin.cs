using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.Modules.Authentication;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using AventStack.ExtentReports;
using VMS_Phase1PortalAT.utls.datas.Company.CompanyAdmin;
using VMS_Phase1PortalAT.utls.datas;


namespace VMS_Phase1PortalAT.Modules.Company.CompanyAdmin
{
    [TestClass]
    public class AddCompanyAdmin
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
        public void AddCompanyAdminSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("test case to test add company admin. make sure you have valid data in addCompanyAdminSuccess in AddCompanyAdminData");
            description = "test case to test add company admin. make sure you have valid data in addCompanyAdminSuccess in AddCompanyAdminData";
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
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company2"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        Console.WriteLine(prevoiusdata.Text);
                        IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement username = driver.FindElement(By.Name("username"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement password = driver.FindElement(By.Name("password"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));

                            name.SendKeys(AddCompanyAdminData.addCompanyAdminSuccess["name"]);
                            username.SendKeys(AddCompanyAdminData.addCompanyAdminSuccess["username"]);
                            email.SendKeys(AddCompanyAdminData.addCompanyAdminSuccess["email"]);
                            password.SendKeys(AddCompanyAdminData.addCompanyAdminSuccess["password"]);
                            Thread.Sleep(1000);
                            submit.Click();
                            Thread.Sleep(2000);
                            IWebElement table1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
                            IWebElement currentdata = driver.FindElement(By.TagName("td"));
                            Assert.AreNotEqual(currentdata, prevoiusdata, "no new data added");
                            test.Pass();
                           
                        }
                        else
                        {
                            Console.WriteLine("doesnt find add null");
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
        public void AddCompanyAdminFailure()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("test case to test add company admin. make sure you have invalid data in addCompanyAdminFailure in AddCompanyAdminData ");
            description = "test case to test add company admin. make sure you have invalid data in addCompanyAdminFailure in AddCompanyAdminData";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company2")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company2"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("name")));
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement username = driver.FindElement(By.Name("username"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement password = driver.FindElement(By.Name("password"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));


                            name.SendKeys(AddCompanyAdminData.addCompanyAdminFailure["name"]);
                            username.SendKeys(AddCompanyAdminData.addCompanyAdminFailure["username"]);
                            email.SendKeys(AddCompanyAdminData.addCompanyAdminFailure["email"]);
                            password.SendKeys(AddCompanyAdminData.addCompanyAdminFailure["password"]);
                            submit.Click();
                            System.Threading.Thread.Sleep(3000);
                            IWebElement table1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
                            IWebElement currentdata = driver.FindElement(By.TagName("td"));
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
        public void AddCompanyAdminBtnDisableWithInvalidField()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("test case to test add company admin with invalid datas. add invalid datas in addCompanyAdminBtnDisable in AddCompanyAdminData file before run");
            description = "test case to test add company admin with invalid datas. add invalid datas in addCompanyAdminBtnDisable in AddCompanyAdminData file before run";
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
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company2"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement username = driver.FindElement(By.Name("username"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement password = driver.FindElement(By.Name("password"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));


                            name.SendKeys(AddCompanyAdminData.addCompanyAdminBtnDisable["name"]);
                            username.SendKeys(AddCompanyAdminData.addCompanyAdminBtnDisable["username"]);
                            email.SendKeys(AddCompanyAdminData.addCompanyAdminBtnDisable["email"]);
                            password.SendKeys(AddCompanyAdminData.addCompanyAdminBtnDisable["password"]);


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

        [TestMethod]
        public void AddCompanyAdminBtnDisableWithoutRequired()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("test case to test add company admin without required datas.add valid data in addCompanyAdminBtnDisableWithoutRequired in AddCompanyAdminData file before run ");
            description = "test case to test add company admin without required datas.add valid data in addCompanyAdminBtnDisableWithoutRequired in AddCompanyAdminData file before run";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company2")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company2"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement username = driver.FindElement(By.Name("username"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement password = driver.FindElement(By.Name("password"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));


                            name.SendKeys(AddCompanyAdminData.addCompanyAdminBtnDisableWithoutRequired["name"]);
                            username.SendKeys(AddCompanyAdminData.addCompanyAdminBtnDisableWithoutRequired["username"]);
                            email.SendKeys(AddCompanyAdminData.addCompanyAdminBtnDisableWithoutRequired["email"]);
                            password.SendKeys(AddCompanyAdminData.addCompanyAdminBtnDisableWithoutRequired["password"]);


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
