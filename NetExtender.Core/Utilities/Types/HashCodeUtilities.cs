// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Utilities.Types
{
    public static class HashCodeUtilities
    {
        public static Int32 Combine<T>(IEnumerable<T> source)
        {
            return Combine(source, null);
        }

        public static Int32 Combine<T>(IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            HashCode code = new HashCode();
            code.AddRange(source, comparer);

            return code.ToHashCode();
        }

        public static void AddRange<T>(this ref HashCode hash, IEnumerable<T> source)
        {
            AddRange(ref hash, source, null);
        }

        public static void AddRange<T>(this ref HashCode hash, IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source)
            {
                hash.Add(item, comparer);
            }
        }
    }
}