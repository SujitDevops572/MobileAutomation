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

namespace VMS_Phase1PortalAT.Modules.Company.Branch
{
    [TestClass]
    public class AddBranch
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
        public void AddBranchSuccess()
        {
            test = extent.CreateTest("add valid data in addBranchSuccess in AddBranchData file before run");
            

            expectedStatus = "Passed";
            description = "add valid data in addBranchSuccess in AddBranchData file before run";
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
                        Thread.Sleep(1000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));

                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                        IWebElement add = driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]"));
                        if (add != null)
                        {
                            add.Click();

                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement location = driver.FindElement(By.Name("location"));
                            IWebElement code = driver.FindElement(By.Name("code"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement address = driver.FindElement(By.Name("address"));
                            IWebElement contactDetails = driver.FindElement(By.Name("contactDetails"));
                            IWebElement gstNo = driver.FindElement(By.Name("gstNo"));
                            IWebElement companyName = driver.FindElement(By.Name("companyName"));
                            IWebElement companyContactNo = driver.FindElement(By.Name("companyContactNo"));
                            IWebElement companyEmail = driver.FindElement(By.Name("companyEmail"));
                            IWebElement companyAddress = driver.FindElement(By.Name("companyAddress"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.SendKeys(AddBranchData.addBranchSuccess["name"]); 
                            location.SendKeys(AddBranchData.addBranchSuccess["location"]);
                            code.SendKeys(AddBranchData.addBranchSuccess["code"]);
                            email.SendKeys(AddBranchData.addBranchSuccess["email"]);
                            contactDetails.SendKeys(AddBranchData.addBranchSuccess["contactDetails"]);
                            address.SendKeys(AddBranchData.addBranchSuccess["address"]);
                            gstNo.SendKeys(AddBranchData.addBranchSuccess["gstNo"]);
                            companyName.SendKeys(AddBranchData.addBranchSuccess["companyName"]);
                            companyContactNo.SendKeys(AddBranchData.addBranchSuccess["companyContactNo"]);
                            companyEmail.SendKeys(AddBranchData.addBranchSuccess["companyEmail"]);
                            companyAddress.SendKeys(AddBranchData.addBranchSuccess["companyAddress"]);
                            Thread.Sleep(1500);
                            submit.Click();
                            Thread.Sleep(1000);
                            IWebElement table1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
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

        public void Fill(IWebDriver driver, Dictionary<String,String> d) {

            IWebElement name = driver.FindElement(By.Name("name"));
            IWebElement location = driver.FindElement(By.Name("location"));
            IWebElement code = driver.FindElement(By.Name("code"));
            IWebElement email = driver.FindElement(By.Name("email"));
            IWebElement address = driver.FindElement(By.Name("address"));
            IWebElement contactDetails = driver.FindElement(By.Name("contactDetails"));
            IWebElement gstNo = driver.FindElement(By.Name("gstNo"));
            IWebElement companyName = driver.FindElement(By.Name("companyName"));
            IWebElement companyContactNo = driver.FindElement(By.Name("companyContactNo"));
            IWebElement companyEmail = driver.FindElement(By.Name("companyEmail"));
            IWebElement companyAddress = driver.FindElement(By.Name("companyAddress"));
            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
            name.SendKeys(AddBranchData.addBranchSuccess["name"]);
            location.SendKeys(AddBranchData.addBranchSuccess["location"]);
            code.SendKeys(AddBranchData.addBranchSuccess["code"]);
            email.SendKeys(AddBranchData.addBranchSuccess["email"]);
            contactDetails.SendKeys(AddBranchData.addBranchSuccess["contactDetails"]);
            address.SendKeys(AddBranchData.addBranchSuccess["address"]);
            gstNo.SendKeys(AddBranchData.addBranchSuccess["gstNo"]);
            companyName.SendKeys(AddBranchData.addBranchSuccess["companyName"]);
            companyContactNo.SendKeys(AddBranchData.addBranchSuccess["companyContactNo"]);
            companyEmail.SendKeys(AddBranchData.addBranchSuccess["companyEmail"]);
            companyAddress.SendKeys(AddBranchData.addBranchSuccess["companyAddress"]);
            submit.Click();

        }

        [TestMethod]
        public void AddBranchFailure()
        {
            test = extent.CreateTest("add invalid data in addBranchFailure in AddBranchData file before run");
            

            expectedStatus = "Passed";
            description = "add invalid data in addBranchFailure in AddBranchData file before run";
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
                        Thread.Sleep(1000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                        IWebElement add = driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement location = driver.FindElement(By.Name("location"));
                            IWebElement code = driver.FindElement(By.Name("code"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement address = driver.FindElement(By.Name("address"));
                            IWebElement contactDetails = driver.FindElement(By.Name("contactDetails"));
                            IWebElement gstNo = driver.FindElement(By.Name("gstNo"));
                            IWebElement companyName = driver.FindElement(By.Name("companyName"));
                            IWebElement companyContactNo = driver.FindElement(By.Name("companyContactNo"));
                            IWebElement companyEmail = driver.FindElement(By.Name("companyEmail"));
                            IWebElement companyAddress = driver.FindElement(By.Name("companyAddress"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.SendKeys(AddBranchData.addBranchFailure["name"]);
                            location.SendKeys(AddBranchData.addBranchFailure["location"]);
                            code.SendKeys(AddBranchData.addBranchFailure["code"]);
                            email.SendKeys(AddBranchData.addBranchFailure["email"]);
                            contactDetails.SendKeys(AddBranchData.addBranchFailure["contactDetails"]);
                            address.SendKeys(AddBranchData.addBranchFailure["address"]);
                            gstNo.SendKeys(AddBranchData.addBranchFailure["gstNo"]);
                            companyName.SendKeys(AddBranchData.addBranchFailure["companyName"]);
                            companyContactNo.SendKeys(AddBranchData.addBranchFailure["companyContactNo"]);
                            companyEmail.SendKeys(AddBranchData.addBranchFailure["companyEmail"]);
                            companyAddress.SendKeys(AddBranchData.addBranchFailure["companyAddress"]);
                            submit.Click();
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
        public void AddBranchInvalidMobileNumber()
        {
            test = extent.CreateTest("Add Branch Invalid Mobile Number");

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
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                        IWebElement add = driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement location = driver.FindElement(By.Name("location"));
                            IWebElement code = driver.FindElement(By.Name("code"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement address = driver.FindElement(By.Name("address"));
                            IWebElement contactDetails = driver.FindElement(By.Name("contactDetails"));
                            IWebElement gstNo = driver.FindElement(By.Name("gstNo"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.SendKeys(AddBranchData.addBranchInvalidMobileNumber["name"]);
                            location.SendKeys(AddBranchData.addBranchInvalidMobileNumber["location"]);
                            code.SendKeys(AddBranchData.addBranchInvalidMobileNumber["code"]);
                            email.SendKeys(AddBranchData.addBranchInvalidMobileNumber["email"]);
                            address.SendKeys(AddBranchData.addBranchInvalidMobileNumber["address"]);
                            contactDetails.SendKeys(AddBranchData.addBranchInvalidMobileNumber["contactDetails"]);
                            gstNo.SendKeys(AddBranchData.addBranchInvalidMobileNumber["gstNo"]);
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
        public void AddBranchInvalidEmail()
        {
            test = extent.CreateTest("Add Branch Invalid Email");

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
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(drv => driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]")));
                        IWebElement add = driver.FindElement(By.XPath("//mat-icon[contains(text(),'add')]"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement location = driver.FindElement(By.Name("location"));
                            IWebElement code = driver.FindElement(By.Name("code"));
                            IWebElement email = driver.FindElement(By.Name("email"));
                            IWebElement address = driver.FindElement(By.Name("address"));
                            IWebElement contactDetails = driver.FindElement(By.Name("contactDetails"));
                            IWebElement gstNo = driver.FindElement(By.Name("gstNo"));
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            name.SendKeys(AddBranchData.addBranchInvalidEmail["name"]);
                            location.SendKeys(AddBranchData.addBranchInvalidEmail["location"]);
                            code.SendKeys(AddBranchData.addBranchInvalidEmail["code"]);
                            email.SendKeys(AddBranchData.addBranchInvalidEmail["email"]);
                            address.SendKeys(AddBranchData.addBranchInvalidEmail["address"]);
                            contactDetails.SendKeys(AddBranchData.addBranchInvalidEmail["contactDetails"]);
                            gstNo.SendKeys(AddBranchData.addBranchInvalidEmail["gstNo"]);
                            Thread.Sleep(1000);
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
