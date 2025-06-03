// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Collections
{
    public sealed class SelectorCollectionWrapper<T, TKey> : ICollection<TKey>
    {
        private ICollection<T> Internal { get; }
        private Func<T, TKey> Selector { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return typeof(T) != typeof(TKey) || Internal.IsReadOnly;
            }
        }

        public SelectorCollectionWrapper(ICollection<T> collection, Func<T, TKey> selector)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public Boolean Contains(TKey item)
        {
            return Internal.Select(Selector).Contains(item);
        }

        public void Add(TKey item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            Internal.Add((T) (Object) item!);
        }

        public Boolean Remove(TKey item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            return Internal.Remove((T) (Object) item!);
        }

        public void Clear()
        {
            if (Internal.IsReadOnly)
            {
                throw new NotSupportedException();
            }

            Internal.Clear();
        }

        public void CopyTo(TKey[] array, Int32 index)
        {
            Internal.Select(Selector).CopyTo(array, index);
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return Internal.Select(Selector).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public sealed class ReadOnlySelectorCollectionWrapper<T, TKey> : IReadOnlyCollection<TKey>
    {
        private IReadOnlyCollection<T> Internal { get; }
        private Func<T, TKey> Selector { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public ReadOnlySelectorCollectionWrapper(IReadOnlyCollection<T> collection, Func<T, TKey> selector)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return Internal.Select(Selector).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}