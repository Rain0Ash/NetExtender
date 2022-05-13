// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return Enumerable.Concat(source, additional);
        }

        public static IEnumerable<T> AppendOr<T>(this IEnumerable<T>? source, T value)
        {
            return source?.Append(value) ?? Factory(value);
        }
        
        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendOr<T>(this IEnumerable<T>? source, IEnumerable<T>? additional)
        {
            return additional is not null ? source?.Append(additional) ?? additional : source;
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
        
        public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> source, Boolean condition, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? Enumerable.Append(source, item) : source;
        }

        public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> source, Boolean condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return condition ? source.Append(additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendIf<T>(this IEnumerable<T>? source, Boolean condition, params IEnumerable<T>?[]? additionals)
        {
            return condition ? source.Append(additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendIf<T>(this IEnumerable<T>? source, T value, Boolean condition, params T[]? values)
        {
            return condition ? source.Append(value, values) : source;
        }
        
        public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> source, Func<Boolean> condition, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? Enumerable.Append(source, item) : source;
        }

        public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> source, Func<Boolean> condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return condition() ? source.Append(additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendIf<T>(this IEnumerable<T>? source, Func<Boolean> condition, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? source.Append(additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendIf<T>(this IEnumerable<T>? source, T value, Func<Boolean> condition, params T[]? values)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            return condition() ? source.Append(value, values) : source;
        }
        
        public static IEnumerable<T> AppendIfNot<T>(this IEnumerable<T> source, Boolean condition, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return !condition ? Enumerable.Append(source, item) : source;
        }
        
        public static IEnumerable<T> AppendIfNot<T>(this IEnumerable<T> source, Boolean condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return !condition ? source.Append(additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendIfNot<T>(this IEnumerable<T>? source, Boolean condition, params IEnumerable<T>?[]? additionals)
        {
            return !condition ? source.Append(additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendIfNot<T>(this IEnumerable<T>? source, Boolean condition, T value, params T[]? values)
        {
            return !condition ? source.Append(value, values) : source;
        }
        
        public static IEnumerable<T> AppendIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? Enumerable.Append(source, item) : source;
        }
        
        public static IEnumerable<T> AppendIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return !condition() ? source.Append(additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendIfNot<T>(this IEnumerable<T>? source, Func<Boolean> condition, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            return !condition() ? source.Append(additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendIfNot<T>(this IEnumerable<T>? source, Func<Boolean> condition, T value, params T[]? values)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            return !condition() ? source.Append(value, values) : source;
        }

        public static IEnumerable<T> AppendAt<T>(this IEnumerable<T> source, Int32 index, T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();
            
            Int64 i = 0;
            while (i++ < index && enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }

            yield return value;

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerable<T> AppendAt<T>(this IEnumerable<T> source, Int32 index, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return ConcatAt(source, index, additional);
        }

        public static IEnumerable<T> AppendAt<T>(this IEnumerable<T>? source, Int32 index, params IEnumerable<T>?[]? additionals)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return ConcatAt(source, index, additionals);
        }

        public static IEnumerable<T> AppendAt<T>(this IEnumerable<T>? source, Int32 index, T value, params T[]? values)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            using IEnumerator<T>? enumerator = source?.GetEnumerator();
            if (enumerator is not null)
            {
                Int64 i = 0;
                while (i++ < index && enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
            
            yield return value;
            
            if (values is not null)
            {
                foreach (T item in values)
                {
                    yield return item;
                }
            }
            
            if (enumerator is null)
            {
                yield break;
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerable<T> AppendAtIf<T>(this IEnumerable<T> source, Boolean condition, Int32 index, T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.AppendAt(index, value) : source;
        }

        public static IEnumerable<T> AppendAtIf<T>(this IEnumerable<T> source, Boolean condition, Int32 index, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.AppendAt(index, additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendAtIf<T>(this IEnumerable<T>? source, Boolean condition, Int32 index, params IEnumerable<T>?[]? additionals)
        {
            return condition ? source.AppendAt(index, additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendAtIf<T>(this IEnumerable<T>? source, Boolean condition, Int32 index, T value, params T[]? values)
        {
            return condition ? source.AppendAt(index, value, values) : source;
        }

        public static IEnumerable<T> AppendAtIf<T>(this IEnumerable<T> source, Func<Boolean> condition, Int32 index, T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? source.AppendAt(index, value) : source;
        }

        public static IEnumerable<T> AppendAtIf<T>(this IEnumerable<T> source, Func<Boolean> condition, Int32 index, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? source.AppendAt(index, additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendAtIf<T>(this IEnumerable<T>? source, Func<Boolean> condition, Int32 index, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? source.AppendAt(index, additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendAtIf<T>(this IEnumerable<T>? source, Func<Boolean> condition, Int32 index, T value, params T[]? values)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            return condition() ? source.AppendAt(index, value, values) : source;
        }

        public static IEnumerable<T> AppendAtIfNot<T>(this IEnumerable<T> source, Boolean condition, Int32 index, T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return !condition ? source.AppendAt(index, value) : source;
        }

        public static IEnumerable<T> AppendAtIfNot<T>(this IEnumerable<T> source, Boolean condition, Int32 index, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return !condition ? source.AppendAt(index, additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendAtIfNot<T>(this IEnumerable<T>? source, Boolean condition, Int32 index, params IEnumerable<T>?[]? additionals)
        {
            return !condition ? source.AppendAt(index, additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendAtIfNot<T>(this IEnumerable<T>? source, Boolean condition, Int32 index, T value, params T[]? values)
        {
            return !condition ? source.AppendAt(index, value, values) : source;
        }

        public static IEnumerable<T> AppendAtIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, Int32 index, T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? source.AppendAt(index, value) : source;
        }

        public static IEnumerable<T> AppendAtIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, Int32 index, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? source.AppendAt(index, additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendAtIfNot<T>(this IEnumerable<T>? source, Func<Boolean> condition, Int32 index, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? source.AppendAt(index, additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? AppendAtIfNot<T>(this IEnumerable<T>? source, Func<Boolean> condition, Int32 index, T value, params T[]? values)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            return !condition() ? source.AppendAt(index, value, values) : source;
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return source.Preconcat(additional);
        }
        
        public static IEnumerable<T> PrependOr<T>(this IEnumerable<T>? source, T value)
        {
            return source?.Prepend(value) ?? Factory(value);
        }
        
        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PrependOr<T>(this IEnumerable<T>? source, IEnumerable<T>? additional)
        {
            return additional is not null ? source?.Prepend(additional) ?? additional : source;
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T>? source, params IEnumerable<T>?[]? additionals)
        {
            return Preconcat(source, additionals);
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
        
        public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> source, Boolean condition, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? Enumerable.Prepend(source, item) : source;
        }

        public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> source, Boolean condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return condition ? source.Prepend(additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PrependIf<T>(this IEnumerable<T>? source, Boolean condition, params IEnumerable<T>?[]? additionals)
        {
            return condition ? source.Prepend(additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PrependIf<T>(this IEnumerable<T>? source, T value, Boolean condition, params T[]? values)
        {
            return condition ? source.Prepend(value, values) : source;
        }
        
        public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> source, Func<Boolean> condition, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? Enumerable.Prepend(source, item) : source;
        }

        public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> source, Func<Boolean> condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return condition() ? source.Prepend(additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PrependIf<T>(this IEnumerable<T>? source, Func<Boolean> condition, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? source.Prepend(additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PrependIf<T>(this IEnumerable<T>? source, T value, Func<Boolean> condition, params T[]? values)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            return condition() ? source.Prepend(value, values) : source;
        }
        
        public static IEnumerable<T> PrependIfNot<T>(this IEnumerable<T> source, Boolean condition, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return !condition ? Enumerable.Prepend(source, item) : source;
        }
        
        public static IEnumerable<T> PrependIfNot<T>(this IEnumerable<T> source, Boolean condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return !condition ? source.Prepend(additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PrependIfNot<T>(this IEnumerable<T>? source, Boolean condition, params IEnumerable<T>?[]? additionals)
        {
            return !condition ? source.Prepend(additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PrependIfNot<T>(this IEnumerable<T>? source, Boolean condition, T value, params T[]? values)
        {
            return !condition ? source.Prepend(value, values) : source;
        }
        
        public static IEnumerable<T> PrependIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? Enumerable.Prepend(source, item) : source;
        }
        
        public static IEnumerable<T> PrependIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return !condition() ? source.Prepend(additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PrependIfNot<T>(this IEnumerable<T>? source, Func<Boolean> condition, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            return !condition() ? source.Prepend(additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PrependIfNot<T>(this IEnumerable<T>? source, Func<Boolean> condition, T value, params T[]? values)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            return !condition() ? source.Prepend(value, values) : source;
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

            foreach (IEnumerable<T> next in additionals.WhereNotNull())
            {
                foreach (T item in next)
                {
                    yield return item;
                }
            }
        }
        
        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? ConcatOr<T>(this IEnumerable<T>? source, IEnumerable<T>? additional)
        {
            return additional is not null ? source?.Concat(additional) ?? additional : source;
        }

        public static IEnumerable<T> ConcatIf<T>(this IEnumerable<T> source, Boolean condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return condition ? Enumerable.Concat(source, additional) : source;
        }
        
        public static IEnumerable<T> ConcatIf<T>(this IEnumerable<T> source, Func<Boolean> condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            
            return condition() ? Enumerable.Concat(source, additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? ConcatIf<T>(this IEnumerable<T>? source, Boolean condition, params IEnumerable<T>?[]? additionals)
        {
            return condition ? source.Concat(additionals) : source;
        }
        
        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? ConcatIf<T>(this IEnumerable<T>? source, Func<Boolean> condition, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? source.Concat(additionals) : source;
        }
        
        public static IEnumerable<T> ConcatIfNot<T>(this IEnumerable<T> source, Boolean condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return !condition ? Enumerable.Concat(source, additional) : source;
        }

        public static IEnumerable<T> ConcatIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            
            return !condition() ? Enumerable.Concat(source, additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? ConcatIfNot<T>(this IEnumerable<T>? source, Boolean condition, params IEnumerable<T>?[]? additionals)
        {
            return !condition ? source.Concat(additionals) : source;
        }
        
        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? ConcatIfNot<T>(this IEnumerable<T>? source, Func<Boolean> condition, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? source.Concat(additionals) : source;
        }

        public static IEnumerable<T> ConcatAt<T>(this IEnumerable<T> source, Int32 index, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            using IEnumerator<T> enumerator = source.GetEnumerator();
            
            Int64 i = 0;
            while (i++ < index && enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }

            foreach (T item in additional)
            {
                yield return item;
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<T> ConcatAt<T>(this IEnumerable<T>? source, Int32 index, params IEnumerable<T>?[]? additionals)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            using IEnumerator<T>? enumerator = source?.GetEnumerator();
            if (enumerator is not null)
            {
                Int64 i = 0;
                while (i++ < index && enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }

            if (additionals is not null)
            {
                foreach (IEnumerable<T> enumerable in additionals.WhereNotNull())
                {
                    foreach (T item in enumerable)
                    {
                        yield return item;
                    }
                }
            }

            if (enumerator is null)
            {
                yield break;
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerable<T> ConcatAtIf<T>(this IEnumerable<T> source, Boolean condition, Int32 index, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            return condition ? source.ConcatAt(index, additional) : source;
        }
        
        public static IEnumerable<T> ConcatAtIf<T>(this IEnumerable<T> source, Func<Boolean> condition, Int32 index, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            return condition() ? source.ConcatAt(index, additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? ConcatAtIf<T>(this IEnumerable<T>? source, Boolean condition, Int32 index, params IEnumerable<T>?[]? additionals)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            return condition ? source.ConcatAt(index, additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? ConcatAtIf<T>(this IEnumerable<T>? source, Func<Boolean> condition, Int32 index, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            return condition() ? source.ConcatAt(index, additionals) : source;
        }

        public static IEnumerable<T> ConcatAtIfNot<T>(this IEnumerable<T> source, Boolean condition, Int32 index, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            return !condition ? source.ConcatAt(index, additional) : source;
        }
        
        public static IEnumerable<T> ConcatAtIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, Int32 index, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            return !condition() ? source.ConcatAt(index, additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? ConcatAtIfNot<T>(this IEnumerable<T>? source, Boolean condition, Int32 index, params IEnumerable<T>?[]? additionals)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            return !condition ? source.ConcatAt(index, additionals) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? ConcatAtIfNot<T>(this IEnumerable<T>? source, Func<Boolean> condition, Int32 index, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            return !condition() ? source.ConcatAt(index, additionals) : source;
        }

        public static IEnumerable<T> Preconcat<T>(this IEnumerable<T> source, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return Enumerable.Concat(additional, source);
        }

        public static IEnumerable<T> Preconcat<T>(this IEnumerable<T>? source, params IEnumerable<T>?[]? additionals)
        {
            if (additionals is not null)
            {
                foreach (IEnumerable<T> next in additionals.WhereNotNull())
                {
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
        
        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PreconcatOr<T>(this IEnumerable<T>? source, IEnumerable<T>? additional)
        {
            return additional is not null ? source?.Preconcat(additional) ?? additional : source;
        }
        
        public static IEnumerable<T> PreconcatIf<T>(this IEnumerable<T> source, Boolean condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return condition ? source.Preconcat(additional) : source;
        }
        
        public static IEnumerable<T> PreconcatIf<T>(this IEnumerable<T> source, Func<Boolean> condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return condition() ? source.Preconcat(additional) : source;
        }

        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PreconcatIf<T>(this IEnumerable<T>? source, Boolean condition, params IEnumerable<T>?[]? additionals)
        {
            return condition ? source.Preconcat(additionals) : source;
        }
        
        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PreconcatIf<T>(this IEnumerable<T>? source, Func<Boolean> condition, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? source.Preconcat(additionals) : source;
        }
        
        public static IEnumerable<T> PreconcatIfNot<T>(this IEnumerable<T> source, Boolean condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return !condition ? source.Preconcat(additional) : source;
        }
        
        public static IEnumerable<T> PreconcatIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, IEnumerable<T> additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (additional is null)
            {
                throw new ArgumentNullException(nameof(additional));
            }

            return !condition() ? source.Preconcat(additional) : source;
        }
        
        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PreconcatIfNot<T>(this IEnumerable<T>? source, Boolean condition, params IEnumerable<T>?[]? additionals)
        {
            return !condition ? source.Preconcat(additionals) : source;
        }
        
        [return: NotNullIfNotNull("source")]
        public static IEnumerable<T>? PreconcatIfNot<T>(this IEnumerable<T>? source, Func<Boolean> condition, params IEnumerable<T>?[]? additionals)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            
            return !condition() ? source.Preconcat(additionals) : source;
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