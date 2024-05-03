using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Types.Counters;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Immutable.Bags
{
    /*public class ImmutableCompressionBagAbstraction<T, TCount, TBag> : ImmutableCompressionBagAbstraction<TCount>, IList<T>, IList, IImmutableList<T> where T : notnull where TCount : unmanaged, IConvertible
    {
        protected TCount Value { get; set; }
        public ImmutableSortedDictionary<T, UInt64>? Constraints { get; }
        
        private ImmutableList<T>? _bag;
        public ImmutableList<T> Bag
        {
            get
            {
                return _bag ??= Build();
            }
        }

        protected virtual ImmutableList<T> Build()
        {
        }

        public sealed class Builder : IDictionary<T, UInt64>, IReadOnlyDictionary<T, UInt64>, IDictionary
        {
            private Counter<T> Internal { get; }
            public ImmutableSortedDictionary<T, UInt64>.Builder? Constraints { get; set; }


            internal Builder(ImmutableCompressionBagAbstraction<,,> map)
            {
                if (map is null)
                {
                    throw new ArgumentNullException(nameof(map));
                }
            }

            Object? IDictionary.this[Object key]
            {
                get
                {
                    return ((IDictionary) Internal)[key];
                }
                set
                {
                    ((IDictionary) Internal)[key] = value;
                }
            }
        }
    }*/
}