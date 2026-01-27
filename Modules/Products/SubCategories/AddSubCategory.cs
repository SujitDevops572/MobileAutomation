using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Products.SubCategories;

namespace VMS_Phase1PortalAT.Modules.Products.SubCategories
{
    [TestClass]
    public class AddSubCategory
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
        public void AddSubCategorySuccess()
        {
            test = extent.CreateTest("Validating Add SubCategory Success");

            expectedStatus = "Passed";
            description = "Test case to test add sub category. add valid data in AddSubCategoryData file";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            try
            {
                LoginSuccess.LoginSuccessCompanyAdmin();
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

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products"))).Click();
                Console.WriteLine("Clicked Products menu");

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products2"))).Click();
                Console.WriteLine("Clicked Subcategories submenu");

                // Wait for table
                IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
                IList<IWebElement> initialRows = table.FindElements(By.TagName("tr"));

                // Store previous first cell TEXT, not element
                string previousText = initialRows.Count > 0
                    ? initialRows[0].FindElements(By.TagName("td"))[0].Text.Trim()
                    : "";

                Console.WriteLine("Previous first row text: " + previousText);

                // Click Add button
                IWebElement addBtn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                addBtn.Click();
                Console.WriteLine("Clicked Add button");

                // Fill form
                IWebElement name = wait.Until(d => d.FindElement(By.Name("name")));
                IWebElement branch = driver.FindElement(By.Name("branch"));
                IWebElement brand = driver.FindElement(By.Name("brand"));
                IWebElement category = driver.FindElement(By.Name("category"));

                name.SendKeys(AddSubCategoryData.addSubCategorySuccess["name"]);
                Console.WriteLine("Entered SubCategory name");

                branch.SendKeys(AddSubCategoryData.addSubCategorySuccess["branch"]);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                driver.FindElements(By.TagName("mat-option"))[0].Click();
                Console.WriteLine("Selected branch");

                brand.SendKeys(AddSubCategoryData.addSubCategorySuccess["brand"]);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                driver.FindElements(By.TagName("mat-option"))[0].Click();
                Console.WriteLine("Selected brand");

                category.SendKeys(AddSubCategoryData.addSubCategorySuccess["category"]);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                driver.FindElements(By.TagName("mat-option"))[0].Click();
                Console.WriteLine("Selected category");

                IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                submit.Click();
                Console.WriteLine("Clicked Submit");

                // Allow DOM to refresh
                //Thread.Sleep(1500);

                Thread.Sleep(2000);
               
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                //bool isSuccess = false;
                //if (snackbar.Text.Contains(""))
                //{
                //    isSuccess = true;

                //}
                Console.WriteLine(snackbar.Text);
                //Assert.IsTrue(isSuccess, " failed..");





                // Wait for updated table
                //IWebElement updatedTable = wait.Until(driver =>
                //{
                //    var tbl = driver.FindElements(By.TagName("tbody")).FirstOrDefault();
                //    if (tbl != null && tbl.Displayed)
                //    {
                //        var rows = tbl.FindElements(By.TagName("tr"));
                //        if (rows.Count > 0)
                //            return tbl;
                //    }
                //    return null;
                //});

                //Console.WriteLine("Table refreshed");

                //IList<IWebElement> rowsAfter = updatedTable.FindElements(By.TagName("tr"));
                //string currentText = rowsAfter[0].FindElements(By.TagName("td"))[0].Text.Trim();

                //Console.WriteLine("New first row text: " + currentText);

                //// Assert TEXT vs TEXT
                //Assert.AreNotEqual(previousText, currentText, "No new data added to table.");

             

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
        //public void AddSubCategorySuccess()
        //{
        //    test = extent.CreateTest("Validating Add SubCategory Success");

        //    expectedStatus = "Passed";
        //    description = "test case to test add sub category. add valid data addSubCategorySuccess in AddSubCategoryData file";
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
        //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products2")));
        //            IWebElement submenu = driver.FindElement(By.Id("menuItem-Products2"));
        //            if (submenu != null)
        //            {
        //                submenu.Click();
        //                IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));

        //                IList<IWebElement> data = table.FindElements(By.TagName("tr"));
        //                IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
        //                IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
        //                if (add != null)
        //                {
        //                    add.Click();
        //                    IWebElement name = driver.FindElement(By.Name("name"));
        //                    IWebElement branch = driver.FindElement(By.Name("branch"));
        //                    name.SendKeys(AddSubCategoryData.addSubCategorySuccess["name"]);
        //                    branch.SendKeys(AddSubCategoryData.addSubCategorySuccess["branch"]);
        //                    Thread.Sleep(500);
        //                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
        //                    IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
        //                    options[0].Click();
        //                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("brand")));
        //                    IWebElement brand = driver.FindElement(By.Name("brand"));
        //                    brand.SendKeys(AddSubCategoryData.addSubCategorySuccess["brand"]);
        //                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
        //                    IList<IWebElement> options2 = driver.FindElements(By.TagName("mat-option"));
        //                    options2[0].Click();
        //                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("category")));
        //                    IWebElement category = driver.FindElement(By.Name("category"));
        //                    category.SendKeys(AddSubCategoryData.addSubCategorySuccess["category"]);
        //                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
        //                    IList<IWebElement> options3 = driver.FindElements(By.TagName("mat-option"));
        //                    options3[0].Click();
        //                    IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
        //                    submit.Click();
        //                    // Wait for page to finish updating after submit
        //                    Thread.Sleep(1000); // allow Angular to update DOM

        //                    // Wait until table body exists and is stable
        //                    IWebElement tableBody = wait.Until(driver =>
        //                    {
        //                        var tbody = driver.FindElements(By.TagName("tbody")).FirstOrDefault();
        //                        if (tbody != null && tbody.Displayed)
        //                        {
        //                            // Ensure table has at least one row
        //                            var rows = tbody.FindElements(By.TagName("tr"));
        //                            if (rows.Count > 0)
        //                            {
        //                                return tbody;
        //                            }
        //                        }
        //                        return null;
        //                    });

        //                    Console.WriteLine("Table loaded successfully");

        //                    // Get all rows
        //                    IList<IWebElement> rowsAfter = tableBody.FindElements(By.TagName("tr"));

        //                    // Extract first row first cell text
        //                    string currentFirstCellValue = rowsAfter[0].FindElements(By.TagName("td"))[0].Text.Trim();

        //                    Console.WriteLine("Previous first row: " + prevoiusdata);
        //                    Console.WriteLine("Current first row: " + currentFirstCellValue);

        //                    // Assert new data was added
        //                    Assert.AreNotEqual(prevoiusdata.Text, currentFirstCellValue, "No new data added to table.");


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
        public void AddSubCategoryFailure()
        {
            test = extent.CreateTest("Validating  Add SubCategory Failure");


            expectedStatus = "Passed";
            description = "test case to test add sub category. add valid data in addSubCategoryFailure in AddSubCategoryData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products2")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products2"));
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
                            name.SendKeys(AddSubCategoryData.addSubCategoryFailure["name"]);
                            branch.SendKeys(AddSubCategoryData.addSubCategoryFailure["branch"]);
                            Thread.Sleep(500);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
                            options[0].Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("brand")));
                            IWebElement brand = driver.FindElement(By.Name("brand"));
                            //brand.SendKeys(AddSubCategoryData.addSubCategoryFailure["brand"]);
                            brand.Click();  
                            Thread.Sleep(500);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options2 = driver.FindElements(By.TagName("mat-option"));
                            options2[0].Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("category")));
                            IWebElement category = driver.FindElement(By.Name("category"));
                            category.SendKeys(AddSubCategoryData.addSubCategoryFailure["category"]);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                            IList<IWebElement> options3 = driver.FindElements(By.TagName("mat-option"));
                            options3[0].Click();
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            submit.Click();
                            Thread.Sleep(1000);
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
        public void AddSubCategoryBtnDisableWithoutRequired()
        {
            test = extent.CreateTest("Validating  Add SubCategory BtnDisable");


            expectedStatus = "Passed";
            description = "test case to test add sub category. add valid data in addSubCategoryBtnDisableWithoutRequired in AddSubCategoryData file";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products2")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Products2"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("mat-fab")));
                        IWebElement add = driver.FindElement(By.ClassName("mat-fab"));
                        if (add != null)
                        {
                            add.Click();
                            IWebElement name = driver.FindElement(By.Name("name"));

                            name.SendKeys(AddSubCategoryData.addSubCategoryBtnDisableWithoutRequired["name"]);

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
