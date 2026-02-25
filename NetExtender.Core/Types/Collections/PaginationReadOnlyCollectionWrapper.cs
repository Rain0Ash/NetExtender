// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Collections
{
    public class PaginationPartialReadOnlyCollectionWrapper<T, TCollection> : PaginationReadOnlyCollectionWrapper<T, TCollection> where TCollection : class, IReadOnlyCollection<T>
    {
        public override Int32 Items
        {
            get
            {
                return base.Count;
            }
        }

        public override Int32 Total { get; }

        public PaginationPartialReadOnlyCollectionWrapper(TCollection source, Int32 count)
            : this(source, null, count)
        {
        }

        public PaginationPartialReadOnlyCollectionWrapper(TCollection source, Int32? size, Int32 count)
            : this(source, 0, size, count)
        {
        }

        public PaginationPartialReadOnlyCollectionWrapper(TCollection source, Int32 index, Int32 count)
            : this(source, index, null, count)
        {
        }

        public PaginationPartialReadOnlyCollectionWrapper(TCollection source, Int32 index, Int32? size, Int32 count)
            : base(source, index, size)
        {
            Total = count >= 0 ? count : throw new ArgumentOutOfRangeException(nameof(count), count, null);
        }

        public override PaginationPartialReadOnlyCollectionWrapper<T, TCollection> WithTimestamp()
        {
            base.WithTimestamp();
            return this;
        }

        public override PaginationPartialReadOnlyCollectionWrapper<T, TCollection> WithTimestamp(DateTimeOffset timestamp)
        {
            base.WithTimestamp(timestamp);
            return this;
        }
    }

    public class PaginationReadOnlyCollectionWrapper<T, TCollection> : PaginationCollection<T, TCollection>, IPaginationReadOnlyCollection<T, TCollection> where TCollection : class, IReadOnlyCollection<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator PaginationReadOnlyCollectionWrapper<T, TCollection>?(TCollection? value)
        {
            return value is not null ? new PaginationReadOnlyCollectionWrapper<T, TCollection>(value, null) : null;
        }

        public override Int32 Count
        {
            get
            {
                return Source.Count;
            }
        }

        public PaginationReadOnlyCollectionWrapper(TCollection source)
            : this(source, null)
        {
        }

        public PaginationReadOnlyCollectionWrapper(TCollection source, Int32? size)
            : this(source, 0, size)
        {
        }

        public PaginationReadOnlyCollectionWrapper(TCollection source, Int32 index)
            : this(source, index, null)
        {
        }

        public PaginationReadOnlyCollectionWrapper(TCollection source, Int32 index, Int32? size)
            : base(source, index, size)
        {
        }

        public override PaginationReadOnlyCollectionWrapper<T, TCollection> WithTimestamp()
        {
            SetTimestamp();
            return this;
        }

        public override PaginationReadOnlyCollectionWrapper<T, TCollection> WithTimestamp(DateTimeOffset timestamp)
        {
            SetTimestamp(timestamp);
            return this;
        }
    }
}