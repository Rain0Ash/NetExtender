// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Newtonsoft.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [JsonConverter(typeof(WeakMaybeJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.WeakMaybeJsonConverter<>))]
    public readonly struct WeakMaybe<T> : IEqualityStruct<WeakMaybe<T>>, IWeakMaybe<T>, IMaybeEquality<T, WeakReference<T>>, IMaybeEquality<T, Maybe<T>>, IMaybeEquality<T, NullMaybe<T>>, IMaybeEquality<T, WeakMaybe<T>>, ICloneable<WeakMaybe<T>>, ISerializable where T : class?
    {
        public static implicit operator WeakMaybe<T>(Maybe<T> value)
        {
            return value.HasValue ? new WeakMaybe<T>(value.Internal) : default;
        }

        public static implicit operator Maybe<T>(WeakMaybe<T> value)
        {
            return value.Maybe;
        }

        public static implicit operator WeakMaybe<T>(T value)
        {
            return new WeakMaybe<T>(value);
        }

        public static implicit operator T(WeakMaybe<T> value)
        {
            return value.Value;
        }

        public static implicit operator WeakMaybe<T>(WeakReference<T>? value)
        {
            return new WeakMaybe<T>(value);
        }

        public static implicit operator WeakReference<T>?(WeakMaybe<T> value)
        {
            return value.Reference;
        }

        public static Boolean operator ==(T? first, WeakMaybe<T> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, WeakMaybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(WeakMaybe<T> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(WeakMaybe<T> first, T? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(WeakMaybe<T> first, WeakMaybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(WeakMaybe<T> first, WeakMaybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(WeakMaybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(WeakMaybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator <(T? first, WeakMaybe<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, WeakMaybe<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(T? first, WeakMaybe<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, WeakMaybe<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(WeakMaybe<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(WeakMaybe<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(WeakMaybe<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(WeakMaybe<T> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(WeakMaybe<T> first, WeakMaybe<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(WeakMaybe<T> first, WeakMaybe<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(WeakMaybe<T> first, WeakMaybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(WeakMaybe<T> first, WeakMaybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(WeakMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(WeakMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(WeakMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(WeakMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        private readonly Maybe<WeakReference<T>?> _value;

        public Boolean HasValue
        {
            get
            {
                return _value.HasValue;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        internal T? Internal
        {
            get
            {
                return _value.Internal is { } reference && reference.TryGetTarget(out T? target) ? target : null;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public WeakReference<T>? Reference
        {
            get
            {
                return _value.Internal;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Maybe<T> Maybe
        {
            get
            {
                return _value.HasValue ? _value.Internal is { } reference ? reference.TryGetTarget(out T? target) ? new Maybe<T>(target) : default : new Maybe<T>(null!) : default;
            }
        }

        Maybe<Object?> IWeakMaybe.Maybe
        {
            get
            {
                return Maybe.HasValue ? new Maybe<Object?>(Maybe.Internal) : default;
            }
        }

        public T Value
        {
            get
            {
                return _value.Internal is { } reference ? reference.TryGetTarget(out T? target) ? target : throw new InvalidOperationException() : null!;
            }
        }

        Object? IWeakMaybe.Value
        {
            get
            {
                return Value;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsNull
        {
            get
            {
                return _value.IsNull;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsAlive
        {
            get
            {
                return _value.HasValue && _value.Internal?.TryGetTarget(out _) is not false;
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

        public WeakMaybe(T value)
        {
            _value = value is not null ? new WeakReference<T>(value) : null;
        }

        public WeakMaybe(WeakReference<T>? value)
        {
            _value = value;
        }

        private WeakMaybe(SerializationInfo info, StreamingContext context)
        {
            _value = new Maybe<T>(info, context) is { HasValue: true, Internal: var @object } ? @object is not null ? new WeakReference<T>(@object) : null : default(Maybe<WeakReference<T>?>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WeakMaybe<T> Create(T value)
        {
            return new WeakMaybe<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap(WeakMaybe<T> maybe)
        {
            return Maybe<T>.Unwrap(maybe.Maybe);
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (Maybe.Unwrap(out T? result))
            {
                value = result;
                return true;
            }

            value = null;
            return false;
        }

        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            return Maybe.Unwrap(out value);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Maybe.GetObjectData(info, context);
        }

        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other, comparer);
        }

        public Int32 CompareTo(WeakReference<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(WeakReference<T>? other, IComparer<T>? comparer)
        {
            return other is not null && other.TryGetTarget(out T? target) ? CompareTo(target, comparer) : CompareTo(default(T), comparer);
        }

        public Int32 CompareTo(Maybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(Maybe<T> other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other, comparer);
        }

        public Int32 CompareTo(NullMaybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(NullMaybe<T> other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other, comparer);
        }

        public Int32 CompareTo(WeakMaybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(WeakMaybe<T> other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other.Maybe, comparer);
        }

        public Int32 CompareTo(IMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(IMaybe<T>? other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other, comparer);
        }

        public Int32 CompareTo(INullMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(INullMaybe<T>? other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other, comparer);
        }

        public Int32 CompareTo(IWeakMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(IWeakMaybe<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? Maybe.CompareTo(other.Maybe, comparer) : 1;
        }

        public override Int32 GetHashCode()
        {
            return Maybe.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                WeakMaybe<T> value => Equals(value.Maybe),
                WeakReference<T> value => Equals(value),
                _ => Maybe.Equals(other)
            };
        }

        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        public Boolean Equals(WeakReference<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(WeakReference<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && other.TryGetTarget(out T? target) ? Equals(target, comparer) : Equals(default(T), comparer);
        }

        public Boolean Equals(Maybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Maybe<T> other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        public Boolean Equals(NullMaybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(NullMaybe<T> other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        public Boolean Equals(WeakMaybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(WeakMaybe<T> other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other.Maybe, comparer);
        }

        public Boolean Equals(IMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        public Boolean Equals(INullMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(INullMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        public Boolean Equals(IWeakMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IWeakMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        public WeakMaybe<T> Clone()
        {
            return this;
        }

        IWeakMaybe<T> IWeakMaybe<T>.Clone()
        {
            return Clone();
        }

        IWeakMaybe<T> ICloneable<IWeakMaybe<T>>.Clone()
        {
            return Clone();
        }

        IWeakMaybe IWeakMaybe.Clone()
        {
            return Clone();
        }

        IWeakMaybe ICloneable<IWeakMaybe>.Clone()
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
            return Maybe.ToString();
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
            return Maybe.ToString(format, provider);
        }

        public String? GetString()
        {
            return Maybe.GetString();
        }

        public String? GetString(EscapeType escape)
        {
            return Maybe.GetString(escape);
        }

        public String? GetString(String? format)
        {
            return Maybe.GetString(format);
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return Maybe.GetString(escape, format);
        }

        public String? GetString(IFormatProvider? provider)
        {
            return Maybe.GetString(provider);
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return Maybe.GetString(escape, provider);
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return Maybe.GetString(format, provider);
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return Maybe.GetString(escape, format, provider);
        }
    }

    public static class WeakMaybe
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WeakMaybe<T> Create<T>(T value) where T : class
        {
            return WeakMaybe<T>.Create(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Unwrap<T>(WeakMaybe<T> maybe) where T : class
        {
            return WeakMaybe<T>.Unwrap(maybe);
        }
    }
}