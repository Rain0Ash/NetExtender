// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NetExtender.Types.Counters;

namespace NetExtender.Types.Concurrent.Counters
{
    public class ConcurrentCounter<T> : ConcurrentCounter<T, Int32> where T : notnull
    {
        public ConcurrentCounter()
        {
        }

        public ConcurrentCounter(Int32 concurrencyLevel, Int32 capacity)
            : base(concurrencyLevel, capacity)
        {
        }

        public ConcurrentCounter(IEqualityComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ConcurrentCounter(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ConcurrentCounter(IEnumerable<KeyValuePair<T, Int32>> collection)
            : base(collection)
        {
        }

        public ConcurrentCounter(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public ConcurrentCounter(IEnumerable<KeyValuePair<T, Int32>> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public ConcurrentCounter(Int32 concurrencyLevel, IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(concurrencyLevel, collection, comparer)
        {
        }

        public ConcurrentCounter(Int32 concurrencyLevel, IEnumerable<KeyValuePair<T, Int32>> collection, IEqualityComparer<T>? comparer)
            : base(concurrencyLevel, collection, comparer)
        {
        }

        protected sealed override Boolean LessOrEquals(Int32 value, Int32 count)
        {
            return value <= count;
        }

        protected sealed override Boolean GreaterOrEquals(Int32 value, Int32 count)
        {
            return value >= count;
        }

        protected sealed override Int32 Increment(Int32 value)
        {
            return ++value;
        }

        protected sealed override Int32 Decrement(Int32 value)
        {
            return --value;
        }

        protected sealed override Int32 Add(Int32 left, Int32 right)
        {
            return left + right;
        }

        protected sealed override Int32 Substract(Int32 left, Int32 right)
        {
            return left - right;
        }
    }
    
    public class ConcurrentCounter64<T> : ConcurrentCounter<T, Int64> where T : notnull
    {
        public ConcurrentCounter64()
        {
        }

        public ConcurrentCounter64(Int32 concurrencyLevel, Int32 capacity)
            : base(concurrencyLevel, capacity)
        {
        }

        public ConcurrentCounter64(IEqualityComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ConcurrentCounter64(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ConcurrentCounter64(IEnumerable<KeyValuePair<T, Int64>> collection)
            : base(collection)
        {
        }

        public ConcurrentCounter64(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public ConcurrentCounter64(IEnumerable<KeyValuePair<T, Int64>> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public ConcurrentCounter64(Int32 concurrencyLevel, IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(concurrencyLevel, collection, comparer)
        {
        }

        public ConcurrentCounter64(Int32 concurrencyLevel, IEnumerable<KeyValuePair<T, Int64>> collection, IEqualityComparer<T>? comparer)
            : base(concurrencyLevel, collection, comparer)
        {
        }

        protected sealed override Boolean LessOrEquals(Int64 value, Int64 count)
        {
            return value <= count;
        }

        protected sealed override Boolean GreaterOrEquals(Int64 value, Int64 count)
        {
            return value >= count;
        }

        protected sealed override Int64 Increment(Int64 value)
        {
            return ++value;
        }

        protected sealed override Int64 Decrement(Int64 value)
        {
            return --value;
        }

        protected sealed override Int64 Add(Int64 left, Int64 right)
        {
            return left + right;
        }

        protected sealed override Int64 Substract(Int64 left, Int64 right)
        {
            return left - right;
        }
    }
    
    public class ConcurrentDecimalCounter<T> : ConcurrentCounter<T, Decimal> where T : notnull
    {
        public ConcurrentDecimalCounter()
        {
        }

        public ConcurrentDecimalCounter(Int32 concurrencyLevel, Int32 capacity)
            : base(concurrencyLevel, capacity)
        {
        }

        public ConcurrentDecimalCounter(IEqualityComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ConcurrentDecimalCounter(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ConcurrentDecimalCounter(IEnumerable<KeyValuePair<T, Decimal>> collection)
            : base(collection)
        {
        }

        public ConcurrentDecimalCounter(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public ConcurrentDecimalCounter(IEnumerable<KeyValuePair<T, Decimal>> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public ConcurrentDecimalCounter(Int32 concurrencyLevel, IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(concurrencyLevel, collection, comparer)
        {
        }

        public ConcurrentDecimalCounter(Int32 concurrencyLevel, IEnumerable<KeyValuePair<T, Decimal>> collection, IEqualityComparer<T>? comparer)
            : base(concurrencyLevel, collection, comparer)
        {
        }

        protected sealed override Boolean LessOrEquals(Decimal value, Decimal count)
        {
            return value <= count;
        }

        protected sealed override Boolean GreaterOrEquals(Decimal value, Decimal count)
        {
            return value >= count;
        }

        protected sealed override Decimal Increment(Decimal value)
        {
            return ++value;
        }

        protected sealed override Decimal Decrement(Decimal value)
        {
            return --value;
        }

        protected sealed override Decimal Add(Decimal left, Decimal right)
        {
            return left + right;
        }

        protected sealed override Decimal Substract(Decimal left, Decimal right)
        {
            return left - right;
        }
    }
    
    public class ConcurrentCounter<T, TCount> : CounterAbstraction<T, TCount> where T : notnull where TCount : unmanaged, IConvertible
    {
        protected sealed override ConcurrentDictionary<T, TCount> Internal { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Internal.IsEmpty;
            }
        }

        public ConcurrentCounter()
        {
            Internal = new ConcurrentDictionary<T, TCount>();
        }

        public ConcurrentCounter(Int32 concurrencyLevel, Int32 capacity)
        {
            Internal = new ConcurrentDictionary<T, TCount>(concurrencyLevel, capacity);
        }

        public ConcurrentCounter(IEqualityComparer<T>? comparer)
        {
            Internal = new ConcurrentDictionary<T, TCount>(comparer);
        }

        public ConcurrentCounter(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new ConcurrentDictionary<T, TCount>();
            AddRange(collection);
        }

        public ConcurrentCounter(IEnumerable<KeyValuePair<T, TCount>> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new ConcurrentDictionary<T, TCount>();
            AddRange(collection);
        }

        public ConcurrentCounter(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new ConcurrentDictionary<T, TCount>(comparer);
            AddRange(collection);
        }

        public ConcurrentCounter(IEnumerable<KeyValuePair<T, TCount>> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new ConcurrentDictionary<T, TCount>(comparer);
            AddRange(collection);
        }

        public ConcurrentCounter(Int32 concurrencyLevel, IEnumerable<T> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new ConcurrentDictionary<T, TCount>(concurrencyLevel, new Counter<T, TCount>(collection, comparer), comparer);
        }

        public ConcurrentCounter(Int32 concurrencyLevel, IEnumerable<KeyValuePair<T, TCount>> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new ConcurrentDictionary<T, TCount>(concurrencyLevel, new Counter<T, TCount>(collection, comparer), comparer);
        }

        public sealed override KeyValuePair<T, TCount>[] ToArray()
        {
            return Internal.ToArray();
        }
    }
}