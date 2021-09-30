// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T>? source, IEnumerable<T>? additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return additional is null ? source : source.Concat(additional);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T>? source, params IEnumerable<T>?[]? additionals)
        {
            return Concat(source, additionals);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T>? source, T value, params T[]? values)
        {
            if (source is not null)
            {
                foreach (T item in source)
                {
                    yield return item;
                }
            }

            yield return value;

            if (values is null)
            {
                yield break;
            }

            foreach (T item in values)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T>? source, IEnumerable<T>? additional)
        {
            return source.Preconcat(additional);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T>? source, params IEnumerable<T>?[]? additionals)
        {
            return source.Preconcat(additionals);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T>? source, T value, params T[]? values)
        {
            yield return value;

            if (values is not null)
            {
                foreach (T item in values)
                {
                    yield return item;
                }
            }

            if (source is null)
            {
                yield break;
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T>? source, params IEnumerable<T>?[]? additionals)
        {
            if (source is not null)
            {
                foreach (T item in source)
                {
                    yield return item;
                }
            }

            if (additionals is null)
            {
                yield break;
            }

            foreach (IEnumerable<T>? next in additionals)
            {
                if (next is null)
                {
                    continue;
                }

                foreach (T item in next)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Preconcat<T>(this IEnumerable<T>? source, IEnumerable<T>? additional)
        {
            if (additional is not null)
            {
                foreach (T item in additional)
                {
                    yield return item;
                }
            }

            if (source is null)
            {
                yield break;
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<T> Preconcat<T>(this IEnumerable<T>? source, params IEnumerable<T>?[]? additionals)
        {
            if (additionals is not null)
            {
                foreach (IEnumerable<T>? next in additionals)
                {
                    if (next is null)
                    {
                        continue;
                    }

                    foreach (T item in next)
                    {
                        yield return item;
                    }
                }
            }

            if (source is null)
            {
                yield break;
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> AppendAggregate<T>(this IEnumerable<T> source, Func<T, T, T> aggregate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (aggregate is null)
            {
                throw new ArgumentNullException(nameof(aggregate));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            T result = enumerator.Current;
            yield return result;
            
            while (enumerator.MoveNext())
            {
                result = aggregate(result, enumerator.Current);
                yield return enumerator.Current;
            }

            yield return result;
        }
        
        public static IEnumerable<T> AppendAggregate<T>(this IEnumerable<T> source, T seed, Func<T, T, T> aggregate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (aggregate is null)
            {
                throw new ArgumentNullException(nameof(aggregate));
            }

            T result = seed;
            
            using IEnumerator<T> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                yield return result;
                yield break;
            }
            
            yield return enumerator.Current;
            
            while (enumerator.MoveNext())
            {
                result = aggregate(result, enumerator.Current);
                yield return enumerator.Current;
            }

            yield return result;
        }
    }
}