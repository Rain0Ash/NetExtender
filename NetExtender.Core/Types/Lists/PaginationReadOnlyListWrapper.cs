// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Types.Collections
{
    public class PaginationPartialReadOnlyListWrapper<T> : PaginationPartialReadOnlyListWrapper<T, List<T>>
    {
        public PaginationPartialReadOnlyListWrapper(List<T> source, Int32 count)
            : base(source, count)
        {
        }

        public PaginationPartialReadOnlyListWrapper(List<T> source, Int32? size, Int32 count)
            : base(source, size, count)
        {
        }

        public PaginationPartialReadOnlyListWrapper(List<T> source, Int32 index, Int32 count)
            : base(source, index, count)
        {
        }

        public PaginationPartialReadOnlyListWrapper(List<T> source, Int32 index, Int32? size, Int32 count)
            : base(source, index, size, count)
        {
        }

        public override PaginationPartialReadOnlyListWrapper<T> WithTimestamp()
        {
            base.WithTimestamp();
            return this;
        }

        public override PaginationPartialReadOnlyListWrapper<T> WithTimestamp(DateTimeOffset timestamp)
        {
            base.WithTimestamp(timestamp);
            return this;
        }
    }

    public class PaginationPartialReadOnlyListWrapper<T, TCollection> : PaginationPartialReadOnlyCollectionWrapper<T, TCollection>, IPaginationReadOnlyList<T, TCollection> where TCollection : class, IReadOnlyList<T>
    {
        public PaginationPartialReadOnlyListWrapper(TCollection source, Int32 count)
            : base(source, count)
        {
        }

        public PaginationPartialReadOnlyListWrapper(TCollection source, Int32? size, Int32 count)
            : base(source, size, count)
        {
        }

        public PaginationPartialReadOnlyListWrapper(TCollection source, Int32 index, Int32 count)
            : base(source, index, count)
        {
        }

        public PaginationPartialReadOnlyListWrapper(TCollection source, Int32 index, Int32? size, Int32 count)
            : base(source, index, size, count)
        {
        }

        public override PaginationPartialReadOnlyListWrapper<T, TCollection> WithTimestamp()
        {
            base.WithTimestamp();
            return this;
        }

        public override PaginationPartialReadOnlyListWrapper<T, TCollection> WithTimestamp(DateTimeOffset timestamp)
        {
            base.WithTimestamp(timestamp);
            return this;
        }

        public T this[Int32 index]
        {
            get
            {
                return Source[index];
            }
        }
    }

    public class PaginationReadOnlyListWrapper<T> : PaginationReadOnlyListWrapper<T, List<T>>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator PaginationReadOnlyListWrapper<T>?(List<T>? value)
        {
            return value is not null ? new PaginationReadOnlyListWrapper<T>(value) : null;
        }

        public PaginationReadOnlyListWrapper(List<T> source)
            : base(source)
        {
        }

        public PaginationReadOnlyListWrapper(List<T> source, Int32? size)
            : base(source, size)
        {
        }

        public PaginationReadOnlyListWrapper(List<T> source, Int32 index)
            : base(source, index)
        {
        }

        public PaginationReadOnlyListWrapper(List<T> source, Int32 index, Int32? size)
            : base(source, index, size)
        {
        }

        public override PaginationReadOnlyListWrapper<T> WithTimestamp()
        {
            base.WithTimestamp();
            return this;
        }

        public override PaginationReadOnlyListWrapper<T> WithTimestamp(DateTimeOffset timestamp)
        {
            base.WithTimestamp(timestamp);
            return this;
        }
    }

    public class PaginationReadOnlyListWrapper<T, TCollection> : PaginationReadOnlyCollectionWrapper<T, TCollection>, IPaginationReadOnlyList<T, TCollection> where TCollection : class, IReadOnlyList<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator PaginationReadOnlyListWrapper<T, TCollection>?(TCollection? value)
        {
            return value is not null ? new PaginationReadOnlyListWrapper<T, TCollection>(value) : null;
        }

        public PaginationReadOnlyListWrapper(TCollection source)
            : base(source)
        {
        }

        public PaginationReadOnlyListWrapper(TCollection source, Int32? size)
            : base(source, size)
        {
        }

        public PaginationReadOnlyListWrapper(TCollection source, Int32 index)
            : base(source, index)
        {
        }

        public PaginationReadOnlyListWrapper(TCollection source, Int32 index, Int32? size)
            : base(source, index, size)
        {
        }

        public override PaginationReadOnlyListWrapper<T, TCollection> WithTimestamp()
        {
            base.WithTimestamp();
            return this;
        }

        public override PaginationReadOnlyListWrapper<T, TCollection> WithTimestamp(DateTimeOffset timestamp)
        {
            base.WithTimestamp(timestamp);
            return this;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            if (!HasSize)
            {
                for (Int32 i = 0; i < Count; i++)
                {
                    yield return Source[i];
                }

                yield break;
            }

            Int32 start = Index * Size;
            Int32 end = Math.Min(start + Size, Count);

            for (Int32 i = start; i < end; i++)
            {
                yield return Source[i];
            }
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