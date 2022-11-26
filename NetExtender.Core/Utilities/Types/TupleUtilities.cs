// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "UseDeconstructionOnParameter")]
    public static class TupleUtilities
    {
        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            yield return value.Item1;
        }

        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            yield return value.Item1;
            yield return value.Item2;
        }

        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
        }

        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
        }

        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
        }

        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T, T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
        }

        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T, T, T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
        }

#pragma warning disable CS8714
        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T, T, T, T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
            yield return value.Rest;
        }
#pragma warning restore CS8714

        public static IEnumerable<T> AsEnumerable<T, TRest>(this Tuple<T, T, T, T, T, T, T, TRest> value) where TRest : ITuple
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            for (Int32 i = 0; i < value.Rest.Length; i++)
            {
                Object? item = value.Rest[i];

                if (item is not null && item is not T && !ConvertUtilities.IsChangeType<T>(item))
                {
                    throw new InvalidOperationException($"Rest value must contains only items of type {typeof(T).Name}");
                }
            }

            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;

            for (Int32 i = 0; i < value.Rest.Length; i++)
            {
                Object? item = value.Rest[i];
                yield return item is not null ? item is T result ? result : ConvertUtilities.ChangeType<T>(item) : default!;
            }
        }

        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T> value)
        {
            yield return value.Item1;
        }

        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
        }

        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
        }

        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
        }

        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T, T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
        }

        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T, T, T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
        }

        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T, T, T, T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
        }

#pragma warning disable CS8714
        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T, T, T, T, T, T> value) where T : struct
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
            yield return value.Rest;
        }
#pragma warning restore CS8714

        public static IEnumerable<T> AsEnumerable<T, TRest>(this ValueTuple<T, T, T, T, T, T, T, TRest> value) where TRest : struct, ITuple
        {
            for (Int32 i = 0; i < value.Rest.Length; i++)
            {
                Object? item = value.Rest[i];

                if (item is not null && item is not T && !ConvertUtilities.IsChangeType<T>(item))
                {
                    throw new InvalidOperationException($"Rest value must contains only items of type {typeof(T).Name}");
                }
            }

            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;

            for (Int32 i = 0; i < value.Rest.Length; i++)
            {
                Object? item = value.Rest[i];
                yield return item is not null ? item is T result ? result : ConvertUtilities.ChangeType<T>(item) : default!;
            }
        }

        public static IEnumerable<Object?> AsEnumerable(this ITuple value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            for (Int32 i = 0; i < value.Length; i++)
            {
                yield return value[i];
            }
        }

        public static IEnumerable<T> AsEnumerable<T>(this ITuple value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            for (Int32 i = 0; i < value.Length; i++)
            {
                Object? item = value[i];

                if (item is not null && item is not T && !ConvertUtilities.IsChangeType<T>(item))
                {
                    throw new InvalidOperationException($"Rest value must contains only items of type {typeof(T).Name}");
                }
            }

            for (Int32 i = 0; i < value.Length; i++)
            {
                Object? item = value[i];
                yield return item is not null ? item is T result ? result : ConvertUtilities.ChangeType<T>(item) : default!;
            }
        }

        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T>? value)
        {
            return value;
        }

        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T>? value)
        {
            return value;
        }

        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T>? value)
        {
            return value;
        }

        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T, T>? value)
        {
            return value;
        }

        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T, T, T>? value)
        {
            return value;
        }

        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T, T, T, T>? value)
        {
            return value;
        }

        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T, T, T, T, T>? value)
        {
            return value;
        }

#pragma warning disable CS8714
        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T, T, T, T, T, T>? value)
        {
            return value;
        }
#pragma warning restore CS8714

        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T, TRest>(this Tuple<T, T, T, T, T, T, T, TRest>? value) where TRest : ITuple
        {
            return value;
        }

        public static ITuple AsTuple<T>(this ValueTuple<T> value)
        {
            return value;
        }

        public static ITuple AsTuple<T>(this ValueTuple<T, T> value)
        {
            return value;
        }

        public static ITuple AsTuple<T>(this ValueTuple<T, T, T> value)
        {
            return value;
        }

        public static ITuple AsTuple<T>(this ValueTuple<T, T, T, T> value)
        {
            return value;
        }

        public static ITuple AsTuple<T>(this ValueTuple<T, T, T, T, T> value)
        {
            return value;
        }

        public static ITuple AsTuple<T>(this ValueTuple<T, T, T, T, T, T> value)
        {
            return value;
        }

        public static ITuple AsTuple<T>(this ValueTuple<T, T, T, T, T, T, T> value)
        {
            return value;
        }

#pragma warning disable CS8714
        public static ITuple AsTuple<T>(this ValueTuple<T, T, T, T, T, T, T, T> value) where T : struct
        {
            return value;
        }
#pragma warning restore CS8714

        public static ITuple AsTuple<T, TRest>(this ValueTuple<T, T, T, T, T, T, T, TRest> value) where TRest : struct, ITuple
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ITuple? AsTuple<T>(this IEnumerable<T> source, Boolean value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            LinkedList<T> items = new LinkedList<T>(source);

            if (items.Count <= 0)
            {
                return null;
            }

            Int32 index = 0;
            Type[] types = new Type[items.Count];
            foreach (T item in items)
            {
                types[index++] = item?.GetType() ?? typeof(Object);
            }

            Func<Type[], Type> factory = value ? GenericTypeUtilities.CreateValueTupleType : GenericTypeUtilities.CreateTupleType;

            if (items.Count <= GenericTypeUtilities.TupleMaximumGeneric)
            {
                return (ITuple?) Activator.CreateInstance(factory(types), items.Cast<Object?>().ToArray());
            }

            Object? inner = null;

            Type[] typearray = new Type[GenericTypeUtilities.TupleMaximumGeneric + 1];
            Object?[] itemarray = new Object?[typearray.Length];
            foreach (Int32 count in types.Reverse().ZipChunk(items.Reverse().Cast<Object?>(), typearray, itemarray, GenericTypeUtilities.TupleMaximumGeneric))
            {
                Array.Reverse(typearray, 0, GenericTypeUtilities.TupleMaximumGeneric);
                Array.Reverse(itemarray, 0, GenericTypeUtilities.TupleMaximumGeneric);

                if (inner is not null && count >= GenericTypeUtilities.TupleMaximumGeneric)
                {
                    typearray[GenericTypeUtilities.TupleMaximumGeneric] = inner.GetType();
                    itemarray[GenericTypeUtilities.TupleMaximumGeneric] = inner;
                }

                Type type = factory(inner is not null && count >= GenericTypeUtilities.TupleMaximumGeneric ? typearray : typearray.Slice(0, count).ToArray());
                inner = Activator.CreateInstance(type, inner is not null && count >= GenericTypeUtilities.TupleMaximumGeneric ? itemarray : itemarray.Slice(0, count).ToArray());
            }

            return (ITuple?) inner;
        }

        public static ITuple? AsTuple(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AsTuple(source.Cast<Object?>());
        }

        public static ITuple? AsTuple<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AsTuple(source, false);
        }

        public static ITuple? AsValueTuple(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AsValueTuple(source.Cast<Object?>());
        }

        public static ITuple? AsValueTuple<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AsTuple(source, true);
        }

        public static KeyValuePair<TKey, TValue> ToPair<TKey, TValue>(this ValueTuple<TKey, TValue> value)
        {
            return new KeyValuePair<TKey, TValue>(value.Item1, value.Item2);
        }

        public static KeyValuePair<TKey, TValue> ToPair<TKey, TValue>(this Tuple<TKey, TValue> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new KeyValuePair<TKey, TValue>(value.Item1, value.Item2);
        }

        public static ValueTuple<T1, T2, T3> Append<T1, T2, T3>(this ValueTuple<T1, T2> value, T3 item)
        {
            return new ValueTuple<T1, T2, T3>(value.Item1, value.Item2, item);
        }

        public static ValueTuple<T1, T2, T3, T4> Append<T1, T2, T3, T4>(this ValueTuple<T1, T2> value, T3 item1, T4 item2)
        {
            return new ValueTuple<T1, T2, T3, T4>(value.Item1, value.Item2, item1, item2);
        }

        public static ValueTuple<T1, T2, T3, T4, T5> Append<T1, T2, T3, T4, T5>(this ValueTuple<T1, T2> value, T3 item1, T4 item2, T5 item3)
        {
            return new ValueTuple<T1, T2, T3, T4, T5>(value.Item1, value.Item2, item1, item2, item3);
        }

        public static ValueTuple<T1, T2, T3, T4> Append<T1, T2, T3, T4>(this ValueTuple<T1, T2, T3> value, T4 item)
        {
            return new ValueTuple<T1, T2, T3, T4>(value.Item1, value.Item2, value.Item3, item);
        }

        public static ValueTuple<T1, T2, T3, T4, T5> Append<T1, T2, T3, T4, T5>(this ValueTuple<T1, T2, T3> value, T4 item1, T5 item2)
        {
            return new ValueTuple<T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, item1, item2);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6> Append<T1, T2, T3, T4, T5, T6>(this ValueTuple<T1, T2, T3> value, T4 item1, T5 item2, T6 item3)
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, item1, item2, item3);
        }

        public static ValueTuple<T1, T2, T3, T4, T5> Append<T1, T2, T3, T4, T5>(this ValueTuple<T1, T2, T3, T4> value, T5 item)
        {
            return new ValueTuple<T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, value.Item4, item);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6> Append<T1, T2, T3, T4, T5, T6>(this ValueTuple<T1, T2, T3, T4> value, T5 item1, T6 item2)
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, item1, item2);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> Append<T1, T2, T3, T4, T5, T6, T7>(this ValueTuple<T1, T2, T3, T4> value, T5 item1, T6 item2, T7 item3)
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, item1, item2, item3);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6> Append<T1, T2, T3, T4, T5, T6>(this ValueTuple<T1, T2, T3, T4, T5> value, T6 item)
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, item);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> Append<T1, T2, T3, T4, T5, T6, T7>(this ValueTuple<T1, T2, T3, T4, T5> value, T6 item1, T7 item2)
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, item1, item2);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> Append<T1, T2, T3, T4, T5, T6, T7, TRest>(this ValueTuple<T1, T2, T3, T4, T5> value, T6 item1, T7 item2, TRest rest) where TRest : struct
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, item1, item2, rest);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> Append<T1, T2, T3, T4, T5, T6, T7>(this ValueTuple<T1, T2, T3, T4, T5, T6> value, T7 item)
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, item);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> Append<T1, T2, T3, T4, T5, T6, T7, TRest>(this ValueTuple<T1, T2, T3, T4, T5, T6> value, T7 item, TRest rest) where TRest : struct
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, item, rest);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> Append<T1, T2, T3, T4, T5, T6, T7, TRest>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7> value, TRest rest) where TRest : struct
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, rest);
        }

        public static Tuple<T1, T2, T3> Append<T1, T2, T3>(this Tuple<T1, T2> value, T3 item)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3>(value.Item1, value.Item2, item);
        }

        public static Tuple<T1, T2, T3, T4> Append<T1, T2, T3, T4>(this Tuple<T1, T2> value, T3 item1, T4 item2)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4>(value.Item1, value.Item2, item1, item2);
        }

        public static Tuple<T1, T2, T3, T4, T5> Append<T1, T2, T3, T4, T5>(this Tuple<T1, T2> value, T3 item1, T4 item2, T5 item3)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5>(value.Item1, value.Item2, item1, item2, item3);
        }

        public static Tuple<T1, T2, T3, T4> Append<T1, T2, T3, T4>(this Tuple<T1, T2, T3> value, T4 item)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4>(value.Item1, value.Item2, value.Item3, item);
        }

        public static Tuple<T1, T2, T3, T4, T5> Append<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3> value, T4 item1, T5 item2)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, item1, item2);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6> Append<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3> value, T4 item1, T5 item2, T6 item3)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, item1, item2, item3);
        }

        public static Tuple<T1, T2, T3, T4, T5> Append<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4> value, T5 item)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, value.Item4, item);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6> Append<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4> value, T5 item1, T6 item2)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, item1, item2);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7> Append<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4> value, T5 item1, T6 item2, T7 item3)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, item1, item2, item3);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6> Append<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5> value, T6 item)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, item);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7> Append<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5> value, T6 item1, T7 item2)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, item1, item2);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> Append<T1, T2, T3, T4, T5, T6, T7, TRest>(this Tuple<T1, T2, T3, T4, T5> value, T6 item1, T7 item2, TRest rest) where TRest : struct
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, item1, item2, rest);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7> Append<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6> value, T7 item)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, item);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> Append<T1, T2, T3, T4, T5, T6, T7, TRest>(this Tuple<T1, T2, T3, T4, T5, T6> value, T7 item, TRest rest) where TRest : struct
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, item, rest);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> Append<T1, T2, T3, T4, T5, T6, T7, TRest>(this Tuple<T1, T2, T3, T4, T5, T6, T7> value, TRest rest) where TRest : struct
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, rest);
        }
    }
}