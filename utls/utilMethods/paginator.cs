using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VMS_Phase1PortalAT.utls.utilMethods
{

    public class paginator
    {
        public void PaginatorSuccess(IWebDriver driver, string menuData, string submenuData)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            Console.WriteLine($"[INFO] Locating menu: {menuData}");
            IWebElement menu = driver.FindElement(By.Id(menuData));
            ClickElementWithRetryAndVerify(driver, menu, "Menu", By.Id(submenuData));  // Waits for submenu to appear

            Console.WriteLine($"[INFO] Locating submenu: {submenuData}");
            IWebElement submenu = driver.FindElement(By.Id(submenuData));
            ClickElementWithRetryAndVerify(driver, submenu, "Submenu", By.ClassName("mat-paginator-range-label"));  // Waits for paginator label

            Thread.Sleep(2000); // Allow data to load

            IWebElement paginatorRange = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("mat-paginator-range-label")));
            string totalText = paginatorRange.Text.Split(new string[] { "of" }, StringSplitOptions.None)[1].Trim();
            int totalNumber = int.Parse(totalText);
            Console.WriteLine($"[INFO] Total items found: {totalNumber}");

            IWebElement paginatorSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("mat-paginator-page-size-select")));
            paginatorSelect.Click();
            Thread.Sleep(1500);
            // Safe wait until paginator options are present, displayed, and list is not empty
            IList<IWebElement> paginatorOptions = wait.Until(driver =>
            {
                try
                {
                    var list = driver.FindElements(By.TagName("mat-option"))
                                     .Where(o => o.Displayed)
                                     .ToList();

                    return list.Count > 0 ? list : null;  // Keep waiting
                }
                catch
                {
                    return null;  // In case of stale or DOM refresh → retry
                }
            });

            // Final safety guard — should never fire, but prevents crash
            if (paginatorOptions == null || paginatorOptions.Count == 0)
            {
                Console.WriteLine("[ERROR] No paginator options found even after wait.");
                return;   // Or throw, depending on requirement
            }

            // Log count
            Console.WriteLine($"[INFO] Found {paginatorOptions.Count} paginator option(s).");


            // Log count
            Console.WriteLine($"[INFO] Total paginator options found: {paginatorOptions.Count}");

            // Safely access index 0
            string paginatorText = paginatorOptions[0].Text.Trim();

            if (!int.TryParse(paginatorText, out int paginatorNumberSel))
                throw new Exception("Paginator option is not a valid number: " + paginatorText);

            Console.WriteLine($"[INFO] Selecting page size: {paginatorNumberSel}");

            // Try clicking — with normal click + JS fallback
            try
            {
                paginatorOptions[0].Click();
            }
            catch
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", paginatorOptions[0]);
            }

            Thread.Sleep(3000); // Wait for table update

            IWebElement table = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
            IList<IWebElement> rows = table.FindElements(By.TagName("tr"));
            Console.WriteLine($"[INFO] Rows displayed: {rows.Count}");

            Assert.IsTrue(rows.Count <= paginatorNumberSel, "Row count exceeds selected page size");

            IWebElement nextBtn = driver.FindElement(By.ClassName("mat-paginator-navigation-next"));
            bool isDisabled = nextBtn.GetAttribute("disabled") != null;

            if (totalNumber > paginatorNumberSel)
            {
                Console.WriteLine("[INFO] Verifying 'Next' button is enabled.");
                Assert.IsFalse(isDisabled, "'Next' button should be enabled.");
            }
            else
            {
                Console.WriteLine("[INFO] Verifying 'Next' button is disabled.");
                Assert.IsTrue(isDisabled, "'Next' button should be disabled.");
            }

            Console.WriteLine("[SUCCESS] PaginatorSuccess completed.");
        }


        public void PaginatorFailure(IWebDriver driver, string menuData, string submenuData)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;


            Console.WriteLine("Checking whether the Paginator values are numeric");
            // ------------------ Click Menu ------------------
            Console.WriteLine($"[INFO] Locating menu: {menuData}");
            IWebElement menu = driver.FindElement(By.Id(menuData));
            ClickElementWithRetryAndVerify(driver, menu, "Menu", By.Id(submenuData));

            // ------------------ Click Submenu ------------------
            Console.WriteLine($"[INFO] Locating submenu: {submenuData}");
            IWebElement submenu = driver.FindElement(By.Id(submenuData));
            ClickElementWithRetryAndVerify(driver, submenu, "Submenu", By.ClassName("mat-paginator-range-label"));

            // ------------------ Wait for paginator range ------------------
            Thread.Sleep(700);
            bool paginatorExists = driver.FindElements(By.ClassName("mat-paginator-range-label")).Count > 0;
            if (!paginatorExists)
                Assert.Fail("Paginator does not exist on the page.");

            IWebElement paginatorRange = wait.Until(
                ExpectedConditions.ElementIsVisible(By.ClassName("mat-paginator-range-label")));

            string totalText = paginatorRange.Text.Split(new string[] { "of" }, StringSplitOptions.None)[1].Trim();
            int totalNumber = int.Parse(totalText);
            Console.WriteLine($"[INFO] Total items: {totalNumber}");

            // ------------------ Paginator Dropdown ------------------
            By paginatorSelectLocator = By.XPath("//mat-select[contains(@aria-label,'Items per page')]");
            By optionLocator = By.CssSelector("div.cdk-overlay-container mat-option .mat-option-text");

            // ------------------ Validate selected value is numeric ------------------
            string dropdownText = driver.FindElement(paginatorSelectLocator).Text.Trim();
            Console.WriteLine($"[INFO] Validating paginator selected value: {dropdownText}");
            if (!int.TryParse(dropdownText, out _))
                Assert.Fail($"Paginator selected value is not numeric: {dropdownText}");
            Console.WriteLine($"[INFO] Paginator selected value is numeric: {dropdownText}");

            // ------------------ Open dropdown with retries and refetch ------------------
            bool dropdownOpened = false;
            for (int attempt = 1; attempt <= 5; attempt++)
            {
                try
                {
                    // Refetch element each time to avoid stale reference
                    IWebElement paginatorSelect = driver.FindElement(paginatorSelectLocator);

                    // Scroll into view
                    js.ExecuteScript("arguments[0].scrollIntoView({block:'center'});", paginatorSelect);
                    Thread.Sleep(200);

                    // JS click
                    js.ExecuteScript("arguments[0].click();", paginatorSelect);
                    Thread.Sleep(400); // allow Angular overlay rendering

                    // Wait for overlay container and visible options
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div.cdk-overlay-container")));
                    wait.Until(d => d.FindElements(optionLocator).Any(e => e.Displayed));

                    dropdownOpened = true;
                    Console.WriteLine($"[INFO] Dropdown opened successfully on attempt {attempt}");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[WARN] Dropdown not opened yet (Attempt {attempt}): {ex.Message}");
                    Thread.Sleep(300);
                }
            }

            if (!dropdownOpened)
                Assert.Fail("Paginator dropdown did not open. No mat-option elements found.");

            // ------------------ Validate all options are numeric ------------------
            var paginatorOptions = driver.FindElements(optionLocator)
                                         .Where(o => o.Displayed)
                                         .ToList();

            if (paginatorOptions.Count == 0)
                Assert.Fail("Paginator options list is empty.");

            Console.WriteLine($"[INFO] Validating all paginator options are numeric...");
            foreach (var option in paginatorOptions)
            {
                string text = option.Text.Trim();
                if (!int.TryParse(text, out _))
                    Assert.Fail($"Non numeric paginator option found: {text}");
                Console.WriteLine($"[INFO] Valid paginator option: {text}");
            }

            Console.WriteLine("[SUCCESS] Paginator validation completed.");
        }

        private const int MaxClickRetries = 3;

        private void ClickElementWithRetryAndVerify(IWebDriver driver, IWebElement element, string elementName, By verifyElementLocator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            Actions actions = new Actions(driver);

            for (int attempt = 1; attempt <= MaxClickRetries; attempt++)
            {
                try
                {
                    Console.WriteLine($" Attempt {attempt}: Hovering and clicking: {elementName}");

                    // Ensure element is clickable before interacting
                    wait.Until(ExpectedConditions.ElementToBeClickable(element));

                    // Scroll to center
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
                    Thread.Sleep(300);

                    // Hover and click
                    actions.MoveToElement(element)
                           .Pause(TimeSpan.FromMilliseconds(300))
                           .Click()
                           .Perform();

                    Console.WriteLine($" Actions click performed on: {elementName}");

                    // Wait for verify element
                    wait.Until(ExpectedConditions.ElementIsVisible(verifyElementLocator));

                    // Extra wait for page rendering completion
                    wait.Until(driver =>
                    {
                        return ((IJavaScriptExecutor)driver)
                                .ExecuteScript("return document.readyState").ToString() == "complete";
                    });

                    Thread.Sleep(500); // stabilizer delay

                    Console.WriteLine($" Verified presence of element after clicking {elementName}");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Exception during click attempt on {elementName}: {ex.Message}");

                    if (attempt == MaxClickRetries)
                        throw;

                    Thread.Sleep(600);
                }
            }
        }



        public void PaginatorSuccess(IWebDriver driver, string menuData, string submenuData, bool state)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(menuData)));
            IWebElement menu = driver.FindElement(By.Id(menuData));
            menu.Click();
            if (menu != null)
            {
                menu.Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(submenuData)));
                IWebElement submenu = driver.FindElement(By.Id(submenuData));
                if (submenu != null)
                {
                    submenu.Click();
                    Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("mat-paginator-range-label")));
                    IWebElement paginatorRange = driver.FindElement(By.ClassName("mat-paginator-range-label"));
                    string totalText = paginatorRange.Text.Split(new string[] { "of" }, StringSplitOptions.None)[1].Trim();
                    int totalNumber = int.Parse(totalText);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("mat-paginator-page-size-select")));
                    IWebElement paginatorSelect = driver.FindElement(By.ClassName("mat-paginator-page-size-select"));
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("mat-select-value-text")));
                    IWebElement paginatorSelectContainer = driver.FindElement(By.ClassName("mat-select-value-text"));
                    IWebElement paginatorSelectValue = paginatorSelectContainer.FindElement(By.TagName("span"));
                    paginatorSelect.Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                    IList<IWebElement> paginatorOptions = driver.FindElements(By.TagName("mat-option"));
                    Console.WriteLine(paginatorOptions[0].Text + " paginator text");
                    int paginatorNumberSel = int.Parse(paginatorOptions[0].Text);
                    paginatorOptions[0].Click();
                    Console.WriteLine(paginatorNumberSel + " kk");
                    Thread.Sleep(4000);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    IWebElement table = driver.FindElement(By.TagName("tbody"));
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.TagName("tr")));
                    IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
                    int rows = datas.Count() % 2;
                    Console.WriteLine(rows + " roews");
                    Console.WriteLine(totalNumber + " totalNumber");
                    if (rows > paginatorNumberSel)
                    {
                        Assert.IsTrue(false, "true");
                    }
                    if (totalNumber > paginatorNumberSel)
                    {
                        IWebElement nextBtn = driver.FindElement(By.ClassName("mat-paginator-navigation-next"));
                        bool isDisabled = nextBtn.GetAttribute("disabled") != null && nextBtn.GetAttribute("disabled").Equals("true");
                        Assert.IsFalse(isDisabled, "true");
                    }
                    else
                    {
                        IWebElement nextBtn = driver.FindElement(By.ClassName("mat-paginator-navigation-next"));
                        bool isDisabled = nextBtn.GetAttribute("disabled") != null && nextBtn.GetAttribute("disabled").Equals("true");
                        Assert.IsTrue(isDisabled, "false");
                    }
                }
                else
                {
                    Console.WriteLine("doesnt find sub menu");
                }
            }
            else
            {
                Console.WriteLine("doesnt find menu");
            }
        }

       
        public void PaginatorTabSuccess(IWebDriver driver, string menuData, string submenuData, string tabName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(menuData)));
            IWebElement menu = driver.FindElement(By.Id(menuData));
            menu.Click();
            if (menu != null)
            {
                menu.Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(submenuData)));
                IWebElement submenu = driver.FindElement(By.Id(submenuData));
                if (submenu != null)
                {
                    submenu.Click();
                    Thread.Sleep(2000);
                    IWebElement tabList = wait.Until(drv => driver.FindElement(By.ClassName("mat-tab-list")));
                    Console.WriteLine(tabList.Text);
                    IWebElement tabLabels = tabList.FindElement(By.ClassName("mat-tab-labels"));
                    IWebElement returnTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'" + tabName + "')]")));
                    returnTab.Click();
                    Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("mat-paginator-range-label")));
                    IWebElement paginatorRange = driver.FindElement(By.ClassName("mat-paginator-range-label"));
                    string totalText = paginatorRange.Text.Split(new string[] { "of" }, StringSplitOptions.None)[1].Trim();
                    int totalNumber = int.Parse(totalText);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("mat-paginator-page-size-select")));
                    IWebElement paginatorSelect = driver.FindElement(By.ClassName("mat-paginator-page-size-select"));
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-select[contains(@aria-label,'Items per page')]")));
                    IWebElement paginatorSelectContainer = driver.FindElement(By.XPath("//mat-select[contains(@aria-label,'Items per page')]"));
                    IWebElement paginatorSelectValue = paginatorSelectContainer.FindElement(By.TagName("span"));
                    paginatorSelect.Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.TagName("mat-option")));
                    IList<IWebElement> paginatorOptions = driver.FindElements(By.TagName("mat-option"));
                    Console.WriteLine(paginatorOptions[0].Text + " paginator text");
                    int paginatorNumberSel = int.Parse(paginatorOptions[0].Text);
                    paginatorOptions[0].Click();
                    Console.WriteLine(paginatorNumberSel + " kk");
                    Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.TagName("tbody")));
                    IWebElement table = driver.FindElement(By.TagName("tbody"));
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.TagName("tr")));
                    IList<IWebElement> datas = table.FindElements(By.TagName("tr"));
                    int rows = datas.Count();
                    Console.WriteLine(rows + " roews");
                    Console.WriteLine(totalNumber + " totalNumber");
                    if (rows > paginatorNumberSel)
                    {
                        Assert.IsTrue(false, "true");
                    }
                    if (totalNumber > paginatorNumberSel)
                    {
                        IWebElement nextBtn = driver.FindElement(By.ClassName("mat-paginator-navigation-next"));
                        bool isDisabled = nextBtn.GetAttribute("disabled") != null && nextBtn.GetAttribute("disabled").Equals("true");
                        Console.WriteLine(isDisabled);
                        Assert.IsFalse(isDisabled, "true");
                    }
                    else
                    {
                        IWebElement nextBtn = driver.FindElement(By.ClassName("mat-paginator-navigation-next"));
                        bool isDisabled = nextBtn.GetAttribute("disabled") != null && nextBtn.GetAttribute("disabled").Equals("true");
                        Console.WriteLine(isDisabled);
                        Assert.IsTrue(isDisabled, "false");
                    }
                }
                else
                {
                    Console.WriteLine("doesnt find sub menu");
                }
            }
            else
            {
                Console.WriteLine("doesnt find menu");
            }
        }
       


    }
}
