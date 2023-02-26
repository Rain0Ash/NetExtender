// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static class ValueTaskUtilities<T>
    {
        public static ValueTask<T?> Default
        {
            get
            {
                return ValueTask.FromResult(default(T));
            }
        }
    }
    
    public static class ValueTaskUtilities
    {
        public static ValueTask<Boolean> True
        {
            get
            {
                return ValueTask.FromResult(true);
            }
        }

        public static ValueTask<Boolean> False
        {
            get
            {
                return ValueTask.FromResult(false);
            }
        }

        public static ValueTask<Int32> Zero
        {
            get
            {
                return ValueTask.FromResult(0);
            }
        }

        public static ValueTask<Int32> One
        {
            get
            {
                return ValueTask.FromResult(1);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<T?> Default<T>()
        {
            return ValueTaskUtilities<T>.Default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<Boolean> ToValueTask(this Boolean value)
        {
            return value ? True : False;
        }
        
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