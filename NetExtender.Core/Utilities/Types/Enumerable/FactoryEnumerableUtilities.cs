// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        public static IEnumerable<T> Factory<T>(T value)
        {
            yield return value;
        }

        public static IEnumerable<T> Factory<T>(T value, params T[]? other)
        {
            yield return value;

            if (other is null || other.Length <= 0)
            {
                yield break;
            }
            
            foreach (T item in other)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Factory<T>(T first, T second)
        {
            yield return first;
            yield return second;
        }

        public static IEnumerable<T> Factory<T>(T first, T second, params T[]? other)
        {
            yield return first;
            yield return second;
            
            if (other is null || other.Length <= 0)
            {
                yield break;
            }

            foreach (T item in other)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Factory<T>(T first, T second, T third)
        {
            yield return first;
            yield return second;
            yield return third;
        }

        public static IEnumerable<T> Factory<T>(T first, T second, T third, params T[]? other)
        {
            yield return first;
            yield return second;
            yield return third;

            if (other is null || other.Length <= 0)
            {
                yield break;
            }
            
            foreach (T item in other)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Factory<T>(Func<T> generator)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            while (true)
            {
                yield return generator();
            }
        }

        public static IEnumerable<T> Factory<T>(Func<Int32, T> generator)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            Int32 i = 0;
            while (true)
            {
                yield return generator(i++);
            }
        }

        public static IEnumerable<T> Factory<T>(Func<Int64, T> generator)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            Int64 i = 0;
            while (true)
            {
                yield return generator(i++);
            }
        }
        
        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<T> values = source.ToArray();

            while (true)
            {
                foreach (T item in values)
                {
                    yield return item;
                }
            }
        }
        
        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (token.IsCancellationRequested)
            {
                yield break;
            }

            ICollection<T> values = source.ToArray();

            while (!token.IsCancellationRequested)
            {
                foreach (T item in values)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Turns a finite sequence into a circular one, or equivalently,
        /// repeats the original sequence indefinitely.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to cycle through.</param>
        /// <param name="count">Count of repeat, 0 is default IEnumerable</param>
        /// <returns>An infinite sequence cycling through the given sequence.</returns>
        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (count < 0)
            {
                foreach (T item in source)
                {
                    yield return item;
                }
                
                yield break;
            }

            ICollection<T> values = source.ToArray();

            for (Int32 i = 0; i <= count; i++)
            {
                foreach (T item in values)
                {
                    yield return item;
                }
            }
        }
        
        /// <summary>
        /// Creates a sequence from start value and next element factory till factory returns null.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="value">Start value.</param>
        /// <param name="next">Next element factory.</param>
        /// <returns>Generated sequence.</returns>
        public static IEnumerable<T> CreateWhileNotNull<T>(T? value, Func<T, T?> next) where T : class
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            while (value is not null)
            {
                yield return value;
                value = next(value);
            }
        }

        /// <summary>
        /// Creates a sequence from start value and next element factory till factory returns null.
        /// </summary>
        /// <typeparam name="T">The type of source element.</typeparam>
        /// <typeparam name="TResult">The type of result element</typeparam>
        /// <param name="value">Start value.</param>
        /// <param name="next">Next element factory.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>Generated sequence.</returns>
        public static IEnumerable<TResult> CreateWhileNotNull<T, TResult>(T? value, Func<T, T?> next, Func<T, TResult> selector) where T : class
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            while (value is not null)
            {
                yield return selector(value);
                value = next(value);
            }
        }
        
        /// <summary>
        /// Creates a sequence from start value and next element factory till factory returns null.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="value">Start value.</param>
        /// <param name="next">Next element factory.</param>
        /// <returns>Generated sequence.</returns>
        public static IEnumerable<T> CreateWhileNotNull<T>(T? value, Func<T, T?> next) where T : struct
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            while (value.HasValue)
            {
                yield return value.Value;
                value = next(value.Value);
            }
        }

        /// <summary>
        /// Creates a sequence from start value and next element factory till factory returns null.
        /// </summary>
        /// <typeparam name="T">The type of source element.</typeparam>
        /// <typeparam name="TResult">The type of result element</typeparam>
        /// <param name="value">Start value.</param>
        /// <param name="next">Next element factory.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>Generated sequence.</returns>
        public static IEnumerable<TResult> CreateWhileNotNull<T, TResult>(T? value, Func<T, T?> next, Func<T, TResult> selector) where T : struct
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            while (value.HasValue)
            {
                yield return selector(value.Value);
                value = next(value.Value);
            }
        }
    }
}