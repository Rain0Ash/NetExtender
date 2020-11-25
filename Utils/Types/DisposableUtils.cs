// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicData.Annotations;

namespace NetExtender.Utils.Types
{
    public static class DisposableUtils
    {
        /// <summary>Invokes the dispose for each item in the <paramref name="disposables"/>.</summary>
        /// <param name="disposables">The multiple <see cref="IDisposable"/> instances.</param>
        /// <exception cref="AggregateException"></exception>
        public static void DisposeAll([NotNull, ItemNotNull, InstantHandle] this IEnumerable<IDisposable> disposables)
        {
            List<Exception> exceptions = null;

            foreach (IDisposable item in disposables)
            {
                try
                {
                    item?.Dispose();
                }
                catch (Exception ex)
                {
                    exceptions ??= new List<Exception>();

                    exceptions.Add(ex);
                }
            }

            if (exceptions is not null)
            {
                throw new AggregateException(exceptions);
            }
        }

        /// <summary>Invokes the dispose for each item in the <paramref name="disposables"/>.</summary>
        /// <param name="disposables">The multiple <see cref="IDisposable"/> instances.</param>
        /// <param name="handler">The exception handler.</param>
        public static void DisposeAll([NotNull, ItemNotNull, InstantHandle] this IEnumerable<IDisposable> disposables, [NotNull, InstantHandle] Func<Exception, Boolean> handler)
        {
            foreach (IDisposable item in disposables)
            {
                try
                {
                    item?.Dispose();
                }
                catch (Exception ex)
                {
                    handler?.Invoke(ex);
                }
            }
        }

        /// <summary>
        /// Calls DisposeAsync if <paramref name="disposable"/> implements <see cref="IAsyncDisposable"/>, otherwise
        /// calls <see cref="IDisposable.Dispose"/>
        /// </summary>
        public static ValueTask DisposeAsync([NotNull] this IDisposable disposable)
        {
            switch (disposable)
            {
                case null:
                    throw new ArgumentNullException(nameof(disposable));
                case IAsyncDisposable asyncDisposable:
                    return asyncDisposable.DisposeAsync();
                default:
                    disposable.Dispose();
                    return new ValueTask();
            }
        }
    }
}