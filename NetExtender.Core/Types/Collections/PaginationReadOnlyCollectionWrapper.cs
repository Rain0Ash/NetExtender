// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Collections
{
    public class PaginationPartialReadOnlyCollectionWrapper<T, TCollection> : PaginationReadOnlyCollectionWrapper<T, TCollection> where TCollection : class, IReadOnlyCollection<T>
    {
        public override Int32 Count { get; }
        
        public override Int32 Items
        {
            get
            {
                return base.Count;
            }
        }
        
        public PaginationPartialReadOnlyCollectionWrapper(TCollection source, Int32 size, Int32 count)
            : this(source, 0, size, count)
        {
        }
        
        public PaginationPartialReadOnlyCollectionWrapper(TCollection source, Int32 index, Int32 size, Int32 count)
            : base(source, index, size)
        {
            Count = count >= 0 ? count : throw new ArgumentOutOfRangeException(nameof(count), count, null);
        }
    }
    
    public class PaginationReadOnlyCollectionWrapper<T, TCollection> : PaginationCollection<T, TCollection>, IPaginationReadOnlyCollection<T, TCollection> where TCollection : class, IReadOnlyCollection<T>
    {
        public override Int32 Count
        {
            get
            {
                return Source.Count;
            }
        }
        
        public override Int32 Items
        {
            get
            {
                return Size;
            }
        }
        
        public PaginationReadOnlyCollectionWrapper(TCollection source, Int32 size)
            : this(source, 0, size)
        {
        }
        
        public PaginationReadOnlyCollectionWrapper(TCollection source, Int32 index, Int32 size)
            : base(source, index, size)
        {
        }

        public sealed override IEnumerator<T> GetEnumerator()
        {
            return Source.GetEnumerator();
        }
    }
}