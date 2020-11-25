// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Combinatorics;

namespace NetExtender.Utils.Types
{
    public static class CollectionUtils
    {
        public static IList<IList<T>> GetCombinations<T>(this ICollection<T> collection, Int32 min = 1)
        {
            return GetCombinations(collection, min, collection.Count);
        }

        public static IList<IList<T>> GetCombinations<T>(this ICollection<T> collection, Int32 min, Int32 max)
        {
            if (min < 1 || min > collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(min));
            }

            if (max < 1 || max < min || max > collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(max));
            }

            IEnumerable<IList<T>> combo = new List<List<T>>();

            for (Int32 i = min; i <= max; i++)
            {
                combo = combo.Concat(new Combinations<T>(collection, i));
            }

            return combo.ToList();
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                collection.Add(item);
            }
        }
    }
}