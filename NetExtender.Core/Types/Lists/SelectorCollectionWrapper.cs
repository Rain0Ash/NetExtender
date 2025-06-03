// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Lists
{
    public sealed class SelectorListWrapper<T, TKey> : IList<TKey>
    {
        private IList<T> Internal { get; }
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

        public SelectorListWrapper(IList<T> collection, Func<T, TKey> selector)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public Boolean Contains(TKey item)
        {
            return Internal.Select(Selector).Contains(item);
        }

        public Int32 IndexOf(TKey item)
        {
            return Internal.Select(Selector).IndexOf(item);
        }

        public void Add(TKey item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            Internal.Add((T) (Object) item!);
        }

        public void Insert(Int32 index, TKey item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            Internal.Insert(index, (T) (Object) item!);
        }

        public Boolean Remove(TKey item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            return Internal.Remove((T) (Object) item!);
        }

        public void RemoveAt(Int32 index)
        {
            if (Internal.IsReadOnly)
            {
                throw new NotSupportedException();
            }
            
            Internal.RemoveAt(index);
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

        public TKey this[Int32 index]
        {
            get
            {
                return Selector(Internal[index]);
            }
            set
            {
                if (IsReadOnly)
                {
                    throw new NotSupportedException();
                }
                
                Internal[index] = (T) (Object) value!;
            }
        }
    }

    public sealed class ReadOnlySelectorListWrapper<T, TKey> : IReadOnlyList<TKey>
    {
        private IReadOnlyList<T> Internal { get; }
        private Func<T, TKey> Selector { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public ReadOnlySelectorListWrapper(IReadOnlyList<T> list, Func<T, TKey> selector)
        {
            Internal = list ?? throw new ArgumentNullException(nameof(list));
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

        public TKey this[Int32 index]
        {
            get
            {
                return Selector(Internal[index]);
            }
        }
    }
}