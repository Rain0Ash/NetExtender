// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            while (await Task.Run(enumerator.MoveNext).ConfigureAwait(false))
            {
                yield return enumerator.Current;
            }
        }

        public static async IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is IAsyncEnumerable<T> async)
            {
                await foreach (T item in async)
                {
                    yield return item;
                }

                yield break;
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        public static IAsyncEnumerator<T> GetAsyncEnumerator<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source is IAsyncEnumerable<T> async ? async.GetAsyncEnumerator() : source.AsAsyncEnumerable().GetAsyncEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task> action)
        {
            return ForEachAsync(source, action, CancellationToken.None);
        }

        public static async Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task> action, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (TSource item in source)
            {
                token.ThrowIfCancellationRequested();
                await action(item).ConfigureAwait(false);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, CancellationToken, Task> action)
        {
            return ForEachAsync(source, action, CancellationToken.None);
        }

        public static async Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, CancellationToken, Task> action, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            foreach (TSource item in source)
            {
                token.ThrowIfCancellationRequested();
                await action(item, token).ConfigureAwait(false);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Int32, Task> action)
        {
            return ForEachAsync(source, action, CancellationToken.None);
        }

        public static async Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Int32, Task> action, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            Int32 index = 0;
            foreach (TSource item in source)
            {
                token.ThrowIfCancellationRequested();
                await action(item, index++).ConfigureAwait(false);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, Task> action)
        {
            return ForEachAsync(source, action, CancellationToken.None);
        }

        public static async Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, Task> action, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            Int32 index = 0;
            foreach (TSource item in source)
            {
                token.ThrowIfCancellationRequested();
                await action(item, index++, token).ConfigureAwait(false);
            }
        }
        
#if NET6_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ParallelForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task> action)
        {
            return ParallelForEachAsync(source, action, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ParallelForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task> action, CancellationToken token)
        {
            return ParallelForEachAsync(source, Environment.ProcessorCount, action, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ParallelForEachAsync<TSource>(this IEnumerable<TSource> source, Int32 parallelism, Func<TSource, Task> action)
        {
            return ParallelForEachAsync(source, parallelism, action, CancellationToken.None);
        }

        public static Task ParallelForEachAsync<TSource>(this IEnumerable<TSource> source, Int32 parallelism, Func<TSource, Task> action, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = parallelism, CancellationToken = token };
            return Parallel.ForEachAsync(source, options, (item, _) => new ValueTask(action(item)));
        }
#endif

        public static List<T> Materialize<T>(this IAsyncEnumerable<T> source)
        {
            return Materialize<T, List<T>>(source);
        }

        public static TCollection Materialize<T, TCollection>(this IAsyncEnumerable<T> source) where TCollection : ICollection<T>, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return MaterializeAsync<T, TCollection>(source).GetAwaiter().GetResult();
        }

        public static Task<List<T>> MaterializeAsync<T>(this IAsyncEnumerable<T> source)
        {
            return MaterializeAsync(source, CancellationToken.None);
        }

        public static Task<TCollection> MaterializeAsync<T, TCollection>(this IAsyncEnumerable<T> source) where TCollection : ICollection<T>, new()
        {
            return MaterializeAsync<T, TCollection>(source, CancellationToken.None);
        }

        public static Task<List<T>> MaterializeAsync<T>(this IAsyncEnumerable<T> source, CancellationToken token)
        {
            return MaterializeAsync<T, List<T>>(source, token);
        }

        public static async Task<TCollection> MaterializeAsync<T, TCollection>(this IAsyncEnumerable<T> source, CancellationToken token) where TCollection : ICollection<T>, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TCollection collection = new TCollection();

            if (collection.IsReadOnly)
            {
                throw new NotSupportedException();
            }

            await foreach (T record in source.WithCancellation(token).ConfigureAwait(false))
            {
                collection.Add(record);
            }

            return collection;
        }
    }
}