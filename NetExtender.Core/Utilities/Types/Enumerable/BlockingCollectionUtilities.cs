// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using NetExtender.Core.Types.Observers;
using NetExtender.Types.Collections;

namespace NetExtender.Utilities.Types
{
    public static class BlockingCollectionUtilities
    {
        public static Partitioner<T> GetConsumingPartitioner<T>(this BlockingCollection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new BlockingCollectionPartitioner<T>(collection);
        }

        private class BlockingCollectionPartitioner<T> : Partitioner<T>
        {
            private BlockingCollection<T> Internal { get; }

            public override Boolean SupportsDynamicPartitions
            {
                get
                {
                    return true;
                }
            }
            
            public BlockingCollectionPartitioner(BlockingCollection<T> collection)
            {
                Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            }

            public override IList<IEnumerator<T>> GetPartitions(Int32 count)
            {
                if (count < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(count));
                }

                IEnumerable<T> partitioner = GetDynamicPartitions();
                return Enumerable.Range(0, count).Select(_ => partitioner.GetEnumerator()).ToArray();
            }

            public override IEnumerable<T> GetDynamicPartitions()
            {
                return Internal.GetConsumingEnumerable();
            }
        }

        public static Boolean TryAdd<T>(this BlockingCollection<T> collection, T item, TimeSpan timeout, CancellationToken token)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            return collection.TryAdd(item, timeout.ToTimeoutMilliseconds(), token);
        }
        
        public static Boolean TryTake<T>(this BlockingCollection<T> collection, [MaybeNullWhen(false)] out T item, TimeSpan timeout, CancellationToken token)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryTake(out item, timeout.ToTimeoutMilliseconds(), token);
        }

        public static void Add<T>(this BlockingCollection<T> collection, IEnumerable<T> source)
        {
            Add(collection, source, false);
        }

        public static void Add<T>(this BlockingCollection<T> collection, IEnumerable<T> source, Boolean complete)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                foreach (T item in source)
                {
                    collection.Add(item);
                }
            }
            finally
            {
                if (complete)
                {
                    collection.CompleteAdding();
                }
            }
        }

        public static IDisposable Subscribe<T>(this BlockingCollection<T> collection, IObservable<T> source)
        {
            return Subscribe(collection, source, false);
        }

        public static IDisposable Subscribe<T>(this BlockingCollection<T> collection, IObservable<T> source, Boolean complete)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            void Complete()
            {
                if (complete)
                {
                    collection.CompleteAdding();
                }
            }

            void Error(Exception _)
            {
                if (complete)
                {
                    collection.CompleteAdding();
                }
            }

            return source.Subscribe(new DelegateObserver<T>
            (
                Complete,
                collection.Add,
                Error
            ));
        }
        
        public static IProducerConsumerCollection<T> ToProducerConsumerCollection<T>(this BlockingCollection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new BlockingProducerConsumerCollection<T>(collection);
        }
        
        public static IProducerConsumerCollection<T> ToProducerConsumerCollection<T>(this BlockingCollection<T> collection, Int32 timeout)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new BlockingProducerConsumerCollection<T>(collection, timeout);
        }
        
        public static IProducerConsumerCollection<T> ToProducerConsumerCollection<T>(this BlockingCollection<T> collection, Int32 timeout, CancellationToken token)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new BlockingProducerConsumerCollection<T>(collection, timeout, token);
        }
        
        public static IProducerConsumerCollection<T> ToProducerConsumerCollection<T>(this BlockingCollection<T> collection, TimeSpan timeout)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new BlockingProducerConsumerCollection<T>(collection, timeout);
        }
        
        public static IProducerConsumerCollection<T> ToProducerConsumerCollection<T>(this BlockingCollection<T> collection, TimeSpan timeout, CancellationToken token)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new BlockingProducerConsumerCollection<T>(collection, timeout, token);
        }
    }
}