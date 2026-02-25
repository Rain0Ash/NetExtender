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
using NetExtender.Types.Times;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [JsonConverter(typeof(DebounceJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(Serialization.Json.Monads.DebounceJsonConverter<>))]
    public readonly struct Debounce<T> : IEqualityStruct<Debounce<T>>, IDebounce<T>, IDebounceEquality<T, Debounce<T>>, ICloneable<Debounce<T>>, ISerializable
    {
        public static implicit operator T(Debounce<T> value)
        {
            return value._value;
        }

        public static implicit operator Debounce<T>(T value)
        {
            return new Debounce<T>(value);
        }

        public static implicit operator Task<Debounce<T>>(Debounce<T> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<Debounce<T>>.Default;
        }

        public static implicit operator ValueTask<Debounce<T>>(Debounce<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<Debounce<T>>.Default;
        }

        public static Boolean operator ==(T? first, Debounce<T> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, Debounce<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(Debounce<T> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Debounce<T> first, T? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(Debounce<T> first, Debounce<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Debounce<T> first, Debounce<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator <(T? first, Debounce<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, Debounce<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(T? first, Debounce<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, Debounce<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(Debounce<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Debounce<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Debounce<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Debounce<T> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(Debounce<T> first, Debounce<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Debounce<T> first, Debounce<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Debounce<T> first, Debounce<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Debounce<T> first, Debounce<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public const DateTimeKind DefaultDateTimeKind = DateTimeKind.Utc;

        private readonly DateTimeProvider _provider;
        private DateTimeProvider Provider
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _provider ? _provider : DateTimeProvider.Utc.Provider;
            }
        }

        private readonly T _value;
        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value;
            }
        }

        private DateTime Now
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Provider.Now;
            }
        }

        public TimeSpan Time
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Now - SetTime;
            }
        }

        private readonly TimeSpan? _delay;
        public TimeSpan Delay
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Normalize(_delay ?? TimeSpan.Zero);
            }
            init
            {
                _delay = value >= TimeSpan.Zero ? value : throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public DateTime SetTime { get; init; }

        public DateTimeKind TimeKind
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Provider.Kind;
            }
        }

        public Boolean IsDebounce
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Time < Delay;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _delay is null && SetTime == default;
            }
        }

        public Debounce(TimeSpan delay)
            : this(delay, DefaultDateTimeKind)
        {
        }

        public Debounce(TimeSpan delay, DateTimeKind kind)
        {
            _delay = delay >= TimeSpan.Zero ? Normalize(delay) : throw new ArgumentOutOfRangeException(nameof(delay), delay, null);
            _provider = new DateTimeProvider(kind);
            _value = default!;
            SetTime = default;
        }

        public Debounce(T value)
            : this(value, DefaultDateTimeKind)
        {
        }

        public Debounce(T value, DateTimeKind kind)
            : this(value, TimeSpan.Zero, kind)
        {
        }

        public Debounce(T value, TimeSpan delay)
            : this(delay, DefaultDateTimeKind)
        {
        }

        public Debounce(T value, TimeSpan delay, DateTimeKind kind)
            : this(delay)
        {
            _provider = new DateTimeProvider(kind);
            _value = value;
            SetTime = Now;
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private Debounce(SerializationInfo info, StreamingContext context)
            : this(info.GetValue<TimeSpan>(nameof(Delay)))
        {
            _value = info.GetValue<T>(nameof(Value));
            SetTime = info.GetDateTime(nameof(SetTime));
            _provider = (DateTimeKind) info.GetByte(nameof(TimeKind)) switch
            {
                DateTimeKind.Unspecified => new DateTimeProvider(DateTimeKind.Unspecified),
                DateTimeKind.Utc => new DateTimeProvider(DateTimeKind.Utc),
                DateTimeKind.Local => new DateTimeProvider(DateTimeKind.Local),
                var kind => throw new SerializationException($"Debounce time kind '{kind}' is not supported.")
            };
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (IsEmpty)
            {
                value = null;
                return false;
            }

            value = _value;
            return true;
        }

        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            if (IsEmpty)
            {
                value = default;
                return false;
            }

            value = _value;
            return true;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), _value);
            info.AddValue(nameof(Delay), _delay);
            info.AddValue(nameof(SetTime), SetTime);
            info.AddValue(nameof(TimeKind), (Byte) TimeKind);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TimeSpan Normalize(TimeSpan value)
        {
            return value >= TimeSpan.Zero ? value : Utilities.Types.Time.Millisecond.Ten;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Set(T value, out Debounce<T> result)
        {
            return Set(value, out DateTime _, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IDebounce<T>.Set(T value)
        {
            return false;
        }

        Boolean IDebounce<T>.Set(T value, [MaybeNullWhen(false)] out IDebounce<T> result)
        {
            if (Set(value, out Debounce<T> debounce))
            {
                result = debounce;
                return true;
            }

            result = null;
            return false;
        }

        public Boolean Set(T value, out TimeSpan delta, out Debounce<T> result)
        {
            delta = Time;
            if (IsDebounce)
            {
                result = default;
                return false;
            }

            result = new Debounce<T>(value, Delay) { SetTime = Now };
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IDebounce<T>.Set(T value, out TimeSpan delta)
        {
            delta = Time;
            return false;
        }

        Boolean IDebounce<T>.Set(T value, out TimeSpan delta, [MaybeNullWhen(false)] out IDebounce<T> result)
        {
            if (Set(value, out delta, out Debounce<T> debounce))
            {
                result = debounce;
                return true;
            }

            result = null;
            return false;
        }

        public Boolean Set(T value, out DateTime time, out Debounce<T> result)
        {
            if (IsDebounce)
            {
                time = SetTime;
                result = default;
                return false;
            }

            time = Now;
            result = new Debounce<T>(value, Delay) { SetTime = time };
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IDebounce<T>.Set(T value, out DateTime time)
        {
            time = SetTime;
            return false;
        }

        Boolean IDebounce<T>.Set(T value, out DateTime time, [MaybeNullWhen(false)] out IDebounce<T> result)
        {
            if (Set(value, out time, out Debounce<T> debounce))
            {
                result = debounce;
                return true;
            }

            result = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Debounce<T> With(TimeSpan delay)
        {
            return new Debounce<T>(_value, delay) { SetTime = SetTime };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return comparer.SafeCompare(_value, other) ?? 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Debounce<T> other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Debounce<T> other, IComparer<T>? comparer)
        {
            return CompareTo(other._value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IDebounce<T>? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IDebounce<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? CompareTo(other.Value, comparer) : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return _value is not null ? _value.GetHashCode() : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(T? other)
        {
            return default(T) is null ? ReferenceEquals(_value, other) : Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(Debounce<T> other)
        {
            return ReferenceEquals(other._value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IDebounce<T>? other)
        {
            return other is not null && ReferenceEquals(other.Value);
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
                T value => Equals(value, comparer),
                Debounce<T> value => Equals(value, comparer),
                IDebounce<T> value => Equals(value, comparer),
                _ => false
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
            return comparer.Equals(_value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Debounce<T> other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Debounce<T> other, IEqualityComparer<T>? comparer)
        {
            return Equals(other._value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IDebounce<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IDebounce<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Debounce<T> Clone()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IDebounce<T> IDebounce<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IDebounce<T> ICloneable<IDebounce<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IDebounce IDebounce.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IDebounce ICloneable<IDebounce>.Clone()
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
            return _value is not null ? _value.ToString() : null;
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
            return _value is not null ? StringUtilities.ToString(in _value, format, provider) : String.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString()
        {
            return IStringProvider.Default.GetString(_value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape)
        {
            return IStringProvider.Default.GetString(_value, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format)
        {
            return IStringProvider.Default.GetString(_value, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format)
        {
            return IStringProvider.Default.GetString(_value, escape, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(_value, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(_value, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format, IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(_value, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(_value, escape, format, provider);
        }
    }
}