// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class CounterUtilities
    {
        private static class Equality<TCount> where TCount : unmanaged, IConvertible
        {
            private static readonly TCount One = MathUnsafe.Increment(default(TCount));
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Unique(TCount first)
            {
                return MathUnsafe.Equal(first, One);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean NotUnique(TCount first)
            {
                return MathUnsafe.Greater(first, One);
            }
        }
        
        public static IEnumerable<KeyValuePair<T, TCount>> Unique<T, TCount>(this IEnumerable<KeyValuePair<T, TCount>> counter) where TCount : unmanaged, IConvertible
        {
            if (counter is null)
            {
                throw new ArgumentNullException(nameof(counter));
            }
            
            return counter.Where(static pair => Equality<TCount>.Unique(pair.Value));
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> Unique<TKey, TValue, TCount>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<KeyValuePair<TKey, TValue>, TCount> selector) where TCount : unmanaged, IConvertible
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            return source.Where(pair => Equality<TCount>.Unique(selector(pair)));
        }
        
        public static IEnumerable<KeyValuePair<T, TCount>> NotUnique<T, TCount>(this IEnumerable<KeyValuePair<T, TCount>> counter) where TCount : unmanaged, IConvertible
        {
            if (counter is null)
            {
                throw new ArgumentNullException(nameof(counter));
            }
            
            return counter.Where(static pair => Equality<TCount>.NotUnique(pair.Value));
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> NotUnique<TKey, TValue, TCount>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<KeyValuePair<TKey, TValue>, TCount> selector) where TCount : unmanaged, IConvertible
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            return source.Where(pair => Equality<TCount>.NotUnique(selector(pair)));
        }
    }
}