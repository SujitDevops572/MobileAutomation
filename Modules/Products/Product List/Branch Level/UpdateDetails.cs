using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Products.ProductList;

namespace VMS_Phase1PortalAT.Modules.Products.Product_List.Branch_Level
{
    [TestClass]
    public class UpdateDetails
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
        public void UpdateDetailsSuccess()
        {

            test = extent.CreateTest("Validating  Update Details Success");

            expectedStatus = "Passed";
            description = "test case to test update details with valid datas. add valid data in UpdateDetailsSuccess in UpdateDetailsBranchLevel file before run";

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
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products3")));
                IWebElement submenu = driver.FindElement(By.Id("menuItem-Products3"));
                if (submenu != null)
                {
                    submenu.Click();
                    IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                    IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                    if (data.Count > 0)
                    {
                        IWebElement firstRow = data[0];
                        IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                        IWebElement lastColumn = columns[columns.Count - 1];
                        lastColumn.Click();

                        IWebElement matMenu = wait.Until(drv => driver.FindElement(By.ClassName("mat-menu-panel")));
                        IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));

                        foreach (var item in menuItems)
                        {
                            if (item.Text.Contains("Update Details"))
                            {

                                item.Click();
                                break;
                            }
                        }
                        wait.Until(drv => driver.FindElement(By.Name("type")));
                        IWebElement type = driver.FindElement(By.Name("type"));
                        IWebElement storage = driver.FindElement(By.Name("storage"));
                        
                        IWebElement usage = driver.FindElement(By.Name("usage"));
                        IWebElement additionalInfo = driver.FindElement(By.Name("additionalInfo"));

                        Thread.Sleep(1000);
                        type.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
                        options[0].Click();
                        storage.SendKeys(UpdateDetailsBranchLevel.UpdateDetailsSuccess["storage"]);
                        IWebElement ingredientsCheck = driver.FindElement(By.TagName("mat-checkbox"));
                        ingredientsCheck.Click();
                        Thread.Sleep(1000);
                        IWebElement ingredients = wait.Until(drv => driver.FindElement(By.Name("ingredients"))); 
                        ingredients.SendKeys(UpdateDetailsBranchLevel.UpdateDetailsSuccess["ingredients"]);
                        ingredients.SendKeys(Keys.Enter);
                      
                        usage.SendKeys(UpdateDetailsBranchLevel.UpdateDetailsSuccess["usage"]);
                        additionalInfo.SendKeys(UpdateDetailsBranchLevel.UpdateDetailsSuccess["additionalInfo"]);
                        
                        IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Save')]")));
                        Console.WriteLine(submit.Text);
                        submit.Click();
                        string title = "Product details updated";
                        IWebElement snackbar = wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                        bool isSuccess = false;
                        if (snackbar.Text.Contains(title))
                        {
                            isSuccess = true;

                        }
                        Assert.IsTrue(isSuccess, " failed..");

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
