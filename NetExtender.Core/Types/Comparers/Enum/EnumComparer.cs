// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using NetExtender.Types.Enums;
using NetExtender.Types.Enums.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Comparers
{
    public readonly struct EnumComparer<T> : IComparer<T>, IComparer<T?>, IComparer<Enum<T>>, IComparer<IEnum<T>>, IEqualityComparer<T>, IEqualityComparer<T?>, IEqualityComparer<Enum<T>>, IEqualityComparer<IEnum<T>> where T : unmanaged, Enum
    {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static unsafe class Info
        {
            public static TypeCode TypeCode { get; }
            public static readonly delegate*<T, T, Int32> Compare;
            public static readonly delegate*<Enum?, Enum?, Int32> AnyCompare;
            public static readonly delegate*<T?, T?, Int32> NullableCompare;
            public new static readonly delegate*<T, T, Boolean> Equals;
            public static readonly delegate*<Enum?, Enum?, Boolean> AnyEquals;
            public static readonly delegate*<T?, T?, Boolean> NullableEquals;
            public new static readonly delegate*<T, Int32> GetHashCode;
            public static readonly delegate*<Enum?, Int32> AnyGetHashCode;
            public static readonly delegate*<T?, Int32> NullableGetHashCode;

            static Info()
            {
                switch (TypeCode = EnumComparer.Any.GetTypeCode(typeof(T)))
                {
                    case TypeCode.SByte:
                        Compare = &EnumComparer<T, SByte>.CompareCore;
                        AnyCompare = &EnumComparer<T, SByte>.AnyCompareCore;
                        NullableCompare = &EnumComparer<T, SByte>.NullableCompareCore;
                        Equals = &EnumComparer<T, SByte>.EqualsCore;
                        AnyEquals = &EnumComparer<T, SByte>.AnyEqualsCore;
                        NullableEquals = &EnumComparer<T, SByte>.NullableEqualsCore;
                        GetHashCode = &EnumComparer<T, SByte>.GetHashCodeCore;
                        AnyGetHashCode = &EnumComparer<T, SByte>.AnyGetHashCodeCore;
                        NullableGetHashCode = &EnumComparer<T, SByte>.NullableGetHashCodeCore;
                        return;
                    case TypeCode.Byte:
                        Compare = &EnumComparer<T, Byte>.CompareCore;
                        AnyCompare = &EnumComparer<T, Byte>.AnyCompareCore;
                        NullableCompare = &EnumComparer<T, Byte>.NullableCompareCore;
                        Equals = &EnumComparer<T, Byte>.EqualsCore;
                        AnyEquals = &EnumComparer<T, Byte>.AnyEqualsCore;
                        NullableEquals = &EnumComparer<T, Byte>.NullableEqualsCore;
                        GetHashCode = &EnumComparer<T, Byte>.GetHashCodeCore;
                        AnyGetHashCode = &EnumComparer<T, Byte>.AnyGetHashCodeCore;
                        NullableGetHashCode = &EnumComparer<T, Byte>.NullableGetHashCodeCore;
                        return;
                    case TypeCode.Int16:
                        Compare = &EnumComparer<T, Int16>.CompareCore;
                        AnyCompare = &EnumComparer<T, Int16>.AnyCompareCore;
                        NullableCompare = &EnumComparer<T, Int16>.NullableCompareCore;
                        Equals = &EnumComparer<T, Int16>.EqualsCore;
                        AnyEquals = &EnumComparer<T, Int16>.AnyEqualsCore;
                        NullableEquals = &EnumComparer<T, Int16>.NullableEqualsCore;
                        GetHashCode = &EnumComparer<T, Int16>.GetHashCodeCore;
                        AnyGetHashCode = &EnumComparer<T, Int16>.AnyGetHashCodeCore;
                        NullableGetHashCode = &EnumComparer<T, Int16>.NullableGetHashCodeCore;
                        return;
                    case TypeCode.UInt16:
                        Compare = &EnumComparer<T, UInt16>.CompareCore;
                        AnyCompare = &EnumComparer<T, UInt16>.AnyCompareCore;
                        NullableCompare = &EnumComparer<T, UInt16>.NullableCompareCore;
                        Equals = &EnumComparer<T, UInt16>.EqualsCore;
                        AnyEquals = &EnumComparer<T, UInt16>.AnyEqualsCore;
                        NullableEquals = &EnumComparer<T, UInt16>.NullableEqualsCore;
                        GetHashCode = &EnumComparer<T, UInt16>.GetHashCodeCore;
                        AnyGetHashCode = &EnumComparer<T, UInt16>.AnyGetHashCodeCore;
                        NullableGetHashCode = &EnumComparer<T, UInt16>.NullableGetHashCodeCore;
                        return;
                    case TypeCode.Int32:
                        Compare = &EnumComparer<T, Int32>.CompareCore;
                        AnyCompare = &EnumComparer<T, Int32>.AnyCompareCore;
                        NullableCompare = &EnumComparer<T, Int32>.NullableCompareCore;
                        Equals = &EnumComparer<T, Int32>.EqualsCore;
                        AnyEquals = &EnumComparer<T, Int32>.AnyEqualsCore;
                        NullableEquals = &EnumComparer<T, Int32>.NullableEqualsCore;
                        GetHashCode = &EnumComparer<T, Int32>.GetHashCodeCore;
                        AnyGetHashCode = &EnumComparer<T, Int32>.AnyGetHashCodeCore;
                        NullableGetHashCode = &EnumComparer<T, Int32>.NullableGetHashCodeCore;
                        return;
                    case TypeCode.UInt32:
                        Compare = &EnumComparer<T, UInt32>.CompareCore;
                        AnyCompare = &EnumComparer<T, UInt32>.AnyCompareCore;
                        NullableCompare = &EnumComparer<T, UInt32>.NullableCompareCore;
                        Equals = &EnumComparer<T, UInt32>.EqualsCore;
                        AnyEquals = &EnumComparer<T, UInt32>.AnyEqualsCore;
                        NullableEquals = &EnumComparer<T, UInt32>.NullableEqualsCore;
                        GetHashCode = &EnumComparer<T, UInt32>.GetHashCodeCore;
                        AnyGetHashCode = &EnumComparer<T, UInt32>.AnyGetHashCodeCore;
                        NullableGetHashCode = &EnumComparer<T, UInt32>.NullableGetHashCodeCore;
                        return;
                    case TypeCode.Int64:
                        Compare = &EnumComparer<T, Int64>.CompareCore;
                        AnyCompare = &EnumComparer<T, Int64>.AnyCompareCore;
                        NullableCompare = &EnumComparer<T, Int64>.NullableCompareCore;
                        Equals = &EnumComparer<T, Int64>.EqualsCore;
                        AnyEquals = &EnumComparer<T, Int64>.AnyEqualsCore;
                        NullableEquals = &EnumComparer<T, Int64>.NullableEqualsCore;
                        GetHashCode = &EnumComparer<T, Int64>.GetHashCodeCore;
                        AnyGetHashCode = &EnumComparer<T, Int64>.AnyGetHashCodeCore;
                        NullableGetHashCode = &EnumComparer<T, Int64>.NullableGetHashCodeCore;
                        return;
                    case TypeCode.UInt64:
                        Compare = &EnumComparer<T, UInt64>.CompareCore;
                        AnyCompare = &EnumComparer<T, UInt64>.AnyCompareCore;
                        NullableCompare = &EnumComparer<T, UInt64>.NullableCompareCore;
                        Equals = &EnumComparer<T, UInt64>.EqualsCore;
                        AnyEquals = &EnumComparer<T, UInt64>.AnyEqualsCore;
                        NullableEquals = &EnumComparer<T, UInt64>.NullableEqualsCore;
                        GetHashCode = &EnumComparer<T, UInt64>.GetHashCodeCore;
                        AnyGetHashCode = &EnumComparer<T, UInt64>.AnyGetHashCodeCore;
                        NullableGetHashCode = &EnumComparer<T, UInt64>.NullableGetHashCodeCore;
                        return;
                    default:
                        throw new EnumUndefinedOrNotSupportedException<TypeCode>(TypeCode, nameof(T), null);
                }
            }
        }

        public static EnumComparer<T> Default
        {
            get
            {
                return default;
            }
        }

        public Any Interface
        {
            get
            {
                return this;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public unsafe Int32 Compare(T first, T second)
        {
            return Info.Compare(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public unsafe Int32 Compare(T? first, T? second)
        {
            return Info.NullableCompare(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public unsafe Int32 Compare(Enum? first, Enum? second)
        {
            return Info.AnyCompare(first, second);
        }

        public Int32 Compare(Enum<T>? first, Enum<T>? second)
        {
            return Compare(first?.Id, second?.Id);
        }

        public Int32 Compare(IEnum? first, IEnum? second)
        {
            return Compare(first?.Id, second?.Id);
        }

        public Int32 Compare(IEnum<T>? first, IEnum<T>? second)
        {
            return Compare(first?.Id, second?.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public unsafe Boolean Equals(T first, T second)
        {
            return Info.Equals(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public unsafe Boolean Equals(T? first, T? second)
        {
            return Info.NullableEquals(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public unsafe Boolean Equals(Enum? first, Enum? second)
        {
            return Info.AnyEquals(first, second);
        }

        public Boolean Equals(Enum<T>? first, Enum<T>? second)
        {
            return Equals(first?.Id, second?.Id);
        }

        public Boolean Equals(IEnum? first, IEnum? second)
        {
            return Equals(first?.Id, second?.Id);
        }

        public Boolean Equals(IEnum<T>? first, IEnum<T>? second)
        {
            return Equals(first?.Id, second?.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public unsafe Int32 GetHashCode(T value)
        {
            return Info.GetHashCode(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public unsafe Int32 GetHashCode(T? value)
        {
            return Info.NullableGetHashCode(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public unsafe Int32 GetHashCode(Enum? value)
        {
            return Info.AnyGetHashCode(value);
        }

        public Int32 GetHashCode(Enum<T>? value)
        {
            return GetHashCode(value?.Id);
        }

        public Int32 GetHashCode(IEnum? value)
        {
            return GetHashCode(value?.Id);
        }

        public Int32 GetHashCode(IEnum<T>? value)
        {
            return GetHashCode(value?.Id);
        }

        public readonly struct Any : IComparer<Enum>, IComparer<IEnum>, IEqualityComparer<Enum>, IEqualityComparer<IEnum>
        {
            public static implicit operator Any(EnumComparer<T> value)
            {
                return new Any(value);
            }
            
            public static implicit operator EnumComparer<T>(Any value)
            {
                return value.Comparer;
            }
            
            private readonly EnumComparer<T> Comparer;

            public Any(EnumComparer<T> comparer)
            {
                Comparer = comparer;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(T first, T second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(T? first, T? second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(Enum? first, Enum? second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(Enum<T>? first, Enum<T>? second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(IEnum? first, IEnum? second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(IEnum<T>? first, IEnum<T>? second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(T first, T second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(T? first, T? second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(Enum? first, Enum? second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(Enum<T>? first, Enum<T>? second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(IEnum? first, IEnum? second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(IEnum<T>? first, IEnum<T>? second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(T value)
            {
                return Comparer.GetHashCode(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(T? value)
            {
                return Comparer.GetHashCode(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(Enum? value)
            {
                return Comparer.GetHashCode(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(Enum<T>? value)
            {
                return Comparer.GetHashCode(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(IEnum? value)
            {
                return Comparer.GetHashCode(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(IEnum<T>? value)
            {
                return Comparer.GetHashCode(value);
            }
        }
    }

    public readonly struct EnumComparer<T, TUnderlying> : IComparer<T>, IComparer<T?>, IComparer<Enum<T>>, IComparer<IEnum<T>>, IEqualityComparer<T>, IEqualityComparer<T?>, IEqualityComparer<Enum<T>>, IEqualityComparer<IEnum<T>> where T : unmanaged, Enum where TUnderlying : unmanaged, IConvertible, IComparable<TUnderlying>, IEquatable<TUnderlying>
    {
        public static EnumComparer<T, TUnderlying> Default
        {
            get
            {
                return default;
            }
        }

        public Any Interface
        {
            get
            {
                return this;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TUnderlying Avoid(T value)
        {
            return EnumComparer.BoxAvoidance<T, TUnderlying>.Value(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Int32 CompareCore(T first, T second)
        {
            return Avoid(first).CompareTo(Avoid(second));
        }

        public Int32 Compare(T first, T second)
        {
            return CompareCore(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Int32 NullableCompareCore(T? first, T? second)
        {
            if (!first.HasValue)
            {
                return second.HasValue ? -1 : 0;
            }

            return second.HasValue ? Avoid(first.Value).CompareTo(Avoid(second.Value)) : 1;
        }

        public Int32 Compare(T? first, T? second)
        {
            return NullableCompareCore(first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Int32 AnyCompareCore(Enum? first, Enum? second)
        {
            return first switch
            {
                null when second is null => 0,
                not null when second is null => 1,
                null => -1,
                T value when second is T other => CompareCore(value, other),
                T => 1,
                _ when second is T => -1,
                _ => EnumComparer.From(first).Compare(first, second)
            };
        }
        
        public Int32 Compare(Enum? first, Enum? second)
        {
            return AnyCompareCore(first, second);
        }

        public Int32 Compare(Enum<T>? first, Enum<T>? second)
        {
            return Compare(first?.Id, second?.Id);
        }

        public Int32 Compare(IEnum? first, IEnum? second)
        {
            return Compare(first?.Id, second?.Id);
        }

        public Int32 Compare(IEnum<T>? first, IEnum<T>? second)
        {
            return Compare(first?.Id, second?.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean EqualsCore(T first, T second)
        {
            return Avoid(first).Equals(Avoid(second));
        }

        public Boolean Equals(T first, T second)
        {
            return EqualsCore(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean NullableEqualsCore(T? first, T? second)
        {
            return !first.HasValue && !second.HasValue || first.HasValue && second.HasValue && Avoid(first.Value).Equals(Avoid(second.Value));
        }

        public Boolean Equals(T? first, T? second)
        {
            return NullableEqualsCore(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean AnyEqualsCore(Enum? first, Enum? second)
        {
            return first switch
            {
                null => second is null,
                T value when second is T other => EqualsCore(value, other),
                not T when second is not null and not T => EnumComparer.From(first).Equals(first, second),
                _ => false
            };
        }

        public Boolean Equals(Enum? first, Enum? second)
        {
            return AnyEqualsCore(first, second);
        }

        public Boolean Equals(Enum<T>? first, Enum<T>? second)
        {
            return Equals(first?.Id, second?.Id);
        }

        public Boolean Equals(IEnum? first, IEnum? second)
        {
            return Equals(first?.Id, second?.Id);
        }

        public Boolean Equals(IEnum<T>? first, IEnum<T>? second)
        {
            return Equals(first?.Id, second?.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Int32 GetHashCodeCore(T value)
        {
            return value.GetHashCode();
        }

        public Int32 GetHashCode(T value)
        {
            return GetHashCodeCore(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Int32 NullableGetHashCodeCore(T? value)
        {
            return value.GetHashCode();
        }

        public Int32 GetHashCode(T? value)
        {
            return NullableGetHashCodeCore(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Int32 AnyGetHashCodeCore(Enum? value)
        {
            return value switch
            {
                null => 0,
                T @enum => GetHashCodeCore(@enum),
                _ => EnumComparer.From(value).GetHashCode(value)
            };
        }

        public Int32 GetHashCode(Enum? value)
        {
            return AnyGetHashCodeCore(value);
        }

        public Int32 GetHashCode(Enum<T>? value)
        {
            return GetHashCode(value?.Id);
        }

        public Int32 GetHashCode(IEnum? value)
        {
            return GetHashCode(value?.Id);
        }

        public Int32 GetHashCode(IEnum<T>? value)
        {
            return GetHashCode(value?.Id);
        }

        public readonly struct Any : IComparer<Enum>, IComparer<IEnum>, IEqualityComparer<Enum>, IEqualityComparer<IEnum>
        {
            public static implicit operator Any(EnumComparer<T, TUnderlying> value)
            {
                return new Any(value);
            }
            
            public static implicit operator EnumComparer<T, TUnderlying>(Any value)
            {
                return value.Comparer;
            }
            
            private readonly EnumComparer<T, TUnderlying> Comparer;

            public Any(EnumComparer<T, TUnderlying> comparer)
            {
                Comparer = comparer;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(T first, T second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(T? first, T? second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(Enum? first, Enum? second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(Enum<T>? first, Enum<T>? second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(IEnum? first, IEnum? second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 Compare(IEnum<T>? first, IEnum<T>? second)
            {
                return Comparer.Compare(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(T first, T second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(T? first, T? second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(Enum? first, Enum? second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(Enum<T>? first, Enum<T>? second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(IEnum? first, IEnum? second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Equals(IEnum<T>? first, IEnum<T>? second)
            {
                return Comparer.Equals(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(T value)
            {
                return Comparer.GetHashCode(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(T? value)
            {
                return Comparer.GetHashCode(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(Enum? value)
            {
                return Comparer.GetHashCode(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(Enum<T>? value)
            {
                return Comparer.GetHashCode(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(IEnum? value)
            {
                return Comparer.GetHashCode(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Int32 GetHashCode(IEnum<T>? value)
            {
                return Comparer.GetHashCode(value);
            }
        }
    }

    public static class EnumComparer
    {
        internal static class BoxAvoidance<TEnum, TUnderlying> where TEnum : Enum
        {
            private static Func<TEnum, TUnderlying> Wrapper { get; }

            static BoxAvoidance()
            {
                ParameterExpression parameter = Expression.Parameter(typeof(TEnum), null);
                UnaryExpression unary = Expression.ConvertChecked(parameter, typeof(TUnderlying));

                Wrapper = Expression.Lambda<Func<TEnum, TUnderlying>>(unary, parameter).Compile();
            }

            public static TUnderlying Value(TEnum value)
            {
                return Wrapper(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Any From(Enum value)
        {
            return value is not null ? new Any(value.GetType()) : throw new ArgumentNullException(nameof(value));
        }

        [SuppressMessage("ReSharper", "SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault")]
        internal readonly struct Any : IComparer<Enum>, IEqualityComparer<Enum>
        {
            public Type Type { get; }
            public TypeCode TypeCode
            {
                get
                {
                    return GetTypeCode(Type);
                }
            }

            public Any(Type type)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
            }

            internal static TypeCode GetTypeCode(Type? type)
            {
                return type is not null ? Enum.GetUnderlyingType(type).ToTypeCode() switch
                {
                    TypeCode.Empty => TypeCode.Empty,
                    TypeCode.SByte => TypeCode.SByte,
                    TypeCode.Byte => TypeCode.Byte,
                    TypeCode.Int16 => TypeCode.Int16,
                    TypeCode.UInt16 => TypeCode.UInt16,
                    TypeCode.Int32 => TypeCode.Int32,
                    TypeCode.UInt32 => TypeCode.UInt32,
                    TypeCode.Int64 => TypeCode.Int64,
                    TypeCode.UInt64 => TypeCode.UInt64,
                    var value => throw new EnumUndefinedOrNotSupportedException<TypeCode>(value, nameof(type), null)
                } : TypeCode.Empty;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static TUnderlying Avoid<TUnderlying>(Enum value)
            {
                return BoxAvoidance<Enum, TUnderlying>.Value(value);
            }

            public Int32 Compare(Enum? first, Enum? second)
            {
                if (first is null)
                {
                    return second is null ? 0 : -1;
                }

                if (second is null)
                {
                    return 1;
                }

                Type type = first.GetType();
                if (type != Type)
                {
                    if (second.GetType() == Type)
                    {
                        return -1;
                    }

                    return new Any(type).Compare(first, second);
                }

                if (second.GetType() != Type)
                {
                    return 1;
                }

                return GetTypeCode(type) switch
                {
                    TypeCode.Empty => -1,
                    TypeCode.SByte => Avoid<SByte>(first).CompareTo(Avoid<SByte>(second)),
                    TypeCode.Byte => Avoid<Byte>(first).CompareTo(Avoid<Byte>(second)),
                    TypeCode.Int16 => Avoid<Int16>(first).CompareTo(Avoid<Int16>(second)),
                    TypeCode.UInt16 => Avoid<UInt16>(first).CompareTo(Avoid<UInt16>(second)),
                    TypeCode.Int32 => Avoid<Int32>(first).CompareTo(Avoid<Int32>(second)),
                    TypeCode.UInt32 => Avoid<UInt32>(first).CompareTo(Avoid<UInt32>(second)),
                    TypeCode.Int64 => Avoid<Int64>(first).CompareTo(Avoid<Int64>(second)),
                    TypeCode.UInt64 => Avoid<UInt64>(first).CompareTo(Avoid<UInt64>(second)),
                    var value => throw new EnumUndefinedOrNotSupportedException<TypeCode>(value, nameof(type), null)
                };
            }
            
            public Boolean Equals(Enum? first, Enum? second)
            {
                if (first is null)
                {
                    return second is null;
                }

                if (second is null)
                {
                    return false;
                }

                Type type = first.GetType();
                if (type != Type)
                {
                    return second.GetType() != Type && new Any(type).Equals(first, second);
                }

                if (second.GetType() != Type)
                {
                    return false;
                }

                return GetTypeCode(type) switch
                {
                    TypeCode.Empty => false,
                    TypeCode.SByte => Avoid<SByte>(first).Equals(Avoid<SByte>(second)),
                    TypeCode.Byte => Avoid<Byte>(first).Equals(Avoid<Byte>(second)),
                    TypeCode.Int16 => Avoid<Int16>(first).Equals(Avoid<Int16>(second)),
                    TypeCode.UInt16 => Avoid<UInt16>(first).Equals(Avoid<UInt16>(second)),
                    TypeCode.Int32 => Avoid<Int32>(first).Equals(Avoid<Int32>(second)),
                    TypeCode.UInt32 => Avoid<UInt32>(first).Equals(Avoid<UInt32>(second)),
                    TypeCode.Int64 => Avoid<Int64>(first).Equals(Avoid<Int64>(second)),
                    TypeCode.UInt64 => Avoid<UInt64>(first).Equals(Avoid<UInt64>(second)),
                    var value => throw new EnumUndefinedOrNotSupportedException<TypeCode>(value, nameof(type), null)
                };
            }

            public Int32 GetHashCode(Enum? value)
            {
                return value is not null ? value.GetHashCode() : 0;
            }
        }
    }
}