// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Collections
{
    public class PaginationReadOnlyCollectionWrapper<T, TCollection> : PaginationCollection<T, TCollection>, IPaginationReadOnlyCollection<T, TCollection> where TCollection : class, IReadOnlyCollection<T>
    {
        public Int32 Count
        {
            get
            {
                return Source.Count;
            }
        }

        public PaginationReadOnlyCollectionWrapper(TCollection source, Int32 index, Int32 size, Int32 count)
            : base(source, index, size, count)
        {
        }

        public sealed override IEnumerator<T> GetEnumerator()
        {
            return Source.GetEnumerator();
        }
    }
}