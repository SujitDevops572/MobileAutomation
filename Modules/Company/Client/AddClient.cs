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
    public class AddClient
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
        public void AddClientSuccess()
        {

            test = extent.CreateTest("test case to test add client. add valid data in addClientSuccess in AddClientData file");

            expectedStatus = "Passed";
            description = "test case to test add client. add valid data in addClientSuccess in AddClientData file";
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
                        Thread.Sleep(3000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                        IWebElement add = driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            IWebElement contactno = driver.FindElement(By.Name("contactno"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement address = driver.FindElement(By.Name("address"));
                            IWebElement gstNo = driver.FindElement(By.Name("gstNo"));
                            IWebElement companyName = driver.FindElement(By.Name("companyName"));
                            IWebElement companyContactNo = driver.FindElement(By.Name("companyContactNo"));
                            IWebElement companyEmail = driver.FindElement(By.Name("companyEmail"));
                            IWebElement companyAddress = driver.FindElement(By.Name("companyAddress"));
                            IWebElement conversionRateFor1Point = driver.FindElement(By.Name("conversionRateFor1Point"));
                            IWebElement lpUsageRestrictionPercentage = driver.FindElement(By.Name("lpUsageRestrictionPercentage"));
                            IWebElement maturityTimeInDays = driver.FindElement(By.Name("maturityTimeInDays"));
                            IWebElement registrationPoints = driver.FindElement(By.Name("registrationPoints"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));

                            name.SendKeys(AddClientData.addClientSuccess["name"]);
                            branch.SendKeys(AddClientData.addClientSuccess["branch"]);
                            contactno.SendKeys(AddClientData.addClientSuccess["contactno"]);
                            email.SendKeys(AddClientData.addClientSuccess["email"]);
                            address.SendKeys(AddClientData.addClientSuccess["address"]);
                            gstNo.SendKeys(AddClientData.addClientSuccess["gstNo"]);
                            companyName.SendKeys(AddClientData.addClientSuccess["companyName"]);
                            companyContactNo.SendKeys(AddClientData.addClientSuccess["companyContactNo"]);
                            companyEmail.SendKeys(AddClientData.addClientSuccess["companyEmail"]);
                            companyAddress.SendKeys(AddClientData.addClientSuccess["companyAddress"]);
                            conversionRateFor1Point.SendKeys(AddClientData.addClientSuccess["conversionRateFor1Point"]);
                            lpUsageRestrictionPercentage.SendKeys(AddClientData.addClientSuccess["lpUsageRestrictionPercentage"]);
                            maturityTimeInDays.SendKeys(AddClientData.addClientSuccess["maturityTimeInDays"]);
                            registrationPoints.SendKeys(AddClientData.addClientSuccess["registrationPoints"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> optionss = driver.FindElements(By.TagName("mat-option"));
                            foreach (IWebElement option in optionss)
                            {
                                if (option.Text.Contains(AddClientData.addClientSuccess["branch"]))
                                {
                                    option.Click();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("branch doesn't exist");
                                }
                            }
                            submit.Click();
                            Thread.Sleep(1000);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            IWebElement table1 = driver.FindElement(By.TagName("tbody"));
                            IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
                            IWebElement currentdata = driver.FindElement(By.TagName("td"));
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
        public void AddClientFailure()
        {
            test = extent.CreateTest("test case to test add client. add valid data in addClientFailure in AddClientData file");


            expectedStatus = "Passed";
            description = "test case to test add client. add valid data in addClientFailure in AddClientData file";
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
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                        IWebElement add = driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            IWebElement contactno = driver.FindElement(By.Name("contactno"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement address = driver.FindElement(By.Name("address"));
                            IWebElement gstNo = driver.FindElement(By.Name("gstNo"));
                            IWebElement companyName = driver.FindElement(By.Name("companyName"));
                            IWebElement companyContactNo = driver.FindElement(By.Name("companyContactNo"));
                            IWebElement companyEmail = driver.FindElement(By.Name("companyEmail"));
                            IWebElement companyAddress = driver.FindElement(By.Name("companyAddress"));
                            IWebElement conversionRateFor1Point = driver.FindElement(By.Name("conversionRateFor1Point"));
                            IWebElement lpUsageRestrictionPercentage = driver.FindElement(By.Name("lpUsageRestrictionPercentage"));
                            IWebElement maturityTimeInDays = driver.FindElement(By.Name("maturityTimeInDays"));
                            IWebElement registrationPoints = driver.FindElement(By.Name("registrationPoints"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.SendKeys(AddClientData.addClientFailure["name"]);
                            branch.SendKeys(AddClientData.addClientFailure["branch"]);
                            contactno.SendKeys(AddClientData.addClientFailure["contactno"]);
                            email.SendKeys(AddClientData.addClientFailure["email"]);
                            address.SendKeys(AddClientData.addClientFailure["address"]);
                            gstNo.SendKeys(AddClientData.addClientFailure["gstNo"]);
                            companyName.SendKeys(AddClientData.addClientFailure["companyName"]);
                            companyContactNo.SendKeys(AddClientData.addClientFailure["companyContactNo"]);
                            companyEmail.SendKeys(AddClientData.addClientFailure["companyEmail"]);
                            companyAddress.SendKeys(AddClientData.addClientFailure["companyAddress"]);
                            conversionRateFor1Point.SendKeys(AddClientData.addClientFailure["conversionRateFor1Point"]);
                            lpUsageRestrictionPercentage.SendKeys(AddClientData.addClientFailure["lpUsageRestrictionPercentage"]);
                            maturityTimeInDays.SendKeys(AddClientData.addClientFailure["maturityTimeInDays"]);
                            registrationPoints.SendKeys(AddClientData.addClientFailure["registrationPoints"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> optionss = driver.FindElements(By.TagName("mat-option"));
                            foreach (IWebElement option in optionss)
                            {
                                if (option.Text.Contains(AddClientData.addClientFailure["branch"]))
                                {
                                    option.Click();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("branch doesn't exist");
                                }
                                submit.Click();
                                Thread.Sleep(1000);
                                IWebElement table1 = driver.FindElement(By.TagName("tbody"));
                                IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
                                IWebElement currentdata = driver.FindElement(By.TagName("td"));
                                Assert.AreEqual(currentdata, prevoiusdata, "new data added");

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
        public void AddClientInvalidMobileNumber()
        {
            test = extent.CreateTest("test case to test add client. add valid data in addClientInvalidMobileNumber in AddClientData file");

            expectedStatus = "Passed";
            description = "test case to test add client. add valid data in addClientInvalidMobileNumber in AddClientData file";
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
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                        IWebElement add = driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            IWebElement contactno = driver.FindElement(By.Name("contactno"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement address = driver.FindElement(By.Name("address"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.SendKeys(AddClientData.addClientInvalidMobileNumber["name"]);
                            branch.SendKeys(AddClientData.addClientInvalidMobileNumber["branch"]);
                            contactno.SendKeys(AddClientData.addClientInvalidMobileNumber["contactno"]);
                            email.SendKeys(AddClientData.addClientInvalidMobileNumber["email"]);
                            address.SendKeys(AddClientData.addClientInvalidMobileNumber["address"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> optionss = driver.FindElements(By.TagName("mat-option"));
                            foreach (IWebElement option in optionss)
                            {
                                if (option.Text.Contains(AddClientData.addClientInvalidMobileNumber["branch"]))
                                {
                                    option.Click();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("branch doesn't exist");
                                }
                            }
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
        public void AddClientInvalidEmail()
        {
            test = extent.CreateTest("test case to test add client. add valid data in addClientInvalidEmail in AddCategoryData file");

            expectedStatus = "Passed";
            description = "test case to test add client. add valid data in addClientInvalidEmail in AddCategoryData file";
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
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                        IWebElement add = driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            IWebElement contactno = driver.FindElement(By.Name("contactno"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement address = driver.FindElement(By.Name("address"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.SendKeys(AddClientData.addClientInvalidEmail["name"]);
                            branch.SendKeys(AddClientData.addClientInvalidEmail["branch"]);
                            contactno.SendKeys(AddClientData.addClientInvalidEmail["contactno"]);
                            email.SendKeys(AddClientData.addClientInvalidEmail["email"]);
                            address.SendKeys(AddClientData.addClientInvalidEmail["address"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> optionss = driver.FindElements(By.TagName("mat-option"));
                            Console.WriteLine(optionss.Count + "options count");
                            foreach (IWebElement option in optionss)
                            {
                                if (option.Text.Contains(AddClientData.addClientInvalidEmail["branch"]))
                                {
                                    option.Click();
                                    break;

                                    test.Skip();
                                }
                                else
                                {
                                    Console.WriteLine("branch doesn't exist");

                                    test.Skip();
                                }
                            }
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
        public void AddClientBtnDisableWithoutRequiredFields()
        {
            test = extent.CreateTest("test case to test add client. add valid data in addClientWithoutRequiredFields in AddClientData file");


            expectedStatus = "Passed";
            description = "test case to test add client. add valid data in addClientWithoutRequiredFields in AddClientData file";

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
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                        IWebElement add = driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            IWebElement contactno = driver.FindElement(By.Name("contactno"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement address = driver.FindElement(By.Name("address"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.SendKeys(AddClientData.addClientWithoutRequiredFields["name"]);
                            branch.SendKeys(AddClientData.addClientWithoutRequiredFields["branch"]);
                            contactno.SendKeys(AddClientData.addClientWithoutRequiredFields["contactno"]);
                            email.SendKeys(AddClientData.addClientWithoutRequiredFields["email"]);
                            address.SendKeys(AddClientData.addClientWithoutRequiredFields["address"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> optionss = driver.FindElements(By.TagName("mat-option"));
                            foreach (IWebElement option in optionss)
                            {
                                if (option.Text.Contains(AddClientData.addClientWithoutRequiredFields["branch"]))
                                {
                                    option.Click();
                                    break;

                                    
                                }
                                else
                                {
                                    Console.WriteLine("branch doesn't exist");

                                }
                            }
                            Assert.IsTrue(submit.GetAttribute("disabled").Equals("true"));
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
        public void AddClientInvalidGstno()
        {
           
            test = extent.CreateTest("test case to test add client. add valid data in addClientInvalidGstno in AddClientData file");

            expectedStatus = "Passed";
            description = "test case to test add client. add valid data in addClientInvalidGstno in AddClientData file";
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
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                        IWebElement add = driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            IWebElement contactno = driver.FindElement(By.Name("contactno"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement address = driver.FindElement(By.Name("address"));
                            IWebElement gstNo = driver.FindElement(By.Name("gstNo"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.SendKeys(AddClientData.addClientInvalidGstno["name"]);
                            branch.SendKeys(AddClientData.addClientInvalidGstno["branch"]);
                            contactno.SendKeys(AddClientData.addClientInvalidGstno["contactno"]);
                            email.SendKeys(AddClientData.addClientInvalidGstno["email"]);
                            address.SendKeys(AddClientData.addClientInvalidGstno["address"]);
                            gstNo.SendKeys(AddClientData.addClientInvalidGstno["gstNo"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> optionss = driver.FindElements(By.TagName("mat-option"));
                            foreach (IWebElement option in optionss)
                            {
                                if (option.Text.Contains(AddClientData.addClientInvalidGstno["branch"]))
                                {
                                    option.Click();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("branch doesn't exist");
                                }
                            }
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
