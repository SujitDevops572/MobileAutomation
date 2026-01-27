using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace VMS_Phase1PortalAT.utls.utilMethods
{
    public class DateSelect
    {
        public static void DateTimeActions(IWebDriver driver, Dictionary<string, string> dict , int index)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            try
            {
                ClickDateIcon(driver, wait , index);

                Console.WriteLine("Picking Date...");
                PickSingleDate(driver, wait, dict["year"], dict["month"], dict["date"]);

                Console.WriteLine("Waiting for table refresh...");
                Thread.Sleep(3000); // Give UI time to update
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception in DateTimeActions: {ex.Message}");
                Console.WriteLine(ex);
            }
        }

        private static void ClickDateIcon(IWebDriver driver, WebDriverWait wait, int index)
        {
            
            var dateIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"(//mat-icon[text()='date_range'])[{index}]")));
            SafeClick(driver, dateIcon, "Date Icon");
        }

        private static void PickSingleDate(IWebDriver driver, WebDriverWait wait, string year, string month, string day)
        {
            // Open calendar control
            var calendarControl = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.owl-dt-calendar-control")));
            SafeClick(driver, calendarControl, "Calendar Control");

            Thread.Sleep(300);

            ClickPickerCell(driver, wait, "owl-date-time-multi-year-view", year);
            ClickPickerCell(driver, wait, "owl-date-time-year-view", month, StringComparison.OrdinalIgnoreCase);
            ClickPickerCell(driver, wait, "owl-date-time-month-view", day);
        }

        private static void ClickPickerCell(IWebDriver driver, WebDriverWait wait, string viewTag, string textToMatch, StringComparison comparison = StringComparison.Ordinal)
        {
            for (int attempt = 0; attempt < 3; attempt++)
            {
                try
                {
                    var view = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName(viewTag)));
                    var cells = view.FindElements(By.TagName("td"));

                    var option = cells.FirstOrDefault(td => td.Text.Trim().Equals(textToMatch, comparison));
                    if (option == null)
                        throw new Exception($" Unable to find '{textToMatch}' in {viewTag}");

                    SafeClick(driver, option, $"{viewTag} Cell: {textToMatch}");
                    Thread.Sleep(300);
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($" Stale element in {viewTag}, retrying...");
                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Error clicking cell in {viewTag}: {ex.Message}");
                    if (attempt == 2) throw;
                }
            }
        }

        private static void SafeClick(IWebDriver driver, IWebElement element, string label = "")
        {
            for (int attempt = 0; attempt < 3; attempt++)
            {
                try
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
                    element.Click();
                    return;
                }
                catch (ElementClickInterceptedException)
                {
                    Console.WriteLine($" Click intercepted on {label}, using JS click...");
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($" Stale element when clicking {label}, retrying...");
                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Error clicking {label}: {ex.Message}");
                    Thread.Sleep(300);
                }
            }

            Console.WriteLine($" Failed to click {label} after retries.");
        }
    }
}
