// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Types.Comparers.Common;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class ListUtilities
    {
        public static T GetRandom<T>(this IList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Count > 0 ? collection[RandomUtilities.NextNonNegative(collection.Count - 1)] : throw new InvalidOperationException();
        }
        
        public static T GetRandomOrDefault<T>(this IList<T> collection, T alternate)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Count > 0 ? collection[RandomUtilities.NextNonNegative(collection.Count - 1)] : alternate;
        }
        
        public static T GetRandom<T>(this IList<T> collection, Func<T> alternate)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return collection.Count > 0 ? collection[RandomUtilities.NextNonNegative(collection.Count - 1)] : alternate();
        }
        
        public static T? GetRandomOrDefault<T>(this IList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Count > 0 ? collection[RandomUtilities.NextNonNegative(collection.Count - 1)] : default;
        }
        
        public static Boolean IndexOf<T>(this IList<T> collection, T item, out Int32 index)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            index = collection.IndexOf(item);
            return index >= 0;
        }

        public static void Insert<T>(this IList<T> collection, Index index, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            collection.Insert(index.GetOffset(collection.Count), item);
        }
        
        public static void InsertRange<T>(this IList<T> collection, Int32 index, IEnumerable<T> source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source)
            {
                collection.Insert(index++, item);
            }
        }

        public static void Swap<T>(this IList<T> collection, Int32 first, Int32 second)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (first < 0 || first >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(first));
            }
            
            if (second < 0 || second >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(second));
            }

            (collection[first], collection[second]) = (collection[second], collection[first]);
        }
        
        public static Boolean TrySwap<T>(this IList<T> collection, Int32 first, Int32 second)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (first < 0 || first >= collection.Count)
            {
                return false;
            }
            
            if (second < 0 || second >= collection.Count)
            {
                return false;
            }

            (collection[first], collection[second]) = (collection[second], collection[first]);
            return true;
        }
        
        public static void Swap(IList collection, Int32 first, Int32 second)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (first < 0 || first >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(first));
            }
            
            if (second < 0 || second >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(second));
            }

            (collection[first], collection[second]) = (collection[second], collection[first]);
        }
        
        public static Boolean TrySwap(IList collection, Int32 first, Int32 second)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (first < 0 || first >= collection.Count)
            {
                return false;
            }
            
            if (second < 0 || second >= collection.Count)
            {
                return false;
            }

            (collection[first], collection[second]) = (collection[second], collection[first]);
            return true;
        }

        public static Int32 BinarySearch<T>(this IList<T> collection, T value)
        {
            return BinarySearch(collection, value, null);
        }

        public static Int32 BinarySearch<T>(this IList<T> collection, T value, IComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            comparer ??= Comparer<T>.Default;

            Int32 lower = 0;
            Int32 upper = collection.Count - 1;

            while (lower <= upper)
            {
                Int32 middle = lower + (upper - lower) / 2;
                Int32 comparison = comparer.Compare(value, collection[middle]);
                
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
        
        public static void Sort<T, TKey>(this List<T> collection, Func<T, TKey> selector)
        {
            Sort(collection, selector, (IComparer<TKey>?) null);
        }
        
        public static void Sort<T, TKey>(this List<T> collection, Func<T, TKey> selector, Comparison<TKey> comparison)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            collection.Sort((first, second) => comparison(selector(first), selector(second)));
        }
        
        public static void Sort<T, TKey>(this List<T> collection, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            comparer ??= Comparer<TKey>.Default;
            collection.Sort((first, second) => comparer.Compare(selector(first), selector(second)));
        }
        
        public static void Sort<T, TKey>(this List<T> collection, Func<T, TKey> selector, Int32 index, Int32 count, IComparer<TKey>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            comparer ??= Comparer<TKey>.Default;
            collection.Sort(index, count, new CustomComparer<T>((first, second) => comparer.Compare(selector(first!), selector(second!))));
        }
        
        public static void Sort<T, TKey>(this List<T> collection, Func<T, TKey> selector, Int32 index, Int32 count, Comparison<TKey> comparison)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }
            
            collection.Sort(index, count, new CustomComparer<T>((first, second) => comparison(selector(first!), selector(second!))));
        }
    }
}