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

        public static implicit operator Task<Maybe<T>>(WeakMaybe<T> value)
        {
            return value.Maybe;
        }

        public static implicit operator ValueTask<Maybe<T>>(WeakMaybe<T> value)
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

        public static implicit operator Task<WeakMaybe<T>>(WeakMaybe<T> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<WeakMaybe<T>>.Default;
        }

        public static implicit operator ValueTask<WeakMaybe<T>>(WeakMaybe<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<WeakMaybe<T>>.Default;
        }

        public static implicit operator WeakReference<T>?(WeakMaybe<T> value)
        {
            return value.Reference;
        }

        public static implicit operator Task<WeakReference<T>?>(WeakMaybe<T> value)
        {
            WeakReference<T>? reference = value.Reference;
            return reference is not null ? Task.FromResult<WeakReference<T>?>(reference) : TaskUtilities<WeakReference<T>?>.Default;
        }

        public static implicit operator ValueTask<WeakReference<T>?>(WeakMaybe<T> value)
        {
            WeakReference<T>? reference = value.Reference;
            return reference is not null ? ValueTask.FromResult<WeakReference<T>?>(reference) : ValueTaskUtilities<WeakReference<T>?>.Default;
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
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value.HasValue;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        internal T? Internal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value.Internal is { } reference && reference.TryGetTarget(out T? target) ? target : null;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public WeakReference<T>? Reference
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value.Internal;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Maybe<T> Maybe
        {
            get
            {
                return _value.HasValue ? _value.Internal is { } reference ? reference.TryGetTarget(out T? target) ? new Maybe<T>(target) : default : new Maybe<T>(null!) : default;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Maybe<Object?> IWeakMaybe.Maybe
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Maybe.HasValue ? new Maybe<Object?>(Maybe.Internal) : default;
            }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value.Internal is { } reference ? reference.TryGetTarget(out T? target) ? target : throw new InvalidOperationException() : null!;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Object? IWeakMaybe.Value
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
                return _value.IsNull;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsAlive
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value.HasValue && _value.Internal?.TryGetTarget(out _) is not false;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            return Maybe.Unwrap(out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Maybe.GetObjectData(info, context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(WeakReference<T>? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(WeakReference<T>? other, IComparer<T>? comparer)
        {
            return other is not null && other.TryGetTarget(out T? target) ? CompareTo(target, comparer) : CompareTo(default(T), comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Maybe<T> other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Maybe<T> other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(NullMaybe<T> other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(NullMaybe<T> other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(WeakMaybe<T> other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(WeakMaybe<T> other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other.Maybe, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IMaybe<T>? other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(INullMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(INullMaybe<T>? other, IComparer<T>? comparer)
        {
            return Maybe.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IWeakMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IWeakMaybe<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? Maybe.CompareTo(other.Maybe, comparer) : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return Maybe.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(T? other)
        {
            return Maybe.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(WeakReference<T>? other)
        {
            return other is not null && other.TryGetTarget(out T? target) && Maybe.ReferenceEquals(target);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(Maybe<T> other)
        {
            return Maybe.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(NullMaybe<T> other)
        {
            return Maybe.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(WeakMaybe<T> other)
        {
            return Maybe.ReferenceEquals(other.Maybe);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IMaybe<T>? other)
        {
            return Maybe.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(INullMaybe<T>? other)
        {
            return Maybe.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IWeakMaybe<T>? other)
        {
            return Maybe.ReferenceEquals(other);
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
                Maybe<T> value => Equals(value),
                NullMaybe<T> value => Equals(value),
                WeakMaybe<T> value => Equals(value.Maybe),
                WeakReference<T> value => Equals(value),
                _ => Maybe.Equals(other)
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
            return Maybe.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(WeakReference<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(WeakReference<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && other.TryGetTarget(out T? target) ? Equals(target, comparer) : Equals(default(T), comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Maybe<T> other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Maybe<T> other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(NullMaybe<T> other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(NullMaybe<T> other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(WeakMaybe<T> other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(WeakMaybe<T> other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other.Maybe, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IMaybe<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(INullMaybe<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(INullMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IWeakMaybe<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IWeakMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            return Maybe.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WeakMaybe<T> Clone()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IWeakMaybe<T> IWeakMaybe<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IWeakMaybe<T> ICloneable<IWeakMaybe<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IWeakMaybe IWeakMaybe.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IWeakMaybe ICloneable<IWeakMaybe>.Clone()
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
            return Maybe.ToString();
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
            return Maybe.ToString(format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString()
        {
            return Maybe.GetString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape)
        {
            return Maybe.GetString(escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format)
        {
            return Maybe.GetString(format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format)
        {
            return Maybe.GetString(escape, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(IFormatProvider? provider)
        {
            return Maybe.GetString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return Maybe.GetString(escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format, IFormatProvider? provider)
        {
            return Maybe.GetString(format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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