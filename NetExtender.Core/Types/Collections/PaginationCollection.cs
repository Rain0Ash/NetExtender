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

        protected PaginationCollection(TCollection source, Int32 index, Int32? size)
            : base(index, size)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public override IEnumerator<T> GetEnumerator()
        {
            if (!HasSize)
            {
                return Source.GetEnumerator();
            }

            static IEnumerator<T> Core(PaginationCollection<T, TCollection> collection)
            {
                (Int32 index, Int32 count, Int32 size) = (collection.Index, collection.Count, collection.Size);

                Int32 start = index * size;
                Int32 end = Math.Min(start + size, count);
                Int32 current = 0;

                foreach (T item in collection)
                {
                    if (current >= start && current < end)
                    {
                        yield return item;
                    }
                    else if (current >= end)
                    {
                        break;
                    }

                    current++;
                }
            }

            return Core(this);
        }
    }

    public abstract class PaginationCollection<T> : PaginationCollection, IPaginationEnumerable<T>
    {
        protected PaginationCollection(Int32 index, Int32? size)
            : base(index, size)
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
        private Int32 _index;
        public virtual Int32 Index
        {
            get
            {
                return HasSize ? _index : 0;
            }
            protected set
            {
                _index = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public Int32 Page
        {
            get
            {
                return Index + 1;
            }
        }

        public virtual Int32 Pages
        {
            get
            {
                if (!HasSize)
                {
                    return 1;
                }

                Int32 total = Total;

                if (total <= 0)
                {
                    return 0;
                }

                return (Int32) Math.Ceiling(total / (Double) Size);
            }
        }

        public Boolean HasSize
        {
            get
            {
                return _size <= 0;
            }
        }

        private Int32 _size;
        public virtual Int32 Size
        {
            get
            {
                (Int32 size, Int32 count) = (_size, Count);
                return size > 0 && size < count ? size : count;
            }
            protected set
            {
                _size = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), value, null);
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

        public virtual Int32 Items
        {
            get
            {
                if (!HasSize)
                {
                    return Count;
                }

                (Int32 index, Int32 count, Int32 size) = (Index, Count, Size);
                Int32 start = index * size;

                return start >= count ? 0 : Math.Min(size, count - start);
            }
        }

        public virtual Int32 Total
        {
            get
            {
                return Count;
            }
        }

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
                return Page < Pages;
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

        protected PaginationCollection(Int32 index, Int32? size)
        {
            _index = index >= 0 ? index : throw new ArgumentOutOfRangeException(nameof(index), index, null);

            _size = size switch
            {
                null => 0,
                > 0 => size.Value,
                _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
            };
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

        public abstract PaginationCollection WithTimestamp();
        public abstract PaginationCollection WithTimestamp(DateTimeOffset timestamp);

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
            Index = resize && HasSize ? Math.Clamp(Index * Size / size, 0, Pages > 0 ? Pages - 1 : 0) : 0;
            return true;
        }

        public abstract IEnumerator GetEnumerator();
    }
}