using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Warehouse.WarehouseList;

namespace VMS_Phase1PortalAT.Modules.Warehouse.WarehouseList
{
    [TestClass]
    public class AddWarehouse
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
        public void AddWarehouseSuccess()
        {


            test = extent.CreateTest("Validating Add Warehouse Success");


            expectedStatus = "Passed";
            description = "test case to test add warehouse with valid datas. add valid data in addWarehouseSuccess in AddWarehouseData file before run";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("tr")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement lat = driver.FindElement(By.Name("lat"));
                            IWebElement lon = driver.FindElement(By.Name("lon"));
                            IWebElement city = driver.FindElement(By.Name("city"));
                            IWebElement state = driver.FindElement(By.Name("state"));
                            IWebElement phoneNo = driver.FindElement(By.Name("phoneNo"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            name.SendKeys(AddWarehouseData.addWarehouseSuccess["name"]);
                            lat.SendKeys(AddWarehouseData.addWarehouseSuccess["lat"]);
                            lon.SendKeys(AddWarehouseData.addWarehouseSuccess["lon"]);
                            city.SendKeys(AddWarehouseData.addWarehouseSuccess["city"]);
                            state.SendKeys(AddWarehouseData.addWarehouseSuccess["state"]);
                            phoneNo.SendKeys(AddWarehouseData.addWarehouseSuccess["phoneNo"]);
                            branch.SendKeys(AddWarehouseData.addWarehouseSuccess["branch"]);
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
        public void AddWarehouseFailure()
        {
            test = extent.CreateTest("Validating  Add Warehouse Failure");


            expectedStatus = "Passed";
            description = "test case to test add warehouse with duplicate datas. add duplicate data in addWarehouseFailure in AddBranchData file before run";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        Thread.Sleep(1000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement lat = driver.FindElement(By.Name("lat"));
                            IWebElement lon = driver.FindElement(By.Name("lon"));
                            IWebElement city = driver.FindElement(By.Name("city"));
                            IWebElement state = driver.FindElement(By.Name("state"));
                            IWebElement phoneNo = driver.FindElement(By.Name("phoneNo"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            name.SendKeys(AddWarehouseData.addWarehouseFailure["name"]);
                            lat.SendKeys(AddWarehouseData.addWarehouseFailure["lat"]);
                            lon.SendKeys(AddWarehouseData.addWarehouseFailure["lon"]);
                            city.SendKeys(AddWarehouseData.addWarehouseFailure["city"]);
                            state.SendKeys(AddWarehouseData.addWarehouseFailure["state"]);
                            phoneNo.SendKeys(AddWarehouseData.addWarehouseFailure["phoneNo"]);
                            branch.SendKeys(AddWarehouseData.addWarehouseFailure["branch"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
                            if (options[0].Text.Contains(AddWarehouseData.addWarehouseFailure["branch"]))
                            {
                                options[0].Click();
                            }

                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            submit.Click();
                            Thread.Sleep(2000);
                            IWebElement table1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
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

                    test.Fail();
                }
                throw;
            }
        }

        [TestMethod]
        public void AddWarehouseBtnDisableWithInvalidField()
        {
            test = extent.CreateTest("Validating Add Warehouse BtnDisable With Invalid Field");



            expectedStatus = "Passed";
            description = "test case to test add warehouse with invalid datas. add valid data in addBranchSuccess in AddBranchData file before run";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("td")));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement lat = driver.FindElement(By.Name("lat"));
                            IWebElement lon = driver.FindElement(By.Name("lon"));
                            IWebElement city = driver.FindElement(By.Name("city"));
                            IWebElement state = driver.FindElement(By.Name("state"));
                            IWebElement phoneNo = driver.FindElement(By.Name("phoneNo"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            name.SendKeys(AddWarehouseData.addWarehouseBtnDisable["name"]);
                            lat.SendKeys(AddWarehouseData.addWarehouseBtnDisable["lat"]);
                            lon.SendKeys(AddWarehouseData.addWarehouseBtnDisable["lon"]);
                            city.SendKeys(AddWarehouseData.addWarehouseBtnDisable["city"]);
                            state.SendKeys(AddWarehouseData.addWarehouseBtnDisable["state"]);
                            phoneNo.SendKeys(AddWarehouseData.addWarehouseBtnDisable["phoneNo"]);
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
        public void AddWarehouseBtnDisableWithoutRequired()
        {
            test = extent.CreateTest("Validating Add Warehouse BtnDisable Without Required");


            expectedStatus = "Passed";
            description = "test case to test add warehouse without required datas";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Warehouse0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Warehouse0"));
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
                            IWebElement lat = driver.FindElement(By.Name("lat"));
                            IWebElement lon = driver.FindElement(By.Name("lon"));
                            IWebElement city = driver.FindElement(By.Name("city"));
                            IWebElement state = driver.FindElement(By.Name("state"));
                            IWebElement phoneNo = driver.FindElement(By.Name("phoneNo"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            name.SendKeys(AddWarehouseData.addWarehouseBtnDisableWithoutRequired["name"]);
                            lat.SendKeys(AddWarehouseData.addWarehouseBtnDisableWithoutRequired["lat"]);
                            lon.SendKeys(AddWarehouseData.addWarehouseBtnDisableWithoutRequired["lon"]);
                            city.SendKeys(AddWarehouseData.addWarehouseBtnDisableWithoutRequired["city"]);
                            state.SendKeys(AddWarehouseData.addWarehouseBtnDisableWithoutRequired["state"]);
                            phoneNo.SendKeys(AddWarehouseData.addWarehouseBtnDisableWithoutRequired["phoneNo"]);
                            branch.SendKeys(AddWarehouseData.addWarehouseBtnDisableWithoutRequired["branch"]);
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
            testResult.WriteTestResults(TestContext, formatedTime, expectedStatus, errorMessage, description);
            
            extent.Flush();
        }
    }
}
