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
using VMS_Phase1PortalAT.utls.datas.Company.CompanyUser;

namespace VMS_Phase1PortalAT.Modules.Company.CompanyUser
{
    [TestClass]
    public class AddCompanyUser
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
        public void AddCompanyUserSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("test case to test add company user with valid datas. add valid data in addCompanyUserSuccess in AddCompanyUserData file before run");
            description = "test case to test add company user with valid datas. add valid data in addCompanyUserSuccess in AddCompanyUserData file before run";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company3")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company3"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        Console.WriteLine(prevoiusdata.Text);
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement username = driver.FindElement(By.Name("username"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement mobile = driver.FindElement(By.Name("mobile"));
                            IWebElement client = driver.FindElement(By.Name("client"));
                            IWebElement password = driver.FindElement(By.Name("password"));
                            IWebElement leanCloudAccess = driver.FindElement(By.TagName("mat-slide-toggle"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));


                            name.SendKeys(AddCompanyUserData.addCompanyUserSuccess["name"]);
                            username.SendKeys(AddCompanyUserData.addCompanyUserSuccess["username"]);
                            email.SendKeys(AddCompanyUserData.addCompanyUserSuccess["email"]);
                            mobile.SendKeys(AddCompanyUserData.addCompanyUserSuccess["mobile"]);
                            password.SendKeys(AddCompanyUserData.addCompanyUserSuccess["password"]);
                            client.SendKeys(AddCompanyUserData.addCompanyUserSuccess["client"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> optionss = driver.FindElements(By.TagName("mat-option"));
                            foreach (IWebElement option in optionss)
                            {
                                Console.WriteLine(option.Text);

                                if (option.Text.Contains(AddCompanyUserData.addCompanyUserSuccess["client"]))
                                {
                                    option.Click();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Client doesn't exist");
                                }
                            }
                            Thread.Sleep(3000);
                            submit.Click();
                            
                            string s = "Company User Added";
                            wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                           
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(s))
                            {
                                isSuccess = true;

                            }



                            //IWebElement table1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            //IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
                            //IWebElement currentdata = driver.FindElement(By.TagName("td"));

                            Assert.IsTrue(isSuccess, "no new data added");
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
        public void AddCompanyUserFailure()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("test case to test add company user with duplicate datas. add duplicate data in addCompanyUserFailure in AddCompanyUserData file before run ");
            description = "test case to test add company user with duplicate datas. add duplicate data in addCompanyUserFailure in AddCompanyUserData file before run";
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
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company3"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        Console.WriteLine(prevoiusdata.Text);
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement username = driver.FindElement(By.Name("username"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement mobile = driver.FindElement(By.Name("mobile"));
                            IWebElement client = driver.FindElement(By.Name("client"));
                            IWebElement password = driver.FindElement(By.Name("password"));
                            IWebElement leanCloudAccess = driver.FindElement(By.TagName("mat-slide-toggle"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));

                            name.SendKeys(AddCompanyUserData.addCompanyUserFailure["name"]);
                            username.SendKeys(AddCompanyUserData.addCompanyUserFailure["username"]);
                            email.SendKeys(AddCompanyUserData.addCompanyUserFailure["email"]);
                            mobile.SendKeys(AddCompanyUserData.addCompanyUserFailure["mobile"]);
                            password.SendKeys(AddCompanyUserData.addCompanyUserFailure["password"]);

                            client.SendKeys(AddCompanyUserData.addCompanyUserFailure["client"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> optionss = driver.FindElements(By.TagName("mat-option"));
                            foreach (IWebElement option in optionss)
                            {
                                Console.WriteLine(option.Text);

                                if (option.Text.Contains(AddCompanyUserData.addCompanyUserFailure["client"]))
                                {
                                    option.Click();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Client doesn't exist");
                                }
                            }
                            submit.Click();
                            Thread.Sleep(3000);
                            IWebElement table1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
                            IWebElement currentdata = driver.FindElement(By.TagName("td"));
                            Console.WriteLine(currentdata.Text);
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
        public void AddCompanyUserBtnDisableWithInvalidFields()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("test case to test add company user with invalid datas. add valid data in addCompanyUserBtnDisable in AddCompanyUserData file before run");
            description = "test case to test add company user with invalid datas. add valid data in addCompanyUserBtnDisable in AddCompanyUserData file before run";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company3")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company3"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement username = driver.FindElement(By.Name("username"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            name.SendKeys(AddCompanyUserData.addCompanyUserBtnDisable["name"]);
                            username.SendKeys(AddCompanyUserData.addCompanyUserBtnDisable["username"]);
                            email.SendKeys(AddCompanyUserData.addCompanyUserBtnDisable["email"]);
                            Thread.Sleep(2000);
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
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
        public void AddCompanyUserBtnDisableWithoutRequired()
        {
            expectedStatus = "Passed";

            test = extent.CreateTest("test case to test add company user with invalid datas. add valid data in addCompanyUserBtnDisableWithoutRequired in AddCompanyUserData file before run");
            description = "test case to test add company user with invalid datas. add valid data in addCompanyUserBtnDisableWithoutRequired in AddCompanyUserData file before run";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company3")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company3"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement username = driver.FindElement(By.Name("username"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement mobile = driver.FindElement(By.Name("mobile"));
                            IWebElement client = driver.FindElement(By.Name("client"));
                            IWebElement password = driver.FindElement(By.Name("password"));
                            IWebElement leanCloudAccess = driver.FindElement(By.TagName("mat-slide-toggle"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));

                            name.SendKeys(AddCompanyUserData.addCompanyUserBtnDisableWithoutRequired["name"]);
                            username.SendKeys(AddCompanyUserData.addCompanyUserBtnDisableWithoutRequired["username"]);
                            email.SendKeys(AddCompanyUserData.addCompanyUserBtnDisableWithoutRequired["email"]);
                            mobile.SendKeys(AddCompanyUserData.addCompanyUserBtnDisableWithoutRequired["mobile"]);
                            password.SendKeys(AddCompanyUserData.addCompanyUserBtnDisableWithoutRequired["password"]);

                            client.SendKeys(AddCompanyUserData.addCompanyUserBtnDisableWithoutRequired["client"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> optionss = driver.FindElements(By.TagName("mat-option"));
                            foreach (IWebElement option in optionss)
                            {
                                if (option.Text.Contains(AddCompanyUserData.addCompanyUserBtnDisableWithoutRequired["client"]))
                                {
                                    option.Click();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Client doesn't exist");
                                }
                            }

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
