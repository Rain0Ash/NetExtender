// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Initializer.Types.Indexers.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Initializer.Types.Indexers
{
#pragma warning disable CS8714
    public class Indexer<T> : IIndexer<T>, IReadOnlyIndexer<T>
    {
        protected Int32 Null { get; set; } = -1;
        protected Dictionary<T, Int32> Index { get; }

        public Int32 Count
        {
            get
            {
                return Null >= 0 ? Index.Count + 1 : Index.Count;
            }
        }

        public Indexer()
            : this((IEqualityComparer<T>?) null)
        {
        }

        public Indexer(IEqualityComparer<T>? comparer)
        {
            Index = new Dictionary<T, Int32>(0, comparer);
        }

        public Indexer(IEnumerable<T> source)
            : this(source, null)
        {
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public Indexer(IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Index = new Dictionary<T, Int32>(source.CountIfMaterialized() ?? 0, comparer);
            AddRange(source);
        }

        private void AddRange(IEnumerable<T> source)
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

        public Int32 IndexOf(T item)
        {
            if (item is null)
            {
                return Null >= 0 ? Null : -1;
            }

            return Index.TryGetValue(item, out Int32 index) ? index : -1;
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

            Int32 i = 0;
            foreach (T item in this)
            {
                array[index + i++] = item;
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
    }
#pragma warning restore CS8714
}