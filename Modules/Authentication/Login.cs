using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using VMS_Phase1PortalAT.utls;
using VMS_Phase1PortalAT.utls.datas;


namespace VMS_Phase1PortalAT.Modules.Authentication
{
    [TestClass]
    public class Login
    {

            private Stopwatch stopwatch;
            private string expectedStatus;
            private string errorMessage;
            private string description;
            public TestContext TestContext { get; set; }

            WriteResultToCSV testResult = new WriteResultToCSV();

            private static ExtentReports extent;
            private static ExtentTest test;

            private  IWebDriver driver;  // ✅ Shared across entire execution
            private readonly setupData setup = new setupData();
            private const int MaxLoginRetries = 2;


        [TestInitialize]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            errorMessage = string.Empty;
            extent = ExtentManager.GetInstance();
            if (driver == null)
            {
                driver = new ChromeDriver();
                stopwatch = Stopwatch.StartNew();
            }
        }


        public void LoginSuccessCompanyAdmin()
            {
                // ✅ Ensure driver is initialized before any actions
                if (driver == null)
                {
                    driver = new ChromeDriver();
                }

                int attempt = 0;
                bool loginSuccess = false;

                while (attempt < MaxLoginRetries && !loginSuccess)
                {
                    try
                    {
                        Console.WriteLine($" Login attempt #{attempt + 1}");

                        if (setup == null || string.IsNullOrEmpty(setup.uri_prefix))
                            throw new NullReferenceException("Setup or URI prefix is not initialized.");
                        if (LoginData.CompanyAdminSuccess == null)
                            throw new NullReferenceException("LoginData.CompanyAdminSuccess is not initialized.");

                        string uriPrefix = setup.uri_prefix;
                        driver.Navigate().GoToUrl(uriPrefix + "/home/login");
                        driver.Manage().Window.Maximize();
                        loginSuccess = TryLogin(driver);

                        if (!loginSuccess)
                        {
                            Console.WriteLine(" Login attempt failed.");
                            attempt++;

                            if (attempt >= MaxLoginRetries)
                                throw new TimeoutException(" Login failed after maximum retries.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Login error: " + ex.Message);
                        attempt++;

                        if (attempt >= MaxLoginRetries)
                            throw new Exception("Login failed after retries", ex);
                    }
                }

                Console.WriteLine("Login successful and dashboard loaded.");
            }

            private bool TryLogin(IWebDriver driver)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                    IWebElement emailField = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("username")));
                    ScrollIntoView(driver, emailField);
                    emailField.Clear();
                    emailField.SendKeys(LoginData.CompanyAdminSuccess["Username"]);

                    IWebElement passwordField = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("password")));
                    ScrollIntoView(driver, passwordField);
                    passwordField.Clear();
                    passwordField.SendKeys(LoginData.CompanyAdminSuccess["Password"]);

                    IWebElement loginButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-raised-button")));
                    loginButton.Click();

                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("menuItem-Machines")));

                    return true;
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine(" Login timeout: dashboard not found.");
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected login error: " + ex.Message);
                    return false;
                }
            }

            private void ScrollIntoView(IWebDriver driver, IWebElement element)
            {
                try
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("⚠ Could not scroll to element: " + ex.Message);
                }
            }

            public WebDriver getdriver()
            {
                if (driver == null)
                {
                    driver = new ChromeDriver(); // ✅ This will be hit only if LoginSuccessCompanyAdmin() hasn't run yet
                }

                return (WebDriver)driver;
            }
        



        [TestMethod]
        public void LoginSuccesssCompanyAdmin()
        {

            test = extent.CreateTest("Login Success Company Admin");
            test.Info("add valid data in CompanyAdminSuccess in LoginData file before run");
            
            expectedStatus = "Passed";
            description = "add valid data in CompanyAdminSuccess in LoginData file before run";
            try
            {
                driver.Manage().Window.Maximize();
                string uriPrrefix = setup.uri_prefix;
                driver.Navigate().GoToUrl(uriPrrefix + "/home/login");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement username = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("username")));
                IWebElement password = driver.FindElement(By.Name("password"));
                IWebElement login = driver.FindElement(By.ClassName("mat-raised-button"));
                username.SendKeys(LoginData.CompanyAdminSuccess["Username"]);
                password.SendKeys(LoginData.CompanyAdminSuccess["Password"]);
                login.Click();
                Thread.Sleep(1000);
                bool isLoggedIn = wait.Until(driv => driv.Url.Contains("portal"));
                Assert.IsTrue(isLoggedIn, "Login Failed At company admin");
                test.Pass();            
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
        public void LoginFailedCompanyAdmin()
        {
            test = extent.CreateTest("Login Failed Company Admin");
            

            expectedStatus = "Passed";
            description = "add invalid data in CompanyAdminFailure in LoginData file before run";
            try
            {
                driver.Manage().Window.Maximize();
                string uriPrrefix = setup.uri_prefix;
                driver.Navigate().GoToUrl(uriPrrefix + "/home/login");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement username = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("username")));
                IWebElement password = driver.FindElement(By.Name("password"));
                IWebElement login = driver.FindElement(By.ClassName("mat-raised-button"));

                username.SendKeys(LoginData.CompanyAdminFailure["Username"]);
                password.SendKeys(LoginData.CompanyAdminFailure["Password"]);
                bool isDisabled = login.GetAttribute("disabled") != null && login.GetAttribute("disabled").Equals("true");
                login.Click();
                bool isLoggedIn = wait.Until(driv => driv.Url.Contains("home/login"));
                Assert.IsTrue(isLoggedIn, "Login Success");
                test.Pass();
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
        public void LoginSuccessCompanyUser()
        {
            test = extent.CreateTest("Login Success Company User");
           


            expectedStatus = "Passed";
            description = "add valid data in CompanyUserSuccess in LoginData file before run";
            try
            {
                driver.Manage().Window.Maximize();
                string uriPrrefix = setup.uri_prefix;
                driver.Navigate().GoToUrl(uriPrrefix + "/home/login");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement username = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("username")));
                IWebElement password = driver.FindElement(By.Name("password"));
                username.SendKeys(LoginData.CompanyUserSuccess["Username"]);
                password.SendKeys(LoginData.CompanyUserSuccess["Password"]);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-raised-button")));
                IWebElement login = driver.FindElement(By.ClassName("mat-raised-button"));
                login.Click();
                bool isLoggedIn = wait.Until(driv => driv.Url.Contains("home"));
                Assert.IsTrue(isLoggedIn, "Login Company User Failed");

                test.Pass();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;

                    test.Fail();
                }
                throw;
            }
        }

        [TestMethod]
        public void LoginFailureCompanyUser()
        {
            test = extent.CreateTest("Login Failure Company User");
            test.Info("add invalid data in CompanyUserFailure in LoginData file before run");


            expectedStatus = "Passed";
            description = "add invalid data in CompanyUserFailure in LoginData file before run";
            try
            {
                driver.Manage().Window.Maximize();
                string uriPrrefix = setup.uri_prefix;
                driver.Navigate().GoToUrl(uriPrrefix + "/home/login");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement username = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("username")));
                IWebElement password = driver.FindElement(By.Name("password"));
                IWebElement login = driver.FindElement(By.ClassName("mat-raised-button"));
                username.SendKeys(LoginData.CompanyUserFailure["Username"]);
                password.SendKeys(LoginData.CompanyUserFailure["Password"]);
                bool isDisabled = login.GetAttribute("disabled") != null && login.GetAttribute("disabled").Equals("true");
                login.Click();
                bool isLoggedIn = wait.Until(driv => driv.Url.Contains("/home/login"));
                Assert.IsTrue(isLoggedIn, "Login Company User Failed");

                test.Pass();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;

                    test.Fail();
                }
                throw;
            }
        }

        [TestMethod]
        public void LoginSuccessClientAdmin()
        {
            test = extent.CreateTest("Login Success Client Admin");
            test.Info("add valid data in ClientAdminSuccess in LoginData file before run");

            expectedStatus = "Passed";
            description = "add valid data in ClientAdminSuccess in LoginData file before run";
            try
            {
                driver.Manage().Window.Maximize();
                string uriPrrefix = setup.uri_prefix;
                driver.Navigate().GoToUrl(uriPrrefix + "/home/login");
                Thread.Sleep(2000);
                IWebElement tabList = driver.FindElement(By.ClassName("mat-tab-list"));
                IWebElement tabLabels = tabList.FindElement(By.ClassName("mat-tab-labels"));
                if (tabLabels.Text.Contains("Client"))
                {
                    IWebElement clientTab = tabList.FindElement(By.TagName("div"));
                    clientTab.Click();
                    Thread.Sleep(2000);
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    IWebElement username = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("username")));
                    username.SendKeys(LoginData.ClientAdminSuccess["Username"]);
                    bool clicked = false;
                    int attempts = 0;

                    while (!clicked && attempts < 2)
                    {
                        try
                        {

                            IWebElement password = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//input)[2]")));
                            password.SendKeys(LoginData.ClientAdminFailure["Password"]);
                            clicked = true;
                        }
                        catch (StaleElementReferenceException)
                        {
                            Thread.Sleep(2000); // Wait before retrying
                            attempts++;
                        }
                    }
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-raised-button")));
                    IWebElement login = driver.FindElement(By.ClassName("mat-raised-button"));
                    login.Click();
                    Thread.Sleep(1000);
                    bool isLoggedIn = wait.Until(driv => driv.Url.Contains("portal/home"));
                    Thread.Sleep(500);
                    Assert.IsTrue(isLoggedIn, "Login Client Admin Failed");

                    test.Pass();
                }
                else
                {
                    Console.WriteLine("Does not contain client.....");

                    test.Skip();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;

                    test.Fail();
                }
                throw;
            }

        }

        [TestMethod]
        public void LoginFailureClientAdmin()
        {
            test = extent.CreateTest("Login Failure Client Admin");
            test.Info("add valid data in ClientAdminSuccess in LoginData file before run");

            expectedStatus = "Passed";
            description = "add invalid data in ClientAdminFailure in LoginData file before run";
            try
            {
                driver.Manage().Window.Maximize();
                string uriPrrefix = setup.uri_prefix;
                driver.Navigate().GoToUrl(uriPrrefix + "/home/login");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-tab-list")));
                IWebElement tabList = driver.FindElement(By.ClassName("mat-tab-list"));

                IWebElement tabLabels = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-tab-labels")));
                if (tabLabels.Text.Contains("Client"))
                {
                    IWebElement clientTab = tabList.FindElement(By.TagName("div"));
                    Console.WriteLine(clientTab.TagName);
                    clientTab.Click();
                    Thread.Sleep(2000);
                    IWebElement username = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("username")));
                    username.SendKeys(LoginData.ClientAdminFailure["Username"]);
                    //
                    bool clicked = false;
                    int attempts = 0;

                    while (!clicked && attempts < 2)
                    {
                        try
                        {
                           
                            IWebElement password = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("(//input)[2]")));
                            password.SendKeys(LoginData.ClientAdminFailure["Password"]);
                            clicked=true;
                        }
                        catch (StaleElementReferenceException)
                        {
                            Thread.Sleep(2000); // Wait before retrying
                            attempts++;
                        }
                    }
                    //
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-raised-button")));
                    IWebElement login = driver.FindElement(By.ClassName("mat-raised-button"));
                    login.Click();
                    bool isLoggedIn = wait.Until(driv => driv.Url.Contains("/home/login"));
                    Assert.IsTrue(isLoggedIn, "Login Client Admin Success or btn is disabled");

                    test.Pass();
                }
                else
                {
                    Console.WriteLine("Does not contain client.....");

                    test.Skip();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;

                    test.Fail();
                }
                throw;
            }
        }

        [TestMethod]
        public void LoginClientUserBtnDisable()
        {
            test = extent.CreateTest("Login Client User BtnDisable");

            if (driver == null)
            {
                driver = new ChromeDriver();
            }

            expectedStatus = "Passed";
            description = "testcase will not fill all required fields";
            try
            {
                driver.Manage().Window.Maximize();
                string uriPrrefix = setup.uri_prefix;
                driver.Navigate().GoToUrl(uriPrrefix + "/home/login");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("mat-tab-list")));
                IWebElement tabList = driver.FindElement(By.ClassName("mat-tab-list"));
                IWebElement tabLabels = tabList.FindElement(By.ClassName("mat-tab-labels"));
                if (tabLabels.Text.Contains("Client"))
                {
                    IWebElement clientTab = tabList.FindElement(By.TagName("div"));
                    Console.WriteLine(clientTab.TagName);
                    clientTab.Click();
                    Thread.Sleep(2000);
                    Thread.Sleep(2000);
                    IWebElement username = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("username")));
                    IWebElement password = driver.FindElement(By.Name("password"));
                    IWebElement login = driver.FindElement(By.ClassName("mat-raised-button"));
                    username.SendKeys("a");
                    Assert.IsTrue(login.GetAttribute("disabled").Equals("true"));

                    test.Pass();                }
                else
                {
                    Console.WriteLine("Does not contain client.....");

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
        public void LoginCompanyUserBtnDisable()
        {
            test = extent.CreateTest("Login Company User BtnDisable");
           

            expectedStatus = "Passed";
            description = "testcase will not fill all required fields";
            if (driver == null)
            {
                driver = new ChromeDriver();
            }

            try
            {
                driver.Manage().Window.Maximize();
                string uriPrrefix = setup.uri_prefix;
                driver.Navigate().GoToUrl(uriPrrefix + "/home/login");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement username = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("username")));
                IWebElement password = driver.FindElement(By.Name("password"));
                IWebElement login = driver.FindElement(By.ClassName("mat-raised-button"));
                username.SendKeys("");
                Assert.IsTrue(login.GetAttribute("disabled").Equals("true"));

                test.Pass();
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
