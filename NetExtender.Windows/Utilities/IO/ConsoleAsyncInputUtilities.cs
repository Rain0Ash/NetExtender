// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Events;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.IO
{
    public static partial class ConsoleAsyncInputUtilities
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Boolean CancelIoEx(IntPtr handle, IntPtr lpOverlapped);

        public static event TypeHandler<HandledEventArgs<String?>>? ConsoleLineInput;
        public static event TypeHandler<HandledEventArgs<ConsoleKeyInfo>>? ConsoleKeyInfoInput;
        public static event TypeHandler<HandledEventArgs<Int32>>? ConsoleKeyCodeInput;

        private static void OnConsoleLineInput(String? line)
        {
            ConsoleLineInput?.Invoke(new HandledEventArgs<String?>(line));
        }

        private static void OnConsoleKeyInfoInput(ConsoleKeyInfo info)
        {
            ConsoleKeyInfoInput?.Invoke(new HandledEventArgs<ConsoleKeyInfo>(info));
        }

        private static void OnConsoleKeyCodeInput(Int32 code)
        {
            ConsoleKeyCodeInput?.Invoke(new HandledEventArgs<Int32>(code));
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
            CancelIoEx(WindowsConsoleUtilities.ConsoleInputHandle, IntPtr.Zero);
        }

        private static async Task<T?> InputAsync<T>(Func<T> handler, CancellationToken token)
        {
            try
            {
                return await Task.Run(handler, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                StopRead();
                return default;
            }
        }

        private static Task<T?> InputAsync<T>(Func<T> handler, Int32 milliseconds, CancellationToken token)
        {
            return InputAsync(handler, TimeSpan.FromMilliseconds(milliseconds), token);
        }

        private static async Task<T?> InputAsync<T>(Func<T> handler, TimeSpan timeout, CancellationToken token)
        {
            using CancellationTokenSource source = new CancellationTokenSource();

            try
            {
                Task<T?> read = InputAsync(handler, source.Token);
                Task delay = Task.Delay(timeout, token);

                if (await Task.WhenAny(read, delay).ConfigureAwait(false) == read)
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
        public static Task<Int32> ReadAsync(Int32 milliseconds)
        {
            return ReadAsync(milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Int32> ReadAsync(TimeSpan timeout)
        {
            return ReadAsync(timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Int32> ReadAsync(Int32 milliseconds, CancellationToken token)
        {
            return InputAsync(Console.Read, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Int32> ReadAsync(TimeSpan timeout, CancellationToken token)
        {
            return InputAsync(Console.Read, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyAsync(CancellationToken token)
        {
            return InputAsync(Console.ReadKey, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyInterceptAsync(CancellationToken token)
        {
            return InputAsync(ConsoleUtilities.ReadKeyIntercept, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyAsync(Int32 milliseconds)
        {
            return ReadKeyAsync(milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyAsync(TimeSpan timeout)
        {
            return ReadKeyAsync(timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyInterceptAsync(Int32 milliseconds)
        {
            return ReadKeyInterceptAsync(milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyInterceptAsync(TimeSpan timeout)
        {
            return ReadKeyInterceptAsync(timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyAsync(Int32 milliseconds, CancellationToken token)
        {
            return InputAsync(Console.ReadKey, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyAsync(TimeSpan timeout, CancellationToken token)
        {
            return InputAsync(Console.ReadKey, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyInterceptAsync(Int32 milliseconds, CancellationToken token)
        {
            return InputAsync(ConsoleUtilities.ReadKeyIntercept, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyInterceptAsync(TimeSpan timeout, CancellationToken token)
        {
            return InputAsync(ConsoleUtilities.ReadKeyIntercept, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> ReadLineAsync(CancellationToken token)
        {
            return InputAsync(Console.ReadLine, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> ReadLineAsync(Int32 milliseconds)
        {
            return ReadLineAsync(milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> ReadLineAsync(TimeSpan timeout)
        {
            return ReadLineAsync(timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> ReadLineAsync(Int32 milliseconds, CancellationToken token)
        {
            return InputAsync(Console.ReadLine, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> ReadLineAsync(TimeSpan timeout, CancellationToken token)
        {
            return InputAsync(Console.ReadLine, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? CastAs<T>()
        {
            return CastAs<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? CastAs<T>(CultureInfo? info)
        {
            String? read = Console.ReadLine();
            return read is not null ? read.CastConvert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> CastReadAsAsync<T>(CancellationToken token)
        {
            return CastReadAsAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T?> CastReadAsAsync<T>(CultureInfo? info, CancellationToken token)
        {
            String? read = await ReadLineAsync(token).ConfigureAwait(false);
            return read is not null ? read.CastConvert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> CastReadAsAsync<T>(Int32 milliseconds)
        {
            return CastReadAsAsync<T>(milliseconds, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> CastReadAsAsync<T>(TimeSpan timeout)
        {
            return CastReadAsAsync<T>(timeout, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> CastReadAsAsync<T>(Int32 milliseconds, CultureInfo? info)
        {
            return CastReadAsAsync<T>(TimeSpan.FromMilliseconds(milliseconds), info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T?> CastReadAsAsync<T>(TimeSpan timeout, CultureInfo? info)
        {
            String? read = await ReadLineAsync(timeout).ConfigureAwait(false);
            return read is not null ? read.CastConvert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> CastReadAsAsync<T>(Int32 milliseconds, CancellationToken token)
        {
            return CastReadAsAsync<T>(milliseconds, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> CastReadAsAsync<T>(TimeSpan timeout, CancellationToken token)
        {
            return CastReadAsAsync<T>(timeout, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> CastReadAsAsync<T>(Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            return CastReadAsAsync<T>(TimeSpan.FromMilliseconds(milliseconds), info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T?> CastReadAsAsync<T>(TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            String? read = await ReadLineAsync(timeout, token).ConfigureAwait(false);
            return read is not null ? read.CastConvert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? ReadAs<T>()
        {
            return ReadAs<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? ReadAs<T>(CultureInfo? info)
        {
            String? read = Console.ReadLine();
            return read is not null ? read.Convert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadAs<T>(ParseHandler<String?, T> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return Console.ReadLine().Convert(converter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadAs<T>(TryParseHandler<String?, T> converter, T alternate)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return Console.ReadLine().Convert(converter, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadAs<T>(TryParseHandler<String?, T> converter, Func<T> generator)
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
        public static T ReadAs<T>(TryParseHandler<String?, T> converter, Func<String?, T> generator)
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
        public static Task<T?> ReadAsAsync<T>(CancellationToken token)
        {
            return ReadAsAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T?> ReadAsAsync<T>(CultureInfo? info, CancellationToken token)
        {
            String? read = await ReadLineAsync(token).ConfigureAwait(false);
            return read is not null ? read.Convert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(ParseHandler<String?, T> converter, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String? read = await ReadLineAsync(token).ConfigureAwait(false);
            return read.Convert(converter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(TryParseHandler<String?, T> converter, T alternate, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String? read = await ReadLineAsync(token).ConfigureAwait(false);
            return read.Convert(converter, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(TryParseHandler<String?, T> converter, Func<T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String? read = await ReadLineAsync(token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(TryParseHandler<String?, T> converter, Func<String?, T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String? read = await ReadLineAsync(token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(Int32 milliseconds)
        {
            return ReadAsAsync<T>(milliseconds, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(TimeSpan timeout)
        {
            return ReadAsAsync<T>(timeout, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(Int32 milliseconds, CultureInfo? info)
        {
            return ReadAsAsync<T>(milliseconds, info, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(TimeSpan timeout, CultureInfo? info)
        {
            return ReadAsAsync<T>(timeout, info, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milliseconds, ParseHandler<String?, T> converter)
        {
            return ReadAsAsync(milliseconds, converter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(TimeSpan timeout, ParseHandler<String?, T> converter)
        {
            return ReadAsAsync(timeout, converter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milliseconds, TryParseHandler<String?, T> converter, T alternate)
        {
            return ReadAsAsync(milliseconds, converter, alternate, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(TimeSpan timeout, TryParseHandler<String?, T> converter, T alternate)
        {
            return ReadAsAsync(timeout, converter, alternate, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milliseconds, TryParseHandler<String?, T> converter, Func<T> generator)
        {
            return ReadAsAsync(milliseconds, converter, generator, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(TimeSpan timeout, TryParseHandler<String?, T> converter, Func<T> generator)
        {
            return ReadAsAsync(timeout, converter, generator, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milliseconds, TryParseHandler<String?, T> converter, Func<String?, T> generator)
        {
            return ReadAsAsync(milliseconds, converter, generator, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(TimeSpan timeout, TryParseHandler<String?, T> converter, Func<String?, T> generator)
        {
            return ReadAsAsync(timeout, converter, generator, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(Int32 milliseconds, CancellationToken token)
        {
            return ReadAsAsync<T>(milliseconds, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(TimeSpan timeout, CancellationToken token)
        {
            return ReadAsAsync<T>(timeout, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T?> ReadAsAsync<T>(Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            String? read = await ReadLineAsync(milliseconds, token).ConfigureAwait(false);
            return read is not null ? read.Convert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T?> ReadAsAsync<T>(TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            String? read = await ReadLineAsync(timeout, token).ConfigureAwait(false);
            return read is not null ? read.Convert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milliseconds, ParseHandler<String?, T> converter, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String? read = await ReadLineAsync(milliseconds, token).ConfigureAwait(false);
            return read.Convert(converter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(TimeSpan timeout, ParseHandler<String?, T> converter, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String? read = await ReadLineAsync(timeout, token).ConfigureAwait(false);
            return read.Convert(converter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milliseconds, TryParseHandler<String?, T> converter, T alternate, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String? read = await ReadLineAsync(milliseconds, token).ConfigureAwait(false);
            return read.Convert(converter, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(TimeSpan timeout, TryParseHandler<String?, T> converter, T alternate, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String? read = await ReadLineAsync(timeout, token).ConfigureAwait(false);
            return read.Convert(converter, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milliseconds, TryParseHandler<String?, T> converter, Func<T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String? read = await ReadLineAsync(milliseconds, token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(TimeSpan timeout, TryParseHandler<String?, T> converter, Func<T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String? read = await ReadLineAsync(timeout, token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milliseconds, TryParseHandler<String?, T> converter, Func<String?, T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String? read = await ReadLineAsync(milliseconds, token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(TimeSpan timeout, TryParseHandler<String?, T> converter, Func<String?, T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String? read = await ReadLineAsync(timeout, token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(CultureInfo? info, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(Int32 milliseconds)
        {
            return CastAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(TimeSpan timeout)
        {
            return CastAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, Int32 milliseconds)
        {
            return CastAsEnumerableAsync<T>(separator, milliseconds, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, TimeSpan timeout)
        {
            return CastAsEnumerableAsync<T>(separator, timeout, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, Int32 milliseconds)
        {
            return CastAsEnumerableAsync<T>(separators, milliseconds, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, TimeSpan timeout)
        {
            return CastAsEnumerableAsync<T>(separators, timeout, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(Int32 milliseconds, CultureInfo? info)
        {
            return CastAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(TimeSpan timeout, CultureInfo? info)
        {
            return CastAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, Int32 milliseconds, CultureInfo? info)
        {
            return (await ReadLineAsync(milliseconds).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, TimeSpan timeout, CultureInfo? info)
        {
            return (await ReadLineAsync(timeout).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, Int32 milliseconds, CultureInfo? info)
        {
            return (await ReadLineAsync(milliseconds).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, TimeSpan timeout, CultureInfo? info)
        {
            return (await ReadLineAsync(timeout).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(Int32 milliseconds, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(TimeSpan timeout, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, Int32 milliseconds, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separator, milliseconds, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, TimeSpan timeout, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separator, timeout, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, Int32 milliseconds, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separators, milliseconds, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, TimeSpan timeout, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separators, timeout, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(milliseconds, token).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(timeout, token).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(milliseconds, token).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(timeout, token).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String separator, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String[] separators, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(CultureInfo? info, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String separator, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String[] separators, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(Int32 milliseconds)
        {
            return ReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(TimeSpan timeout)
        {
            return ReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String separator, Int32 milliseconds)
        {
            return ReadAsEnumerableAsync<T>(separator, milliseconds, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String separator, TimeSpan timeout)
        {
            return ReadAsEnumerableAsync<T>(separator, timeout, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String[] separators, Int32 milliseconds)
        {
            return ReadAsEnumerableAsync<T>(separators, milliseconds, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String[] separators, TimeSpan timeout)
        {
            return ReadAsEnumerableAsync<T>(separators, timeout, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(Int32 milliseconds, CultureInfo? info)
        {
            return ReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(TimeSpan timeout, CultureInfo? info)
        {
            return ReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String separator, Int32 milliseconds, CultureInfo? info)
        {
            return (await ReadLineAsync(milliseconds).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String separator, TimeSpan timeout, CultureInfo? info)
        {
            return (await ReadLineAsync(timeout).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String[] separators, Int32 milliseconds, CultureInfo? info)
        {
            return (await ReadLineAsync(milliseconds).ConfigureAwait(false))?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String[] separators, TimeSpan timeout, CultureInfo? info)
        {
            return (await ReadLineAsync(timeout).ConfigureAwait(false))?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(Int32 milliseconds, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(TimeSpan timeout, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String separator, Int32 milliseconds, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separator, milliseconds, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String separator, TimeSpan timeout, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separator, timeout, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String[] separators, Int32 milliseconds, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separators, milliseconds, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String[] separators, TimeSpan timeout, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separators, timeout, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String separator, Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(milliseconds, token).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String separator, TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(timeout, token).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String[] separators, Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(milliseconds, token).ConfigureAwait(false))?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T?>?> ReadAsEnumerableAsync<T>(String[] separators, TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(timeout, token).ConfigureAwait(false))?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(CultureInfo? info, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(Int32 milliseconds)
        {
            return TryReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(TimeSpan timeout)
        {
            return TryReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, Int32 milliseconds)
        {
            return TryReadAsEnumerableAsync<T>(separator, milliseconds, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, TimeSpan timeout)
        {
            return TryReadAsEnumerableAsync<T>(separator, timeout, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, Int32 milliseconds)
        {
            return TryReadAsEnumerableAsync<T>(separators, milliseconds, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, TimeSpan timeout)
        {
            return TryReadAsEnumerableAsync<T>(separators, timeout, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(Int32 milliseconds, CultureInfo? info)
        {
            return TryReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(TimeSpan timeout, CultureInfo? info)
        {
            return TryReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, Int32 milliseconds, CultureInfo? info)
        {
            return (await ReadLineAsync(milliseconds).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, TimeSpan timeout, CultureInfo? info)
        {
            return (await ReadLineAsync(timeout).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, Int32 milliseconds, CultureInfo? info)
        {
            return (await ReadLineAsync(milliseconds).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, TimeSpan timeout, CultureInfo? info)
        {
            return (await ReadLineAsync(timeout).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(Int32 milliseconds, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(TimeSpan timeout, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, Int32 milliseconds, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separator, milliseconds, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, TimeSpan timeout, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separator, timeout, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, Int32 milliseconds, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separators, milliseconds, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, TimeSpan timeout, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separators, timeout, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, milliseconds, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtilities.DefaultSeparator, timeout, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(milliseconds, token).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(timeout, token).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, Int32 milliseconds, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(milliseconds, token).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, TimeSpan timeout, CultureInfo? info, CancellationToken token)
        {
            return (await ReadLineAsync(timeout, token).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }
    }
}