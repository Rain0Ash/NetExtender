// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Collections;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Immutable.Dictionaries.Interfaces;
using NetExtender.Types.Immutable.Maps.Interfaces;
using NetExtender.Types.Maps;
using NetExtender.Types.Maps.Interfaces;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        internal static IEnumerable<TOutput> ConvertAll<T, TOutput>(this IEnumerable<T> source, Converter<T, TOutput> converter)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            foreach (T item in source)
            {
                yield return converter(item);
            }
        }
        
        public static IEnumerable<TTo?> SelectAs<TFrom, TTo>(this IEnumerable<TFrom> source) where TTo : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (TFrom item in source)
            {
                yield return item as TTo;
            }
        }

        public static IEnumerable<TTo> SelectWhereIs<TFrom, TTo>(this IEnumerable<TFrom> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (TFrom item in source)
            {
                if (item is TTo convert)
                {
                    yield return convert;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ICollection<T> AsCollection<T>(this IEnumerable<T>? source)
        {
            return AsCollection(source, out _);
        }

        public static ICollection<T> AsCollection<T>(this IEnumerable<T>? source, out Int32 count)
        {
            ICollection<T> collection = source is not null ? source as ICollection<T> ?? source.ToArray() : Array.Empty<T>();
            count = collection.Count;

            return collection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyCollection<T> AsReadOnlyCollection<T>(this IEnumerable<T>? source)
        {
            return AsReadOnlyCollection(source, out _);
        }

        public static IReadOnlyCollection<T> AsReadOnlyCollection<T>(this IEnumerable<T>? source, out Int32 count)
        {
            IReadOnlyCollection<T> result = source switch
            {
                null => Array.Empty<T>(),
                IReadOnlyCollection<T> collection => collection,
                ICollection<T> collection => new CollectionReadOnlyWrapper<T>(collection),
                ICollection collection => new NonGenericReadOnlyCollectionWrapper<T>(collection),
                _ => source.ToArray()
            };

            count = result.Count;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> AsIReadOnlyList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IReadOnlyList<T> ?? source.ToList() : new List<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> AsIList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IList<T> ?? source.ToList() : new List<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] AsArray<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as T[] ?? source.ToArray() : Array.Empty<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T> AsList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as List<T> ?? source.ToList() : new List<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedList<TKey, TValue> AsSortedList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as SortedList<TKey, TValue> ?? source.ToSortedList() : new SortedList<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedList<TKey, TValue> AsSortedList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as SortedList<TKey, TValue> ?? source.ToSortedList(comparer) : new SortedList<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedList<T> AsLinkedList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as LinkedList<T> ?? source.ToLinkedList() : new LinkedList<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Stack<T> AsStack<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as Stack<T> ?? source.ToStack() : new Stack<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Queue<T> AsQueue<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as Queue<T> ?? source.ToQueue() : new Queue<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlySet<T> AsIReadOnlySet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IReadOnlySet<T> ?? source.ToHashSet() : new HashSet<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlySet<T> AsIReadOnlySet<T>(this IEnumerable<T>? source, IEqualityComparer<T>? comparer)
        {
            return source is not null ? source as IReadOnlySet<T> ?? source.ToHashSet(comparer) : new HashSet<T>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISet<T> AsISet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as ISet<T> ?? source.ToHashSet() : new HashSet<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISet<T> AsISet<T>(this IEnumerable<T>? source, IEqualityComparer<T>? comparer)
        {
            return source is not null ? source as ISet<T> ?? source.ToHashSet(comparer) : new HashSet<T>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<T> AsHashSet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as HashSet<T> ?? source.ToHashSet() : new HashSet<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<T> AsHashSet<T>(this IEnumerable<T>? source, IEqualityComparer<T>? comparer)
        {
            return source is not null ? source as HashSet<T> ?? source.ToHashSet(comparer) : new HashSet<T>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedSet<T> AsSortedSet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as SortedSet<T> ?? source.ToSortedSet() : new SortedSet<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedSet<T> AsHashSet<T>(this IEnumerable<T>? source, IComparer<T>? comparer)
        {
            return source is not null ? source as SortedSet<T> ?? source.ToSortedSet(comparer) : new SortedSet<T>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<TKey, TValue> AsIReadOnlyDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as IReadOnlyDictionary<TKey, TValue> ?? source.ToDictionary() : new Dictionary<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<TKey, TValue> AsIReadOnlyDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as IReadOnlyDictionary<TKey, TValue> ?? source.ToDictionary(comparer) : new Dictionary<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<TKey, TValue> AsIDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as IDictionary<TKey, TValue> ?? source.ToDictionary() : new Dictionary<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<TKey, TValue> AsIDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as IDictionary<TKey, TValue> ?? source.ToDictionary(comparer) : new Dictionary<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> AsDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as Dictionary<TKey, TValue> ?? source.ToDictionary() : new Dictionary<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> AsDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as Dictionary<TKey, TValue> ?? source.ToDictionary(comparer) : new Dictionary<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyIndexDictionary<TKey, TValue> AsIReadOnlyIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as IReadOnlyIndexDictionary<TKey, TValue> ?? source.ToIndexDictionary() : new IndexDictionary<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyIndexDictionary<TKey, TValue> AsIReadOnlyIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as IReadOnlyIndexDictionary<TKey, TValue> ?? source.ToIndexDictionary(comparer) : new IndexDictionary<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IIndexDictionary<TKey, TValue> AsIIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as IIndexDictionary<TKey, TValue> ?? source.ToIndexDictionary() : new IndexDictionary<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IIndexDictionary<TKey, TValue> AsIIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as IIndexDictionary<TKey, TValue> ?? source.ToIndexDictionary(comparer) : new IndexDictionary<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> AsIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as IndexDictionary<TKey, TValue> ?? source.ToIndexDictionary() : new IndexDictionary<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> AsIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as IndexDictionary<TKey, TValue> ?? source.ToIndexDictionary(comparer) : new IndexDictionary<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> AsSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as SortedDictionary<TKey, TValue> ?? source.ToSortedDictionary() : new SortedDictionary<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> AsSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? equality) where TKey : notnull
        {
            return source is not null ? source as SortedDictionary<TKey, TValue> ?? source.ToSortedDictionary(equality) : new SortedDictionary<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> AsSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as SortedDictionary<TKey, TValue> ?? source.ToSortedDictionary(comparer) : new SortedDictionary<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> AsSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? equality, IComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as SortedDictionary<TKey, TValue> ?? source.ToSortedDictionary(equality, comparer) : new SortedDictionary<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyMap<TKey, TValue> AsIReadOnlyMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IReadOnlyMap<TKey, TValue> ?? source.ToMap() : new Map<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyMap<TKey, TValue> AsIReadOnlyMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IReadOnlyMap<TKey, TValue> ?? source.ToMap(comparer) : new Map<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyMap<TKey, TValue> AsIReadOnlyMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IReadOnlyMap<TKey, TValue> ?? source.ToMap(keyComparer, valueComparer) : new Map<TKey, TValue>(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMap<TKey, TValue> AsIMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IMap<TKey, TValue> ?? source.ToMap() : new Map<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMap<TKey, TValue> AsIMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IMap<TKey, TValue> ?? source.ToMap(comparer) : new Map<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMap<TKey, TValue> AsIMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IMap<TKey, TValue> ?? source.ToMap(keyComparer, valueComparer) : new Map<TKey, TValue>(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> AsMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as Map<TKey, TValue> ?? source.ToMap() : new Map<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> AsMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as Map<TKey, TValue> ?? source.ToMap(comparer) : new Map<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> AsMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as Map<TKey, TValue> ?? source.ToMap(keyComparer, valueComparer) : new Map<TKey, TValue>(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyIndexMap<TKey, TValue> AsIReadOnlyIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IReadOnlyIndexMap<TKey, TValue> ?? source.ToIndexMap() : new IndexMap<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyMap<TKey, TValue> AsIReadOnlyIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IReadOnlyIndexMap<TKey, TValue> ?? source.ToIndexMap(comparer) : new IndexMap<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyIndexMap<TKey, TValue> AsIReadOnlyIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IReadOnlyIndexMap<TKey, TValue> ?? source.ToIndexMap(keyComparer, valueComparer) : new IndexMap<TKey, TValue>(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IIndexMap<TKey, TValue> AsIIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IIndexMap<TKey, TValue> ?? source.ToIndexMap() : new IndexMap<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IIndexMap<TKey, TValue> AsIIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IIndexMap<TKey, TValue> ?? source.ToIndexMap(comparer) : new IndexMap<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IIndexMap<TKey, TValue> AsIIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IIndexMap<TKey, TValue> ?? source.ToIndexMap(keyComparer, valueComparer) : new IndexMap<TKey, TValue>(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TValue> AsIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IndexMap<TKey, TValue> ?? source.ToIndexMap() : new IndexMap<TKey, TValue>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TValue> AsIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IndexMap<TKey, TValue> ?? source.ToIndexMap(comparer) : new IndexMap<TKey, TValue>(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TValue> AsIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IndexMap<TKey, TValue> ?? source.ToIndexMap(keyComparer, valueComparer) : new IndexMap<TKey, TValue>(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is<T>(this CollectionType type) where T : IEnumerable
        {
            return Is(typeof(T), type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is<T>(this CollectionType type, Boolean strict) where T : IEnumerable
        {
            return Is(typeof(T), type, strict);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is(this IEnumerable? enumerable, CollectionType type)
        {
            return Is(enumerable?.GetType(), type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is(this IEnumerable? enumerable, CollectionType type, Boolean strict)
        {
            return Is(enumerable?.GetType(), type, strict);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is(this Type? type, CollectionType collection)
        {
            return type is not null && type.GetCollectionType().HasFlag(collection);
        }

        public static Boolean Is(this Type? type, CollectionType collection, Boolean strict)
        {
            if (type is null)
            {
                return false;
            }

            Boolean istype = collection switch
            {
                CollectionType.None => true,
                CollectionType.Generic => type.IsGenericType,
                CollectionType.Enumerable => TypeStorage.IsEnumerable(type),
                CollectionType.GenericEnumerable => TypeStorage.IsGenericEnumerable(type),
                CollectionType.Collection => TypeStorage.IsCollection(type),
                CollectionType.GenericCollection => TypeStorage.IsGenericCollection(type),
                CollectionType.Array => TypeStorage.IsArray(type),
                CollectionType.GenericArray => TypeStorage.IsGenericArray(type),
                CollectionType.List => TypeStorage.IsList(type),
                CollectionType.GenericList => TypeStorage.IsGenericList(type),
                CollectionType.Set => TypeStorage.IsSet(type),
                CollectionType.GenericSet => TypeStorage.IsGenericSet(type),
                CollectionType.Dictionary => TypeStorage.IsDictionary(type),
                CollectionType.GenericDictionary => TypeStorage.IsGenericDictionary(type),
                CollectionType.Map => TypeStorage.IsMap(type),
                CollectionType.GenericMap => TypeStorage.IsGenericMap(type),
                CollectionType.Stack => TypeStorage.IsStack(type),
                CollectionType.GenericStack => TypeStorage.IsGenericStack(type),
                CollectionType.Queue => TypeStorage.IsQueue(type),
                CollectionType.GenericQueue => TypeStorage.IsGenericQueue(type),
                _ => throw new EnumUndefinedOrNotSupportedException<CollectionType>(collection, nameof(collection), null)
            };

            if (strict || istype)
            {
                return istype;
            }

            return Is(type, collection);
        }

        public static CollectionType GetCollectionType<T>() where T : IEnumerable
        {
            return GetCollectionType(typeof(T));
        }

        public static CollectionType GetCollectionType(this IEnumerable? enumerable)
        {
            return GetCollectionType(enumerable?.GetType());
        }

        // ReSharper disable once CognitiveComplexity
        public static CollectionType GetCollectionType(this Type? type)
        {
            if (type is null)
            {
                return CollectionType.None;
            }

            CollectionType collection = CollectionType.None;

            if (TypeStorage.IsGenericArray(type))
            {
                collection |= CollectionType.GenericArray;
            }
            else if (TypeStorage.IsArray(type))
            {
                collection |= CollectionType.Array;
            }

            if (TypeStorage.IsGenericList(type))
            {
                collection |= CollectionType.GenericList;
            }
            else if (TypeStorage.IsList(type))
            {
                collection |= CollectionType.List;
            }

            if (TypeStorage.IsGenericSet(type))
            {
                collection |= CollectionType.GenericSet;
            }
            else if (TypeStorage.IsSet(type))
            {
                collection |= CollectionType.Set;
            }

            if (TypeStorage.IsGenericDictionary(type))
            {
                collection |= CollectionType.GenericDictionary;
            }
            else if (TypeStorage.IsDictionary(type))
            {
                collection |= CollectionType.Dictionary;
            }

            if (TypeStorage.IsGenericMap(type))
            {
                collection |= CollectionType.GenericMap;
            }
            else if (TypeStorage.IsMap(type))
            {
                collection |= CollectionType.Map;
            }

            if (TypeStorage.IsGenericStack(type))
            {
                collection |= CollectionType.GenericStack;
            }
            else if (TypeStorage.IsStack(type))
            {
                collection |= CollectionType.Stack;
            }

            if (TypeStorage.IsGenericQueue(type))
            {
                collection |= CollectionType.GenericQueue;
            }
            else if (TypeStorage.IsQueue(type))
            {
                collection |= CollectionType.Queue;
            }

            if (collection.HasFlag(CollectionType.Collection))
            {
                return collection;
            }

            if (TypeStorage.IsGenericCollection(type))
            {
                collection |= CollectionType.GenericCollection;
            }
            else if (TypeStorage.IsCollection(type))
            {
                collection |= CollectionType.Collection;
            }

            if (collection.HasFlag(CollectionType.Enumerable))
            {
                return collection;
            }

            if (TypeStorage.IsGenericEnumerable(type))
            {
                collection |= CollectionType.GenericEnumerable;
            }
            else if (TypeStorage.IsEnumerable(type))
            {
                collection |= CollectionType.Enumerable;
            }

            return collection;
        }

        internal static class TypeStorage
        {
            private readonly struct Status
            {
                public Boolean IsType { get; init; }
                public Boolean IsNonGenericType { get; init; }
                public Boolean IsGenericType { get; init; }

                public Status(Boolean type, Boolean nongeneric, Boolean generic)
                {
                    IsType = type;
                    IsNonGenericType = nongeneric;
                    IsGenericType = generic;
                }
            }

            private static ConcurrentDictionary<Type, Status> EnumerableStorage { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> CollectionStorage { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> ListStorage { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> SetStorage { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> DictionaryStorage { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> MapStorage { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> StackStorage { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> QueueStorage { get; } = new ConcurrentDictionary<Type, Status>();

            public static IImmutableSet<Type> EnumerableTypes { get; } = ImmutableHashSet.Create(typeof(IEnumerable));
            public static IImmutableSet<Type> GenericEnumerableTypes { get; } = ImmutableHashSet.Create(typeof(IEnumerable<>));
            public static IImmutableSet<Type> CollectionTypes { get; } = ImmutableHashSet.Create(typeof(ICollection));
            public static IImmutableSet<Type> GenericCollectionTypes { get; } = ImmutableHashSet.Create(typeof(ICollection<>), typeof(IReadOnlyCollection<>));
            public static IImmutableSet<Type> ListTypes { get; } = ImmutableHashSet.Create(typeof(IList));
            public static IImmutableSet<Type> GenericListTypes { get; } = ImmutableHashSet.Create(typeof(IList<>), typeof(IReadOnlyList<>), typeof(IImmutableList<>));
            public static IImmutableSet<Type> SetTypes { get; } = ImmutableHashSet.Create(typeof(ISet));
            public static IImmutableSet<Type> GenericSetTypes { get; } = ImmutableHashSet.Create(typeof(ISet<>), typeof(IReadOnlySet<>), typeof(IImmutableSet<>));
            public static IImmutableSet<Type> DictionaryTypes { get; } = ImmutableHashSet.Create(typeof(IDictionary));
            public static IImmutableSet<Type> GenericDictionaryTypes { get; } = ImmutableHashSet.Create(typeof(IDictionary<,>), typeof(IReadOnlyDictionary<,>), typeof(IIndexDictionary<,>), typeof(IReadOnlyIndexDictionary<,>), typeof(IImmutableDictionary<,>), typeof(IImmutableIndexDictionary<,>));
            public static IImmutableSet<Type> MapTypes { get; } = ImmutableHashSet.Create<Type>();
            public static IImmutableSet<Type> GenericMapTypes { get; } = ImmutableHashSet.Create(typeof(IMap<,>), typeof(IReadOnlyMap<,>), typeof(IIndexMap<,>), typeof(IReadOnlyIndexMap<,>), typeof(IImmutableMap<,>), typeof(IImmutableIndexMap<,>));
            public static IImmutableSet<Type> StackTypes { get; } = ImmutableHashSet.Create(typeof(Stack));
            public static IImmutableSet<Type> GenericStackTypes { get; } = ImmutableHashSet.Create(typeof(Stack<>), typeof(ConcurrentStack<>), typeof(IImmutableStack<>));
            public static IImmutableSet<Type> QueueTypes { get; } = ImmutableHashSet.Create(typeof(Queue));
            public static IImmutableSet<Type> GenericQueueTypes { get; } = ImmutableHashSet.Create(typeof(Queue<>), typeof(ConcurrentQueue<>), typeof(IImmutableQueue<>));

            private static Status Create(Type type, IImmutableSet<Type> types, IImmutableSet<Type> generics)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                type = type.TryGetGenericTypeDefinition();

                Type[] interfaces = type.TryGetGenericTypeDefinitionInterfaces();
                Boolean isnongeneric = types.Contains(type) || types.Overlaps(interfaces);
                Boolean isgeneric = generics.Contains(type) || generics.Overlaps(interfaces);

                Boolean istype = isnongeneric || isgeneric;

                return new Status(istype, isnongeneric, isgeneric);
            }

            private static Status CreateEnumerable(Type type)
            {
                return Create(type, EnumerableTypes, GenericEnumerableTypes);
            }

            private static Status CreateCollection(Type type)
            {
                return Create(type, CollectionTypes, GenericCollectionTypes);
            }

            private static Status CreateList(Type type)
            {
                return Create(type, ListTypes, GenericListTypes);
            }

            private static Status CreateSet(Type type)
            {
                return Create(type, SetTypes, GenericSetTypes);
            }

            private static Status CreateDictionary(Type type)
            {
                return Create(type, DictionaryTypes, GenericDictionaryTypes);
            }

            private static Status CreateMap(Type type)
            {
                return Create(type, MapTypes, GenericMapTypes);
            }

            private static Status CreateStack(Type type)
            {
                return Create(type, StackTypes, GenericStackTypes);
            }

            private static Status CreateQueue(Type type)
            {
                return Create(type, QueueTypes, GenericQueueTypes);
            }

            private static Boolean Is(Type? type, ConcurrentDictionary<Type, Status> cache, Func<Type, Status> factory)
            {
                if (type is null)
                {
                    return false;
                }

                if (type.IsAbstract && type.IsSealed)
                {
                    return false;
                }

                return cache.GetOrAdd(type.TryGetGenericTypeDefinition(), factory).IsType;
            }

            private static Boolean IsNonGeneric(Type? type, ConcurrentDictionary<Type, Status> cache, Func<Type, Status> factory)
            {
                if (type is null)
                {
                    return false;
                }

                if (type.IsGenericType || type.IsAbstract && type.IsSealed)
                {
                    return false;
                }

                return cache.GetOrAdd(type, factory).IsNonGenericType;
            }

            private static Boolean IsGeneric(Type? type, ConcurrentDictionary<Type, Status> cache, Func<Type, Status> factory)
            {
                if (type is null)
                {
                    return false;
                }

                if (!type.IsGenericType || type.IsAbstract && type.IsSealed)
                {
                    return false;
                }

                return cache.GetOrAdd(type.GetGenericTypeDefinition(), factory).IsGenericType;
            }

            public static Boolean IsArray(Type? type)
            {
                return type is not null && type.IsArray;
            }

            public static Boolean IsNonGenericArray(Type? type)
            {
                return type is not null && type.IsArray && !type.IsGenericType;
            }

            public static Boolean IsGenericArray(Type? type)
            {
                return type is not null && type.IsArray && type.IsGenericType;
            }

            public static Boolean IsEnumerable(Type? type)
            {
                return type is not null && Is(type, EnumerableStorage, CreateEnumerable);
            }

            public static Boolean IsNonGenericEnumerable(Type? type)
            {
                return type is not null && IsNonGeneric(type, EnumerableStorage, CreateEnumerable);
            }

            public static Boolean IsGenericEnumerable(Type? type)
            {
                return type is not null && IsGeneric(type, EnumerableStorage, CreateEnumerable);
            }

            public static Boolean IsCollection(Type? type)
            {
                return type is not null && Is(type, CollectionStorage, CreateCollection);
            }

            public static Boolean IsNonGenericCollection(Type? type)
            {
                return type is not null && IsNonGeneric(type, CollectionStorage, CreateCollection);
            }

            public static Boolean IsGenericCollection(Type? type)
            {
                return type is not null && IsGeneric(type, CollectionStorage, CreateCollection);
            }

            public static Boolean IsList(Type? type)
            {
                return type is not null && Is(type, ListStorage, CreateList);
            }

            public static Boolean IsNonGenericList(Type? type)
            {
                return type is not null && IsNonGeneric(type, ListStorage, CreateList);
            }

            public static Boolean IsGenericList(Type? type)
            {
                return type is not null && IsGeneric(type, ListStorage, CreateList);
            }

            public static Boolean IsSet(Type? type)
            {
                return type is not null && Is(type, SetStorage, CreateSet);
            }

            public static Boolean IsNonGenericSet(Type? type)
            {
                return type is not null && IsNonGeneric(type, SetStorage, CreateSet);
            }

            public static Boolean IsGenericSet(Type? type)
            {
                return type is not null && IsGeneric(type, SetStorage, CreateSet);
            }

            public static Boolean IsDictionary(Type? type)
            {
                return type is not null && Is(type, DictionaryStorage, CreateDictionary);
            }

            public static Boolean IsNonGenericDictionary(Type? type)
            {
                return type is not null && IsNonGeneric(type, DictionaryStorage, CreateDictionary);
            }

            public static Boolean IsGenericDictionary(Type? type)
            {
                return type is not null && IsGeneric(type, DictionaryStorage, CreateDictionary);
            }

            public static Boolean IsMap(Type? type)
            {
                return type is not null && Is(type, MapStorage, CreateMap);
            }

            public static Boolean IsNonGenericMap(Type? type)
            {
                return type is not null && IsNonGeneric(type, MapStorage, CreateMap);
            }

            public static Boolean IsGenericMap(Type? type)
            {
                return type is not null && IsGeneric(type, MapStorage, CreateMap);
            }

            public static Boolean IsStack(Type? type)
            {
                return type is not null && Is(type, StackStorage, CreateStack);
            }

            public static Boolean IsNonGenericStack(Type? type)
            {
                return type is not null && IsNonGeneric(type, StackStorage, CreateStack);
            }

            public static Boolean IsGenericStack(Type? type)
            {
                return type is not null && IsGeneric(type, StackStorage, CreateStack);
            }

            public static Boolean IsQueue(Type? type)
            {
                return type is not null && Is(type, QueueStorage, CreateQueue);
            }

            public static Boolean IsNonGenericQueue(Type? type)
            {
                return type is not null && IsNonGeneric(type, QueueStorage, CreateQueue);
            }

            public static Boolean IsGenericQueue(Type? type)
            {
                return type is not null && IsGeneric(type, QueueStorage, CreateQueue);
            }
        }
    }
}