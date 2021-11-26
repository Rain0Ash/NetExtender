// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using NetExtender.Types.Monads;

namespace NetExtender.Utilities.Types
{
    public static class MaybeUtilities
    {
        public static T? Unwrap<T>(this Maybe<T> maybe)
        {
            return maybe.HasValue ? maybe.Value : default;
        }
        
        public static T Unwrap<T>(this Maybe<T> maybe, T alternate)
        {
            return maybe.HasValue ? maybe.Value : alternate;
        }
        
        public static T Unwrap<T>(this Maybe<T> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return maybe.HasValue ? maybe.Value : alternate();
        }

        public static Maybe<TResult> Bind<T, TResult>(this Maybe<T> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return maybe.HasValue ? selector(maybe.Value) : default(Maybe<TResult>);
        }

        public static Maybe<TResult> Bind<T, TResult>(this Maybe<T> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return maybe.HasValue ? selector(maybe.Value) : default;
        }

        public static Maybe<T> Or<T>(this Maybe<T> maybe, Func<T> fallback)
        {
            if (fallback is null)
            {
                throw new ArgumentNullException(nameof(fallback));
            }

            return maybe.HasValue ? maybe : fallback();
        }

        public static async Task<Maybe<T>> Or<T>(this Maybe<T> maybe, Func<Task<T>> fallback)
        {
            if (fallback is null)
            {
                throw new ArgumentNullException(nameof(fallback));
            }

            return maybe.HasValue ? maybe : await fallback().ConfigureAwait(false);
        }

        public static Maybe<T> Or<T>(this Maybe<T> maybe, Maybe<T> fallback)
        {
            return maybe.HasValue ? maybe : fallback;
        }

        public static async Task<Maybe<T>> Or<T>(this Maybe<T> maybe, Task<Maybe<T>> fallback)
        {
            if (fallback is null)
            {
                throw new ArgumentNullException(nameof(fallback));
            }

            return maybe.HasValue ? maybe : await fallback.ConfigureAwait(false);
        }

        public static Maybe<T> Or<T>(this Maybe<T> maybe, Func<Maybe<T>> fallback)
        {
            if (fallback is null)
            {
                throw new ArgumentNullException(nameof(fallback));
            }

            return maybe.HasValue ? maybe : fallback();
        }

        public static async Task<Maybe<T>> Or<T>(this Maybe<T> maybe, Func<Task<Maybe<T>>> fallback)
        {
            if (fallback is null)
            {
                throw new ArgumentNullException(nameof(fallback));
            }

            return maybe.HasValue ? maybe : await fallback();
        }
    }
}