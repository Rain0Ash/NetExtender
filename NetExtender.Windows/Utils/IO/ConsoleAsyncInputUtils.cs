// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Events;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.IO
{
    public static partial class ConsoleAsyncInputUtils
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Boolean CancelIoEx(IntPtr handle, IntPtr lpOverlapped);

        public static event TypeHandler<TypeHandledEventArgs<String?>> ConsoleLineInput = null!;
        public static event TypeHandler<TypeHandledEventArgs<ConsoleKeyInfo>> ConsoleKeyInfoInput = null!;
        public static event TypeHandler<TypeHandledEventArgs<Int32>> ConsoleKeyCodeInput = null!;

        private static void OnConsoleLineInput(String? line)
        {
            ConsoleLineInput?.Invoke(new TypeHandledEventArgs<String?>(line));
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
            CancelIoEx(WindowsConsoleUtils.ConsoleInputHandle, IntPtr.Zero);
        }

        private static async Task<T?> InputAsync<T>(Func<T> func, CancellationToken token)
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

        private static async Task<T?> InputAsync<T>(Func<T> func, Int32 milli, CancellationToken token)
        {
            using CancellationTokenSource source = new CancellationTokenSource();

            try
            {
                Task<T?> read = InputAsync(func, source.Token);
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
        public static Task<ConsoleKeyInfo> ReadKeyAsync(CancellationToken token)
        {
            return InputAsync(Console.ReadKey, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ConsoleKeyInfo> ReadKeyInterceptAsync(CancellationToken token)
        {
            return InputAsync(ConsoleUtils.ReadKeyIntercept, token);
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
            return InputAsync(ConsoleUtils.ReadKeyIntercept, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> ReadLineAsync(CancellationToken token)
        {
            return InputAsync(Console.ReadLine, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> ReadLineAsync(Int32 milli)
        {
            return ReadLineAsync(milli, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> ReadLineAsync(Int32 milli, CancellationToken token)
        {
            return InputAsync(Console.ReadLine, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? CastAs<T>()
        {
            return CastAs<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? CastAs<T>(CultureInfo info)
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
        public static async Task<T?> CastReadAsAsync<T>(CultureInfo info, CancellationToken token)
        {
            String? read = await ReadLineAsync(token).ConfigureAwait(false);
            return read is not null ? read.CastConvert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> CastReadAsAsync<T>(Int32 milli)
        {
            return CastReadAsAsync<T>(CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T?> CastReadAsAsync<T>(CultureInfo info, Int32 milli)
        {
            String? read = await ReadLineAsync(milli).ConfigureAwait(false);
            return read is not null ? read.CastConvert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> CastReadAsAsync<T>(Int32 milli, CancellationToken token)
        {
            return CastReadAsAsync<T>(CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T?> CastReadAsAsync<T>(CultureInfo info, Int32 milli, CancellationToken token)
        {
            String? read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read is not null ? read.CastConvert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? ReadAs<T>()
        {
            return ReadAs<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? ReadAs<T>(CultureInfo info)
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
        public static T ReadAs<T>(TryParseHandler<String?, T> converter, T @default)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return Console.ReadLine().Convert(converter, @default);
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
        public static async Task<T?> ReadAsAsync<T>(CultureInfo info, CancellationToken token)
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
        public static async Task<T> ReadAsAsync<T>(TryParseHandler<String?, T> converter, T @default, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String? read = await ReadLineAsync(token).ConfigureAwait(false);
            return read.Convert(converter, @default);
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
        public static Task<T?> ReadAsAsync<T>(Int32 milli)
        {
            return ReadAsAsync<T>(CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(CultureInfo info, Int32 milli)
        {
            return ReadAsAsync<T>(info, milli, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milli, ParseHandler<String?, T> converter)
        {
            return ReadAsAsync(milli, converter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String?, T> converter, T @default)
        {
            return ReadAsAsync(milli, converter, @default, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String?, T> converter, Func<T> generator)
        {
            return ReadAsAsync(milli, converter, generator, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String?, T> converter, Func<String?, T> generator)
        {
            return ReadAsAsync(milli, converter, generator, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(Int32 milli, CancellationToken token)
        {
            return ReadAsAsync<T>(CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T?> ReadAsAsync<T>(CultureInfo info, Int32 milli, CancellationToken token)
        {
            String? read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read is not null ? read.Convert<T>(info) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milli, ParseHandler<String?, T> converter, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String? read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read.Convert(converter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String?, T> converter, T @default, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            String? read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read.Convert(converter, @default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String?, T> converter, Func<T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String? read = await ReadLineAsync(milli, token).ConfigureAwait(false);
            return read.Convert(converter, generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<T> ReadAsAsync<T>(Int32 milli, TryParseHandler<String?, T> converter, Func<String?, T> generator, CancellationToken token)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            String? read = await ReadLineAsync(milli, token).ConfigureAwait(false);
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
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(CultureInfo info, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(Int32 milli)
        {
            return CastAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, Int32 milli)
        {
            return CastAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, Int32 milli)
        {
            return CastAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(CultureInfo info, Int32 milli)
        {
            return CastAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(Int32 milli, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, Int32 milli, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, Int32 milli, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(CultureInfo info, Int32 milli, CancellationToken token)
        {
            return CastAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> CastAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String separator, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String[] separators, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(CultureInfo info, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String separator, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(Int32 milli)
        {
            return ReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String separator, Int32 milli)
        {
            return ReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String[] separators, Int32 milli)
        {
            return ReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(CultureInfo info, Int32 milli)
        {
            return ReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(Int32 milli, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String separator, Int32 milli, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String[] separators, Int32 milli, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(CultureInfo info, Int32 milli, CancellationToken token)
        {
            return ReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> ReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.Convert<T>(separators, info);
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
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(CultureInfo info, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, CancellationToken token)
        {
            return (await ReadLineAsync(token).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(Int32 milli)
        {
            return TryReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, Int32 milli)
        {
            return TryReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, Int32 milli)
        {
            return TryReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(CultureInfo info, Int32 milli)
        {
            return TryReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli)
        {
            return (await ReadLineAsync(milli).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(Int32 milli, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, Int32 milli, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separator, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, Int32 milli, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(separators, CultureInfo.InvariantCulture, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(CultureInfo info, Int32 milli, CancellationToken token)
        {
            return TryReadAsEnumerableAsync<T>(StringUtils.DefaultSeparator, info, milli, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String separator, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<T>?> TryReadAsEnumerableAsync<T>(String[] separators, CultureInfo info, Int32 milli, CancellationToken token)
        {
            return (await ReadLineAsync(milli, token).ConfigureAwait(false))?.TryConvert<T>(separators, info);
        }
    }
}