// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Types.Collections
{
    public class PaginationReadOnlyListWrapper<T, TCollection> : PaginationReadOnlyCollectionWrapper<T, TCollection>, IPaginationReadOnlyList<T, TCollection> where TCollection : class, IReadOnlyList<T>
    {
        public PaginationReadOnlyListWrapper(TCollection source, Int32 index, Int32 size, Int32 count)
            : base(source, index, size, count)
        {
        }

        public T this[Int32 index]
        {
            get
            {
                return Source[index];
            }
        }
    }
}