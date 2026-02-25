// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NetExtender.Interfaces;
using NetExtender.Newtonsoft.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [JsonConverter(typeof(MaybeJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.MaybeJsonConverter<>))]
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

        public static implicit operator Task<Maybe<T>>(Maybe<T> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator ValueTask<Maybe<T>>(Maybe<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<Maybe<T>>.Default;
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

        public static Boolean operator <(T? first, Maybe<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, Maybe<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(T? first, Maybe<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, Maybe<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Maybe<T> first, T? second)
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

        public static Boolean operator >(Maybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Maybe<T> first, Maybe<T> second)
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

        public static Boolean operator >(Maybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Maybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Maybe<T?> Null
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Maybe<T?>(default);
            }
        }

        private readonly Boolean? _state;
        public Boolean HasValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _state ?? false;
            }
        }

        private readonly T _value;

        //TODO: Replace to Unwrap in usages
        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        internal T Internal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value;
            }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return HasValue ? _value : throw new InvalidOperationException();
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Object? IMaybe.Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Value;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsNull
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return HasValue && _value is null;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        internal Maybe(SerializationInfo info, StreamingContext context)
        {
            _state = info.GetBoolean(nameof(HasValue));
            _value = _state is true ? info.GetValue<T>(nameof(Value)) : default!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<T> Create(T value)
        {
            return new Maybe<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap(Maybe<T> maybe)
        {
            return maybe.Internal;
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (HasValue)
            {
                value = _value;
                return true;
            }

            value = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            if (HasValue)
            {
                value = _value;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(HasValue), HasValue);

            if (HasValue)
            {
                info.AddValue(nameof(Value), _value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out T value, out Boolean notnull)
        {
            value = _value;
            notnull = HasValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Maybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(Maybe<T> other, IComparer<T>? comparer)
        {
            return other.HasValue ? HasValue ? comparer.SafeCompare(_value, other._value) ?? 0 : -1 : HasValue ? 1 : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(NullMaybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(NullMaybe<T> other, IComparer<T>? comparer)
        {
            return HasValue ? comparer.SafeCompare(_value, other.Value) ?? 0 : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IWeakMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IWeakMaybe<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? CompareTo(other.Maybe, comparer) : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return HasValue && _value is not null ? _value.GetHashCode() : 0;
        }

        public Boolean ReferenceEquals(T? other)
        {
            return default(T) is null ? HasValue && ReferenceEquals(_value, other) : Equals(other);
        }

        public Boolean ReferenceEquals(Maybe<T> other)
        {
            return default(T) is null ? HasValue == other.HasValue && ReferenceEquals(_value, other._value) : Equals(other);
        }

        public Boolean ReferenceEquals(NullMaybe<T> other)
        {
            return default(T) is null ? HasValue && ReferenceEquals(_value, other.Value) : Equals(other);
        }

        public Boolean ReferenceEquals(IMaybe<T>? other)
        {
            return other is not null && (default(T) is null ? !HasValue && !other.HasValue || HasValue && other.HasValue && ReferenceEquals(_value, other.Value) : Equals(other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(INullMaybe<T>? other)
        {
            return other is not null && ReferenceEquals(other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IWeakMaybe<T>? other)
        {
            return other is not null && ReferenceEquals(other.Maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                IWeakMaybe<T> value => Equals(value, comparer),
                _ when _value is not null => HasValue && _value.Equals(other),
                _ => false
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return HasValue && comparer.Equals(_value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Maybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Maybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return !HasValue && !other.HasValue || HasValue && other.HasValue && comparer.Equals(_value, other._value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(NullMaybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(NullMaybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return HasValue && comparer.Equals(_value, other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(INullMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(INullMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other is not null && HasValue && comparer.Equals(_value, other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IWeakMaybe<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IWeakMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Maybe, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Maybe<T> Clone()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMaybe<T> IMaybe<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMaybe<T> ICloneable<IMaybe<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMaybe IMaybe.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMaybe ICloneable<IMaybe>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad<T> IMonad<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad<T> ICloneable<IMonad<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad IMonad.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Object ICloneable.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override String? ToString()
        {
            return HasValue && _value is not null ? _value.ToString() : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return HasValue && _value is not null ? StringUtilities.ToString(in _value, format, provider) : String.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString()
        {
            return HasValue ? IStringProvider.Default.GetString(_value) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape)
        {
            return HasValue ? IStringProvider.Default.GetString(_value, escape) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format)
        {
            return HasValue ? IStringProvider.Default.GetString(_value, format) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format)
        {
            return HasValue ? IStringProvider.Default.GetString(_value, escape, format) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(IFormatProvider? provider)
        {
            return HasValue ? IStringProvider.Default.GetString(_value, provider) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return HasValue ? IStringProvider.Default.GetString(_value, escape, provider) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format, IFormatProvider? provider)
        {
            return HasValue ? IStringProvider.Default.GetString(_value, format, provider) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return HasValue ? IStringProvider.Default.GetString(_value, escape, format, provider) : null;
        }
    }

    public static class Maybe
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Maybe<T> Create<T>(T value)
        {
            return Maybe<T>.Create(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap<T>(Maybe<T> maybe)
        {
            return Maybe<T>.Unwrap(maybe);
        }
    }
}