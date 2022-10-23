// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static class GenericTypeUtilities
    {
        public static Type KeyValuePairType { get; } = typeof(KeyValuePair<,>);
        public static Type MaybeType { get; } = typeof(Maybe<>);
        public static Type NullMaybeType { get; } = typeof(NullMaybe<>);

        public const Int32 TupleMaximumGeneric = 7;
        public static IImmutableDictionary<Type, Int32> TupleType { get; } = new HashSet<Type>
        {
            typeof(Tuple<>), typeof(Tuple<,>), typeof(Tuple<,,>), typeof(Tuple<,,,>),
            typeof(Tuple<,,,,>), typeof(Tuple<,,,,,>), typeof(Tuple<,,,,,,>), typeof(Tuple<,,,,,,,>)
        }.ToImmutableDictionary(type => type, ReflectionUtilities.GetGenericArgumentsCount);
        
        public static IImmutableDictionary<Type, Int32> ValueTupleType { get; } = new HashSet<Type>
        {
            typeof(ValueTuple<>), typeof(ValueTuple<,>), typeof(ValueTuple<,,>), typeof(ValueTuple<,,,>),
            typeof(ValueTuple<,,,,>), typeof(ValueTuple<,,,,,>), typeof(ValueTuple<,,,,,,>), typeof(ValueTuple<,,,,,,,>)
        }.ToImmutableDictionary(type => type, ReflectionUtilities.GetGenericArgumentsCount);
        
        public static IImmutableSet<Type> MemorySpanType { get; } = new HashSet<Type>
        {
            typeof(Memory<>), typeof(ReadOnlyMemory<>), typeof(Span<>), typeof(ReadOnlySpan<>)
        }.ToImmutableHashSet();

        public static Type CreateTupleType(params Type[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            if (arguments.Length <= 0)
            {
                throw new ArgumentOutOfRangeException();
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
                8 when TupleType.ContainsKey(arguments[7].TryGetGenericTypeDefinition()) => typeof(Tuple<,,,,,,,>).MakeGenericType(arguments),
                _ => typeof(Tuple<,,,,,,,>).MakeGenericType(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], CreateTupleType(arguments.Skip(7).ToArray())),
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
                throw new ArgumentOutOfRangeException();
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
                8 when ValueTupleType.ContainsKey(arguments[7].TryGetGenericTypeDefinition()) => typeof(ValueTuple<,,,,,,,>).MakeGenericType(arguments),
                _ => typeof(ValueTuple<,,,,,,,>).MakeGenericType(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], CreateValueTupleType(arguments.Skip(7).ToArray())),
            };
        }

        public static Boolean IsTuple(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type generic = type.TryGetGenericTypeDefinition();
            return TupleType.ContainsKey(generic) || ValueTupleType.ContainsKey(generic);
        }

        public static Boolean IsTuple(Type type, out Int32 count)
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

            if (!IsTuple(type.GetGenericArguments().Last(), out Int32 inner))
            {
                return true;
            }
            
            count += inner - 1;
            return true;
        }

        public static Boolean IsMemorySpan(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type generic = type.TryGetGenericTypeDefinition();
            return MemorySpanType.Contains(generic);
        }
    }
}