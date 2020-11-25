// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Runtime.Serialization;
using DynamicData.Annotations;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    public class FixedSortedSet<T> : SortedSet<T>, ISet
    {
        public FixedSortedSet()
        {
        }

        public FixedSortedSet([CanBeNull] IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public FixedSortedSet([NotNull] IEnumerable<T> collection)
            : base(collection)
        {
        }

        public FixedSortedSet([NotNull] IEnumerable<T> collection, [CanBeNull] IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected FixedSortedSet([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}