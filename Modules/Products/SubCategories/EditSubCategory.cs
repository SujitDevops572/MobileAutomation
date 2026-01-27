using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Products.SubCategories;

namespace VMS_Phase1PortalAT.Modules.Products.SubCategories
{
    [TestClass]
    public class EditSubCategory
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
        public void EditSubCategorySuccess()
        {
            test = extent.CreateTest("Validating Edit SubCategory Success");
            expectedStatus = "Passed";

            if (driver == null)
            {
                Login login = new Login();
                driver = login.getdriver();
                login.LoginSuccessCompanyAdmin();
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            //-------------------------------------------------------------
            // Optimized reusable helpers
            //-------------------------------------------------------------

            void WaitForOverlays()
            {
                // Find overlays first
                var overlays = driver.FindElements(By.CssSelector(".cdk-overlay-backdrop, .cdk-overlay-pane"));

                // Only wait if at least one overlay is displayed
                if (overlays.Any(o => o.Displayed))
                {
                    wait.Until(d =>
                    {
                        try
                        {
                            var activeOverlays = d.FindElements(By.CssSelector(".cdk-overlay-backdrop, .cdk-overlay-pane"))
                                                  .Where(o => o.Displayed).ToList();
                            return activeOverlays.Count == 0;
                        }
                        catch
                        {
                            return true;
                        }
                    });

                    Thread.Sleep(80); // small pause after overlay disappears
                }
            }


            void WaitForOverlaysExceptDialog()
            {
                wait.Until(d =>
                {
                    try
                    {
                        var overlays = d.FindElements(By.CssSelector(".cdk-overlay-backdrop, .cdk-overlay-pane"));
                        var blocking = overlays.Where(o => !o.GetAttribute("class").Contains("mat-dialog-container"));
                        return blocking.Count() == 0;
                    }
                    catch { return true; }
                });

                Thread.Sleep(80);
            }

            IWebElement SafeFind(By locator)
{
    return wait.Until(d =>
    {
        try
        {
            IWebElement el = null;

            // Try locating the element first
            try
            {
                el = d.FindElement(locator);
                if (el.Displayed && el.Enabled)
                    return el;
            }
            catch
            {
                // Element not found yet, continue to check overlays
            }

            // If element not ready, wait for any overlays to disappear
            WaitForOverlays();

            // Re-locate the element after overlay disappears
            el = d.FindElement(locator);
            return (el.Displayed && el.Enabled) ? el : null;
        }
        catch
        {
            return null; // retry in next polling interval
        }
    });
}


            void SafeClick(By locator)
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        // First, attempt to locate the element safely
                        IWebElement el = SafeFind(locator);

                        // If not ready, SafeFind will already handle overlays, retry in next loop
                        if (el == null) { Thread.Sleep(150); continue; }

                        // Wait until element is clickable
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));

                        // Scroll element into view
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", el);
                        Thread.Sleep(80);

                        try
                        {
                            el.Click();
                        }
                        catch
                        {
                            // JS click as fallback
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", el);
                        }

                        return; // success, exit method
                    }
                    catch
                    {
                        // Retry after short sleep
                        Thread.Sleep(150);
                    }
                }

                // Final fallback: try JS click one last time
                try
                {
                    IWebElement el = SafeFind(locator);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", el);
                }
                catch
                {
                    throw new Exception($"Unable to click element: {locator}");
                }
            }

            void SafeSendKeys(By locator, string text)
            {
                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        //WaitForOverlays();
                        IWebElement el = SafeFind(locator);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));

                        el.Clear();
                        el.SendKeys(text);
                        return;
                    }
                    catch
                    {
                        Thread.Sleep(120);
                    }
                }

                IWebElement jsEl = SafeFind(locator);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value='';", jsEl);
                jsEl.SendKeys(text);
            }

            void WaitForTableStable()
            {
                wait.Until(d =>
                {
                    try
                    {
                        var rows = d.FindElements(By.CssSelector("tbody tr"));
                        if (rows.Count == 0) return false;

                        int c1 = rows.Count;
                        Thread.Sleep(160);
                        int c2 = d.FindElements(By.CssSelector("tbody tr")).Count;
                        Thread.Sleep(160);
                        int c3 = d.FindElements(By.CssSelector("tbody tr")).Count;

                        return c1 == c2 && c2 == c3;
                    }
                    catch { return false; }
                });

                Thread.Sleep(150);
            }

            IWebElement WaitSnackbar()
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementIsVisible(By.CssSelector(".mat-snack-bar-container")));
                
            }

            //-------------------------------------------------------------
            // Test Steps (unchanged except fixed name field)
            //-------------------------------------------------------------
            try
            {
                // Navigate
                SafeClick(By.Id("menuItem-Products"));
                SafeClick(By.Id("menuItem-Products2"));

                // Search filter
                SafeClick(By.ClassName("searchTypeBox"));
                wait.Until(d => d.FindElements(By.CssSelector("mat-option")).Count >= 2);
                driver.FindElements(By.CssSelector("mat-option"))[1].Click();

                IWebElement search = SafeFind(By.Name("searchText"));
                search.Clear();
                search.SendKeys(EditSubCategoryData.EditSubCategorySuccess["name"]);
                search.SendKeys(Keys.Enter);

                // Wait for table
                WaitForTableStable();

                var rows = driver.FindElements(By.CssSelector("tbody tr"));
                if (rows.Count == 0)
                {
                    test.Skip("No results found after search.");
                    return;
                }

                WaitForTableStable();
                // Open 3-dots menu
                SafeClick(By.XPath("(//tbody/tr)[1]/td[last()]"));
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".mat-menu-panel")));

                var menuItems = driver.FindElements(By.CssSelector(".mat-menu-panel button"));
                var editBtn = menuItems.FirstOrDefault(m => m.Text.Contains("Edit"));
                if (editBtn == null) throw new Exception("Edit button not found");

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", editBtn);

                //// Wait dialog
                //By dialogLocator = By.CssSelector("mat-dialog-container");
                //wait.Until(ExpectedConditions.ElementIsVisible(dialogLocator));
                //WaitForOverlaysExceptDialog();

                //-------------------------------------------------------------
                // FIXED: Safe locate name input inside Angular dialog
                //-------------------------------------------------------------
                // Assuming it's the first mat-form-field/input in the dialog
                By nameLocator = By.CssSelector("mat-dialog-container app-edit-sub-category mat-form-field:first-of-type input.mat-input-element");

                var nameField = wait.Until(d =>
                {
                    var el = d.FindElement(nameLocator);
                    return (el.Displayed && el.Enabled) ? el : null;
                });


               
                //SafeFind(nameLocator); // ensures located
                SafeSendKeys(nameLocator, EditSubCategoryData.EditSubCategorySuccess["name1"]);

                // Submit
                By submitBtn = By.CssSelector("mat-dialog-container button.mat-raised-button");
                SafeClick(submitBtn);

                // Snackbar confirmation
                IWebElement snack = WaitSnackbar();
                Console.WriteLine(snack.Text);
                Assert.IsTrue(snack.Text.Contains("Sub Category Updated"));

                test.Pass("Sub Category Updated successfully.");
            }
            catch (Exception ex)
            {
                test.Fail("Error: " + ex.Message);
                throw;
            }
        }




        [TestMethod]
        public void EditSubCategoryFailure()
        {

            test = extent.CreateTest("Validating Edit SubCategory Failure");

            expectedStatus = "Passed";
            description = "test case to test edit sub category. add valid data in EditSubCategoryFailure in EditSubCategoryData file";
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
                        searchInput.SendKeys(EditSubCategoryData.EditSubCategoryFailure["subCategoryId"]);
                        searchInput.SendKeys(Keys.Enter);
                        Thread.Sleep(2000);
                        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                        IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                        IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
                        if (data.Count > 0)
                        {
                            IWebElement firstRow = data[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            IWebElement lastColumn = columns[columns.Count - 1];
                            lastColumn.Click();
                            Thread.Sleep(1000);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
                            IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                            foreach (var item in menuItems)
                            {
                                if (item.Text.Contains("Edit"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            Thread.Sleep(1000);

                            By nameLocator = By.XPath("//mat-label[contains(text(),'Sub Category Name')]/ancestor::mat-form-field//input");
                            By submitLocator = By.ClassName("mat-raised-button");

                            // find the input safely
                            IWebElement name = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(nameLocator));

                            // safely clear and type
                            for (int i = 0; i < 3; i++)
                            {
                                try
                                {
                                    name = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(nameLocator));
                                    name.Clear();
                                    name.SendKeys(EditSubCategoryData.EditSubCategoryFailure["name"]);
                                    break;
                                }
                                catch (StaleElementReferenceException)
                                {
                                    name = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(nameLocator));
                                }
                                catch (ElementClickInterceptedException)
                                {
                                    Thread.Sleep(300);
                                }
                            }

                            // find the button safely
                            IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(submitLocator));

                            // safe click
                            for (int i = 0; i < 3; i++)
                            {
                                try
                                {
                                    submit.Click();
                                    break;
                                }
                                catch (ElementClickInterceptedException)
                                {
                                    Thread.Sleep(300);
                                    submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(submitLocator));
                                }
                                catch (StaleElementReferenceException)
                                {
                                    submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(submitLocator));
                                }
                            }






                            //IWebElement name = driver.FindElement(By.XPath("//mat-label[contains(text(),'Sub Category Name')]/ancestor::mat-form-field//input\r\n"));
                            //IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            //name.Clear();
                            //name.SendKeys(EditSubCategoryData.EditSubCategoryFailure["name"]);
                            //submit.Click();



                            string title = "Error updating Sub Category";
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".mat-snack-bar-container")));
                            IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                            bool isSuccess = false;
                            if (snackbar.Text.Contains(title))
                            {
                                isSuccess = true;

                            }
                            Console.WriteLine(snackbar.Text);
                            Assert.IsTrue(isSuccess, " failed..");

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
        public void EditCategoryBtnDisable()
        {
            test = extent.CreateTest("Validating  Edit Category BtnDisable");

            expectedStatus = "Passed";
            description = "test case to test edit sub category with invalid data";
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
                        if (data.Count > 0)
                        {
                            IWebElement firstRow = data[0];
                            IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                            IWebElement lastColumn = columns[columns.Count - 1];
                            lastColumn.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("mat-menu-panel-0")));
                            IWebElement matMenu = driver.FindElement(By.Id("mat-menu-panel-0"));
                            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
                            foreach (var item in menuItems)
                            {
                                if (item.Text.Contains("Edit"))
                                {
                                    item.Click();
                                    break;
                                }
                            }
                            Thread.Sleep(1000);
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
