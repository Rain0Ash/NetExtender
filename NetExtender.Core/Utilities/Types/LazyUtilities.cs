// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class LazyUtilities
    {
        public static LazyThreadSafetyMode GetModeFromIsThreadSafe(Boolean value)
        {
            return value ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None;
        }

        public static Lazy<T> Create<T>(T value)
        {
            return new Lazy<T>(() => value, false).Force();
        }

        public static ILazy<T> Wrapper<T>(this Lazy<T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new LazyWrapper<T>(value);
        }

        public static T Initialize<T>(this Lazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Value;
        }

        public static T Initialize<T>(this ILazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Value;
        }

        public static Lazy<T> Force<T>(this Lazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _ = source.Value;
            return source;
        }

        public static ILazy<T> Force<T>(this ILazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _ = source.Value;
            return source;
        }

        public static T ValueOrDefault<T>(this Lazy<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.IsValueCreated ? source.Value : alternate;
        }

        public static T ValueOrDefault<T>(this ILazy<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.IsValueCreated ? source.Value : alternate;
        }

        public static T ValueOrDefault<T>(this Lazy<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.IsValueCreated ? source.Value : alternate.Invoke();
        }

        public static T ValueOrDefault<T>(this ILazy<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.IsValueCreated ? source.Value : alternate.Invoke();
        }

        public static T? ValueOrDefault<T>(this Lazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.IsValueCreated ? source.Value : default;
        }

        public static T? ValueOrDefault<T>(this ILazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.IsValueCreated ? source.Value : default;
        }

        public static T GetValue<T>(this Lazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Value;
        }

        public static T GetValue<T>(this ILazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Value;
        }

        public static Task<T> GetValueAsync<T>(this Lazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Factory.StartNew(() => source.Value);
        }

        public static Task<T> GetValueAsync<T>(this ILazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Factory.StartNew(() => source.Value);
        }

        public static Boolean TryGetValue<T>(this Lazy<T> source, [MaybeNullWhen(false)] out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.IsValueCreated)
            {
                result = default;
                return false;
            }

            result = source.Value;
            return true;
        }

        public static Boolean TryGetValue<T>(this ILazy<T> source, [MaybeNullWhen(false)] out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.IsValueCreated)
            {
                result = default;
                return false;
            }

            result = source.Value;
            return true;
        }
    }
}