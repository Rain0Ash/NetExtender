using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.Types.Monads;
using NetExtender.Types.Sorters;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Concurrent.Observable
{
    [Serializable]
    public class ConcurrentObservableSortedCollection<T> : ConcurrentObservableSortedCollection<T, ConcurrentObservableSortedCollection<T>>
    {
        public sealed override Int32 Count
        {
            get
            {
                return base.Count;
            }
        }

        public sealed override Boolean IsEmpty
        {
            get
            {
                return base.IsEmpty;
            }
        }

        public ConcurrentObservableSortedCollection()
        {
        }

        public ConcurrentObservableSortedCollection(Boolean @lock)
            : base(@lock)
        {
        }

        public ConcurrentObservableSortedCollection(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ConcurrentObservableSortedCollection(IComparer<T>? comparer, Boolean @lock)
            : base(comparer, @lock)
        {
        }

        public ConcurrentObservableSortedCollection(IEnumerable<T>? source)
            : base(source)
        {
        }

        public ConcurrentObservableSortedCollection(IEnumerable<T>? source, Boolean @lock)
            : base(source, @lock)
        {
        }

        public ConcurrentObservableSortedCollection(IEnumerable<T>? source, IComparer<T>? comparer)
            : base(source, comparer)
        {
        }

        public ConcurrentObservableSortedCollection(IEnumerable<T>? source, IComparer<T>? comparer, Boolean @lock)
            : base(source, comparer, @lock)
        {
        }

        public ConcurrentObservableSortedCollection(ImmutableList<T>? source)
            : base(source)
        {
        }

        public ConcurrentObservableSortedCollection(ImmutableList<T>? source, Boolean @lock)
            : base(source, @lock)
        {
        }

        public ConcurrentObservableSortedCollection(ImmutableList<T>? source, IComparer<T>? comparer)
            : base(source, comparer)
        {
        }

        public ConcurrentObservableSortedCollection(ImmutableList<T>? source, IComparer<T>? comparer, Boolean @lock)
            : base(source, comparer, @lock)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ConcurrentObservableSortedCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("Formatter-based serialization is obsolete and should not be used.", DiagnosticId = "SYSLIB0050", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
#endif
        public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        protected sealed override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);
        }

        public sealed override Boolean Contains(T item)
        {
            return base.Contains(item);
        }

        public sealed override Int32 IndexOf(T item)
        {
            return base.IndexOf(item);
        }

        public sealed override Boolean Add(T item)
        {
            return base.Add(item);
        }

        public sealed override void AddRange(IEnumerable<T>? items)
        {
            base.AddRange(items);
        }

        public sealed override Int32 Insert(T item)
        {
            return base.Insert(item);
        }

        public sealed override void Insert(Int32 index, T item)
        {
            base.Insert(index, item);
        }

        public sealed override void InsertRange(Int32 index, IEnumerable<T>? items)
        {
            base.InsertRange(index, items);
        }

        public sealed override Boolean Remove(T item, IEqualityComparer<T>? comparer)
        {
            return base.Remove(item);
        }

        public sealed override Boolean RemoveLast([MaybeNullWhen(false)] out T item)
        {
            return base.RemoveLast(out item);
        }

        public sealed override void RemoveAt(Int32 index)
        {
            base.RemoveAt(index);
        }

        public sealed override void RemoveRange(Int32 index, Int32 count)
        {
            base.RemoveRange(index, count);
        }

        public sealed override void RemoveRange(IEnumerable<T>? items, IEqualityComparer<T>? comparer)
        {
            base.RemoveRange(items);
        }

        public sealed override void Reset(IEnumerable<T>? items)
        {
            base.Reset(items);
        }

        protected sealed override void Clear(out Int32 count)
        {
            base.Clear(out count);
        }

        public sealed override void CopyTo(T[] array, Int32 index)
        {
            base.CopyTo(array, index);
        }

        public sealed override T this[Int32 index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }
    }

    [Serializable]
    public abstract class ConcurrentObservableSortedCollection<T, TSelf> : ConcurrentObservableCollection<T, TSelf> where TSelf : ConcurrentObservableSortedCollection<T, TSelf>
    {
        protected BinarySorter<T> Sorter { get; }

        protected ConcurrentObservableSortedCollection()
            : this(true)
        {
        }

        protected ConcurrentObservableSortedCollection(Boolean @lock)
            : this(ImmutableList<T>.Empty, @lock)
        {
        }

        protected ConcurrentObservableSortedCollection(IComparer<T>? comparer)
            : this(comparer, true)
        {
        }

        protected ConcurrentObservableSortedCollection(IComparer<T>? comparer, Boolean @lock)
            : this(ImmutableList<T>.Empty, comparer, @lock)
        {
        }

        protected ConcurrentObservableSortedCollection(IEnumerable<T>? source)
            : this(source, true)
        {
        }

        protected ConcurrentObservableSortedCollection(IEnumerable<T>? source, Boolean @lock)
            : this(source, null, @lock)
        {
        }

        protected ConcurrentObservableSortedCollection(IEnumerable<T>? source, IComparer<T>? comparer)
            : this(source, comparer, true)
        {
        }

        protected ConcurrentObservableSortedCollection(IEnumerable<T>? source, IComparer<T>? comparer, Boolean @lock)
            : base(source?.OrderBy(comparer), @lock)
        {
            Sorter = new BinarySorter<T>(comparer);
        }

        protected ConcurrentObservableSortedCollection(ImmutableList<T>? source)
            : this(source, true)
        {
        }

        protected ConcurrentObservableSortedCollection(ImmutableList<T>? source, Boolean @lock)
            : this(source, null, @lock)
        {
        }

        protected ConcurrentObservableSortedCollection(ImmutableList<T>? source, IComparer<T>? comparer)
            : this(source, comparer, true)
        {
        }

        protected ConcurrentObservableSortedCollection(ImmutableList<T>? source, IComparer<T>? comparer, Boolean @lock)
            : base(source?.Sort(comparer), @lock)
        {
            Sorter = new BinarySorter<T>(comparer);
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ConcurrentObservableSortedCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Sorter = new BinarySorter<T>();
            Collection = Collection.Sort(Sorter);
        }

        public override void AddRange(IEnumerable<T>? items)
        {
            if (items.CantHaveCount())
            {
                return;
            }

            FlowResult<ImmutableList<T>> result = Notify<IEnumerable<T>, ImmutableList<T>>(items,
                static (_, modify, items) => modify.AddRange(items).Flow(),
                static (@this, _, _, modify) => modify.Sort(@this.Sorter),
                static (_, _, old, _, @new) => @new.Count > old.Count ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Source(@new.GetRange(old.Count, @new.Count - old.Count))) : null
            );

            if (result.Exception is { } exception)
            {
                throw exception;
            }
        }

        public override Int32 Insert(T item)
        {
            FlowResult<Int32> result = Notify(item,
                static (@this, modify, item) => @this.Sorter.GetInsertIndex(modify, item, modify.Count, static (source, index) => source[index]).Flow(),
                static (_, modify, item, index) => modify.Insert(index, item),
                static (_, _, _, item, index) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index)
            );

            return result.Exception is not { } exception ? result.Unwrap(out Int32 index) ? index : -1 : throw exception;
        }

        public override void Insert(Int32 index, T item)
        {
            Add(item);
        }

        public override void InsertRange(Int32 index, IEnumerable<T>? items)
        {
            AddRange(items);
        }

        public override void Reset(IEnumerable<T>? items)
        {
            if (items is null || items.CountIfMaterialized() <= 0)
            {
                Clear();
                return;
            }

            Exception? exception = Notify(items,
                static (@this, _, items) => ImmutableList.CreateRange(items.OrderBy(static item => item, @this.Sorter)),
                static (_, _, old, _) => old.Count > 0 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(old), 0) : null,
                static (_, @new, _, _) => @new.Count > 0 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Source(@new), 0) : null
            );

            if (exception is not null)
            {
                throw exception;
            }
        }

        public override T this[Int32 index]
        {
            get
            {
                return base[index];
            }
            set
            {
                RemoveAt(index);
                Add(value);
            }
        }
    }
}