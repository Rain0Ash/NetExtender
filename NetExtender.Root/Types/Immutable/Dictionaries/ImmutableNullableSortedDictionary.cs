using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Collections;
using NetExtender.Types.Enumerators;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Immutable.Dictionaries
{
    public static class ImmutableNullableSortedDictionary
    {
        public static ImmutableNullableSortedDictionary<TKey, TValue> Create<TKey, TValue>()
        {
            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty;
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> Create<TKey, TValue>(IComparer<TKey>? keyComparer)
        {
            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> Create<TKey, TValue>(IComparer<NullMaybe<TKey>>? keyComparer)
        {
            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> Create<TKey, TValue>(IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> Create<TKey, TValue>(IComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.AddRange(items);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IComparer<TKey>? keyComparer, IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IComparer<NullMaybe<TKey>>? keyComparer, IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer, IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>()
        {
            return Create<TKey, TValue>().ToBuilder();
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IComparer<TKey>? keyComparer)
        {
            return Create<TKey, TValue>(keyComparer).ToBuilder();
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IComparer<NullMaybe<TKey>>? keyComparer)
        {
            return Create<TKey, TValue>(keyComparer).ToBuilder();
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return Create(keyComparer, valueComparer).ToBuilder();
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return Create(keyComparer, valueComparer).ToBuilder();
        }
    }

    //TODO: extensions methods for ImmutableDictionary Copy
    public sealed class ImmutableNullableSortedDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>, IDictionary<TKey, TValue>, IDictionary
    {
        public static ImmutableNullableSortedDictionary<TKey, TValue> Empty { get; } = new ImmutableNullableSortedDictionary<TKey, TValue>(ImmutableSortedDictionary<NullMaybe<TKey>, TValue>.Empty);

        private ImmutableSortedDictionary<NullMaybe<TKey>, TValue> Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Internal.IsEmpty;
            }
        }

        public IComparer<NullMaybe<TKey>> Comparer
        {
            get
            {
                return KeyComparer;
            }
        }

        public IComparer<NullMaybe<TKey>> KeyComparer
        {
            get
            {
                return Internal.KeyComparer;
            }
        }

        public IEqualityComparer<TValue> ValueComparer
        {
            get
            {
                return Internal.ValueComparer;
            }
        }

        private NullableSelectorCollectionWrapper<TKey>? _keys;

        public IEnumerable<TKey> Keys
        {
            get
            {
                return Internal.Keys.Select(NullMaybe<TKey>.Unwrap);
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return _keys ??= SelectorCollectionWrapper.Nullable(((IDictionary<NullMaybe<TKey>, TValue>) Internal).Keys);
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                return _keys ??= SelectorCollectionWrapper.Nullable(((IDictionary<NullMaybe<TKey>, TValue>) Internal).Keys);
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return Internal.Values;
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                return ((IDictionary) Internal).Values;
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                return ((IDictionary<NullMaybe<TKey>, TValue>) Internal).Values;
            }
        }

        Boolean IDictionary.IsReadOnly
        {
            get
            {
                return ((IDictionary) Internal).IsReadOnly;
            }
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return ((IDictionary) Internal).IsReadOnly;
            }
        }

        Boolean IDictionary.IsFixedSize
        {
            get
            {
                return ((IDictionary) Internal).IsFixedSize;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Internal).SyncRoot;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Internal).IsSynchronized;
            }
        }

        private ImmutableNullableSortedDictionary(ImmutableSortedDictionary<NullMaybe<TKey>, TValue> dictionary)
        {
            Internal = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ImmutableNullableSortedDictionary<TKey, TValue> Self(ImmutableSortedDictionary<NullMaybe<TKey>, TValue> value)
        {
            return new ImmutableNullableSortedDictionary<TKey, TValue>(value);
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> WithComparers(IComparer<TKey>? comparer)
        {
            return WithComparers(comparer?.ToNullMaybeComparer());
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> WithComparers(IComparer<NullMaybe<TKey>>? comparer)
        {
            ImmutableSortedDictionary<NullMaybe<TKey>, TValue> @new = Internal.WithComparers(comparer);
            return @new != Internal ? Self(@new) : this;
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> WithComparers(IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return WithComparers(keyComparer?.ToNullMaybeComparer(), valueComparer);
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> WithComparers(IComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            ImmutableSortedDictionary<NullMaybe<TKey>, TValue> @new = Internal.WithComparers(keyComparer, valueComparer);
            return @new != Internal ? Self(@new) : this;
        }

        public Builder ToBuilder()
        {
            return new Builder(Internal.ToBuilder());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Object Key(Object? key)
        {
            return key switch
            {
                null => default(NullMaybe<TKey>),
                TKey convert => new NullMaybe<TKey>(convert),
                NullMaybe<TKey> convert => convert,
                _ => key
            };
        }

        Boolean IDictionary.Contains(Object? key)
        {
            return ((IDictionary) Internal).Contains(Key(key));
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return Internal.Contains(item.KeyNullable());
        }

        public Boolean ContainsKey(TKey key)
        {
            return Internal.ContainsKey(key);
        }

        public Boolean TryGetKey(TKey equalKey, out TKey actualKey)
        {
            if (Internal.TryGetKey(equalKey, out NullMaybe<TKey> result))
            {
                actualKey = result;
                return true;
            }

            actualKey = result;
            return false;
        }

        public Boolean ContainsValue(TValue value)
        {
            return Internal.ContainsValue(value);
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return Internal.TryGetValue(key, out value);
        }

        void IDictionary.Add(Object? key, Object? value)
        {
            ((IDictionary) Internal).Add(Key(key), value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<NullMaybe<TKey>, TValue>>) Internal).Add(item.KeyNullable());
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            ((IDictionary<NullMaybe<TKey>, TValue>) Internal).Add(key, value);
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            ImmutableSortedDictionary<NullMaybe<TKey>, TValue> @new = Internal.Add(key, value);
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>>? pairs)
        {
            ImmutableSortedDictionary<NullMaybe<TKey>, TValue> @new = pairs is not null ? Internal.AddRange(EnumerableBaseUtilities.KeyNullable(pairs)) : Internal;
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>>? pairs)
        {
            return AddRange(pairs);
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> SetItem(TKey key, TValue value)
        {
            ImmutableSortedDictionary<NullMaybe<TKey>, TValue> @new = Internal.SetItem(key, value);
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
        {
            return SetItem(key, value);
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            ImmutableSortedDictionary<NullMaybe<TKey>, TValue> @new = items is not null ? Internal.SetItems(EnumerableBaseUtilities.KeyNullable(items)) : Internal;
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return SetItems(items);
        }

        void IDictionary.Remove(Object? key)
        {
            ((IDictionary) Internal).Remove(Key(key));
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<NullMaybe<TKey>, TValue>>) Internal).Remove(item.KeyNullable());
        }

        Boolean IDictionary<TKey, TValue>.Remove(TKey key)
        {
            return ((IDictionary<NullMaybe<TKey>, TValue>) Internal).Remove(key);
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> Remove(TKey key)
        {
            ImmutableSortedDictionary<NullMaybe<TKey>, TValue> @new = Internal.Remove(key);
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey>? keys)
        {
            ImmutableSortedDictionary<NullMaybe<TKey>, TValue> @new = keys is not null ? Internal.RemoveRange(EnumerableBaseUtilities.Nullable(keys)) : Internal;
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey>? keys)
        {
            return RemoveRange(keys);
        }

        void IDictionary.Clear()
        {
            ((IDictionary) Internal).Clear();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            ((ICollection<KeyValuePair<NullMaybe<TKey>, TValue>>) Internal).Clear();
        }

        public ImmutableNullableSortedDictionary<TKey, TValue> Clear()
        {
            ImmutableSortedDictionary<NullMaybe<TKey>, TValue> @new = Internal.Clear();
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
        {
            return Clear();
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            switch (array)
            {
                case KeyValuePair<TKey, TValue>[] destination:
                    CopyTo(destination, index);
                    return;
                case KeyValuePair<NullMaybe<TKey>, TValue>[] destination:
                    ((ICollection<KeyValuePair<NullMaybe<TKey>, TValue>>) Internal).CopyTo(destination, index);
                    return;
                case TKey[] destination:
                    EnumerableBaseUtilities.CopyTo(Internal, destination, index, static pair => pair.Key.Value);
                    return;
                case NullMaybe<TKey>[] destination:
                    EnumerableBaseUtilities.CopyTo(Internal, destination, index, static pair => pair.Key);
                    return;
                case TValue[] destination:
                    EnumerableBaseUtilities.CopyTo(Internal, destination, index, static pair => pair.Value);
                    return;
                default:
                {
                    if (Internal is not ICollection source)
                    {
                        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                        throw array is null ? new ArgumentNullException(nameof(array)) : new InvalidOperationException();
                    }

                    source.CopyTo(array, index);
                    return;
                }
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 index)
        {
            EnumerableBaseUtilities.CopyTo(Internal, array, index, static pair => new KeyValuePair<TKey, TValue>(pair.Key, pair.Value));
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(Internal.GetEnumerator());
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new DictionaryEnumerator<TKey, TValue, Enumerator>(GetEnumerator());
        }

        Object? IDictionary.this[Object? key]
        {
            get
            {
                return ((IDictionary) Internal)[Key(key)];
            }
            set
            {
                ((IDictionary) Internal)[Key(key)] = value;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return Internal[key];
            }
        }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                return this[key];
            }
            set
            {
                ((IDictionary<NullMaybe<TKey>, TValue>) Internal)[key] = value;
            }
        }

        public sealed class Builder : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IDictionary
        {
            private ImmutableSortedDictionary<NullMaybe<TKey>, TValue>.Builder Internal { get; }

            public Int32 Count
            {
                get
                {
                    return Internal.Count;
                }
            }

            public IComparer<NullMaybe<TKey>> Comparer
            {
                get
                {
                    return KeyComparer;
                }
            }

            public IComparer<NullMaybe<TKey>> KeyComparer
            {
                get
                {
                    return Internal.KeyComparer;
                }
            }

            public IEqualityComparer<TValue> ValueComparer
            {
                get
                {
                    return Internal.ValueComparer;
                }
            }

            private NullableSelectorCollectionWrapper<TKey>? _keys;

            public IEnumerable<TKey> Keys
            {
                get
                {
                    return Internal.Keys.Select(NullMaybe<TKey>.Unwrap);
                }
            }

            ICollection IDictionary.Keys
            {
                get
                {
                    return _keys ??= SelectorCollectionWrapper.Nullable(((IDictionary<NullMaybe<TKey>, TValue>) Internal).Keys);
                }
            }

            ICollection<TKey> IDictionary<TKey, TValue>.Keys
            {
                get
                {
                    return _keys ??= SelectorCollectionWrapper.Nullable(((IDictionary<NullMaybe<TKey>, TValue>) Internal).Keys);
                }
            }

            public IEnumerable<TValue> Values
            {
                get
                {
                    return Internal.Values;
                }
            }

            ICollection IDictionary.Values
            {
                get
                {
                    return ((IDictionary) Internal).Values;
                }
            }

            ICollection<TValue> IDictionary<TKey, TValue>.Values
            {
                get
                {
                    return ((IDictionary<NullMaybe<TKey>, TValue>) Internal).Values;
                }
            }

            Boolean IDictionary.IsReadOnly
            {
                get
                {
                    return ((IDictionary) Internal).IsReadOnly;
                }
            }

            Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
            {
                get
                {
                    return ((IDictionary) Internal).IsReadOnly;
                }
            }

            Boolean IDictionary.IsFixedSize
            {
                get
                {
                    return ((IDictionary) Internal).IsFixedSize;
                }
            }

            Object ICollection.SyncRoot
            {
                get
                {
                    return ((ICollection) Internal).SyncRoot;
                }
            }

            Boolean ICollection.IsSynchronized
            {
                get
                {
                    return ((ICollection) Internal).IsSynchronized;
                }
            }

            public Builder(ImmutableSortedDictionary<NullMaybe<TKey>, TValue>.Builder builder)
            {
                Internal = builder ?? throw new ArgumentNullException(nameof(builder));
            }

            public ImmutableNullableSortedDictionary<TKey, TValue> ToImmutable()
            {
                return Self(Internal.ToImmutable());
            }

            Boolean IDictionary.Contains(Object key)
            {
                return ((IDictionary) Internal).Contains(Key(key));
            }

            public Boolean Contains(KeyValuePair<TKey, TValue> item)
            {
                return Internal.Contains(item.KeyNullable());
            }

            public Boolean ContainsKey(TKey key)
            {
                return Internal.ContainsKey(key);
            }

            public Boolean ContainsValue(TValue value)
            {
                return Internal.ContainsValue(value);
            }

            public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
            {
                return Internal.TryGetValue(key, out value);
            }

            void IDictionary.Add(Object key, Object? value)
            {
                ((IDictionary) Internal).Add(Key(key), value);
            }

            public void Add(KeyValuePair<TKey, TValue> item)
            {
                Internal.Add(item.KeyNullable());
            }

            public void Add(TKey key, TValue value)
            {
                Internal.Add(key, value);
            }

            public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
            {
                if (items is null)
                {
                    throw new ArgumentNullException(nameof(items));
                }

                Internal.AddRange(EnumerableBaseUtilities.KeyNullable(items));
            }

            void IDictionary.Remove(Object key)
            {
                ((IDictionary) Internal).Remove(Key(key));
            }

            public Boolean Remove(KeyValuePair<TKey, TValue> item)
            {
                return Internal.Remove(item.KeyNullable());
            }

            public Boolean Remove(TKey key)
            {
                return Internal.Remove(key);
            }

            public void Clear()
            {
                Internal.Clear();
            }

            void ICollection.CopyTo(Array array, Int32 index)
            {
                switch (array)
                {
                    case KeyValuePair<TKey, TValue>[] destination:
                        CopyTo(destination, index);
                        return;
                    case KeyValuePair<NullMaybe<TKey>, TValue>[] destination:
                        ((ICollection<KeyValuePair<NullMaybe<TKey>, TValue>>) Internal).CopyTo(destination, index);
                        return;
                    case TKey[] destination:
                        EnumerableBaseUtilities.CopyTo(Internal, destination, index, static pair => pair.Key.Value);
                        return;
                    case NullMaybe<TKey>[] destination:
                        EnumerableBaseUtilities.CopyTo(Internal, destination, index, static pair => pair.Key);
                        return;
                    case TValue[] destination:
                        EnumerableBaseUtilities.CopyTo(Internal, destination, index, static pair => pair.Value);
                        return;
                    default:
                    {
                        if (Internal is not ICollection source)
                        {
                            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                            throw array is null ? new ArgumentNullException(nameof(array)) : new InvalidOperationException();
                        }

                        source.CopyTo(array, index);
                        return;
                    }
                }
            }

            public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 index)
            {
                EnumerableBaseUtilities.CopyTo(Internal, array, index, static pair => new KeyValuePair<TKey, TValue>(pair.Key, pair.Value));
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(Internal.GetEnumerator());
            }

            IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            IDictionaryEnumerator IDictionary.GetEnumerator()
            {
                return new DictionaryEnumerator<TKey, TValue, Enumerator>(GetEnumerator());
            }

            Object? IDictionary.this[Object key]
            {
                get
                {
                    return ((IDictionary) Internal)[Key(key)];
                }
                set
                {
                    ((IDictionary) Internal)[Key(key)] = value;
                }
            }

            public TValue this[TKey key]
            {
                get
                {
                    return Internal[key];
                }
                set
                {
                    Internal[key] = value;
                }
            }
        }

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private ImmutableSortedDictionary<NullMaybe<TKey>, TValue>.Enumerator Internal;

            public readonly KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    KeyValuePair<NullMaybe<TKey>, TValue> current = Internal.Current;
                    return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                }
            }

            readonly Object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Enumerator(ImmutableSortedDictionary<NullMaybe<TKey>, TValue>.Enumerator enumerator)
            {
                Internal = enumerator;
            }

            public Boolean MoveNext()
            {
                return Internal.MoveNext();
            }

            public void Reset()
            {
                Internal.Reset();
            }

            public void Dispose()
            {
                Internal.Dispose();
            }
        }
    }
}