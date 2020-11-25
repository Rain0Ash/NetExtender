// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using DynamicData.Annotations;
using NetExtender.Types.Queues;

namespace NetExtender.Utils.Threading
{
    /// <summary>
    /// Parallel extensions.
    /// </summary>
    [PublicAPI]
    public static class ParallelUtils
    {
        public const String DefaultProcessName = "ParallelProcess";
        
        /// <summary>
        /// Implements Provider-Consumer pattern.
        /// </summary>
        /// <typeparam name="TSource">Type of source value</typeparam>
        /// <typeparam name="TTarget">Type of target value</typeparam>
        /// <param name="source">Incoming data.</param>
        /// <param name="providerCount">Number of provider threads.</param>
        /// <param name="providerFunc">Provider function</param>
        /// <param name="consumerCount">Number of consumer threads.</param>
        /// <param name="consumerAction">Consumer action.</param>
        /// <param name="processName">Process name pattern.</param>
        public static void RunInParallel<TSource, TTarget>([NotNull, InstantHandle] this IEnumerable<TSource> source, Int32 providerCount,
            [NotNull, InstantHandle] Func<TSource, TTarget> providerFunc, Int32 consumerCount,
            [NotNull, InstantHandle] Action<TTarget> consumerAction, String processName = DefaultProcessName)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (providerFunc is null)
            {
                throw new ArgumentNullException(nameof(providerFunc));
            }

            if (consumerAction is null)
            {
                throw new ArgumentNullException(nameof(consumerAction));
            }

            using ParallelQueue providerQueue = new ParallelQueue(providerCount, processName + "_provider_");
            using ParallelQueue consumerQueue = new ParallelQueue(consumerCount, processName + "_consumer_");
            foreach (TSource item in source)
            {
                TSource pItem = item;

                providerQueue.EnqueueItem(() =>
                {
                    TTarget data = providerFunc(pItem);

                    // ReSharper disable once AccessToDisposedClosure
                    consumerQueue.EnqueueItem(() => consumerAction(data));
                });
            }

            providerQueue.WaitAll();
            consumerQueue.WaitAll();
        }

        /// <summary>
        /// Implements Provider-Consumer pattern.
        /// </summary>
        /// <typeparam name="TSource">Type of source value</typeparam>
        /// <typeparam name="TTarget">Type of target value</typeparam>
        /// <param name="source">Incoming data.</param>
        /// <param name="providerFunc">Provider function</param>
        /// <param name="consumerCount">Number of consumer threads.</param>
        /// <param name="consumerAction">Consumer action.</param>
        /// <param name="name">Process name pattern.</param>
        public static void RunInParallel<TSource, TTarget>([NotNull, InstantHandle] this IEnumerable<TSource> source, [NotNull, InstantHandle] Func<TSource, TTarget> providerFunc,
            Int32 consumerCount, [NotNull, InstantHandle] Action<TTarget> consumerAction, String name = DefaultProcessName)
        {
            RunInParallel(source, Environment.ProcessorCount, providerFunc, consumerCount, consumerAction, name);
        }

        /// <summary>
        /// Implements Provider-Consumer pattern.
        /// </summary>
        /// <typeparam name="TSource">Type of source value</typeparam>
        /// <typeparam name="TTarget">Type of target value</typeparam>
        /// <param name="source">Incoming data.</param>
        /// <param name="providerCount">Number of provider threads.</param>
        /// <param name="providerFunc">Provider function</param>
        /// <param name="consumerAction">Consumer action.</param>
        /// <param name="name">Process name pattern.</param>
        public static void RunInParallel<TSource, TTarget>([NotNull, InstantHandle] this IEnumerable<TSource> source, Int32 providerCount,
            [NotNull, InstantHandle] Func<TSource, TTarget> providerFunc, [NotNull, InstantHandle] Action<TTarget> consumerAction, String name = DefaultProcessName)
        {
            RunInParallel(source, providerCount, providerFunc, Environment.ProcessorCount, consumerAction, name);
        }

        /// <summary>
        /// Implements Provider-Consumer pattern.
        /// </summary>
        /// <typeparam name="TSource">Type of source value</typeparam>
        /// <typeparam name="TTarget">Type of target value</typeparam>
        /// <param name="source">Incoming data.</param>
        /// <param name="providerFunc">Provider function</param>
        /// <param name="consumerAction">Consumer action.</param>
        /// <param name="name">Process name pattern.</param>
        public static void RunInParallel<TSource, TTarget>([NotNull, InstantHandle] this IEnumerable<TSource> source, [NotNull, InstantHandle] Func<TSource, TTarget> providerFunc,
            [NotNull, InstantHandle] Action<TTarget> consumerAction,
            String name = "ParallelProcess")
        {
            RunInParallel(source, Environment.ProcessorCount / 2, providerFunc, Environment.ProcessorCount / 2, consumerAction, name);
        }

        /// <summary>
        /// Runs in parallel provided source of actions.
        /// </summary>
        /// <param name="source">Actions to run.</param>
        /// <param name="count">number of threads to use.</param>
        /// <param name="name">Process name pattern.</param>
        public static void RunInParallel([NotNull, ItemNotNull, InstantHandle] this IEnumerable<Action> source, Int32 count, String name = DefaultProcessName)
        {
            using ParallelQueue queue = new ParallelQueue(count, name + '_');
            foreach (Action action in source)
            {
                Action data = action;
                queue.EnqueueItem(data);
            }

            queue.WaitAll();
        }

        /// <summary>
        /// Runs in parallel provided source of actions.
        /// </summary>
        /// <param name="source">Actions to run.</param>
        /// <param name="name">Process name pattern.</param>
        public static void RunInParallel([NotNull, InstantHandle] this IEnumerable<Action> source, String name = DefaultProcessName)
        {
            RunInParallel(source, Environment.ProcessorCount, name);
        }

        /// <summary>
        /// Runs in parallel actions for provided data source.
        /// </summary>
        /// <typeparam name="T">Type of element in source</typeparam>
        /// <param name="source">Source to run.</param>
        /// <param name="count">number of threads to use.</param>
        /// <param name="action">Action to run.</param>
        /// <param name="name">Process name.</param>
        public static void RunInParallel<T>([NotNull, InstantHandle] this IEnumerable<T> source, Int32 count, [NotNull, InstantHandle] Action<T> action, String name = DefaultProcessName)
        {
            using ParallelQueue queue = new ParallelQueue(count, name + '_');
            foreach (T item in source)
            {
                T data = item;
                Action<T> run = action;
                queue.EnqueueItem(() => run(data));
            }

            queue.WaitAll();
        }

        /// <summary>
        /// Runs in parallel actions for provided data source.
        /// </summary>
        /// <typeparam name="T">Type of element in source</typeparam>
        /// <param name="source">Source to run.</param>
        /// <param name="action">Action to run.</param>
        /// <param name="name">Process name.</param>
        public static void RunInParallel<T>([NotNull, InstantHandle] this IEnumerable<T> source, [NotNull, InstantHandle] Action<T> action, String name = DefaultProcessName)
        {
            RunInParallel(source, Environment.ProcessorCount, action, name);
        }
    }
}