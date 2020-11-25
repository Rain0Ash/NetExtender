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
using NetExtender.Events.Args;
using NetExtender.Types.Maps;
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
        [DllImport("user32.dll")]
        public static extern Int32 DeleteMenu(IntPtr hMenu, Int32 nPosition, Int32 wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, Boolean bRevert);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(Int32 nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Boolean CancelIoEx(IntPtr handle, IntPtr lpOverlapped);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        [DllImport("kernel32.dll")]
        private static extern Boolean GetConsoleMode(IntPtr hConsoleHandle, out UInt32 lpMode);

        [DllImport("kernel32.dll")]
        private static extern Boolean SetConsoleMode(IntPtr hConsoleHandle, UInt32 dwMode);

        [DllImport("kernel32.dll")]
        private static extern Boolean SetConsoleTitle(String lpConsoleTitle);

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

        public static Boolean IsConsoleVisible
        {
            set
            {
                if (ConsoleWindow == IntPtr.Zero)
                {
                    return;
                }

                if (value)
                {
                    ShowWindow(ConsoleWindow, 5);
                    return;
                }

                ShowWindow(ConsoleWindow, 0);
            }
        }

        private const UInt32 VTProcessing = 0x0004;

        public static Boolean? IsVTCodeEnabled
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
        
        private const Int32 MfBycommand = 0x00000000;
        public const Int32 ScClose = 0xF060;

        public static Boolean ConsoleExitButtonEnabled
        {
            set
            {
                throw new NotImplementedException();
                
                if (!value)
                {
                    DeleteMenu(GetSystemMenu(ConsoleWindow, false),ScClose, MfBycommand);
                }
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

        /*
        public static Boolean ConsoleListenerEnabled
        {
            get
            {
                return ConsoleListener.Running;
            }
            set
            {
                if (value)
                {
                    ConsoleListener.Start();
                    return;
                }
                
                ConsoleListener.Stop();
            }
        }
        */

        public static Boolean HandleExit
        {
            set
            {
                ConsoleExitHandler.ExitHandle = value;
            }
        }

        public static IConsoleWindowExitHandler WindowExitHandler
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
        public static Task<T> ReadAsAsync<T>(Int32 milli)
        {
            return ReadAsAsync<T>(CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(CultureInfo info, Int32 milli)
        {
            String read = await ReadLineAsync(milli).ConfigureAwait(false);
            return read is null ? default : read.Convert<T>(info);
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

        private static readonly IMap<Color, ConsoleColor> ColorMap = new Map<Color, ConsoleColor>
        {
            [Color.Black] = ConsoleColor.Black,
            [Color.Blue] = ConsoleColor.Blue,
            [Color.Cyan] = ConsoleColor.Cyan,
            [Color.Gray] = ConsoleColor.Gray,
            [Color.Green] = ConsoleColor.Green,
            [Color.Magenta] = ConsoleColor.Magenta,
            [Color.Red] = ConsoleColor.Red,
            [Color.White] = ConsoleColor.White,
            [Color.Yellow] = ConsoleColor.Yellow,
            [Color.DarkBlue] = ConsoleColor.DarkBlue,
            [Color.DarkCyan] = ConsoleColor.DarkCyan,
            [Color.DarkGray] = ConsoleColor.DarkGray,
            [Color.DarkGreen] = ConsoleColor.DarkGreen,
            [Color.DarkMagenta] = ConsoleColor.DarkMagenta,
            [Color.DarkRed] = ConsoleColor.DarkRed,
            [Color.Orange] = ConsoleColor.DarkYellow
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConsoleColor GetColor(Color color)
        {
            return ColorMap.TryGetValue(color, ConsoleColor.White);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetColor(ConsoleColor consoleColor)
        {
            return ColorMap.TryGetKey(consoleColor, Color.White);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, IFormatProvider info = null)
        {
            ToConsole(value, false, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor color, IFormatProvider info = null)
        {
            ToConsole(value, color, false, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor color, ConsoleColor bColor, IFormatProvider info = null)
        {
            ToConsole(value, color, bColor, false, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine()
        {
            WriteLine(String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, IFormatProvider info = null)
        {
            ToConsole(value, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor color, IFormatProvider info = null)
        {
            ToConsole(value, color, true, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor color, ConsoleColor bColor, IFormatProvider info = null)
        {
            ToConsole(value, color, bColor, true, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, IFormatProvider info)
        {
            ToConsole(value, true, info);
        }

        private static readonly Object ConsoleLock = new Object();
        
        public static void ToConsole<T>(this T value, Boolean newLine = true, IFormatProvider info = null)
        {
            String str = value.GetString(info ?? CultureInfo.InvariantCulture) ?? "null";

            if (newLine)
            {
                Console.WriteLine(str);
            }
            else
            {
                Console.Write(str);
            }
        }
        
        public static void ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine = true, IFormatProvider info = null)
        {
            lock (ConsoleLock)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = foreground;

                ToConsole(value, newLine, info);

                Console.ForegroundColor = color;
            }
        }

        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine = true,
            IFormatProvider info = null)
        {
            lock (ConsoleLock)
            {
                ConsoleColor color = Console.BackgroundColor;
                Console.BackgroundColor = background;

                ToConsole(value, foreground, newLine, info);

                Console.BackgroundColor = color;
            }
        }
    }
}