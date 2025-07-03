// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Newtonsoft.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [JsonConverter(typeof(NullMaybeJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.NullMaybeJsonConverter<>))]
    public readonly struct NullMaybe<T> : IEqualityStruct<NullMaybe<T>>, INullMaybe<T>, IMaybeEquality<T, Maybe<T>>, IMaybeEquality<T, NullMaybe<T>>, ICloneable<NullMaybe<T>>, ISerializable
    {
        public static implicit operator NullMaybe<T>(Maybe<T> value)
        {
            return value.HasValue ? new NullMaybe<T>(value.Value) : default;
        }

        public static implicit operator Maybe<T>(NullMaybe<T> value)
        {
            return new Maybe<T>(value);
        }

        public static implicit operator NullMaybe<T>(T value)
        {
            return new NullMaybe<T>(value);
        }

        public static implicit operator T(NullMaybe<T> value)
        {
            return value.Value;
        }
        
        public static Boolean operator ==(T? first, NullMaybe<T> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, NullMaybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(NullMaybe<T> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(NullMaybe<T> first, T? second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(NullMaybe<T> first, NullMaybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(NullMaybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(NullMaybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator >(T? first, NullMaybe<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, NullMaybe<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(T? first, NullMaybe<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, NullMaybe<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(NullMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(NullMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(NullMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(NullMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        private readonly Maybe<T> _value;
        public T Value
        {
            get
            {
                return _value.Internal;
            }
        }

        Object? INullMaybe.Value
        {
            get
            {
                return Value;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            get
            {
                return _value.IsEmpty;
            }
        }

        public NullMaybe(T value)
        {
            _value = value;
        }
        
        private NullMaybe(SerializationInfo info, StreamingContext context)
        {
            _value = info.GetValue<T>(nameof(Value));
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), Value);
        }

        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return comparer.SafeCompare(Value, other) ?? 0;
        }

        public Int32 CompareTo(Maybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(Maybe<T> other, IComparer<T>? comparer)
        {
            return other.HasValue ? comparer.SafeCompare(Value, other.Value) ?? 0 : 1;
        }

        public Int32 CompareTo(NullMaybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(NullMaybe<T> other, IComparer<T>? comparer)
        {
            return comparer.SafeCompare(Value, other.Value) ?? 0;
        }

        public Int32 CompareTo(IMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(IMaybe<T>? other, IComparer<T>? comparer)
        {
            if (other is null)
            {
                return 1;
            }
            
            return other.HasValue ? comparer.SafeCompare(Value, other.Value) ?? 0 : 1;
        }

        public Int32 CompareTo(INullMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(INullMaybe<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? comparer.SafeCompare(Value, other.Value) ?? 0 : 1;
        }

        public override Int32 GetHashCode()
        {
            return Value is not null ? Value.GetHashCode() : 0;
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                null => Value is null,
                T value => Equals(value, comparer),
                Maybe<T> value => Equals(value, comparer),
                NullMaybe<T> value => Equals(value),
                IMaybe<T> value => Equals(value),
                INullMaybe<T> value => Equals(value),
                _ => Equals(Value, other)
            };
        }

        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Value, other);
        }

        public Boolean Equals(Maybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Maybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other.HasValue && comparer.Equals(Value, other.Value);
        }

        public Boolean Equals(NullMaybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(NullMaybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Value, other.Value);
        }

        public Boolean Equals(IMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other is not null && other.HasValue && comparer.Equals(Value, other.Value);
        }

        public Boolean Equals(INullMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(INullMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other is not null && comparer.Equals(Value, other.Value);
        }

        public NullMaybe<T> Clone()
        {
            return this;
        }

        INullMaybe<T> INullMaybe<T>.Clone()
        {
            return Clone();
        }

        INullMaybe<T> ICloneable<INullMaybe<T>>.Clone()
        {
            return Clone();
        }

        INullMaybe INullMaybe.Clone()
        {
            return Clone();
        }

        INullMaybe ICloneable<INullMaybe>.Clone()
        {
            return Clone();
        }

        IMonad<T> IMonad<T>.Clone()
        {
            return Clone();
        }

        IMonad<T> ICloneable<IMonad<T>>.Clone()
        {
            return Clone();
        }

        IMonad IMonad.Clone()
        {
            return Clone();
        }

        IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        public override String? ToString()
        {
            return Value is { } value ? value.ToString() : null;
        }

        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        public String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Value is { } value ? StringUtilities.ToString(in value, format, provider) : String.Empty;
        }

        public String? GetString()
        {
            return _value.GetString();
        }

        public String? GetString(EscapeType escape)
        {
            return _value.GetString(escape);
        }

        public String? GetString(String? format)
        {
            return _value.GetString(format);
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return _value.GetString(escape, format);
        }

        public String? GetString(IFormatProvider? provider)
        {
            return _value.GetString(provider);
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return _value.GetString(escape, provider);
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return _value.GetString(format, provider);
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return _value.GetString(escape, format, provider);
        }
    }
}