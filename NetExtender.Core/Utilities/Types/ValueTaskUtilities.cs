// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static class ValueTaskUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<T> ToValueTask<T>(this T value)
        {
            return ValueTask.FromResult(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask ToCanceledValueTask(this CancellationToken token)
        {
            return ValueTask.FromCanceled(token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<T> ToCanceledValueTask<T>(this CancellationToken token)
        {
            return ValueTask.FromCanceled<T>(token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask ToExceptionValueTask(this Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return ValueTask.FromException(exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<T> ToExceptionValueTask<T>(this Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return ValueTask.FromException<T>(exception);
        }

#if AWAIT_AS_IN_JAVASCRIPT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTaskAwaiter<T> GetAwaiter<T>(this T value)
        {
            return ValueTask.FromResult(value).GetAwaiter();
        }
#endif
    }
}