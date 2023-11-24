// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Collections
{
    public class PaginationCollectionWrapper<T, TCollection> : PaginationCollection<T, TCollection>, IPaginationCollection<T, TCollection> where TCollection : class, ICollection<T>
    {
        public Int32 Count
        {
            get
            {
                return Source.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Source.IsReadOnly;
            }
        }

        public PaginationCollectionWrapper(TCollection source, Int32 index, Int32 size, Int32 count)
            : base(source, index, size, count)
        {
        }

        public Boolean Contains(T item)
        {
            return Source.Contains(item);
        }

        public void Add(T item)
        {
            Source.Add(item);
        }

        public Boolean Remove(T item)
        {
            return Source.Remove(item);
        }

        public void Clear()
        {
            Source.Clear();
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            Source.CopyTo(array, arrayIndex);
        }

        public sealed override IEnumerator<T> GetEnumerator()
        {
            return Source.GetEnumerator();
        }
    }
}