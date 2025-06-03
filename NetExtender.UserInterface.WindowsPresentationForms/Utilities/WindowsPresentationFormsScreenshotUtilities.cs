// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using NetExtender.Utilities.Types;
using NetExtender.Windows.Utilities;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationFormsScreenshotUtilities
    {
        public static Bitmap? MakeScreenshot(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return WindowsScreenshotUtilities.MakeScreenshot(window.GetHandle());
        }
        
        public static Bitmap? MakeScreenshot(this Window window, ScreenshotType type)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return WindowsScreenshotUtilities.MakeScreenshot(window.GetHandle(), type);
        }
        
        public static Boolean TryMakeScreenshot(this Window window, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return WindowsScreenshotUtilities.TryMakeScreenshot(window.GetHandle(), out screenshot);
        }
        
        public static Boolean TryMakeScreenshot(this Window window, ScreenshotType type, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return WindowsScreenshotUtilities.TryMakeScreenshot(window.GetHandle(), type, out screenshot);
        }

        public static Boolean TryMakeScreenshot(this Window window, [MaybeNullWhen(false)] out BitmapSource screenshot)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (!TryMakeScreenshot(window, out Bitmap? bitmap))
            {
                screenshot = default;
                return false;
            }

            try
            {
                screenshot = bitmap.ToBitmapSource();
                return true;
            }
            finally
            {
                bitmap.Dispose();
            }
        }

        public static Boolean TryMakeScreenshot(this Window window, ScreenshotType type, [MaybeNullWhen(false)] out BitmapSource screenshot)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (!TryMakeScreenshot(window, type, out Bitmap? bitmap))
            {
                screenshot = default;
                return false;
            }

            try
            {
                screenshot = bitmap.ToBitmapSource();
                return true;
            }
            finally
            {
                bitmap.Dispose();
            }
        }
    }
}