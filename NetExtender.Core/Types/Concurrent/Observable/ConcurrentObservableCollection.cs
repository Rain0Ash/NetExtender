// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.Types.Concurrent.Observable.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Types.Sizes;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Concurrent.Observable
{
    [Serializable]
    public partial class ConcurrentObservableCollection<T> : ConcurrentObservableCollection<T, ConcurrentObservableCollection<T>>
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

        public ConcurrentObservableCollection()
        {
        }

        public ConcurrentObservableCollection(Boolean @lock)
            : base(@lock)
        {
        }

        public ConcurrentObservableCollection(IEnumerable<T>? source)
            : base(source)
        {
        }

        public ConcurrentObservableCollection(IEnumerable<T>? source, Boolean @lock)
            : base(source, @lock)
        {
        }

        public ConcurrentObservableCollection(ImmutableList<T>? source)
            : base(source)
        {
        }

        public ConcurrentObservableCollection(ImmutableList<T>? source, Boolean @lock)
            : base(source, @lock)
        {
        }

        protected ConcurrentObservableCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

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
    public abstract class ConcurrentObservableCollection<T, TSelf> : ConcurrentObservableBase<T, ImmutableList<T>, TSelf>, IConcurrentObservableList<T>, IReadOnlyList<T>, IList where TSelf : ConcurrentObservableCollection<T, TSelf>
    {
        private ConcurrentDictionary<Int32, ImmutableList<T>> Storage { get; } = new ConcurrentDictionary<Int32, ImmutableList<T>>();
        protected sealed override ImmutableList<T> Collection { get; set; }
        private ConcurrentObservableCollection<T>.Bridge _view;

        public sealed override IList<T> View
        {
            get
            {
                return Collection;
            }
        }

        public IList<T> TwoWayView
        {
            get
            {
                return _view;
            }
        }

        public sealed override ImmutableList<T> Immutable
        {
            get
            {
                return Collection;
            }
        }

        public override Int32 Count
        {
            get
            {
                return Immutable.Count;
            }
        }

        public override Boolean IsEmpty
        {
            get
            {
                return Immutable.IsEmpty;
            }
        }

        Int32 ICollection<T>.Count
        {
            get
            {
                ImmutableList<T> immutable = Immutable;
                Storage[Environment.CurrentManagedThreadId] = immutable;
                return immutable.Count;
            }
        }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        Boolean IList.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        Boolean IList.IsFixedSize
        {
            get
            {
                return ((IList) Immutable).IsFixedSize;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Immutable).SyncRoot;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Immutable).IsSynchronized;
            }
        }

        protected ConcurrentObservableCollection()
            : this(true)
        {
        }

        protected ConcurrentObservableCollection(Boolean @lock)
            : this(ImmutableList<T>.Empty, @lock)
        {
        }

        protected ConcurrentObservableCollection(IEnumerable<T>? source)
            : this(source, true)
        {
        }

        protected ConcurrentObservableCollection(IEnumerable<T>? source, Boolean @lock)
            : this(source is not null ? ImmutableList.CreateRange(source) : null, @lock)
        {
        }

        protected ConcurrentObservableCollection(ImmutableList<T>? source)
            : this(source, true)
        {
        }

        protected ConcurrentObservableCollection(ImmutableList<T>? source, Boolean @lock)
            : base(@lock)
        {
            Collection = source ?? ImmutableList<T>.Empty;
            _view = new ConcurrentObservableCollection<T>.Bridge(this);
            PropertyChanging += OnViewChanging;
            PropertyChanged += OnViewChanged;
        }

        protected ConcurrentObservableCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Collection = info.GetValue(nameof(Collection), typeof(T[])) is T[] array ? ImmutableList.CreateRange(array) : ImmutableList<T>.Empty;
            _view = new ConcurrentObservableCollection<T>.Bridge(this);
            PropertyChanging += OnViewChanging;
            PropertyChanged += OnViewChanged;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Collection), Immutable.ToArray());
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            _view = _view.UpdateSource(Immutable);
            base.OnCollectionChanged(args);
        }

        private void OnViewChanging(Object? sender, PropertyChangingEventArgs args)
        {
            if (args.PropertyName == nameof(View))
            {
                RaisePropertyChanging(nameof(TwoWayView));
            }
        }

        private void OnViewChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(View))
            {
                RaisePropertyChanged(nameof(TwoWayView));
            }
        }

        public void BeginEdit()
        {
            if (Lock is null)
            {
                _view.Freeze = true;
                return;
            }
            
            Lock.EnterWriteLock();
            _view.Freeze = true;
            Lock.ExitWriteLock();
        }

        public void EndEdit()
        {
            if (Lock is null)
            {
                _view.Freeze = false;
                return;
            }
            
            Lock.EnterWriteLock();
            _view.Freeze = false;
            Lock.ExitWriteLock();
        }

        Boolean IList.Contains(Object? value)
        {
            return ((IList) Immutable).Contains(value);
        }

        public override Boolean Contains(T item)
        {
            return Immutable.Contains(item);
        }

        Int32 IList.IndexOf(Object? value)
        {
            return ((IList) Immutable).IndexOf(value);
        }

        public virtual Int32 IndexOf(T item)
        {
            return Immutable.IndexOf(item);
        }

        Int32 IList.Add(Object? value)
        {
            return Insert((T) value!);
        }

        public override Boolean Add(T item)
        {
            return Insert(item) >= 0;
        }

        public virtual void AddRange(IEnumerable<T>? items)
        {
            if (items is null || items.CountIfMaterialized() <= 0)
            {
                return;
            }

            FlowResult<Int32> result = Notify(items,
                static (_, modify, _) => modify.Count.Flow(),
                static (_, modify, items, _) => modify.AddRange(items),
                static (_, @new, old, _, index) => @new.Count > old.Count ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Source(@new.GetRange(index, @new.Count - old.Count)), index) : null
            );

            if (result.Exception is { } exception)
            {
                throw exception;
            }
        }

        public virtual Int32 Insert(T item)
        {
            FlowResult<Int32> result = Notify(item,
                static (_, modify, _) => modify.Count.Flow(),
                static (_, modify, item, _) => modify.Add(item),
                static (_, _, _, item, index) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index)
            );

            return result.Exception is not { } exception ? result.Unwrap(out Int32 index) ? index : -1 : throw exception;
        }

        void IList.Insert(Int32 index, Object? value)
        {
            Insert(index, (T) value!);
        }

        public virtual void Insert(Int32 index, T item)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            Exception? exception = Notify((Index: index, Item: item),
                static (_, modify, argument) => modify.Insert(argument.Index, argument.Item),
                static (_, _, _, argument) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, argument.Item, argument.Index)
            );

            if (exception is not null)
            {
                throw exception;
            }
        }

        public virtual void InsertRange(Int32 index, IEnumerable<T>? items)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            if (items is null || items.CountIfMaterialized() <= 0)
            {
                return;
            }
            
            Exception? exception = Notify((Index: index, Items: items),
                static (_, modify, argument) => modify.InsertRange(argument.Index, argument.Items),
                static (_, @new, old, argument) => @new.Count > old.Count ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Source(@new.GetRange(argument.Index, @new.Count - old.Count)), argument.Index) : null
            );

            if (exception is not null)
            {
                throw exception;
            }
        }

        void IList.Remove(Object? value)
        {
            Remove((T) value!);
        }

        public sealed override Boolean Remove(T item)
        {
            return Remove(item, null);
        }

        public virtual Boolean Remove(T item, IEqualityComparer<T>? comparer)
        {
            FlowResult<Int32> result = Notify((Item: item, Comparer: comparer),
                static (_, modify, argument) => modify.IndexOf(argument.Item, argument.Comparer) is var index && index >= 0 ? index.Flow() : default,
                static (_, modify, _, index) => modify.RemoveAt(index),
                static (_, _, _, item, index) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index)
            );

            return result.Exception is not { } exception ? result.Unwrap(out Int32 index) && index >= 0 : throw exception;
        }

        void IList.RemoveAt(Int32 index)
        {
            RemoveAt(index);
        }

        public virtual void RemoveAt(Int32 index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            FlowResult<T> result = Notify(index,
                static (_, modify, index) => modify[index].Flow(),
                static (_, modify, index, _) => modify.RemoveAt(index),
                static (_, _, _, index, item) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index)
            );

            if (result.Exception is { } exception)
            {
                throw exception;
            }
        }

        public Boolean RemoveLast()
        {
            return RemoveLast(out _);
        }

        [SuppressMessage("ReSharper", "RedundantAssignment")]
        public virtual Boolean RemoveLast([MaybeNullWhen(false)] out T item)
        {
             FlowResult<(Int32, T)> result = Notify(-1,
                static (_, modify, index) => (index = modify.Count - 1) >= 0 ? (Index: index, Item: modify[index]).Flow() : default,
                static (_, modify, _, item) => modify.RemoveAt(item.Index),
                static (_, _, _, _, item) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item.Item, item.Index)
            );

            if (result.Exception is { } exception)
            {
                throw exception;
            }

            if (result.Unwrap(out (Int32 Index, T Item) @return))
            {
                item = @return.Item;
                return @return.Index >= 0;
            }

            item = default;
            return false;
        }

        public virtual void RemoveRange(Int32 index, Int32 count)
        {
            FlowResult<ImmutableList<T>> result = Notify((Index: index, Count: count),
                static (_, modify, argument) => modify.GetRange(argument.Index, argument.Count).Flow(),
                static (_, modify, argument, _) => modify.RemoveRange(argument.Index, argument.Count),
                static (_, _, _, argument, items) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(items), argument.Index)
            );

            if (result.Exception is { } exception)
            {
                throw exception;
            }
        }

        public void RemoveRange(IEnumerable<T>? items)
        {
            RemoveRange(items, null);
        }

        public virtual void RemoveRange(IEnumerable<T>? items, IEqualityComparer<T>? comparer)
        {
            if (items is null || items.CountIfMaterialized() <= 0)
            {
                return;
            }
            
            Exception? exception = Notify((Items: items, Comparer: comparer),
                static (_, modify, argument) => modify.RemoveRange(argument.Items, argument.Comparer),
                static (_, @new, old, argument) => @new.Count < old.Count ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(@new.RemoveRange(old, argument.Comparer))) : null
            );

            if (exception is not null)
            {
                throw exception;
            }
        }

        public virtual void Reset(IEnumerable<T>? items)
        {
            if (items is null || items.CountIfMaterialized() <= 0)
            {
                Clear();
                return;
            }
            
            Exception? exception = Notify(items,
                static (_, _, items) => ImmutableList.CreateRange(items),
                static (_, _, old, _) => old.Count > 0 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(old), 0) : null,
                static (_, @new, _, _) => @new.Count > 0 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Source(@new), 0) : null
            );

            if (exception is not null)
            {
                throw exception;
            }
        }

        protected override unsafe void Clear(out Int32 count)
        {
            Exception? exception;

            fixed (Int32* pointer = &count)
            {
                exception = Notify(new UnsafePointer<Int32>(pointer),
                    static (_, modify, pointer) =>
                    {
                        pointer.Value = modify.Count;
                        return modify.Clear();
                    },
                    static (_, _, old, pointer) => pointer.Value > 0 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(old), 0) : null
                );
            }

            if (exception is not null)
            {
                throw exception;
            }
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            ((ICollection) Immutable).CopyTo(array, index);
        }

        public override void CopyTo(T[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (!Storage.TryRemove(Environment.CurrentManagedThreadId, out ImmutableList<T>? immutable))
            {
                immutable = Immutable;
            }

            if (array.Length - index <= 0)
            {
                return;
            }

            Int32 count = Math.Min(array.Length - index, immutable.Count);

            if (count > 0)
            {
                immutable.CopyTo(0, array, index, count);
            }
        }

        public T[] ToArray()
        {
            return Immutable.ToArray();
        }

        public List<T> ToList()
        {
            return Immutable.ToList();
        }

        public ImmutableList<T>.Enumerator GetEnumerator()
        {
            return Immutable.GetEnumerator();
        }

        protected sealed override IEnumerator<T> Enumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        Object? IList.this[Int32 index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (T) value!;
            }
        }

        public virtual T this[Int32 index]
        {
            get
            {
                return index >= 0 ? Immutable[index] : throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            set
            {
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
                
                FlowResult<T> result = Notify((index, value),
                    static (TSelf _, ImmutableList<T> modify, (Int32 Index, T Value) argument) => modify[argument.Index].Flow(),
                    static (TSelf _, ImmutableList<T> modify, (Int32 Index, T Value) argument, T _) => modify.SetItem(argument.Index, argument.Value),
                    static (TSelf _, ImmutableList<T> _, ImmutableList<T> _, (Int32 Index, T Value) argument, T item) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, argument.Value, item, argument.Index)
                );

                if (result.Exception is { } exception)
                {
                    throw exception;
                }
            }
        }
    }
}