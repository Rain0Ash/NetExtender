// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetExtender.Utils.Types
{
    public static class DisposableUtils
    {
        /// <summary>Invokes the dispose for each item in the <paramref name="source"/>.</summary>
        /// <param name="source">The multiple <see cref="IDisposable"/> instances.</param>
        public static void DisposeAll(this IEnumerable<IDisposable?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (IDisposable? item in source)
            {
                try
                {
                    item?.Dispose();
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }

        /// <summary>Invokes the dispose for each item in the <paramref name="source"/>.</summary>
        /// <param name="source">The multiple <see cref="IDisposable"/> instances.</param>
        /// <param name="handler">The exception handler.</param>
        public static void DisposeAll(this IEnumerable<IDisposable?> source, Func<Exception, Boolean>? handler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (handler is null)
            {
                DisposeAll(source);
                return;
            }

            foreach (IDisposable? item in source)
            {
                try
                {
                    item?.Dispose();
                }
                catch (Exception exception)
                {
                    handler.Invoke(exception);
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