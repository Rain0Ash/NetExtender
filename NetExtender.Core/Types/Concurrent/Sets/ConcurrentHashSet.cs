// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Collections.Concurrent
{
    //TODO: refactoring
    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    public class ConcurrentHashSet<T> : IReadOnlyCollection<T>, ICollection<T>
    {
        private const Int32 DefaultCapacity = 31;
        private const Int32 MaxLockNumber = 1024;

        private readonly Boolean _growLockArray;

        private Int32 _budget;
        private volatile Tables _tables;

        private static Int32 DefaultConcurrencyLevel
        {
            get
            {
                return Environment.ProcessorCount;
            }
        }

        /// <summary>
        /// Gets the <see cref="IEqualityComparer{T}" />
        /// that is used to determine equality for the values in the set.
        /// </summary>
        /// <value>
        /// The <see cref="IEqualityComparer{T}" /> generic interface implementation that is used to 
        /// provide hash values and determine equality for the values in the current <see cref="ConcurrentHashSet{T}" />.
        /// </value>
        /// <remarks>
        /// <see cref="ConcurrentHashSet{T}" /> requires an equality implementation to determine
        /// whether values are equal. You can specify an implementation of the <see cref="IEqualityComparer{T}" />
        /// generic interface by using a constructor that accepts a comparer parameter;
        /// if you do not specify one, the default generic equality comparer <see cref="EqualityComparer{T}.Default" /> is used.
        /// </remarks>
        public IEqualityComparer<T> Comparer { get; }

        /// <summary>
        /// Gets the number of items contained in the <see
        /// cref="ConcurrentHashSet{T}"/>.
        /// </summary>
        /// <value>The number of items contained in the <see
        /// cref="ConcurrentHashSet{T}"/>.</value>
        /// <remarks>Count has snapshot semantics and represents the number of items in the <see
        /// cref="ConcurrentHashSet{T}"/>
        /// at the moment when Count was accessed.</remarks>
        public Int32 Count
        {
            get
            {
                Int32 count = 0;
                Int32 acquired = 0;
                
                try
                {
                    AcquireAllLocks(ref acquired);

                    Int32[] locks = _tables.CountPerLock;
                    
                    for (Int32 i = 0; i < locks.Length; i++)
                    {
                        count += locks[i];
                    }
                }
                finally
                {
                    ReleaseLocks(0, acquired);
                }

                return count;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="ConcurrentHashSet{T}"/> is empty.
        /// </summary>
        /// <value>true if the <see cref="ConcurrentHashSet{T}"/> is empty; otherwise,
        /// false.</value>
        public Boolean IsEmpty
        {
            get
            {
                if (!IsBucketsEmpty())
                {
                    return false;
                }

                Int32 acquired = 0;
                
                try
                {
                    AcquireAllLocks(ref acquired);
                    return IsBucketsEmpty();
                }
                finally
                {
                    ReleaseLocks(0, acquired);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="ConcurrentHashSet{T}"/>
        /// class that is empty, has the default concurrency level, has the default initial capacity, and
        /// uses the default comparer for the item type.
        /// </summary>
        public ConcurrentHashSet()
            : this(DefaultConcurrencyLevel, DefaultCapacity, true, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="ConcurrentHashSet{T}"/>
        /// class that is empty, has the specified concurrency level and capacity, and uses the default
        /// comparer for the item type.
        /// </summary>
        /// <param name="concurrency">The estimated number of threads that will update the
        /// <see cref="ConcurrentHashSet{T}"/> concurrently.</param>
        /// <param name="capacity">The initial number of elements that the <see
        /// cref="ConcurrentHashSet{T}"/>
        /// can contain.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="concurrency"/> is
        /// less than 1.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> <paramref name="capacity"/> is less than
        /// 0.</exception>
        public ConcurrentHashSet(Int32 concurrency, Int32 capacity)
            : this(concurrency, capacity, false, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentHashSet{T}"/>
        /// class that contains elements copied from the specified <see
        /// cref="IEnumerable{T}"/>, has the default concurrency
        /// level, has the default initial capacity, and uses the default comparer for the item type.
        /// </summary>
        /// <param name="collection">The <see
        /// cref="IEnumerable{T}"/> whose elements are copied to
        /// the new
        /// <see cref="ConcurrentHashSet{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is a null reference.</exception>
        public ConcurrentHashSet(IEnumerable<T> collection)
            : this(collection, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentHashSet{T}"/>
        /// class that is empty, has the specified concurrency level and capacity, and uses the specified
        /// <see cref="IEqualityComparer{T}"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="IEqualityComparer{T}"/>
        /// implementation to use when comparing items.</param>
        public ConcurrentHashSet(IEqualityComparer<T>? comparer)
            : this(DefaultConcurrencyLevel, DefaultCapacity, true, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentHashSet{T}"/>
        /// class that contains elements copied from the specified <see
        /// cref="IEnumerable"/>, has the default concurrency level, has the default
        /// initial capacity, and uses the specified
        /// <see cref="IEqualityComparer{T}"/>.
        /// </summary>
        /// <param name="collection">The <see
        /// cref="IEnumerable{T}"/> whose elements are copied to
        /// the new
        /// <see cref="ConcurrentHashSet{T}"/>.</param>
        /// <param name="comparer">The <see cref="IEqualityComparer{T}"/>
        /// implementation to use when comparing items.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is a null reference
        /// (Nothing in Visual Basic).
        /// </exception>
        public ConcurrentHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : this(comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            InitializeFromCollection(collection);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentHashSet{T}"/> 
        /// class that contains elements copied from the specified <see cref="IEnumerable"/>, 
        /// has the specified concurrency level, has the specified initial capacity, and uses the specified 
        /// <see cref="IEqualityComparer{T}"/>.
        /// </summary>
        /// <param name="concurrency">The estimated number of threads that will update the 
        /// <see cref="ConcurrentHashSet{T}"/> concurrently.</param>
        /// <param name="collection">The <see cref="IEnumerable{T}"/> whose elements are copied to the new 
        /// <see cref="ConcurrentHashSet{T}"/>.</param>
        /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use 
        /// when comparing items.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="collection"/> is a null reference.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="concurrency"/> is less than 1.
        /// </exception>
        public ConcurrentHashSet(Int32 concurrency, IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : this(concurrency, DefaultCapacity, false, comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            InitializeFromCollection(collection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentHashSet{T}"/>
        /// class that is empty, has the specified concurrency level, has the specified initial capacity, and
        /// uses the specified <see cref="IEqualityComparer{T}"/>.
        /// </summary>
        /// <param name="concurrency">The estimated number of threads that will update the
        /// <see cref="ConcurrentHashSet{T}"/> concurrently.</param>
        /// <param name="capacity">The initial number of elements that the <see
        /// cref="ConcurrentHashSet{T}"/>
        /// can contain.</param>
        /// <param name="comparer">The <see cref="IEqualityComparer{T}"/>
        /// implementation to use when comparing items.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="concurrency"/> is less than 1. -or-
        /// <paramref name="capacity"/> is less than 0.
        /// </exception>
        public ConcurrentHashSet(Int32 concurrency, Int32 capacity, IEqualityComparer<T>? comparer)
            : this(concurrency, capacity, false, comparer)
        {
        }

        private ConcurrentHashSet(Int32 concurrency, Int32 capacity, Boolean growLockArray, IEqualityComparer<T>? comparer)
        {
            if (concurrency < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(concurrency));
            }

            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            // The capacity should be at least as large as the concurrency level. Otherwise, we would have locks that don't guard
            // any buckets.
            if (capacity < concurrency)
            {
                capacity = concurrency;
            }

            Object[] locks = new Object[concurrency];
            for (Int32 i = 0; i < locks.Length; i++)
            {
                locks[i] = new Object();
            }

            Int32[] countPerLock = new Int32[locks.Length];
            Node[] buckets = new Node[capacity];

            Comparer = comparer ?? EqualityComparer<T>.Default;
            _tables = new Tables(buckets, locks, countPerLock);
            _growLockArray = growLockArray;
            _budget = buckets.Length / locks.Length;
        }

        /// <summary>
        /// Adds the specified item to the <see cref="ConcurrentHashSet{T}"/>.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>true if the items was added to the <see cref="ConcurrentHashSet{T}"/>
        /// successfully; false if it already exists.</returns>
        /// <exception cref="OverflowException">The <see cref="ConcurrentHashSet{T}"/>
        /// contains too many items.</exception>
        public Boolean Add(T item)
        {
            return AddInternal(item, item is not null ? Comparer.GetHashCode(item) : 0, true, out _);
        }
        
        /// <summary>
        /// Adds the specified item to the <see cref="ConcurrentHashSet{T}"/>.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="set">The item that was added, or if the item already existed, the existing item</param>
        /// <returns>true if the items was added to the <see cref="ConcurrentHashSet{T}"/>
        /// successfully; false if it already exists.</returns>
        /// <exception cref="OverflowException">The <see cref="ConcurrentHashSet{T}"/>
        /// contains too many items.</exception>
        public Boolean Add(T item, out T set)
        {
            return AddInternal(item, item is not null ? Comparer.GetHashCode(item) : 0, true, out set);
        }

        /// <summary>
        /// Removes all items from the <see cref="ConcurrentHashSet{T}"/>.
        /// </summary>
        public void Clear()
        {
            Int32 acquired = 0;
            
            try
            {
                AcquireAllLocks(ref acquired);

                if (IsBucketsEmpty())
                {
                    return;
                }

                Tables tables = _tables;
                Tables newTables = new Tables(new Node[DefaultCapacity], tables.Locks, new Int32[tables.CountPerLock.Length]);
                _tables = newTables;
                _budget = Math.Max(1, newTables.Buckets.Length / newTables.Locks.Length);
            }
            finally
            {
                ReleaseLocks(0, acquired);
            }
        }

        /// <summary>
        /// Determines whether the <see cref="ConcurrentHashSet{T}"/> contains the specified
        /// item.
        /// </summary>
        /// <param name="item">The item to locate in the <see cref="ConcurrentHashSet{T}"/>.</param>
        /// <returns>true if the <see cref="ConcurrentHashSet{T}"/> contains the item; otherwise, false.</returns>
        public Boolean Contains(T item)
        {
            return TryGetValue(item, out _);
        }

        /// <summary>
        /// Searches the <see cref="ConcurrentHashSet{T}"/> for a given value and returns the equal value it finds, if any.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <param name="result">The value from the set that the search found, or the default value of <typeparamref name="T"/> when the search yielded no match.</param>
        /// <returns>A value indicating whether the search was successful.</returns>
        /// <remarks>
        /// This can be useful when you want to reuse a previously stored reference instead of
        /// a newly constructed one (so that more sharing of references can occur) or to look up
        /// a value that has more complete data than the value you currently have, although their
        /// comparer functions indicate they are equal.
        /// </remarks>
        public Boolean TryGetValue(T value, [MaybeNullWhen(false)] out T result)
        {
            Int32 hashcode = value is not null ? Comparer.GetHashCode(value) : 0;

            // We must capture the _buckets field in a local variable. It is set to a new table on each table resize.
            Tables tables = _tables;
            Int32 bucket = GetBucket(hashcode, tables.Buckets.Length);

            // We can get away w/out a lock here.
            // The Volatile.Read ensures that the load of the fields of 'n' doesn't move before the load from buckets[i].
            Node? current = Volatile.Read(ref tables.Buckets[bucket]);

            while (current is not null)
            {
                if (hashcode == current.Hashcode && Comparer.Equals(current.Item, value))
                {
                    result = current.Item;
                    return true;
                }

                current = current.Next;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Attempts to remove the item from the <see cref="ConcurrentHashSet{T}"/>.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>true if an item was removed successfully; otherwise, false.</returns>
        public Boolean TryRemove(T item)
        {
            Int32 hashcode = item is not null ? Comparer.GetHashCode(item) : 0;
            
            while (true)
            {
                Tables tables = _tables;

                GetBucketAndLockNo(hashcode, out Int32 bucketNo, out Int32 lockNo, tables.Buckets.Length, tables.Locks.Length);

                lock (tables.Locks[lockNo])
                {
                    // If the table just got resized, we may not be holding the right lock, and must retry.
                    // This should be a rare occurrence.
                    if (tables != _tables)
                    {
                        continue;
                    }

                    Node? previous = null;
                    for (Node? current = tables.Buckets[bucketNo]; current is not null; current = current.Next)
                    {
                        if (hashcode == current.Hashcode && Comparer.Equals(current.Item, item))
                        {
                            if (previous is null)
                            {
                                Volatile.Write(ref tables.Buckets[bucketNo], current.Next);
                            }
                            else
                            {
                                previous.Next = current.Next;
                            }

                            tables.CountPerLock[lockNo]--;
                            return true;
                        }

                        previous = current;
                    }
                }

                return false;
            }
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        void ICollection<T>.CopyTo(T[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            Int32 locksAcquired = 0;
            try
            {
                AcquireAllLocks(ref locksAcquired);

                Int32 count = 0;

                Int32[] countPerLock = _tables.CountPerLock;
                for (Int32 i = 0; i < countPerLock.Length && count >= 0; i++)
                {
                    count += countPerLock[i];
                }

                if (array.Length - count < arrayIndex || count < 0) //"count" itself or "count + arrayIndex" can overflow
                {
                    throw new ArgumentException(
                        "The index is equal to or greater than the length of the array, or the number of elements in the set is greater than the available space from index to the end of the destination array.");
                }

                CopyToItems(array, arrayIndex);
            }
            finally
            {
                ReleaseLocks(0, locksAcquired);
            }
        }

        Boolean ICollection<T>.Remove(T item)
        {
            return TryRemove(item);
        }

        private void InitializeFromCollection(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                AddInternal(item, item is not null ? Comparer.GetHashCode(item) : 0, false, out _);
            }

            if (_budget > 0)
            {
                return;
            }

            Tables tables = _tables;
            _budget = tables.Buckets.Length / tables.Locks.Length;
        }

        private Boolean AddInternal(T item, Int32 hashcode, Boolean acquire, out T set)
        {
            while (true)
            {
                Tables tables = _tables;

                GetBucketAndLockNo(hashcode, out Int32 bucketNo, out Int32 lockNo, tables.Buckets.Length, tables.Locks.Length);

                Boolean resizeDesired = false;
                Boolean lockTaken = false;
                try
                {
                    if (acquire)
                    {
                        Monitor.Enter(tables.Locks[lockNo], ref lockTaken);
                    }

                    // If the table just got resized, we may not be holding the right lock, and must retry.
                    // This should be a rare occurrence.
                    if (tables != _tables)
                    {
                        continue;
                    }

                    // Try to find this item in the bucket
                    for (Node? current = tables.Buckets[bucketNo]; current is not null; current = current.Next)
                    {
                        if (hashcode != current.Hashcode || !Comparer.Equals(current.Item, item))
                        {
                            continue;
                        }

                        set = current.Item;
                        return false;
                    }

                    // The item was not found in the bucket. Insert the new item.
                    Volatile.Write(ref tables.Buckets[bucketNo], new Node(item, hashcode, tables.Buckets[bucketNo]));
                    checked
                    {
                        tables.CountPerLock[lockNo]++;
                    }

                    //
                    // If the number of elements guarded by this lock has exceeded the budget, resize the bucket table.
                    // It is also possible that GrowTable will increase the budget but won't resize the bucket table.
                    // That happens if the bucket table is found to be poorly utilized due to a bad hash function.
                    //
                    if (tables.CountPerLock[lockNo] > _budget)
                    {
                        resizeDesired = true;
                    }
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(tables.Locks[lockNo]);
                    }
                }

                //
                // The fact that we got here means that we just performed an insertion. If necessary, we will grow the table.
                //
                // Concurrency notes:
                // - Notice that we are not holding any locks at when calling GrowTable. This is necessary to prevent deadlocks.
                // - As a result, it is possible that GrowTable will be called unnecessarily. But, GrowTable will obtain lock 0
                //   and then verify that the table we passed to it as the argument is still the current table.
                //
                if (resizeDesired)
                {
                    GrowTable(tables);
                }

                set = item;
                return true;
            }
        }

        private static Int32 GetBucket(Int32 hashcode, Int32 bucketCount)
        {
            Int32 bucketNo = (hashcode & 0x7fffffff) % bucketCount;
            return bucketNo;
        }

        private static void GetBucketAndLockNo(Int32 hashcode, out Int32 bucketNo, out Int32 lockNo, Int32 bucketCount, Int32 lockCount)
        {
            bucketNo = (hashcode & 0x7fffffff) % bucketCount;
            lockNo = bucketNo % lockCount;
        }

        private Boolean IsBucketsEmpty()
        {
            Int32[] count = _tables.CountPerLock;
            
            for (Int32 i = 0; i < count.Length; i++)
            {
                if (count[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void GrowTable(Tables tables)
        {
            Int32 acquired = 0;
            
            try
            {
                // The thread that first obtains _locks[0] will be the one doing the resize operation
                AcquireLocks(0, 1, ref acquired);

                // Make sure nobody resized the table while we were waiting for lock 0:
                if (tables != _tables)
                {
                    // We assume that since the table reference is different, it was already resized (or the budget
                    // was adjusted). If we ever decide to do table shrinking, or replace the table for other reasons,
                    // we will have to revisit this logic.
                    return;
                }

                // Compute the (approx.) total size. Use an Int64 accumulation variable to avoid an overflow.
                Int64 approximation = 0;
                
                for (Int32 i = 0; i < tables.CountPerLock.Length; i++)
                {
                    approximation += tables.CountPerLock[i];
                }

                //
                // If the bucket array is too empty, double the budget instead of resizing the table
                //
                if (approximation < tables.Buckets.Length / 4)
                {
                    _budget = 2 * _budget;
                    if (_budget < 0)
                    {
                        _budget = Int32.MaxValue;
                    }

                    return;
                }

                // Compute the new table size. We find the smallest integer larger than twice the previous table size, and not divisible by
                // 2,3,5 or 7. We can consider a different table-sizing policy in the future.
                
                const Int32 maximum = 0X7FEFFFFF;
                Int32 newLength = 0;
                Boolean maximize = false;
                try
                {
                    checked
                    {
                        // Double the size of the buckets table and add one, so that we have an odd integer.
                        newLength = tables.Buckets.Length * 2 + 1;

                        // Now, we only need to check odd integers, and find the first that is not divisible
                        // by 3, 5 or 7.
                        while (newLength % 3 == 0 || newLength % 5 == 0 || newLength % 7 == 0)
                        {
                            newLength += 2;
                        }

                        if (newLength > maximum)
                        {
                            maximize = true;
                        }
                    }
                }
                catch (OverflowException)
                {
                    maximize = true;
                }

                if (maximize)
                {
                    newLength = maximum;

                    // We want to make sure that GrowTable will not be called again, since table is at the maximum size.
                    // To achieve that, we set the budget to int.MaxValue.
                    //
                    // (There is one special case that would allow GrowTable() to be called in the future: 
                    // calling Clear() on the ConcurrentHashSet will shrink the table and lower the budget.)
                    _budget = Int32.MaxValue;
                }

                // Now acquire all other locks for the table
                AcquireLocks(1, tables.Locks.Length, ref acquired);

                Object[] newLocks = tables.Locks;

                // Add more locks
                if (_growLockArray && tables.Locks.Length < MaxLockNumber)
                {
                    newLocks = new Object[tables.Locks.Length * 2];
                    Array.Copy(tables.Locks, newLocks, tables.Locks.Length);
                    for (Int32 i = tables.Locks.Length; i < newLocks.Length; i++)
                    {
                        newLocks[i] = new Object();
                    }
                }

                Node[] newBuckets = new Node[newLength];
                Int32[] newCountPerLock = new Int32[newLocks.Length];

                // Copy all data into a new table, creating new nodes for all elements
                for (Int32 i = 0; i < tables.Buckets.Length; i++)
                {
                    Node? current = tables.Buckets[i];
                    while (current is not null)
                    {
                        Node? next = current.Next;
                        GetBucketAndLockNo(current.Hashcode, out Int32 newBucketNo, out Int32 newLockNo, newBuckets.Length, newLocks.Length);

                        newBuckets[newBucketNo] = new Node(current.Item, current.Hashcode, newBuckets[newBucketNo]);

                        checked
                        {
                            newCountPerLock[newLockNo]++;
                        }

                        current = next;
                    }
                }

                // Adjust the budget
                _budget = Math.Max(1, newBuckets.Length / newLocks.Length);

                // Replace tables with the new versions
                _tables = new Tables(newBuckets, newLocks, newCountPerLock);
            }
            finally
            {
                // Release all locks that we took earlier
                ReleaseLocks(0, acquired);
            }
        }

        private void AcquireAllLocks(ref Int32 locksAcquired)
        {
            // First, acquire lock 0
            AcquireLocks(0, 1, ref locksAcquired);

            // Now that we have lock 0, the _locks array will not change (i.e., grow),
            // and so we can safely read _locks.Length.
            AcquireLocks(1, _tables.Locks.Length, ref locksAcquired);
        }

        private void AcquireLocks(Int32 from, Int32 to, ref Int32 acquired)
        {
            Object[] locks = _tables.Locks;

            for (Int32 i = from; i < to; i++)
            {
                Boolean taken = false;
                
                try
                {
                    Monitor.Enter(locks[i], ref taken);
                }
                finally
                {
                    if (taken)
                    {
                        acquired++;
                    }
                }
            }
        }

        private void ReleaseLocks(Int32 from, Int32 to)
        {
            for (Int32 i = from; i < to; i++)
            {
                Monitor.Exit(_tables.Locks[i]);
            }
        }

        private void CopyToItems(IList<T> array, Int32 index)
        {
            Node?[] buckets = _tables.Buckets;
            
            for (Int32 i = 0; i < buckets.Length; i++)
            {
                for (Node? current = buckets[i]; current is not null; current = current.Next)
                {
                    array[index] = current.Item;
                    index++; //this should never flow, CopyToItems is only called when there's no overflow risk
                }
            }
        }

        private class Tables
        {
            public readonly Node?[] Buckets;
            public readonly Object[] Locks;

            public readonly Int32[] CountPerLock;

            public Tables(Node?[] buckets, Object[] locks, Int32[] count)
            {
                Buckets = buckets;
                Locks = locks;
                CountPerLock = count;
            }
        }

        private class Node
        {
            public readonly T Item;
            public readonly Int32 Hashcode;

            public volatile Node? Next;

            public Node(T item, Int32 hashcode, Node? next)
            {
                Item = item;
                Hashcode = hashcode;
                Next = next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through the <see
        /// cref="ConcurrentHashSet{T}"/>.</summary>
        /// <returns>An enumerator for the <see cref="ConcurrentHashSet{T}"/>.</returns>
        /// <remarks>
        /// The enumerator returned from the collection is safe to use concurrently with
        /// reads and writes to the collection, however it does not represent a moment-in-time snapshot
        /// of the collection.  The contents exposed through the enumerator may contain modifications
        /// made to the collection after <see cref="IEnumerable{T}.GetEnumerator"/> was called.
        /// </remarks>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>Returns a value-type enumerator that iterates through the <see
        /// cref="ConcurrentHashSet{T}"/>.</summary>
        /// <returns>An enumerator for the <see cref="ConcurrentHashSet{T}"/>.</returns>
        /// <remarks>
        /// The enumerator returned from the collection is safe to use concurrently with
        /// reads and writes to the collection, however it does not represent a moment-in-time snapshot
        /// of the collection.  The contents exposed through the enumerator may contain modifications
        /// made to the collection after <see cref="GetEnumerator"/> was called.
        /// </remarks>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Represents an enumerator for <see cref="ConcurrentHashSet{T}" />.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private const Int32 StateUninitialized = 0;
            private const Int32 StateOuterloop = 1;
            private const Int32 StateInnerLoop = 2;
            private const Int32 StateDone = 3;
            
            private readonly ConcurrentHashSet<T> _set;

            private Node?[]? _buckets;
            private Node? _node;
            private Int32 _i;
            private Int32 _state;

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <value>The element in the collection at the current position of the enumerator.</value>
            public T Current { get; private set; }

            readonly Object? IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            /// <summary>
            /// Constructs an enumerator for <see cref="ConcurrentHashSet{T}" />.
            /// </summary>
            public Enumerator(ConcurrentHashSet<T> set)
            {
                _set = set;
                _buckets = null;
                _node = null;
                Current = default!;
                _i = -1;
                _state = StateUninitialized;
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            public Boolean MoveNext()
            {
                switch (_state)
                {
                    case StateUninitialized:
                        _buckets = _set._tables.Buckets;
                        _i = -1;
                        goto case StateOuterloop;

                    case StateOuterloop:
                        Node?[]? buckets = _buckets;

                        if (buckets is null)
                        {
                            return false;
                        }
                        
                        Int32 i = ++_i;
                        if ((UInt32) i < (UInt32) buckets.Length)
                        {
                            _node = Volatile.Read(ref buckets[i]);
                            _state = StateInnerLoop;
                            goto case StateInnerLoop;
                        }

                        goto default;

                    case StateInnerLoop:
                        Node? node = _node;
                        if (node is not null)
                        {
                            Current = node.Item;
                            _node = node.Next;
                            return true;
                        }

                        goto case StateOuterloop;

                    default:
                        _state = StateDone;
                        return false;
                }
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                _buckets = null;
                _node = null;
                Current = default!;
                _i = -1;
                _state = StateUninitialized;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
            }
        }
    }
}