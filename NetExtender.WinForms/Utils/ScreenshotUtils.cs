// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NetExtender.Types.Native.Windows;
using NetExtender.Utils.Static;

namespace NetExtender.Utils.Windows
{
    public static class ScreenshotUtils
    {
        /// <summary>
        /// Takes a screenshot of the screen as a whole
        /// </summary>
        /// <param name="screen">Screen to get the screenshot from</param>
        /// <returns>Returns a Bitmap containing the screen shot</returns>
        public static Bitmap TakeScreenshot(this Screen screen)
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
                // ignored
            }

            return bitmap;
        }

        /// <summary>
        /// Takes a screenshot of the screen as a whole (if multiple screens are attached, it takes an image containing them all)
        /// </summary>
        /// <param name="screens">Screens to get the screenshot from</param>
        /// <returns>Returns a Bitmap containing the screen shot</returns>
        public static Bitmap TakeScreenshot(this IEnumerable<Screen> screens)
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
                // ignored
            }

            return bitmap;
        }

        /// <summary>
        /// Takes a screenshots of the screens
        /// </summary>
        /// <param name="screens">Screens to get the screenshot from</param>
        /// <returns>Returns a Bitmap containing the screen shot</returns>
        public static IEnumerable<Bitmap> TakeScreenshots(this IEnumerable<Screen> screens)
        {
            if (screens is null)
            {
                throw new ArgumentNullException(nameof(screens));
            }

            return screens.Select(TakeScreenshot);
        }
        
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetWindowRect(IntPtr hWnd, out WinRectangle lpRect);

        public static Boolean TakeScreenshot(IntPtr handle, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            if (handle == IntPtr.Zero)
            {
                screenshot = default;
                return false;
            }

            if (!GetWindowRect(handle, out WinRectangle rect))
            {
                screenshot = default;
                return false;
            }

            Rectangle rectangle = rect;

            screenshot = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);

            using Graphics graphics = Graphics.FromImage(screenshot);
            graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, new Size(rectangle.Width, rectangle.Height), CopyPixelOperation.SourceCopy);

            return true;
        }
    }
}