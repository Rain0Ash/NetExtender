// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.NewtonSoft.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [JsonConverter(typeof(MaybeJsonConverter<>))]
    public readonly struct Maybe<T> : IEqualityStruct<Maybe<T>>, IMaybe<T>, IMaybeEquality<T, Maybe<T>>, IMaybeEquality<T, NullMaybe<T>>, ICloneable<Maybe<T>>, ISerializable
    {
        public static implicit operator Boolean(Maybe<T> value)
        {
            return value.HasValue;
        }

        public static explicit operator T(Maybe<T> value)
        {
            return value.Value;
        }

        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        public static Boolean operator ==(T? first, Maybe<T> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, Maybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(Maybe<T> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Maybe<T> first, T? second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(Maybe<T> first, NullMaybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Maybe<T> first, NullMaybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator >(T? first, Maybe<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, Maybe<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(T? first, Maybe<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, Maybe<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Maybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Maybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(Maybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Maybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Maybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Maybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(Maybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Maybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        private readonly Boolean? _state;
        public Boolean HasValue
        {
            get
            {
                return _state ?? false;
            }
        }

        private readonly T _value;
        internal T Internal
        {
            get
            {
                return _value;
            }
        }
        
        public T Value
        {
            get
            {
                return HasValue ? _value : throw new InvalidOperationException();
            }
        }
        
        Object? IMaybe.Value
        {
            get
            {
                return Value;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return _state is null;
            }
        }

        public Maybe(T value)
        {
            _state = true;
            _value = value;
        }
        
        private Maybe(SerializationInfo info, StreamingContext context)
        {
            _state = info.GetBoolean(nameof(HasValue));
            _value = _state is true ? info.GetValue<T>(nameof(Value)) : default!;
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(HasValue), HasValue);

            if (HasValue)
            {
                info.AddValue(nameof(Value), _value);
            }
        }

        public void Deconstruct(out T value, out Boolean notnull)
        {
            value = _value;
            notnull = HasValue;
        }

        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            if (other is null)
            {
                return !HasValue ? 0 : comparer.SafeCompare(_value, other) ?? 0;
            }

            return HasValue ? comparer.SafeCompare(_value, other) ?? 0 : -1;
        }

        public Int32 CompareTo(Maybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(Maybe<T> other, IComparer<T>? comparer)
        {
            return other.HasValue ? HasValue ? comparer.SafeCompare(_value, other._value) ?? 0 : -1 : HasValue ? 1 : 0;
        }

        public Int32 CompareTo(NullMaybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(NullMaybe<T> other, IComparer<T>? comparer)
        {
            return HasValue ? comparer.SafeCompare(_value, other.Value) ?? 0 : -1;
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
            
            return other.HasValue ? HasValue ? comparer.SafeCompare(_value, other.Value) ?? 0 : -1 : HasValue ? 1 : 0;
        }

        public Int32 CompareTo(INullMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(INullMaybe<T>? other, IComparer<T>? comparer)
        {
            if (other is null)
            {
                return 1;
            }
            
            return HasValue ? comparer.SafeCompare(_value, other.Value) ?? 0 : -1;
        }

        public override Int32 GetHashCode()
        {
            return HasValue && _value is not null ? _value.GetHashCode() : 0;
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                null => !HasValue || _value is null || _value.Equals(other),
                T value => Equals(value, comparer),
                Maybe<T> value => Equals(value, comparer),
                NullMaybe<T> value => Equals(value, comparer),
                IMaybe<T> value => Equals(value, comparer),
                INullMaybe<T> value => Equals(value, comparer),
                _ when _value is not null => HasValue && _value.Equals(other),
                _ => false
            };
        }

        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return HasValue && comparer.Equals(_value, other);
        }

        public Boolean Equals(Maybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Maybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return !HasValue && !other.HasValue || HasValue && other.HasValue && comparer.Equals(_value, other._value);
        }

        public Boolean Equals(NullMaybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(NullMaybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return HasValue && comparer.Equals(_value, other.Value);
        }

        public Boolean Equals(IMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            if (other is null)
            {
                return false;
            }
            
            comparer ??= EqualityComparer<T>.Default;
            return !HasValue && !other.HasValue || HasValue && other.HasValue && comparer.Equals(_value, other.Value);
        }

        public Boolean Equals(INullMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(INullMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other is not null && HasValue && comparer.Equals(_value, other.Value);
        }

        public Maybe<T> Clone()
        {
            return this;
        }

        IMaybe<T> IMaybe<T>.Clone()
        {
            return Clone();
        }

        IMaybe<T> ICloneable<IMaybe<T>>.Clone()
        {
            return Clone();
        }

        IMaybe IMaybe.Clone()
        {
            return Clone();
        }

        IMaybe ICloneable<IMaybe>.Clone()
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
            return HasValue && _value is not null ? _value.ToString() : null;
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
            return HasValue && _value is not null ? StringUtilities.ToString(in _value, format, provider) : String.Empty;
        }

        public String? GetString()
        {
            return HasValue ? _value.GetString() : null;
        }

        public String? GetString(EscapeType escape)
        {
            return HasValue ? _value.GetString(escape) : null;
        }

        public String? GetString(String? format)
        {
            return HasValue ? _value.GetString(format) : null;
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return HasValue ? _value.GetString(escape, format) : null;
        }

        public String? GetString(IFormatProvider? provider)
        {
            return HasValue ? _value.GetString(provider) : null;
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return HasValue ? _value.GetString(escape, provider) : null;
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return HasValue ? _value.GetString(format, provider) : null;
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return HasValue ? _value.GetString(escape, format, provider) : null;
        }
    }
}