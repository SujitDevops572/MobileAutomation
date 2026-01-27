using AventStack.ExtentReports;
using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
using VMS_Phase1PortalAT.utls.datas.W.Transactions.Purchase;

namespace VMS_Phase1PortalAT.Modules.W.Transactions.Purchase
{
    [TestClass] 
    public class OptionsPurchase
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
        public void ExpiryDatePurchase()
        {

            test = extent.CreateTest("Edit Expiry Date Success");

            expectedStatus = "Passed";
            description = "test case to test Edit Expiry Date Success";
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
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("(//mat-icon[text()='more_vert'])[1]")));
                        IWebElement Options = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                        Options.Click();
                       
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Update Expiry Date')]")));
                        IWebElement UpdateExpDate = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Update Expiry Date')]"));
                        UpdateExpDate.Click();
                        
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[text()='date_range']")));
                        IWebElement CalenderButton = driver.FindElement(By.XPath("(//mat-icon[text()='date_range'])[1]"));
                        CalenderButton.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='owl-dt-calendar-control']")));
                        IWebElement YearSelect = driver.FindElement(By.XPath("//div[@class='owl-dt-calendar-control']"));
                        YearSelect.Click();
                        Thread.Sleep(1000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("owl-date-time-multi-year-view")));
                        IWebElement Years = driver.FindElement(By.TagName("owl-date-time-multi-year-view"));
                        IList<IWebElement> YearOption = Years.FindElements(By.TagName("td"));
                        foreach (IWebElement option in YearOption)
                        {

                            if (option.Text.Trim().Equals(PurchaseAdd.UpdateExpiryDate["year"])) { option.Click(); break; }
                        }
                        Thread.Sleep(1000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("owl-date-time-year-view")));
                        IWebElement Months = driver.FindElement(By.TagName("owl-date-time-year-view"));
                        IList<IWebElement> MonthOption = Months.FindElements(By.TagName("td"));
                        foreach (IWebElement option in MonthOption)
                        {

                            if (option.Text.Trim().ToLower().Equals(PurchaseAdd.UpdateExpiryDate["month"])) { option.Click(); break; }
                        }
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("owl-date-time-month-view")));
                        IWebElement Dates = driver.FindElement(By.TagName("owl-date-time-month-view"));
                        IList<IWebElement> DateOption = Dates.FindElements(By.TagName("td"));
                        foreach (IWebElement option in DateOption)
                        {

                            if (option.Text.Trim().Equals(PurchaseAdd.UpdateExpiryDate["date"])) { option.Click(); break; }
                        }
                        IWebElement Save = driver.FindElement(By.XPath("//span[text()=' Save ']"));
                        Save.Click();
                        Thread.Sleep(1000);

                        wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                        IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                        Console.WriteLine(snackbar.Text);
                       Assert.IsTrue(snackbar.Text.Contains("Expiry Date Updated"), " failed..");

                    }
                }
            }
            catch (Exception ex){ 
                 
                Console.WriteLine(ex.StackTrace); 
            }
        }



        [TestMethod]
        public void DeletePurchase()
        {

            test = extent.CreateTest("Delete Purchase");

            expectedStatus = "Passed";
            description = "test case to test Delete Purchase Success";
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
                    submenu.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Options = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Options.Click();
                                      
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Delete')]")));
                    IWebElement DeletePurchase = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Delete')]"));
                    DeletePurchase.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]")));
                    IWebElement D_Confirm = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]"));
                    D_Confirm.Click();

                    wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    Console.WriteLine(snackbar.Text);
                    Assert.IsTrue(snackbar.Text.Contains("Error Deleting Warehouse Purchase"), " failed..");


                }
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
            }
                    
        }




        [TestMethod]
        public void InfoPurchase()
        {

            test = extent.CreateTest("Info of Purchase");

            expectedStatus = "Passed";
            description = "test case to test whether it displays Info of Purchase";
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
                    submenu.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("(//tbody/tr)[1]/td[last()]")));
                    IWebElement Options = driver.FindElement(By.XPath("(//tbody/tr)[1]/td[last()]"));
                    Options.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Info')]")));
                    IWebElement Info = driver.FindElement(By.XPath("//div[contains(@class,'mat-menu-content')]//button[contains(text(),'Info')]"));
                    Info.Click();
                    Thread.Sleep(1000);

                    IWebElement data = null;
                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Product Name')]/parent::div")));
                        data = driver.FindElement(By.XPath("//span[contains(text(),'Product Name')]/parent::div"));
                        Console.WriteLine(data.Text);
                    }
                    catch (Exception ex){ 
                        test.Fail(ex);  
                        Assert.Fail(ex.Message);    
                    }
                                       
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//mat-dialog-container//span[contains(text(),'Close')]")));
                    IWebElement close = driver.FindElement(By.XPath("//mat-dialog-container//span[contains(text(),'Close')]"));
                    close.Click();
                    Thread.Sleep(1000);

                    test.Pass();

                }
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
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
        
