// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Queues;

namespace NetExtender.Utilities.Threading
{
    /// <summary>
    /// Parallel extensions.
    /// </summary>
    public static class ParallelUtilities
    {
        public const String DefaultProcessName = "ParallelProcess";

        /// <inheritdoc cref="RunInParallel{TSource,TTarget}(System.Collections.Generic.IEnumerable{TSource},String,Int32,System.Func{TSource,TTarget},Int32,System.Action{TTarget})"/>
        public static void RunInParallel<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> function, Int32 consumer, Action<TTarget> action)
        {
            RunInParallel(source, DefaultProcessName, function, consumer, action);
        }

        /// <inheritdoc cref="RunInParallel{TSource,TTarget}(System.Collections.Generic.IEnumerable{TSource},String,Int32,System.Func{TSource,TTarget},Int32,System.Action{TTarget})"/>
        public static void RunInParallel<TSource, TTarget>(this IEnumerable<TSource> source, String name, Func<TSource, TTarget> function, Int32 consumer, Action<TTarget> action)
        {
            RunInParallel(source, name, Environment.ProcessorCount, function, consumer, action);
        }

        /// <inheritdoc cref="RunInParallel{TSource,TTarget}(System.Collections.Generic.IEnumerable{TSource},String,Int32,System.Func{TSource,TTarget},Int32,System.Action{TTarget})"/>
        public static void RunInParallel<TSource, TTarget>(this IEnumerable<TSource> source, Int32 provider, Func<TSource, TTarget> function, Action<TTarget> action)
        {
            RunInParallel(source, DefaultProcessName, provider, function, action);
        }

        /// <inheritdoc cref="RunInParallel{TSource,TTarget}(System.Collections.Generic.IEnumerable{TSource},String,Int32,System.Func{TSource,TTarget},Int32,System.Action{TTarget})"/>
        public static void RunInParallel<TSource, TTarget>(this IEnumerable<TSource> source, String name, Int32 provider, Func<TSource, TTarget> function, Action<TTarget> action)
        {
            RunInParallel(source, name, provider, function, Environment.ProcessorCount, action);
        }

        /// <inheritdoc cref="RunInParallel{TSource,TTarget}(System.Collections.Generic.IEnumerable{TSource},String,Int32,System.Func{TSource,TTarget},Int32,System.Action{TTarget})"/>
        public static void RunInParallel<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> function, Action<TTarget> action)
        {
            RunInParallel(source, DefaultProcessName, function, action);
        }

        /// <inheritdoc cref="RunInParallel{TSource,TTarget}(System.Collections.Generic.IEnumerable{TSource},String,Int32,System.Func{TSource,TTarget},Int32,System.Action{TTarget})"/>
        public static void RunInParallel<TSource, TTarget>(this IEnumerable<TSource> source, String name, Func<TSource, TTarget> function, Action<TTarget> action)
        {
            RunInParallel(source, name, Environment.ProcessorCount / 2, function, Environment.ProcessorCount / 2, action);
        }

        /// <inheritdoc cref="RunInParallel{TSource,TTarget}(System.Collections.Generic.IEnumerable{TSource},String,Int32,System.Func{TSource,TTarget},Int32,System.Action{TTarget})"/>
        public static void RunInParallel<TSource, TTarget>(this IEnumerable<TSource> source, Int32 provider, Func<TSource, TTarget> function, Int32 consumer, Action<TTarget> action)
        {
            RunInParallel(source, DefaultProcessName, provider, function, consumer, action);
        }

        /// <summary>
        /// Implements Provider-Consumer pattern.
        /// </summary>
        /// <typeparam name="TSource">Type of source value</typeparam>
        /// <typeparam name="TTarget">Type of target value</typeparam>
        /// <param name="source">Incoming data.</param>
        /// <param name="provider">Number of provider threads.</param>
        /// <param name="function">Provider function</param>
        /// <param name="consumer">Number of consumer threads.</param>
        /// <param name="action">Consumer action.</param>
        /// <param name="name">Process name pattern.</param>
        public static void RunInParallel<TSource, TTarget>(this IEnumerable<TSource> source, String name, Int32 provider, Func<TSource, TTarget> function, Int32 consumer, Action<TTarget> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            using ParallelQueue pqueue = new ParallelQueue(provider, name + "_provider_");
            using ParallelQueue cqueue = new ParallelQueue(consumer, name + "_consumer_");

            foreach (TSource item in source)
            {
                pqueue.Enqueue(() =>
                {
                    TTarget value = function(item);
                    // ReSharper disable once AccessToDisposedClosure
                    cqueue.Enqueue(() => action(value));
                });
            }

            pqueue.WaitAll();
            cqueue.WaitAll();
        }

        /// <inheritdoc cref="RunInParallel(System.Collections.Generic.IEnumerable{Action},String,Int32)"/>
        public static void RunInParallel(this IEnumerable<Action> source)
        {
            RunInParallel(source, DefaultProcessName);
        }

        /// <inheritdoc cref="RunInParallel(System.Collections.Generic.IEnumerable{Action},String,Int32)"/>
        public static void RunInParallel(this IEnumerable<Action> source, Int32 count)
        {
            RunInParallel(source, DefaultProcessName, count);
        }

        /// <inheritdoc cref="RunInParallel(System.Collections.Generic.IEnumerable{Action},String,Int32)"/>
        public static void RunInParallel(this IEnumerable<Action> source, String name)
        {
            RunInParallel(source, name, Environment.ProcessorCount);
        }

        /// <summary>
        /// Runs in parallel provided source of actions.
        /// </summary>
        /// <param name="source">Actions to run.</param>
        /// <param name="count">number of threads to use.</param>
        /// <param name="name">Process name pattern.</param>
        public static void RunInParallel(this IEnumerable<Action> source, String name, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            using ParallelQueue queue = new ParallelQueue(count, name + '_');

            foreach (Action action in source)
            {
                queue.Enqueue(action);
            }

            queue.WaitAll();
        }

        /// <inheritdoc cref="RunInParallel{T}(System.Collections.Generic.IEnumerable{T},String,Int32,System.Action{T})"/>
        public static void RunInParallel<T>(this IEnumerable<T> source, Int32 count, Action<T> action)
        {
            RunInParallel(source, DefaultProcessName, count, action);
        }

        /// <summary>
        /// Runs in parallel actions for provided data source.
        /// </summary>
        /// <typeparam name="T">Type of element in source</typeparam>
        /// <param name="source">Source to run.</param>
        /// <param name="count">number of threads to use.</param>
        /// <param name="action">Action to run.</param>
        /// <param name="name">Process name.</param>
        public static void RunInParallel<T>(this IEnumerable<T> source, String name, Int32 count, Action<T> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            using ParallelQueue queue = new ParallelQueue(count, name + '_');

            foreach (T item in source)
            {
                queue.Enqueue(() => action(item));
            }

            queue.WaitAll();
        }

        /// <inheritdoc cref="RunInParallel{T}(System.Collections.Generic.IEnumerable{T},String,System.Action{T})"/>
        public static void RunInParallel<T>(this IEnumerable<T> source, Action<T> action)
        {
            RunInParallel(source, DefaultProcessName, action);
        }

        /// <summary>
        /// Runs in parallel actions for provided data source.
        /// </summary>
        /// <typeparam name="T">Type of element in source</typeparam>
        /// <param name="source">Source to run.</param>
        /// <param name="action">Action to run.</param>
        /// <param name="name">Process name.</param>
        public static void RunInParallel<T>(this IEnumerable<T> source, String name, Action<T> action)
        {
            RunInParallel(source, name, Environment.ProcessorCount, action);
        }
    }
}