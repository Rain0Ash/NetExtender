// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using DynamicData.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NetExtender.Utils.Browser
{
    public static class SeleniumUtils
    {
        public static Boolean IsReadyState(this IWebDriver driver)
        {
            return IsReadyState((IJavaScriptExecutor) driver);
        }
        
        public static Boolean IsReadyState(this IJavaScriptExecutor executor)
        {
            return String.Equals(executor.ExecuteScript("return document.readyState").ToString(), "complete", StringComparison.OrdinalIgnoreCase);
        }

        public static T WaitUntil<T>(this IWebDriver driver, [NotNull] Func<IWebDriver, T> condition)
        {
            return WaitUntil(driver, condition, TimeSpan.FromSeconds(3));
        }
        
        public static T WaitUntil<T>(this IWebDriver driver, [NotNull] Func<IWebDriver, T> condition, TimeSpan timeout)
        {
            return WaitUntil(driver, condition, timeout, TimeSpan.FromMilliseconds(50));
        }
        
        public static T WaitUntil<T>(this IWebDriver driver, [NotNull] Func<IWebDriver, T> condition, TimeSpan timeout, TimeSpan polling)
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, timeout) { PollingInterval = polling };

            return wait.Until(condition);
        }

        public static Boolean WaitBrowser(this IWebDriver driver)
        {
            return WaitUntil(driver, IsReadyState);
        }
    }
}