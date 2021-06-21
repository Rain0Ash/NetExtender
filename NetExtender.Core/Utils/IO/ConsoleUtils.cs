// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utils.UserInterface;
using NetExtender.Utils.System;
using NetExtender.Utils.System.Devices;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.IO
{
    public enum ConsoleInputType
    {
        None,
        Line,
        KeyInfo,
        KeyInfoIntercept,
        KeyCode
    }

    public enum ConsoleCtrlType
    {
        CtrlCEvent = 0,
        CtrlBreakEvent = 1,
        CtrlCloseEvent = 2,
        CtrlLogoffEvent = 5,
        CtrlShutdownEvent = 6,
        CtrlTaskManagerClosing = 10
    }

    public static partial class ConsoleUtils
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(Int32 nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Boolean CancelIoEx(IntPtr handle, IntPtr lpOverlapped);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, Int32 x, Int32 y, Int32 cx, Int32 cy, Int32 flags);

        [StructLayout(LayoutKind.Sequential)]
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
            [MarshalAs(UnmanagedType.LPArray), Out]
            ConsoleFont[] fonts);

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
            public readonly String FontName;

            public FontInfo(Int16 size)
                : this(null, size)
            {
            }
            
            public FontInfo(String name, Int16 size)
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

        private static readonly IntPtr ConsoleInputHandle = GetStdHandle(-10);
        private static readonly IntPtr ConsoleOutputHandle = GetStdHandle(-11);
        private static readonly IntPtr ConsoleErrorHandle = GetStdHandle(-12);

        /// <summary>
        /// Invoked when <see cref="HandleExit"/> is true.
        /// Handle only CTRL+C and CTRL+Break.
        /// </summary>
        public static event TypeHandler<TypeCancelEventArgs<ConsoleCtrlType>> ConsoleExit;

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

        private static Icon icon;

        public static Icon ConsoleIcon
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

        public static ConsoleFont[] ConsoleFonts
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

                return GetConsoleFontInfo(ConsoleOutputHandle, false, (UInt32) fonts.Length, fonts) ? fonts : null;
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

                ConsoleFont[] fonts = ConsoleFonts;

                if (fonts is null)
                {
                    return;
                }

                if (value >= fonts.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                SetConsoleFont(ConsoleOutputHandle, value);
            }
        }

        public static FontInfo Font
        {
            get
            {
                FontInfo font = FontInfo.Create();

                if (!GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref font))
                {
                    InteropUtils.ThrowLastWin32Exception();
                }

                return font;
            }
            set
            {
                if (!SetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref value))
                {
                    InteropUtils.ThrowLastWin32Exception();
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
                return GUIUtils.GetWindowRectangle(ConsoleWindow);
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
                InteropUtils.ThrowLastWin32Exception();
            }
        }

        [Obsolete]
        public static void CenterToScreenByScreen()
        {
            Rectangle rectangle = Rectangle;
            Screen screen = Screen.FromHandle(ConsoleWindow);

            Size size = rectangle.Size;
            SetWindowPosition(screen.WorkingArea.Width / 2 - rectangle.Width / 2, screen.WorkingArea.Height / 2 - rectangle.Height / 2, size);
        }

        public static void CenterToScreen()
        {
            CenterToScreen(DefaultMonitorType.Primary);
        }

        public static void CenterToScreen(DefaultMonitorType type)
        {
            MonitorInfo screen = DeviceUtils.GetMonitorInfoFromWindow(ConsoleWindow, type);

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
                    GUIUtils.ShowWindow(ConsoleWindow, WindowStateType.Show);
                    return;
                }

                GUIUtils.ShowWindow(ConsoleWindow, WindowStateType.Hide);
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

                GUIUtils.ShowWindow(ConsoleWindow, value.Value);
            }
        }

        private const UInt32 VTProcessing = 0x0004;

        public static Boolean? VTCode
        {
            get
            {
                return GetMode(ConsoleOutputHandle, VTProcessing);
            }
            set
            {
                ChangeMode(ConsoleOutputHandle, value, VTProcessing);
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
                return GetMode(ConsoleInputHandle, QuickEditFlags);
            }
            set
            {
                if (!value.HasValue)
                {
                    return;
                }

                if (value.Value)
                {
                    ChangeMode(ConsoleInputHandle, true, QuickEditFlags);
                    return;
                }

                ChangeMode(ConsoleInputHandle, false, QuickEditMode);
            }
        }

        private const UInt32 EchoInputMode = 0x0004;
        private const UInt32 WindowInputMode = 0x0008;
        private const UInt32 MouseInputMode = 0x0010;

        public static Boolean? IsEchoInputEnabled
        {
            get
            {
                return GetMode(ConsoleInputHandle, EchoInputMode);
            }
            set
            {
                ChangeMode(ConsoleInputHandle, value, EchoInputMode);
            }
        }

        public static Boolean? IsWindowInputEnabled
        {
            get
            {
                return GetMode(ConsoleInputHandle, WindowInputMode);
            }
            set
            {
                ChangeMode(ConsoleInputHandle, value, WindowInputMode);
            }
        }

        public static Boolean? IsMouseInputEnabled
        {
            get
            {
                return GetMode(ConsoleInputHandle, MouseInputMode);
            }
            set
            {
                ChangeMode(ConsoleInputHandle, value, MouseInputMode);
            }
        }

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

        public static event ConsoleCancelEventHandler CancelKeyPress
        {
            add
            {
                Console.CancelKeyPress += value;
            }
            remove
            {
                Console.CancelKeyPress -= value;
            }
        }

        public static Boolean HandleExit
        {
            set
            {
                ConsoleExitHandler.ExitHandle = value;
            }
        }

        public static IWindowExitHandler WindowExitHandler
        {
            get
            {
                return ConsoleExitHandler.ExitWindow;
            }
            set
            {
                ConsoleExitHandler.ExitWindow = value;
            }
        }

        public static void SetDefaultWindowExitHandler(Boolean force = false)
        {
            ConsoleExitHandler.SetDefaultForm(force);
        }

        private static Boolean OnConsoleExit(ConsoleCtrlType type)
        {
            TypeCancelEventArgs<ConsoleCtrlType> handler = new TypeCancelEventArgs<ConsoleCtrlType>(type);
            ConsoleExit?.Invoke(handler);
            return handler.Cancel;
        }

        public static event TypeHandler<TypeHandledEventArgs<String>> ConsoleLineInput;
        public static event TypeHandler<TypeHandledEventArgs<ConsoleKeyInfo>> ConsoleKeyInfoInput;
        public static event TypeHandler<TypeHandledEventArgs<Int32>> ConsoleKeyCodeInput;

        private static void OnConsoleLineInput(String line)
        {
            ConsoleLineInput?.Invoke(new TypeHandledEventArgs<String>(line));
        }

        private static void OnConsoleKeyInfoInput(ConsoleKeyInfo info)
        {
            ConsoleKeyInfoInput?.Invoke(new TypeHandledEventArgs<ConsoleKeyInfo>(info));
        }

        private static void OnConsoleKeyCodeInput(Int32 code)
        {
            ConsoleKeyCodeInput?.Invoke(new TypeHandledEventArgs<Int32>(code));
        }

        public static ConsoleInputType AsyncInputType
        {
            get
            {
                return AsyncInput.InputType;
            }
            set
            {
                AsyncInput.InputType = value;
            }
        }

        private static void StopRead()
        {
            CancelIoEx(ConsoleInputHandle, IntPtr.Zero);
        }

        private static async Task<T> InputAsync<T>(Func<T> func, CancellationToken token)
        {
            try
            {
                return await Task.Run(func, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                StopRead();
                return default;
            }
        }

        private static async Task<T> InputAsync<T>(Func<T> func, Int32 milli, CancellationToken token)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            try
            {
                Task<T> read = InputAsync(func, source.Token);
                Task delay = Task.Delay(milli, token);

                if (await Task.WhenAny(read, delay).ConfigureAwait(false) != delay)
                {
                    return await read.ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
            }

            source.Cancel();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Int32> ReadAsync(CancellationToken token)
        {
            return InputAsync(Console.Read, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Int32> ReadAsync(Int32 milli)
        {
            return ReadAsync(milli, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Int32> ReadAsync(Int32 milli, CancellationToken token)
        {
            return InputAsync(Console.Read, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConsoleKeyInfo ReadKeyIntercept()
        {
            return Console.ReadKey(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyAsync(CancellationToken token)
        {
            return InputAsync(Console.ReadKey, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyInterceptAsync(CancellationToken token)
        {
            return InputAsync(ReadKeyIntercept, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyAsync(Int32 milli)
        {
            return ReadKeyAsync(milli, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyInterceptAsync(Int32 milli)
        {
            return ReadKeyInterceptAsync(milli, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyAsync(Int32 milli, CancellationToken token)
        {
            return InputAsync(Console.ReadKey, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyInterceptAsync(Int32 milli, CancellationToken token)
        {
            return InputAsync(ReadKeyIntercept, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReadLineAsync(CancellationToken token)
        {
            return InputAsync(Console.ReadLine, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReadLineAsync(Int32 milli)
        {
            return ReadLineAsync(milli, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReadLineAsync(Int32 milli, CancellationToken token)
        {
            return InputAsync(Console.ReadLine, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CastAs<T>()
        {
            return CastAs<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CastAs<T>(CultureInfo info)
        {
            String read = Console.ReadLine();
            return read is null ? default : read.CastConvert<T>(info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CastReadAsAsync<T>(CancellationToken token)
        {
            return CastReadAsAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> CastReadAsAsync<T>(CultureInfo info, CancellationToken token)
        {
            String read = await ReadLineAsync(token).ConfigureAwait(false);
            return read is null ? default : read.CastConvert<T>(info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CastReadAsAsync<T>(Int32 milli)
        {
            return CastReadAsAsync<T>(CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> CastReadAsAsync<T>(CultureInfo info, Int32 milli)
        {
            String read = await ReadLineAsync(milli).ConfigureAwait(false);
            return read is null ? default : read.CastConvert<T>(info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CastReadAsAsync<T>(Int32 milli, CancellationToken token)
        {
            return CastReadAsAsync<T>(CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> CastReadAsAsync<T>(CultureInfo info, Int32 milli, CancellationToken token)
        {
            String read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read is null ? default : read.CastConvert<T>(info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadAs<T>()
        {
            return ReadAs<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadAs<T>(CultureInfo info)
        {
            String read = Console.ReadLine();
            return read is null ? default : read.Convert<T>(info);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadAs<T>(ParseHandler<String, T> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return Console.ReadLine().Convert(converter);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadAs<T>(TryParseHandler<String, T> converter, T @default)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return Console.ReadLine().Convert(converter, @default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadAs<T>(TryParseHandler<String, T> converter, Func<T> generator)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            return Console.ReadLine().Convert(converter, generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadAs<T>(TryParseHandler<String, T> converter, Func<String, T> generator)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            return Console.ReadLine().Convert(converter, generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(CancellationToken token)
        {
            return ReadAsAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(CultureInfo info, CancellationToken token)
        {
            String read = await ReadLineAsync(token).ConfigureAwait(false);
            return read is null ? default : read.Convert<T>(info);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(ParseHandler<String, T> converter, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String read = await ReadLineAsync(token).ConfigureAwait(false);
            return read.Convert(converter);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(TryParseHandler<String, T> converter, T @default, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String read = await ReadLineAsync(token).ConfigureAwait(false);
            return read.Convert(converter, @default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(TryParseHandler<String, T> converter, Func<T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String read = await ReadLineAsync(token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(TryParseHandler<String, T> converter, Func<String, T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String read = await ReadLineAsync(token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milli)
        {
            return ReadAsAsync<T>(CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(CultureInfo info, Int32 milli)
        {
            return ReadAsAsync<T>(info, milli, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milli, ParseHandler<String, T> converter)
        {
            return ReadAsAsync(milli, converter, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String, T> converter, T @default)
        {
            return ReadAsAsync(milli, converter, @default, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String, T> converter, Func<T> generator)
        {
            return ReadAsAsync(milli, converter, generator, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String, T> converter, Func<String, T> generator)
        {
            return ReadAsAsync(milli, converter, generator, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milli, CancellationToken token)
        {
            return ReadAsAsync<T>(CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(CultureInfo info, Int32 milli, CancellationToken token)
        {
            String read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read is null ? default : read.Convert<T>(info);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milli, ParseHandler<String, T> converter, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read.Convert(converter);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String, T> converter, T @default, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read.Convert(converter, @default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String, T> converter, Func<T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String, T> converter, Func<String, T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> CastReadAsEnumerable<T>()
        {
            return CastReadAsEnumerable<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> CastReadAsEnumerable<T>(CultureInfo info)
        {
            return CastReadAsEnumerable<T>(StringUtils.DefaultSeparator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> CastReadAsEnumerable<T>(String separator, CultureInfo info)
        {
            return Console.ReadLine()?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> CastReadAsEnumerable<T>(String[] separators, CultureInfo info)
        {
            return Console.ReadLine()?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String separator, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String[] separators, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(CultureInfo info, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String separator, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String[] separators, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(Int32 milli)
        {
            return CastAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String separator, Int32 milli)
        {
            return CastAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String[] separators, Int32 milli)
        {
            return CastAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(CultureInfo info, Int32 milli)
        {
            return CastAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(Int32 milli, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String separator, Int32 milli, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String[] separators, Int32 milli, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> CastAsEnumerableAsync<T>(CultureInfo info, Int32 milli, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> CastAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ReadAsEnumerable<T>()
        {
            return ReadAsEnumerable<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ReadAsEnumerable<T>(CultureInfo info)
        {
            return ReadAsEnumerable<T>(StringUtils.DefaultSeparator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ReadAsEnumerable<T>(String separator, CultureInfo info)
        {
            return Console.ReadLine()?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ReadAsEnumerable<T>(String[] separators, CultureInfo info)
        {
            return Console.ReadLine()?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String separator, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String[] separators, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(CultureInfo info, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String separator, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(Int32 milli)
        {
            return ReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String separator, Int32 milli)
        {
            return ReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String[] separators, Int32 milli)
        {
            return ReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(CultureInfo info, Int32 milli)
        {
            return ReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(Int32 milli, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String separator, Int32 milli, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String[] separators, Int32 milli, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(CultureInfo info, Int32 milli, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> ReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> TryReadAsEnumerable<T>()
        {
            return TryReadAsEnumerable<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> TryReadAsEnumerable<T>(CultureInfo info)
        {
            return TryReadAsEnumerable<T>(StringUtils.DefaultSeparator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> TryReadAsEnumerable<T>(String separator, CultureInfo info)
        {
            return Console.ReadLine()?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> TryReadAsEnumerable<T>(String[] separators, CultureInfo info)
        {
            return Console.ReadLine()?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String separator, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String[] separators, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(CultureInfo info, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String separator, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(Int32 milli)
        {
            return TryReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String separator, Int32 milli)
        {
            return TryReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String[] separators, Int32 milli)
        {
            return TryReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(CultureInfo info, Int32 milli)
        {
            return TryReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(Int32 milli, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String separator, Int32 milli, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String[] separators, Int32 milli, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(CultureInfo info, Int32 milli, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>> TryReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearLine()
        {
            ClearLine(Console.CursorTop);
        }

        public static void ClearLine(Int32 line)
        {
            Console.SetCursorPosition(0, line);
            Console.Write(new String(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, line);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear()
        {
            Console.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value)
        {
            Write(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, IFormatProvider provider)
        {
            Write(value, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, EscapeType escape)
        {
            Write(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, EscapeType escape, IFormatProvider provider)
        {
            ToConsole(value, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground)
        {
            Write(value, foreground, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, IFormatProvider provider)
        {
            Write(value, foreground, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, EscapeType escape)
        {
            Write(value, foreground, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, EscapeType escape, IFormatProvider provider)
        {
            ToConsole(value, foreground, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, ConsoleColor background)
        {
            Write(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, ConsoleColor background, IFormatProvider provider)
        {
            Write(value, foreground, background, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape)
        {
            Write(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, IFormatProvider provider)
        {
            ToConsole(value, foreground, background, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine()
        {
            WriteLine(String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value)
        {
            WriteLine(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, IFormatProvider provider)
        {
            WriteLine(value, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, EscapeType escape)
        {
            WriteLine(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, EscapeType escape, IFormatProvider provider)
        {
            ToConsole(value, escape, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground)
        {
            WriteLine(value, foreground, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, IFormatProvider provider)
        {
            WriteLine(value, foreground, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, EscapeType escape)
        {
            WriteLine(value, foreground, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, EscapeType escape, IFormatProvider provider)
        {
            ToConsole(value, foreground, escape, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background)
        {
            WriteLine(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background, IFormatProvider provider)
        {
            WriteLine(value, foreground, background, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape)
        {
            WriteLine(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, IFormatProvider provider)
        {
            ToConsole(value, foreground, background, escape, true, provider);
        }

        private static readonly Object ConsoleLock = new Object();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value)
        {
            ToConsole(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, EscapeType escape)
        {
            ToConsole(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, IFormatProvider provider)
        {
            ToConsole(value, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, EscapeType escape, IFormatProvider provider)
        {
            ToConsole(value, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, Boolean newLine)
        {
            ToConsole(value, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, EscapeType escape, Boolean newLine)
        {
            ToConsole(value, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, Boolean newLine, EscapeType escape)
        {
            ToConsole(value, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, Boolean newLine, IFormatProvider provider)
        {
            ToConsole(value, newLine, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, EscapeType escape, Boolean newLine, IFormatProvider provider)
        {
            ToConsole(value, newLine, escape, provider);
        }

        public static void ToConsole<T>(this T value, Boolean newLine, EscapeType escape, IFormatProvider provider)
        {
            String str = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtils.NullString;

            if (newLine)
            {
                Console.WriteLine(str);
            }
            else
            {
                Console.Write(str);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground)
        {
            ToConsole(value, foreground, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape)
        {
            ToConsole(value, foreground, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, IFormatProvider provider)
        {
            ToConsole(value, foreground, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape, IFormatProvider provider)
        {
            ToConsole(value, foreground, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine)
        {
            ToConsole(value, foreground, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape, Boolean newLine)
        {
            ToConsole(value, foreground, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine, EscapeType escape)
        {
            ToConsole(value, foreground, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine, IFormatProvider provider)
        {
            ToConsole(value, foreground, newLine, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape, Boolean newLine, IFormatProvider provider)
        {
            ToConsole(value, foreground, newLine, escape, provider);
        }

        public static void ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine, EscapeType escape, IFormatProvider provider)
        {
            lock (ConsoleLock)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = foreground;

                ToConsole(value, newLine, escape, provider);

                Console.ForegroundColor = color;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background)
        {
            ToConsole(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape)
        {
            ToConsole(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, IFormatProvider provider)
        {
            ToConsole(value, foreground, background, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, IFormatProvider provider)
        {
            ToConsole(value, foreground, background, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine)
        {
            ToConsole(value, foreground, background, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, Boolean newLine)
        {
            ToConsole(value, foreground, background, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine, EscapeType escape)
        {
            ToConsole(value, foreground, background, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine, IFormatProvider provider)
        {
            ToConsole(value, foreground, background, newLine, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, Boolean newLine, IFormatProvider provider)
        {
            ToConsole(value, foreground, background, newLine, escape, provider);
        }

        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine, EscapeType escape, IFormatProvider provider)
        {
            lock (ConsoleLock)
            {
                ConsoleColor color = Console.BackgroundColor;
                Console.BackgroundColor = background;

                ToConsole(value, foreground, newLine, escape, provider);

                Console.BackgroundColor = color;
            }
        }
    }
}