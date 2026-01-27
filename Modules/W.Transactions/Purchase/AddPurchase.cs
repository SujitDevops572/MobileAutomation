using AventStack.ExtentReports;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.W.Transactions.Purchase;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.Purchase
{
    [TestClass]
    public class AddPurchase
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
        public void AddPurchaseSuccess()
        {

            test = extent.CreateTest("Add purchase");

            expectedStatus = "Passed";
            description = "test case to test add purchase";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    LoginSuccess.LoginSuccessCompanyAdmin();
                    return driver.FindElements(By.Id("menuItem-W. Transactions")).Any();
                });

                Console.WriteLine("Login successful and main menu is visible.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Login failed: " + errorMessage);
                throw;
            }
            try
            {
               
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-W. Transactions"));
                Actions a = new Actions(driver);

                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions0"));
                    if (submenu != null)
                    {
                        submenu.Click();                      
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='main_content']//mat-icon[text()='add']")));
                        IWebElement add = driver.FindElement(By.XPath("//div[@class='main_content']//mat-icon[text()='add']"));
                        add.Click();
                        Thread.Sleep(1000);
                        //IWebElement Scan = driver.FindElement(By.XPath("//span[text()=' Scan ']"));
                        //Scan.Click();
                        //IAlert alert = driver.SwitchTo().Alert();
                        //IWebElement Cancel = driver.FindElement(By.XPath("//span[text()=' Cancel ']"));
                        //if (alert != null  && Cancel!=null)
                        //{
                        //    driver.FindElement(By.XPath("//span[text()=' Cancel ']"));
                        //    Cancel.Click();
                        //}
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//input)[2]")));
                        /*addWarehouse*/ driver.FindElement(By.XPath("//input[@aria-label='Warehouse']")).Click();
                         /*addWarehouse Option*/driver.FindElement(By.XPath("//span[text()=' test2 ']")).Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@aria-label='Vendors']")));
                        IWebElement addvendor = driver.FindElement(By.XPath("//input[@aria-label='Vendors']"));
                        addvendor.SendKeys(PurchaseAdd.AddPurchase["vendor"]); a.Click(driver.FindElement(By.XPath("(//h2)[1]"))).Build().Perform();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@aria-label='Products']")));
                        IWebElement addProduct = driver.FindElement(By.XPath("//input[@aria-label='Products']"));
                        addProduct.SendKeys(PurchaseAdd.AddPurchase["product"]); a.Click(driver.FindElement(By.XPath("(//h2)[1]"))).Build().Perform();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@name='qty']")));
                        IWebElement addQuantity = driver.FindElement(By.XPath("//input[@name='qty']"));
                        addQuantity.SendKeys(PurchaseAdd.AddPurchase["qty"]);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@name='batchId']")));
                        IWebElement addBatchId = driver.FindElement(By.XPath("//input[@name='batchId']"));
                        addBatchId.SendKeys(PurchaseAdd.AddPurchase["batch id"]);
                        //Expiry Date
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text()='date_range']")));
                        IWebElement CalenderButton = driver.FindElement(By.XPath("(//mat-icon[text()='date_range'])[1]"));
                        CalenderButton.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='owl-dt-calendar-control']")));
                        IWebElement YearSelect = driver.FindElement(By.XPath("//div[@class='owl-dt-calendar-control']"));
                        YearSelect.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("owl-date-time-multi-year-view")));
                        IWebElement Years = driver.FindElement(By.TagName("owl-date-time-multi-year-view"));
                        IList<IWebElement> YearOption = Years.FindElements(By.TagName("td"));
                        foreach (IWebElement option in YearOption)
                        {

                            if (option.Text.Trim().Equals(PurchaseAdd.ExpiryDate["year"])) { option.Click(); break; }
                        }
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("owl-date-time-container")));
                        IWebElement Months = driver.FindElement(By.TagName("owl-date-time-container"));
                        IList<IWebElement> MonthOption = Months.FindElements(By.TagName("td"));
                        foreach (IWebElement option in MonthOption)
                        {

                            if (option.Text.Trim().ToLower().Equals(PurchaseAdd.ExpiryDate["month"])) { option.Click(); break; }
                        }
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("owl-date-time-month-view")));
                        IWebElement Dates = driver.FindElement(By.TagName("owl-date-time-month-view"));
                        IList<IWebElement> DateOption = Dates.FindElements(By.TagName("td"));
                        foreach (IWebElement option in DateOption)
                        {

                            if (option.Text.Trim().Equals(PurchaseAdd.ExpiryDate["date"])) { option.Click(); break; }
                        }
                        //Bill Date
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//mat-icon[text()='date_range'])[2]")));
                        IWebElement BillCalenderButton = driver.FindElement(By.XPath("(//mat-icon[text()='date_range'])[2]"));
                        BillCalenderButton.Click();
                        Thread.Sleep(2000);

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='owl-dt-calendar-control']")));
                        IWebElement BillYearSelect = driver.FindElement(By.XPath("//div[@class='owl-dt-calendar-control']"));
                        BillYearSelect.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("owl-date-time-multi-year-view")));
                        IWebElement B_Years = driver.FindElement(By.TagName("owl-date-time-multi-year-view"));
                        IList<IWebElement> B_YearOption = B_Years.FindElements(By.TagName("td"));
                        foreach (IWebElement option in B_YearOption)
                        {

                            if (option.Text.Trim().Equals(PurchaseAdd.BillDate["year"])) { option.Click(); break; }
                        }
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("owl-date-time-container")));
                        IWebElement B_Months = driver.FindElement(By.TagName("owl-date-time-container"));
                        IList<IWebElement> B_MonthOption = B_Months.FindElements(By.TagName("td"));
                        foreach (IWebElement option in B_MonthOption)
                        {

                            if (option.Text.Trim().ToLower().Equals(PurchaseAdd.BillDate["month"])) { option.Click(); break; }
                        }
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("owl-date-time-month-view")));
                        IWebElement B_Dates = driver.FindElement(By.TagName("owl-date-time-month-view"));
                        IList<IWebElement> B_DateOption = B_Dates.FindElements(By.TagName("td"));
                        foreach (IWebElement option in B_DateOption)
                        {

                            if (option.Text.Trim().Equals(PurchaseAdd.BillDate["date"])) { option.Click(); break; }
                        }

                        //Uploading-File
                        //IWebElement upload = driver.FindElement(By.XPath("//mat-icon[text()='cloud_upload']"));
                        //upload.Click();

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[text()=' Add ']")));
                        IWebElement AddButton = driver.FindElement(By.XPath("//span[text()=' Add ']"));
                        AddButton.Click();
                        IWebElement SaveButton = driver.FindElement(By.XPath("//span[text()=' Save ']"));
                        SaveButton.Click();
                        Thread.Sleep(2000); 
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//tbody//tr[1]//td[11]")));
                        string[] CreatedOn = driver.FindElement(By.XPath("//tbody//tr[1]//td[11]")).Text.Split();
                        Console.WriteLine(CreatedOn[1]);
                        string [] currentTime = DateTime.Now.ToString().Split();
                        Console.WriteLine("1st - "+currentTime[1]);
                        if (!currentTime[1].Equals(CreatedOn[1]))
                        {
                            string[] currentTime2 = DateTime.Now.AddSeconds(-1).ToString().Split();
                            if (currentTime2[1].Equals(CreatedOn[1]))
                                test.Pass();
                            Console.WriteLine("2nd - "+currentTime2[1]);
                        }
                        else { test.Pass(); }
                            

                        // IWebElement CancelButton = driver.FindElement(By.XPath("//span[text()=' Cancel ']"));
                        //IWebElement ExportButton = driver.FindElement(By.XPath("//button[@mattooltip='Export data']"));
                        //ExportButton.Click();

                    }
                }

            }
            catch (Exception ex)
            {
                string Error = ex.InnerException.Message;

                test.Fail(Error);
            }
        }



        [TestMethod]
        public void AddPurchaseFailure()
        {

            test = extent.CreateTest("Add Purchase Failure");

            expectedStatus = "Passed";
            description = "test case to test Add Purchase Failure and checking the button's Visibility";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Logging in...");

                // Ensure login completes and the menu is present
                wait.Until(driver =>
                {
                    LoginSuccess.LoginSuccessCompanyAdmin();
                    return driver.FindElements(By.Id("menuItem-W. Transactions")).Any();
                });

                Console.WriteLine("Login successful and main menu is visible.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail("Login failed: " + errorMessage);
                throw;
            }
            try
            {
              
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions")));
                IWebElement menu = driver.FindElement(By.Id("menuItem-W. Transactions"));
                Actions a = new Actions(driver);

                if (menu != null)
                {
                    menu.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-W. Transactions0")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-W. Transactions0"));
                    if (submenu != null)
                    {
                        submenu.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        Console.WriteLine(prevoiusdata);
                      
                        IWebElement addButton = wait.Until(driver =>
                        {
                            var element = driver.FindElement(By.XPath("//div[@class='main_content']//mat-icon[text()='add']"));
                            return (element != null && element.Displayed && element.Enabled) ? element : null;
                        });

                        addButton.Click();

                        Thread.Sleep(1000);
                        //IWebElement Scan = driver.FindElement(By.XPath("//span[text()=' Scan ']"));
                        //Scan.Click();
                        //IAlert alert = driver.SwitchTo().Alert();
                        //IWebElement Cancel = driver.FindElement(By.XPath("//span[text()=' Cancel ']"));
                        //if (alert != null  && Cancel!=null)
                        //{
                        //    driver.FindElement(By.XPath("//span[text()=' Cancel ']"));
                        //    Cancel.Click();
                        //}
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//input)[2]")));
                        /*addWarehouse*/
                        driver.FindElement(By.XPath("//input[@aria-label='Warehouse']")).Click();
                        /*addWarehouse Option*/
                        driver.FindElement(By.XPath("//span[text()=' test2 ']")).Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@aria-label='Vendors']")));
                        IWebElement addvendor = driver.FindElement(By.XPath("//input[@aria-label='Vendors']"));
                        addvendor.SendKeys(PurchaseAdd.AddPurchase["I_vendor"]); a.Click(driver.FindElement(By.XPath("(//h2)[1]"))).Build().Perform();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@aria-label='Products']")));
                        IWebElement addProduct = driver.FindElement(By.XPath("//input[@aria-label='Products']"));
                        addProduct.SendKeys(PurchaseAdd.AddPurchase["I_product"]); a.Click(driver.FindElement(By.XPath("(//h2)[1]"))).Build().Perform();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@name='qty']")));
                        IWebElement addQuantity = driver.FindElement(By.XPath("//input[@name='qty']"));
                        addQuantity.SendKeys(PurchaseAdd.AddPurchase["I_qty"]);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@name='batchId']")));
                        IWebElement addBatchId = driver.FindElement(By.XPath("//input[@name='batchId']"));
                        addBatchId.SendKeys(PurchaseAdd.AddPurchase["I_batch id"]);
                        //Expiry Date
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text()='date_range']")));
                        IWebElement CalenderButton = driver.FindElement(By.XPath("(//mat-icon[text()='date_range'])[1]"));
                        CalenderButton.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='owl-dt-calendar-control']")));
                        IWebElement YearSelect = driver.FindElement(By.XPath("//div[@class='owl-dt-calendar-control']"));
                        YearSelect.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("owl-date-time-multi-year-view")));
                        IWebElement Years = driver.FindElement(By.TagName("owl-date-time-multi-year-view"));
                        IList<IWebElement> YearOption = Years.FindElements(By.TagName("td"));
                        foreach (IWebElement option in YearOption)
                        {

                            if (option.Text.Trim().Equals(PurchaseAdd.ExpiryDate["year"])) { option.Click(); break; }
                        }
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("owl-date-time-container")));
                        IWebElement Months = driver.FindElement(By.TagName("owl-date-time-container"));
                        IList<IWebElement> MonthOption = Months.FindElements(By.TagName("td"));
                        foreach (IWebElement option in MonthOption)
                        {

                            if (option.Text.Trim().ToLower().Equals(PurchaseAdd.ExpiryDate["month"])) { option.Click(); break; }
                        }
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("owl-date-time-month-view")));
                        IWebElement Dates = driver.FindElement(By.TagName("owl-date-time-month-view"));
                        IList<IWebElement> DateOption = Dates.FindElements(By.TagName("td"));
                        foreach (IWebElement option in DateOption)
                        {

                            if (option.Text.Trim().Equals(PurchaseAdd.ExpiryDate["date"])) { option.Click(); break; }
                        }
                        //Bill Date
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//mat-icon[text()='date_range'])[2]")));
                        IWebElement BillCalenderButton = driver.FindElement(By.XPath("(//mat-icon[text()='date_range'])[2]"));
                        BillCalenderButton.Click();
                        Thread.Sleep(2000);

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='owl-dt-calendar-control']")));
                        IWebElement BillYearSelect = driver.FindElement(By.XPath("//div[@class='owl-dt-calendar-control']"));
                        BillYearSelect.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("owl-date-time-multi-year-view")));
                        IWebElement B_Years = driver.FindElement(By.TagName("owl-date-time-multi-year-view"));
                        IList<IWebElement> B_YearOption = B_Years.FindElements(By.TagName("td"));
                        foreach (IWebElement option in B_YearOption)
                        {

                            if (option.Text.Trim().Equals(PurchaseAdd.BillDate["year"])) { option.Click(); break; }
                        }
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("owl-date-time-container")));
                        IWebElement B_Months = driver.FindElement(By.TagName("owl-date-time-container"));
                        IList<IWebElement> B_MonthOption = B_Months.FindElements(By.TagName("td"));
                        foreach (IWebElement option in B_MonthOption)
                        {

                            if (option.Text.Trim().ToLower().Equals(PurchaseAdd.BillDate["month"])) { option.Click(); break; }
                        }
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("owl-date-time-month-view")));
                        IWebElement B_Dates = driver.FindElement(By.TagName("owl-date-time-month-view"));
                        IList<IWebElement> B_DateOption = B_Dates.FindElements(By.TagName("td"));
                        foreach (IWebElement option in B_DateOption)
                        {

                            if (option.Text.Trim().Equals(PurchaseAdd.BillDate["date"])) { option.Click(); break; }
                        }

                        //Uploading-File
                        //IWebElement upload = driver.FindElement(By.XPath("//mat-icon[text()='cloud_upload']"));
                        //upload.Click();

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[text()=' Add ']")));
                        IWebElement AddButton = driver.FindElement(By.XPath("//span[text()=' Add ']"));
                        bool addButtons = AddButton.Enabled;
                        IWebElement SaveButton = driver.FindElement(By.XPath("//span[text()=' Save ']"));
                        bool saveButton = SaveButton.Enabled;
                        Assert.IsTrue(addButtons && saveButton , "Button Disabled");
                        test.Pass();

                        // IWebElement CancelButton = driver.FindElement(By.XPath("//span[text()=' Cancel ']"));
                        //IWebElement ExportButton = driver.FindElement(By.XPath("//button[@mattooltip='Export data']"));
                        //ExportButton.Click();

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
 


[TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;
            System.String formatedTime = $"{timeTaken.TotalSeconds:F2}";
            driver.Quit();
            testResult.WriteTestResults(TestContext, formatedTime, expectedStatus, errorMessage, description);

            extent.Flush();
        }
    }
}
