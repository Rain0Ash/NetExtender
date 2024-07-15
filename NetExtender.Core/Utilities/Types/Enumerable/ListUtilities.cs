// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Types.Comparers;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Random.Interfaces;
using NetExtender.Types.Spans;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class ListUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this List<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            return CollectionsMarshal.AsSpan(collection);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> AsSpan<T>(this List<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            return CollectionsMarshal.AsSpan(collection);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new ReadOnlyCollection<T>(collection);
        }

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

        public static T GetRandomOrDefault<T>(this IList<T> collection, Func<T> alternate)
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
                throw new ArgumentOutOfRangeException(nameof(first), first, null);
            }

            if (second < 0 || second >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(second), second, null);
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
        
        public static void Replace<T>(this IList<T> collection, params T[] items)
        {
            Replace(collection, 0, items);
        }

        public static void Replace<T>(this IList<T> collection, IEnumerable<T> source)
        {
            Replace(collection, 0, source);
        }
        
        public static void Replace<T>(this IList<T> collection, Int32 start, params T[] items)
        {
            Replace(collection, start, (IEnumerable<T>) items);
        }
        
        public static void Replace<T>(this IList<T> collection, Int32 start, IEnumerable<T> source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
        
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
        
            if (collection.IsReadOnly)
            {
                throw new NotSupportedException();
            }
            
            if (start < 0 || start > collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, null);
            }
            
            using IEnumerator<T> enumerator = source.GetEnumerator();
            
            Int32 i = start;
            while (i < collection.Count && enumerator.MoveNext())
            {
                collection[i++] = enumerator.Current;
            }
            
            while (enumerator.MoveNext())
            {
                collection.Add(enumerator.Current);
                i++;
            }
        }
        
        public static void FullReplace<T>(this IList<T> collection, params T[] items)
        {
            FullReplace(collection, 0, items);
        }

        public static void FullReplace<T>(this IList<T> collection, IEnumerable<T> source)
        {
            FullReplace(collection, 0, source);
        }
        
        public static void FullReplace<T>(this IList<T> collection, Int32 start, params T[] items)
        {
            FullReplace(collection, start, (IEnumerable<T>) items);
        }
        
        public static void FullReplace<T>(this IList<T> collection, Int32 start, IEnumerable<T> source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
        
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
        
            if (collection.IsReadOnly)
            {
                throw new NotSupportedException();
            }
            
            if (start < 0 || start > collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, null);
            }
            
            using IEnumerator<T> enumerator = source.GetEnumerator();
            
            Int32 i = start;
            while (i < collection.Count && enumerator.MoveNext())
            {
                collection[i++] = enumerator.Current;
            }
            
            while (enumerator.MoveNext())
            {
                collection.Add(enumerator.Current);
                i++;
            }
            
            for (Int32 j = collection.Count - 1; j >= i; j--)
            {
                collection.RemoveAt(j);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this IList<T> collection)
        {
            Shuffle(collection, RandomUtilities.Generator);
        }

        public static void Shuffle<T>(this IList<T> collection, Random random)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < collection.Count; i++)
            {
                Int32 j = random.Next(i, collection.Count);
                (collection[i], collection[j]) = (collection[j], collection[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this IList<T> collection, IRandom random)
        {
            Shuffle<T, IRandom>(collection, random);
        }

        public static void Shuffle<T, TRandom>(this IList<T> collection, TRandom random) where TRandom : IRandom
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < collection.Count; i++)
            {
                Int32 j = random.Next(i, collection.Count);
                (collection[i], collection[j]) = (collection[j], collection[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Rotate<T>(this IList<T> collection)
        {
            Rotate(collection, 1);
        }

        public static void Rotate<T>(this IList<T> collection, Int32 offset)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count <= 1)
            {
                return;
            }

            offset %= collection.Count;
            switch (offset)
            {
                case 0:
                    return;
                case <= 0:
                {
                    offset = -offset;
                    for (Int32 i = 0; i < offset; i++)
                    {
                        T temp = collection[^1];
                        collection.RemoveAt(collection.Count - 1);
                        collection.Insert(0, temp);
                    }

                    break;
                }
                default:
                {
                    for (Int32 i = 0; i < offset; i++)
                    {
                        T temp = collection[0];
                        collection.RemoveAt(0);
                        collection.Add(temp);
                    }

                    break;
                }
            }
        }

        internal static Int32 BinarySearch<T>(IEnumerable<T> collection, T value)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection switch
            {
                IList<T> list => BinarySearch(list, value),
                IReadOnlyList<T> list => BinarySearch(list, value),
                _ => throw new TypeNotSupportedException(collection.GetType())
            };
        }

        internal static Int32 BinarySearch<T>(IEnumerable<T> collection, T value, IComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection switch
            {
                IList<T> list => BinarySearch(list, value, comparer),
                IReadOnlyList<T> list => BinarySearch(list, value, comparer),
                _ => throw new TypeNotSupportedException(collection.GetType())
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 BinarySearch<T>(IReadOnlyList<T> collection, T value)
        {
            return BinarySearch(collection, value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Int32 BinarySearch<T>(IReadOnlyList<T> collection, T value, IComparer<T>? comparer)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 BinarySearch<T>(IList<T> collection, T value)
        {
            return BinarySearch(collection, value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Int32 BinarySearch<T>(IList<T> collection, T value, IComparer<T>? comparer)
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

            Int32 Comparison(T? first, T? second)
            {
                return comparer.Compare(selector(first!), selector(second!));
            }

            collection.Sort(index, count, new ComparisonComparer<T>(Comparison));
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

            Int32 Comparison(T? first, T? second)
            {
                return comparison(selector(first!), selector(second!));
            }

            collection.Sort(index, count, new ComparisonComparer<T>(Comparison));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyPaginationObserver<T> ReadOnlyPaginationObserver<T>(this List<T> collection, Int32 size)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            return new ReadOnlyPaginationObserver<T>(collection.AsReadOnlySpan(), size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PaginationObserver<T> PaginationObserver<T>(this List<T> collection, Int32 size)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            return new PaginationObserver<T>(collection.AsSpan(), size);
        }
    }
}