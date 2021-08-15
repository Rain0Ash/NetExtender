// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Core.Workstation.Interfaces;
using NetExtender.Events;
using NetExtender.UserInterface;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Static;
using NetExtender.Utilities.Windows;

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
        public readonly struct ConsoleFont
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
        private static extern Boolean GetConsoleFontInfo(IntPtr hOutput, [MarshalAs(UnmanagedType.Bool)] Boolean bMaximize, UInt32 count,
            [MarshalAs(UnmanagedType.LPArray), Out] ConsoleFont[] fonts);

        [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public readonly struct FontInfo
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
        private static extern Boolean SetCurrentConsoleFontEx(IntPtr hConsoleOutput, Boolean MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetCurrentConsoleFontEx(IntPtr hConsoleOutput, Boolean MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

        [DllImport("kernel32.dll")]
        private static extern UInt32 GetLastError();

        public static event TypeHandler<TypeCancelEventArgs<ConsoleCtrlType>> ConsoleExit = null!;

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

        public static ConsoleFont[]? ConsoleFonts
        {
            get
            {
                if (ConsoleWindow == IntPtr.Zero)
                {
                    return null;
                }

                ConsoleFont[] fonts = new ConsoleFont[GetNumberOfConsoleFonts()];

                if (fonts.Length <= 0)
                {
                    return null;
                }

                return GetConsoleFontInfo(WindowsConsoleUtilities.ConsoleOutputHandle, false, (UInt32) fonts.Length, fonts) ? fonts : null;
            }
        }

        public static UInt32 ConsoleFontIndex
        {
            set
            {
                if (ConsoleWindow == IntPtr.Zero)
                {
                    return;
                }

                ConsoleFont[]? fonts = ConsoleFonts;

                if (fonts is null)
                {
                    return;
                }

                if (value >= fonts.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                SetConsoleFont(WindowsConsoleUtilities.ConsoleOutputHandle, value);
            }
        }

        public static FontInfo Font
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

        public static Int16 FontSize
        {
            get
            {
                return Font.Size;
            }
            set
            {
                SetCurrentFont(value);
            }
        }

        public static void SetCurrentFont(Int16 size = 0)
        {
            SetCurrentFont(null, size);
        }

        public static void SetCurrentFont(String? font, Int16 size = 0)
        {
            Font = new FontInfo(font ?? Font.FontName, size > 0 ? size : Font.Size);
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
        public static (Int32 x, Int32 y) GetCursorPosition()
        {
            return Console.GetCursorPosition();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCursorPosition()
        {
            ResetCursorPosition();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetCursorPosition()
        {
            SetCursorPosition(0, 0);
        }

        /// <inheritdoc cref="Console.SetCursorPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCursorPosition(Int32 x, Int32 y)
        {
            Console.SetCursorPosition(x, y);
        }

        /// <inheritdoc cref="Console.SetBufferSize"/>
        public static Size Buffer
        {
            get
            {
                return new Size(Console.BufferWidth, Console.BufferHeight);
            }
            set
            {
                SetBufferSize(value.Width, value.Height);
            }
        }

        /// <inheritdoc cref="Console.SetBufferSize"/>
        public static void SetBufferSize(Int32 width, Int32 height)
        {
            Console.SetBufferSize(width, height);
        }

        /// <inheritdoc cref="Console.SetWindowSize"/>
        public static Size Size
        {
            get
            {
                return new Size(Console.WindowWidth, Console.WindowHeight);
            }
            set
            {
                SetWindowSize(Math.Min(value.Width, Console.LargestWindowWidth), Math.Min(value.Height, Console.LargestWindowHeight));
            }
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
        public static Graphics GetConsoleGraphics()
        {
            return Graphics.FromHwnd(ConsoleWindow);
        }

        /// <inheritdoc cref="Console.SetWindowSize"/>
        public static void SetWindowSize(Int32 width, Int32 height)
        {
            Console.SetWindowSize(width, height);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        public static Point Position
        {
            get
            {
                return new Point(Console.WindowLeft, Console.WindowTop);
            }
            set
            {
                SetWindowPosition(value.X, value.Y);
            }
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowPosition(Int32 x, Int32 y)
        {
            SetWindowPosition(x, y, Rectangle);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowPosition(Int32 x, Int32 y, Rectangle rectangle)
        {
            SetWindowPosition(x, y, rectangle.Size);
        }

        /// <inheritdoc cref="Console.SetWindowPosition"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWindowPosition(Int32 x, Int32 y, Size size)
        {
            if (!SetWindowPos(ConsoleWindow, IntPtr.Zero, x, y, size.Width, size.Height, 0x4 | 0x10))
            {
                WindowsInteropUtilities.ThrowLastWin32Exception();
            }
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

        public static void CenterToScreen()
        {
            CenterToScreen(DefaultMonitorType.Primary);
        }

        public static void CenterToScreen(DefaultMonitorType type)
        {
            MonitorInfo screen = DeviceUtilities.GetMonitorInfoFromWindow(ConsoleWindow, type);

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

                return (WindowStateType) placement.ShowCmd switch
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
                    _ => throw new NotSupportedException()
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

        public static Boolean? VTCode
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
            TypeCancelEventArgs<ConsoleCtrlType> handler = new TypeCancelEventArgs<ConsoleCtrlType>(type);
            ConsoleExit?.Invoke(handler);
            return handler.Cancel;
        }
    }
}