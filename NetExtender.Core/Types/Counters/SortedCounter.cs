// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Counters
{
    public class SortedCounter<T> : SortedCounter<T, Int32>
    {
        public SortedCounter()
        {
        }

        public SortedCounter(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public SortedCounter(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public SortedCounter(IEnumerable<KeyValuePair<T, Int32>> collection)
            : base(collection)
        {
        }

        public SortedCounter(IEnumerable<T> collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public SortedCounter(IEnumerable<KeyValuePair<T, Int32>> collection, IComparer<T>? comparer)
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
    
    public class SortedCounter64<T> : SortedCounter<T, Int64>
    {
        public SortedCounter64()
        {
        }

        public SortedCounter64(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public SortedCounter64(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public SortedCounter64(IEnumerable<KeyValuePair<T, Int64>> collection)
            : base(collection)
        {
        }

        public SortedCounter64(IEnumerable<T> collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public SortedCounter64(IEnumerable<KeyValuePair<T, Int64>> collection, IComparer<T>? comparer)
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
    
    public class DecimalSortedCounter<T> : SortedCounter<T, Decimal>
    {
        public DecimalSortedCounter()
        {
        }

        public DecimalSortedCounter(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public DecimalSortedCounter(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public DecimalSortedCounter(IEnumerable<KeyValuePair<T, Decimal>> collection)
            : base(collection)
        {
        }

        public DecimalSortedCounter(IEnumerable<T> collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public DecimalSortedCounter(IEnumerable<KeyValuePair<T, Decimal>> collection, IComparer<T>? comparer)
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
    
    public class SortedCounter<T, TCount> : CounterAbstraction<T, TCount> where TCount : unmanaged, IConvertible
    {
        protected sealed override NullableSortedDictionary<T, TCount> Internal { get; }

        public IComparer<T> Comparer
        {
            get
            {
                return Internal.KeyComparer;
            }
        }

        public IComparer<NullMaybe<T>> NullComparer
        {
            get
            {
                return Internal.Comparer;
            }
        }

        public SortedCounter()
        {
            Internal = new NullableSortedDictionary<T, TCount>();
        }

        public SortedCounter(IComparer<T>? comparer)
        {
            Internal = new NullableSortedDictionary<T, TCount>(comparer);
        }

        public SortedCounter(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new NullableSortedDictionary<T, TCount>();
            AddRange(collection);
        }

        public SortedCounter(IEnumerable<KeyValuePair<T, TCount>> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new NullableSortedDictionary<T, TCount>();
            AddRange(collection);
        }

        public SortedCounter(IEnumerable<T> collection, IComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new NullableSortedDictionary<T, TCount>(comparer);
            AddRange(collection);
        }

        public SortedCounter(IEnumerable<KeyValuePair<T, TCount>> collection, IComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new NullableSortedDictionary<T, TCount>(comparer);
            AddRange(collection);
        }
        
        public sealed override KeyValuePair<T, TCount>[] ToArray()
        {
            return base.ToArray();
        }
    }
}