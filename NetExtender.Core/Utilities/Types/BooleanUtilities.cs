// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Numerics;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static class BooleanUtilities
    {
        private static class Storage<T> where T : struct, IEquatable<T>
        {
            private static T? one;
            public static T One
            {
                get
                {
                    return one ??= ReflectionOperator.Get<T>(UnaryOperator.Increment) is { } @operator ? @operator.Invoke(default) : throw new NotInitializedException($"Use '{nameof(BooleanUtilities)}.{nameof(SetOne)}' method to set value that equivalent to 'one'.");
                }
                set
                {
                    one = value;
                }
            }
        }

        static BooleanUtilities()
        {
            SetOne(IntPtr.Add(IntPtr.Zero, 1));
            SetOne(UIntPtr.Add(UIntPtr.Zero, 1));
            SetOne(BigInteger.One);
            SetOne(Complex.One);
            SetOne(Time.Millisecond.One);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetOne<T>(T one) where T : struct, IEquatable<T>
        {
            Storage<T>.One = one;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T To<T>(this Boolean value) where T : struct, IEquatable<T>
        {
            return value ? Storage<T>.One : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? To<T>(this Boolean? value) where T : struct, IEquatable<T>
        {
            return value switch
            {
                null => null,
                true => Storage<T>.One,
                false => default(T)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? To<T>(this Trilean value) where T : struct, IEquatable<T>
        {
            return To<T>((Boolean?) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Then(this Boolean value, Action handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (value)
            {
                handler();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Then<T>(this Boolean value, Action<T> handler, T arg)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (value)
            {
                handler(arg);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Then<T1, T2>(this Boolean value, Action<T1, T2> handler, T1 arg1, T2 arg2)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (value)
            {
                handler(arg1, arg2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Then<T1, T2, T3>(this Boolean value, Action<T1, T2, T3> handler, T1 arg1, T2 arg2, T3 arg3)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (value)
            {
                handler(arg1, arg2, arg3);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Then<T1, T2, T3, T4>(this Boolean value, Action<T1, T2, T3, T4> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (value)
            {
                handler(arg1, arg2, arg3, arg4);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Then<T1, T2, T3, T4, T5>(this Boolean value, Action<T1, T2, T3, T4, T5> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (value)
            {
                handler(arg1, arg2, arg3, arg4, arg5);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Then<T1, T2, T3, T4, T5, T6>(this Boolean value, Action<T1, T2, T3, T4, T5, T6> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (value)
            {
                handler(arg1, arg2, arg3, arg4, arg5, arg6);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult? Then<TResult>(this Boolean value, Func<TResult> handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return value ? handler() : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult? Then<T, TResult>(this Boolean value, Func<T, TResult?> handler, T arg)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return value ? handler(arg) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult? Then<T1, T2, TResult>(this Boolean value, Func<T1, T2, TResult?> handler, T1 arg1, T2 arg2)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return value ? handler(arg1, arg2) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult? Then<T1, T2, T3, TResult>(this Boolean value, Func<T1, T2, T3, TResult?> handler, T1 arg1, T2 arg2, T3 arg3)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return value ? handler(arg1, arg2, arg3) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult? Then<T1, T2, T3, T4, TResult>(this Boolean value, Func<T1, T2, T3, T4, TResult?> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return value ? handler(arg1, arg2, arg3, arg4) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult? Then<T1, T2, T3, T4, T5, TResult>(this Boolean value, Func<T1, T2, T3, T4, T5, TResult?> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return value ? handler(arg1, arg2, arg3, arg4, arg5) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult? Then<T1, T2, T3, T4, T5, T6, TResult>(this Boolean value, Func<T1, T2, T3, T4, T5, T6, TResult?> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return value ? handler(arg1, arg2, arg3, arg4, arg5, arg6) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult Then<TResult>(this Boolean value, Func<Boolean, TResult> handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return handler(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult Then<T, TResult>(this Boolean value, Func<Boolean, T, TResult> handler, T arg)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return handler(value, arg);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult Then<T1, T2, TResult>(this Boolean value, Func<Boolean, T1, T2, TResult> handler, T1 arg1, T2 arg2)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return handler(value, arg1, arg2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult Then<T1, T2, T3, TResult>(this Boolean value, Func<Boolean, T1, T2, T3, TResult> handler, T1 arg1, T2 arg2, T3 arg3)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return handler(value, arg1, arg2, arg3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult Then<T1, T2, T3, T4, TResult>(this Boolean value, Func<Boolean, T1, T2, T3, T4, TResult> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return handler(value, arg1, arg2, arg3, arg4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult Then<T1, T2, T3, T4, T5, TResult>(this Boolean value, Func<Boolean, T1, T2, T3, T4, T5, TResult> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return handler(value, arg1, arg2, arg3, arg4, arg5);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TResult Then<T1, T2, T3, T4, T5, T6, TResult>(this Boolean value, Func<Boolean, T1, T2, T3, T4, T5, T6, TResult> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return handler(value, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Else(this Boolean value, Action handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (!value)
            {
                handler();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Else<T>(this Boolean value, Action<T> handler, T arg)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (!value)
            {
                handler(arg);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Else<T1, T2>(this Boolean value, Action<T1, T2> handler, T1 arg1, T2 arg2)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (!value)
            {
                handler(arg1, arg2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Else<T1, T2, T3>(this Boolean value, Action<T1, T2, T3> handler, T1 arg1, T2 arg2, T3 arg3)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (!value)
            {
                handler(arg1, arg2, arg3);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Else<T1, T2, T3, T4>(this Boolean value, Action<T1, T2, T3, T4> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (!value)
            {
                handler(arg1, arg2, arg3, arg4);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Else<T1, T2, T3, T4, T5>(this Boolean value, Action<T1, T2, T3, T4, T5> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (!value)
            {
                handler(arg1, arg2, arg3, arg4, arg5);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Else<T1, T2, T3, T4, T5, T6>(this Boolean value, Action<T1, T2, T3, T4, T5, T6> handler, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (!value)
            {
                handler(arg1, arg2, arg3, arg4, arg5, arg6);
            }
        }
    }
}