using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Collections;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Concurrent.Dictionaries
{
    public class NullableConcurrentDictionary<TKey, TValue> : ConcurrentDictionary<NullMaybe<TKey>, TValue>, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        public new ICollection<TKey> Keys
        {
            get
            {
                return new SelectorCollectionWrapper<NullMaybe<TKey>, TKey>(base.Keys, static nullable => nullable);
            }
        }

        public new ICollection<TValue> Values
        {
            get
            {
                return base.Values;
            }
        }
        
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }
        
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values;
            }
        }
        
        Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<NullMaybe<TKey>, TValue>>) this).IsReadOnly;
            }
        }
        
        public NullableConcurrentDictionary()
        {
        }
        
        public NullableConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
            : base(collection is not null ? collection.Select(Convert) : throw new ArgumentNullException(nameof(collection)))
        {
        }
        
        public NullableConcurrentDictionary(IEnumerable<KeyValuePair<NullMaybe<TKey>, TValue>> collection)
            : base(collection)
        {
        }
        
        public NullableConcurrentDictionary(IEnumerable<KeyValuePair<NullMaybe<TKey>, TValue>> collection, IEqualityComparer<TKey>? comparer)
            : this(collection, comparer?.ToNullMaybeEqualityComparer())
        {
        }
        
        public NullableConcurrentDictionary(IEnumerable<KeyValuePair<NullMaybe<TKey>, TValue>> collection, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(collection, comparer)
        {
        }
        
        public NullableConcurrentDictionary(IEqualityComparer<TKey>? comparer)
            : this(comparer?.ToNullMaybeEqualityComparer())
        {
        }
        
        public NullableConcurrentDictionary(IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(comparer)
        {
        }
        
        public NullableConcurrentDictionary(Int32 concurrencyLevel, IEnumerable<KeyValuePair<NullMaybe<TKey>, TValue>> collection, IEqualityComparer<TKey>? comparer)
            : this(concurrencyLevel, collection, comparer?.ToNullMaybeEqualityComparer())
        {
        }
        
        public NullableConcurrentDictionary(Int32 concurrencyLevel, IEnumerable<KeyValuePair<NullMaybe<TKey>, TValue>> collection, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(concurrencyLevel, collection, comparer)
        {
        }
        
        public NullableConcurrentDictionary(Int32 concurrencyLevel, Int32 capacity)
            : base(concurrencyLevel, capacity)
        {
        }
        
        public NullableConcurrentDictionary(Int32 concurrencyLevel, Int32 capacity, IEqualityComparer<TKey>? comparer)
            : this(concurrencyLevel, capacity, comparer?.ToNullMaybeEqualityComparer())
        {
        }
        
        public NullableConcurrentDictionary(Int32 concurrencyLevel, Int32 capacity, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(concurrencyLevel, capacity, comparer)
        {
        }
        
        private static KeyValuePair<NullMaybe<TKey>, TValue> Convert(KeyValuePair<TKey, TValue> item)
        {
            return new KeyValuePair<NullMaybe<TKey>, TValue>(item.Key, item.Value);
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<NullMaybe<TKey>, TValue>) this).Contains(new KeyValuePair<NullMaybe<TKey>, TValue>(item.Key, item.Value));
        }
        
        public Boolean ContainsKey(TKey key)
        {
            return base.ContainsKey(key);
        }
        
        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return base.TryGetValue(key, out value);
        }
        
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<NullMaybe<TKey>, TValue>) this).Add(new KeyValuePair<NullMaybe<TKey>, TValue>(item.Key, item.Value));
        }
        
        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            ((IDictionary<NullMaybe<TKey>, TValue>) this).Add(key, value);
        }
        
        public Boolean TryAdd(TKey key, TValue value)
        {
            return base.TryAdd(key, value);
        }
        
        public Boolean TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
        {
            return base.TryUpdate(key, newValue, comparisonValue);
        }
        
        public TValue GetOrAdd(TKey key, TValue value)
        {
            return base.GetOrAdd(key, value);
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            return base.GetOrAdd(key, static (@null, factory) => factory(@null), valueFactory);
        }

        public TValue GetOrAdd<TArg>(TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
        {
            return base.GetOrAdd(key, static (@null, argument) => argument.Factory(@null, argument.Argument), (Factory: valueFactory, Argument: factoryArgument));
        }
        
        public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            return base.AddOrUpdate(key, addValue, (@null, value) => updateValueFactory(@null, value));
        }
        
        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            return base.AddOrUpdate(key, static (@null, argument) => argument.Value(@null), static (@null, value, argument) => argument.Update(@null, value), (Value: addValueFactory, Update: updateValueFactory));
        }
        
        public TValue AddOrUpdate<TArg>(TKey key, Func<TKey, TArg, TValue> addValueFactory, Func<TKey, TValue, TArg, TValue> updateValueFactory, TArg factoryArgument)
        {
            return base.AddOrUpdate(key, static (@null, argument) => argument.Value(@null, argument.Argument), static (@null, value, argument) => argument.Update(@null, value, argument.Argument), (Value: addValueFactory, Update: updateValueFactory, Argument: factoryArgument));
        }
        
        public Boolean TryRemove(KeyValuePair<TKey, TValue> item)
        {
            return base.TryRemove(new KeyValuePair<NullMaybe<TKey>, TValue>(item.Key, item.Value));
        }
        
        public Boolean TryRemove(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return base.TryRemove(key, out value);
        }
        
        Boolean ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<NullMaybe<TKey>, TValue>) this).Remove(new KeyValuePair<NullMaybe<TKey>, TValue>(item.Key, item.Value));
        }
        
        Boolean IDictionary<TKey, TValue>.Remove(TKey key)
        {
            return ((IDictionary<NullMaybe<TKey>, TValue>) this).Remove(key);
        }
        
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            CollectionUtilities.CopyTo(this, array, arrayIndex);
        }
        
        public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach ((NullMaybe<TKey> key, TValue? value) in (IEnumerable<KeyValuePair<NullMaybe<TKey>, TValue>>) this)
            {
                yield return new KeyValuePair<TKey, TValue>(key, value);
            }
        }
        
        public TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }
}