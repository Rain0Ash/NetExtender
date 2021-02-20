// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.Types.Numerics;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Sets
{
    [Serializable]
    public class RangeSet<T> : FixedSortedSet<Range<T>> where T : IComparable<T>
    {
        public RangeSet()
        {
        }

        public RangeSet(IEnumerable<Range<T>> collection)
            : base(collection)
        {
        }

        protected RangeSet(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public Boolean Add(T min, T max)
        {
            return Add(new Range<T>(min, max));
        }
        
        public Boolean Contains(T value)
        {
            return Count > 0 && this.Any(range => range.Contains(value));
        }

        public IEnumerable<Range<T>> All(T value)
        {
            return Count > 0 ? this.Where(range => range.Contains(value)) : Enumerable.Empty<Range<T>>();
        }

        public Range<T>[] FindAll(T value)
        {
            return Count > 0 ? All(value).ToArray() : Array.Empty<Range<T>>();
        }

        public Range<T> Minimum()
        {
            return this.MinBy(range => range.Min);
        }
        
        public Range<T> Maximum()
        {
            return this.MaxBy(range => range.Max);
        }
    }
}