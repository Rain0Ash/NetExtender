// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Collections
{
    public class PaginationPartialCollectionWrapper<T, TCollection> : PaginationCollectionWrapper<T, TCollection> where TCollection : class, ICollection<T>
    {
        public override Int32 Items
        {
            get
            {
                return base.Count;
            }
        }

        public override Int32 Total { get; }

        public PaginationPartialCollectionWrapper(TCollection source, Int32 count)
            : this(source, null, count)
        {
        }

        public PaginationPartialCollectionWrapper(TCollection source, Int32? size, Int32 count)
            : this(source, 0, size, count)
        {
        }

        public PaginationPartialCollectionWrapper(TCollection source, Int32 index, Int32 count)
            : this(source, index, null, count)
        {
        }

        public PaginationPartialCollectionWrapper(TCollection source, Int32 index, Int32? size, Int32 count)
            : base(source, index, size)
        {
            Total = count >= 0 ? count : throw new ArgumentOutOfRangeException(nameof(count), count, null);
        }

        public override PaginationPartialCollectionWrapper<T, TCollection> WithTimestamp()
        {
            base.WithTimestamp();
            return this;
        }

        public override PaginationPartialCollectionWrapper<T, TCollection> WithTimestamp(DateTimeOffset timestamp)
        {
            base.WithTimestamp(timestamp);
            return this;
        }
    }

    public class PaginationCollectionWrapper<T, TCollection> : PaginationCollection<T, TCollection>, IPaginationCollection<T, TCollection> where TCollection : class, ICollection<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator PaginationCollectionWrapper<T, TCollection>?(TCollection? value)
        {
            return value is not null ? new PaginationCollectionWrapper<T, TCollection>(value, null) : null;
        }

        public override Int32 Count
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

        public PaginationCollectionWrapper(TCollection source)
            : this(source, null)
        {
        }

        public PaginationCollectionWrapper(TCollection source, Int32? size)
            : this(source, 0, size)
        {
        }

        public PaginationCollectionWrapper(TCollection source, Int32 index)
            : this(source, index, null)
        {
        }

        public PaginationCollectionWrapper(TCollection source, Int32 index, Int32? size)
            : base(source, index, size)
        {
        }

        public override PaginationCollectionWrapper<T, TCollection> WithTimestamp()
        {
            SetTimestamp();
            return this;
        }

        public override PaginationCollectionWrapper<T, TCollection> WithTimestamp(DateTimeOffset timestamp)
        {
            SetTimestamp(timestamp);
            return this;
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

        public void CopyTo(T[] array, Int32 index)
        {
            Source.CopyTo(array, index);
        }
    }
}