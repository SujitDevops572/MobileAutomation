using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Warehouse.Vendors;
using VMS_Phase1PortalAT.utls.datas.Warehouse.WarehouseList;

namespace VMS_Phase1PortalAT.Modules.Warehouse.Vendors
{
    [TestClass]
    public class AddVendor
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
        public void AddVendorSuccess()
        {
            test = extent.CreateTest("Validating Add Vendor Success ");

            expectedStatus = "Passed";
            description = "test case to test add vendor with valid datas. add valid data in addVendorSuccess in AddVendorData file before run";
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
                IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse1"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement lat = driver.FindElement(By.Name("email"));
                            IWebElement lon = driver.FindElement(By.Name("mobile"));
                            IWebElement city = driver.FindElement(By.Name("contactPersonName"));
                            IWebElement state = driver.FindElement(By.Name("contactPersonEmail"));
                            IWebElement phoneNo = driver.FindElement(By.Name("contactPersonMobile"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            name.SendKeys(AddVendorData.addVendorSuccess["name"]);
                            lat.SendKeys(AddVendorData.addVendorSuccess["email"]);
                            lon.SendKeys(AddVendorData.addVendorSuccess["mobile"]);
                            city.SendKeys(AddVendorData.addVendorSuccess["contactPersonName"]);
                            state.SendKeys(AddVendorData.addVendorSuccess["contactPersonEmail"]);
                            phoneNo.SendKeys(AddVendorData.addVendorSuccess["contactPersonMobile"]);
                            branch.SendKeys(AddVendorData.addVendorSuccess["branch"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
                            if (options[0].Text.Contains(AddWarehouseData.addWarehouseSuccess["branch"]))
                            {
                                options[0].Click();
                            }
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
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
        public void AddVendorFailure()
        {
            test = extent.CreateTest("Validating Add Vendor Failure");


            expectedStatus = "Passed";
            description = "test case to test add vendor with duplicate datas. add valid data in addVendorFailure in AddVendorData file before run";
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
                IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse1"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement lat = driver.FindElement(By.Name("email"));
                            IWebElement lon = driver.FindElement(By.Name("mobile"));
                            IWebElement city = driver.FindElement(By.Name("contactPersonName"));
                            IWebElement state = driver.FindElement(By.Name("contactPersonEmail"));
                            IWebElement phoneNo = driver.FindElement(By.Name("contactPersonMobile"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            name.SendKeys(AddVendorData.addVendorFailure["name"]);
                            lat.SendKeys(AddVendorData.addVendorFailure["email"]);
                            lon.SendKeys(AddVendorData.addVendorFailure["mobile"]);
                            city.SendKeys(AddVendorData.addVendorFailure["contactPersonName"]);
                            state.SendKeys(AddVendorData.addVendorFailure["contactPersonEmail"]);
                            phoneNo.SendKeys(AddVendorData.addVendorFailure["contactPersonMobile"]);
                            branch.SendKeys(AddVendorData.addVendorFailure["branch"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
                            if (options[0].Text.Contains(AddWarehouseData.addWarehouseSuccess["branch"]))
                            {
                                options[0].Click();
                            }
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            submit.Click();
                            Thread.Sleep(2000);
                            IWebElement table1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
                            IWebElement currentdata = driver.FindElement(By.TagName("td"));
                            Assert.AreEqual(currentdata, prevoiusdata, "no new data added");

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
        public void AddVendorBtnDisableWithInvalidField()
        {
            test = extent.CreateTest("Validating  Add Vendor BtnDisable With InvalidField");


            expectedStatus = "Passed";
            description = "test case to test add vendor with invalid datas. add valid data in addWVendorBtnDisable in AddVendorData file before run";
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
                IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse1"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement lat = driver.FindElement(By.Name("email"));
                            IWebElement lon = driver.FindElement(By.Name("mobile"));
                            IWebElement city = driver.FindElement(By.Name("contactPersonName"));
                            IWebElement state = driver.FindElement(By.Name("contactPersonEmail"));
                            IWebElement phoneNo = driver.FindElement(By.Name("contactPersonMobile"));
                            name.SendKeys(AddVendorData.addWVendorBtnDisable["name"]);
                            lat.SendKeys(AddVendorData.addWVendorBtnDisable["email"]);
                            lon.SendKeys(AddVendorData.addWVendorBtnDisable["mobile"]);
                            city.SendKeys(AddVendorData.addWVendorBtnDisable["contactPersonName"]);
                            state.SendKeys(AddVendorData.addWVendorBtnDisable["contactPersonEmail"]);
                            phoneNo.SendKeys(AddVendorData.addWVendorBtnDisable["contactPersonMobile"]);
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
        public void AddVendorBtnDisableWithoutRequired()
        {

            test = extent.CreateTest("Validating Add Vendor BtnDisable WithoutRequired");


            expectedStatus = "Passed";
            description = "test case to test add vendor without required datas";
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
                IWebElement menu = driver.FindElement(By.Id("menuItem-Warehouse"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse1"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("mat-fab")));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement lat = driver.FindElement(By.Name("email"));
                            IWebElement lon = driver.FindElement(By.Name("mobile"));
                            IWebElement city = driver.FindElement(By.Name("contactPersonName"));
                            IWebElement state = driver.FindElement(By.Name("contactPersonEmail"));
                            IWebElement phoneNo = driver.FindElement(By.Name("contactPersonMobile"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            name.SendKeys(AddVendorData.addVendorBtnDisableWithoutRequired["name"]);
                            lat.SendKeys(AddVendorData.addVendorBtnDisableWithoutRequired["email"]);
                            lon.SendKeys(AddVendorData.addVendorBtnDisableWithoutRequired["mobile"]);
                            city.SendKeys(AddVendorData.addVendorBtnDisableWithoutRequired["contactPersonName"]);
                            state.SendKeys(AddVendorData.addVendorBtnDisableWithoutRequired["contactPersonEmail"]);
                            phoneNo.SendKeys(AddVendorData.addVendorBtnDisableWithoutRequired["contactPersonMobile"]);
                            branch.SendKeys(AddVendorData.addVendorBtnDisableWithoutRequired["branch"]);
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


        [TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;
            String formatedTime = $"{timeTaken.TotalSeconds:F2}";
            driver.Quit();

            extent.Flush();
        }
    }
}
