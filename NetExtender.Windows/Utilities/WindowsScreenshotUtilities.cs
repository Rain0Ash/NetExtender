using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Types.Exceptions;
using NetExtender.Windows.Types;

namespace NetExtender.Windows.Utilities
{
    public enum ScreenshotType : Byte
    {
        Full,
        Title,
        Content
    }
    
    public static class WindowsScreenshotUtilities
    {
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
            if (handle != IntPtr.Zero && ClientToScreen(handle, out point) && point is { X: >= 0, Y: >= 0 })
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean PrintWindow(IntPtr handle, Bitmap bitmap)
        {
            return PrintWindow(handle, bitmap, 0);
        }

        private static Boolean PrintWindow(IntPtr handle, Bitmap bitmap, UInt32 flags)
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

            if (hdc == IntPtr.Zero)
            {
                return false;
            }

            try
            {
                return PrintWindow(handle, hdc, flags);
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
        private static Boolean GetContentSize(IntPtr handle, ScreenshotType type, out Boolean client, out Rectangle bounds, out Rectangle content)
        {
            if (handle == IntPtr.Zero)
            {
                client = false;
                bounds = default;
                content = default;
                return false;
            }
            
            switch (type)
            {
                case ScreenshotType.Full:
                {
                    client = false;
                    bounds = default;
                    content = default;
                    return false;
                }
                case ScreenshotType.Title:
                {
                    client = false;
                    
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
                    client = true;
                    
                    if (!GetWindowRect(handle, out WinRectangle rect) || !GetWindowInformation(handle, out WinWindowInfo info))
                    {
                        bounds = default;
                        content = default;
                        return false;
                    }

                    Size border = info.BorderSize;
                    bounds = rect;
                    Int32 title = GetSystemMetrics(0x21) + GetSystemMetrics(0x4) + GetSystemMetrics(0x5C);
                    content = new Rectangle(border.Width, title, bounds.Width - border.Width * 2, bounds.Height - border.Width - title);
                    return true;
                }
                default:
                    throw new EnumUndefinedOrNotSupportedException<ScreenshotType>(type, nameof(type), null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap? MakeScreenshot(IntPtr handle)
        {
            return MakeScreenshot(handle, ScreenshotType.Content);
        }

        // TODO: Check
        public static Bitmap? MakeScreenshot(IntPtr handle, ScreenshotType type)
        {
            if (handle == IntPtr.Zero)
            {
                return null;
            }

            if (!GetContentSize(handle, type, out Boolean client, out Rectangle rectangle, out Rectangle content))
            {
                return null;
            }

            using Bitmap raw = new Bitmap(rectangle.Width, rectangle.Height);

            if (client)
            {
                return PrintWindow(handle, raw) ? raw.Clone(content, raw.PixelFormat) : null;
            }

            using Graphics graphics = Graphics.FromImage(raw);
            graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, new Size(rectangle.Width, rectangle.Height), CopyPixelOperation.SourceCopy);
            return raw.Clone(content, raw.PixelFormat);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryMakeScreenshot(IntPtr handle, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            return TryMakeScreenshot(handle, ScreenshotType.Content, out screenshot);
        }

        public static Boolean TryMakeScreenshot(IntPtr handle, ScreenshotType type, [MaybeNullWhen(false)] out Bitmap screenshot)
        {
            try
            {
                screenshot = MakeScreenshot(handle, type);
                return screenshot is not null;
            }
            catch (Exception)
            {
                screenshot = default;
                return false;
            }
        }
    }
}