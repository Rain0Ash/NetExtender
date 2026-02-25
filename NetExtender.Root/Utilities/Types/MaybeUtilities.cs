// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetExtender.Types.Monads;

namespace NetExtender.Utilities.Types
{
    public static class MaybeUtilities
    {
        public static Type MaybeType { get; } = typeof(Maybe<>);
        public static Type NullMaybeType { get; } = typeof(NullMaybe<>);
        public static Type WeakMaybeType { get; } = typeof(NullMaybe<>);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<T> Maybe<T>(this T value)
        {
            return new Maybe<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullMaybe<T> NullMaybe<T>(this T value)
        {
            return new NullMaybe<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WeakMaybe<T> WeakMaybe<T>(this T value) where T : class
        {
            return new WeakMaybe<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Unwrap<T>(this Maybe<T> maybe)
        {
            return maybe.HasValue ? maybe.Internal : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap<T>(this Maybe<T> maybe, T alternate)
        {
            return maybe.HasValue ? maybe.Internal : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap<T>(this Maybe<T> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return maybe.HasValue ? maybe.Internal : alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Unwrap<T>(this Maybe<T> maybe, [MaybeNullWhen(false)] out T result)
        {
            if (maybe.HasValue)
            {
                result = maybe.Internal;
                return true;
            }

            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Unwrap<T>(this Maybe<T> maybe, T alternate, [MaybeNullWhen(false)] out T result)
        {
            if (maybe.HasValue)
            {
                result = maybe.Internal;
                return true;
            }

            result = alternate;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Unwrap<T>(this Maybe<T> maybe, Func<T> alternate, [MaybeNullWhen(false)] out T result)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            if (maybe.HasValue)
            {
                result = maybe.Internal;
                return true;
            }

            result = alternate();
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Unwrap<T>(this WeakMaybe<T> maybe) where T : class
        {
            return Unwrap(maybe.Maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap<T>(this WeakMaybe<T> maybe, T alternate) where T : class
        {
            return Unwrap(maybe.Maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap<T>(this WeakMaybe<T> maybe, Func<T> alternate) where T : class
        {
            return Unwrap(maybe.Maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Unwrap<T>(this WeakMaybe<T> maybe, [MaybeNullWhen(false)] out T result) where T : class
        {
            return Unwrap(maybe.Maybe, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Unwrap<T>(this WeakMaybe<T> maybe, T alternate, [MaybeNullWhen(false)] out T result) where T : class
        {
            return Unwrap(maybe.Maybe, alternate, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Unwrap<T>(this WeakMaybe<T> maybe, Func<T> alternate, [MaybeNullWhen(false)] out T result) where T : class
        {
            return Unwrap(maybe.Maybe, alternate, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this Task<Maybe<T>> maybe)
        {
            return Unwrap(await maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<Maybe<T>> maybe, T alternate)
        {
            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<Maybe<T>> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<Maybe<T>> maybe, Func<Task<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<Maybe<T>> maybe, Func<ConfiguredTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<Maybe<T>> maybe, Func<ValueTask<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<Maybe<T>> maybe, Func<ConfiguredValueTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this Task<WeakMaybe<T>> maybe) where T : class
        {
            return Unwrap(await maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<WeakMaybe<T>> maybe, T alternate) where T : class
        {
            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<WeakMaybe<T>> maybe, Func<T> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<WeakMaybe<T>> maybe, Func<Task<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<WeakMaybe<T>> maybe, Func<ConfiguredTaskAwaitable<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<WeakMaybe<T>> maybe, Func<ValueTask<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this Task<WeakMaybe<T>> maybe, Func<ConfiguredValueTaskAwaitable<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe)
        {
            return Unwrap(await maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, T alternate)
        {
            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<Task<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<ConfiguredTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<ValueTask<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<ConfiguredValueTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this ConfiguredTaskAwaitable<WeakMaybe<T>> maybe) where T : class
        {
            return Unwrap(await maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<WeakMaybe<T>> maybe, T alternate) where T : class
        {
            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<WeakMaybe<T>> maybe, Func<T> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<WeakMaybe<T>> maybe, Func<Task<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<WeakMaybe<T>> maybe, Func<ConfiguredTaskAwaitable<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<WeakMaybe<T>> maybe, Func<ValueTask<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredTaskAwaitable<WeakMaybe<T>> maybe, Func<ConfiguredValueTaskAwaitable<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this ValueTask<Maybe<T>> maybe)
        {
            return Unwrap(await maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<Maybe<T>> maybe, T alternate)
        {
            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<Maybe<T>> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<Maybe<T>> maybe, Func<Task<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<Maybe<T>> maybe, Func<ConfiguredTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<Maybe<T>> maybe, Func<ValueTask<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<Maybe<T>> maybe, Func<ConfiguredValueTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this ValueTask<WeakMaybe<T>> maybe) where T : class
        {
            return Unwrap(await maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<WeakMaybe<T>> maybe, T alternate) where T : class
        {
            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<WeakMaybe<T>> maybe, Func<T> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<WeakMaybe<T>> maybe, Func<Task<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<WeakMaybe<T>> maybe, Func<ConfiguredTaskAwaitable<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<WeakMaybe<T>> maybe, Func<ValueTask<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ValueTask<WeakMaybe<T>> maybe, Func<ConfiguredValueTaskAwaitable<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe)
        {
            return Unwrap(await maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, T alternate)
        {
            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<Task<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<ConfiguredTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<ValueTask<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<ConfiguredValueTaskAwaitable<T>> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T?> Unwrap<T>(this ConfiguredValueTaskAwaitable<WeakMaybe<T>> maybe) where T : class
        {
            return Unwrap(await maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<WeakMaybe<T>> maybe, T alternate) where T : class
        {
            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<WeakMaybe<T>> maybe, Func<T> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<WeakMaybe<T>> maybe, Func<Task<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<WeakMaybe<T>> maybe, Func<ConfiguredTaskAwaitable<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<WeakMaybe<T>> maybe, Func<ValueTask<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<T> Unwrap<T>(this ConfiguredValueTaskAwaitable<WeakMaybe<T>> maybe, Func<ConfiguredValueTaskAwaitable<T>> alternate) where T : class
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Unwrap(await maybe, out T? result) ? result : await alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<TResult> Bind<T, TResult>(this Maybe<T> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return maybe.HasValue ? selector(maybe.Internal) : default(Maybe<TResult>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<TResult> Bind<T, TResult>(this Maybe<T> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return maybe.HasValue ? selector(maybe.Internal) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<TResult> Bind<T, TResult>(this WeakMaybe<T> maybe, Func<T, TResult> selector) where T : class
        {
            return Bind(maybe.Maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<TResult> Bind<T, TResult>(this WeakMaybe<T> maybe, Func<T, Maybe<TResult>> selector) where T : class
        {
            return Bind(maybe.Maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this Task<Maybe<T>> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this Task<Maybe<T>> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this Task<WeakMaybe<T>> maybe, Func<T, TResult> selector) where T : class
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this Task<WeakMaybe<T>> maybe, Func<T, Maybe<TResult>> selector) where T : class
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredTaskAwaitable<Maybe<T>> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredTaskAwaitable<WeakMaybe<T>> maybe, Func<T, TResult> selector) where T : class
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredTaskAwaitable<WeakMaybe<T>> maybe, Func<T, Maybe<TResult>> selector) where T : class
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ValueTask<Maybe<T>> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ValueTask<Maybe<T>> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ValueTask<WeakMaybe<T>> maybe, Func<T, TResult> selector) where T : class
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ValueTask<WeakMaybe<T>> maybe, Func<T, Maybe<TResult>> selector) where T : class
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<T, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredValueTaskAwaitable<Maybe<T>> maybe, Func<T, Maybe<TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredValueTaskAwaitable<WeakMaybe<T>> maybe, Func<T, TResult> selector) where T : class
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask<Maybe<TResult>> Bind<T, TResult>(this ConfiguredValueTaskAwaitable<WeakMaybe<T>> maybe, Func<T, Maybe<TResult>> selector) where T : class
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Bind(await maybe, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<T> Or<T>(this Maybe<T> maybe, Maybe<T> fallback)
        {
            return maybe.HasValue ? maybe : fallback;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<T> Or<T>(this WeakMaybe<T> maybe, Maybe<T> fallback) where T : class
        {
            return Or(maybe.Maybe, fallback);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<T> Or<T>(this WeakMaybe<T> maybe, Func<T> fallback) where T : class
        {
            return Or(maybe.Maybe, fallback);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<T> Or<T>(this WeakMaybe<T> maybe, Func<Maybe<T>> fallback) where T : class
        {
            return Or(maybe.Maybe, fallback);
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
        public static Task<Maybe<T>> Or<T>(this WeakMaybe<T> maybe, Task<Maybe<T>> fallback) where T : class
        {
            return Or(maybe.Maybe, fallback);
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
        public static Task<Maybe<T>> Or<T>(this WeakMaybe<T> maybe, Func<Task<T>> fallback) where T : class
        {
            return Or(maybe.Maybe, fallback);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Maybe<T>> Or<T>(this WeakMaybe<T> maybe, Func<Task<Maybe<T>>> fallback) where T : class
        {
            return Or(maybe.Maybe, fallback);
        }
    }
}