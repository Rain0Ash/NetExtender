// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads.Debouce
{
    [Serializable]
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

        public static Boolean operator >(T? first, Debounce<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, Debounce<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(T? first, Debounce<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, Debounce<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(Debounce<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Debounce<T> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(Debounce<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Debounce<T> first, T? second)
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

        public static Boolean operator <(Debounce<T> first, Debounce<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Debounce<T> first, Debounce<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        private readonly T _value;
        public T Value
        {
            get
            {
                return _value;
            }
        }

        public TimeSpan Time
        {
            get
            {
                return DateTime.Now - SetTime;
            }
        }
        
        private readonly TimeSpan? _delay;
        public TimeSpan Delay
        {
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
        
        public Boolean IsDebounce
        {
            get
            {
                return Time < Delay;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return _delay is null && SetTime == default;
            }
        }

        public Debounce(TimeSpan delay)
        {
            _delay = delay >= TimeSpan.Zero ? Normalize(delay) : throw new ArgumentOutOfRangeException(nameof(delay), delay, null);
            _value = default!;
            SetTime = default;
        }
        
        public Debounce(T value)
            : this(value, TimeSpan.Zero)
        {
        }
        
        public Debounce(T value, TimeSpan delay)
            : this(delay)
        {
            _value = value;
            SetTime = DateTime.Now;
        }
        
        private Debounce(SerializationInfo info, StreamingContext context)
            : this(info.GetValue<TimeSpan>(nameof(Delay)))
        {
            _value = info.GetValue<T>(nameof(Value));
            SetTime = info.GetDateTime(nameof(SetTime));
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), _value);
            info.AddValue(nameof(Delay), _delay);
            info.AddValue(nameof(SetTime), SetTime);
        }
        
        private static TimeSpan Normalize(TimeSpan value)
        {
            return value >= TimeSpan.Zero ? value : Utilities.Types.Time.Millisecond.Ten;
        }
        
        public Boolean Set(T value, out Debounce<T> result)
        {
            return Set(value, out DateTime _, out result);
        }
        
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
            
            result = new Debounce<T>(value, Delay) { SetTime = DateTime.Now };
            return true;
        }
        
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
            
            time = DateTime.Now;
            result = new Debounce<T>(value, Delay) { SetTime = time };
            return true;
        }
        
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
        
        public Debounce<T> With(TimeSpan delay)
        {
            return new Debounce<T>(_value, delay) { SetTime = SetTime };
        }
        
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }
        
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return comparer.SafeCompare(_value, other) ?? 0;
        }

        public Int32 CompareTo(Debounce<T> other)
        {
            return CompareTo(other, null);
        }
        
        public Int32 CompareTo(Debounce<T> other, IComparer<T>? comparer)
        {
            return CompareTo(other._value, comparer);
        }
        
        public Int32 CompareTo(IDebounce<T>? other)
        {
            return CompareTo(other, null);
        }
        
        public Int32 CompareTo(IDebounce<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? CompareTo(other.Value, comparer) : 1;
        }
        
        public override Int32 GetHashCode()
        {
            return _value is not null ? _value.GetHashCode() : 0;
        }

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
        
        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }
        
        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(_value, other);
        }
        
        public Boolean Equals(Debounce<T> other)
        {
            return Equals(other, null);
        }
        
        public Boolean Equals(Debounce<T> other, IEqualityComparer<T>? comparer)
        {
            return Equals(other._value, comparer);
        }
        
        public Boolean Equals(IDebounce<T>? other)
        {
            return Equals(other, null);
        }
        
        public Boolean Equals(IDebounce<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Value, comparer);
        }
        
        public Debounce<T> Clone()
        {
            return this;
        }

        IDebounce<T> IDebounce<T>.Clone()
        {
            return Clone();
        }

        IDebounce<T> ICloneable<IDebounce<T>>.Clone()
        {
            return Clone();
        }

        IDebounce IDebounce.Clone()
        {
            return Clone();
        }

        IDebounce ICloneable<IDebounce>.Clone()
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
            return _value is not null ? _value.ToString() : null;
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
            return _value is not null ? StringUtilities.ToString(in _value, format, provider) : String.Empty;
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