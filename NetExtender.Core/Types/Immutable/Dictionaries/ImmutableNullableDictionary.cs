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
    public static class ImmutableNullableDictionary
    {
        public static ImmutableNullableDictionary<TKey, TValue> Create<TKey, TValue>()
        {
            return ImmutableNullableDictionary<TKey, TValue>.Empty;
        }

        public static ImmutableNullableDictionary<TKey, TValue> Create<TKey, TValue>(IEqualityComparer<TKey>? keyComparer)
        {
            return ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
        }

        public static ImmutableNullableDictionary<TKey, TValue> Create<TKey, TValue>(IEqualityComparer<NullMaybe<TKey>>? keyComparer)
        {
            return ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
        }

        public static ImmutableNullableDictionary<TKey, TValue> Create<TKey, TValue>(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        public static ImmutableNullableDictionary<TKey, TValue> Create<TKey, TValue>(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        public static ImmutableNullableDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return ImmutableNullableDictionary<TKey, TValue>.Empty.AddRange(items);
        }

        public static ImmutableNullableDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEqualityComparer<TKey>? keyComparer, IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
        }

        public static ImmutableNullableDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
        }

        public static ImmutableNullableDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
        }

        public static ImmutableNullableDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer, IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
        }

        public static ImmutableNullableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>()
        {
            return Create<TKey, TValue>().ToBuilder();
        }

        public static ImmutableNullableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IEqualityComparer<TKey>? keyComparer)
        {
            return Create<TKey, TValue>(keyComparer).ToBuilder();
        }

        public static ImmutableNullableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IEqualityComparer<NullMaybe<TKey>>? keyComparer)
        {
            return Create<TKey, TValue>(keyComparer).ToBuilder();
        }

        public static ImmutableNullableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return Create(keyComparer, valueComparer).ToBuilder();
        }

        public static ImmutableNullableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return Create(keyComparer, valueComparer).ToBuilder();
        }
    }

    public sealed class ImmutableNullableDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>, IDictionary<TKey, TValue>, IDictionary
    {
        public static ImmutableNullableDictionary<TKey, TValue> Empty { get; } = new ImmutableNullableDictionary<TKey, TValue>(ImmutableDictionary<NullMaybe<TKey>, TValue>.Empty);

        private ImmutableDictionary<NullMaybe<TKey>, TValue> Internal { get; }

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

        public IEqualityComparer<NullMaybe<TKey>> Comparer
        {
            get
            {
                return KeyComparer;
            }
        }

        public IEqualityComparer<NullMaybe<TKey>> KeyComparer
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

        private ImmutableNullableDictionary(ImmutableDictionary<NullMaybe<TKey>, TValue> dictionary)
        {
            Internal = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ImmutableNullableDictionary<TKey, TValue> Self(ImmutableDictionary<NullMaybe<TKey>, TValue> value)
        {
            return new ImmutableNullableDictionary<TKey, TValue>(value);
        }

        public ImmutableNullableDictionary<TKey, TValue> WithComparers(IEqualityComparer<TKey>? comparer)
        {
            return WithComparers(comparer?.ToNullMaybeEqualityComparer());
        }

        public ImmutableNullableDictionary<TKey, TValue> WithComparers(IEqualityComparer<NullMaybe<TKey>>? comparer)
        {
            ImmutableDictionary<NullMaybe<TKey>, TValue> @new = Internal.WithComparers(comparer);
            return @new != Internal ? Self(@new) : this;
        }

        public ImmutableNullableDictionary<TKey, TValue> WithComparers(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return WithComparers(keyComparer?.ToNullMaybeEqualityComparer(), valueComparer);
        }

        public ImmutableNullableDictionary<TKey, TValue> WithComparers(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            ImmutableDictionary<NullMaybe<TKey>, TValue> @new = Internal.WithComparers(keyComparer, valueComparer);
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

        public ImmutableNullableDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            ImmutableDictionary<NullMaybe<TKey>, TValue> @new = Internal.Add(key, value);
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }

        public ImmutableNullableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>>? pairs)
        {
            ImmutableDictionary<NullMaybe<TKey>, TValue> @new = pairs is not null ? Internal.AddRange(pairs.KeyNullable()) : Internal;
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>>? pairs)
        {
            return AddRange(pairs);
        }

        public ImmutableNullableDictionary<TKey, TValue> SetItem(TKey key, TValue value)
        {
            ImmutableDictionary<NullMaybe<TKey>, TValue> @new = Internal.SetItem(key, value);
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
        {
            return SetItem(key, value);
        }

        public ImmutableNullableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            ImmutableDictionary<NullMaybe<TKey>, TValue> @new = items is not null ? Internal.SetItems(items.KeyNullable()) : Internal;
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

        public ImmutableNullableDictionary<TKey, TValue> Remove(TKey key)
        {
            ImmutableDictionary<NullMaybe<TKey>, TValue> @new = Internal.Remove(key);
            return @new != Internal ? Self(@new) : this;
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }

        public ImmutableNullableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey>? keys)
        {
            ImmutableDictionary<NullMaybe<TKey>, TValue> @new = keys is not null ? Internal.RemoveRange(keys.Nullable()) : Internal;
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

        public ImmutableNullableDictionary<TKey, TValue> Clear()
        {
            ImmutableDictionary<NullMaybe<TKey>, TValue> @new = Internal.Clear();
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
                    Internal.CopyTo(destination, index, static pair => pair.Key.Value);
                    return;
                case NullMaybe<TKey>[] destination:
                    Internal.CopyTo(destination, index, static pair => pair.Key);
                    return;
                case TValue[] destination:
                    Internal.CopyTo(destination, index, static pair => pair.Value);
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
            Internal.CopyTo(array, index, static pair => new KeyValuePair<TKey, TValue>(pair.Key, pair.Value));
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
            private ImmutableDictionary<NullMaybe<TKey>, TValue>.Builder Internal { get; }

            public Int32 Count
            {
                get
                {
                    return Internal.Count;
                }
            }

            public IEqualityComparer<NullMaybe<TKey>> Comparer
            {
                get
                {
                    return KeyComparer;
                }
            }

            public IEqualityComparer<NullMaybe<TKey>> KeyComparer
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

            public Builder(ImmutableDictionary<NullMaybe<TKey>, TValue>.Builder builder)
            {
                Internal = builder ?? throw new ArgumentNullException(nameof(builder));
            }

            public ImmutableNullableDictionary<TKey, TValue> ToImmutable()
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

                Internal.AddRange(items.KeyNullable());
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
                        Internal.CopyTo(destination, index, static pair => pair.Key.Value);
                        return;
                    case NullMaybe<TKey>[] destination:
                        Internal.CopyTo(destination, index, static pair => pair.Key);
                        return;
                    case TValue[] destination:
                        Internal.CopyTo(destination, index, static pair => pair.Value);
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
                Internal.CopyTo(array, index, static pair => new KeyValuePair<TKey, TValue>(pair.Key, pair.Value));
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
            private ImmutableDictionary<NullMaybe<TKey>, TValue>.Enumerator Internal;

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

            public Enumerator(ImmutableDictionary<NullMaybe<TKey>, TValue>.Enumerator enumerator)
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