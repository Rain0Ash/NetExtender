// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
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