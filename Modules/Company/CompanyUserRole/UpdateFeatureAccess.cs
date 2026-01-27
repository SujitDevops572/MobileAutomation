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
using VMS_Phase1PortalAT.utls.datas.Company.CompanyUserRole;

namespace VMS_Phase1PortalAT.Modules.Company.CompanyUserRole
{
    [TestClass]
    public class UpdateFeatureAccess
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
        public void UpdateFeatureAccessSuccess()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("Update feature access ");
            description = "test case to test update feature access";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company4")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company4"));
                    if (submenu != null)
                    {
                        submenu.Click();
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
                        searchInput.SendKeys(UpdateFeatureAccessData.UpdateFeatureAccessSuccess["Role"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> datas = table.FindElements(By.TagName("tr"));

                        if (datas.Count > 0)
                        {
                            IWebElement firstRow = datas[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            IWebElement lastColumn = columns[3];
                            lastColumn.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                            IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));


                            foreach (var item in menuItems)
                            {
                                if (item.Text.Contains("Feature Access Info"))
                                {
                                    item.Click();
                                    Thread.Sleep(4000);


                                    break;
                                }
                            }
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-checkbox")));
                            IWebElement checkbox = driver.FindElement(By.TagName("mat-checkbox"));
                            checkbox.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-dialog-container")));
                            IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                            IList<IWebElement> dialogBtn = dialogElement.FindElements(By.TagName("button"));
                            foreach (var item in dialogBtn)
                            {
                                if (item.Text.Contains("Save"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            string title = "Feature Access Info Updated";
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            Console.WriteLine($"Snackbar: {snackbar.Text}");
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Assert.IsTrue(isSuccess, "Feature Access info updated failed..");
                            test.Pass();

                            Thread.Sleep(2000);
                            IList<IWebElement> columns1 = driver.FindElements(By.TagName("td"));
                            IWebElement lastColumn1 = columns1[3];
                            lastColumn1.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                            IWebElement matMenu1 = driver.FindElement(By.ClassName("mat-menu-panel"));
                            IList<IWebElement> menuItems1= matMenu1.FindElements(By.TagName("button"));


                            foreach (var item in menuItems1)
                            {
                                if (item.Text.Contains("Feature Access Info"))
                                {
                                    item.Click();
                                    Thread.Sleep(3000);
                                    break;
                                }
                            }
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-checkbox")));
                            IWebElement checkbox1 = driver.FindElement(By.XPath("(//input[@type='checkbox'])[1]"));
                            Console.WriteLine(checkbox1.GetAttribute("aria-checked"));
                            if (checkbox1.GetAttribute("aria-checked") == "true")
                            {
                                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkbox1);

                                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-dialog-container")));
                                IWebElement dialogElement1 = driver.FindElement(By.TagName("mat-dialog-container"));
                                IList<IWebElement> dialogBtn1 = dialogElement1.FindElements(By.TagName("button"));
                                foreach (var item in dialogBtn1)
                                {
                                    if (item.Text.Contains("Save"))
                                    {
                                        item.Click();
                                        Console.WriteLine("Reverted the changes");
                                        break;
                                    }
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("no data");
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
        public void UpdateFeatureAccessBtnDisable()
        {
            expectedStatus = "Passed";
            test = extent.CreateTest("feature access");
            description = "test case to test update feature access button disable using invalid data";
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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Company4")));
                    IWebElement submenu = driver.FindElement(By.Id("menuItem-Company4"));
                    if (submenu != null)
                    {
                        submenu.Click();
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
                        searchInput.SendKeys(UpdateFeatureAccessData.UpdateFeatureAccessBtnDisable["Role"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> datas = table.FindElements(By.TagName("tr"));

                        if (datas.Count > 0)
                        {
                            IWebElement firstRow = datas[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            Console.WriteLine(columns.Count + "columsss   count...");
                            IWebElement lastColumn = columns[3];
                            lastColumn.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                            IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));


                            foreach (var item in menuItems)
                            {
                                if (item.Text.Contains("Feature Access Info"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-dialog-container")));
                            IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                            IList<IWebElement> dialogBtn = dialogElement.FindElements(By.TagName("button"));
                            foreach (var item in dialogBtn)
                            {
                                if (item.Text.Contains("Save"))
                                {
                                    Assert.IsTrue(item.GetAttribute("disabled").Equals("true"));
                                    test.Pass();
                                }
                            }


                        }
                        else
                        {
                            Console.WriteLine("no data");
                            test.Skip();
                        }
                    }
                    else
                    {
                        Console.WriteLine("doesnt find submenu");
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
