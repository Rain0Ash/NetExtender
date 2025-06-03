using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Dictionaries
{
    public class DictionaryCollection<TKey, TValue> : Dictionary<TKey, TValue>, IHashDictionary<TKey, TValue>, IReadOnlyHashDictionary<TKey, TValue> where TKey : notnull
    {
        public DictionaryCollection()
        {
        }

        public DictionaryCollection(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
        }

        public DictionaryCollection(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
            : base(dictionary, comparer)
        {
        }

        public DictionaryCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
            : base(collection)
        {
        }

        public DictionaryCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer)
            : base(collection, comparer)
        {
        }

        public DictionaryCollection(IEqualityComparer<TKey>? comparer)
            : base(comparer)
        {
        }

        public DictionaryCollection(Int32 capacity)
            : base(capacity)
        {
        }

        public DictionaryCollection(Int32 capacity, IEqualityComparer<TKey>? comparer)
            : base(capacity, comparer)
        {
        }

        protected DictionaryCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}