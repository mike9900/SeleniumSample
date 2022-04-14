using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumExample
{
    public static class Extensions
    {
        public static IWebElement WaitFindElement(this IWebDriver driver, By by, int timeout = 30)
        {
            IWebElement result = null;
            driver.WaitUntil(d =>
            {
                try
                {
                    result = d.FindElement(by);
                    return result != null;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }, timeout);

            return result;
        }

        public static bool WaitUntil(this IWebDriver driver, Func<IWebDriver, bool> func, int timeout = 10)
        {
            WebDriverWait driverWait = new(driver, TimeSpan.FromSeconds(timeout));
            
            try
            {
                return driverWait.Until(func);
            }
            catch
            {
                return false;
            }
        }

        public static bool PageLoaded(this IWebDriver driver)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
        }
    }
}
