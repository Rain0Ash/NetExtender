// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Native.Windows;

namespace NetExtender.Utilities.Windows
{
    public enum ScreenshotType
    {
        Full,
        Title,
        Content
    }
    
    public static class ScreenshotUtilities
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
        private static extern Boolean GetWindowRect(IntPtr handle, out WinRectangle rectangle);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetClientRect(IntPtr handle, out WinRectangle rectangle);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean ClientToScreen(IntPtr handle, out Point point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean ClientInScreen(IntPtr handle, out Point point)
        {
            if (handle != IntPtr.Zero && ClientToScreen(handle, out point) && point.X >= 0 && point.Y >= 0)
            {
                return true;
            }

            point = default;
            return false;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern Boolean GetWindowInfo(IntPtr handle, ref WinWindowInfo info);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean GetWindowInformation(IntPtr handle, out WinWindowInfo info)
        {
            info = new WinWindowInfo { Size = (UInt32) Marshal.SizeOf(typeof(WinWindowInfo)) };
            return GetWindowInfo(handle, ref info);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern Int32 GetSystemMetrics(Int32 index);

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean PrintWindow(IntPtr handle, IntPtr hdc, UInt32 flags);

        private static Boolean PrintWindow(IntPtr handle, Bitmap bitmap)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            if (handle == IntPtr.Zero)
            {
                return false;
            }

            using Graphics graphics = Graphics.FromImage(bitmap);
            IntPtr hdc = graphics.GetHdc();

            try
            {
                return PrintWindow(handle, hdc, 0);
            }
            finally
            {
                graphics.ReleaseHdc(hdc);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct WinWindowInfo
        {
            public UInt32 Size { get; init; }
            public WinRectangle Window { get; init; }
            public WinRectangle Client { get; init; }
            public UInt32 Style { get; init; }
            public UInt32 ExStyle { get; init; }
            public UInt32 WindowStatus { get; init; }
            public Int32 WindowBorderX { get; init; }
            public Int32 WindowBorderY { get; init; }
            public UInt16 AtomWindowType { get; init; }
            public UInt16 CreatorVersion { get; init; }

            public Size BorderSize
            {
                get
                {
                    return new Size(WindowBorderX, WindowBorderY);
                }
            }
        }

        //TODO:
        private static Boolean GetContentSize(IntPtr handle, ScreenshotType type, out Rectangle bounds, out Rectangle content)
        {
            if (handle == IntPtr.Zero)
            {
                bounds = default;
                content = default;
                return false;
            }
            
            switch (type)
            {
                case ScreenshotType.Full:
                {
                    bounds = default;
                    content = default;
                    return false;
                }
                case ScreenshotType.Title:
                {
                    if (!GetWindowRect(handle, out WinRectangle rect) || !GetWindowInformation(handle, out WinWindowInfo info))
                    {
                        bounds = default;
                        content = default;
                        return false;
                    }

                    Size border = info.BorderSize;
                    bounds = rect;
                    content = new Rectangle(border.Width, 0, bounds.Width - border.Width * 2, bounds.Height - border.Width);
                    return true;
                }
                case ScreenshotType.Content:
                {
                    if (!GetClientRect(handle, out WinRectangle rect) || !ClientInScreen(handle, out Point point) || !GetWindowInformation(handle, out WinWindowInfo info))
                    {
                        bounds = default;
                        content = default;
                        return false;
                    }

                    Rectangle rectangle = rect;
                    Size border = info.BorderSize;
                    Int32 title = GetSystemMetrics(0x21) + GetSystemMetrics(0x4) + GetSystemMetrics(0x5C);
                    bounds = new Rectangle(point.X, point.Y, rectangle.Right, rectangle.Bottom);
                    content = new Rectangle(border.Width, title, bounds.Width - border.Width, bounds.Height - title);
                    return true;
                }
                default:
                    throw new EnumUndefinedOrNotSupportedException<ScreenshotType>(type, nameof(type), null);
            }
        }

        public static Boolean TakeScreenshot(IntPtr handle, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            return TakeScreenshot(handle, ScreenshotType.Content, out screenshot);
        }

        public static Boolean TakeScreenshot(IntPtr handle, ScreenshotType type, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            if (handle == IntPtr.Zero)
            {
                screenshot = null;
                return false;
            }

            if (!GetContentSize(handle, type, out Rectangle rectangle, out Rectangle content))
            {
                screenshot = null;
                return false;
            }

            try
            {
                using Bitmap raw = new Bitmap(rectangle.Width, rectangle.Height);

                if (!PrintWindow(handle, raw))
                {
                    raw.Dispose();
                    screenshot = null;
                    return false;
                }

                screenshot = raw.Clone(content, raw.PixelFormat);
                return true;
            }
            catch (Exception)
            {
                screenshot = null;
                return false;
            }
        }
    }
}