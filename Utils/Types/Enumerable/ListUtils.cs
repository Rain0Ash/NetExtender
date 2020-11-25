// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Utils.Types
{
    public static class ListUtils
    {
        public static T TryGetValue<T>(this IReadOnlyList<T> collection, Int32 index, T defaultValue = default)
        {
            return TryGetValue(collection, index, out T value) ? value : defaultValue;
        }

        public static Boolean TryGetValue<T>(this IReadOnlyList<T> collection, Int32 index, out T value, T defaultValue = default)
        {
            if (collection.InBounds(index))
            {
                value = collection[index];
                return true;
            }

            value = defaultValue;
            return false;
        }

        public static void Swap(IList source, Int32 index1, Int32 index2)
        {
            Object temp = source[index1];
            source[index1] = source[index2];
            source[index2] = temp;
        }
        
        public static Boolean TrySwap(IList source, Int32 index1, Int32 index2)
        {
            try
            {
                Swap(source, index1, index2);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Swap<T>(this IList<T> source, Int32 index1, Int32 index2)
        {
            T temp = source[index1];
            source[index1] = source[index2];
            source[index2] = temp;
        }
        
        public static Boolean TrySwap<T>(this IList<T> source, Int32 index1, Int32 index2)
        {
            try
            {
                Swap(source, index1, index2);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public static Int32 BinarySearch<T>(this IList<T> source, T value, IComparer<T> comparer = null)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= Comparer<T>.Default;

            Int32 lower = 0;
            Int32 upper = source.Count - 1;

            while (lower <= upper)
            {
                Int32 middle = lower + (upper - lower) / 2;
                Int32 comparison = comparer.Compare(value, source[middle]);
                
                if (comparison == 0)
                {
                    return middle;
                }

                if (comparison < 0)
                {
                    upper = middle - 1;
                }
                else
                {
                    lower = middle + 1;
                }
            }

            return ~lower;
        }
    }
}