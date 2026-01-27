using AventStack.ExtentReports;
using DocumentFormat.OpenXml.Presentation;
using FlaUI.Core.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
using VMS_Phase1PortalAT.utls.datas.Advertisement.List;
using WindowsInput;
using WindowsInput.Native;

namespace VMS_Phase1PortalAT.Modules.Advertisement.List
{

  [TestClass]
  public class AddList
    {

        private Stopwatch stopwatch;
        private string expectedStatus;
        private string errorMessage;
        private string description;
        private readonly string systemName = Environment.UserName;
        private string downloadPath;
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
            downloadPath = setupDatas.downloadPath;
            downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", true);

            extent = ExtentManager.GetInstance();


        }
        [TestMethod]
        public void AddListSuccess()
        {
            test = extent.CreateTest("test case to test Add List"); expectedStatus = "Passed"; description = "test case to test Add List"; Login LoginSuccess = new Login(); driver = LoginSuccess.getdriver(); try { LoginSuccess.LoginSuccessCompanyAdmin(); } catch (Exception ex) { if (ex.InnerException != null) { errorMessage = ex.InnerException.Message; } }
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements"))); IWebElement menu = driver.FindElement(By.Id("menuItem-Advertisements")); if (menu != null)
                {
                    menu.Click(); wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("menuItem-Advertisements0"))); IWebElement submenu = driver.FindElement(By.Id("menuItem-Advertisements0")); if (submenu != null)
                    {
                        submenu.Click();
                        Thread.Sleep(1500);
                        wait.Until(driver => driver.FindElements(By.TagName("tr")).Count > 0); 
                        IWebElement Add = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-icon[contains(text(),'add')]"))); 
                        Add.Click(); 
                        Thread.Sleep(200); 
                        IWebElement title = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-label[contains(text(),'Advertisement Title')]/ancestor::mat-form-field//input"))); 
                        title.SendKeys(AddListData.AddListSuccess["Title"]); 
                        Thread.Sleep(200); 
                        IWebElement Contentype = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-select[@name='contentType']"))); 
                        Contentype.Click(); 
                        IList<IWebElement> Contentoptions = wait.Until(driver => { var els = driver.FindElements(By.XPath("//mat-option")); return els.Count > 0 ? els : throw new NoSuchElementException(); }); 
                        foreach (IWebElement a in Contentoptions) { if (a.Text.Contains(AddListData.AddListSuccess["Content"])) { a.Click(); break; } }
                        Thread.Sleep(1500);

                        IWebElement clientInput = wait.Until(driver =>
                        {
                            var el = driver.FindElement(By.XPath("//mat-label[contains(text(),'Client')]/ancestor::mat-form-field//input"));
                            if (!el.Displayed || !el.Enabled)
                                throw new NoSuchElementException("Client input is not visible or enabled yet");

                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", el);
                            return el; 
                        });

                        try
                        {
                            clientInput.Click(); // normal Selenium click
                        }
                        catch (ElementClickInterceptedException)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", clientInput);
                        }





                        //IWebElement Client = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//mat-label[contains(text(),'Client')]/ancestor::mat-form-field//input"))); 
                        //Client.Click(); 
                        IList<IWebElement> Clientoptions = wait.Until(driver => { var els = driver.FindElements(By.XPath("//mat-option")); return els.Count > 0 ? els : throw new NoSuchElementException(); }); 
                        foreach (IWebElement a in Clientoptions) { if (a.Text.Contains(AddListData.AddListSuccess["Client"])) { a.Click(); break; } }
                        Thread.Sleep(2000); 
                        IWebElement AdvertisementFile = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Select File')]"))); 
                        AdvertisementFile.Click(); 
                        Thread.Sleep(500); 
                        var sim = new InputSimulator(); 
                        sim.Keyboard.TextEntry(AddListData.AddListSuccess["Adv File"]); 
                        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN); // RETURN is same as ENTER
                        Console.WriteLine("Uploaded a file"); 
                        Thread.Sleep(1000); 
                        IWebElement save = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Save')]"))); 
                        save.Click(); 
                        wait.Until(drv => driver.FindElement(By.CssSelector(".mat-snack-bar-container"))); 
                        IWebElement snackbar = driver.FindElement(By.CssSelector(".mat-snack-bar-container")); 
                        Console.WriteLine(snackbar.Text); 
                        Assert.IsTrue(snackbar.Text.Contains("Advertisement Added"), " failed.."); } 
                } 
            } 
            catch (Exception ex) 
            { 
                errorMessage = ex.Message; 
                if (ex.InnerException != null) 
                { errorMessage = ex.InnerException.Message; test.Fail(errorMessage); } throw; 
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
