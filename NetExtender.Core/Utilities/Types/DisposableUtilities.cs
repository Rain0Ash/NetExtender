// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static class DisposableUtilities
    {
        /// <summary>Invokes the dispose for each item in the <paramref name="source"/>.</summary>
        /// <param name="source">The multiple <see cref="IDisposable"/> instances.</param>
        public static void DisposeAll<T>(this IEnumerable<T?> source) where T : IDisposable
        {
            DisposeAll(source, null);
        }

        /// <summary>Invokes the dispose for each item in the <paramref name="source"/>.</summary>
        /// <param name="source">The multiple <see cref="IDisposable"/> instances.</param>
        /// <param name="handler">The exception handler.</param>
        public static void DisposeAll<T>(this IEnumerable<T?> source, Func<Exception, Boolean>? handler) where T : IDisposable
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T? item in source)
            {
                if (item is null)
                {
                    continue;
                }
                
                try
                {
                    item.Dispose();
                }
                catch (Exception exception)
                {
                    handler?.Invoke(exception);
                }
            }
        }
        /// <summary>Invokes the dispose for each item in the <paramref name="source"/>.</summary>
        /// <param name="source">The multiple <see cref="IDisposable"/> instances.</param>
        public static ValueTask DisposeAllAsync<T>(this IEnumerable<T?> source) where T : IAsyncDisposable
        {
            return DisposeAllAsync(source, null);
        }

        /// <summary>Invokes the dispose for each item in the <paramref name="source"/>.</summary>
        /// <param name="source">The multiple <see cref="IDisposable"/> instances.</param>
        /// <param name="handler">The exception handler.</param>
        public static async ValueTask DisposeAllAsync<T>(this IEnumerable<T?> source, Func<Exception, Boolean>? handler) where T : IAsyncDisposable
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T? item in source)
            {
                if (item is null)
                {
                    continue;
                }
                
                try
                {
                    await item.DisposeAsync();
                }
                catch (Exception exception)
                {
                    handler?.Invoke(exception);
                }
            }
        }

        /// <summary>
        /// Calls DisposeAsync if <paramref name="disposable"/> implements <see cref="IAsyncDisposable"/>, otherwise
        /// calls <see cref="IDisposable.Dispose"/>
        /// </summary>
        public static ValueTask DisposeAsync(this IDisposable disposable)
        {
            if (disposable is null)
            {
                throw new ArgumentNullException(nameof(disposable));
            }

            if (disposable is IAsyncDisposable async)
            {
                return async.DisposeAsync();
            }
            
            disposable.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}