using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Products.Categories;

namespace VMS_Phase1PortalAT.Modules.Products.Categories
{
    [TestClass]
    public class EditCategory
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

        //[TestMethod]
        //public void EditCategorySuccess()
        //{

        //    test = extent.CreateTest("Validating Edit Category Success");

        //    expectedStatus = "Passed";
        //    description = "test case to test edit category. add valid data in EditBranchSuccess in EditBranchData file";
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
        //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products1")));
        //            IWebElement submenu = driver.FindElement(By.Id("menuItem-Products1"));
        //            if (submenu != null)
        //            {
        //                submenu.Click();
        //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
        //                IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
        //                Thread.Sleep(1000);
        //                select.Click();
        //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
        //                IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
        //                searchOptions[0].Click();
        //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
        //                IWebElement searchInput = driver.FindElement(By.Name("searchText"));
        //                searchInput.Clear();
        //                searchInput.SendKeys(EditCategoryData.EdiCategorySuccess["name"]);
        //                searchInput.SendKeys(Keys.Enter);
        //                Thread.Sleep(1000);

        //                // Wait for the table body to be clickable
        //                IWebElement tableBody = wait.Until(
        //                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody"))
        //                );

        //                // Refetch the rows each time to avoid stale reference
        //                IList<IWebElement> dataRows = wait.Until(driver =>
        //                {
        //                    var rows = tableBody.FindElements(By.TagName("tr"));
        //                    return rows.Count > 0 ? rows : null;
        //                });

        //                if (dataRows != null && dataRows.Count > 0)
        //                {
        //                    IWebElement firstRow = dataRows[0];

        //                    // Refetch columns to avoid stale references
        //                    IList<IWebElement> columns = wait.Until(driver =>
        //                    {
        //                        var cols = firstRow.FindElements(By.TagName("td"));
        //                        return cols.Count > 0 ? cols : null;
        //                    });

        //                    IWebElement lastColumn = columns[columns.Count - 1];

        //                    // Click safely using Actions
        //                    Actions actions = new Actions(driver);
        //                    actions.MoveToElement(lastColumn).Click().Perform();

        //                    // Wait for mat-menu to appear
        //                    IWebElement matMenu = wait.Until(driver =>
        //                    {
        //                        var menu = driver.FindElement(By.ClassName("mat-menu-panel"));
        //                        return menu.Displayed ? menu : null;
        //                    });

        //                    IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));

        //                    // Click the Edit button
        //                    IWebElement editButton = menuItems.FirstOrDefault(item => item.Text.Contains("Edit"));
        //                    if (editButton != null)
        //                    {
        //                        actions.MoveToElement(editButton).Click().Perform();
        //                    }



        //                //    IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
        //                //IList<IWebElement> data = table.FindElements(By.TagName("tr"));
        //                //IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
        //                //if (data.Count > 0)
        //                //{
        //                //    IWebElement firstRow = data[0];
        //                //    IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
        //                //    IWebElement lastColumn = columns[columns.Count - 1];
        //                //    Thread.Sleep(1500);
        //                //        lastColumn.Click();
        //                //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
        //                //    IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
        //                //    IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));

        //                //    foreach (var item in menuItems)
        //                //    {
        //                //        if (item.Text.Contains("Edit"))
        //                //        {
        //                //            item.Click();
        //                //            break;
        //                //        }
        //                //    }
        //                    IWebElement name = driver.FindElement(By.Name("name"));
        //                    IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
        //                    name.Clear();
        //                    name.SendKeys(EditCategoryData.EdiCategorySuccess["name1"]);
        //                    Thread.Sleep(1000);
        //                    submit.Click();
        //                    string title = "Category Updated";
        //                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".mat-snack-bar-container")));
        //                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
        //                    bool isSuccess = false;
        //                    if (snackbar.Text.Contains(title))
        //                    {
        //                        isSuccess = true;

        //                    }
        //                    Assert.IsTrue(isSuccess, " failed..");

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



        //[TestMethod]
        //public void EditCategorySuccess()
        //{
        //    test = extent.CreateTest("Validating Edit Category Success");

        //    expectedStatus = "Passed";
        //    description = "test case to test edit category. add valid data in EditBranchSuccess in EditBranchData file";
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

        //        // Click main menu
        //        IWebElement menu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products")));
        //        menu.Click();

        //        // Click submenu
        //        IWebElement submenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products1")));
        //        submenu.Click();

        //        // Click search type dropdown
        //        IWebElement select = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
        //        select.Click();

        //        // Select first mat-option
        //        IWebElement option = wait.Until(driver =>
        //        {
        //            var elements = driver.FindElements(By.TagName("mat-option"));
        //            return elements.Count > 0 ? elements[0] : null;
        //        });
        //        option.Click();

        //        // Enter search text
        //        IWebElement searchInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("searchText")));
        //        searchInput.Clear();
        //        searchInput.SendKeys(EditCategoryData.EdiCategorySuccess["name"]);
        //        searchInput.SendKeys(Keys.Enter);

        //        // Wait for table
        //        IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
        //        IList<IWebElement> data = table.FindElements(By.TagName("tr"));

        //        if (data.Count > 0)
        //        {
        //            IWebElement firstRow = data[0];



        //            // Click last column safely
        //            // Wait for the last column to be visible
        //            IWebElement lastColumn = wait.Until(driver =>
        //            {
        //                //var columns = firstRow.FindElements(By.TagName("td"));
        //                // return columns.Count > 0 ? columns[columns.Count - 1] : null;
        //                return driver.FindElement(By.XPath("(//td)[10]"));
        //            });
        //            lastColumn.Click();
        //            // Scroll into view (helps if element is offscreen)
        //            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", lastColumn);

        //            //// Click using JavaScript to avoid overlay / intercepted clicks
        //            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", lastColumn);

        //            // Wait for mat-menu and click Edit
        //            IWebElement matMenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-menu-panel")));
        //            IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
        //            foreach (var item in menuItems)
        //            {
        //                if (item.Text.Contains("Edit"))
        //                {
        //                    // Scroll menu item into view
        //                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", item);

        //                    // Click using JavaScript
        //                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", item);

        //                    break;
        //                }
        //            }

        //            // Edit category
        //            IWebElement name = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("name")));
        //            IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-raised-button")));
        //            name.Clear();
        //            name.SendKeys(EditCategoryData.EdiCategorySuccess["name"]);
        //            submit.Click();

        //            // Verify snackbar
        //            IWebElement snackbar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".mat-snack-bar-container")));
        //            Assert.IsTrue(snackbar.Text.Contains("Category Updated"), "Edit category failed");

        //            test.Pass();
        //        }
        //        else
        //        {
        //            Console.WriteLine("No rows found");
        //            test.Skip();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorMessage = ex.Message;
        //        if (ex.InnerException != null)
        //        {
        //            errorMessage = ex.InnerException.Message;
        //        }
        //        test.Fail(errorMessage);
        //        throw;
        //    }
        //}



        [TestMethod]
        public void EditCategorySuccess()
        {
            test = extent.CreateTest("Validating Edit Category Success");

            expectedStatus = "Passed";
            description = "Test editing category using valid data";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            try
            {
                LoginSuccess.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

                ClickWhenClickable(By.Id("menuItem-Products"), wait);
                ClickWhenClickable(By.Id("menuItem-Products1"), wait);

                //  SEARCH DROPDOWN
                
                Console.WriteLine("Opening Search dropdown...");
                IWebElement searchBox = wait.Until(d => d.FindElement(By.CssSelector(".searchTypeBox")));
                IWebElement matSelect = searchBox.FindElement(By.CssSelector("mat-select"));
                IWebElement trigger = matSelect.FindElement(By.CssSelector(".mat-select-trigger"));
                js.ExecuteScript("arguments[0].click();", trigger);

                // Wait for options and select first
                IList<IWebElement> options = wait.Until(driver =>
                {
                    var list = driver.FindElements(By.CssSelector(".cdk-overlay-container mat-option"));
                    return list.Count > 0 ? list : null;
                });
                js.ExecuteScript("arguments[0].click();", options[0]);

                IWebElement searchInput = wait.Until(d => d.FindElement(By.Name("searchText")));
                searchInput.Clear();
                searchInput.SendKeys(EditCategoryData.EdiCategorySuccess["name"]);
                searchInput.SendKeys(Keys.Enter);

                WaitForAngular(driver);

                Console.WriteLine("Waiting for table rows...");
                IList<IWebElement> rows = wait.Until(d =>
                {
                    var r = d.FindElements(By.CssSelector("table tbody tr"))
                             .Where(x => x.Displayed).ToList();
                    return r.Count > 0 ? r : null;
                });

                Console.WriteLine("Rows found: " + rows.Count);
                IWebElement firstRow = rows[0];

                IWebElement actionBtn = firstRow.FindElements(By.TagName("button")).LastOrDefault();
                if (actionBtn == null)
                    throw new Exception("Action button not found in row!");

                Console.WriteLine("Clicking action button...");
                js.ExecuteScript("arguments[0].scrollIntoView(true);", actionBtn);
                js.ExecuteScript("arguments[0].click();", actionBtn);

                WaitForAngular(driver);

              
                //  OPEN MAT-MENU
                IWebElement matMenu = driver.FindElements(By.CssSelector(".cdk-overlay-container .mat-menu-panel"))
                                            .FirstOrDefault(m => m.Displayed);
                if (matMenu == null)
                    throw new Exception("mat-menu did not open!");

                Console.WriteLine("mat-menu detected");

                var menuItems = matMenu.FindElements(By.CssSelector("button, mat-menu-item, div[role='menuitem'], li"))
                                       .Where(x => x.Displayed)
                                       .ToList();

                if (menuItems.Count == 0)
                    throw new Exception("No menu items found inside mat-menu.");

                var editBtn = menuItems.FirstOrDefault(x => x.Text.Trim().ToLower().Contains("edit"));
                if (editBtn == null)
                    throw new Exception("Edit option NOT found in mat-menu!");

                Console.WriteLine("Clicking Edit...");
                js.ExecuteScript("arguments[0].click();", editBtn);

                WaitForAngular(driver);

                //  EDIT CATEGORY FORM
                IWebElement nameField = wait.Until(d => d.FindElement(By.Name("name")));
                nameField.Clear();
                nameField.SendKeys(EditCategoryData.EdiCategorySuccess["name1"]);

                IWebElement submitButton = wait.Until(d =>
                    d.FindElements(By.CssSelector("button.mat-raised-button"))
                     .Where(x => x.Displayed && x.Enabled)
                     .First()
                );

                Console.WriteLine("Clicking Submit...");
                js.ExecuteScript("arguments[0].click();", submitButton);

                // =========================
                //  IMMEDIATE SNACKBAR DETECTION & ASSERTION
                // =========================
                Console.WriteLine("Waiting for snackbar...");
                DetectSnackbarMessage(driver, "Category Updated"); // This method asserts internally
                Console.WriteLine("Snackbar validated.");

                test.Pass();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("Test failed: " + errorMessage);
                test.Fail(errorMessage);
                throw;
            }
        }

        // =========================
        //  HELPER METHOD
        // =========================
        private void DetectSnackbarMessage(IWebDriver driver, string expectedText, int timeoutSeconds = 5)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));

            bool found = wait.Until(d =>
            {
                var bars = d.FindElements(By.CssSelector(".mat-snack-bar-container"));

                foreach (var bar in bars)
                {
                    string txt = bar.Text.Trim();
                    if (!string.IsNullOrEmpty(txt) &&
                        txt.Contains(expectedText, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Snackbar detected: " + txt);
                        return true;
                    }
                }

                return false; // retry
            });

            Assert.IsTrue(found, $"Snackbar with text '{expectedText}' not detected.");
        }











        [TestMethod]
        public void EditCategoryFailure()
        {
            test = extent.CreateTest("Validating Edit Category Failure");
            expectedStatus = "Passed";
            description = "Test invalid data case for category edit.";

            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                LoginSuccess.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
            }

            try
            {
                // =========================
                //  NAVIGATION
                // =========================
                Console.WriteLine("Clicking main menu...");
                ClickWhenClickable(By.Id("menuItem-Products"), wait);

                Console.WriteLine("Clicking submenu...");
                ClickWhenClickable(By.Id("menuItem-Products1"), wait);


                // =========================
                //  SEARCH DROPDOWN
                // =========================
                Console.WriteLine("Locating searchTypeBox...");
                IWebElement searchBox = wait.Until(d => d.FindElement(By.CssSelector(".searchTypeBox")));

                Console.WriteLine("Opening search dropdown...");
                IWebElement matSelect = searchBox.FindElement(By.CssSelector("mat-select"));
                IWebElement trigger = matSelect.FindElement(By.CssSelector(".mat-select-trigger"));
                js.ExecuteScript("arguments[0].click();", trigger);

                // wait for options
                IList<IWebElement> options = wait.Until(d =>
                {
                    var o = d.FindElements(By.CssSelector(".cdk-overlay-container mat-option"));
                    return o.Count > 1 ? o : null;
                });

                Console.WriteLine($"Options found: {options.Count}. Clicking Category Id...");
                js.ExecuteScript("arguments[0].click();", options[0]);


                // =========================
                //  SEARCH TEXT
                // =========================
                Console.WriteLine("Entering search text...");
                IWebElement searchInput = wait.Until(d => d.FindElement(By.Name("searchText")));
                searchInput.Clear();
                searchInput.SendKeys(EditCategoryData.EditCategoryFailure["name"]);
                searchInput.SendKeys(Keys.Enter);

                WaitForAngular(driver);


                // =========================
                //  TABLE LOAD
                // =========================
                Console.WriteLine("Waiting for table rows...");
                IList<IWebElement> rows = wait.Until(d =>
                {
                    var r = d.FindElements(By.CssSelector("table tbody tr"))
                            .Where(x => x.Displayed).ToList();
                    return r.Count > 0 ? r : null;
                });

                Console.WriteLine($"Rows loaded: {rows.Count}");

                IWebElement firstRow = rows[0];
                IWebElement actionBtn = firstRow.FindElements(By.TagName("button")).LastOrDefault();

                if (actionBtn == null)
                    throw new Exception("Action button not found in row.");


                // =========================
                //  OPEN ACTION MENU
                // =========================
                Console.WriteLine("Clicking action button...");
                js.ExecuteScript("arguments[0].scrollIntoView(true);", actionBtn);
                js.ExecuteScript("arguments[0].click();", actionBtn);

                WaitForAngular(driver);

                // retry load mat-menu
                IWebElement matMenu = null;
                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        matMenu = driver.FindElements(By.CssSelector(".cdk-overlay-container .mat-menu-panel"))
                                        .FirstOrDefault(x => x.Displayed);
                        if (matMenu != null)
                            break;
                    }
                    catch { }

                    Console.WriteLine("Retry clicking action button...");
                    js.ExecuteScript("arguments[0].click();", actionBtn);
                    Thread.Sleep(400);
                }

                if (matMenu == null)
                    throw new Exception("mat-menu did not open.");

                Console.WriteLine("mat-menu detected");

                IWebElement editBtn = matMenu.FindElements(By.TagName("button"))
                                             .FirstOrDefault(x => x.Text.Trim().Equals("Edit", StringComparison.OrdinalIgnoreCase));

                if (editBtn == null)
                    throw new Exception("Edit option not found in menu.");

                Console.WriteLine("Clicking Edit...");
                js.ExecuteScript("arguments[0].click();", editBtn);

                WaitForAngular(driver);


                // =========================
                //  EDIT FORM
                // =========================
                Console.WriteLine("Editing category...");
                IWebElement nameField = wait.Until(d => d.FindElement(By.Name("name")));
                nameField.Clear();
                nameField.SendKeys(EditCategoryData.EditCategoryFailure["name"]);

                // get the submit button
                IWebElement submitBtn = wait.Until(d =>
                    d.FindElements(By.CssSelector("button.mat-raised-button"))
                     .Where(x => x.Displayed)
                     .First()
                );

                Console.WriteLine("Checking if Save button is disabled...");

                // =========================
                //  ** ASSERT SAVE BUTTON DISABLED **
                // =========================
                bool isDisabled =
                    submitBtn.GetAttribute("disabled") != null ||
                    submitBtn.GetAttribute("aria-disabled") == "true" ||
                    !submitBtn.Enabled;

                Assert.IsTrue(isDisabled, "Save button is NOT disabled for invalid input.");

                test.Pass();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("Test failed: " + errorMessage);
                test.Fail(errorMessage);
                throw;
            }
        }



        // ====================================================================
        // HELPER METHODS
        // ====================================================================
        private void ClickWhenClickable(By selector, WebDriverWait wait)
        {
            wait.Until(d =>
            {
                var el = d.FindElement(selector);
                if (el.Displayed && el.Enabled)
                {
                    el.Click();
                    return true;
                }
                return false;
            });
        }

        private void WaitForAngular(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                    .Until(d =>
                    {
                        try
                        {
                            return (bool)js.ExecuteScript(
                                "return (window.getAllAngularTestabilities && getAllAngularTestabilities().every(x => x.isStable()))"
                            );
                        }
                        catch
                        {
                            return true;
                        }
                    });
            }
            catch { }

            Thread.Sleep(300); // micro stabilization
        }


        






        //[TestMethod]
        //public void EditCategoryFailure()
        //{
        //    test = extent.CreateTest("Validating Edit Category Failure");

        //    expectedStatus = "Passed";
        //    description = "test case to test edit category. add invalid data in EditCategoryFailure in EditCategoryData file";
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
        //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products1")));
        //            IWebElement submenu = driver.FindElement(By.Id("menuItem-Products1"));
        //            if (submenu != null)
        //            {
        //                submenu.Click();
        //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("searchTypeBox")));
        //                IWebElement select = driver.FindElement(By.ClassName("searchTypeBox"));
        //                Thread.Sleep(1000);
        //                select.Click();
        //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
        //                IList<IWebElement> searchOptions = driver.FindElements(By.TagName("mat-option"));
        //                searchOptions[1].Click();
        //                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("searchText")));
        //                IWebElement searchInput = driver.FindElement(By.Name("searchText"));
        //                searchInput.Clear();
        //                searchInput.SendKeys(EditCategoryData.EditCategoryFailure["categoryId"]);
        //                searchInput.SendKeys(Keys.Enter);
        //                Thread.Sleep(1000);

        //                // Wait for the table body to be clickable
        //                IWebElement tableBody = wait.Until(
        //                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody"))
        //                );

        //                // Refetch the rows each time to avoid stale reference
        //                IList<IWebElement> dataRows = wait.Until(driver =>
        //                {
        //                    var rows = tableBody.FindElements(By.TagName("tr"));
        //                    return rows.Count > 0 ? rows : null;
        //                });

        //                if (dataRows != null && dataRows.Count > 0)
        //                {
        //                    IWebElement firstRow = dataRows[0];

        //                    // Refetch columns to avoid stale references
        //                    IList<IWebElement> columns = wait.Until(driver =>
        //                    {
        //                        var cols = firstRow.FindElements(By.TagName("td"));
        //                        return cols.Count > 0 ? cols : null;
        //                    });

        //                    IWebElement lastColumn = columns[columns.Count - 1];

        //                    // Click safely using Actions
        //                    Actions actions = new Actions(driver);
        //                    actions.MoveToElement(lastColumn).Click().Perform();

        //                    // Wait for mat-menu to appear
        //                    IWebElement matMenu = wait.Until(driver =>
        //                    {
        //                        var menu = driver.FindElement(By.ClassName("mat-menu-panel"));
        //                        return menu.Displayed ? menu : null;
        //                    });

        //                    IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));

        //                    // Click the Edit button
        //                    IWebElement editButton = menuItems.FirstOrDefault(item => item.Text.Contains("Edit"));
        //                    if (editButton != null)
        //                    {
        //                        actions.MoveToElement(editButton).Click().Perform();
        //                    }






        //                //IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
        //                //IList<IWebElement> data = table.FindElements(By.TagName("tr"));
        //                //IWebElement prevoiusdata = driver.FindElement(By.TagName("td"));
        //                //if (data.Count > 0)
        //                //{
        //                //    IWebElement firstRow = data[0];
        //                //    IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
        //                //    IWebElement lastColumn = columns[columns.Count - 1];
        //                //    Thread.Sleep(1500);
        //                //    lastColumn.Click();

        //                //    Thread.Sleep(1000);
        //                //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-menu-panel")));
        //                //    IWebElement matMenu = driver.FindElement(By.ClassName("mat-menu-panel"));
        //                //    IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));
        //                //    foreach (var item in menuItems)
        //                //    {
        //                //        if (item.Text.Contains("Edit"))
        //                //        {
        //                //            item.Click();
        //                //            break;
        //                //        }
        //                //    }
        //                //    Thread.Sleep(1000);
        //                IWebElement name = driver.FindElement(By.Name("name"));
        //                    IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
        //                    name.Clear();
        //                    name.SendKeys(EditCategoryData.EditCategoryFailure["name"]);
        //                    submit.Click();
        //                    string title = "Error updating Category";
        //                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".mat-snack-bar-container")));
        //                    IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
        //                    bool isSuccess = false;
        //                    if (snackbar.Text.Contains(title))
        //                    {
        //                        isSuccess = true;

        //                    }
        //                    Assert.IsTrue(isSuccess, " failed..");

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
        public void EditCategoryBtnDisable()
        {
            test = extent.CreateTest("Validating  Edit Category BtnDisable");


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
