// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using NetExtender.Types.Tuples;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "UseDeconstructionOnParameter")]
    public static class TupleUtilities
    {
        public const Int32 TupleMaximumGeneric = 7;
        
        public static IImmutableDictionary<Type, Int32> TupleType { get; } = new HashSet<Type>
        {
            typeof(Tuple<>), typeof(Tuple<,>), typeof(Tuple<,,>), typeof(Tuple<,,,>),
            typeof(Tuple<,,,,>), typeof(Tuple<,,,,,>), typeof(Tuple<,,,,,,>), typeof(Tuple<,,,,,,,>)
        }.ToImmutableDictionary(static type => type, ReflectionUtilities.GetGenericArgumentsCount);

        public static IImmutableDictionary<Type, Int32> ValueTupleType { get; } = new HashSet<Type>
        {
            typeof(ValueTuple<>), typeof(ValueTuple<,>), typeof(ValueTuple<,,>), typeof(ValueTuple<,,,>),
            typeof(ValueTuple<,,,,>), typeof(ValueTuple<,,,,,>), typeof(ValueTuple<,,,,,,>), typeof(ValueTuple<,,,,,,,>)
        }.ToImmutableDictionary(static type => type, ReflectionUtilities.GetGenericArgumentsCount);
        
        private static class TupleCache
        {
            private static ConcurrentDictionary<Type, ImmutableArray<Func<ITuple, Object>>> Cache { get; } = new ConcurrentDictionary<Type, ImmutableArray<Func<ITuple, Object>>>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Contains(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                return Cache.TryGetValue(type, out ImmutableArray<Func<ITuple, Object>> result) && result.Length > 0;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryGetValue(Type type, out ImmutableArray<Func<ITuple, Object>> result)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                result = Cache.GetOrAdd(type, Create);
                return result.Length > 0;
            }

            private static ImmutableArray<Func<ITuple, Object>> Create(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
                
                if (!type.IsTuple(out Int32 count))
                {
                    throw new NotSupportedException();
                }
                
                Func<ITuple, Object>[] array = new Func<ITuple, Object>[count];

                for (Int32 index = 0; index < count; index++)
                {
                    array[index] = Index(type, index).Compile();
                }

                static Expression<Func<ITuple, Object>> Index(Type type, Int32 index)
                {
                    (Int32 depth, Int32 remainder) = Math.DivRem(index, 7);

                    ParameterExpression parameter = Expression.Parameter(typeof(ITuple), "tuple");
                    Expression current = Expression.Convert(parameter, type);

                    for (Int32 i = 0; i < depth; i++)
                    {
                        Type rest = TupleRestType(current.Type) ?? throw new InvalidOperationException();
                        current = Expression.PropertyOrField(current, TupleFieldName(TupleMaximumGeneric));
                        current = Expression.Convert(current, rest);
                    }

                    current = Expression.PropertyOrField(current, TupleFieldName(remainder));

                    return Expression.Lambda<Func<ITuple, Object>>(Expression.Convert(current, typeof(ITuple)), parameter);
                }

                return array.ToImmutableArray();
            }
        }

        public static Type CreateTupleType(params Type[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            if (arguments.Length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arguments), arguments.Length, null);
            }

            return arguments.Length switch
            {
                1 => typeof(Tuple<>).MakeGenericType(arguments),
                2 => typeof(Tuple<,>).MakeGenericType(arguments),
                3 => typeof(Tuple<,,>).MakeGenericType(arguments),
                4 => typeof(Tuple<,,,>).MakeGenericType(arguments),
                5 => typeof(Tuple<,,,,>).MakeGenericType(arguments),
                6 => typeof(Tuple<,,,,,>).MakeGenericType(arguments),
                7 => typeof(Tuple<,,,,,,>).MakeGenericType(arguments),
                TupleMaximumGeneric + 1 when TupleType.ContainsKey(arguments[7].TryGetGenericTypeDefinition()) => typeof(Tuple<,,,,,,,>).MakeGenericType(arguments),
                _ => typeof(Tuple<,,,,,,,>).MakeGenericType(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], CreateTupleType(arguments.Skip(7).ToArray()))
            };
        }

        public static Type CreateValueTupleType(params Type[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            if (arguments.Length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arguments), arguments.Length, null);
            }

            return arguments.Length switch
            {
                1 => typeof(ValueTuple<>).MakeGenericType(arguments),
                2 => typeof(ValueTuple<,>).MakeGenericType(arguments),
                3 => typeof(ValueTuple<,,>).MakeGenericType(arguments),
                4 => typeof(ValueTuple<,,,>).MakeGenericType(arguments),
                5 => typeof(ValueTuple<,,,,>).MakeGenericType(arguments),
                6 => typeof(ValueTuple<,,,,,>).MakeGenericType(arguments),
                7 => typeof(ValueTuple<,,,,,,>).MakeGenericType(arguments),
                TupleMaximumGeneric + 1 when ValueTupleType.ContainsKey(arguments[7].TryGetGenericTypeDefinition()) => typeof(ValueTuple<,,,,,,,>).MakeGenericType(arguments),
                _ => typeof(ValueTuple<,,,,,,,>).MakeGenericType(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], CreateValueTupleType(arguments.Skip(7).ToArray()))
            };
        }

        public static Boolean IsTuple(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type generic = type.TryGetGenericTypeDefinition();
            return TupleType.ContainsKey(generic) || ValueTupleType.ContainsKey(generic);
        }

        public static Boolean IsTuple(this Type type, out Int32 count)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type generic = type.TryGetGenericTypeDefinition();
            if (!TupleType.TryGetValue(generic, out count) && !ValueTupleType.TryGetValue(generic, out count))
            {
                return false;
            }

            if (count < 8)
            {
                return true;
            }

            if (!IsTuple(type.GetGenericArguments()[^1], out Int32 inner))
            {
                return true;
            }

            count += inner - 1;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetTupleFields(this Type type, out ImmutableArray<Func<ITuple, Object>> result)
        {
            return TupleCache.TryGetValue(type, out result);
        }

        public static String TupleFieldName(Int32 index)
        {
            return index switch
            {
                0 => "Item1",
                1 => "Item2",
                2 => "Item3",
                3 => "Item4",
                4 => "Item5",
                5 => "Item6",
                6 => "Item7",
                TupleMaximumGeneric => "Rest",
                _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
            };
        }

        public static Type? TupleRestType(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!IsTuple(type))
            {
                return null;
            }

            Type[] arguments = type.GetGenericArguments();
            return arguments.Length > TupleMaximumGeneric ? arguments[TupleMaximumGeneric] : null;
        }

        public static TupleEnumerator<T> GetEnumerator<T>(this T value) where T : ITuple
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new TupleEnumerator<T>(value);
        }
        
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
            foreach (T? item in items)
            {
                types[index++] = item?.GetType() ?? typeof(Object);
            }

            Func<Type[], Type> factory = value ? CreateValueTupleType : CreateTupleType;

            if (items.Count <= TupleMaximumGeneric)
            {
                return (ITuple?) Activator.CreateInstance(factory(types), items.Cast<Object?>().ToArray());
            }

            Object? inner = null;

            Type[] typearray = new Type[TupleMaximumGeneric + 1];
            Object?[] itemarray = new Object?[typearray.Length];
            foreach (Int32 count in types.Reverse().ZipChunk(items.Reverse().Cast<Object?>(), typearray, itemarray, TupleMaximumGeneric))
            {
                Array.Reverse(typearray, 0, TupleMaximumGeneric);
                Array.Reverse(itemarray, 0, TupleMaximumGeneric);

                if (inner is not null && count >= TupleMaximumGeneric)
                {
                    typearray[TupleMaximumGeneric] = inner.GetType();
                    itemarray[TupleMaximumGeneric] = inner;
                }

                Type type = factory(inner is not null && count >= TupleMaximumGeneric ? typearray : typearray.Slice(0, count).ToArray());
                inner = Activator.CreateInstance(type, inner is not null && count >= TupleMaximumGeneric ? itemarray : itemarray.Slice(0, count).ToArray());
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