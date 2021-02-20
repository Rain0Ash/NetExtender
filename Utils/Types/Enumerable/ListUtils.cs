// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NetExtender.Utils.Numerics;

namespace NetExtender.Utils.Types
{
    public static class ListUtils
    {
        public static T GetRandom<T>([NotNull] this IList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Count <= 0 ? default : collection[RandomUtils.NextNonNegative(collection.Count - 1)];
        }

        public static void Insert<T>([NotNull] this IList<T> collection, Index index, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            collection.Insert(index.GetOffset(collection.Count), item);
        }
        
        public static void Swap<T>(this IList<T> source, Int32 first, Int32 second)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (first < 0 || first >= source.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(first));
            }
            
            if (second < 0 || second >= source.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(second));
            }

            (source[first], source[second]) = (source[second], source[first]);
        }
        
        public static Boolean TrySwap<T>(this IList<T> source, Int32 first, Int32 second)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (first < 0 || first >= source.Count)
            {
                return false;
            }
            
            if (second < 0 || second >= source.Count)
            {
                return false;
            }

            (source[first], source[second]) = (source[second], source[first]);
            return true;
        }
        
        public static void Swap([NotNull] IList source, Int32 first, Int32 second)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (first < 0 || first >= source.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(first));
            }
            
            if (second < 0 || second >= source.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(second));
            }

            (source[first], source[second]) = (source[second], source[first]);
        }
        
        public static Boolean TrySwap(IList source, Int32 first, Int32 second)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (first < 0 || first >= source.Count)
            {
                return false;
            }
            
            if (second < 0 || second >= source.Count)
            {
                return false;
            }

            (source[first], source[second]) = (source[second], source[first]);
            return true;
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
                
                switch (comparison)
                {
                    case 0:
                        return middle;
                    case < 0:
                        upper = middle - 1;
                        break;
                    default:
                        lower = middle + 1;
                        break;
                }
            }

            return ~lower;
        }

        public static Boolean IndexOf<T>([NotNull] this IList<T> source, T item, out Int32 index)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            index = source.IndexOf(item);
            return index >= 0;
        }
        
        public static void InsertRange<T>([NotNull] this IList<T> source, Int32 index, [NotNull] IEnumerable<T> items)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (T item in items)
            {
                source.Insert(index++, item);
            }
        }
    }
}