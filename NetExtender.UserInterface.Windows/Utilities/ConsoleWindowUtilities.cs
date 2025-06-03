// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Types.Attributes;
using NetExtender.Types.Console.Interfaces;
using NetExtender.Types.Events;
using NetExtender.Types.Exceptions;
using NetExtender.UserInterface;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Windows;
using NetExtender.Workstation.Interfaces;

namespace NetExtender.Utilities.UserInterface
{
    public enum ConsoleCtrlType
    {
        CtrlCEvent = 0,
        CtrlBreakEvent = 1,
        CtrlCloseEvent = 2,
        CtrlLogoffEvent = 5,
        CtrlShutdownEvent = 6,
        CtrlTaskManagerClosing = 10
    }

    public static class ConsoleWindowUtilities
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, Int32 x, Int32 y, Int32 cx, Int32 cy, Int32 flags);

        [StructLayout(LayoutKind.Sequential)]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private readonly struct WindowPlacement
        {
            public Int32 Length { get; init; }
            public UInt32 Flags { get; init; }
            public UInt32 ShowCmd { get; init; }
            public Point MinPosition { get; init; }
            public Point MaxPosition { get; init; }
            public Rectangle NormalPosition { get; init; }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

        [DllImport("kernel32.dll")]
        private static extern Boolean GetConsoleMode(IntPtr hConsoleHandle, out UInt32 lpMode);

        [DllImport("kernel32.dll")]
        private static extern Boolean SetConsoleMode(IntPtr hConsoleHandle, UInt32 dwMode);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern Boolean SetConsoleTitle(String lpConsoleTitle);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public readonly struct FontIndex
        {
            public readonly UInt32 Index;
            public readonly Int16 SizeX;
            public readonly Int16 SizeY;
        }

        [DllImport("kernel32.dll")]
        private static extern Boolean SetConsoleIcon(IntPtr hIcon);

        [DllImport("kernel32")]
        private static extern Boolean SetConsoleFont(IntPtr hOutput, UInt32 index);

        [DllImport("kernel32.dll")]
        private static extern UInt32 GetNumberOfConsoleFonts();

        [DllImport("kernel32")]
        private static extern Boolean GetConsoleFontInfo(IntPtr hOutput, [MarshalAs(UnmanagedType.Bool)] Boolean bMaximize, UInt32 count, [MarshalAs(UnmanagedType.LPArray), Out] FontIndex[] fonts);

        [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal readonly struct FontInfo
        {
            public static FontInfo Create()
            {
                return new FontInfo(0);
            }

            private const Int32 CBSize = 84;

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            private Int32 cbSize { get; }
            public Int32 Index { get; init; }
            public Int16 Width { get; init; }
            public Int16 Size { get; }
            public Int32 Family { get; init; }
            public Int32 Weight { get; init; }

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public readonly String? FontName;

            public FontInfo(Int16 size)
                : this(null, size)
            {
            }

            public FontInfo(String? name, Int16 size)
            {
                cbSize = CBSize;
                FontName = name;
                Index = 0;
                Width = 0;
                Size = size;
                Family = 54;
                Weight = 400;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetCurrentConsoleFontEx(IntPtr hConsoleOutput, Boolean MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean SetCurrentConsoleFontEx(IntPtr hConsoleOutput, Boolean MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

        [DllImport("kernel32.dll")]
        private static extern UInt32 GetLastError();

        public static event TypeHandler<CancelEventArgs<ConsoleCtrlType>>? ConsoleExit;

        /*
        public delegate void ConsoleMouseEvent(MOUSE_EVENT_RECORD r);
        public delegate void ConsoleKeyEvent(KEY_EVENT_RECORD r);
        public delegate void ConsoleWindowBufferSizeEvent(WINDOW_BUFFER_SIZE_RECORD r);

        public static event ConsoleMouseEvent MouseEvent;
        public static event ConsoleKeyEvent KeyEvent;
        public static event ConsoleWindowBufferSizeEvent WindowBufferSizeEvent;
        */

        public static IntPtr ConsoleWindow
        {
            get
            {
                return GetConsoleWindow();
            }
        }

        private static Boolean? GetMode(IntPtr handle, UInt32 mode)
        {
            if (handle == IntPtr.Zero)
            {
                return null;
            }

            if (!GetConsoleMode(handle, out UInt32 current))
            {
                return null;
            }

            return (current & mode) == mode;
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private static Boolean ChangeMode(IntPtr handle, Boolean? value, UInt32 mode)
        {
            if (handle == IntPtr.Zero || value is null)
            {
                return false;
            }

            if (!GetConsoleMode(handle, out UInt32 current))
            {
                return false;
            }

            if (value.Value)
            {
                SetConsoleMode(handle, current | mode);
                return true;
            }

            SetConsoleMode(handle, current & ~mode);
            return true;
        }

        public static String Title
        {
            get
            {
                return Console.Title;
            }
            set
            {
                Console.Title = value;
            }
        }

        public static Boolean TryGetTitle([MaybeNullWhen(false)] out String result)
        {
            try
            {
                result = Console.Title;
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public static Boolean TrySetTitle(String? value)
        {
            try
            {
                Console.Title = value ?? String.Empty;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static Icon? icon;
        public static Icon? ConsoleIcon
        {
            get
            {
                return icon;
            }
            set
            {
                if (ConsoleWindow == IntPtr.Zero)
                {
                    return;
                }

                if (SetConsoleIcon(value?.Handle ?? IntPtr.Zero))
                {
                    icon = value;
                }
            }
        }

        public static FontIndex[]? ConsoleFonts
        {
            get
            {
                if (ConsoleWindow == IntPtr.Zero)
                {
                    return null;
                }

                FontIndex[] fonts = new FontIndex[GetNumberOfConsoleFonts()];

                if (fonts.Length <= 0)
                {
                    return null;
                }

                return GetConsoleFontInfo(WindowsConsoleUtilities.ConsoleOutputHandle, false, (UInt32) fonts.Length, fonts) ? fonts : null;
            }
        }

        public static UInt32 ConsoleFontIndex
        {
            get
            {
                if (ConsoleWindow == IntPtr.Zero)
                {
                    throw new InvalidOperationException();
                }

                FontInfo result = ConsoleFont;
                return result.Index >= 0 ? (UInt32) result.Index : throw new InvalidOperationException();
            }
            set
            {
                if (ConsoleWindow == IntPtr.Zero)
                {
                    return;
                }

                FontIndex[]? fonts = ConsoleFonts;

                if (fonts is null)
                {
                    return;
                }

                if (value >= fonts.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }

                SetConsoleFont(WindowsConsoleUtilities.ConsoleOutputHandle, value);
            }
        }

        private static FontInfo ConsoleFont
        {
            get
            {
                FontInfo font = FontInfo.Create();

                if (!GetCurrentConsoleFontEx(WindowsConsoleUtilities.ConsoleOutputHandle, false, ref font))
                {
                    WindowsInteropUtilities.ThrowLastWin32Exception();
                }

                return font;
            }
            set
            {
                if (!SetCurrentConsoleFontEx(WindowsConsoleUtilities.ConsoleOutputHandle, false, ref value))
                {
                    WindowsInteropUtilities.ThrowLastWin32Exception();
                }
            }
        }

        public static IConsoleFontInfo? Font
        {
            get
            {
                FontInfo font;
                
                try
                {
                    font = ConsoleFont;
                }
                catch (Exception)
                {
                    return null;
                }
                
                IConsoleFont result = ConsoleFontInfo.New;
                result.FontName = font.FontName;
                result.Index = font.Index;
                result.Width = font.Width;
                result.Size = font.Size;
                result.Family = font.Family;
                result.Weight = font.Weight;
                return result;
            }
            set
            {
                if (value is null)
                {
                    return;
                }

                FontInfo font = new FontInfo(value.FontName, value.Size)
                {
                    Index = value.Index,
                    Width = value.Width,
                    Family = value.Family,
                    Weight = value.Weight
                };

                try
                {
                    ConsoleFont = font;
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException("Cannot set console font.", exception);
                }
            }
        }

        public static Int16 FontSize
        {
            get
            {
                return ConsoleFont.Size;
            }
            set
            {
                SetFont(value);
            }
        }

        public static void SetFont()
        {
            SetFont(0);
        }

        public static void SetFont(Int16 size)
        {
            SetFont(null, size);
        }

        public static void SetFont(String? font)
        {
            SetFont(font, 0);
        }

        public static void SetFont(String? font, Int16 size)
        {
            ConsoleFont = new FontInfo(font ?? ConsoleFont.FontName, size > 0 ? size : ConsoleFont.Size);
        }

        public static Point Cursor
        {
            get
            {
                (Int32 x, Int32 y) = GetCursorPosition();
                return new Point(x, y);
            }
            set
            {
                SetCursorPosition(value.X, value.Y);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Int32 X, Int32 Y) GetCursorPosition()
        {
            return Console.GetCursorPosition();
        }

        /// <inheritdoc cref="Console.SetCursorPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCursorPosition(Int32 x, Int32 y)
        {
            Console.SetCursorPosition(x, y);
        }

        /// <inheritdoc cref="Console.SetCursorPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCursorPosition(Point position)
        {
            Console.SetCursorPosition(position.X, position.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetCursorPosition()
        {
            SetCursorPosition(0, 0);
        }

        /// <inheritdoc cref="Console.SetBufferSize"/>
        public static Size BufferSize
        {
            get
            {
                return GetBufferSize();
            }
            set
            {
                SetBufferSize(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size GetBufferSize()
        {
            return new Size(Console.BufferWidth, Console.BufferHeight);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetBufferSize(out Size result)
        {
            try
            {
                result = GetBufferSize();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        /// <inheritdoc cref="Console.SetBufferSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBufferSize(Int32 width, Int32 height)
        {
            Console.SetBufferSize(width, height);
        }

        /// <inheritdoc cref="Console.SetBufferSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBufferSize(Size value)
        {
            SetBufferSize(value.Width, value.Height);
        }

        /// <inheritdoc cref="Console.SetBufferSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetBufferSize(Int32 width, Int32 height)
        {
            try
            {
                SetBufferSize(width, height);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc cref="Console.SetBufferSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetBufferSize(Size value)
        {
            return TrySetBufferSize(value.Width, value.Height);
        }

        /// <inheritdoc cref="Console.SetWindowSize"/>
        public static Size Size
        {
            get
            {
                return GetWindowSize();
            }
            set
            {
                SetWindowSize(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size GetWindowSize()
        {
            return new Size(Console.WindowWidth, Console.WindowHeight);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetWindowSize(out Size result)
        {
            try
            {
                result = GetWindowSize();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        /// <inheritdoc cref="Console.SetWindowSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowSize(Int32 width, Int32 height)
        {
            Console.SetWindowSize(Math.Min(width, Console.LargestWindowWidth), Math.Min(height, Console.LargestWindowHeight));
        }

        /// <inheritdoc cref="Console.SetWindowSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowSize(Size value)
        {
           SetWindowSize(value.Width, value.Height);
        }

        /// <inheritdoc cref="Console.SetWindowSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetWindowSize(Int32 width, Int32 height)
        {
            try
            {
                SetWindowSize(width, height);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc cref="Console.SetWindowSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetWindowSize(Size value)
        {
            return TrySetWindowSize(value.Width, value.Height);
        }

        public static Rectangle Rectangle
        {
            get
            {
                return UserInterfaceUtilities.GetWindowRectangle(ConsoleWindow);
            }
        }

        /// <summary>
        /// Return new <see cref="IDisposable"/> <see cref="System.Drawing.Graphics"/> for console window.
        /// </summary>
        public static Graphics CreateConsoleGraphics()
        {
            return Graphics.FromHwnd(ConsoleWindow);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        public static Point Position
        {
            get
            {
                return GetWindowPosition();
            }
            set
            {
                SetWindowPosition(value.X, value.Y);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point GetWindowPosition()
        {
            return new Point(Console.WindowLeft, Console.WindowTop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetWindowPosition(out Point point)
        {
            try
            {
                point = GetWindowPosition();
                return true;
            }
            catch (Exception)
            {
                point = default;
                return false;
            }
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowPosition(Point position)
        {
            SetWindowPosition(position.X, position.Y);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowPosition(Int32 x, Int32 y)
        {
            SetWindowPosition(x, y, Rectangle);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowPosition(Point position, Rectangle rectangle)
        {
            SetWindowPosition(position.X, position.Y, rectangle);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowPosition(Int32 x, Int32 y, Rectangle rectangle)
        {
            SetWindowPosition(x, y, rectangle.Size);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowPosition(Point position, Size size)
        {
            SetWindowPosition(position.X, position.Y, size);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowPosition(Int32 x, Int32 y, Size size)
        {
            if (!TrySetWindowPosition(x, y, size))
            {
                WindowsInteropUtilities.ThrowLastWin32Exception();
            }
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetWindowPosition(Point position)
        {
            return TrySetWindowPosition(position.X, position.Y);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetWindowPosition(Int32 x, Int32 y)
        {
            return TrySetWindowPosition(x, y, Rectangle);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetWindowPosition(Point position, Rectangle rectangle)
        {
            return TrySetWindowPosition(position.X, position.Y, rectangle);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetWindowPosition(Int32 x, Int32 y, Rectangle rectangle)
        {
            return TrySetWindowPosition(x, y, rectangle.Size);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetWindowPosition(Point position, Size size)
        {
            return TrySetWindowPosition(position.X, position.Y, size);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrySetWindowPosition(Int32 x, Int32 y, Size size)
        {
            return SetWindowPos(ConsoleWindow, IntPtr.Zero, x, y, size.Width, size.Height, 0x4 | 0x10);
        }

        public static void CenterToScreen()
        {
            CenterToScreen(DefaultMonitorType.Primary);
        }

        public static void CenterToScreen(IScreen screen)
        {
            if (screen is null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            Rectangle rectangle = Rectangle;
            SetWindowPosition(screen.WorkingArea.Width / 2 - rectangle.Width / 2, screen.WorkingArea.Height / 2 - rectangle.Height / 2, rectangle.Size);
        }

        public static void CenterToScreen(DefaultMonitorType type)
        {
            MonitorInfo screen = WindowsDeviceUtilities.GetMonitorInfoFromWindow(ConsoleWindow, type);

            Rectangle rectangle = Rectangle;
            Rectangle area = screen.WorkingArea;

            SetWindowPosition(area.Width / 2 - rectangle.Width / 2, area.Height / 2 - rectangle.Height / 2, rectangle.Size);
        }

        public static Boolean? IsConsoleVisible
        {
            get
            {
                if (ConsoleWindow == IntPtr.Zero)
                {
                    return null;
                }

                WindowPlacement placement = new WindowPlacement();

                if (!GetWindowPlacement(ConsoleWindow, ref placement))
                {
                    return null;
                }

                WindowStateType state = (WindowStateType) placement.ShowCmd;
                return state switch
                {
                    WindowStateType.Hide => false,
                    WindowStateType.Normal => true,
                    WindowStateType.Maximize => true,
                    WindowStateType.Minimize => true,
                    WindowStateType.Restore => true,
                    WindowStateType.Show => true,
                    WindowStateType.ShowMinimized => true,
                    WindowStateType.ShowNoActivate => true,
                    WindowStateType.ShowMininimizedNoActive => true,
                    WindowStateType.ShowNormalNoActivate => true,
                    _ => throw new EnumUndefinedOrNotSupportedException<WindowStateType>(state, nameof(state), null)
                };
            }
            set
            {
                if (value is null)
                {
                    return;
                }

                if (ConsoleWindow == IntPtr.Zero)
                {
                    return;
                }

                if (value.Value)
                {
                    UserInterfaceUtilities.ShowWindow(ConsoleWindow, WindowStateType.Show);
                    return;
                }

                UserInterfaceUtilities.ShowWindow(ConsoleWindow, WindowStateType.Hide);
            }
        }

        public static WindowStateType? ConsoleWindowState
        {
            get
            {
                if (ConsoleWindow == IntPtr.Zero)
                {
                    return null;
                }

                WindowPlacement placement = new WindowPlacement();

                if (!GetWindowPlacement(ConsoleWindow, ref placement))
                {
                    return null;
                }

                return (WindowStateType) placement.ShowCmd;
            }
            set
            {
                if (value is null)
                {
                    return;
                }

                if (ConsoleWindow == IntPtr.Zero)
                {
                    return;
                }

                UserInterfaceUtilities.ShowWindow(ConsoleWindow, value.Value);
            }
        }

        private const UInt32 VTProcessing = 0x0004;

        public static Boolean? IsVTCode
        {
            get
            {
                return GetMode(WindowsConsoleUtilities.ConsoleOutputHandle, VTProcessing);
            }
            set
            {
                ChangeMode(WindowsConsoleUtilities.ConsoleOutputHandle, value, VTProcessing);
            }
        }

        /// <summary>
        /// This flag enables the user to use the mouse to select and edit text. To enable
        /// this option, you must also set the ExtendedFlags flag.
        /// </summary>
        private const UInt32 QuickEditMode = 64;

        // ExtendedFlags must be combined with
        // InsertMode and QuickEditMode when setting
        /// <summary>
        /// ExtendedFlags must be enabled in order to enable InsertMode or QuickEditMode.
        /// </summary>
        private const UInt32 ExtendedFlags = 128;

        private const UInt32 QuickEditFlags = QuickEditMode | ExtendedFlags;

        public static Boolean? IsQuickEditEnabled
        {
            get
            {
                return GetMode(WindowsConsoleUtilities.ConsoleInputHandle, QuickEditFlags);
            }
            set
            {
                if (!value.HasValue)
                {
                    return;
                }

                if (value.Value)
                {
                    ChangeMode(WindowsConsoleUtilities.ConsoleInputHandle, true, QuickEditFlags);
                    return;
                }

                ChangeMode(WindowsConsoleUtilities.ConsoleInputHandle, false, QuickEditMode);
            }
        }

        private const UInt32 EchoInputMode = 0x0004;
        private const UInt32 WindowInputMode = 0x0008;
        private const UInt32 MouseInputMode = 0x0010;

        public static Boolean? IsEchoInputEnabled
        {
            get
            {
                return GetMode(WindowsConsoleUtilities.ConsoleInputHandle, EchoInputMode);
            }
            set
            {
                ChangeMode(WindowsConsoleUtilities.ConsoleInputHandle, value, EchoInputMode);
            }
        }

        public static Boolean? IsWindowInputEnabled
        {
            get
            {
                return GetMode(WindowsConsoleUtilities.ConsoleInputHandle, WindowInputMode);
            }
            set
            {
                ChangeMode(WindowsConsoleUtilities.ConsoleInputHandle, value, WindowInputMode);
            }
        }

        public static Boolean? IsMouseInputEnabled
        {
            get
            {
                return GetMode(WindowsConsoleUtilities.ConsoleInputHandle, MouseInputMode);
            }
            set
            {
                ChangeMode(WindowsConsoleUtilities.ConsoleInputHandle, value, MouseInputMode);
            }
        }

        /*

        public static Boolean ConsoleExitButtonEnabled
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public static Boolean ConsoleMaximizeButtonEnabled
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public static Boolean ConsoleMinimizeButtonEnabled
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        */

        private static Boolean OnConsoleExit(ConsoleCtrlType type)
        {
            CancelEventArgs<ConsoleCtrlType> handler = new CancelEventArgs<ConsoleCtrlType>(type);
            ConsoleExit?.Invoke(handler);
            return handler.Cancel;
        }
    }

    [StaticInitializerRequired]
    internal sealed class ConsoleFontInfo : NetExtender.Types.Console.ConsoleFontInfo
    {
        internal static IConsoleFont New
        {
            get
            {
                return new Font();
            }
        }
        
        static ConsoleFontInfo()
        {
            Font? Current()
            {
                if (ConsoleWindowUtilities.Font is not { } info)
                {
                    return null;
                }

                return new Font
                {
                    FontName = info.FontName,
                    Index = info.Index,
                    Width = info.Width,
                    Size = info.Size,
                    Family = info.Family,
                    Weight = info.Weight
                };
            }

            NetExtender.Types.Console.ConsoleFontInfo.Current = Current;
        }

        private ConsoleFontInfo()
        {
        }
    }
}