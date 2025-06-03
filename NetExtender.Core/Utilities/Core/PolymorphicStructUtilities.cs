using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static unsafe class PolymorphicStructUtilities
    {
        private static ConcurrentDictionary<(Type Type, String Method), (Type Delegate, IntPtr Pointer)> Storage { get; } = new ConcurrentDictionary<(Type, String), (Type, IntPtr)>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get(Type type, String method, out delegate*<void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T>(Type type, String method, out delegate*<T, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2>(Type type, String method, out delegate*<T1, T2, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3>(Type type, String method, out delegate*<T1, T2, T3, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4>(Type type, String method, out delegate*<T1, T2, T3, T4, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5>(Type type, String method, out delegate*<T1, T2, T3, T4, T5, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6>(Type type, String method, out delegate*<T1, T2, T3, T4, T5, T6, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7>(Type type, String method, out delegate*<T1, T2, T3, T4, T5, T6, T7, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7, T8>(Type type, String method, out delegate*<T1, T2, T3, T4, T5, T6, T7, T8, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Type type, String method, out delegate*<T1, T2, T3, T4, T5, T6, T7, T8, T9, void> @delegate)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<TResult>(Type type, String method, out delegate*<TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T, TResult>(Type type, String method, out delegate*<T, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, TResult>(Type type, String method, out delegate*<T1, T2, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, TResult>(Type type, String method, out delegate*<T1, T2, T3, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, TResult>(Type type, String method, out delegate*<T1, T2, T3, T4, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, TResult>(Type type, String method, out delegate*<T1, T2, T3, T4, T5, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, TResult>(Type type, String method, out delegate*<T1, T2, T3, T4, T5, T6, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7, TResult>(Type type, String method, out delegate*<T1, T2, T3, T4, T5, T6, T7, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Type type, String method, out delegate*<T1, T2, T3, T4, T5, T6, T7, T8, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Type type, String method, out delegate*<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> @delegate)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get(Type type, String method, out delegate*<void*, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T>(Type type, String method, out delegate*<void*, T, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2>(Type type, String method, out delegate*<void*, T1, T2, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3>(Type type, String method, out delegate*<void*, T1, T2, T3, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, T5, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, T5, T6, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, T5, T6, T7, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7, T8>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, T5, T6, T7, T8, void> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, T5, T6, T7, T8, T9, void> @delegate)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<TResult>(Type type, String method, out delegate*<void*, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T, TResult>(Type type, String method, out delegate*<void*, T, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, TResult>(Type type, String method, out delegate*<void*, T1, T2, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, TResult>(Type type, String method, out delegate*<void*, T1, T2, T3, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, TResult>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, TResult>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, T5, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, TResult>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, T5, T6, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7, TResult>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, T5, T6, T7, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, T5, T6, T7, T8, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Type type, String method, out delegate*<void*, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> @delegate)
        {
            throw new NotImplementedException();
        }
    }
}