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
using NetExtender.Types.Maps;
using NetExtender.Types.Maps.Interfaces;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
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
                ICollection<T> collection => new ReadOnlyCollectionWrapper<T>(collection),
                ICollection collection => new NonGenericReadOnlyCollectionWrapper<T>(collection),
                _ => source.ToArray()
            };

            count = result.Count;
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> AsIReadOnlyList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is IReadOnlyList<T> list ? list : source.ToList() : new List<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> AsIList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is IList<T> list ? list : source.ToList() : new List<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] AsArray<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is T[] array ? array : source.ToArray() : Array.Empty<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T> AsList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is List<T> list ? list : source.ToList() : new List<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedList<T> AsLinkedList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is LinkedList<T> linked ? linked : source.ToLinkedList() : new LinkedList<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Stack<T> AsStack<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is Stack<T> stack ? stack : source.ToStack() : new Stack<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Queue<T> AsQueue<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is Queue<T> queue ? queue : source.ToQueue() : new Queue<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlySet<T> AsIReadOnlySet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is IReadOnlySet<T> set ? set : source.ToHashSet() : new HashSet<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlySet<T> AsIReadOnlySet<T>(this IEnumerable<T>? source, IEqualityComparer<T>? comparer)
        {
            return source is not null ? source is IReadOnlySet<T> set ? set : source.ToHashSet(comparer) : new HashSet<T>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISet<T> AsISet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is ISet<T> set ? set : source.ToHashSet() : new HashSet<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISet<T> AsISet<T>(this IEnumerable<T>? source, IEqualityComparer<T>? comparer)
        {
            return source is not null ? source is ISet<T> set ? set : source.ToHashSet(comparer) : new HashSet<T>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<T> AsHashSet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is HashSet<T> set ? set : source.ToHashSet() : new HashSet<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<T> AsHashSet<T>(this IEnumerable<T>? source, IEqualityComparer<T>? comparer)
        {
            return source is not null ? source is HashSet<T> set ? set : source.ToHashSet(comparer) : new HashSet<T>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedSet<T> AsSortedSet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source is SortedSet<T> set ? set : source.ToSortedSet() : new SortedSet<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedSet<T> AsHashSet<T>(this IEnumerable<T>? source, IComparer<T>? comparer)
        {
            return source is not null ? source is SortedSet<T> set ? set : source.ToSortedSet(comparer) : new SortedSet<T>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<TKey, TValue> AsIReadOnlyDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source is IReadOnlyDictionary<TKey, TValue> dictionary ? dictionary : source.ToDictionary() : new Dictionary<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<TKey, TValue> AsIReadOnlyDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source is IReadOnlyDictionary<TKey, TValue> dictionary ? dictionary : source.ToDictionary(comparer) : new Dictionary<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<TKey, TValue> AsIDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source is IDictionary<TKey, TValue> dictionary ? dictionary : source.ToDictionary() : new Dictionary<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<TKey, TValue> AsIDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source is IDictionary<TKey, TValue> dictionary ? dictionary : source.ToDictionary(comparer) : new Dictionary<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> AsDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source is Dictionary<TKey, TValue> dictionary ? dictionary : source.ToDictionary() : new Dictionary<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> AsDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source is Dictionary<TKey, TValue> dictionary ? dictionary : source.ToDictionary(comparer) : new Dictionary<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyIndexDictionary<TKey, TValue> AsIReadOnlyIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source is IReadOnlyIndexDictionary<TKey, TValue> dictionary ? dictionary : source.ToIndexDictionary() : new IndexDictionary<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyIndexDictionary<TKey, TValue> AsIReadOnlyIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source is IReadOnlyIndexDictionary<TKey, TValue> dictionary ? dictionary : source.ToIndexDictionary(comparer) : new IndexDictionary<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IIndexDictionary<TKey, TValue> AsIIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source is IIndexDictionary<TKey, TValue> dictionary ? dictionary : source.ToIndexDictionary() : new IndexDictionary<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IIndexDictionary<TKey, TValue> AsIIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source is IIndexDictionary<TKey, TValue> dictionary ? dictionary : source.ToIndexDictionary(comparer) : new IndexDictionary<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> AsIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source is IndexDictionary<TKey, TValue> dictionary ? dictionary : source.ToIndexDictionary() : new IndexDictionary<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> AsIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source is IndexDictionary<TKey, TValue> dictionary ? dictionary : source.ToIndexDictionary(comparer) : new IndexDictionary<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> AsSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source is SortedDictionary<TKey, TValue> dictionary ? dictionary : source.ToSortedDictionary() : new SortedDictionary<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> AsSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? equality) where TKey : notnull
        {
            return source is not null ? source is SortedDictionary<TKey, TValue> dictionary ? dictionary : source.ToSortedDictionary(equality) : new SortedDictionary<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> AsSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source is SortedDictionary<TKey, TValue> dictionary ? dictionary : source.ToSortedDictionary(comparer) : new SortedDictionary<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> AsSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? equality, IComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source is SortedDictionary<TKey, TValue> dictionary ? dictionary : source.ToSortedDictionary(equality, comparer) : new SortedDictionary<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyMap<TKey, TValue> AsIReadOnlyMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IReadOnlyMap<TKey, TValue> map ? map : source.ToMap() : new Map<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyMap<TKey, TValue> AsIReadOnlyMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IReadOnlyMap<TKey, TValue> map ? map : source.ToMap(comparer) : new Map<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyMap<TKey, TValue> AsIReadOnlyMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IReadOnlyMap<TKey, TValue> map ? map : source.ToMap(keyComparer, valueComparer) : new Map<TKey, TValue>(keyComparer, valueComparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMap<TKey, TValue> AsIMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IMap<TKey, TValue> map ? map : source.ToMap() : new Map<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMap<TKey, TValue> AsIMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IMap<TKey, TValue> map ? map : source.ToMap(comparer) : new Map<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMap<TKey, TValue> AsIMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IMap<TKey, TValue> map ? map : source.ToMap(keyComparer, valueComparer) : new Map<TKey, TValue>(keyComparer, valueComparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> AsMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is Map<TKey, TValue> map ? map : source.ToMap() : new Map<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> AsMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is Map<TKey, TValue> map ? map : source.ToMap(comparer) : new Map<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> AsMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is Map<TKey, TValue> map ? map : source.ToMap(keyComparer, valueComparer) : new Map<TKey, TValue>(keyComparer, valueComparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyIndexMap<TKey, TValue> AsIReadOnlyIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IReadOnlyIndexMap<TKey, TValue> map ? map : source.ToIndexMap() : new IndexMap<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyMap<TKey, TValue> AsIReadOnlyIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IReadOnlyIndexMap<TKey, TValue> map ? map : source.ToIndexMap(comparer) : new IndexMap<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyIndexMap<TKey, TValue> AsIReadOnlyIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IReadOnlyIndexMap<TKey, TValue> map ? map : source.ToIndexMap(keyComparer, valueComparer) : new IndexMap<TKey, TValue>(keyComparer, valueComparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IIndexMap<TKey, TValue> AsIIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IIndexMap<TKey, TValue> map ? map : source.ToIndexMap() : new IndexMap<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IIndexMap<TKey, TValue> AsIIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IIndexMap<TKey, TValue> map ? map : source.ToIndexMap(comparer) : new IndexMap<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IIndexMap<TKey, TValue> AsIIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IIndexMap<TKey, TValue> map ? map : source.ToIndexMap(keyComparer, valueComparer) : new IndexMap<TKey, TValue>(keyComparer, valueComparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TValue> AsIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IndexMap<TKey, TValue> map ? map : source.ToIndexMap() : new IndexMap<TKey, TValue>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TValue> AsIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IndexMap<TKey, TValue> map ? map : source.ToIndexMap(comparer) : new IndexMap<TKey, TValue>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TValue> AsIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source is IndexMap<TKey, TValue> map ? map : source.ToIndexMap(keyComparer, valueComparer) : new IndexMap<TKey, TValue>(keyComparer, valueComparer);
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
        public static Boolean Is(this IEnumerable enumerable, CollectionType type)
        {
            return Is(enumerable?.GetType(), type);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is(this IEnumerable enumerable, CollectionType type, Boolean strict)
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
                CollectionType.Enumerable => TypeCache.IsEnumerable(type),
                CollectionType.GenericEnumerable => TypeCache.IsGenericEnumerable(type),
                CollectionType.Collection => TypeCache.IsCollection(type),
                CollectionType.GenericCollection => TypeCache.IsGenericCollection(type),
                CollectionType.Array => TypeCache.IsArray(type),
                CollectionType.GenericArray => TypeCache.IsGenericArray(type),
                CollectionType.List => TypeCache.IsList(type),
                CollectionType.GenericList => TypeCache.IsGenericList(type),
                CollectionType.Set => TypeCache.IsSet(type),
                CollectionType.GenericSet => TypeCache.IsGenericSet(type),
                CollectionType.Dictionary => TypeCache.IsDictionary(type),
                CollectionType.GenericDictionary => TypeCache.IsGenericDictionary(type),
                CollectionType.Stack => TypeCache.IsStack(type),
                CollectionType.GenericStack => TypeCache.IsGenericStack(type),
                CollectionType.Queue => TypeCache.IsQueue(type),
                CollectionType.GenericQueue => TypeCache.IsGenericQueue(type),
                _ => throw new NotSupportedException()
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

        public static CollectionType GetCollectionType(this IEnumerable enumerable)
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

            if (TypeCache.IsGenericArray(type))
            {
                collection |= CollectionType.GenericArray;
            }
            else if (TypeCache.IsArray(type))
            {
                collection |= CollectionType.Array;
            }
            
            if (TypeCache.IsGenericList(type))
            {
                collection |= CollectionType.GenericList;
            }
            else if (TypeCache.IsList(type))
            {
                collection |= CollectionType.List;
            }
            
            if (TypeCache.IsGenericSet(type))
            {
                collection |= CollectionType.GenericSet;
            }
            else if (TypeCache.IsSet(type))
            {
                collection |= CollectionType.Set;
            }
            
            if (TypeCache.IsGenericDictionary(type))
            {
                collection |= CollectionType.GenericDictionary;
            }
            else if (TypeCache.IsDictionary(type))
            {
                collection |= CollectionType.Dictionary;
            }
            
            if (TypeCache.IsGenericStack(type))
            {
                collection |= CollectionType.GenericStack;
            }
            else if (TypeCache.IsStack(type))
            {
                collection |= CollectionType.Stack;
            }
            
            if (TypeCache.IsGenericQueue(type))
            {
                collection |= CollectionType.GenericQueue;
            }
            else if (TypeCache.IsQueue(type))
            {
                collection |= CollectionType.Queue;
            }

            if (collection.HasFlag(CollectionType.Collection))
            {
                return collection;
            }

            if (TypeCache.IsGenericCollection(type))
            {
                collection |= CollectionType.GenericCollection;
            }
            else if (TypeCache.IsCollection(type))
            {
                collection |= CollectionType.Collection;
            }

            if (collection.HasFlag(CollectionType.Enumerable))
            {
                return collection;
            }
            
            if (TypeCache.IsGenericEnumerable(type))
            {
                collection |= CollectionType.GenericEnumerable;
            }
            else if (TypeCache.IsEnumerable(type))
            {
                collection |= CollectionType.Enumerable;
            }

            return collection;
        }

        internal static class TypeCache
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
            
            private static ConcurrentDictionary<Type, Status> EnumerableCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> CollectionCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> ListCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> SetCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> DictionaryCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> StackCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> QueueCache { get; } = new ConcurrentDictionary<Type, Status>();
            
            public static IImmutableSet<Type> EnumerableTypes { get; } = new HashSet<Type>{typeof(IEnumerable)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericEnumerableTypes { get; } = new HashSet<Type>{typeof(IEnumerable<>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> CollectionTypes { get; } = new HashSet<Type>{typeof(ICollection)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericCollectionTypes { get; } = new HashSet<Type>{typeof(ICollection<>), typeof(IReadOnlyCollection<>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> ListTypes { get; } = new HashSet<Type>{typeof(IList)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericListTypes { get; } = new HashSet<Type>{typeof(IList<>), typeof(IReadOnlyList<>), typeof(IImmutableList<>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> SetTypes { get; } = new HashSet<Type>{typeof(ISet)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericSetTypes { get; } = new HashSet<Type>{typeof(ISet<>), typeof(IReadOnlySet<>), typeof(IImmutableSet<>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> DictionaryTypes { get; } = new HashSet<Type>{typeof(IDictionary)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericDictionaryTypes { get; } = new HashSet<Type>{typeof(IDictionary<,>), typeof(IReadOnlyDictionary<,>), typeof(IImmutableDictionary<,>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> StackTypes { get; } = new HashSet<Type>{typeof(Stack)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericStackTypes { get; } = new HashSet<Type>{typeof(Stack<>), typeof(ConcurrentStack<>), typeof(IImmutableStack<>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> QueueTypes { get; } = new HashSet<Type>{typeof(Queue)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericQueueTypes { get; } = new HashSet<Type>{typeof(Queue<>), typeof(ConcurrentQueue<>), typeof(IImmutableQueue<>)}.ToImmutableHashSet();

            private static Status Create(Type type, IImmutableSet<Type> types, IImmutableSet<Type> generics)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                type = type.TryGetGenericTypeDefinition();

                Type[] interfaces = type.GetGenericTypeDefinitionInterfaces();
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
                return type is not null && Is(type, EnumerableCache, CreateEnumerable);
            }
            
            public static Boolean IsNonGenericEnumerable(Type? type)
            {
                return type is not null && IsNonGeneric(type, EnumerableCache, CreateEnumerable);
            }
            
            public static Boolean IsGenericEnumerable(Type? type)
            {
                return type is not null && IsGeneric(type, EnumerableCache, CreateEnumerable);
            }
            
            public static Boolean IsCollection(Type? type)
            {
                return type is not null && Is(type, CollectionCache, CreateCollection);
            }
            
            public static Boolean IsNonGenericCollection(Type? type)
            {
                return type is not null && IsNonGeneric(type, CollectionCache, CreateCollection);
            }
            
            public static Boolean IsGenericCollection(Type? type)
            {
                return type is not null && IsGeneric(type, CollectionCache, CreateCollection);
            }
            
            public static Boolean IsList(Type? type)
            {
                return type is not null && Is(type, ListCache, CreateList);
            }
            
            public static Boolean IsNonGenericList(Type? type)
            {
                return type is not null && IsNonGeneric(type, ListCache, CreateList);
            }
            
            public static Boolean IsGenericList(Type? type)
            {
                return type is not null && IsGeneric(type, ListCache, CreateList);
            }

            public static Boolean IsSet(Type? type)
            {
                return type is not null && Is(type, SetCache, CreateSet);
            }
            
            public static Boolean IsNonGenericSet(Type? type)
            {
                return type is not null && IsNonGeneric(type, SetCache, CreateSet);
            }
            
            public static Boolean IsGenericSet(Type? type)
            {
                return type is not null && IsGeneric(type, SetCache, CreateSet);
            }
            
            public static Boolean IsDictionary(Type? type)
            {
                return type is not null && Is(type, DictionaryCache, CreateDictionary);
            }
            
            public static Boolean IsNonGenericDictionary(Type? type)
            {
                return type is not null && IsNonGeneric(type, DictionaryCache, CreateDictionary);
            }
            
            public static Boolean IsGenericDictionary(Type? type)
            {
                return type is not null && IsGeneric(type, DictionaryCache, CreateDictionary);
            }
            
            public static Boolean IsStack(Type? type)
            {
                return type is not null && Is(type, StackCache, CreateStack);
            }
            
            public static Boolean IsNonGenericStack(Type? type)
            {
                return type is not null && IsNonGeneric(type, StackCache, CreateStack);
            }
            
            public static Boolean IsGenericStack(Type? type)
            {
                return type is not null && IsGeneric(type, StackCache, CreateStack);
            }
            
            public static Boolean IsQueue(Type? type)
            {
                return type is not null && Is(type, QueueCache, CreateQueue);
            }
            
            public static Boolean IsNonGenericQueue(Type? type)
            {
                return type is not null && IsNonGeneric(type, QueueCache, CreateQueue);
            }
            
            public static Boolean IsGenericQueue(Type? type)
            {
                return type is not null && IsGeneric(type, QueueCache, CreateQueue);
            }
        }
    }
}