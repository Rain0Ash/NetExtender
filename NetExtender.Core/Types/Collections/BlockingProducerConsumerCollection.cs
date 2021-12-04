// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Collections
{
    public sealed class BlockingProducerConsumerCollection<T> : IProducerConsumerCollection<T>
    {
        private BlockingCollection<T> Internal { get; }
        private TimeSpan Timeout { get; }
        private CancellationToken Token { get; }
        
        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Internal).IsSynchronized;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Internal).SyncRoot;
            }
        }

        public BlockingProducerConsumerCollection(BlockingCollection<T> collection)
            : this(collection, System.Threading.Timeout.InfiniteTimeSpan, CancellationToken.None)
        {
        }
        
        public BlockingProducerConsumerCollection(BlockingCollection<T> collection, Int32 timeout)
            : this(collection, timeout, CancellationToken.None)
        {
        }

        public BlockingProducerConsumerCollection(BlockingCollection<T> collection, Int32 timeout, CancellationToken token)
            : this(collection, TimeSpan.FromMilliseconds(timeout), token)
        {
        }

        public BlockingProducerConsumerCollection(BlockingCollection<T> collection, TimeSpan timeout)
            : this(collection, timeout, CancellationToken.None)
        {
        }

        public BlockingProducerConsumerCollection(BlockingCollection<T> collection, TimeSpan timeout, CancellationToken token)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Timeout = timeout;
            Token = token;
        }

        public Boolean TryAdd(T item)
        {
            return Internal.TryAdd(item, Timeout, Token);
        }

        public Boolean TryTake([MaybeNullWhen(false)] out T item)
        {
            return Internal.TryTake(out item, Timeout, Token);
        }
        
        public void CopyTo(Array array, Int32 index)
        {
            ((ICollection) Internal).CopyTo(array, index);
        }
        
        public void CopyTo(T[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public T[] ToArray()
        {
            return Internal.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) Internal).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}