// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Collections;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Lists
{
    public abstract class NullableSelectorListWrapper<T> : NullableSelectorCollectionWrapper<T>, IList<T>, IList<NullMaybe<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static NullableSelectorListWrapper<T> Create(IList<T> source)
        {
            return new ToNullable(source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static NullableSelectorListWrapper<T> Create(IList<T> source, Boolean @null)
        {
            return @null ? new ToNullNullable(source) : new ToNullable(source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static NullableSelectorListWrapper<T> Create(IList<NullMaybe<T>> source)
        {
            return new FromNullable(source);
        }
        
        public abstract override IList<T> Source { get; }
        public abstract override IList<NullMaybe<T>> Nullable { get; }
        
        public abstract Int32 IndexOf(T item);
        public abstract Int32 IndexOf(NullMaybe<T> item);
        public abstract void Insert(Int32 index, T item);
        public abstract void Insert(Int32 index, NullMaybe<T> item);
        public abstract void RemoveAt(Int32 index);

        protected abstract T Get(Int32 index);
        protected abstract void Set(Int32 index, T item);

        T IList<T>.this[Int32 index]
        {
            get
            {
                return Get(index);
            }
            set
            {
                Set(index, value);
            }
        }

        public abstract NullMaybe<T> this[Int32 index] { get; set; }

        private sealed class ToNullable : NullableSelectorListWrapper<T>
        {
            public override IList<T> Source { get; }

            public override IList<NullMaybe<T>> Nullable
            {
                get
                {
                    return this;
                }
            }

            public override Int32 Count
            {
                get
                {
                    return Source.Count;
                }
            }

            public override Boolean IsReadOnly
            {
                get
                {
                    return Source.IsReadOnly;
                }
            }

            public override Object SyncRoot
            {
                get
                {
                    return Source is ICollection source ? source.SyncRoot : Source;
                }
            }

            public override Boolean IsSynchronized
            {
                get
                {
                    return Source is ICollection { IsSynchronized: true };
                }
            }

            public ToNullable(IList<T> source)
            {
                Source = source ?? throw new ArgumentNullException(nameof(source));
            }

            public override Boolean Contains(T item)
            {
                return item is not null && Source.Contains(item);
            }

            public override Boolean Contains(NullMaybe<T> item)
            {
                return item.Value is { } value && Source.Contains(value);
            }

            public override Int32 IndexOf(T item)
            {
                return item is not null ? Source.IndexOf(item) : -1;
            }

            public override Int32 IndexOf(NullMaybe<T> item)
            {
                return item.Value is { } value ? Source.IndexOf(value) : -1;
            }

            protected override T Get(Int32 index)
            {
                return Source[index];
            }

            protected override void Set(Int32 index, T item)
            {
                if (item is not null)
                {
                    Source[index] = item;
                }
            }

            public override void Add(T item)
            {
                if (item is not null)
                {
                    Source.Add(item);
                }
            }

            public override void Add(NullMaybe<T> item)
            {
                if (item.Value is { } value)
                {
                    Source.Add(value);
                }
            }

            public override void Insert(Int32 index, T item)
            {
                if (item is not null)
                {
                    Source.Insert(index, item);
                }
            }

            public override void Insert(Int32 index, NullMaybe<T> item)
            {
                if (item.Value is { } value)
                {
                    Source.Insert(index, value);
                }
            }

            public override Boolean Remove(T item)
            {
                return item is not null && Source.Remove(item);
            }

            public override Boolean Remove(NullMaybe<T> item)
            {
                return item.Value is { } value && Source.Remove(value);
            }

            public override void RemoveAt(Int32 index)
            {
                Source.RemoveAt(index);
            }

            public override void Clear()
            {
                Source.Clear();
            }

            protected override void CopyTo(Array array, Int32 index)
            {
                switch (array)
                {
                    case T[] destination:
                        CopyTo(destination, index);
                        return;
                    case NullMaybe<T>[] destination:
                        CopyTo(destination, index);
                        return;
                    default:
                    {
                        if (Source is not ICollection source)
                        {
                            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                            throw array is null ? new ArgumentNullException(nameof(array)) : new InvalidOperationException();
                        }

                        source.CopyTo(array, index);
                        return;
                    }
                }
            }

            public override void CopyTo(T[] array, Int32 index)
            {
                Source.CopyTo(array, index);
            }

            public override void CopyTo(NullMaybe<T>[] array, Int32 index)
            {
                Source.CopyTo(array, index);
            }

            public override IEnumerator<T> GetEnumerator()
            {
                return Source.GetEnumerator();
            }

            protected override IEnumerator<NullMaybe<T>> GetNullableEnumerator()
            {
                foreach (T item in this)
                {
                    yield return item;
                }
            }

            public override Int32 GetHashCode()
            {
                return Source.GetHashCode();
            }

            public override Boolean Equals(Object? other)
            {
                return Source.Equals(other);
            }

            public override String? ToString()
            {
                return Source.ToString();
            }

            public override NullMaybe<T> this[Int32 index]
            {
                get
                {
                    return Source[index];
                }
                set
                {
                    if (value.Value is { } item)
                    {
                        Source[index] = item;
                    }
                }
            }
        }

        private sealed class ToNullNullable : NullableSelectorListWrapper<T>
        {
            public override IList<T> Source { get; }

            public override IList<NullMaybe<T>> Nullable
            {
                get
                {
                    return this;
                }
            }

            public override Int32 Count
            {
                get
                {
                    return Source.Count;
                }
            }

            public override Boolean IsReadOnly
            {
                get
                {
                    return Source.IsReadOnly;
                }
            }

            public override Object SyncRoot
            {
                get
                {
                    return Source is ICollection source ? source.SyncRoot : Source;
                }
            }

            public override Boolean IsSynchronized
            {
                get
                {
                    return Source is ICollection { IsSynchronized: true };
                }
            }

            public ToNullNullable(IList<T> source)
            {
                Source = source ?? throw new ArgumentNullException(nameof(source));
            }

            public override Boolean Contains(T item)
            {
                return Source.Contains(item);
            }

            public override Boolean Contains(NullMaybe<T> item)
            {
                return Source.Contains(item);
            }

            public override Int32 IndexOf(T item)
            {
                return Source.IndexOf(item);
            }

            public override Int32 IndexOf(NullMaybe<T> item)
            {
                return Source.IndexOf(item);
            }

            protected override T Get(Int32 index)
            {
                return Source[index];
            }

            protected override void Set(Int32 index, T item)
            {
                Source[index] = item;
            }

            public override void Add(T item)
            {
                Source.Add(item);
            }

            public override void Add(NullMaybe<T> item)
            {
                Source.Add(item);
            }

            public override void Insert(Int32 index, T item)
            {
                Source.Insert(index, item);
            }

            public override void Insert(Int32 index, NullMaybe<T> item)
            {
                Source.Insert(index, item);
            }

            public override Boolean Remove(T item)
            {
                return Source.Remove(item);
            }

            public override Boolean Remove(NullMaybe<T> item)
            {
                return Source.Remove(item);
            }

            public override void RemoveAt(Int32 index)
            {
                Source.RemoveAt(index);
            }

            public override void Clear()
            {
                Source.Clear();
            }

            protected override void CopyTo(Array array, Int32 index)
            {
                switch (array)
                {
                    case T[] destination:
                        CopyTo(destination, index);
                        return;
                    case NullMaybe<T>[] destination:
                        CopyTo(destination, index);
                        return;
                    default:
                    {
                        if (Source is not ICollection source)
                        {
                            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                            throw array is null ? new ArgumentNullException(nameof(array)) : new InvalidOperationException();
                        }

                        source.CopyTo(array, index);
                        return;
                    }
                }
            }

            public override void CopyTo(T[] array, Int32 index)
            {
                Source.CopyTo(array, index);
            }

            public override void CopyTo(NullMaybe<T>[] array, Int32 index)
            {
                Source.CopyTo(array, index);
            }

            public override IEnumerator<T> GetEnumerator()
            {
                return Source.GetEnumerator();
            }

            protected override IEnumerator<NullMaybe<T>> GetNullableEnumerator()
            {
                foreach (T item in this)
                {
                    yield return item;
                }
            }

            public override Int32 GetHashCode()
            {
                return Source.GetHashCode();
            }

            public override Boolean Equals(Object? other)
            {
                return Source.Equals(other);
            }

            public override String? ToString()
            {
                return Source.ToString();
            }

            public override NullMaybe<T> this[Int32 index]
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

        private sealed class FromNullable : NullableSelectorListWrapper<T>
        {
            public override IList<T> Source
            {
                get
                {
                    return this;
                }
            }

            public override IList<NullMaybe<T>> Nullable { get; }

            public override Int32 Count
            {
                get
                {
                    return Source.Count;
                }
            }

            public override Boolean IsReadOnly
            {
                get
                {
                    return Source.IsReadOnly;
                }
            }

            public override Object SyncRoot
            {
                get
                {
                    return Nullable is IList source ? source.SyncRoot : Nullable;
                }
            }

            public override Boolean IsSynchronized
            {
                get
                {
                    return Nullable is IList { IsSynchronized: true };
                }
            }

            public FromNullable(IList<NullMaybe<T>> source)
            {
                Nullable = source ?? throw new ArgumentNullException(nameof(source));
            }

            public override Boolean Contains(T item)
            {
                return Nullable.Contains(item);
            }

            public override Boolean Contains(NullMaybe<T> item)
            {
                return Nullable.Contains(item);
            }

            public override Int32 IndexOf(T item)
            {
                return Nullable.IndexOf(item);
            }

            public override Int32 IndexOf(NullMaybe<T> item)
            {
                return Nullable.IndexOf(item);
            }

            protected override T Get(Int32 index)
            {
                return Nullable[index];
            }

            protected override void Set(Int32 index, T item)
            {
                Nullable[index] = item;
            }

            public override void Add(T item)
            {
                Nullable.Add(item);
            }

            public override void Add(NullMaybe<T> item)
            {
                Nullable.Add(item);
            }

            public override void Insert(Int32 index, T item)
            {
                Nullable.Insert(index, item);
            }

            public override void Insert(Int32 index, NullMaybe<T> item)
            {
                Nullable.Insert(index, item);
            }

            public override Boolean Remove(T item)
            {
                return Nullable.Remove(item);
            }

            public override Boolean Remove(NullMaybe<T> item)
            {
                return Nullable.Remove(item);
            }

            public override void RemoveAt(Int32 index)
            {
                Nullable.RemoveAt(index);
            }

            public override void Clear()
            {
                Nullable.Clear();
            }

            protected override void CopyTo(Array array, Int32 index)
            {
                switch (array)
                {
                    case T[] destination:
                        CopyTo(destination, index);
                        return;
                    case NullMaybe<T>[] destination:
                        CopyTo(destination, index);
                        return;
                    default:
                    {
                        if (Nullable is not ICollection source)
                        {
                            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                            throw array is null ? new ArgumentNullException(nameof(array)) : new InvalidOperationException();
                        }

                        source.CopyTo(array, index);
                        return;
                    }
                }
            }

            public override void CopyTo(T[] array, Int32 index)
            {
                Nullable.CopyTo(array, index);
            }

            public override void CopyTo(NullMaybe<T>[] array, Int32 index)
            {
                Nullable.CopyTo(array, index);
            }

            public override IEnumerator<T> GetEnumerator()
            {
                return Nullable.Cast<T>().GetEnumerator();
            }

            protected override IEnumerator<NullMaybe<T>> GetNullableEnumerator()
            {
                return Nullable.GetEnumerator();
            }

            public override Int32 GetHashCode()
            {
                return Nullable.GetHashCode();
            }

            public override Boolean Equals(Object? other)
            {
                return Nullable.Equals(other);
            }

            public override String? ToString()
            {
                return Nullable.ToString();
            }

            public override NullMaybe<T> this[Int32 index]
            {
                get
                {
                    return Nullable[index];
                }
                set
                {
                    Nullable[index] = value;
                }
            }
        }
    }
    
    public sealed class TwoWaySelectorListWrapper<T, TKey> : IList<TKey>
    {
        public IList<T> Source { get; }
        private Func<T, TKey> To { get; }
        private Func<TKey, T> From { get; }

        public Int32 Count
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

        public TwoWaySelectorListWrapper(IList<T> collection, Func<T, TKey> to, Func<TKey, T> from)
        {
            Source = collection ?? throw new ArgumentNullException(nameof(collection));
            To = to ?? throw new ArgumentNullException(nameof(to));
            From = from ?? throw new ArgumentNullException(nameof(from));
        }

        public Boolean Contains(T item)
        {
            return Source.Contains(item);
        }

        public Boolean Contains(TKey item)
        {
            return Source.Contains(From(item));
        }

        public Int32 IndexOf(T item)
        {
            return Source.IndexOf(item);
        }

        public Int32 IndexOf(TKey item)
        {
            return Source.IndexOf(From(item));
        }

        public void Add(T item)
        {
            Source.Add(item);
        }

        public void Add(TKey item)
        {
            Source.Add(From(item));
        }

        public void Insert(Int32 index, T item)
        {
            Source.Insert(index, item);
        }

        public void Insert(Int32 index, TKey item)
        {
            Source.Insert(index, From(item));
        }

        public Boolean Remove(T item)
        {
            return Source.Remove(item);
        }

        public Boolean Remove(TKey item)
        {
            return Source.Remove(From(item));
        }

        public void RemoveAt(Int32 index)
        {
            Source.RemoveAt(index);
        }

        public void Clear()
        {
            Source.Clear();
        }

        public void CopyTo(T[] array, Int32 index)
        {
            Source.CopyTo(array, index);
        }

        public void CopyTo(TKey[] array, Int32 index)
        {
            Source.CopyTo(array, index, To);
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return Source.Select(To).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override Int32 GetHashCode()
        {
            return Source.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Source.Equals(other);
        }

        public override String? ToString()
        {
            return Source.ToString();
        }

        public TKey this[Int32 index]
        {
            get
            {
                return To(Source[index]);
            }
            set
            {
                Source[index] = From(value);
            }
        }
    }
    
    public sealed class SelectorListWrapper<T, TKey> : IList<TKey>
    {
        private IList<T> Internal { get; }
        private Func<T, TKey> Selector { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return typeof(T) != typeof(TKey) || Internal.IsReadOnly;
            }
        }

        public SelectorListWrapper(IList<T> collection, Func<T, TKey> selector)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public Boolean Contains(TKey item)
        {
            return Internal.Select(Selector).Contains(item);
        }

        public Int32 IndexOf(TKey item)
        {
            return Internal.Select(Selector).IndexOf(item);
        }

        public void Add(TKey item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            Internal.Add((T) (Object) item!);
        }

        public void Insert(Int32 index, TKey item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            Internal.Insert(index, (T) (Object) item!);
        }

        public Boolean Remove(TKey item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            return Internal.Remove((T) (Object) item!);
        }

        public void RemoveAt(Int32 index)
        {
            if (Internal.IsReadOnly)
            {
                throw new NotSupportedException();
            }
            
            Internal.RemoveAt(index);
        }

        public void Clear()
        {
            if (Internal.IsReadOnly)
            {
                throw new NotSupportedException();
            }

            Internal.Clear();
        }

        public void CopyTo(TKey[] array, Int32 index)
        {
            Internal.CopyTo(array, index, Selector);
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return Internal.Select(Selector).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Internal.Equals(other);
        }

        public override String? ToString()
        {
            return Internal.ToString();
        }

        public TKey this[Int32 index]
        {
            get
            {
                return Selector(Internal[index]);
            }
            set
            {
                if (IsReadOnly)
                {
                    throw new NotSupportedException();
                }
                
                Internal[index] = (T) (Object) value!;
            }
        }
    }

    public sealed class ReadOnlySelectorListWrapper<T, TKey> : IReadOnlyList<TKey>
    {
        private IReadOnlyList<T> Internal { get; }
        private Func<T, TKey> Selector { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public ReadOnlySelectorListWrapper(IReadOnlyList<T> list, Func<T, TKey> selector)
        {
            Internal = list ?? throw new ArgumentNullException(nameof(list));
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return Internal.Select(Selector).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Internal.Equals(other);
        }

        public override String? ToString()
        {
            return Internal.ToString();
        }

        public TKey this[Int32 index]
        {
            get
            {
                return Selector(Internal[index]);
            }
        }
    }

    public static class SelectorListWrapper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullableSelectorCollectionWrapper<T> Nullable<T>(IList<T>? source)
        {
            return NullableSelectorListWrapper<T>.Create(source ?? new List<T>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullableSelectorListWrapper<T> Nullable<T>(IList<T>? source, Boolean @null)
        {
            return NullableSelectorListWrapper<T>.Create(source ?? new List<T>(), @null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullableSelectorListWrapper<T> Nullable<T>(IList<NullMaybe<T>>? source)
        {
            return NullableSelectorListWrapper<T>.Create(source ?? new List<NullMaybe<T>>());
        }
    }
}