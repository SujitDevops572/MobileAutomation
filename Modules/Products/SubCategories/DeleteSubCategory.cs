using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Products.SubCategories;

namespace VMS_Phase1PortalAT.Modules.Products.SubCategories
{
    [TestClass]
    public class DeleteSubCategory
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
        public void DeleteSubCategorySuccess()
        {
            test = extent.CreateTest("Validating Delete SubCategory Success");
            expectedStatus = "Passed";

            Login login = new Login();
            driver = login.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // ---------------- SAFETY HELPERS ----------------
            IWebElement SafeFind(By locator)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                return wait.Until(driver =>
                {
                    try
                    {
                        // Fetch element fresh each time
                        var element = driver.FindElement(locator);

                        // Return only if visible and enabled
                        if (element.Displayed && element.Enabled)
                        {
                            return element; 
                        }

                        // If not ready, throw exception to retry
                        throw new Exception("Element not interactable yet");
                    }
                    catch (StaleElementReferenceException)
                    {
                        throw; // will be caught by WebDriverWait and retried
                    }
                    catch (NoSuchElementException)
                    {
                        throw; // element not yet in DOM → retry
                    }
                    catch (ElementClickInterceptedException)
                    {
                        throw; // overlay blocks → retry
                    }
                });
            }





            IList<IWebElement>? SafeFindAll(By locator)
            {
                return wait.Until(driver =>
                {
                    try
                    {
                        var list = driver.FindElements(locator)
                                         .Where(x => x.Displayed && x.Enabled)
                                         .ToList();

                        return list.Count > 0 ? list : null;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return null; // Retry
                    }
                    catch (ElementClickInterceptedException)
                    {
                        return null; // Retry
                    }
                    catch
                    {
                        return null; // Retry
                    }
                });
            }


            void SafeClick(By locator)
            {
                for (int i = 0; i < 6; i++)
                {
                    try
                    {
                        IWebElement el = SafeFind(locator);

                        ((IJavaScriptExecutor)driver).ExecuteScript(
                            "arguments[0].scrollIntoView({block:'center'});", el);

                        try
                        {
                            el.Click();
                            return;
                        }
                        catch
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", el);
                            return;
                        }
                    }
                    catch
                    {
                        Thread.Sleep(300);
                    }
                }
                throw new Exception("SafeClick failed for: " + locator);
            }

            void SafeSendKeys(By locator, string text)
            {
                var el = SafeFind(locator);
                el.Clear();
                el.SendKeys(text);
            }

            // ------------------------------------------------------

            try
            {
                login.LoginSuccessCompanyAdmin();

                // Open Products Menu
                SafeClick(By.Id("menuItem-Products"));
                SafeClick(By.Id("menuItem-Products2"));

                // Select search type
                SafeClick(By.ClassName("searchTypeBox"));

                var searchOptions = SafeFindAll(By.TagName("mat-option"));
                searchOptions[0].Click();

                // Enter search text
                SafeSendKeys(By.Name("searchText"), DeleteSubcategoryData.DeleteSubCategorySuccess["name"]);
                driver.FindElement(By.Name("searchText")).SendKeys(Keys.Enter);

                Thread.Sleep(800);

                // Wait for table
                IWebElement table = SafeFind(By.TagName("tbody"));
                var rows = SafeFindAll(By.XPath("//tbody/tr"));

                if (rows.Count == 0)
                {
                    Console.WriteLine("No rows found.");
                    test.Skip();
                    return;
                }

                wait.Until(ExpectedConditions.ElementExists(By.XPath("//tbody/tr")));

                SafeClick(By.XPath("//tbody/tr[1]/td[last()]"));

                // Wait menu
                IWebElement menuPanel = SafeFind(By.ClassName("mat-menu-panel"));
                SafeClick(By.XPath("//button[contains(text(),'Delete')]"));
               

                //foreach (var b in menuButtons)
                //{
                //    if (b.Text.Contains("Delete"))
                //    {
                //        b.Click();
                //        break;
                //    }
                //}

                // Dialog confirm
                IWebElement dialog = SafeFind(By.TagName("mat-dialog-container"));
                var confirmBtns = SafeFindAll(By.XPath("//mat-dialog-container//button[contains(@class,'mat-primary')]"));

                foreach (var btn in confirmBtns)
                {
                    if (btn.Text.Contains("Confirm"))
                    {
                        btn.Click();
                        break;
                    }
                }

                Thread.Sleep(1500);

               
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
               // bool isSuccess = false;
                Console.WriteLine(snackbar.Text);
                //if (snackbar.Text.Contains(snackbar.Text))
                //{
                //    isSuccess = true;

                //}
                //Assert.IsTrue(isSuccess, " failed..");


                //IWebElement noData = SafeFind(By.XPath("//div[contains(text(),'No Data')]"));
                //Assert.IsTrue(noData.Text.Contains("No Data"), "Delete failed — item still present.");

                test.Pass();
            }
            catch (Exception ex)
            {
                test.Fail(ex.Message);
                throw;
            }
        }






        [TestMethod]
        public void DeleteSubCategoryFailure()
        {
            test = extent.CreateTest("Validating Delete SubCategory Failure");


            expectedStatus = "Passed";
            description = "test case to test delete sub category. add valid data in DeleteSubCategoryFailure in DeleteSubcategoryData file";
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
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
                        IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
                        Thread.Sleep(1000);
                        select.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
                        searchOptions[1].Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
                        IWebElement searchInput = driver.FindElement(By.Name("searchText"));
                        searchInput.Clear();
                        searchInput.SendKeys(DeleteSubcategoryData.DeleteSubCategoryFailure["subCategoryId"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(1000);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-focus-indicator")));
                        IWebElement actions = driver.FindElement(By.ClassName("mat-focus-indicator"));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IWebElement table = driver.FindElement(By.TagName("tbody"));
                        IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
                        IWebElement previousData = driver.FindElement(By.TagName("td"));
                        if (datas.Count > 0)
                        {
                            IWebElement firstRow = datas[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            IWebElement lastColumn = columns[columns.Count - 1];
                            lastColumn.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                            IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                            foreach (var item in menuItems)
                            {
                                if (item.Text.Contains("Delete"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-dialog-container")));
                            IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                            IList<IWebElement> dialogBtn = dialogElement.FindElements(By.ClassName("mat-primary"));
                            foreach (var item in dialogBtn)
                            {
                                if (item.Text.Contains("Confirm"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            Thread.Sleep(1000);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                            IWebElement table1 = driver.FindElement(By.TagName("tbody"));
                            IList<IWebElement> tableDatas = table1.FindElements(By.TagName("tr"));
                            IWebElement currentdata = driver.FindElement(By.TagName("td"));
                            Assert.AreEqual(currentdata, previousData, "Error in deleting");

                            test.Pass();    
                        }
                        else
                        {
                            Console.WriteLine("No rows found in the table.");

                            test.Skip();
                        }
                    }
                    else
                    {
                        Console.WriteLine("doesnt find add null");

                        test.Skip();
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
            String formatedTime = $"{timeTaken.TotalSeconds:F2}";
            driver.Quit();
            testResult.WriteTestResults(TestContext, formatedTime, expectedStatus, errorMessage, description);


            extent.Flush();
        }
    }
}
