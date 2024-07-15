using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Initializer.Types.Monads.Debouce
{
    public readonly struct Debounce<T> : IDebounce<T>, IDebounceEquatable<T, Debounce<T>>, IDebounceComparable<T, Debounce<T>>, ICloneable<Debounce<T>>
    {
        public static implicit operator T(Debounce<T> value)
        {
            return value.Value;
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
        
        public T Value { get; }
        
        public TimeSpan Time
        {
            get
            {
                return DateTime.Now - SetTime;
            }
        }
        
        public DateTime SetTime { get; private init; }
        
        private readonly TimeSpan _delay;
        public TimeSpan Delay
        {
            get
            {
                return Normalize(_delay);
            }
            init
            {
                _delay = value >= default(TimeSpan) ? value : throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
        
        public Boolean IsDebounce
        {
            get
            {
                return Time < Delay;
            }
        }
        
        public Debounce(T value)
            : this(value, default)
        {
        }
        
        public Debounce(T value, TimeSpan delay)
        {
            _delay = delay >= default(TimeSpan) ? Normalize(delay) : throw new ArgumentOutOfRangeException(nameof(value), value, null);
            Value = value;
            SetTime = DateTime.Now;
        }
        
        private static TimeSpan Normalize(TimeSpan value)
        {
            return value >= default(TimeSpan) ? value : Utilities.Types.Time.Millisecond.Ten;
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
            
            result = default;
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
            
            result = default;
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
            
            result = default;
            return false;
        }
        
        public Debounce<T> With(TimeSpan delay)
        {
            return new Debounce<T>(Value, delay) { SetTime = SetTime };
        }
        
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }
        
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            try
            {
                comparer ??= Comparer<T>.Default;
                return comparer.Compare(Value, other);
            }
            catch (ArgumentException)
            {
                return 0;
            }
        }
        
        public Int32 CompareTo(Debounce<T> other)
        {
            return CompareTo(other, null);
        }
        
        public Int32 CompareTo(Debounce<T> other, IComparer<T>? comparer)
        {
            return CompareTo(other.Value, comparer);
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
            return Value?.GetHashCode() ?? 0;
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
            return comparer.Equals(Value, other);
        }
        
        public Boolean Equals(Debounce<T> other)
        {
            return Equals(other, null);
        }
        
        public Boolean Equals(Debounce<T> other, IEqualityComparer<T>? comparer)
        {
            return Equals(other.Value, comparer);
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
        
        Object ICloneable.Clone()
        {
            return Clone();
        }
        
        IDebounce IDebounce.Clone()
        {
            return Clone();
        }
        
        IDebounce<T> IDebounce<T>.Clone()
        {
            return Clone();
        }
        
        IDebounce<T> ICloneable<IDebounce<T>>.Clone()
        {
            return Clone();
        }
        
        public override String? ToString()
        {
            return Value?.ToString();
        }
    }
}