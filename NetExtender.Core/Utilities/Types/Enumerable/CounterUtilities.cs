// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class CounterUtilities
    {
        private static class Equality<T> where T : IComparable<T>
        {
            private static readonly T One;

            static Equality()
            {
                try
                {
                    One = INetExtenderNumberConstantsBase<T>.One;
                }
                catch (Exception exception)
                {
                    T? @default = default;
                    if (@default is null)
                    {
                        throw;
                    }

                    try
                    {
                        One = INetExtenderIncrementOperators<T>.Increment(@default);
                    }
                    catch (Exception fallback)
                    {
                        throw new AggregateException(exception, fallback);
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Unique(T value)
            {
                return EqualityComparer<T>.Default.Equals(value, One);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean NotUnique(T value)
            {
                return Comparer<T>.Default.Compare(value, One) > 0;
            }
        }

        public static IEnumerable<KeyValuePair<T, TCount>> Unique<T, TCount>(this IEnumerable<KeyValuePair<T, TCount>> counter) where TCount : IComparable<TCount>
        {
            if (counter is null)
            {
                throw new ArgumentNullException(nameof(counter));
            }

            return counter.Where(static pair => Equality<TCount>.Unique(pair.Value));
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> Unique<TKey, TValue, TCount>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<KeyValuePair<TKey, TValue>, TCount> selector) where TCount : IComparable<TCount>
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

        public static IEnumerable<KeyValuePair<T, TCount>> NotUnique<T, TCount>(this IEnumerable<KeyValuePair<T, TCount>> counter) where TCount : IComparable<TCount>
        {
            if (counter is null)
            {
                throw new ArgumentNullException(nameof(counter));
            }

            return counter.Where(static pair => Equality<TCount>.NotUnique(pair.Value));
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> NotUnique<TKey, TValue, TCount>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<KeyValuePair<TKey, TValue>, TCount> selector) where TCount : IComparable<TCount>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(selector, static (selector, pair) => Equality<TCount>.NotUnique(selector(pair)));
        }
    }
}