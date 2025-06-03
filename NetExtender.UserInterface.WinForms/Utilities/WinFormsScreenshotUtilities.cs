// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using NetExtender.Utilities.Windows;
using NetExtender.Windows.Utilities;

namespace NetExtender.Utilities.UserInterface
{
    public static class WinFormsScreenshotUtilities
    {
        /// <summary>
        /// Takes a screenshot of the screen as a whole
        /// </summary>
        /// <param name="screen">Screen to get the screenshot from</param>
        /// <returns>Returns a Bitmap containing the screenshot</returns>
        public static Bitmap MakeScreenshot(this Screen screen)
        {
            if (screen is null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            Bitmap bitmap = new Bitmap(screen.Bounds.Width > 1 ? screen.Bounds.Width : 1, screen.Bounds.Height > 1 ? screen.Bounds.Height : 1, PixelFormat.Format32bppArgb);

            try
            {
                if (screen.Bounds.Width > 1 && screen.Bounds.Height > 1)
                {
                    using Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screen.Bounds.Size, CopyPixelOperation.SourceCopy);
                }
            }
            catch (Exception)
            {
            }

            return bitmap;
        }

        /// <summary>
        /// Takes a screenshot of the screen as a whole (if multiple screens are attached, it takes an image containing them all)
        /// </summary>
        /// <param name="screens">Screens to get the screenshot from</param>
        /// <returns>Returns a Bitmap containing the screenshot</returns>
        public static Bitmap MakeScreenshot(this IEnumerable<Screen> screens)
        {
            if (screens is null)
            {
                throw new ArgumentNullException(nameof(screens));
            }

            Rectangle rectangle = screens.Aggregate(Rectangle.Empty, (current, screen) => Rectangle.Union(current, screen.Bounds));
            Bitmap bitmap = new Bitmap(rectangle.Width > 1 ? rectangle.Width : 1, rectangle.Height > 1 ? rectangle.Width : 1, PixelFormat.Format32bppArgb);

            try
            {
                if (rectangle.Width > 1 && rectangle.Height > 1)
                {
                    using Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
                }
            }
            catch (Exception)
            {
            }

            return bitmap;
        }

        /// <summary>
        /// Takes a screenshots of the screens
        /// </summary>
        /// <param name="screens">Screens to get the screenshot from</param>
        /// <returns>Returns a Bitmap containing the screenshot</returns>
        public static IEnumerable<Bitmap> MakeScreenshots(this IEnumerable<Screen> screens)
        {
            if (screens is null)
            {
                throw new ArgumentNullException(nameof(screens));
            }

            return screens.Select(MakeScreenshot);
        }
        
        public static Bitmap? MakeScreenshot(this Form form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return WindowsScreenshotUtilities.MakeScreenshot(form.Handle);
        }
        
        public static Bitmap? MakeScreenshot(this Form form, ScreenshotType type)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return WindowsScreenshotUtilities.MakeScreenshot(form.Handle, type);
        }
        
        public static Boolean TryMakeScreenshot(this Form form, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return WindowsScreenshotUtilities.TryMakeScreenshot(form.Handle, out screenshot);
        }
        
        public static Boolean TryMakeScreenshot(this Form form, ScreenshotType type, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return WindowsScreenshotUtilities.TryMakeScreenshot(form.Handle, type, out screenshot);
        }
    }
}