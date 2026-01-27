using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using Docker.DotNet.Models;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;
using VMS_Phase1PortalAT.utls.datas.Advertisement.Mapping;
using VMS_Phase1PortalAT.utls.datas.Company.Branch;
using VMS_Phase1PortalAT.utls.datas.UserManagement.ClientUserList;


namespace VMS_Phase1PortalAT.Modules.Advertisement.Mapping
{
    [TestClass]
    public class OptionsMapping
    {
        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;

        public required TestContext TestContext { get; set; }

        private IWebDriver driver;
        private setupData setupDatas = new setupData();
        private WriteResultToCSV testResult = new WriteResultToCSV();

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
        public void EditMapping()
        {
            test = extent.CreateTest("Check the Edit Mapping in Mapping Option");

            expectedStatus = "Passed";
            description = "Test case to check the EditMapping in Mapping Option";

            // Initialize WebDriver
            Login loginHelper = new Login();
            driver = loginHelper.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                loginHelper.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                // Navigate to Validation Group List
                Console.WriteLine("Navigating to Client User List...");
                ClickElementWhenReady(wait, By.Id("menuItem-Advertisements"));
                ClickElementWhenReady(wait, By.Id("menuItem-Advertisements3"));

                WaitForTableToLoad(wait);

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
                searchInput.SendKeys(SearchMappingData.SearchScheduleSuccess["search"]);
                searchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);


                ClickElementWhenReady(wait, By.XPath("(//tbody/tr)[1]/td[last()]"));
                Thread.Sleep(200);
                ClickElementWhenReady(wait, By.XPath("//div[contains(@class,'mat-menu-panel')]//button[contains(text(),'Edit')]"));
                Thread.Sleep(3000);

                ClickElementWhenReady(wait, By.XPath("//mat-dialog-container//mat-select[contains(@name,'advertisementId')]"));
                Thread.Sleep(500);
                IList<IWebElement> Advoptions = driver.FindElements(By.XPath("//mat-option"));
                foreach (IWebElement a in Advoptions) {
                    if (a.Text.Split(" ").Contains(SearchMappingData.SearchScheduleSuccess["search"]))
                    {
                        a.Click();
                        Console.WriteLine("Clicked"+a.Text);
                        break;
                    }
                }
                Thread.Sleep(1500);
                ClickElementWhenReady(wait, By.XPath("//span[text()=' Save ']"));
                Thread.Sleep(100);


                var snackbarText = wait.Until(drv =>
                {
                    var el = drv.FindElement(By.CssSelector(".mat-snack-bar-container"));
                    return el.Displayed && !string.IsNullOrWhiteSpace(el.Text)
                           ? el.Text
                           : null;
                });

                Console.WriteLine(snackbarText);

                Assert.IsTrue(snackbarText.Contains("Machine Mapping Added"), "Not Edited!!!");

                Thread.Sleep(500);

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                }
                Console.WriteLine("Test failed with error: " + errorMessage);
                test.Fail(errorMessage);
                expectedStatus = "Failed";
                throw;
            }
        }



        [TestMethod]
        public void ScheduleAdv() {

            test = extent.CreateTest("Check the Schedule Adv in Mapping Option");

            expectedStatus = "Passed";
            description = "Test case to check the Schedule Adv in Mapping Option";

            // Initialize WebDriver
            Login loginHelper = new Login();
            driver = loginHelper.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                loginHelper.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                // Navigate to Validation Group List
                Console.WriteLine("Navigating to Client User List...");
                ClickElementWhenReady(wait, By.Id("menuItem-Advertisements"));
                ClickElementWhenReady(wait, By.Id("menuItem-Advertisements3"));

                WaitForTableToLoad(wait);

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
                searchInput.SendKeys(SearchMappingData.SearchScheduleSuccess["Adv. Name"]);
                searchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);

                IWebElement lastcol = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//tbody/tr)[last()]/td[last()]")));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", lastcol);
                wait.Until(ExpectedConditions.ElementToBeClickable(lastcol)).Click();
                Thread.Sleep(700);

                ClickElementWhenReady(wait, By.XPath("//div[contains(@class,'mat-menu-panel')]//button[contains(text(),'Schedules for Adv')]"));
                Thread.Sleep(200);
                ClickElementWhenReady(wait, By.XPath("//mat-dialog-container//mat-icon[contains(text(),'add')]"));
                Thread.Sleep(200);

                IWebElement dateRangeBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'date_range')]")));
                dateRangeBtn.Click();
                Thread.Sleep(3000);

                String today = DateTime.Today.Day.ToString();
                Console.WriteLine(today);
                String futureDate = DateTime.Today.AddDays(3).Day.ToString();
                Console.WriteLine(futureDate);

                //IWebElement nextButton = driver.FindElement(By.XPath("//button[@aria-label='Next month']"));
                //IWebElement prevButton = driver.FindElement(By.XPath("//button[@aria-label='Previous month']"));

                //IWebElement selectbtn = driver.FindElement(By.ClassName("owl-dt-calendar-control-content"));
                //selectbtn.Click();

                //IWebElement yearselect = driver.FindElement(By.ClassName("owl-date-time-multi-year-view"));


                //while (!currentMonthLabel.Text.Contains(today))
                //{
                //    prevButton.Click();
                //    Thread.Sleep(500);
                //    currentMonthLabel = driver.FindElement(By.ClassName("owl-dt-calendar-control-content"));
                //}

                //IWebElement calender = driver.FindElement(By.ClassName("owl-dt-container-inner"));


                IWebElement month1 = driver.FindElement(By.TagName("owl-date-time-month-view"));

                IWebElement dateElement = month1.FindElement(By.ClassName("owl-dt-calendar-body"));
                IList<IWebElement> dateElements = dateElement.FindElements(By.TagName("td"));
                //Console.WriteLine(currentMonthLabel.Text + " current month");
                foreach (IWebElement datElement in dateElements)
                {
                    if (datElement.Text.Equals(today))
                    {
                        datElement.Click();

                    }
                    if (datElement.Text.Equals(futureDate))
                    {
                        datElement.Click();
                        break;
                    }
                }
                Thread.Sleep(3000);

                IWebElement timeRangeBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[contains(@name,'trange')]")));
                timeRangeBtn.Click();

                IWebElement uparrow = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//input[@class='owl-dt-timer-input'])[1]")));
                uparrow.Clear();
                uparrow.SendKeys(AddMappingData.dateRange["amins"]);
                IWebElement downarrow = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//input[@class='owl-dt-timer-input'])[2]")));
                downarrow.Clear();
                downarrow.SendKeys(AddMappingData.dateRange["asecs"]);
                Thread.Sleep(500);

                IWebElement nextBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'To')]")));
                nextBtn.Click();
                IWebElement auparrow = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//input[@class='owl-dt-timer-input'])[1]")));
                auparrow.Clear();
                auparrow.SendKeys(AddMappingData.dateRange["bmins"]);
                IWebElement adownarrow = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//input[@class='owl-dt-timer-input'])[2]")));
                adownarrow.Clear();
                adownarrow.SendKeys(AddMappingData.dateRange["bsecs"]);
                Thread.Sleep(1000);
                IWebElement set = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Set']")));
                set.Click();
                Thread.Sleep(500);

                IWebElement Addtime = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Add Time Range')]")));
                Addtime.Click();

                IWebElement create = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//span[contains(text(),'Create')]")));
                create.Click();

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar1 = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar1.Text);
                Assert.IsTrue(snackbar1.Text.Contains("Date Range Added"), " failed..");
                //IWebElement table = wait.Until(d => d.FindElement(By.XPath("//mat-dialog-container//table")));
                //var headers = table.FindElements(By.XPath(".//thead/tr/th"))
                //                   .Select(h => h.Text.Trim())
                //                   .ToList();
                //int startDateIndex = headers.FindIndex(h => h.Equals("Start Date", StringComparison.OrdinalIgnoreCase));
                //int endDateIndex = headers.FindIndex(h => h.Equals("End Date", StringComparison.OrdinalIgnoreCase));

                //if (startDateIndex == -1 || endDateIndex == -1)
                //{
                //    throw new Exception("Start Date or End Date column not found");
                //}
                //var rows = table.FindElements(By.XPath(".//tbody/tr"));
                //if (rows.Count == 0)
                //    throw new Exception("No rows found in the table");

                //var lastRow = rows.Last();
                //var cells = lastRow.FindElements(By.TagName("td"));

                //string lastStartDate = cells[startDateIndex].Text.Trim();
                //string lastEndDate = cells[endDateIndex].Text.Trim();

                //Console.WriteLine($"Last Start Date: {lastStartDate}");
                //Console.WriteLine($"Last End Date: {lastEndDate}");


                //if (lastStartDate == today && lastEndDate == futureDate)
                //{
                //    Console.WriteLine("Validation Passed: Last row matches expected values!");
                //}
                //else
                //{
                //    Console.WriteLine("Validation Failed: Last row does NOT match expected values.");
                //}


                Thread.Sleep(3000);
                IWebElement Action = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-dialog-container//td[last()]")));
                Action.Click();
                Thread.Sleep(500);
                IWebElement delete = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div//button[contains(text(),'Delete')]")));
                delete.Click();
                IWebElement confirm = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Confirm')]")));
                confirm.Click();


                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Date Range Deleted"), " not deleted..");

            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                }
                Console.WriteLine("Test failed with error: " + errorMessage);
                test.Fail(errorMessage);
                expectedStatus = "Failed";
                throw;
            }
        }



        [TestMethod]
        public void SetasDefaultAdv()
        {

            test = extent.CreateTest("Check the Set as Default Adv in Mapping Option");

            expectedStatus = "Passed";
            description = "Test case to check the Set as Default Adv in Mapping Option";

            // Initialize WebDriver
            Login loginHelper = new Login();
            driver = loginHelper.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                loginHelper.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                // Navigate to Validation Group List
                Console.WriteLine("Navigating to Client User List...");
                IWebElement menuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements")));
                menuBtn.Click();
                IWebElement submenuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements3")));
                submenuBtn.Click();
                Thread.Sleep(2000);



                int rowCount = driver.FindElements(By.XPath("//tbody/tr")).Count;
                Console.WriteLine("Total Rows: " + rowCount);

                for (int i = 1; i <= rowCount; i++)
                {
                    WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                    try
                    {
                        // Click last column of current row
                        IWebElement lastCol = wait1.Until(
                            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                                By.XPath($"(//tbody/tr)[{i}]/td[last()]")
                            ));
                        Console.WriteLine($"Row {i}: Last column clicked.");
                        lastCol.Click();

                        // Look for "Set as default"
                        IWebElement setAsDefault = wait1.Until(
                            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                                By.XPath("//button[contains(text(),'Set as default')]")
                            ));

                        // Found → click & exit
                        setAsDefault.Click();
                        Console.WriteLine(" Set as default clicked. Exiting loop.");
                        break;
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine($" 'Set as default' not visible in row {i}. Closing menu & moving to next row...");

                        //Close the mat-menu using ESC
                        Actions a = new Actions(driver);
                        a.SendKeys(Keys.Escape).Perform();

                        // Give menu time to close
                        Thread.Sleep(300);

                        continue;
                    }
                    catch (StaleElementReferenceException)
                    {
                        Console.WriteLine($" Stale element in row {i}, retrying...");
                        i--; // retry the same row
                        continue;
                    }
                }



                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Advertisement Mapping Updated"), " failed..");

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                }
                Console.WriteLine("Test failed with error: " + errorMessage);
                test.Fail(errorMessage);
                expectedStatus = "Failed";
                throw;

            }
               
            
            }



        [TestMethod]
        public void DeleteAdv()
        {

            test = extent.CreateTest("Check the Delete Adv in Mapping Option");

            expectedStatus = "Passed";
            description = "Test case to check the Delete Adv in Mapping Option";

            // Initialize WebDriver
            Login loginHelper = new Login();
            driver = loginHelper.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            try
            {
                Console.WriteLine("Starting login process...");
                loginHelper.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful and dashboard loaded.");

                Console.WriteLine("Navigating to Client User List...");
                IWebElement menuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements")));
                menuBtn.Click();
                IWebElement submenuBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements3")));
                submenuBtn.Click();

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
                searchInput.SendKeys(SearchMappingData.SearchScheduleSuccess["Machine Id"]);
                searchInput.SendKeys(Keys.Enter);
                Thread.Sleep(1000);


                IWebElement lastcol = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//tbody/tr)[last()]/td[last()]")));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", lastcol);
                wait.Until(ExpectedConditions.ElementToBeClickable(lastcol)).Click();

                IWebElement delete = wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        By.XPath("//div[contains(@class,'mat-menu-panel')]//button[contains(text(),'Delete')]")
                    )
                );
                delete.Click();

                IWebElement confirm = wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("//mat-dialog-container//span[contains(text(),'Confirm')]")
                    )
                );
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", confirm);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", confirm);

                wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container"));
                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(snackbar.Text.Contains("Advertisement Mapping Deleted"), " failed..");

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                }
                Console.WriteLine("Test failed with error: " + errorMessage);
                test.Fail(errorMessage);
                expectedStatus = "Failed";
                throw;

            }
            }


       

        [TestCleanup]
        public void Cleanup()
        {
            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;
            string formattedTime = $"{timeTaken.TotalSeconds:F2}";
            try
            {
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error quitting driver: " + e.Message);
            }

            Console.WriteLine($"Test finished. Time taken: {formattedTime} seconds. Status: {expectedStatus}");

            testResult.WriteTestResults(TestContext, formattedTime, expectedStatus, errorMessage, description);
            extent.Flush();
        }


        private void ClickElementWhenReady(WebDriverWait wait, By locator)
        {
            wait.Until(driver =>
            {
                try
                {
                    // Wait until no overlay that blocks clicks
                    var overlays = driver.FindElements(By.CssSelector("div.overlay"));
                    if (overlays.Any(o => o.Displayed))
                    {
                        Console.WriteLine("Overlay present; waiting...");
                        return false;
                    }

                    var element = driver.FindElement(locator);

                    if (element != null && element.Displayed && element.Enabled)
                    {
                        element.Click();
                        Console.WriteLine($"Clicked element: {locator}");
                        return true;
                    }

                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"StaleElementReferenceException for {locator}; retrying...");
                    return false;
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"NoSuchElementException for {locator}; retrying...");
                    return false;
                }
                catch (ElementClickInterceptedException)
                {
                    Console.WriteLine($"ElementClickInterceptedException for {locator}; retrying...");
                    return false;
                }
            });
        }
        private void WaitForTableToLoad(WebDriverWait wait)
        {
            Console.WriteLine("Waiting for table to load...");

            wait.Until(driver =>
            {
                try
                {
                    var tableRows = driver.FindElements(By.CssSelector("table tbody tr"));
                    if (tableRows.Count == 0)
                    {
                        Console.WriteLine("No rows found yet; waiting...");
                        return false;
                    }

                    // All rows must be displayed
                    foreach (var row in tableRows.ToList())
                    {
                        try
                        {
                            if (!row.Displayed)
                            {
                                Console.WriteLine("A row not displayed yet; waiting...");
                                return false;
                            }
                        }
                        catch (StaleElementReferenceException)
                        {
                            Console.WriteLine("Row became stale; will retry waiting for table...");
                            return false;
                        }
                    }

                    Console.WriteLine("Table loaded with rows displayed.");
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("StaleElementReferenceException while getting rows; retrying table load...");
                    return false;
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Table or rows not found; retrying table load...");
                    return false;
                }
            });
        }




    }
}
