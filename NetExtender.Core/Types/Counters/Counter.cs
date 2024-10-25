// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Counters
{
    public class Counter<T> : Counter<T, Int32>
    {
        public Counter()
        {
        }

        public Counter(Int32 capacity)
            : base(capacity)
        {
        }

        public Counter(IEqualityComparer<T>? comparer)
            : base(comparer)
        {
        }

        public Counter(Int32 capacity, IEqualityComparer<T>? comparer)
            : base(capacity, comparer)
        {
        }

        public Counter(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public Counter(IEnumerable<KeyValuePair<T, Int32>> collection)
            : base(collection)
        {
        }

        public Counter(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public Counter(IEnumerable<KeyValuePair<T, Int32>> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected sealed override Boolean Less(Int32 value, Int32 count)
        {
            return value < count;
        }

        protected sealed override Boolean LessOrEquals(Int32 value, Int32 count)
        {
            return value <= count;
        }

        protected sealed override Boolean Greater(Int32 value, Int32 count)
        {
            return value > count;
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

        protected sealed override Int32 Subtract(Int32 left, Int32 right)
        {
            return left - right;
        }
    }
    
    public class Counter64<T> : Counter<T, Int64>
    {
        public Counter64()
        {
        }

        public Counter64(Int32 capacity)
            : base(capacity)
        {
        }

        public Counter64(IEqualityComparer<T>? comparer)
            : base(comparer)
        {
        }

        public Counter64(Int32 capacity, IEqualityComparer<T>? comparer)
            : base(capacity, comparer)
        {
        }

        public Counter64(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public Counter64(IEnumerable<KeyValuePair<T, Int64>> collection)
            : base(collection)
        {
        }

        public Counter64(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public Counter64(IEnumerable<KeyValuePair<T, Int64>> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected sealed override Boolean Less(Int64 value, Int64 count)
        {
            return value < count;
        }

        protected sealed override Boolean LessOrEquals(Int64 value, Int64 count)
        {
            return value <= count;
        }

        protected sealed override Boolean Greater(Int64 value, Int64 count)
        {
            return value > count;
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

        protected sealed override Int64 Subtract(Int64 left, Int64 right)
        {
            return left - right;
        }
    }
    
    public class DecimalCounter<T> : Counter<T, Decimal>
    {
        public DecimalCounter()
        {
        }

        public DecimalCounter(Int32 capacity)
            : base(capacity)
        {
        }

        public DecimalCounter(IEqualityComparer<T>? comparer)
            : base(comparer)
        {
        }

        public DecimalCounter(Int32 capacity, IEqualityComparer<T>? comparer)
            : base(capacity, comparer)
        {
        }

        public DecimalCounter(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public DecimalCounter(IEnumerable<KeyValuePair<T, Decimal>> collection)
            : base(collection)
        {
        }

        public DecimalCounter(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public DecimalCounter(IEnumerable<KeyValuePair<T, Decimal>> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected sealed override Boolean Less(Decimal value, Decimal count)
        {
            return value < count;
        }

        protected sealed override Boolean LessOrEquals(Decimal value, Decimal count)
        {
            return value <= count;
        }

        protected sealed override Boolean Greater(Decimal value, Decimal count)
        {
            return value > count;
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

        protected sealed override Decimal Subtract(Decimal left, Decimal right)
        {
            return left - right;
        }
    }
    
    public class Counter<T, TCount> : CounterAbstraction<T, TCount> where TCount : unmanaged, IConvertible
    {
        protected sealed override NullableDictionary<T, TCount> Internal { get; }
        
        public IEqualityComparer<T> Comparer
        {
            get
            {
                return Internal.KeyComparer;
            }
        }
        
        public IEqualityComparer<NullMaybe<T>> NullComparer
        {
            get
            {
                return Internal.Comparer;
            }
        }

        public Counter()
        {
            Internal = new NullableDictionary<T, TCount>();
        }

        public Counter(Int32 capacity)
        {
            Internal = new NullableDictionary<T, TCount>(capacity);
        }

        public Counter(IEqualityComparer<T>? comparer)
        {
            Internal = new NullableDictionary<T, TCount>(comparer);
        }

        public Counter(Int32 capacity, IEqualityComparer<T>? comparer)
        {
            Internal = new NullableDictionary<T, TCount>(capacity, comparer);
        }

        public Counter(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new NullableDictionary<T, TCount>();
            AddRange(collection);
        }

        public Counter(IEnumerable<KeyValuePair<T, TCount>> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new NullableDictionary<T, TCount>();
            AddRange(collection);
        }

        public Counter(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new NullableDictionary<T, TCount>(comparer);
            AddRange(collection);
        }

        public Counter(IEnumerable<KeyValuePair<T, TCount>> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new NullableDictionary<T, TCount>(comparer);
            AddRange(collection);
        }

        public sealed override KeyValuePair<T, TCount>[] ToArray()
        {
            return base.ToArray();
        }
    }
}