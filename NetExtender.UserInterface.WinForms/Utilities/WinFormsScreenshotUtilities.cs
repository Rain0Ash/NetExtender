// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using NetExtender.Utilities.Windows;

namespace NetExtender.Utilities.UserInterface
{
    public static class WinFormsScreenshotUtilities
    {
        public static Bitmap? MakeScreenshot(this Form form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return ScreenshotUtilities.MakeScreenshot(form.Handle);
        }
        
        public static Bitmap? MakeScreenshot(this Form form, ScreenshotType type)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return ScreenshotUtilities.MakeScreenshot(form.Handle, type);
        }
        
        public static Boolean TryMakeScreenshot(this Form form, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return ScreenshotUtilities.TryMakeScreenshot(form.Handle, out screenshot);
        }
        
        public static Boolean TryMakeScreenshot(this Form form, ScreenshotType type, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return ScreenshotUtilities.TryMakeScreenshot(form.Handle, type, out screenshot);
        }
    }
}