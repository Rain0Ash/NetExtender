// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utilities.Windows;

namespace NetExtender.Utilities.UserInterface
{
    public static class WinFormsScreenshotUtilities
    {
        public static Boolean TakeScreenshot(this Form form, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return ScreenshotUtilities.TakeScreenshot(form.Handle, out screenshot);
        }

        public static Boolean TakeScreenshot<T>(this T window, [MaybeNullWhen(false)] out Bitmap screenshot) where T : IUserInterfaceHandle
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return ScreenshotUtilities.TakeScreenshot(window.Handle, out screenshot);
        }
    }
}