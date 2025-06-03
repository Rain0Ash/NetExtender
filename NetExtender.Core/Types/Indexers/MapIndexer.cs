// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Indexers.Interfaces;
using NetExtender.Types.Maps;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Indexers
{
#pragma warning disable CS8714
    public class MapIndexer<T> : IMapIndexer<T>, IReadOnlyMapIndexer<T>
    {
        protected Int32 Null { get; set; } = -1;
        protected Map<T, Int32> Index { get; }

        public Int32 Count
        {
            get
            {
                return Null >= 0 ? Index.Count + 1 : Index.Count;
            }
        }

        public IEqualityComparer<T> Comparer
        {
            get
            {
                return Index.Comparer;
            }
        }

        public MapIndexer()
            : this((IEqualityComparer<T>?) null)
        {
        }

        public MapIndexer(IEqualityComparer<T>? comparer)
        {
            Index = new Map<T, Int32>(0, comparer, null);
        }

        public MapIndexer(IEnumerable<T> source)
            : this(source, null)
        {
        }

        public MapIndexer(IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Index = new Map<T, Int32>(source.CountIfMaterialized(0), comparer, null);
            AddRange(source);
        }

        protected void AddRange(IEnumerable<T> source)
        {
            foreach ((Int32 counter, T item) in source.Enumerate())
            {
                if (item is not null)
                {
                    Index.TryAdd(item, counter);
                    continue;
                }

                if (Null < 0)
                {
                    Null = counter;
                }
            }

            Index.TrimExcess();
        }

        public Boolean Contains(T item)
        {
            return item is not null ? Index.ContainsKey(item) : Null >= 0;
        }

        public Boolean ContainsIndex(Int32 index)
        {
            return index == Null || Index.ContainsValue(index);
        }

        public Int32 IndexOf(T item)
        {
            if (item is null)
            {
                return Null >= 0 ? Null : -1;
            }

            return Index.TryGetValue(item, out Int32 index) ? index : -1;
        }

        public T? ValueOf(Int32 index)
        {
            return index >= 0 && index != Null && Index.TryGetKey(index, out T? key) ? key : default;
        }

        public Boolean ValueOf(Int32 index, [MaybeNullWhen(false)] out T value)
        {
            if (index < 0)
            {
                value = default;
                return false;
            }

            if (index != Null)
            {
                return Index.TryGetKey(index, out value);
            }

            value = default!;
            return true;
        }

        public Boolean Rebuild(IEnumerable<T> source)
        {
            Clear();
            AddRange(source);
            return true;
        }

        public void Clear()
        {
            Null = -1;
            Index.Clear();
        }

        public void CopyTo(T[] array)
        {
            CopyTo(array, 0);
        }

        public void CopyTo(T[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0 || index > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (array.Length - index - 1 < Count)
            {
                throw new ArgumentOutOfRangeException(nameof(array), array.Length, null);
            }

            foreach (T item in this)
            {
                array[index++] = item;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Index.OrderKeysByValues().AppendAtIf(Null >= 0, Null, default(T)!).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Int32 this[T item]
        {
            get
            {
                return IndexOf(item);
            }
        }

        public T this[Int32 index]
        {
            get
            {
                return Index[index];
            }
        }
    }
#pragma warning restore CS8714
}