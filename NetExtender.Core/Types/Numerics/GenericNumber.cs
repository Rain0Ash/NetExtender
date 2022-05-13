// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Types.Numerics.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Numerics
{
    public readonly struct GenericNumber : IGenericNumber
    {
        public static GenericNumber Create<T>(T value) where T : struct, IConvertible
        {
            return new GenericNumber<T>(value);
        }
        
        public static implicit operator GenericNumber(SByte value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(Byte value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(Int16 value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(UInt16 value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(Int32 value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(UInt32 value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(Int64 value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(UInt64 value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(Single value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(Double value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(Decimal value)
        {
            return Create(value);
        }
        
        public static implicit operator GenericNumber(Enum value)
        {
            return new GenericNumber(value);
        }

        public static Boolean operator ==(GenericNumber first, GenericNumber second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(GenericNumber first, GenericNumber second)
        {
            return !(first == second);
        }

        public static Boolean operator <(GenericNumber first, GenericNumber second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator >(GenericNumber first, GenericNumber second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator <=(GenericNumber first, GenericNumber second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >=(GenericNumber first, GenericNumber second)
        {
            return first.CompareTo(second) >= 0;
        }

        private IGenericNumber Value { get; }

        public Type UnderlyingType
        {
            get
            {
                return Value.UnderlyingType;
            }
        }

        public Int32 Size
        {
            get
            {
                return Value.Size;
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class GenericCache
        {
            private static ConcurrentDictionary<Type, Func<ValueType, IGenericNumber>> Constructors { get; }
            
            static GenericCache()
            {
                Constructors = new ConcurrentDictionary<Type, Func<ValueType, IGenericNumber>>();
            }

            private static Func<ValueType, IGenericNumber> CreateExpression(Type type)
            {
                if (!type.IsValueType)
                {
                    throw new InvalidOperationException($"Type {type} is not a value type.");
                }
            
                if (!type.HasInterface(typeof(IConvertible)))
                {
                    throw new InvalidOperationException($"Type {type} is not IConvertible");
                }
                
                ConstructorInfo? info = typeof(GenericNumberWrapper<>).MakeGenericType(type).GetConstructor(new[] { type });

                if (info is null)
                {
                    throw new InvalidOperationException($"Unknown constructor for type {nameof(GenericNumber)}Wrapper<{type}>");
                }

                ParameterExpression argument = Expression.Parameter(typeof(ValueType));
                UnaryExpression body = Expression.Convert(Expression.New(info, Expression.Convert(argument, type)), typeof(IGenericNumber));
                return Expression.Lambda<Func<ValueType, IGenericNumber>>(body, argument).Compile();
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static IGenericNumber Create<T>(T value) where T : struct
            {
                return Constructors.GetOrAdd(typeof(T), CreateExpression).Invoke(value);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static IGenericNumber Create(ValueType value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return Constructors.GetOrAdd(value.GetType(), CreateExpression).Invoke(value);
            }
        }

        internal GenericNumber(IGenericNumber value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public GenericNumber(ValueType value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = GenericCache.Create(value);
        }

        public TypeCode GetTypeCode()
        {
            return Value.GetTypeCode();
        }

        public Boolean ToBoolean(IFormatProvider? provider)
        {
            return Value.ToBoolean(provider);
        }

        public Char ToChar(IFormatProvider? provider)
        {
            return Value.ToChar(provider);
        }

        public SByte ToSByte(IFormatProvider? provider)
        {
            return Value.ToSByte(provider);
        }

        public Byte ToByte(IFormatProvider? provider)
        {
            return Value.ToByte(provider);
        }

        public Int16 ToInt16(IFormatProvider? provider)
        {
            return Value.ToInt16(provider);
        }

        public UInt16 ToUInt16(IFormatProvider? provider)
        {
            return Value.ToUInt16(provider);
        }

        public Int32 ToInt32(IFormatProvider? provider)
        {
            return Value.ToInt32(provider);
        }

        public UInt32 ToUInt32(IFormatProvider? provider)
        {
            return Value.ToUInt32(provider);
        }

        public Int64 ToInt64(IFormatProvider? provider)
        {
            return Value.ToInt64(provider);
        }

        public UInt64 ToUInt64(IFormatProvider? provider)
        {
            return Value.ToUInt64(provider);
        }

        public Single ToSingle(IFormatProvider? provider)
        {
            return Value.ToSingle(provider);
        }

        public Double ToDouble(IFormatProvider? provider)
        {
            return Value.ToDouble(provider);
        }

        public Decimal ToDecimal(IFormatProvider? provider)
        {
            return Value.ToDecimal(provider);
        }

        public DateTime ToDateTime(IFormatProvider? provider)
        {
            return Value.ToDateTime(provider);
        }

        public override String? ToString()
        {
            return Value.ToString();
        }

        public String ToString(IFormatProvider? provider)
        {
            return Value.ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Value.ToString(format, provider);
        }

        Object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            return Value.ToType(conversionType, provider);
        }

        public override Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is GenericNumber number && Equals(number);
        }

        public Boolean Equals(ValueType? other)
        {
            return Value.Equals(other);
        }

        public Boolean Equals(GenericNumber other)
        {
            Decimal first = Value.ToDecimal(CultureInfo.InvariantCulture);
            Decimal second = other.Value.ToDecimal(CultureInfo.InvariantCulture);
            return first == second;
        }

        public Int32 CompareTo(Object? obj)
        {
            return Value.CompareTo(obj);
        }

        public Int32 CompareTo(ValueType? other)
        {
            return Value.CompareTo(other);
        }

        public Int32 CompareTo(GenericNumber other)
        {
            Decimal first = Value.ToDecimal(CultureInfo.InvariantCulture);
            Decimal second = other.Value.ToDecimal(CultureInfo.InvariantCulture);
            return first.CompareTo(second);
        }

        public Boolean TryFormat(Span<Char> destination, out Int32 written, ReadOnlySpan<Char> format, IFormatProvider? provider)
        {
            if (Value is ISpanFormattable formattable)
            {
                return formattable.TryFormat(destination, out written, format, provider);
            }

            written = 0;
            return false;
        }
    }

    public readonly struct GenericNumber<T> : IGenericNumber<T> where T : struct, IConvertible
    {
        public static implicit operator GenericNumber(GenericNumber<T> value)
        {
            return new GenericNumber((IGenericNumber) new GenericNumberWrapper<T>(value));
        }

        public static implicit operator GenericNumber<T>(T value)
        {
            return new GenericNumber<T>(value);
        }

        public static implicit operator T(GenericNumber<T> value)
        {
            return value.Value;
        }
        
        public static Boolean operator ==(GenericNumber<T> first, GenericNumber<T> second)
        {
            return first.Equals(second.Value);
        }
        
        public static Boolean operator ==(GenericNumber<T> first, GenericNumber second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator !=(GenericNumber<T> first, GenericNumber<T> second)
        {
            return !(first == second);
        }
        
        public static Boolean operator !=(GenericNumber<T> first, GenericNumber second)
        {
            return !(first == second);
        }

        public static Boolean operator <(GenericNumber<T> first, GenericNumber<T> second)
        {
            return first.CompareTo(second.Value) < 0;
        }
        
        public static Boolean operator <(GenericNumber<T> first, GenericNumber second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator >(GenericNumber<T> first, GenericNumber<T> second)
        {
            return first.CompareTo(second.Value) > 0;
        }
        
        public static Boolean operator >(GenericNumber<T> first, GenericNumber second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator <=(GenericNumber<T> first, GenericNumber<T> second)
        {
            return first.CompareTo(second.Value) <= 0;
        }
        
        public static Boolean operator <=(GenericNumber<T> first, GenericNumber second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >=(GenericNumber<T> first, GenericNumber<T> second)
        {
            return first.CompareTo(second.Value) >= 0;
        }
        
        public static Boolean operator >=(GenericNumber<T> first, GenericNumber second)
        {
            return first.CompareTo(second) >= 0;
        }

        public Type UnderlyingType
        {
            get
            {
                return typeof(T);
            }
        }

        public Int32 Size
        {
            get
            {
                return Marshal.SizeOf<T>();
            }
        }

        public T Value { get; }

        public GenericNumber(T value)
        {
            Value = value;
        }
        
        public GenericNumber Generic()
        {
            return this;
        }

        public TypeCode GetTypeCode()
        {
            return Value.GetTypeCode();
        }

        Boolean IConvertible.ToBoolean(IFormatProvider? provider)
        {
            return Value.ToBoolean(provider);
        }

        Char IConvertible.ToChar(IFormatProvider? provider)
        {
            return Value.ToChar(provider);
        }

        SByte IConvertible.ToSByte(IFormatProvider? provider)
        {
            return Value.ToSByte(provider);
        }

        Byte IConvertible.ToByte(IFormatProvider? provider)
        {
            return Value.ToByte(provider);
        }

        Int16 IConvertible.ToInt16(IFormatProvider? provider)
        {
            return Value.ToInt16(provider);
        }

        UInt16 IConvertible.ToUInt16(IFormatProvider? provider)
        {
            return Value.ToUInt16(provider);
        }

        Int32 IConvertible.ToInt32(IFormatProvider? provider)
        {
            return Value.ToInt32(provider);
        }

        UInt32 IConvertible.ToUInt32(IFormatProvider? provider)
        {
            return Value.ToUInt32(provider);
        }

        Int64 IConvertible.ToInt64(IFormatProvider? provider)
        {
            return Value.ToInt64(provider);
        }

        UInt64 IConvertible.ToUInt64(IFormatProvider? provider)
        {
            return Value.ToUInt64(provider);
        }

        Single IConvertible.ToSingle(IFormatProvider? provider)
        {
            return Value.ToSingle(provider);
        }

        Double IConvertible.ToDouble(IFormatProvider? provider)
        {
            return Value.ToDouble(provider);
        }

        Decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            return Value.ToDecimal(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        {
            return Value.ToDateTime(provider);
        }

        public override String? ToString()
        {
            return Value.ToString();
        }

        public String ToString(IFormatProvider? provider)
        {
            return Value.ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            if (Value is IFormattable formattable)
            {
                return formattable.ToString(format, provider);
            }
            
            return Value.ToString(provider);
        }

        Object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            return Value.ToType(conversionType, provider);
        }

        public override Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return Value.Equals(obj);
        }

        public Boolean Equals(ValueType? other)
        {
            return Equals((Object?) other);
        }

        public Boolean Equals(T other)
        {
            return Value.Equals(other);
        }

        public Boolean Equals(GenericNumber other)
        {
            return new GenericNumberWrapper<T>(this).Equals(other);
        }

        public Int32 CompareTo(Object? other)
        {
            if (Value is IComparable comparable)
            {
                return comparable.CompareTo(other);
            }

            return Value.ToDecimal(CultureInfo.InvariantCulture).CompareTo(other);
        }

        public Int32 CompareTo(T other)
        {
            if (Value is IComparable<T> comparable)
            {
                return comparable.CompareTo(other);
            }

            Decimal first = Value.ToDecimal(CultureInfo.InvariantCulture);
            Decimal second = other.ToDecimal(CultureInfo.InvariantCulture);
            return first.CompareTo(second);
        }

        public Int32 CompareTo(ValueType? other)
        {
            return CompareTo((Object?) other);
        }

        public Int32 CompareTo(GenericNumber other)
        {
            return new GenericNumberWrapper<T>(this).CompareTo(other);
        }

        public Boolean TryFormat(Span<Char> destination, out Int32 written, ReadOnlySpan<Char> format, IFormatProvider? provider)
        {
            if (Value is ISpanFormattable formattable)
            {
                return formattable.TryFormat(destination, out written, format, provider);
            }

            written = 0;
            return false;
        }
    }

    internal readonly struct GenericNumberWrapper<T> : IGenericNumber where T : struct, IConvertible
    {
        private GenericNumber<T> Internal { get; }

        public Type UnderlyingType
        {
            get
            {
                return Internal.UnderlyingType;
            }
        }

        public Int32 Size
        {
            get
            {
                return Internal.Size;
            }
        }

        public T Value
        {
            get
            {
                return Internal.Value;
            }
        }

        public GenericNumberWrapper(T value)
        {
            Internal = new GenericNumber<T>(value);
        }

        public GenericNumberWrapper(GenericNumber<T> value)
        {
            Internal = value;
        }

        public TypeCode GetTypeCode()
        {
            return Internal.GetTypeCode();
        }

        Boolean IConvertible.ToBoolean(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToBoolean(provider);
        }

        Char IConvertible.ToChar(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToChar(provider);
        }

        SByte IConvertible.ToSByte(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToSByte(provider);
        }

        Byte IConvertible.ToByte(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToByte(provider);
        }

        Int16 IConvertible.ToInt16(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToInt16(provider);
        }

        UInt16 IConvertible.ToUInt16(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToUInt16(provider);
        }

        Int32 IConvertible.ToInt32(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToInt32(provider);
        }

        UInt32 IConvertible.ToUInt32(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToUInt32(provider);
        }

        Int64 IConvertible.ToInt64(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToInt64(provider);
        }

        UInt64 IConvertible.ToUInt64(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToUInt64(provider);
        }

        Single IConvertible.ToSingle(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToSingle(provider);
        }

        Double IConvertible.ToDouble(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToDouble(provider);
        }

        Decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToDecimal(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToDateTime(provider);
        }

        public override String? ToString()
        {
            return Internal.ToString();
        }

        public String ToString(IFormatProvider? provider)
        {
            return Internal.ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Internal.ToString(format, provider);
        }

        Object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            return ((IConvertible) Internal).ToType(conversionType, provider);
        }

        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return Internal.Equals(obj);
        }

        public Boolean Equals(ValueType? other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(GenericNumber other)
        {
            return new GenericNumber((IGenericNumber) this).Equals(other);
        }

        public Int32 CompareTo(Object? obj)
        {
            return Internal.CompareTo(obj);
        }

        public Int32 CompareTo(ValueType? other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(GenericNumber other)
        {
            return new GenericNumber((IGenericNumber) this).CompareTo(other);
        }

        public Boolean TryFormat(Span<Char> destination, out Int32 charsWritten, ReadOnlySpan<Char> format, IFormatProvider? provider)
        {
            return Internal.TryFormat(destination, out charsWritten, format, provider);
        }
    }
}