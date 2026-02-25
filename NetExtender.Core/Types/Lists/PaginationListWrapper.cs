// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Collections;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Types.Lists
{
    public class PaginationPartialListWrapper<T> : PaginationPartialListWrapper<T, List<T>>
    {
        public PaginationPartialListWrapper(List<T> source, Int32 count)
            : base(source, count)
        {
        }

        public PaginationPartialListWrapper(List<T> source, Int32? size, Int32 count)
            : base(source, size, count)
        {
        }

        public PaginationPartialListWrapper(List<T> source, Int32 index, Int32 count)
            : base(source, index, count)
        {
        }

        public PaginationPartialListWrapper(List<T> source, Int32 index, Int32? size, Int32 count)
            : base(source, index, size, count)
        {
        }

        public override PaginationPartialListWrapper<T> WithTimestamp()
        {
            base.WithTimestamp();
            return this;
        }

        public override PaginationPartialListWrapper<T> WithTimestamp(DateTimeOffset timestamp)
        {
            base.WithTimestamp(timestamp);
            return this;
        }
    }

    public class PaginationPartialListWrapper<T, TCollection> : PaginationPartialCollectionWrapper<T, TCollection>, IPaginationList<T, TCollection> where TCollection : class, IList<T>
    {
        public PaginationPartialListWrapper(TCollection source, Int32 count)
            : base(source, count)
        {
        }

        public PaginationPartialListWrapper(TCollection source, Int32? size, Int32 count)
            : base(source, size, count)
        {
        }

        public PaginationPartialListWrapper(TCollection source, Int32 index, Int32 count)
            : base(source, index, count)
        {
        }

        public PaginationPartialListWrapper(TCollection source, Int32 index, Int32? size, Int32 count)
            : base(source, index, size, count)
        {
        }

        public override PaginationPartialListWrapper<T, TCollection> WithTimestamp()
        {
            base.WithTimestamp();
            return this;
        }

        public override PaginationPartialListWrapper<T, TCollection> WithTimestamp(DateTimeOffset timestamp)
        {
            base.WithTimestamp(timestamp);
            return this;
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

    public class PaginationListWrapper<T> : PaginationListWrapper<T, List<T>>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator PaginationListWrapper<T>?(List<T>? value)
        {
            return value is not null ? new PaginationListWrapper<T>(value) : null;
        }

        public PaginationListWrapper(List<T> source)
            : base(source)
        {
        }

        public PaginationListWrapper(List<T> source, Int32? size)
            : base(source, size)
        {
        }

        public PaginationListWrapper(List<T> source, Int32 index)
            : base(source, index)
        {
        }

        public PaginationListWrapper(List<T> source, Int32 index, Int32? size)
            : base(source, index, size)
        {
        }

        public override PaginationListWrapper<T> WithTimestamp()
        {
            base.WithTimestamp();
            return this;
        }

        public override PaginationListWrapper<T> WithTimestamp(DateTimeOffset timestamp)
        {
            base.WithTimestamp(timestamp);
            return this;
        }
    }

    public class PaginationListWrapper<T, TCollection> : PaginationCollectionWrapper<T, TCollection>, IPaginationList<T, TCollection> where TCollection : class, IList<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator PaginationListWrapper<T, TCollection>?(TCollection? value)
        {
            return value is not null ? new PaginationListWrapper<T, TCollection>(value) : null;
        }

        public PaginationListWrapper(TCollection source)
            : base(source)
        {
        }

        public PaginationListWrapper(TCollection source, Int32? size)
            : base(source, size)
        {
        }

        public PaginationListWrapper(TCollection source, Int32 index)
            : base(source, index)
        {
        }

        public PaginationListWrapper(TCollection source, Int32 index, Int32? size)
            : base(source, index, size)
        {
        }

        public override PaginationListWrapper<T, TCollection> WithTimestamp()
        {
            base.WithTimestamp();
            return this;
        }

        public override PaginationListWrapper<T, TCollection> WithTimestamp(DateTimeOffset timestamp)
        {
            base.WithTimestamp(timestamp);
            return this;
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
            set
            {
                Source[index] = value;
            }
        }
    }

    public class PaginationList<T> : List<T>, IPaginationList<T>
    {
        public Int32 Index
        {
            get
            {
                return 0;
            }
        }

        public Int32 Page
        {
            get
            {
                return 1;
            }
        }

        public Int32 Pages
        {
            get
            {
                return 1;
            }
        }

        public Int32 Items
        {
            get
            {
                return Count;
            }
        }

        public Int32 Total
        {
            get
            {
                return Count;
            }
        }

        public Boolean HasSize
        {
            get
            {
                return false;
            }
        }

        public Int32 Size
        {
            get
            {
                return Count;
            }
        }

        public Boolean CanResize
        {
            get
            {
                return false;
            }
        }

        public Boolean HasPrevious
        {
            get
            {
                return false;
            }
        }

        public Boolean HasNext
        {
            get
            {
                return false;
            }
        }

        private DateTimeOffset? _timestamp;
        public DateTimeOffset? Timestamp
        {
            get
            {
                return _timestamp;
            }
            init
            {
                _timestamp = value;
            }
        }

        public PaginationList()
        {
        }

        public PaginationList(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public PaginationList(Int32 capacity)
            : base(capacity)
        {
        }

        public Boolean SetTimestamp()
        {
            return SetTimestamp(DateTimeOffset.UtcNow);
        }

        public Boolean SetTimestamp(DateTimeOffset? timestamp)
        {
            _timestamp = timestamp;
            return true;
        }

        public PaginationList<T> WithTimestamp()
        {
            SetTimestamp();
            return this;
        }

        public PaginationList<T> WithTimestamp(DateTimeOffset timestamp)
        {
            SetTimestamp(timestamp);
            return this;
        }

        public Boolean Resize(Int32 size)
        {
            return false;
        }

        public Boolean Resize(Int32 size, Boolean resize)
        {
            return false;
        }
    }
}