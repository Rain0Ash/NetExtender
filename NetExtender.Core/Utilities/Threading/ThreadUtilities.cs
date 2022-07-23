// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

// ReSharper disable HeapView.CanAvoidClosure

namespace NetExtender.Utilities.Threading
{
    public static class ThreadUtilities
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
        
        public static Thread SetName(this Thread thread, String? name)
        {
            if (thread is null)
            {
                throw new ArgumentNullException(nameof(thread));
            }

            thread.Name = name;
            return thread;
        }

        public static Thread SetBackground(this Thread thread)
        {
            return SetBackground(thread, true);
        }

        public static Thread SetBackground(this Thread thread, Boolean background)
        {
            if (thread is null)
            {
                throw new ArgumentNullException(nameof(thread));
            }

            thread.IsBackground = background;
            return thread;
        }

        public static Thread SetPriority(this Thread thread, ThreadPriority priority)
        {
            if (thread is null)
            {
                throw new ArgumentNullException(nameof(thread));
            }

            thread.Priority = priority;
            return thread;
        }

        public static Thread SetCurrentCulture(this Thread thread, CultureInfo? info)
        {
            if (thread is null)
            {
                throw new ArgumentNullException(nameof(thread));
            }

            if (info is null)
            {
                return thread;
            }

            thread.CurrentCulture = info;
            return thread;
        }

        public static Thread SetCurrentUICulture(this Thread thread, CultureInfo? info)
        {
            if (thread is null)
            {
                throw new ArgumentNullException(nameof(thread));
            }

            if (info is null)
            {
                return thread;
            }

            thread.CurrentUICulture = info;
            return thread;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Exception? SafeInvoke(Action action)
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

        private static void Create(Action action, ApartmentState state)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Exception? exception = null;
            Thread thread = new Thread(() => exception = SafeInvoke(action));
            thread.TrySetApartmentState(state);
            thread.Start();
            thread.Join();

            if (exception is not null)
            {
                throw exception;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void STA(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Create(action, ApartmentState.STA);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void STA<T>(Action<T> action, T arg)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void STA<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void STA<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void STA<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void STA<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void STA<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void STA<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void STA<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void STA<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            STA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void MTA(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Create(action.Invoke, ApartmentState.MTA);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void MTA<T>(Action<T> action, T arg)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void MTA<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void MTA<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void MTA<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void MTA<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void MTA<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void MTA<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void MTA<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void MTA<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MTA(() => action.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }
        
        private static T Create<T>(Func<T> function, ApartmentState state)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            T result = default!;
            Exception? exception = null;
            
            Thread thread = new Thread(() => exception = SafeInvoke(() => result = function.Invoke()));
            thread.TrySetApartmentState(state);
            thread.Start();
            thread.Join();

            if (exception is not null)
            {
                throw exception;
            }

            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult STA<TResult>(Func<TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Create(function, ApartmentState.STA);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult STA<T, TResult>(Func<T, TResult> function, T arg)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult STA<T1, T2, TResult>(Func<T1, T2, TResult> function, T1 arg1, T2 arg2)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult STA<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function, T1 arg1, T2 arg2, T3 arg3)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult STA<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3, arg4));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult STA<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult STA<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult STA<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult STA<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return STA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult STA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return STA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult MTA<TResult>(Func<TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Create(function.Invoke, ApartmentState.MTA);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult MTA<T, TResult>(Func<T, TResult> function, T arg)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult MTA<T1, T2, TResult>(Func<T1, T2, TResult> function, T1 arg1, T2 arg2)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult MTA<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function, T1 arg1, T2 arg2, T3 arg3)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult MTA<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult MTA<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult MTA<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult MTA<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult MTA<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult MTA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return MTA(() => function.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }
    }
}