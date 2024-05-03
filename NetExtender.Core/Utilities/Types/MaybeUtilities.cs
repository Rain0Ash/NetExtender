// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetExtender.Types.Monads;

namespace NetExtender.Utilities.Types
{
    public static class MaybeUtilities
    {
        public static Type MaybeType { get; } = typeof(Maybe<>);
        public static Type NullMaybeType { get; } = typeof(NullMaybe<>);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Unwrap<T>(this Maybe<T> maybe)
        {
            return maybe.HasValue ? maybe.Value : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap<T>(this Maybe<T> maybe, T alternate)
        {
            return maybe.HasValue ? maybe.Value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap<T>(this Maybe<T> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return maybe.HasValue ? maybe.Value : alternate();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this ValueTask<Maybe<T>> maybe)
        {
            Maybe<T> result = await maybe;
            return result.Unwrap();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<Maybe<T>> maybe, T alternate)
        {
            Maybe<T> result = await maybe;
            return result.Unwrap(alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<Maybe<T>> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.Unwrap(alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<Maybe<T>> maybe, Func<ValueTask<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<Maybe<T>> maybe, Func<Task<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe)
        {
            Maybe<T> result = await maybe;
            return result.Unwrap();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, T alternate)
        {
            Maybe<T> result = await maybe;
            return result.Unwrap(alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.Unwrap(alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<ValueTask<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<ConfiguredValueTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<Task<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<ConfiguredTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this Task<Maybe<T>> maybe)
        {
            Maybe<T> result = await maybe;
            return result.Unwrap();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<Maybe<T>> maybe, T alternate)
        {
            Maybe<T> result = await maybe;
            return result.Unwrap(alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<Maybe<T>> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.Unwrap(alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<Maybe<T>> maybe, Func<ValueTask<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<Maybe<T>> maybe, Func<Task<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe)
        {
            Maybe<T> result = await maybe;
            return result.Unwrap();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, T alternate)
        {
            Maybe<T> result = await maybe;
            return result.Unwrap(alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.Unwrap(alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<ValueTask<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<ConfiguredValueTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<Task<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<ConfiguredTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? result.Value : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<TResult> Bind<T, TResult>(this Maybe<T> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return maybe.HasValue ? selector(maybe.Value) : default(Maybe<TResult>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<TResult> Bind<T, TResult>(this Maybe<T> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return maybe.HasValue ? selector(maybe.Value) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ValueTask<Maybe<T>> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? selector(result.Value) : default(Maybe<TResult>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ValueTask<Maybe<T>> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? selector(result.Value) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? selector(result.Value) : default(Maybe<TResult>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? selector(result.Value) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this Task<Maybe<T>> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? selector(result.Value) : default(Maybe<TResult>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this Task<Maybe<T>> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? selector(result.Value) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? selector(result.Value) : default(Maybe<TResult>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Maybe<T> result = await maybe;
            return result.HasValue ? selector(result.Value) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<T> Or<T>(this Maybe<T> maybe, Func<T> fallback)
        {
            if (fallback is null)
            {
                throw new ArgumentNullException(nameof(fallback));
            }

            return maybe.HasValue ? maybe : fallback.Invoke();
        }

        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<Maybe<T>> Or<T>(this Maybe<T> maybe, Func<Task<T>> fallback)
        {
            if (fallback is null)
            {
                throw new ArgumentNullException(nameof(fallback));
            }

            return maybe.HasValue ? maybe : await fallback.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<T> Or<T>(this Maybe<T> maybe, Maybe<T> fallback)
        {
            return maybe.HasValue ? maybe : fallback;
        }

        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<Maybe<T>> Or<T>(this Maybe<T> maybe, Task<Maybe<T>> fallback)
        {
            if (fallback is null)
            {
                throw new ArgumentNullException(nameof(fallback));
            }

            return maybe.HasValue ? maybe : await fallback;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<T> Or<T>(this Maybe<T> maybe, Func<Maybe<T>> fallback)
        {
            if (fallback is null)
            {
                throw new ArgumentNullException(nameof(fallback));
            }

            return maybe.HasValue ? maybe : fallback.Invoke();
        }

        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<Maybe<T>> Or<T>(this Maybe<T> maybe, Func<Task<Maybe<T>>> fallback)
        {
            if (fallback is null)
            {
                throw new ArgumentNullException(nameof(fallback));
            }

            return maybe.HasValue ? maybe : await fallback.Invoke();
        }
    }
}