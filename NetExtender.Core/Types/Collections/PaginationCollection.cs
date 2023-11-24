// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Types.Enumerables.Interfaces;

namespace NetExtender.Types.Collections
{
    public abstract class PaginationCollection<T, TCollection> : PaginationCollection<T>, IPaginationEnumerable<T, TCollection> where TCollection : class, IEnumerable<T>
    {
        public TCollection Source { get; }
        
        protected PaginationCollection(TCollection source, Int32 index, Int32 size, Int32 count)
            : base(index, size, count)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }
    }
    
    public abstract class PaginationCollection<T> : PaginationCollection, IPaginationEnumerable<T>
    {
        protected PaginationCollection(Int32 index, Int32 size, Int32 count)
            : base(index, size, count)
        {
        }

        public abstract override IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public abstract class PaginationCollection : IPaginationEnumerable
    {
        public Int32 Index { get; }
        public Int32 Total { get; }
        public Int32 Items { get; }
        public Int32 Size { get; }

        public Boolean HasPrevious
        {
            get
            {
                return Index > 1;
            }
        }

        public Boolean HasNext
        {
            get
            {
                return Index < Total;
            }
        }

        protected PaginationCollection(Int32 index, Int32 size, Int32 count)
        {
            Index = index >= 1 ? index : throw new ArgumentOutOfRangeException(nameof(index), index, null);
            Size = size > 0 ? size : throw new ArgumentOutOfRangeException(nameof(size), size, null);
            Items = count >= 0 ? count : throw new ArgumentOutOfRangeException(nameof(count), count, null);
            Total = Math.Abs((Int32) Math.Ceiling(Items / (Double) Size));
        }

        public abstract IEnumerator GetEnumerator();
    }
}