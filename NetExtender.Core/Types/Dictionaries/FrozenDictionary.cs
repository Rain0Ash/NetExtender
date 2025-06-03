using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Enumerators;

namespace NetExtender.Types.Dictionaries
{
#if NET8_0_OR_GREATER
    public sealed class FrozenHashDictionary<TKey, TValue> : FrozenDictionary<TKey, TValue, System.Collections.Frozen.FrozenDictionary<TKey, TValue>, Dictionary<TKey, TValue>>, IHashDictionary<TKey, TValue> where TKey : notnull
    {
        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                return Internal.Comparer;
            }
        }

        public FrozenHashDictionary()
            : this(Array.Empty<KeyValuePair<TKey, TValue>>(), null)
        {
        }

        public FrozenHashDictionary(IEqualityComparer<TKey>? comparer)
            : this(Array.Empty<KeyValuePair<TKey, TValue>>(), comparer)
        {
        }

        public FrozenHashDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
            : this(collection, null)
        {
        }

        public FrozenHashDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer)
            : base(collection is not null ? System.Collections.Frozen.FrozenDictionary.ToFrozenDictionary(collection, comparer) : throw new ArgumentNullException(nameof(collection)), true)
        {
        }

        protected override System.Collections.Frozen.FrozenDictionary<TKey, TValue> Switch(Dictionary<TKey, TValue> dictionary)
        {
            return System.Collections.Frozen.FrozenDictionary.ToFrozenDictionary(dictionary, dictionary.Comparer);
        }

        protected override Dictionary<TKey, TValue> Switch(System.Collections.Frozen.FrozenDictionary<TKey, TValue> dictionary)
        {
            return new Dictionary<TKey, TValue>(dictionary, dictionary.Comparer);
        }

        public System.Collections.Frozen.FrozenDictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            Freeze();
            return Internal.GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
#endif
    
    public abstract class FrozenSortedDictionary<TKey, TValue, TDictionary> : FrozenDictionary<TKey, TValue, TDictionary, SortedDictionaryCollection<TKey, TValue>>, ISortedDictionary<TKey, TValue>, IReadOnlySortedDictionary<TKey, TValue> where TKey : notnull where TDictionary : class, ISortedDictionary<TKey, TValue>
    {
        protected override ISortedDictionary<TKey, TValue> Dictionary
        {
            get
            {
                return IsFrozen ? Internal : Modify!;
            }
        }

        public virtual IComparer<TKey> Comparer
        {
            get
            {
                return Dictionary.Comparer;
            }
        }

        protected FrozenSortedDictionary(TDictionary dictionary, Boolean freezable)
            : base(dictionary, freezable)
        {
        }

        protected override SortedDictionaryCollection<TKey, TValue> Switch(TDictionary set)
        {
            return new SortedDictionaryCollection<TKey, TValue>(set, set.Comparer);
        }
    }
    
    public abstract class FrozenHashDictionary<TKey, TValue, TDictionary> : FrozenDictionary<TKey, TValue, TDictionary, DictionaryCollection<TKey, TValue>>, IHashDictionary<TKey, TValue> where TKey : notnull where TDictionary : class, IHashDictionary<TKey, TValue>
    {
        protected override IHashDictionary<TKey, TValue> Dictionary
        {
            get
            {
                return IsFrozen ? Internal : Modify!;
            }
        }

        public virtual IEqualityComparer<TKey> Comparer
        {
            get
            {
                return Dictionary.Comparer;
            }
        }

        protected FrozenHashDictionary(TDictionary dictionary, Boolean freezable)
            : base(dictionary, freezable)
        {
        }

        protected override DictionaryCollection<TKey, TValue> Switch(TDictionary dictionary)
        {
            return new DictionaryCollection<TKey, TValue>(dictionary, dictionary.Comparer);
        }
    }
    
    public abstract class FrozenDictionary<TKey, TValue, TDictionary, TModify> : IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue> where TDictionary : class, IDictionary<TKey, TValue> where TModify : class, IDictionary<TKey, TValue>
    {
        protected TDictionary Internal { get; private set; }
        protected TModify? Modify { get; private set; }

        protected virtual IDictionary<TKey, TValue> Dictionary
        {
            get
            {
                return IsFrozen ? Internal : Modify!;
            }
        }

        public virtual Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return Dictionary.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return Dictionary.Values;
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

        private readonly DictionaryKeyCollection<TKey, TValue> _keys;
        ICollection IDictionary.Keys
        {
            get
            {
                return _keys;
            }
        }

        private readonly DictionaryValueCollection<TKey, TValue> _values;
        ICollection IDictionary.Values
        {
            get
            {
                return _values;
            }
        }

        Boolean IDictionary.IsFixedSize
        {
            get
            {
                return (Internal as IDictionary)?.IsFixedSize ?? false;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return (Internal as ICollection)?.IsSynchronized ?? false;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return (Internal as ICollection)?.SyncRoot ?? Internal;
            }
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        Boolean IDictionary.IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        private Boolean? _frozen;
        public Boolean IsFrozen
        {
            get
            {
                return _frozen is true;
            }
        }

        protected Boolean CanRead
        {
            get
            {
                return _frozen is not false;
            }
        }

        protected Boolean CanWrite
        {
            get
            {
                return _frozen is not true && !Internal.IsReadOnly;
            }
        }

        protected FrozenDictionary(TDictionary dictionary, Boolean freezable)
        {
            Internal = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            _keys = new DictionaryKeyCollection<TKey, TValue>(this);
            _values = new DictionaryValueCollection<TKey, TValue>(this);
            _frozen = freezable ? true : null;
        }

        protected abstract TDictionary Switch(TModify dictionary);
        protected abstract TModify Switch(TDictionary dictionary);

        public void Freeze()
        {
            switch (_frozen)
            {
                case null:
                case true:
                    return;
                case false:
                    _frozen = true;

                    if (Modify is { } modify)
                    {
                        Internal = Switch(modify);
                    }
                    else
                    {
                        Internal.Clear();
                    }

                    Modify?.Clear();
                    return;
            }
        }

        public void Unfreeze()
        {
            switch (_frozen)
            {
                case null:
                case false:
                    return;
                case true:
                    _frozen = false;
                    Modify = Switch(Internal);
                    Internal.Clear();
                    return;
            }
        }

        Boolean IDictionary.Contains(Object key)
        {
            if (key is not TKey convert)
            {
                throw new ArgumentException($"The key '{key}' is not of type '{typeof(TKey).Name}'.", nameof(key));
            }

            return ContainsKey(convert);
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return Dictionary.Contains(item);
        }

        public Boolean ContainsKey(TKey key)
        {
            return Dictionary.ContainsKey(key);
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return Dictionary.TryGetValue(key, out value);
        }

        void IDictionary.Add(Object key, Object? value)
        {
            if (key is not TKey ckey)
            {
                throw new ArgumentException($"The key '{key}' is not of type '{typeof(TKey).Name}'.", nameof(key));
            }
            
            if (value is not TValue cvalue)
            {
                throw new ArgumentException($"The value '{value}' is not of type '{typeof(TValue).Name}'.", nameof(value));
            }
            
            Add(ckey, cvalue);
        }

        public void Add(TKey key, TValue value)
        {
            Unfreeze();
            Dictionary.Add(key, value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Unfreeze();
            Dictionary.Add(item);
        }

        void IDictionary.Remove(Object key)
        {
            if (key is not TKey convert)
            {
                throw new ArgumentException($"The key '{key}' is not of type '{typeof(TKey).Name}'.", nameof(key));
            }
            
            Remove(convert);
        }

        public Boolean Remove(TKey key)
        {
            Unfreeze();
            return Dictionary.Remove(key);
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            Unfreeze();
            return Dictionary.Remove(item);
        }
        
        public void Clear()
        {
            Unfreeze();
            Dictionary.Clear();
        }

        public virtual void CopyTo(Array array, Int32 index)
        {
            switch (Dictionary)
            {
                case ICollection collection:
                    collection.CopyTo(array, index);
                    return;
                case not null when array is KeyValuePair<TKey, TValue>[] convert:
                    CopyTo(convert, index);
                    return;
                case not null when array is TKey[] convert:
                    Keys.CopyTo(convert, index);
                    return;
                case not null when array is TValue[] convert:
                    Values.CopyTo(convert, index);
                    return;
                default:
                    throw new ArgumentException($"Array must be of type '{typeof(KeyValuePair<TKey, TValue>[]).Name}'.", nameof(array));
            }
        }
        
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 index)
        {
            Dictionary.CopyTo(array, index);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new DictionaryEnumerator<TKey, TValue>(Dictionary.GetEnumerator());
        }

        public override String ToString()
        {
            return Dictionary.ToString() ?? String.Empty;
        }

        Object? IDictionary.this[Object key]
        {
            get
            {
                if (key is not TKey convert)
                {
                    throw new ArgumentException($"The key '{key}' is not of type '{typeof(TKey).Name}'.", nameof(key));
                }

                return this[convert];
            }
            set
            {
                if (key is not TKey ckey)
                {
                    throw new ArgumentException($"The key '{key}' is not of type '{typeof(TKey).Name}'.", nameof(key));
                }
                
                if (value is not TValue cvalue)
                {
                    throw new ArgumentException($"The value '{value}' is not of type '{typeof(TValue).Name}'.", nameof(value));
                }

                this[ckey] = cvalue;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return Dictionary[key];
            }
            set
            {
                Unfreeze();
                Dictionary[key] = value;
            }
        }
    }
}