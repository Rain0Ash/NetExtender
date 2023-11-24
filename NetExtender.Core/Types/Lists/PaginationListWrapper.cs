// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Collections;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Types.Lists
{
    public class PaginationListWrapper<T, TCollection> : PaginationCollectionWrapper<T, TCollection>, IPaginationList<T, TCollection> where TCollection : class, IList<T>
    {
        public PaginationListWrapper(TCollection source, Int32 index, Int32 size, Int32 count)
            : base(source, index, size, count)
        {
        }

        public Int32 IndexOf(T item)
        {
            return Source.IndexOf(item);
        }

        public void Insert(Int32 index, T item)
        {
            Source.Insert(index, item);
        }

        public void RemoveAt(Int32 index)
        {
            Source.RemoveAt(index);
        }

        public T this[Int32 index]
        {
            get
            {
                return Source[index];
            }
            set
            {
                Source[index] = value;
            }
        }
    }
}