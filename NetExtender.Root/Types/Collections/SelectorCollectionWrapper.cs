// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Collections
{
    public abstract class NullableSelectorCollectionWrapper<T> : ICollection<T>, ICollection<NullMaybe<T>>, ICollection
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static NullableSelectorCollectionWrapper<T> Create(ICollection<T> source)
        {
            return new ToNullable(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static NullableSelectorCollectionWrapper<T> Create(ICollection<T> source, Boolean @null)
        {
            return @null ? new ToNullNullable(source) : new ToNullable(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static NullableSelectorCollectionWrapper<T> Create(ICollection<NullMaybe<T>> source)
        {
            return new FromNullable(source);
        }

        public abstract ICollection<T> Source { get; }
        public abstract ICollection<NullMaybe<T>> Nullable { get; }

        public abstract Int32 Count { get; }
        public abstract Boolean IsReadOnly { get; }
        public abstract Object SyncRoot { get; }
        public abstract Boolean IsSynchronized { get; }

        public abstract Boolean Contains(T item);
        public abstract Boolean Contains(NullMaybe<T> item);
        public abstract void Add(T item);
        public abstract void Add(NullMaybe<T> item);
        public abstract Boolean Remove(T item);
        public abstract Boolean Remove(NullMaybe<T> item);
        public abstract void Clear();
        protected abstract void CopyTo(Array array, Int32 index);

        void ICollection.CopyTo(Array array, Int32 index)
        {
            CopyTo(array, index);
        }

        public void CopyTo(T[] array)
        {
            CopyTo(array, 0);
        }

        public abstract void CopyTo(T[] array, Int32 index);

        public void CopyTo(NullMaybe<T>[] array)
        {
            CopyTo(array, 0);
        }

        public abstract void CopyTo(NullMaybe<T>[] array, Int32 index);

        public abstract IEnumerator<T> GetEnumerator();
        protected abstract IEnumerator<NullMaybe<T>> GetNullableEnumerator();

        IEnumerator<NullMaybe<T>> IEnumerable<NullMaybe<T>>.GetEnumerator()
        {
            return GetNullableEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private sealed class ToNullable : NullableSelectorCollectionWrapper<T>
        {
            public override ICollection<T> Source { get; }

            public override ICollection<NullMaybe<T>> Nullable
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

            public ToNullable(ICollection<T> source)
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

            public override Boolean Remove(T item)
            {
                return item is not null && Source.Remove(item);
            }

            public override Boolean Remove(NullMaybe<T> item)
            {
                return item.Value is { } value && Source.Remove(value);
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
                EnumerableBaseUtilities.CopyTo(Source, array, index);
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
        }

        private sealed class ToNullNullable : NullableSelectorCollectionWrapper<T>
        {
            public override ICollection<T> Source { get; }

            public override ICollection<NullMaybe<T>> Nullable
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

            public ToNullNullable(ICollection<T> source)
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

            public override void Add(T item)
            {
                Source.Add(item);
            }

            public override void Add(NullMaybe<T> item)
            {
                Source.Add(item);
            }

            public override Boolean Remove(T item)
            {
                return Source.Remove(item);
            }

            public override Boolean Remove(NullMaybe<T> item)
            {
                return Source.Remove(item);
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
                EnumerableBaseUtilities.CopyTo(Source, array, index);
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
        }

        private sealed class FromNullable : NullableSelectorCollectionWrapper<T>
        {
            public override ICollection<T> Source
            {
                get
                {
                    return this;
                }
            }

            public override ICollection<NullMaybe<T>> Nullable { get; }

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
                    return Nullable is ICollection source ? source.SyncRoot : Nullable;
                }
            }

            public override Boolean IsSynchronized
            {
                get
                {
                    return Nullable is ICollection { IsSynchronized: true };
                }
            }

            public FromNullable(ICollection<NullMaybe<T>> source)
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

            public override void Add(T item)
            {
                Nullable.Add(item);
            }

            public override void Add(NullMaybe<T> item)
            {
                Nullable.Add(item);
            }

            public override Boolean Remove(T item)
            {
                return Nullable.Remove(item);
            }

            public override Boolean Remove(NullMaybe<T> item)
            {
                return Nullable.Remove(item);
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
                EnumerableBaseUtilities.CopyTo(Nullable, array, index);
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
        }
    }

    public sealed class TwoWaySelectorCollectionWrapper<T, TKey> : ICollection<TKey>
    {
        public ICollection<T> Source { get; }
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

        public TwoWaySelectorCollectionWrapper(ICollection<T> collection, Func<T, TKey> to, Func<TKey, T> from)
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

        public void Add(T item)
        {
            Source.Add(item);
        }

        public void Add(TKey item)
        {
            Source.Add(From(item));
        }

        public Boolean Remove(T item)
        {
            return Source.Remove(item);
        }

        public Boolean Remove(TKey item)
        {
            return Source.Remove(From(item));
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
            EnumerableBaseUtilities.CopyTo(Source, array, index, To);
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
    }

    public sealed class SelectorCollectionWrapper<T, TKey> : ICollection<TKey>
    {
        private ICollection<T> Internal { get; }
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

        public SelectorCollectionWrapper(ICollection<T> collection, Func<T, TKey> selector)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public Boolean Contains(TKey item)
        {
            return Internal.Select(Selector).Contains(item);
        }

        public void Add(TKey item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            Internal.Add((T) (Object) item!);
        }

        public Boolean Remove(TKey item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            return Internal.Remove((T) (Object) item!);
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
            EnumerableBaseUtilities.CopyTo(Internal, array, index, Selector);
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
    }

    public sealed class ReadOnlySelectorCollectionWrapper<T, TKey> : IReadOnlyCollection<TKey>
    {
        private IReadOnlyCollection<T> Internal { get; }
        private Func<T, TKey> Selector { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public ReadOnlySelectorCollectionWrapper(IReadOnlyCollection<T> collection, Func<T, TKey> selector)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
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
    }

    public static class SelectorCollectionWrapper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullableSelectorCollectionWrapper<T> Nullable<T>(ICollection<T>? source)
        {
            return NullableSelectorCollectionWrapper<T>.Create(source ?? new List<T>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullableSelectorCollectionWrapper<T> Nullable<T>(ICollection<T>? source, Boolean @null)
        {
            return NullableSelectorCollectionWrapper<T>.Create(source ?? new List<T>(), @null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullableSelectorCollectionWrapper<T> Nullable<T>(ICollection<NullMaybe<T>>? source)
        {
            return NullableSelectorCollectionWrapper<T>.Create(source ?? new List<NullMaybe<T>>());
        }
    }
}