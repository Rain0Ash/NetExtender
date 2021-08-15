// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows;
using NetExtender.Utilities.Windows;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationFormsScreenshotUtilities
    {
        public static Boolean TakeScreenshot(this Window window, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return ScreenshotUtilities.TakeScreenshot(window.GetHandle(), out screenshot);
        }
    }
}