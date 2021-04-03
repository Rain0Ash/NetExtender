// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if Selenium
using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NetExtender.Times;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NetExtender.Utils.Browser
{
    public static class SeleniumUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsReadyState([NotNull] this IWebDriver driver)
        {
            if (driver is null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            return IsReadyState((IJavaScriptExecutor) driver);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsReadyState([NotNull] this IJavaScriptExecutor executor)
        {
            if (executor is null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            return String.Equals(executor.ExecuteScript("return document.readyState").ToString(), "complete", StringComparison.OrdinalIgnoreCase);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WaitUntil<T>([NotNull] this IWebDriver driver, [NotNull] Func<IWebDriver, T> condition)
        {
            return WaitUntil(driver, condition, Time.Second.Three);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WaitUntil<T>([NotNull] this IWebDriver driver, [NotNull] Func<IWebDriver, T> condition, TimeSpan timeout)
        {
            return WaitUntil(driver, condition, timeout, Time.Milli.Fifty);
        }
        
        public static T WaitUntil<T>([NotNull] this IWebDriver driver, [NotNull] Func<IWebDriver, T> condition, TimeSpan timeout, TimeSpan polling)
        {
            if (driver is null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            IWait<IWebDriver> wait = new WebDriverWait(driver, timeout) { PollingInterval = polling };

            return wait.Until(condition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean WaitBrowser([NotNull] this IWebDriver driver)
        {
            if (driver is null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            return WaitUntil(driver, IsReadyState);
        }
    }
}

#endif