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
        
        protected PaginationCollection(TCollection source, Int32 index, Int32 size)
            : base(index, size)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }
    }
    
    public abstract class PaginationCollection<T> : PaginationCollection, IPaginationEnumerable<T>
    {
        protected PaginationCollection(Int32 index, Int32 size)
            : base(index, size)
        {
        }

        public abstract override IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    //TODO: Fix logic
    public abstract class PaginationCollection : IPaginationEnumerable
    {
        private Int32 _index;
        public virtual Int32 Index
        {
            get
            {
                return _index;
            }
            protected set
            {
                _index = value;
            }
        }
        
        public Int32 Page
        {
            get
            {
                return Index + 1;
            }
        }
        
        public virtual Int32 Total
        {
            get
            {
                return Math.Abs((Int32) Math.Ceiling(Count / (Double) Size));
            }
        }
        
        public abstract Int32 Items { get; }
        
        private Int32 _size;
        public virtual Int32 Size
        {
            get
            {
                return _size;
            }
            protected set
            {
                _size = value;
            }
        }
        
        public virtual Boolean CanResize
        {
            get
            {
                return false;
            }
        }
        
        public abstract Int32 Count { get; }

        public Boolean HasPrevious
        {
            get
            {
                return Index > 0;
            }
        }

        public Boolean HasNext
        {
            get
            {
                return Page < Total;
            }
        }

        protected PaginationCollection(Int32 index, Int32 size)
        {
            _index = index >= 0 ? index : throw new ArgumentOutOfRangeException(nameof(index), index, null);
            _size = size > 0 ? size : throw new ArgumentOutOfRangeException(nameof(size), size, null);
        }
        
        public Boolean Resize(Int32 size)
        {
            return Resize(size, false);
        }
        
        public virtual Boolean Resize(Int32 size, Boolean resize)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }
            
            if (!CanResize)
            {
                return false;
            }
            
            Size = size;
            Index = resize ? Math.Clamp(Index * Size / size, 0, Total - 1) : 0;
            return true;
        }

        public abstract IEnumerator GetEnumerator();
    }
}