using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Products.Brand;

namespace VMS_Phase1PortalAT.Modules.Products.Brand
{
    [TestClass]
    public class AddBrand
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
        public void AddBrandSuccess()
        {
            test = extent.CreateTest("Validating add brand - valid data");

            expectedStatus = "Passed";
            description = "Test add brand with valid data from AddBrandData";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            try
            {
                LoginSuccess.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail(errorMessage);
                throw;
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Navigate menus
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products"))).Click();
                Console.WriteLine("Clicked Products menu");

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products0"))).Click();
                Console.WriteLine("Clicked Brand submenu");

                // Wait for table to load
                IWebElement tableBody = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
                IList<IWebElement> initialRows = tableBody.FindElements(By.TagName("tr"));

                // Store previous row TEXT only (NO WebElement)
                string previousText = initialRows.Count > 0
                    ? initialRows[0].FindElements(By.TagName("td"))[0].Text.Trim()
                    : "";

                Console.WriteLine("Previous first row text: " + previousText);

                // Click Add Button
                IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                add.Click();
                Console.WriteLine("Clicked Add");

                // Fill form
                IWebElement name = wait.Until(d => d.FindElement(By.Name("name")));
                IWebElement branch = driver.FindElement(By.Name("branch"));

                name.SendKeys(AddBrandData.addBrandSuccess["name"]);
                Console.WriteLine("Entered Brand Name");

                branch.SendKeys(AddBrandData.addBrandSuccess["branch"]);
                Console.WriteLine("Entered Branch");

                // Handle mat-option safely
                IList<IWebElement> options = wait.Until(d =>
                {
                    var list = d.FindElements(By.TagName("mat-option"));
                    return list.Count > 0 ? list : null;
                });

                options[0].Click();
                Console.WriteLine("Selected Branch Option");

                // Submit
                IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-raised-button")));
                submit.Click();
                Console.WriteLine("Clicked Submit");

                // Give Angular time to refresh
                Thread.Sleep(1200);

                // Wait for refreshed table
                IWebElement updatedTable = wait.Until(driver =>
                {
                    var tbl = driver.FindElements(By.TagName("tbody")).FirstOrDefault();
                    if (tbl != null && tbl.Displayed)
                    {
                        var rows = tbl.FindElements(By.TagName("tr"));
                        if (rows.Count > 0)
                            return tbl;
                    }
                    return null;
                });

                Console.WriteLine("Table refreshed");


                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                Thread.Sleep(1000);
                select.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                searchOptions[0].Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                searchInput.Clear();
                searchInput.SendKeys(AddBrandData.addBrandSuccess["name"]);
                searchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);


                IList<IWebElement> rowsAfter = updatedTable.FindElements(By.TagName("tr"));
                string currentText = rowsAfter[0].FindElements(By.TagName("td"))[0].Text.Trim();

                Console.WriteLine("Current first row text: " + currentText);

                // Assert change
                Assert.AreNotEqual(previousText, currentText, "No new brand added to table.");

                test.Pass();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail(errorMessage);
                Console.WriteLine("Test failed: " + errorMessage);
                throw;
            }
        }













        //[TestMethod]
        //public void AddBrandSuccess()
        //{

        //    test = extent.CreateTest("Validating add brand - valid data");

        //    expectedStatus = "Passed";
        //    description = "test case to test add brand. make sure you have valid data in addBrandSuccess in AddBrandData";
        //    Login LoginSuccess = new Login();
        //    driver = LoginSuccess.getdriver();
        //    try
        //    {
        //        LoginSuccess.LoginSuccessCompanyAdmin();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException != null)
        //        {
        //            errorMessage = ex.InnerException.Message;
        //        }
        //    }
        //    try
        //    {
        //        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
        //        IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
        //        if (menu != null)
        //        {
        //            menu.Click();
        //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products0")));
        //            IWebElement submenu = driver.FindElement(By.Id("menuItem-Products0"));
        //            if (submenu != null)
        //            {
        //                submenu.Click();
        //                IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
        //                IList<IWebElement> data = table.FindElements(By.TagName("tr"));
        //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("td")));
        //                IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
        //                IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
        //                if (add != null)
        //                {
        //                    add.Click();
        //                    IWebElement name = driver.FindElement(By.Name("name"));
        //                    IWebElement branch = driver.FindElement(By.Name("branch"));
        //                    name.SendKeys(AddBrandData.addBrandSuccess["name"]);
        //                    branch.SendKeys(AddBrandData.addBrandSuccess["branch"]);
        //                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
        //                    IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
        //                    Thread.Sleep(1000);
        //                    if (options[0].Text.Contains(AddBrandData.addBrandSuccess["branch"]))
        //                    {
        //                        options[0].Click();
        //                    }
        //                    IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
        //                    submit.Click();
        //                    Thread.Sleep(2000);

        //                    IWebElement updatedTable = wait.Until(driver =>
        //                    {
        //                        var tbl = driver.FindElements(By.TagName("tbody")).FirstOrDefault();
        //                        if (tbl != null && tbl.Displayed)
        //                        {
        //                            var rows = tbl.FindElements(By.TagName("tr"));
        //                            if (rows.Count > 0)
        //                                return tbl;
        //                        }
        //                        return null;
        //                    });

        //                    Console.WriteLine("Table refreshed");

        //                    IList<IWebElement> rowsAfter = updatedTable.FindElements(By.TagName("tr"));
        //                    string currentText = rowsAfter[0].FindElements(By.TagName("td"))[0].Text.Trim();

        //                    Console.WriteLine("New first row text: " + currentText);

        //                    // Assert TEXT vs TEXT
        //                    Assert.AreNotEqual(prevoiusdata.Text, currentText, "No new data added to table.");

        //                    test.Pass();
        //                }
        //                else
        //                {
        //                    Console.WriteLine("doesnt find add null");

        //                    test.Skip();
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("doesnt find sub menu");

        //                test.Skip();
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("doesnt find menu");

        //            test.Skip();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorMessage = ex.Message;
        //        if (ex.InnerException != null)
        //        {
        //            errorMessage = ex.InnerException.Message;

        //            test.Fail(errorMessage);
        //        }
        //        throw;
        //    }
        //}

        [TestMethod]
        public void AddBrandFailure()
        {

            test = extent.CreateTest("Validating  add brand invalid data");

            expectedStatus = "Passed";
            description = "test case to test add brand. make sure you have invalid data in addBrandFailure in AddBrandData";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products0"));
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
                            name.SendKeys(AddBrandData.addBrandFailure["name"]);
                            branch.SendKeys(AddBrandData.addBrandFailure["branch"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
                            if (options[0].Text.Contains(AddBrandData.addBrandFailure["branch"]))
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

                    test.Fail(errorMessage);
                }
                throw;
            }
        }

        [TestMethod]
        public void AddBrandBtnDisableWithInvalidField()
        {

            test = extent.CreateTest("Validating add brand with invalid datas - addBrandBtnDisable");

            expectedStatus = "Passed";
            description = "test case to test add brand with invalid datas. add invalid datas in addBrandBtnDisable in AddBrandData file before run";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));
                            name.SendKeys(AddBrandData.addBrandBtnDisable["name"]);
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
        public void AddBrandBtnDisableWithoutRequired()
        {

            test = extent.CreateTest("Validating add brand without required data");

            expectedStatus = "Passed";
            description = "test case to test add brand without required data. add invalid datas in addBrandBtnDisableWithoutRequired in AddBrandData file before run";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            LoginSuccess.LoginSuccessCompanyAdmin();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
            IWebElement menu = driver.FindElement(By.Id("menuItem-Products"));
            if (menu != null)
            {
                menu.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products0")));
                IWebElement submenu = driver.FindElement(By.Id("menuItem-Products0"));
                if (submenu != null)
                {
                    submenu.Click();
                    IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("mat-fab")));
                    IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                    if (add != null)
                    {
                        add.Click();
                        IWebElement name = driver.FindElement(By.Name("name"));
                        name.SendKeys(AddBrandData.addBrandBtnDisableWithoutRequired["name"]);
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
