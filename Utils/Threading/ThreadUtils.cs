// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;

namespace NetExtender.Utils.Threading
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "HeapView.CanAvoidClosure")]
    public static class ThreadUtils
    {
        public static ApartmentState State
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Thread.CurrentThread.GetApartmentState();
            }
        }
        
        public static Boolean IsSTA
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return State == ApartmentState.STA;
            }
        }

        public static Boolean IsMTA
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return State == ApartmentState.MTA;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Exception? SafeInvoke([NotNull] Action action)
        {
            try
            {
                action.Invoke();
                return null;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        private static void Create([NotNull] Action action, ApartmentState state)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Exception? exception = null;
            Thread thread = new Thread(() => exception = SafeInvoke(action));
            thread.SetApartmentState(state);
            thread.Start();
            thread.Join();

            if (exception is not null)
            {
                throw exception;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void STA([NotNull] Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Create(action.Invoke, ApartmentState.STA);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void STA<T>([NotNull] Action<T> action, T arg)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void STA<T1, T2>([NotNull] Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void STA<T1, T2, T3>([NotNull] Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void STA<T1, T2, T3, T4>([NotNull] Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void STA<T1, T2, T3, T4, T5>([NotNull] Action<T1, T2, T3, T4, T5> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void STA<T1, T2, T3, T4, T5, T6>([NotNull] Action<T1, T2, T3, T4, T5, T6> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void STA<T1, T2, T3, T4, T5, T6, T7>([NotNull] Action<T1, T2, T3, T4, T5, T6, T7> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void STA<T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void STA<T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MTA([NotNull] Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Create(action.Invoke, ApartmentState.MTA);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MTA<T>([NotNull] Action<T> action, T arg)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MTA<T1, T2>([NotNull] Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MTA<T1, T2, T3>([NotNull] Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MTA<T1, T2, T3, T4>([NotNull] Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MTA<T1, T2, T3, T4, T5>([NotNull] Action<T1, T2, T3, T4, T5> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MTA<T1, T2, T3, T4, T5, T6>([NotNull] Action<T1, T2, T3, T4, T5, T6> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MTA<T1, T2, T3, T4, T5, T6, T7>([NotNull] Action<T1, T2, T3, T4, T5, T6, T7> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MTA<T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MTA<T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }
        
        private static TResult? Create<TResult>([NotNull] Func<TResult> function, ApartmentState state)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            Exception? exception = null;
            TResult? result = default;
            Thread thread = new Thread(() => SafeInvoke(() => result = function.Invoke()));
            thread.SetApartmentState(state);
            thread.Start();
            thread.Join();

            if (exception is not null)
            {
                throw exception;
            }

            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? STA<TResult>([NotNull] Func<TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Create(function.Invoke, ApartmentState.STA);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? STA<T, TResult>([NotNull] Func<T, TResult> function, T arg)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? STA<T1, T2, TResult>([NotNull] Func<T1, T2, TResult> function, T1 arg1, T2 arg2)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? STA<T1, T2, T3, TResult>([NotNull] Func<T1, T2, T3, TResult> function, T1 arg1, T2 arg2, T3 arg3)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? STA<T1, T2, T3, T4, TResult>([NotNull] Func<T1, T2, T3, T4, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3, arg4));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? STA<T1, T2, T3, T4, T5, TResult>([NotNull] Func<T1, T2, T3, T4, T5, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? STA<T1, T2, T3, T4, T5, T6, TResult>([NotNull] Func<T1, T2, T3, T4, T5, T6, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? STA<T1, T2, T3, T4, T5, T6, T7, TResult>([NotNull] Func<T1, T2, T3, T4, T5, T6, T7, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? STA<T1, T2, T3, T4, T5, T6, T7, T8, TResult>([NotNull] Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return STA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? STA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>([NotNull] Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? MTA<TResult>([NotNull] Func<TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Create(function.Invoke, ApartmentState.MTA);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? MTA<T, TResult>([NotNull] Func<T, TResult> function, T arg)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? MTA<T1, T2, TResult>([NotNull] Func<T1, T2, TResult> function, T1 arg1, T2 arg2)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? MTA<T1, T2, T3, TResult>([NotNull] Func<T1, T2, T3, TResult> function, T1 arg1, T2 arg2, T3 arg3)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? MTA<T1, T2, T3, T4, TResult>([NotNull] Func<T1, T2, T3, T4, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? MTA<T1, T2, T3, T4, T5, TResult>([NotNull] Func<T1, T2, T3, T4, T5, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? MTA<T1, T2, T3, T4, T5, T6, TResult>([NotNull] Func<T1, T2, T3, T4, T5, T6, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? MTA<T1, T2, T3, T4, T5, T6, T7, TResult>([NotNull] Func<T1, T2, T3, T4, T5, T6, T7, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? MTA<T1, T2, T3, T4, T5, T6, T7, T8, TResult>([NotNull] Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? MTA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>([NotNull] Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }
    }
}