// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Combinatoric;

namespace NetExtender.Utilities.Types
{
    public static class CollectionUtilities
    {
        public static T PopRandom<T>(this ICollection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return TryPopRandom(collection, out T? result) ? result : throw new NotSupportedException();
        }

        public static Boolean TryPopRandom<T>(this ICollection<T> collection, [MaybeNullWhen(false)] out T result)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.IsReadOnly || collection.Count <= 0)
            {
                result = default;
                return false;
            }

            try
            {
                result = collection.GetRandom();
                return collection.Remove(result);
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static IList<IList<T>> GetCombinations<T>(this ICollection<T> collection, Int32 min = 1)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return GetCombinations(collection, min, collection.Count);
        }

        public static IList<IList<T>> GetCombinations<T>(this ICollection<T> collection, Int32 min, Int32 max)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (min < 1 || min > collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, null);
            }

            if (max < 1 || max < min || max > collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(max), max, null);
            }

            IEnumerable<IList<T>> combo = new List<List<T>>();

            for (Int32 i = min; i <= max; i++)
            {
                combo = combo.Concat(new Combinations<T>(collection, i));
            }

            return combo.ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsReadOnly<T>(this ICollection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.IsReadOnly;
        }

        public static Boolean Contains<T>(this ICollection<T> collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Contains(item);
        }

        public static void Add<T>(this ICollection<T> collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.Add(item);
        }

        public static Boolean AddIf<T>(this ICollection<T> collection, T item, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (!condition)
            {
                return false;
            }

            collection.Add(item);
            return true;
        }

        public static Boolean AddIf<T>(this ICollection<T> collection, T item, Func<T, Boolean> predicate)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (!predicate(item))
            {
                return false;
            }

            collection.Add(item);
            return true;
        }

        public static Boolean AddIfNot<T>(this ICollection<T> collection, T item, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (condition)
            {
                return false;
            }

            collection.Add(item);
            return true;
        }

        public static Boolean AddIfNot<T>(this ICollection<T> collection, T item, Func<T, Boolean> predicate)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (predicate(item))
            {
                return false;
            }

            collection.Add(item);
            return true;
        }

        public static Boolean AddIfNotNull<T>(this ICollection<T> collection, T? item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (item is null)
            {
                return false;
            }

            collection.Add(item);
            return true;
        }

        public static Boolean AddIfUnique<T>(this ICollection<T> collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Contains(item))
            {
                return false;
            }

            collection.Add(item);
            return true;
        }

        public static Boolean Remove<T>(this ICollection<T> collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Remove(item);
        }

        public static Boolean RemoveIf<T>(this ICollection<T> collection, T item, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return condition && collection.Remove(item);
        }

        public static Boolean RemoveIf<T>(this ICollection<T> collection, T item, Func<T, Boolean> predicate)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) && collection.Remove(item);
        }

        public static Boolean RemoveIfNot<T>(this ICollection<T> collection, T item, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return !condition && collection.Remove(item);
        }

        public static Boolean RemoveIfNot<T>(this ICollection<T> collection, T item, Func<T, Boolean> predicate)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return !predicate(item) && collection.Remove(item);
        }

        public static void AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            AddRange(collection, (IEnumerable<T>) items);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
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

            foreach (T item in source)
            {
                collection.Add(item);
            }
        }

        public static void RemoveRange<T>(this ICollection<T> collection, params T[] items)
        {
            RemoveRange(collection, (IEnumerable<T>) items);
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> source)
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

            foreach (T item in source)
            {
                collection.Remove(item);
            }
        }

        public static Int32 RemoveAll<T>(this ICollection<T> collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.IsReadOnly)
            {
                throw new NotSupportedException();
            }

            Int32 result = 0;
            while (collection.Remove(item))
            {
                result++;
            }

            return result;
        }

        public static Int32 RemoveAll<T>(this ICollection<T> collection, params T[] source)
        {
            return RemoveAll(collection, (IEnumerable<T>) source);
        }

        public static Int32 RemoveAll<T>(this ICollection<T> collection, IEnumerable<T> source)
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

            Int32 count = 0;
            foreach (T item in source)
            {
                while (collection.Remove(item))
                {
                    count++;
                }
            }

            return count;
        }

        public static StringCollection ToStringCollection(this IEnumerable<String?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            StringCollection collection = new StringCollection();
            collection.AddRange(source.AsArray()!);

            return collection;
        }

        public static void CopyTo<T>(this IEnumerable<T> source, T[] array)
        {
            CopyTo(source, array, 0);
        }

        public static void CopyTo<T>(this IEnumerable<T> source, T[] array, Int32 index)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (source is ICollection<T> collection)
            {
                collection.CopyTo(array, index);
                return;
            }

            if (source is IReadOnlyCollection<T> count && count.Count + index > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(array), array.Length, null);
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            for (Int32 i = index; i < array.Length && enumerator.MoveNext(); i++)
            {
                array[i] = enumerator.Current;
            }
        }
    }
}