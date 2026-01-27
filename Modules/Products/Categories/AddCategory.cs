using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Products.Categories;

namespace VMS_Phase1PortalAT.Modules.Products.Categories
{
    [TestClass]
    public class AddCategory
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
        public void AddCategorySuccess()
        {

            test = extent.CreateTest("Validating add category Success");

            expectedStatus = "Passed";
            description = "test case to test add category. add valid data in addCategorySuccess in AddCategoryData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products1"));
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
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            name.SendKeys(AddCategoryData.addCategorySuccess["name"]);
                            branch.Click();
                            //branch.SendKeys(AddCategoryData.addCategorySuccess["branch"]);

                            //wait.Until(driver => driver.FindElements(By.TagName("mat-option")).Count > 0);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
                            options[0].Click();
                           
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("brand")));
                            IWebElement brand = driver.FindElement(By.Name("brand"));
                            brand.Click();
                            //brand.SendKeys(AddCategoryData.addCategorySuccess["brand"]);
                            
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options2 = driver.FindElements(By.TagName("mat-option"));
                            options2[0].Click();
                           
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
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

        [TestMethod]
        public void AddCategoryFailure()
        {
            test = extent.CreateTest("Validating add category Failure");

            expectedStatus = "Passed";
            description = "test case to test add category. add valid data in addCategoryFailure in AddCategoryData file";
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
                throw;
            }
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products1"));
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
                            IWebElement name = driver.FindElement(By.Name("name"));
                            IWebElement branch = driver.FindElement(By.Name("branch"));
                            name.SendKeys(AddCategoryData.addCategoryFailure["name"]);
                            branch.Click();
                            //branch.SendKeys(AddCategoryData.addCategoryFailure["branch"]);
                            Thread.Sleep(1500);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
                            options[0].Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("brand")));
                            IWebElement brand = driver.FindElement(By.Name("brand"));
                            brand.Click();  
                            // brand.SendKeys(AddCategoryData.addCategoryFailure["brand"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options2 = driver.FindElements(By.TagName("mat-option"));
                            options2[0].Click();
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            submit.Click();
                            Thread.Sleep(1000);
                            IWebElement table1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            wait.Until(drv => table1.FindElements(By.TagName("tr")).Count > 0);
                            IList<IWebElement> datas = table1.FindElements(By.TagName("tr"));
                            IWebElement currentdata = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
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

                    test.Skip();                }
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
        public void AddCategoryBtnDisableWithoutRequired()
        {
            test = extent.CreateTest("Validating Add Category BtnDisable");

            expectedStatus = "Passed";
            description = "test case to test add category. add valid data in addWCategoryBtnDisable in AddCategoryData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products1")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products1"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("mat-fab")));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            name.SendKeys(AddCategoryData.addWCategoryBtnDisable["name"]);
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
