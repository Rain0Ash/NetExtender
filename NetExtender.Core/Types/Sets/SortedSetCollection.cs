// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    [Serializable]
    public class SortedSetCollection<T> : SortedSet<T>, ISet
    {
        public SortedSetCollection()
        {
        }

        public SortedSetCollection(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public SortedSetCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public SortedSetCollection(IEnumerable<T> collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected SortedSetCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}