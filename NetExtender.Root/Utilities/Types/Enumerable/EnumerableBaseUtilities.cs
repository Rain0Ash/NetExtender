using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Indexers.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Types.Random.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableBaseUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<T> GetEnumerableFrom<T>(T item)
        {
            yield return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean IsReadOnly<T>(IEnumerable<T> source)
        {
            return source switch
            {
                null => throw new ArgumentNullException(nameof(source)),
                ICollection<T> collection => collection.IsReadOnly,
                _ => true
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static Boolean AllSame<T>(IEnumerable<T> source, IEqualityComparer<T>? comparer, [MaybeNullWhen(true)] out T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= EqualityComparer<T>.Default;

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                value = default;
                return true;
            }

            value = enumerator.Current;

            while (enumerator.MoveNext())
            {
                if (!comparer.Equals(value, enumerator.Current))
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean AnyNotNull<T>(IEnumerable<T?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Any(static item => item is not null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean AllNotNull<T>(IEnumerable<T?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.All(static item => item is not null);
        }

        internal static IEnumerable<T> WhereNot<T>(IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (!predicate(item))
                {
                    yield return item;
                }
            }
        }

        internal static IEnumerable<T> WhereNot<T>(IEnumerable<T> source, Func<T, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 i = -1;
            foreach (T item in source)
            {
                if (!predicate(item, checked(++i)))
                {
                    yield return item;
                }
            }
        }

        internal static IEnumerable<T> WhereNotNull<T>(IEnumerable<T?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(static item => item is not null)!;
        }

        internal static IEnumerable<T?> WhereNotNull<T>(IEnumerable<T?> source) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(static item => item.HasValue);
        }

        internal static IEnumerable<T> WhereNotNull<T, TItem>(IEnumerable<T?> source, Func<T, TItem> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T? item in source)
            {
                if (item is not null && predicate(item) is not null)
                {
                    yield return item;
                }
            }
        }

        internal static IEnumerable<T> WhereNotNull<T>(IEnumerable<T?> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return WhereNotNull(source).Where(predicate);
        }

        internal static IEnumerable<T> WhereNotNull<T>(IEnumerable<T?> source, Func<T, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return WhereNotNull(source).Where(predicate);
        }

        internal static Int32 IndexOf<T>(IEnumerable<T> source, T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IList<T> list => list.IndexOf(value),
                IIndexer<T> indexer => indexer.IndexOf(value),
                _ => IndexOf(source, value, null)
            };
        }

        internal static Int32 IndexOf<T>(IEnumerable<T> source, T value, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= EqualityComparer<T>.Default;

            if (source is IIndexer<T> indexer && ReferenceEquals(indexer.Comparer, comparer))
            {
                return indexer.IndexOf(value);
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (comparer.Equals(item, value))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        internal static Boolean IndexOf<T>(IEnumerable<T> source, T value, out Int32 index)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            index = IndexOf(source, value);
            return index >= 0;
        }

        internal static Boolean IndexOf<T>(IEnumerable<T> source, T value, IEqualityComparer<T>? comparer, out Int32 index)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            index = IndexOf(source, value, comparer);
            return index >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (T Min, T Max) MinMax<T>(IEnumerable<T> source)
        {
            return MinMax(source, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static (T Min, T Max) MinMax<T>(IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            comparer ??= Comparer<T>.Default;

            T current = enumerator.Current;

            T min = current;
            T max = current;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (comparer.Compare(current, min) < 0)
                {
                    min = current;
                }

                if (comparer.Compare(current, max) > 0)
                {
                    max = current;
                }
            }

            return (min, max);
        }

        internal static IEnumerable<T> Where<T, TArgument>(IEnumerable<T> source, TArgument argument, Func<TArgument, T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (predicate(argument, item))
                {
                    yield return item;
                }
            }
        }

        internal static IEnumerable<T> Where<T, TArgument>(IEnumerable<T> source, TArgument argument, Func<TArgument, T, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 i = -1;
            foreach (T item in source)
            {
                if (predicate(argument, item, checked(++i)))
                {
                    yield return item;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<KeyValuePair<TKey, TValue>> WhereKeyNotNull<TKey, TValue>(IEnumerable<KeyValuePair<TKey?, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(static item => item.Key is not null)!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Random<T>(IEnumerable<T> source, [MaybeNullWhen(false)] out T result)
        {
            return Random(source, null, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean Random<T>(IEnumerable<T> source, Random? random, [MaybeNullWhen(false)] out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            random ??= new Random();

            Int64 count = 0;
            result = default!;
            foreach (T element in source)
            {
                if (random.NextInt64(count++) == 0)
                {
                    result = element;
                }
            }

            return count > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static T GetRandom<T>(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IList<T> list => ListBaseUtilities.GetRandom(list),
                IReadOnlyList<T> list => list.Count > 0 ? list[RandomUtilities.NextNonNegative(list.Count - 1)] : throw new InvalidOperationException(),
                ICollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : throw new InvalidOperationException(),
                IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : throw new InvalidOperationException(),
                _ => Random(source, out T? result) ? result : throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static T? GetRandomOrDefault<T>(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IList<T> list => ListBaseUtilities.GetRandomOrDefault(list),
                IReadOnlyList<T> list => list.Count > 0 ? list[RandomUtilities.NextNonNegative(list.Count - 1)] : default,
                ICollection<T> collection => collection.Count > 0 ? collection.ElementAtOrDefault(RandomUtilities.NextNonNegative(collection.Count - 1)) : default,
                IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAtOrDefault(RandomUtilities.NextNonNegative(collection.Count - 1)) : default,
                _ => Random(source, out T? result) ? result : default
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static T GetRandomOrDefault<T>(IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IList<T> list => ListBaseUtilities.GetRandomOrDefault(list, alternate),
                IReadOnlyList<T> list => list.Count > 0 ? list[RandomUtilities.NextNonNegative(list.Count - 1)] : alternate,
                ICollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate,
                IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate,
                _ => Random(source, out T? result) ? result : alternate
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static T GetRandomOrDefault<T>(IEnumerable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source switch
            {
                IList<T> list => ListBaseUtilities.GetRandomOrDefault(list, alternate),
                IReadOnlyList<T> list => list.Count > 0 ? list[RandomUtilities.NextNonNegative(list.Count - 1)] : alternate(),
                ICollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate(),
                IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate(),
                _ => Random(source, out T? result) ? result : alternate()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<T> Shuffle<T>(IEnumerable<T> source)
        {
            return Shuffle(source, RandomUtilities.Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static IEnumerable<T> Shuffle<T>(IEnumerable<T> source, Random random)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            T[] buffer = source.ToArray();

            for (Int32 i = 0; i < buffer.Length; i++)
            {
                Int32 j = random.Next(i, buffer.Length);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<T> Shuffle<T>(IEnumerable<T> source, IRandom random)
        {
            return Shuffle<T, IRandom>(source, random);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static IEnumerable<T> Shuffle<T, TRandom>(IEnumerable<T> source, TRandom random) where TRandom : IRandom
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            T[] buffer = source.ToArray();

            for (Int32 i = 0; i < buffer.Length; i++)
            {
                Int32 j = random.Next(i, buffer.Length);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<Maybe<T>> Maybe<T>(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<NullMaybe<T>> Nullable<T>(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<WeakMaybe<T>> Weak<T>(IEnumerable<T> source) where T : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>> Nullable<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (KeyValuePair<TKey, TValue> pair in source)
            {
                yield return pair.Nullable();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<KeyValuePair<NullMaybe<TKey>, TValue>> KeyNullable<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (KeyValuePair<TKey, TValue> pair in source)
            {
                yield return pair.KeyNullable();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<KeyValuePair<TKey, NullMaybe<TValue>>> ValueNullable<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (KeyValuePair<TKey, TValue> pair in source)
            {
                yield return pair.ValueNullable();
            }
        }

        // ReSharper disable once CognitiveComplexity
        internal static IEnumerable<Int32> ZipChunk<T1, T2>(IEnumerable<T1> first, IEnumerable<T2> second, T1[] chunk1, T2[] chunk2, Int32 length)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (chunk1 is null)
            {
                throw new ArgumentNullException(nameof(chunk1));
            }

            if (chunk2 is null)
            {
                throw new ArgumentNullException(nameof(chunk2));
            }

            Int32 chunklength = Math.Min(chunk1.Length, chunk2.Length);

            if (length > chunklength)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            if (chunklength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunklength), chunklength, null);
            }

            length = length <= 0 ? chunklength : length;

            using IEnumerator<T1> enumerator1 = first.GetEnumerator();
            using IEnumerator<T2> enumerator2 = second.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                chunk1[0] = enumerator1.Current;
                chunk2[0] = enumerator2.Current;

                Int32 i;
                for (i = 1; i < length && enumerator1.MoveNext() && enumerator2.MoveNext(); i++)
                {
                    chunk1[i] = enumerator1.Current;
                    chunk2[i] = enumerator2.Current;
                }

                if (i >= length)
                {
                    yield return i;
                    continue;
                }

                yield return i;
                yield break;
            }
        }

        // ReSharper disable once CognitiveComplexity
        internal static IEnumerable<Int32> ZipChunk<T1, T2, T3>(IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, T1[] chunk1, T2[] chunk2, T3[] chunk3, Int32 length)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (third is null)
            {
                throw new ArgumentNullException(nameof(third));
            }

            if (chunk1 is null)
            {
                throw new ArgumentNullException(nameof(chunk1));
            }

            if (chunk2 is null)
            {
                throw new ArgumentNullException(nameof(chunk2));
            }

            if (chunk3 is null)
            {
                throw new ArgumentNullException(nameof(chunk3));
            }

            Int32 chunklength = Math.Min(chunk1.Length, Math.Min(chunk2.Length, chunk3.Length));

            if (length > chunklength)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            if (chunklength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunklength), chunklength, null);
            }

            length = length <= 0 ? chunklength : length;

            using IEnumerator<T1> enumerator1 = first.GetEnumerator();
            using IEnumerator<T2> enumerator2 = second.GetEnumerator();
            using IEnumerator<T3> enumerator3 = third.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext())
            {
                chunk1[0] = enumerator1.Current;
                chunk2[0] = enumerator2.Current;
                chunk3[0] = enumerator3.Current;

                Int32 i;
                for (i = 1; i < length && enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext(); i++)
                {
                    chunk1[i] = enumerator1.Current;
                    chunk2[i] = enumerator2.Current;
                    chunk3[i] = enumerator3.Current;
                }

                if (i >= length)
                {
                    yield return i;
                    continue;
                }

                yield return i;
                yield break;
            }
        }

        // ReSharper disable once CognitiveComplexity
        internal static IEnumerable<Int32> ZipChunk<T1, T2, T3, T4>(IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, IEnumerable<T4> fourth, T1[] chunk1, T2[] chunk2, T3[] chunk3, T4[] chunk4, Int32 length)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (third is null)
            {
                throw new ArgumentNullException(nameof(third));
            }

            if (fourth is null)
            {
                throw new ArgumentNullException(nameof(fourth));
            }

            if (chunk1 is null)
            {
                throw new ArgumentNullException(nameof(chunk1));
            }

            if (chunk2 is null)
            {
                throw new ArgumentNullException(nameof(chunk2));
            }

            if (chunk3 is null)
            {
                throw new ArgumentNullException(nameof(chunk3));
            }

            if (chunk4 is null)
            {
                throw new ArgumentNullException(nameof(chunk4));
            }

            Int32 chunklength = Math.Min(Math.Min(chunk1.Length, chunk2.Length), Math.Min(chunk3.Length, chunk4.Length));

            if (length > chunklength)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            if (chunklength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunklength), chunklength, null);
            }

            length = length <= 0 ? chunklength : length;

            using IEnumerator<T1> enumerator1 = first.GetEnumerator();
            using IEnumerator<T2> enumerator2 = second.GetEnumerator();
            using IEnumerator<T3> enumerator3 = third.GetEnumerator();
            using IEnumerator<T4> enumerator4 = fourth.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() && enumerator4.MoveNext())
            {
                chunk1[0] = enumerator1.Current;
                chunk2[0] = enumerator2.Current;
                chunk3[0] = enumerator3.Current;
                chunk4[0] = enumerator4.Current;

                Int32 i;
                for (i = 1; i < length && enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() && enumerator4.MoveNext(); i++)
                {
                    chunk1[i] = enumerator1.Current;
                    chunk2[i] = enumerator2.Current;
                    chunk3[i] = enumerator3.Current;
                    chunk4[i] = enumerator4.Current;
                }

                if (i >= length)
                {
                    yield return i;
                    continue;
                }

                yield return i;
                yield break;
            }
        }
    }
}