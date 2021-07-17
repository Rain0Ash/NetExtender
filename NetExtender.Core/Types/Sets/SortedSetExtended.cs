// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    [Serializable]
    public class SortedSetExtended<T> : SortedSet<T>, ISet
    {
        public SortedSetExtended()
        {
        }

        public SortedSetExtended(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public SortedSetExtended(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public SortedSetExtended(IEnumerable<T> collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected SortedSetExtended(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}