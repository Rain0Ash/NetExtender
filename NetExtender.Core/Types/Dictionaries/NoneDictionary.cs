using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Collections;
using NetExtender.Types.Enumerators;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Dictionaries
{
    public sealed class NoneDictionary : NoneDictionary<Object?, Object?>
    {
        public new static NoneDictionary Empty { get; } = new NoneDictionary();
    }
    
    public class NoneDictionary<TKey, TValue> : NoneDictionary<TKey, TValue, NoneCollection<KeyValuePair<TKey, TValue>>>
    {
        public static NoneDictionary<TKey, TValue> Empty { get; } = new NoneDictionary<TKey, TValue>();

        public NoneDictionary()
            : base(new NoneCollection<KeyValuePair<TKey, TValue>>())
        {
        }

        public sealed override ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
    }
    
    public abstract class NoneDictionary<TKey, TValue, TCollection> : IDictionary, IReadOnlyDictionary<TKey, TValue> where TCollection : class, ICollection, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
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

        public NoneCollection<TKey> Keys
        {
            get
            {
                return NoneCollection<TKey>.Empty;
            }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return Keys;
            }
        }

        public NoneCollection<TValue> Values
        {
            get
            {
                return NoneCollection<TValue>.Empty;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values;
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                return Values;
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
                return Internal.SyncRoot;
            }
        }

        public Boolean IsFixedSize
        {
            get
            {
                return true;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return true;
            }
        }

        protected NoneDictionary(TCollection collection)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        Boolean IDictionary.Contains(Object key)
        {
            return false;
        }

        public Boolean ContainsKey(TKey key)
        {
            return false;
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            value = default;
            return false;
        }

        void IDictionary.Add(Object key, Object? value)
        {
            throw new ReadOnlyException();
        }

        void IDictionary.Remove(Object key)
        {
            throw new ReadOnlyException();
        }

        void IDictionary.Clear()
        {
        }

        public void CopyTo(Array array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public abstract ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator GetEnumerator();

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
            return new DictionaryEnumerator<TKey, TValue>(GetEnumerator());
        }

        public TValue this[TKey key]
        {
            get
            {
                throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
            }
        }

        Object? IDictionary.this[Object key]
        {
            get
            {
                throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
            }
            set
            {
                throw new ReadOnlyException();
            }
        }
    }
}