using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Types.Concurrent.Observable.Interfaces;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Types.Sizes;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Concurrent.Observable
{
    [Serializable]
    public class ConcurrentObservableHashSet<T> : ConcurrentObservableHashSet<T, ConcurrentObservableHashSet<T>>
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

        public ConcurrentObservableHashSet()
        {
        }

        public ConcurrentObservableHashSet(Boolean @lock)
            : base(@lock)
        {
        }

        public ConcurrentObservableHashSet(IEqualityComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ConcurrentObservableHashSet(IEqualityComparer<T>? comparer, Boolean @lock)
            : base(comparer, @lock)
        {
        }

        public ConcurrentObservableHashSet(IEnumerable<T>? source)
            : base(source)
        {
        }

        public ConcurrentObservableHashSet(IEnumerable<T>? source, Boolean @lock)
            : base(source, @lock)
        {
        }

        public ConcurrentObservableHashSet(IEnumerable<T>? source, IEqualityComparer<T>? comparer)
            : base(source, comparer)
        {
        }

        public ConcurrentObservableHashSet(IEnumerable<T>? source, IEqualityComparer<T>? comparer, Boolean @lock)
            : base(source, comparer, @lock)
        {
        }

        public ConcurrentObservableHashSet(ImmutableHashSet<T>? source)
            : base(source)
        {
        }

        public ConcurrentObservableHashSet(ImmutableHashSet<T>? source, Boolean @lock)
            : base(source, @lock)
        {
        }

        public ConcurrentObservableHashSet(SerializationInfo info, StreamingContext context)
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

        public sealed override Boolean IsSubsetOf(IEnumerable<T>? other)
        {
            return base.IsSubsetOf(other);
        }

        public sealed override Boolean IsProperSubsetOf(IEnumerable<T>? other)
        {
            return base.IsProperSubsetOf(other);
        }

        public sealed override Boolean IsSupersetOf(IEnumerable<T>? other)
        {
            return base.IsSupersetOf(other);
        }

        public sealed override Boolean IsProperSupersetOf(IEnumerable<T>? other)
        {
            return base.IsProperSupersetOf(other);
        }

        public sealed override Boolean Overlaps(IEnumerable<T>? other)
        {
            return base.Overlaps(other);
        }

        public sealed override Boolean SetEquals(IEnumerable<T>? other)
        {
            return base.SetEquals(other);
        }

        protected sealed override Int32? Union(IEnumerable<T>? other)
        {
            return base.Union(other);
        }

        protected sealed override Int32? Intersect(IEnumerable<T>? other)
        {
            return base.Intersect(other);
        }

        protected sealed override Int32? Except(IEnumerable<T>? other)
        {
            return base.Except(other);
        }

        protected sealed override Int32? SymmetricExcept(IEnumerable<T>? other)
        {
            return base.SymmetricExcept(other);
        }

        public sealed override Boolean Add(T value)
        {
            return base.Add(value);
        }

        public sealed override void CopyTo(T[] array, Int32 index)
        {
            base.CopyTo(array, index);
        }

        public sealed override Boolean Remove(T value)
        {
            return base.Remove(value);
        }

        protected sealed override void Clear(out Int32 count)
        {
            base.Clear(out count);
        }
    }

    [Serializable]
    public abstract class ConcurrentObservableHashSet<T, TSelf> : ConcurrentObservableBase<T, ImmutableHashSet<T>, TSelf>, IConcurrentObservableHashSet<T>, IReadOnlyHashSet<T>, ISet where TSelf : ConcurrentObservableHashSet<T, TSelf>
    {
        protected sealed override ImmutableHashSet<T> Collection { get; set; }

        public sealed override ISet<T> View
        {
            get
            {
                return Collection;
            }
        }

        public IEqualityComparer<T> Comparer
        {
            get
            {
                return Collection.KeyComparer;
            }
        }

        public sealed override ImmutableHashSet<T> Immutable
        {
            get
            {
                return Collection;
            }
        }

        IImmutableSet<T> IConcurrentObservableSet<T>.Immutable
        {
            get
            {
                return Immutable;
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

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Collection).SyncRoot;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Collection).IsSynchronized;
            }
        }

        protected ConcurrentObservableHashSet()
            : this(true)
        {
        }

        protected ConcurrentObservableHashSet(Boolean @lock)
            : this(ImmutableHashSet<T>.Empty, @lock)
        {
        }

        protected ConcurrentObservableHashSet(IEqualityComparer<T>? comparer)
            : this(comparer, true)
        {
        }

        protected ConcurrentObservableHashSet(IEqualityComparer<T>? comparer, Boolean @lock)
            : this(ImmutableHashSet<T>.Empty.WithComparer(comparer), @lock)
        {
        }

        protected ConcurrentObservableHashSet(IEnumerable<T>? source)
            : this(source, true)
        {
        }

        protected ConcurrentObservableHashSet(IEnumerable<T>? source, Boolean @lock)
            : this(source, null, @lock)
        {
        }

        protected ConcurrentObservableHashSet(IEnumerable<T>? source, IEqualityComparer<T>? comparer)
            : this(source, comparer, true)
        {
        }

        protected ConcurrentObservableHashSet(IEnumerable<T>? source, IEqualityComparer<T>? comparer, Boolean @lock)
            : this(source is not null ? ImmutableHashSet.CreateRange(comparer, source) : ImmutableHashSet<T>.Empty.WithComparer(comparer), @lock)
        {
        }

        protected ConcurrentObservableHashSet(ImmutableHashSet<T>? source)
            : this(source, true)
        {
        }

        protected ConcurrentObservableHashSet(ImmutableHashSet<T>? source, Boolean @lock)
            : base(@lock)
        {
            Collection = source ?? ImmutableHashSet<T>.Empty;
        }

        protected ConcurrentObservableHashSet(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Collection = info.GetValue(nameof(Collection), typeof(T[])) is T[] array ? ImmutableHashSet.CreateRange(array) : ImmutableHashSet<T>.Empty;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Collection), Immutable.ToArray());
        }
        
        public override Boolean Contains(T item)
        {
            return Immutable.Contains(item);
        }

        public virtual Boolean IsSubsetOf(IEnumerable<T>? other)
        {
            return Immutable.IsSubsetOf(other ?? ImmutableHashSet<T>.Empty);
        }

        public virtual Boolean IsProperSubsetOf(IEnumerable<T>? other)
        {
            return Immutable.IsProperSubsetOf(other ?? ImmutableHashSet<T>.Empty);
        }

        public virtual Boolean IsSupersetOf(IEnumerable<T>? other)
        {
            return Immutable.IsSupersetOf(other ?? ImmutableHashSet<T>.Empty);
        }

        public virtual Boolean IsProperSupersetOf(IEnumerable<T>? other)
        {
            return Immutable.IsProperSupersetOf(other ?? ImmutableHashSet<T>.Empty);
        }

        public virtual Boolean Overlaps(IEnumerable<T>? other)
        {
            return Immutable.Overlaps(other ?? ImmutableHashSet<T>.Empty);
        }

        public virtual Boolean SetEquals(IEnumerable<T>? other)
        {
            return Immutable.SetEquals(other ?? ImmutableHashSet<T>.Empty);
        }

        protected virtual unsafe Int32? Union(IEnumerable<T>? other)
        {
            if (other is null)
            {
                return null;
            }

            if (other.CountIfMaterialized() <= 0)
            {
                return 0;
            }

            Int32 count = 0;
            
            Exception? exception = Notify((Items: other, Count: new UnsafePointer<Int32>(&count)),
                static (_, modify, argument) =>
                {
                    ImmutableHashSet<T> @new = modify.Union(argument.Items);
                    argument.Count.Value = @new.Count - modify.Count;
                    return @new;
                },
                static (_, @new, old, argument) => argument.Count > 0 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Source(@new.Except(old))) : null
            );

            return exception is null ? count : throw exception;
        }

        protected virtual unsafe Int32? Intersect(IEnumerable<T>? other)
        {
            Int32 count = 0;
            if (other is null || other.CountIfMaterialized() <= 0)
            {
                Clear(out count);
                return count;
            }
    
            Exception? exception = Notify((Items: other, Count: new UnsafePointer<Int32>(&count)),
                static (_, modify, argument) =>
                {
                    ImmutableHashSet<T> @new = modify.Intersect(argument.Items);
                    argument.Count.Value = modify.Count - @new.Count;
                    return @new;
                },
                static (_, @new, old, argument) => argument.Count > 0 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(old.Except(@new))) : null
            );

            return exception is null ? count : throw exception;
        }

        protected virtual unsafe Int32? Except(IEnumerable<T>? other)
        {
            if (other is null)
            {
                return null;
            }

            if (other.CountIfMaterialized() <= 0)
            {
                return 0;
            }

            Int32 count = 0;
    
            Exception? exception = Notify((Items: other, Count: new UnsafePointer<Int32>(&count)),
                static (_, modify, argument) =>
                {
                    ImmutableHashSet<T> @new = modify.Except(argument.Items);
                    argument.Count.Value = modify.Count - @new.Count;
                    return @new;
                },
                static (_, @new, old, argument) => argument.Count > 0 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(old.Except(@new))) : null
            );

            return exception is null ? count : throw exception;
        }

        protected virtual unsafe Int32? SymmetricExcept(IEnumerable<T>? other)
        {
            if (other is null)
            {
                return null;
            }

            if (other.CountIfMaterialized() <= 0)
            {
                return 0;
            }
    
            Int32 count = 0;
    
            Exception? exception = Notify((Items: other, Count: new UnsafePointer<Int32>(&count)),
                static (_, modify, argument) =>
                {
                    ImmutableHashSet<T> @new = modify.SymmetricExcept(argument.Items);
                    argument.Count.Value = Math.Abs(modify.Count - @new.Count);
                    return @new;
                },
                static (_, @new, old, _) => old.Except(@new) is { Count: > 0 } set ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(set)) : null,
                static (_, @new, old, _) => @new.Except(old) is { Count: > 0 } set ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Source(set)) : null
            );
            
            return exception is null ? count : throw exception;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public override unsafe Boolean Add(T value)
        {
            Boolean result = false;

            Exception? exception = Notify((Value: value, Result: new UnsafePointer<Boolean>(&result)),
                static (_, modify, argument) =>
                {
                    ImmutableHashSet<T> @new = modify.Add(argument.Value);
                    argument.Result.Value = @new != modify;
                    return @new;
                },
                static (_, _, _, argument) => argument.Result ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, argument.Value) : null
            );
            
            return exception is null ? result : throw exception;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 AddRange(IEnumerable<T>? other)
        {
            return Union(other) ?? 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnionWith(IEnumerable<T>? other)
        {
            Union(other);
        }

        public void IntersectWith(IEnumerable<T>? other)
        {
            Intersect(other);
        }

        public void ExceptWith(IEnumerable<T>? other)
        {
            Except(other);
        }

        public void SymmetricExceptWith(IEnumerable<T>? other)
        {
            SymmetricExcept(other);
        }

        public override unsafe Boolean Remove(T value)
        {
            Boolean result = false;
            
            Exception? exception = Notify((Value: value, Result: new UnsafePointer<Boolean>(&result)),
                static (_, modify, argument) =>
                {
                    ImmutableHashSet<T> @new = modify.Remove(argument.Value);
                    argument.Result.Value = @new != modify;
                    return @new;
                },
                static (_, _, _, argument) => argument.Result ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, argument.Value) : null
            );
            
            return exception is null ? result : throw exception;
        }

        public Int32 RemoveRange(IEnumerable<T>? other)
        {
            return Except(other) ?? 0;
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
            ((ICollection<T>) Immutable).CopyTo(array, index);
        }

        public ImmutableHashSet<T>.Enumerator GetEnumerator()
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
    }
}