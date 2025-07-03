using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Collections
{
    public sealed class NoneCollection : NoneCollection<Object?>
    {
        public new static ICollection Empty
        {
            get
            {
                return NoneCollection<Object?>.Empty;
            }
        }
    }

    public class NoneCollection<T> : NoneCollection<T, ImmutableList<T>>
    {
        public static NoneCollection<T> Empty { get; } = new NoneCollection<T>();

        public NoneCollection()
            : base(ImmutableList<T>.Empty)
        {
        }

        public sealed override ImmutableList<T>.Enumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
    }

    public abstract class NoneCollection<T, TCollection> : ICollection, IReadOnlyCollection<T>, ICollection<T> where TCollection : class, ICollection, IReadOnlyCollection<T>
    {
        protected TCollection Internal { get; }

        protected ICollection Collection
        {
            get
            {
                return Internal;
            }
        }

        public Int32 Count
        {
            get
            {
                return Collection.Count;
            }
        }

        public Boolean IsSynchronized
        {
            get
            {
                return true;
            }
        }

        public Object SyncRoot
        {
            get
            {
                return Internal;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return SyncRoot;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return true;
            }
        }

        protected NoneCollection(TCollection collection)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public Boolean Contains(T item)
        {
            return false;
        }

        void ICollection<T>.Add(T item)
        {
            throw new ReadOnlyException();
        }

        Boolean ICollection<T>.Remove(T item)
        {
            return false;
        }

        void ICollection<T>.Clear()
        {
        }

        public void CopyTo(Array array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public void CopyTo(T[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public abstract ImmutableList<T>.Enumerator GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}