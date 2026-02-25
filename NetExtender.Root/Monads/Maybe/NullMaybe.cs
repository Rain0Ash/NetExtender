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
            return value.HasValue ? new NullMaybe<T>(value.Internal) : default;
        }

        public static implicit operator Maybe<T>(NullMaybe<T> value)
        {
            return new Maybe<T>(value);
        }

        public static implicit operator Task<Maybe<T>>(NullMaybe<T> value)
        {
            return !value.IsEmpty ? Task.FromResult<Maybe<T>>(value) : TaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator ValueTask<Maybe<T>>(NullMaybe<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<Maybe<T>>(value) : ValueTaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator NullMaybe<T>(T value)
        {
            return new NullMaybe<T>(value);
        }

        public static implicit operator T(NullMaybe<T> value)
        {
            return value.Value;
        }

        public static implicit operator Task<NullMaybe<T>>(NullMaybe<T> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<NullMaybe<T>>.Default;
        }

        public static implicit operator ValueTask<NullMaybe<T>>(NullMaybe<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<NullMaybe<T>>.Default;
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

        public static Boolean operator <(T? first, NullMaybe<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, NullMaybe<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(T? first, NullMaybe<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, NullMaybe<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(NullMaybe<T> first, T? second)
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

        public static Boolean operator >(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(NullMaybe<T> first, NullMaybe<T> second)
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

        public static Boolean operator >(NullMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(NullMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        private readonly Maybe<T> _value;
        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value.Internal;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Object? INullMaybe.Value
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
                return _value.Internal is null;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullMaybe<T> Create(T value)
        {
            return new NullMaybe<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap(NullMaybe<T> maybe)
        {
            return maybe.Value;
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (Value is { } result)
            {
                value = result;
                return true;
            }

            value = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            if (Value is { } result)
            {
                value = result;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return comparer.SafeCompare(Value, other) ?? 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Maybe<T> other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Maybe<T> other, IComparer<T>? comparer)
        {
            return other.HasValue ? comparer.SafeCompare(Value, other.Value) ?? 0 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(NullMaybe<T> other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(NullMaybe<T> other, IComparer<T>? comparer)
        {
            return comparer.SafeCompare(Value, other.Value) ?? 0;
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

            return other.HasValue ? comparer.SafeCompare(Value, other.Value) ?? 0 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(INullMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(INullMaybe<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? comparer.SafeCompare(Value, other.Value) ?? 0 : 1;
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
            return Value is not null ? Value.GetHashCode() : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(T? other)
        {
            return ReferenceEquals(Value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(Maybe<T> other)
        {
            return other.HasValue && ReferenceEquals(Value, other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(NullMaybe<T> other)
        {
            return ReferenceEquals(other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IMaybe<T>? other)
        {
            return other is not null && other.HasValue && ReferenceEquals(other.Value);
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
                null => Value is null,
                T value => Equals(value, comparer),
                Maybe<T> value => Equals(value, comparer),
                NullMaybe<T> value => Equals(value),
                IMaybe<T> value => Equals(value),
                INullMaybe<T> value => Equals(value),
                IWeakMaybe<T> value => Equals(value),
                _ => Equals(Value, other)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Maybe<T> other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Maybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other.HasValue && comparer.Equals(Value, other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(NullMaybe<T> other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(NullMaybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Value, other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IMaybe<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other is not null && other.HasValue && comparer.Equals(Value, other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(INullMaybe<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(INullMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other is not null && comparer.Equals(Value, other.Value);
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
        public NullMaybe<T> Clone()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        INullMaybe<T> INullMaybe<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        INullMaybe<T> ICloneable<INullMaybe<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        INullMaybe INullMaybe.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        INullMaybe ICloneable<INullMaybe>.Clone()
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
            return Value is { } value ? value.ToString() : null;
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
            return Value is { } value ? StringUtilities.ToString(in value, format, provider) : String.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString()
        {
            return _value.GetString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape)
        {
            return _value.GetString(escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format)
        {
            return _value.GetString(format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format)
        {
            return _value.GetString(escape, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(IFormatProvider? provider)
        {
            return _value.GetString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return _value.GetString(escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format, IFormatProvider? provider)
        {
            return _value.GetString(format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return _value.GetString(escape, format, provider);
        }
    }

    public static class NullMaybe
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullMaybe<T> Create<T>(T value)
        {
            return NullMaybe<T>.Create(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap<T>(NullMaybe<T> maybe)
        {
            return NullMaybe<T>.Unwrap(maybe);
        }
    }
}