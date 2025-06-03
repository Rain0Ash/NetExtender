// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Collections;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        /// <summary>
        /// Gets collection count if <see cref="source"/> is materialized, otherwise null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        [Pure]
        public static Int32? CountIfMaterialized<T>([JetBrains.Annotations.NoEnumeration] this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

#if NET6_0_OR_GREATER
            return source.TryGetNonEnumeratedCount(out Int32 count) ? count : source is IReadOnlyCollection<T> collection ? collection.Count : null;
#else
            return source switch
            {
                ICollection<T> collection => collection.Count,
                IReadOnlyCollection<T> collection => collection.Count,
                ICollection collection => collection.Count,
                _ => null
            };
#endif
        }

        [Pure]
        public static Boolean CountIfMaterialized<T>([JetBrains.Annotations.NoEnumeration] this IEnumerable<T> source, out Int32 count)
        {
            Int32? result = CountIfMaterialized(source);
            count = result ?? 0;
            return result is not null;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CountIfMaterialized<T>([JetBrains.Annotations.NoEnumeration] this IEnumerable<T> source, Int32 count)
        {
            return CountIfMaterialized(source, out Int32 result) && result >= 0 ? result : count;
        }

        [Pure]
        public static Boolean IsMaterialized<T>([JetBrains.Annotations.NoEnumeration] this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return IsMaterialized(source, out _);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMaterialized<T>([JetBrains.Annotations.NoEnumeration] this IEnumerable<T> source, [NotNullWhen(true)] out Int32? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            count = CountIfMaterialized(source);
            return count is not null;
        }

        public static IReadOnlyCollection<T> Materialize<T>(this IEnumerable<T>? source)
        {
            return source switch
            {
                null => Array.Empty<T>(),
                IReadOnlyCollection<T> collection => collection,
                ICollection<T> collection => new CollectionReadOnlyWrapper<T>(collection),
                _ => source.ToArray()
            };
        }

        public static IReadOnlyCollection<T> Materialize<T>(this IEnumerable<T>? source, out Int32 count)
        {
            switch (source)
            {
                case null:
                    count = 0;
                    return Array.Empty<T>();
                case IReadOnlyCollection<T> collection:
                    count = collection.Count;
                    return collection;
                case ICollection<T> collection:
                    count = collection.Count;
                    return new CollectionReadOnlyWrapper<T>(collection);
                default:
                    T[] result = source.ToArray();
                    count = result.Length;
                    return result;
            }
        }

        public static IReadOnlyCollection<T> Materialize<T>(this IEnumerable<T>? source, out Int64 count)
        {
            switch (source)
            {
                case null:
                    count = 0;
                    return Array.Empty<T>();
                case IReadOnlyCollection<T> collection:
                    count = collection.Count;
                    return collection;
                case ICollection<T> collection:
                    count = collection.Count;
                    return new CollectionReadOnlyWrapper<T>(collection);
                default:
                    T[] result = source.ToArray();
                    count = result.LongLength;
                    return result;
            }
        }

        public static IEnumerable<T> MaterializeIf<T>(this IEnumerable<T> source, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? Materialize(source) : source;
        }

        public static IEnumerable<T> MaterializeIf<T>(this IEnumerable<T> source, Boolean condition, out Int32? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!condition)
            {
                count = default;
                return source;
            }

            source = Materialize(source, out Int32 result);
            count = result;
            return source;
        }

        public static IEnumerable<T> MaterializeIf<T>(this IEnumerable<T> source, Boolean condition, out Int64? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!condition)
            {
                count = default;
                return source;
            }

            source = Materialize(source, out Int64 result);
            count = result;
            return source;
        }

        public static IEnumerable<T> MaterializeIf<T>(this IEnumerable<T> source, Func<Boolean> condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return MaterializeIf(source, condition());
        }

        public static IEnumerable<T> MaterializeIf<T>(this IEnumerable<T> source, Func<Boolean> condition, out Int32? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return MaterializeIf(source, condition(), out count);
        }

        public static IEnumerable<T> MaterializeIf<T>(this IEnumerable<T> source, Func<Boolean> condition, out Int64? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return MaterializeIf(source, condition(), out count);
        }

        public static IEnumerable<T> MaterializeIfNot<T>(this IEnumerable<T> source, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return !condition ? Materialize(source) : source;
        }

        public static IEnumerable<T> MaterializeIfNot<T>(this IEnumerable<T> source, Boolean condition, out Int32? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition)
            {
                count = default;
                return source;
            }

            source = Materialize(source, out Int32 result);
            count = result;
            return source;
        }

        public static IEnumerable<T> MaterializeIfNot<T>(this IEnumerable<T> source, Boolean condition, out Int64? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition)
            {
                count = default;
                return source;
            }

            source = Materialize(source, out Int64 result);
            count = result;
            return source;
        }

        public static IEnumerable<T> MaterializeIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return MaterializeIfNot(source, condition());
        }

        public static IEnumerable<T> MaterializeIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, out Int32? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return MaterializeIfNot(source, condition(), out count);
        }

        public static IEnumerable<T> MaterializeIfNot<T>(this IEnumerable<T> source, Func<Boolean> condition, out Int64? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return MaterializeIfNot(source, condition(), out count);
        }

        [Pure]
        public static Int32? CountIfMaterializedByReflection([JetBrains.Annotations.NoEnumeration] this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.TryGetPropertyValue("Length", out Int32 length) ? length :
                source.TryGetPropertyValue("Count", out length) ? length :
                source.TryGetPropertyValue("LongLength", out length) ? length :
                source.TryGetPropertyValue("LongCount", out length) ? length : null;
        }

        [Pure]
        public static Int64? LongCountIfMaterializedByReflection([JetBrains.Annotations.NoEnumeration] this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.TryGetPropertyValue("LongLength", out Int64 length) ? length :
                source.TryGetPropertyValue("LongCount", out length) ? length :
                source.TryGetPropertyValue("Length", out length) ? length :
                source.TryGetPropertyValue("Count", out length) ? length : null;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMaterializedByReflection([JetBrains.Annotations.NoEnumeration] this IEnumerable source)
        {
            return IsMaterializedByReflection(source, out Int32? _);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMaterializedByReflection([JetBrains.Annotations.NoEnumeration] this IEnumerable source, [NotNullWhen(true)] out Int32? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            count = CountIfMaterializedByReflection(source);
            return count is not null;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMaterializedByReflection([JetBrains.Annotations.NoEnumeration] this IEnumerable source, [NotNullWhen(true)] out Int64? count)
        {
            return IsLongMaterializedByReflection(source, out count);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLongMaterializedByReflection([JetBrains.Annotations.NoEnumeration] this IEnumerable source)
        {
            return IsLongMaterializedByReflection(source, out _);
        }

        [Pure]
        public static Boolean IsLongMaterializedByReflection([JetBrains.Annotations.NoEnumeration] this IEnumerable source, [NotNullWhen(true)] out Int64? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            count = LongCountIfMaterializedByReflection(source);
            return count is not null;
        }

        /// <summary>
        /// Gets collection if <see cref="source"/> is materialized, otherwise ToArray();ed collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="nullToEmpty"></param>
        [Pure]
        public static IEnumerable<T> Materialize<T>(this IEnumerable<T> source, Boolean nullToEmpty)
        {
            return source switch
            {
                null when nullToEmpty => Enumerable.Empty<T>(),
                null => throw new ArgumentNullException(nameof(source)),
                ICollection<T> => source,
                IReadOnlyCollection<T> => source,
                _ => source.ToArray()
            };
        }

        public static IEnumerable<T> Dematerialize<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            using IEnumerator<T> enumerator = source.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
}