using AventStack.ExtentReports;
using FlaUI.Core.Input;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V126.Preload;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VMS_Phase1PortalAT.Modules.Authentication;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;

namespace VMS_Phase1PortalAT.Modules.Machines.MachineList
{
    [TestClass]
    public class UnMerge
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
        public void UnMergeSuccess()
        {
            test = extent.CreateTest("Validating merge the slots");
            expectedStatus = "Passed";
            description = "Test case for unmerging slots";

            Login login = new Login();
            driver = login.getdriver();

            try
            {
                login.LoginSuccessCompanyAdmin();
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message;
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));



            void SafeClick(By locator)
            {
                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        // Always re-fetch the element
                        IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                        Console.WriteLine(element.Enabled + element.Text);
                        // 1️⃣ Try normal Selenium click
                        try
                        {
                            element.Click();
                            return; // success
                        }
                        catch (Exception)
                        {
                            // Do nothing → fallback to JS click
                        }

                        // 2️⃣ JS fallback click
                        ((IJavaScriptExecutor)driver)
                            .ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);

                        ((IJavaScriptExecutor)driver)
                            .ExecuteScript("arguments[0].click();", element);

                        return; // JS click success
                    }
                    catch (StaleElementReferenceException)
                    {
                        Thread.Sleep(500); // retry loop
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(500); // retry loop
                    }
                    catch (InvalidOperationException)
                    {
                        Thread.Sleep(500); // overlay/animation
                    }
                }

                throw new Exception("Could not click element: " + locator);
            }




            void SafeRowClick(IWebDriver driver, string clickXPath, string checkXPath)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                int attempt = 0;
                Console.WriteLine("-------------------------------------------------------");
                while (attempt < 9)
                {
                    attempt++;
                    Console.WriteLine($"Attempt {attempt}");

                    try
                    {
                        // ⭐ STEP 1 — Try to safely fetch CHECK element
                        IWebElement checkElement = null;

                        try
                        {
                            checkElement = driver.FindElement(By.XPath(checkXPath));
                            Console.WriteLine("Check element FOUND in DOM.");

                            // If element exists but not visible yet → wait for visibility
                            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(checkXPath)));

                            // Now element exists & visible — check if interactable
                            if (checkElement.Displayed && checkElement.Enabled)
                            {
                                Console.WriteLine("Check element is visible & interactable → RETURN (NO CLICK)");
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Check element exists but NOT interactable → waiting...");
                                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(checkXPath)));
                                Console.WriteLine("Check element now interactable → RETURN (NO CLICK)");
                                return;
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            Console.WriteLine("Check element NOT found → Need to click.");
                        }

                        // ⭐ STEP 2 — Check element NOT found → Now fetch click element
                        IWebElement elementToClick = null;

                        try
                        {
                            elementToClick = wait.Until(
                                ExpectedConditions.ElementToBeClickable(By.XPath(clickXPath))
                            );
                        }
                        catch
                        {
                            Console.WriteLine("Click element not ready → retry...");
                            Thread.Sleep(300);
                            continue;
                        }

                        // Click
                        try
                        {
                            elementToClick.Click();
                            Console.WriteLine("Clicked normally");
                        }
                        catch
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", elementToClick);
                            Console.WriteLine("Clicked via JS fallback");
                        }

                        // ⭐ STEP 3 — After clicking, wait for CHECK element again
                        try
                        {
                            IWebElement checkAfterClick = wait.Until(
                                ExpectedConditions.ElementIsVisible(By.XPath(checkXPath))
                            );

                            // Ensure interactable
                            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(checkXPath)));

                            Console.WriteLine("Check element became interactable after clicking → RETURN");
                            return;
                        }
                        catch
                        {
                            Console.WriteLine("Check element still not found after click.");
                        }
                    }
                    catch (Exception ex) when (
                        ex is StaleElementReferenceException ||
                        ex is ElementClickInterceptedException ||
                        ex is InvalidOperationException
                    )
                    {
                        Console.WriteLine("Recovered from: " + ex.GetType().Name);
                        Thread.Sleep(300);
                    }

                    Thread.Sleep(250);
                }

                Console.WriteLine(" Max attempts reached. SafeRowClick failed.");
            }









            //    for (int attempt = 0; attempt < 10; attempt++)
            //{
            //    try
            //    {
            //        // Find element to click
            //        IWebElement elementToClick = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(clickXPath)));

            //        // Try normal click first
            //        try
            //        {
            //            elementToClick.Click();
            //                Console.WriteLine(attempt);
            //                return;
            //        }
            //        catch
            //        {
            //            // Fallback to JS click if normal click fails
            //            ((IJavaScriptExecutor)driver).ExecuteScript(
            //                "arguments[0].scrollIntoView({block:'center'}); arguments[0].click();",
            //                elementToClick
            //            );
            //                return;
            //        }

            //        // Wait/check for next element after click
            //      //  wait.Until(ExpectedConditions.ElementExists(By.XPath(checkXPath)));

            //        // Success → exit method
            //        //return;
            //    }
            //    catch (StaleElementReferenceException) { Thread.Sleep(300); }
            //    catch (ElementClickInterceptedException) { Thread.Sleep(300); }
            //    catch (InvalidOperationException) { Thread.Sleep(300); }
            //}

            //throw new Exception($"SafeRowClick failed for clickXPath: {clickXPath}, checkXPath: {checkXPath}");







            IWebElement SafeElement(By locator)
            {
                return wait.Until(ExpectedConditions.ElementExists(locator));
            }





            try
            {
                // OPEN menu → Machines
                SafeClick(By.Id("menuItem-Machines"));

                // submenu → Machines0
                SafeClick(By.Id("menuItem-Machines0"));

                //table//tr[1]/td[last()]

                // TABLE BODY
                IWebElement table = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
                IList<IWebElement> rows = table.FindElements(By.TagName("tr"));

                if (rows.Count == 0)
                {
                    test.Skip();
                    return;
                }

                IWebElement actionColumn = rows[0].FindElements(By.TagName("td")).Last();
                actionColumn.Click();
                // CLICK last column
                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", actionColumn);

                // MAT MENU
                IWebElement matMenu = SafeElement(By.ClassName("mat-menu-panel"));
                IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));

                foreach (var item in menuItems)
                {
                    if (item.Text.Contains("View Details"))
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", item);
                        break;
                    }
                }

                Thread.Sleep(1500);

                IWebElement rowHolder = SafeElement(By.ClassName("rowHolder2"));
                IList<IWebElement> buttons = rowHolder.FindElements(By.TagName("button"));

                // First button (Unmerge)
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", buttons[0]);

                Thread.Sleep(600);

                // dialog
                IWebElement dialogElement = SafeElement(By.TagName("mat-dialog-container"));
                Console.WriteLine();

                // SELECT ROW DROPDOWN
                SafeRowClick(driver, "//mat-select[contains(@name,'selectedSlotRowData')]", "//mat-select[contains(@name,'selectedSlotRowData')]/following::mat-option");
                Thread.Sleep(350);

                SafeRowClick(driver, "(//mat-option)[1]", "//mat-select[contains(@name,'slotTobemergerdto')]");
                Thread.Sleep(350);

                SafeRowClick(driver, "//mat-select[contains(@name,'slotTobemergerdto')]", "//mat-select[contains(@name,'slotTobemergerdto')]/following::mat-option");
                Thread.Sleep(350);

                //wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("mat-option")));
                //foreach (var option in driver.FindElements(By.TagName("mat-option")))
                //{
                //    option.Click();
                //    //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", option);
                //    break;
                //}

                // SELECT SLOT

                //SafeRowClick(driver, "//mat-select[contains(@name,'slotTobemergerdto')]", "mat-option");
                
                wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("mat-option")));
                IList<IWebElement> slotOptions = driver.FindElements(By.TagName("mat-option"));

                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", slotOptions[0]);
                slotOptions[0].Click();

                Thread.Sleep(800);

                // SUBMIT
                IWebElement submit = dialogElement.FindElement(By.ClassName("mat-raised-button"));
                submit.Click();
                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", submit);

                Thread.Sleep(1000);
                // SNACKBAR CHECK
                IWebElement snackbar = wait.Until(
                    drv => drv.FindElement(By.CssSelector(".mat-snack-bar-container"))
                );

                bool success = snackbar.Text.Contains("Unmerged");

                Console.WriteLine(snackbar.Text);
                Assert.IsTrue(success, "Unmerge operation failed.");

                test.Pass();
            }
            catch (Exception ex)
            {
                test.Fail(ex.Message);
                throw;
            }
        }

       








        [TestMethod]
        public void UnMergeBtnDisable()
        {
            test = extent.CreateTest("Validating unmerge slot button disable with invalid data");

            expectedStatus = "Passed";
            description = "test case to test unmerge slot button disable with invalid data";
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            LoginSuccess.LoginSuccessCompanyAdmin();

            IWebElement menu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines")));
            if (menu != null)
            {
                menu.Click();

                IWebElement submenu = wait.Until(drv => driver.FindElement(By.Id("menuItem-Machines0")));
                if (submenu != null)
                {
                    submenu.Click();

                    IWebElement table = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    IList<IWebElement> data = table.FindElements(By.TagName("tr"));
                    if (data.Count > 0)
                    {
                        IWebElement firstRow = data[0];
                        IList<IWebElement> columns = firstRow.FindElements(By.TagName("td"));
                        IWebElement lastColumn = columns[columns.Count - 1];

                        lastColumn.Click();
                        IWebElement matMenu = wait.Until(drv => driver.FindElement(By.Id("mat-menu-panel-0")));
                        IList<IWebElement> menuItems = matMenu.FindElements(By.TagName("button"));


                        foreach (var item in menuItems)
                        {
                            if (item.Text.Contains("View Details"))
                            {
                                item.Click();
                                break;
                            }
                        }
                        Thread.Sleep(1000);
                        IWebElement rowHolder = wait.Until(ExpectedConditions.ElementExists(By.ClassName("rowHolder2")));
                        IList<IWebElement> buttons = rowHolder.FindElements(By.TagName("button"));
                        foreach (var item in buttons)
                        {
                            if (item.Text.Contains("link_off"))
                            {
                                item.Click();
                                break;
                            }
                        }
                        IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));

                        IWebElement submit = dialogElement.FindElement(By.ClassName("mat-raised-button"));
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
