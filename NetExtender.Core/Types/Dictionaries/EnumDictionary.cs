// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Comparers;

namespace NetExtender.Types.Dictionaries
{
    public class EnumDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : unmanaged, Enum
    {
        public EnumDictionary()
            : base(new EnumEqualityComparer<TKey>())
        {
        }

        public EnumDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary, new EnumEqualityComparer<TKey>())
        {
        }

        public EnumDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
            : base(collection, new EnumEqualityComparer<TKey>())
        {
        }

        public EnumDictionary(Int32 capacity)
            : base(capacity, new EnumEqualityComparer<TKey>())
        {
        }
    }
}