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
                throw new ArgumentException("Collection is read-only.", nameof(source));
            }
            
            await foreach (T record in source.WithCancellation(token).ConfigureAwait(false))
            {
                collection.Add(record);
            }

            return collection;
        }
    }
}