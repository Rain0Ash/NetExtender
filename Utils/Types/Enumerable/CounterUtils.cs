// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NetExtender.Types.Counters.Interfaces;

namespace NetExtender.Utils.Types
{
    public static class CounterUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, Int32>> Order<TKey>([NotNull] this ICounter<TKey> counter)
        {
            if (counter is null)
            {
                throw new ArgumentNullException(nameof(counter));
            }

            return counter.OrderByValues();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, Int32>> OrderDescending<TKey>([NotNull] this ICounter<TKey> counter)
        {
            if (counter is null)
            {
                throw new ArgumentNullException(nameof(counter));
            }

            return counter.OrderByValuesDescending();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, Int64>> Order<TKey>([NotNull] this ILongCounter<TKey> counter)
        {
            if (counter is null)
            {
                throw new ArgumentNullException(nameof(counter));
            }

            return counter.OrderByValues();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, Int64>> OrderDescending<TKey>([NotNull] this ILongCounter<TKey> counter)
        {
            if (counter is null)
            {
                throw new ArgumentNullException(nameof(counter));
            }

            return counter.OrderByValuesDescending();
        }
    }
}