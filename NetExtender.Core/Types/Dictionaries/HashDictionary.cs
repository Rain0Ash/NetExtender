using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Dictionaries
{
    [Serializable]
    public class HashDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IHashDictionary<TKey, TValue>, IReadOnlyHashDictionary<TKey, TValue> where TKey : notnull
    {
        public HashDictionary()
        {
        }

        public HashDictionary(IEqualityComparer<TKey>? comparer)
            : base(comparer)
        {
        }

        public HashDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
        }

        public HashDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
            : base(dictionary, comparer)
        {
        }

        public HashDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
            : base(collection)
        {
        }

        public HashDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer)
            : base(collection, comparer)
        {
        }

        public HashDictionary(Int32 capacity)
            : base(capacity)
        {
        }

        public HashDictionary(Int32 capacity, IEqualityComparer<TKey>? comparer)
            : base(capacity, comparer)
        {
        }

        protected HashDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}