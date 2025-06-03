// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Combinatoric;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static class CollectionUtilities
    {
        // ReSharper disable once ClassNeverInstantiated.Local
        private sealed class ArrayAccessor<T> : Collection<T>
        {
            public static Func<Collection<T>, IList<T>> Getter { get; }

            static ArrayAccessor()
            {
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.NonPublic;
                Getter = typeof(Collection<T>).GetProperty(nameof(Items), binding)?.CreateGetExpression<Collection<T>, IList<T>>().Compile() ?? throw new NeverOperationException();
            }

            private ArrayAccessor()
            {
            }

            public static T[]? InternalArray(Collection<T> collection)
            {
                if (collection is null)
                {
                    throw new ArgumentNullException(nameof(collection));
                }

                return Getter(collection) switch
                {
                    null => null,
                    T[] array => array,
                    List<T> list => list.Internal(),
                    _ => null
                };
            }

            public static List<T>? InternalList(Collection<T> collection)
            {
                if (collection is null)
                {
                    throw new ArgumentNullException(nameof(collection));
                }

                return Getter(collection) switch
                {
                    null => null,
                    List<T> list => list,
                    _ => null
                };
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[]? InternalArray<T>(this Collection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return ArrayAccessor<T>.InternalArray(collection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T>? InternalList<T>(this Collection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return ArrayAccessor<T>.InternalList(collection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> Internal<T>(this Collection<T> collection)
        {
            return InternalIList(collection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> InternalIList<T>(this Collection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return ArrayAccessor<T>.Getter(collection);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this Collection<T> collection)
        {
            return AsSpan(collection);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> AsSpan<T>(this Collection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.InternalArray();
        }

        public static void Rewrite<T>(this Collection<T> collection, IEnumerable<T>? source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            IList<T> @internal = collection.Internal();
            @internal.Clear();

            if (source is not null)
            {
                @internal.AddRange(source);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TCollection Rewrite<T, TCollection>(this TCollection collection, IEnumerable<T>? source) where TCollection : Collection<T>
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Rewrite<T>(collection, source);
            return collection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean EnsureCapacity<T>(this Collection<T> collection, ref Int32 capacity)
        {
            return EnsureCapacity(collection, capacity, out capacity);
        }

        public static Boolean EnsureCapacity<T>(this Collection<T> collection, Int32 capacity, out Int32 result)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (capacity < 0 || collection.Internal() is not List<T> list)
            {
                result = collection.Count;
                return false;
            }
            
            result = list.EnsureCapacity(capacity);
            return result > capacity;
        }

        public static Boolean TrimExcess<T>(this Collection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Internal() is not List<T> list)
            {
                return false;
            }
            
            list.TrimExcess();
            return true;
        }
        
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

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 BinarySearch<T>(this Collection<T> collection, T item, IComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Internal() switch
            {
                T[] array => Array.BinarySearch(array, item, comparer),
                List<T> list => list.BinarySearch(item, comparer),
                { } list => ListUtilities.BinarySearch(list, item, comparer)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32? BinarySearch<T>(this Collection<T> collection, Int32 index, T item, IComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (index < 0 || index >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return collection.Internal() switch
            {
                T[] array => Array.BinarySearch(array, index, array.Length - index, item, comparer),
                List<T> list => list.BinarySearch(index, list.Count - index, item, comparer),
                _ when comparer is not null => collection.Skip(index).FindIndex(value => comparer.Compare(item, value) == 0, out Int32 result) ? result + index : null,
                _ => collection.Skip(index).IndexOf(item, out Int32 result) ? result + index : null
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32? BinarySearch<T>(this Collection<T> collection, Int32 index, Int32 count, T item, IComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (index < 0 || index >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (count < 0 || index > collection.Count - count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            return collection.Internal() switch
            {
                T[] array => Array.BinarySearch(array, index, count, item, comparer),
                List<T> list => list.BinarySearch(index, count, item, comparer),
                _ when comparer is not null => collection.Skip(index).Take(count).FindIndex(value => comparer.Compare(item, value) == 0, out Int32 result) ? result + index : null,
                _ => collection.Skip(index).Take(count).IndexOf(item, out Int32 result) ? result + index : null
            };
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

        public static void AddRange<T>(this ICollection<T> collection, params T[]? items)
        {
            AddRange(collection, (IEnumerable<T>?) items);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T>? source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (collection.IsReadOnly)
            {
                throw new NotSupportedException();
            }
            
            if (source is null)
            {
                return;
            }
            
            if (collection is List<T> list)
            {
                list.AddRange(source);
                return;
            }

            foreach (T item in source)
            {
                collection.Add(item);
            }
        }

        public static void Reload<T>(this ICollection<T> collection, params T[]? items)
        {
            Reload(collection, (IEnumerable<T>?) items);
        }

        public static void Reload<T>(this ICollection<T> collection, IEnumerable<T>? source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (collection.IsReadOnly)
            {
                throw new NotSupportedException();
            }
            
            collection.Clear();
            
            if (source is not null)
            {
                collection.AddRange(source);
            }
        }

        public static void RemoveRange<T>(this ICollection<T> collection, params T[]? items)
        {
            RemoveRange(collection, (IEnumerable<T>?) items);
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T>? source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (collection.IsReadOnly)
            {
                throw new NotSupportedException();
            }
            
            if (source is null)
            {
                return;
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

        public static Int32 RemoveAll<T>(this ICollection<T> collection, params T[]? source)
        {
            return RemoveAll(collection, (IEnumerable<T>?) source);
        }

        public static Int32 RemoveAll<T>(this ICollection<T> collection, IEnumerable<T>? source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (collection.IsReadOnly)
            {
                throw new NotSupportedException();
            }
            
            if (source is null)
            {
                return 0;
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

        public static Int32 RemoveAll<T>(this ICollection<T> collection, Predicate<T> match)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            if (collection.IsReadOnly)
            {
                throw new NotSupportedException();
            }

            if (collection is List<T> list)
            {
                return list.RemoveAll(match);
            }
            
            List<T> remove = new List<T>();

            foreach (T item in collection)
            {
                if (match(item))
                {
                    remove.Add(item);
                }
            }

            foreach (T item in remove)
            {
                collection.Remove(item);
            }

            return remove.Count;
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

        public static void CopyTo<TSource, TResult>(this IEnumerable<TSource> source, TResult[] array, Func<TSource, TResult> selector)
        {
            CopyTo(source, array, 0, selector);
        }

        public static void CopyTo<TSource, TResult>(this IEnumerable<TSource> source, TResult[] array, Int32 index, Func<TSource, TResult> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            if (source is IReadOnlyCollection<TSource> count && count.Count + index > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(array), array.Length, null);
            }
            
            using IEnumerator<TSource> enumerator = source.GetEnumerator();

            for (Int32 i = index; i < array.Length && enumerator.MoveNext(); i++)
            {
                array[i] = selector(enumerator.Current);
            }
        }
    }
}