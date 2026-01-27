using AventStack.ExtentReports;
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
using VMS_Phase1PortalAT.utls.datas.Products.ProductList;
using VMS_Phase1PortalAT.utls.datas.W.Transactions.RefillRequest;

namespace VMS_Phase1PortalAT.Modules.Products.Product_List.Branch_Level
{
    [TestClass]
    public class AddProduct
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
        //public void AddProductSuccess()
        //{

        //    test = extent.CreateTest("Validating Add Product Success");

        //    expectedStatus = "Passed";
        //    description = "test case to test add product. add valid data in addProductSuccess in AddBranchLevelProductData file";
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
        //            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products3")));
        //            IWebElement submenu = driver.FindElement(By.Id("menuItem-Products3"));
        //            if (submenu != null)
        //            {
        //                submenu.Click();
        //                IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
        //                if (add != null)
        //                {
        //                    add.Click();
        //                    IWebElement dialogBox = wait.Until(driv => driver.FindElement(By.TagName("mat-dialog-container")));
        //                    if (dialogBox != null)
        //                    {
        //                        wait.Until(drv => driver.FindElement(By.Name("name")));
        //                        IWebElement name = driver.FindElement(By.Name("name"));
        //                        IWebElement branch = dialogBox.FindElement(By.Name("branch"));
        //                        IWebElement category = driver.FindElement(By.XPath("//mat-select[@name='category']"));
        //                        IWebElement subcategory = driver.FindElement(By.XPath("//mat-select[@name='subcategory']"));
        //                        IWebElement brand = driver.FindElement(By.Name("brand"));
        //                        IWebElement hsnCode = driver.FindElement(By.Name("hsnCode"));
        //                        IWebElement customProductId = driver.FindElement(By.Name("customProductId"));
        //                        IWebElement addGST = driver.FindElement(By.Name("addGST"));
        //                        addGST.Click();
        //                        wait.Until(drv => driver.FindElement(By.Name("mrp")));
        //                        IWebElement mrp = driver.FindElement(By.Name("mrp"));
        //                        IWebElement cgst = driver.FindElement(By.Name("cgst"));
        //                        IWebElement sgst = driver.FindElement(By.Name("sgst"));
        //                        IWebElement utgst = driver.FindElement(By.Name("utgst"));
        //                        IWebElement cess = driver.FindElement(By.Name("cess"));
        //                        IWebElement taxablePriceCovS = driver.FindElement(By.Name("taxablePriceCovS"));
        //                        IWebElement taxablePriceCovUT = driver.FindElement(By.Name("taxablePriceCovUT"));
        //                        IWebElement fileUpload = driver.FindElement(By.Name("fileUpload"));
        //                        IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
        //                        name.SendKeys(AddBranchLevelProductData.addProductSuccess["name"]);
        //                        branch.SendKeys(AddBranchLevelProductData.addProductSuccess["branch"]);
        //                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
        //                        IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));
        //                        options[0].Click();
        //                        brand.Click();
        //                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
        //                        IList<IWebElement> brandOptions = driver.FindElements(By.TagName("mat-option"));
        //                        brandOptions[0].Click();
        //                        category.Click();
        //                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
        //                        IList<IWebElement> categoryOptions = driver.FindElements(By.TagName("mat-option"));
        //                        categoryOptions[0].Click();
        //                        subcategory.Click();
        //                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
        //                        IList<IWebElement> subcategoryOptions = driver.FindElements(By.TagName("mat-option"));
        //                        subcategoryOptions[0].Click();
        //                        hsnCode.SendKeys(AddBranchLevelProductData.addProductSuccess["hsnCode"]);
        //                        customProductId.SendKeys(AddBranchLevelProductData.addProductSuccess["customProductId"]);
        //                        mrp.SendKeys(AddBranchLevelProductData.addProductSuccess["mrp"]);
        //                        cgst.SendKeys(AddBranchLevelProductData.addProductSuccess["cgst"]);
        //                        sgst.SendKeys(AddBranchLevelProductData.addProductSuccess["sgst"]);
        //                        utgst.SendKeys(AddBranchLevelProductData.addProductSuccess["utgst"]);
        //                        cess.SendKeys(AddBranchLevelProductData.addProductSuccess["cess"]);
        //                        taxablePriceCovS.SendKeys(AddBranchLevelProductData.addProductSuccess["taxablePriceCovS"]);
        //                        taxablePriceCovUT.SendKeys(AddBranchLevelProductData.addProductSuccess["taxablePriceCovUT"]);
        //                        taxablePriceCovUT.SendKeys(Keys.Enter);
        //                        fileUpload.SendKeys(AddBranchLevelProductData.addProductSuccess["fileUpload"]);


        //                        // Click the Submit button
        //                        submit.Click();

        //                        // Short explicit wait to detect snackbar quickly
        //                        WebDriverWait shortWait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        //                        IWebElement snackbar = shortWait.Until(d =>
        //                        {
        //                            var element = d.FindElements(By.CssSelector(".mat-snack-bar-container"))
        //                                           .FirstOrDefault(e => e.Displayed);
        //                            return element; // will return null until found
        //                        });

        //                        // Validate the snackbar message
        //                        string expectedMessage = "Product Added";
        //                        bool isSuccess = snackbar.Text.Contains(expectedMessage);

        //                        Assert.IsTrue(isSuccess, "Snackbar did not display success message: " + expectedMessage);

        //                        test.Pass();


        //                    }


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
        public void AddProductSuccess()
        {
            test = extent.CreateTest("Validating Add Product Success");
            expectedStatus = "Passed";
            description = "Test adding product with valid data";

            Login login = new Login();
            driver = login.getdriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                login.LoginSuccessCompanyAdmin();
                Console.WriteLine("Login successful");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                test.Fail(errorMessage);
                throw;
            }

            try
            {
                // Navigate to Products submenu
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products"))).Click();
                Console.WriteLine("Clicked Products menu");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Products3"))).Click();
                Console.WriteLine("Clicked Products submenu");

                // Click Add button
                IWebElement addBtn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                addBtn.Click();
                Console.WriteLine("Clicked Add button");

                // Wait for dialog
                IWebElement dialogBox = wait.Until(d => d.FindElement(By.TagName("mat-dialog-container")));
                Console.WriteLine("Dialog opened");

                // Helper to fill text fields
                void FillText(By locator, string value, string fieldName)
                {
                    var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                    element.Clear();
                    element.SendKeys(value);
                    Console.WriteLine("Entered value in field: " + fieldName);
                }

                // Fill all text fields first
                FillText(By.Name("name"), AddBranchLevelProductData.addProductSuccess["name"], "name");
                FillText(By.Name("hsnCode"), AddBranchLevelProductData.addProductSuccess["hsnCode"], "hsnCode");
                FillText(By.Name("customProductId"), AddBranchLevelProductData.addProductSuccess["customProductId"], "customProductId");

                // File upload
                try
                {
                    IWebElement fileUpload = dialogBox.FindElement(By.CssSelector("input[type='file']"));
                    fileUpload.SendKeys(AddBranchLevelProductData.addProductSuccess["fileUpload"]);
                    Console.WriteLine("File uploaded successfully");
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("File upload input not found, skipping");
                }

                // Click Add GST checkbox
                IWebElement addGST = dialogBox.FindElement(By.Name("addGST"));
                addGST.Click();
                Console.WriteLine("Clicked Add GST checkbox");

                // Fill GST fields
                FillText(By.Name("mrp"), AddBranchLevelProductData.addProductSuccess["mrp"], "mrp");
                FillText(By.Name("cgst"), AddBranchLevelProductData.addProductSuccess["cgst"], "cgst");
                FillText(By.Name("sgst"), AddBranchLevelProductData.addProductSuccess["sgst"], "sgst");
                FillText(By.Name("utgst"), AddBranchLevelProductData.addProductSuccess["utgst"], "utgst");
                FillText(By.Name("cess"), AddBranchLevelProductData.addProductSuccess["cess"], "cess");
                FillText(By.Name("taxablePriceCovS"), AddBranchLevelProductData.addProductSuccess["taxablePriceCovS"], "taxablePriceCovS");
                FillText(By.Name("taxablePriceCovUT"), AddBranchLevelProductData.addProductSuccess["taxablePriceCovUT"], "taxablePriceCovUT");

                IWebElement taxablePriceCovUT = dialogBox.FindElement(By.Name("taxablePriceCovUT"));
                taxablePriceCovUT.SendKeys(Keys.Enter);
                Console.WriteLine("Pressed Enter on taxablePriceCovUT");

                // --- Branch autocomplete handling ---
                IWebElement branchInput = dialogBox.FindElement(By.Name("branch"));
                branchInput.SendKeys(AddBranchLevelProductData.addProductSuccess["branch"]);
                Console.WriteLine("Entered value in branch input");

                IWebElement branchPanel = wait.Until(d =>
                {
                    var panel = d.FindElements(By.CssSelector("div.mat-autocomplete-panel"))
                                 .FirstOrDefault(p => p.Displayed);
                    return panel;
                });

                branchPanel.FindElements(By.TagName("mat-option"))[0].Click();
                Console.WriteLine("Selected first option in branch");

                // Helper to select first mat-option in overlay (always re-find the mat-select)
                void SelectMatOption(By matSelectLocator, string name)
                {
                    IWebElement matSelect = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(matSelectLocator));
                    matSelect.Click();
                    Console.WriteLine("Clicked mat-select: " + name);

                    IWebElement panel = wait.Until(d => d.FindElement(By.CssSelector("div.mat-select-panel")));
                    var options = panel.FindElements(By.TagName("mat-option"));
                    options[0].Click();
                    Console.WriteLine("Selected first option in: " + name);

                    // Small wait for Angular to rerender
                    Thread.Sleep(500);
                }

                // Select brand → category → subcategory
                SelectMatOption(By.Name("brand"), "brand");
                SelectMatOption(By.Name("category"), "category");
                SelectMatOption(By.Name("subcategory"), "subcategory");

                // Click submit
                IWebElement submit = dialogBox.FindElement(By.CssSelector("button.mat-raised-button"));
                submit.Click();
                Console.WriteLine("Clicked Submit");

                // Wait for snackbar
                WebDriverWait shortWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IWebElement snackbar = shortWait.Until(d =>
                {
                    var el = d.FindElements(By.CssSelector(".mat-snack-bar-container")).FirstOrDefault(e => e.Displayed);
                    return el;
                });

                string expectedMessage = "Product Added";
                Assert.IsTrue(snackbar.Text.Contains(expectedMessage), "Snackbar did not display success message: " + expectedMessage);
                test.Pass();
                Console.WriteLine("Product added successfully, snackbar detected.");
            }
            catch (Exception ex)
            {
                errorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("Test failed: " + errorMessage);
                test.Fail(errorMessage);
                throw;
            }
        }








       
        public void AddRequestBtnDisable()
        {
            test = extent.CreateTest("Validating Add Request BtnDisable");

            expectedStatus = "Passed";
            description = "test case to test add product. add valid data in addRequestBtnDisable in AddBranchLevelProductData file";
            Login LoginSuccess = new Login();
            driver = LoginSuccess.getdriver();
            LoginSuccess.LoginSuccessCompanyAdmin();
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
                    IWebElement add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-fab")));
                    if (add != null)
                    {
                        add.Click();
                        Thread.Sleep(1000);
                        IWebElement machineId = driver.FindElement(By.Name("machineIds"));
                        machineId.SendKeys(AddRefillRequestData.addRequestBtnDisable["machineIds"]);
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> options = driver.FindElements(By.TagName("mat-option"));

                        if (options[0].Text.Contains(AddRefillRequestData.addRequestBtnDisable["machineIds"]))
                        {
                            options[0].Click();
                        }

                        IWebElement next = driver.FindElement(By.ClassName("mat-raised-button"));
                        if (next.Enabled)
                        {
                            next.Click();
                            Thread.Sleep(1000);
                            IWebElement submit = driver.FindElement(By.ClassName("mat-raised-button"));
                            Assert.IsTrue(submit.GetAttribute("disabled").Equals("true"));

                            test.Pass();    
                        }
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
