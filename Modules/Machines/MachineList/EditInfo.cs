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

namespace VMS_Phase1PortalAT.Modules.Machines.MachineList
{
    [TestClass]
    public class EditInfo
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
        public void EditInfoSuccess()
        {
            test = extent.CreateTest("Validating  edit info for machine");

            expectedStatus = "Passed";
            description = "test case to test edit info for machine.add valid datas in editInfoSuccess in EditInfoData file before run";
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
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
                        IWebElement divEdit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("wideItem")));
                        IWebElement editIcon = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("edit_icon")));
                        Console.WriteLine($"{editIcon.Text}");
                        editIcon.Click();

                        IWebElement serialNumber = driver.FindElement(By.Name("serialNumber"));
                        IWebElement drange = driver.FindElement(By.Name("drange"));
                        IWebElement companyName = driver.FindElement(By.Name("companyName"));
                        IWebElement contactNo = driver.FindElement(By.Name("contactNo"));
                        IWebElement gstNo = driver.FindElement(By.Name("gstNo"));
                        IWebElement email = driver.FindElement(By.Name("email"));
                        IWebElement address = driver.FindElement(By.Name("address"));
                        IWebElement companyAddress = driver.FindElement(By.Name("companyAddress"));
                        IWebElement submit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Save')]")));
                        serialNumber.Clear();
                        serialNumber.SendKeys(EditInfoData.editInfoSuccess["serialNumber"]);
                        drange.Clear();
                        drange.SendKeys(EditInfoData.editInfoSuccess["drange"]);
                        companyName.Clear();
                        companyName.SendKeys(EditInfoData.editInfoSuccess["companyName"]);
                        contactNo.Clear();
                        contactNo.SendKeys(EditInfoData.editInfoSuccess["contactNo"]);
                        gstNo.Clear();
                        gstNo.SendKeys(EditInfoData.editInfoSuccess["gstNo"]);
                        email.Clear();
                        email.SendKeys(EditInfoData.editInfoSuccess["email"]);
                        address.Clear();
                        address.SendKeys(EditInfoData.editInfoSuccess["address"]);
                        companyAddress.Clear();
                        companyAddress.SendKeys(EditInfoData.editInfoSuccess["companyAddress"]);
                        contactNo.Clear();
                        contactNo.SendKeys(EditInfoData.editInfoSuccess["contactNo"]);


                        IWebElement selectSupervisor = driver.FindElement(By.Name("supervisor"));
                        selectSupervisor.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                        IList<IWebElement> supervisorOptions = driver.FindElements(By.TagName("mat-option"));
                        supervisorOptions[1].Click();

                        IWebElement paymentSelect = driver.FindElement(By.Name("paymentOptions"));
                        paymentSelect.Click();
                        driver.FindElement(By.Id("UPI Only")).Click();


                        IWebElement directRefillOption = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("directRefill")));
                        directRefillOption.Click();
                        submit.Click();
                        string title = "Machine Info Updated";
                        IWebElement snackbar = wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container")));
                        bool isSuccess = false;
                        if (snackbar.Text.Contains(title))
                        {
                            isSuccess = true;

                        }
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
        public void EditInfoBtnDisable()
        {
            test = extent.CreateTest("Validating editInfo without any fields");

            expectedStatus = "Passed";
            description = "test case to test editInfo without any fields";
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
             WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
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
                        IWebElement divEdit = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("wideItem")));
                        IWebElement editIcon = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("edit_icon")));
                        Console.WriteLine($"{editIcon.Text}");
                        editIcon.Click();

                        IWebElement serialNumber = driver.FindElement(By.Name("serialNumber"));
                        IWebElement drange = driver.FindElement(By.Name("drange"));
                        IWebElement companyName = driver.FindElement(By.Name("companyName"));
                        IWebElement contactNo = driver.FindElement(By.Name("contactNo"));
                        IWebElement gstNo = driver.FindElement(By.Name("gstNo"));
                        IWebElement email = driver.FindElement(By.Name("email"));
                        IWebElement address = driver.FindElement(By.Name("address"));
                        IWebElement companyAddress = driver.FindElement(By.Name("companyAddress"));

                        Thread.Sleep(1000);
                        serialNumber.Clear();
                        drange.Clear();
                        Thread.Sleep(1000);
                        companyName.Clear();
                        contactNo.Clear();
                        gstNo.Clear();
                        gstNo.SendKeys(EditInfoData.editInfoSuccess["igstNo"]);
                        email.Clear();
                        address.Clear();
                        companyAddress.Clear();
                        Thread.Sleep(5000);
                          /*  IWebElement checkbox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-checkbox")));
                            IWebElement disabledAutoRefill = checkbox.FindElement(By.Name("directRefill"));
                            Thread.Sleep(1000);
                            if (disabledAutoRefill.GetAttribute("aria-checked")=="true")
                            {
                                Thread.Sleep(4000);
                                Console.WriteLine("999");
                                disabledAutoRefill.Click();
                            }*/
                            /*IWebElement checkbox2 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.TagName("mat-checkbox")));
                            IWebElement directRefillOption = checkbox2.FindElement(By.Name("disabledAutoRefill"));
                            if (directRefillOption.GetAttribute("aria-checked") == "true")
                            {
                                directRefillOption.Click();
                            }*/
                            string inputValue = serialNumber.GetAttribute("value");
                        Console.WriteLine(inputValue + " value");
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("mat-dialog-container")));
                        IWebElement dialogElement = driver.FindElement(By.TagName("mat-dialog-container"));
                        Thread.Sleep(5000);
                        IList<IWebElement> dialogBtn = dialogElement.FindElements(By.TagName("button"));

                        foreach (var item in dialogBtn)
                        {
                            Console.WriteLine(item.Text + " test");
                            if (item.Text.Contains("Save"))
                            {
                                Thread.Sleep(5000);
                                item.Click();
                                Assert.IsTrue(item.GetAttribute("disabled").Equals("true"));
                                    test.Pass();
                                break;
                            }
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
