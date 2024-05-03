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
            DisposeAll(source, (Func<Exception, Boolean>?) null);
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
        /// <param name="handler">The exception handler.</param>
        public static void DisposeAll<T>(this IEnumerable<T?> source, Func<T?, Exception, Boolean>? handler) where T : IDisposable
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
                    handler?.Invoke(item, exception);
                }
            }
        }
        
        /// <summary>Invokes the dispose for each item in the <paramref name="source"/>.</summary>
        /// <param name="source">The multiple <see cref="IDisposable"/> instances.</param>
        public static ValueTask DisposeAllAsync<T>(this IEnumerable<T?> source) where T : IAsyncDisposable
        {
            return DisposeAllAsync(source, (Func<Exception, Boolean>?) null);
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
                    await item.DisposeAsync().ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    handler?.Invoke(exception);
                }
            }
        }

        /// <summary>Invokes the dispose for each item in the <paramref name="source"/>.</summary>
        /// <param name="source">The multiple <see cref="IDisposable"/> instances.</param>
        /// <param name="handler">The exception handler.</param>
        public static async ValueTask DisposeAllAsync<T>(this IEnumerable<T?> source, Func<T?, Exception, Boolean>? handler) where T : IAsyncDisposable
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
                    await item.DisposeAsync().ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    handler?.Invoke(item, exception);
                }
            }
        }

        /// <summary>
        /// Calls DisposeAsync if <paramref name="disposable"/> implements <see cref="IAsyncDisposable"/>, otherwise
        /// calls <see cref="IDisposable.Dispose"/>
        /// </summary>
        public static ValueTask DisposeAsync(this IDisposable disposable)
        {
            switch (disposable)
            {
                case null:
                    throw new ArgumentNullException(nameof(disposable));
                case IAsyncDisposable async:
                    return async.DisposeAsync();
                default:
                    disposable.Dispose();
                    return ValueTask.CompletedTask;
            }
        }

        public static ValueTask TryDisposeAllAsync<T>(this IEnumerable<T?> source)
        {
            return TryDisposeAllAsync(source, (Func<Exception, Boolean>?) null);
        }

        public static async ValueTask TryDisposeAllAsync<T>(this IEnumerable<T?> source, Func<Exception, Boolean>? handler)
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
                    switch (item)
                    {
                        case IAsyncDisposable disposable:
                            await disposable.DisposeAsync().ConfigureAwait(false);
                            return;
                        case IDisposable disposable:
                            disposable.Dispose();
                            return;
                    }
                }
                catch (Exception exception)
                {
                    handler?.Invoke(exception);
                }
            }
        }

        public static async ValueTask TryDisposeAllAsync<T>(this IEnumerable<T?> source, Func<T?, Exception, Boolean>? handler)
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
                    switch (item)
                    {
                        case IAsyncDisposable disposable:
                            await disposable.DisposeAsync().ConfigureAwait(false);
                            return;
                        case IDisposable disposable:
                            disposable.Dispose();
                            return;
                    }
                }
                catch (Exception exception)
                {
                    handler?.Invoke(item, exception);
                }
            }
        }
    }
}