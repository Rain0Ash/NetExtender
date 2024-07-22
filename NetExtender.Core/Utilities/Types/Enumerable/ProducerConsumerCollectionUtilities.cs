// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Collections;
using NetExtender.Types.Observers;

namespace NetExtender.Utilities.Types
{
    public static class ProducerConsumerCollectionUtilities
    {
        public static IEnumerable<T> GetConsumingEnumerable<T>(this IProducerConsumerCollection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            while (collection.TryTake(out T? item))
            {
                yield return item;
            }
        }

        public static Boolean TryAdd<T>(this IProducerConsumerCollection<T> collection, IEnumerable<T> source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Aggregate(false, (current, item) => current | collection.TryAdd(item));
        }

        public static IDisposable Subscribe<T>(this IProducerConsumerCollection<T> collection, IObservable<T> observable)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (observable is null)
            {
                throw new ArgumentNullException(nameof(observable));
            }

            return observable.Subscribe(new DelegateObserver<T>(ActionUtilities.Default, item => collection.TryAdd(item), ActionUtilities.Default));
        }

        public static void Clear<T>(this IProducerConsumerCollection<T?> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            while (collection.TryTake(out T? _))
            {
            }
        }

        public static IProducerConsumerCollection<T> ToProducerOnlyCollection<T>(this IProducerConsumerCollection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new ProducerOrConsumerCollection<T>(collection, ProducerConsumerCollectionType.Produce);
        }

        public static IProducerConsumerCollection<T> ToConsumerOnlyCollection<T>(this IProducerConsumerCollection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new ProducerOrConsumerCollection<T>(collection, ProducerConsumerCollectionType.Consume);
        }
    }
}