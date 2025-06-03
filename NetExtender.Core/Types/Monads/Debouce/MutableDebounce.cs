// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Interfaces.Notify;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads.Debouce
{
    [Serializable]
    public class NotifyDebounce<T> : MutableDebounce<T>
    {
        public static implicit operator NotifyDebounce<T>(T value)
        {
            return new NotifyDebounce<T>(value);
        }
        
        public static implicit operator NotifyDebounce<T>(Debounce<T> value)
        {
            return new NotifyDebounce<T>(value);
        }
        
        public new static NotifyDebounce<T?> New
        {
            get
            {
                return new NotifyDebounce<T?>(default(T));
            }
        }
        
        protected override Debounce<T> Internal
        {
            get
            {
                return base.Internal;
            }
            set
            {
                Boolean @internal = !Internal.Equals(value.Value);
                Boolean delay = Internal.Delay != Delay;
                
                if (@internal)
                {
                    OnPropertyChanging(nameof(Value));
                    OnPropertyChanging(nameof(Time));
                    OnPropertyChanging(nameof(SetTime));
                }

                if (delay)
                {
                    OnPropertyChanging(nameof(Delay));
                }
                
                if (@internal || delay)
                {
                    OnPropertyChanging(nameof(IsDebounce));
                }
                
                base.Internal = value;
                
                if (@internal)
                {
                    OnPropertyChanged(nameof(Value));
                    OnPropertyChanged(nameof(Time));
                    OnPropertyChanged(nameof(SetTime));
                }
                
                if (delay)
                {
                    OnPropertyChanged(nameof(Delay));
                }
                
                if (@internal || delay)
                {
                    OnPropertyChanged(nameof(IsDebounce));
                }
            }
        }
        
        public NotifyDebounce(TimeSpan delay)
            : base(delay)
        {
        }
        
        public NotifyDebounce(T value)
            : base(value)
        {
        }
        
        public NotifyDebounce(T value, TimeSpan delay)
            : base(value, delay)
        {
        }
        
        public NotifyDebounce(Debounce<T> value)
            : base(value)
        {
        }
        
        public override NotifyDebounce<T> Clone()
        {
            return new NotifyDebounce<T>(Internal);
        }
    }
    
    [Serializable]
    public class MutableDebounce<T> : IDebounce<T>, IDebounceEquality<T, Debounce<T>>, IDebounceEquality<T, MutableDebounce<T>>, ICloneable<Debounce<T>>, ICloneable<MutableDebounce<T>>, ISerializable, INotifyProperty
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(MutableDebounce<T>? value)
        {
            return value is not null ? value.Value : default;
        }
        
        public static implicit operator Debounce<T>(MutableDebounce<T>? value)
        {
            return value?.Internal ?? default;
        }

        public static implicit operator MutableDebounce<T>(T value)
        {
            return new MutableDebounce<T>(value);
        }

        public static implicit operator MutableDebounce<T>(Debounce<T> value)
        {
            return new MutableDebounce<T>(value);
        }
        
        public static Boolean operator ==(T? first, MutableDebounce<T>? second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, MutableDebounce<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(MutableDebounce<T>? first, T? second)
        {
            return first is not null && first.Equals(second);
        }

        public static Boolean operator !=(MutableDebounce<T>? first, T? second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(Debounce<T> first, MutableDebounce<T>? second)
        {
            return second == first;
        }

        public static Boolean operator !=(Debounce<T> first, MutableDebounce<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(MutableDebounce<T>? first, Debounce<T> second)
        {
            return first is not null && first.Equals(second);
        }

        public static Boolean operator !=(MutableDebounce<T>? first, Debounce<T> second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(MutableDebounce<T>? first, MutableDebounce<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && first.Equals(second);
        }

        public static Boolean operator !=(MutableDebounce<T>? first, MutableDebounce<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator >(T? first, MutableDebounce<T>? second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, MutableDebounce<T>? second)
        {
            return second <= first;
        }

        public static Boolean operator <(T? first, MutableDebounce<T>? second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, MutableDebounce<T>? second)
        {
            return second >= first;
        }

        public static Boolean operator >(MutableDebounce<T>? first, T? second)
        {
            return first is not null && first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(MutableDebounce<T>? first, T? second)
        {
            return first is null && second is null || first is not null && first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(MutableDebounce<T>? first, T? second)
        {
            return first is not null && first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(MutableDebounce<T>? first, T? second)
        {
            return first is null && second is null || first is not null && first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Debounce<T> first, MutableDebounce<T>? second)
        {
            return second < first;
        }

        public static Boolean operator >=(Debounce<T> first, MutableDebounce<T>? second)
        {
            return second <= first;
        }

        public static Boolean operator <(Debounce<T> first, MutableDebounce<T>? second)
        {
            return second > first;
        }

        public static Boolean operator <=(Debounce<T> first, MutableDebounce<T>? second)
        {
            return second >= first;
        }

        public static Boolean operator >(MutableDebounce<T>? first, Debounce<T> second)
        {
            return first is not null && first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(MutableDebounce<T>? first, Debounce<T> second)
        {
            return first is not null && first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(MutableDebounce<T>? first, Debounce<T> second)
        {
            return first is not null && first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(MutableDebounce<T>? first, Debounce<T> second)
        {
            return first is not null && first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(MutableDebounce<T>? first, MutableDebounce<T>? second)
        {
            return first is not null && (second is null || first.CompareTo(second) > 0);
        }

        public static Boolean operator >=(MutableDebounce<T>? first, MutableDebounce<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && (second is null || first.CompareTo(second) >= 0);
        }

        public static Boolean operator <(MutableDebounce<T>? first, MutableDebounce<T>? second)
        {
            return first is null && second is not null || first is not null && first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(MutableDebounce<T>? first, MutableDebounce<T>? second)
        {
            return ReferenceEquals(first, second) || first is null && second is not null || first is not null && first.CompareTo(second) <= 0;
        }
        
        public static MutableDebounce<T?> New
        {
            get
            {
                return new MutableDebounce<T?>(default(T));
            }
        }
        
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected Guid Id { get; } = Guid.NewGuid();
        
        private Debounce<T> _internal;
        protected virtual Debounce<T> Internal
        {
            get
            {
                return _internal;
            }
            set
            {
                _internal = value;
            }
        }
        
        public T Value
        {
            get
            {
                return _internal.Value;
            }
            set
            {
                if (!Set(value))
                {
                    throw new DebounceException($"Can't set value. Set time is '{SetTime}' with delay '{Delay}'.");
                }
            }
        }
        
        public TimeSpan Time
        {
            get
            {
                return _internal.Time;
            }
        }

        public TimeSpan Delay
        {
            get
            {
                return _internal.Delay;
            }
            init
            {
                Internal = Internal.With(value);
            }
        }

        public DateTime SetTime
        {
            get
            {
                return _internal.SetTime;
            }
        }

        public Boolean IsDebounce
        {
            get
            {
                return _internal.IsDebounce;
            }
        }
        
        public Boolean IsEmpty
        {
            get
            {
                return _internal.IsEmpty;
            }
        }
        
        public MutableDebounce(TimeSpan delay)
        {
            _internal = new Debounce<T>(delay);
        }
        
        public MutableDebounce(T value)
        {
            _internal = new Debounce<T>(value);
        }
        
        public MutableDebounce(T value, TimeSpan delay)
        {
            _internal = new Debounce<T>(value, delay);
        }
        
        public MutableDebounce(Debounce<T> value)
        {
            _internal = value;
        }

        protected MutableDebounce(SerializationInfo info, StreamingContext context)
        {
            _internal = new Debounce<T>(info.GetValue<T>(nameof(Value)), info.GetValue<TimeSpan>(nameof(Delay))) { SetTime = info.GetDateTime(nameof(SetTime)) };
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), _internal.Value);
            info.AddValue(nameof(Delay), _internal.Delay);
            info.AddValue(nameof(SetTime), _internal.SetTime);
        }

        protected void OnPropertyChanging([CallerMemberName] String? property = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChanging(property));
        }
        
        protected void OnPropertyChanged([CallerMemberName] String? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChanged(property));
        }
        
        public Boolean Set(T value)
        {
            if (!_internal.Set(value, out Debounce<T> result))
            {
                return false;
            }
            
            _internal = result;
            return true;
        }
        
        Boolean IDebounce<T>.Set(T value, [MaybeNullWhen(false)] out IDebounce<T> result)
        {
            if (Set(value))
            {
                result = this;
                return true;
            }
            
            result = null;
            return false;
        }
        
        public Boolean Set(T value, out TimeSpan delta)
        {
            if (!_internal.Set(value, out delta, out Debounce<T> result))
            {
                return false;
            }
            
            Internal = result;
            return true;
        }
        
        Boolean IDebounce<T>.Set(T value, out TimeSpan delta, [MaybeNullWhen(false)] out IDebounce<T> result)
        {
            if (Set(value, out delta))
            {
                result = this;
                return true;
            }
            
            result = default;
            return false;
        }
        
        public Boolean Set(T value, out DateTime time)
        {
            if (!_internal.Set(value, out time, out Debounce<T> result))
            {
                return false;
            }
            
            Internal = result;
            return true;
        }
        
        Boolean IDebounce<T>.Set(T value, out DateTime time, [MaybeNullWhen(false)] out IDebounce<T> result)
        {
            if (Set(value, out time))
            {
                result = this;
                return true;
            }
            
            result = null;
            return false;
        }
        
        public Int32 CompareTo(T? other)
        {
            return _internal.CompareTo(other);
        }
        
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(Debounce<T> other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(Debounce<T> other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(MutableDebounce<T>? other)
        {
            return other is not null ? _internal.CompareTo(other._internal) : 1;
        }

        public Int32 CompareTo(MutableDebounce<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? _internal.CompareTo(other._internal, comparer) : 1;
        }

        public Int32 CompareTo(IDebounce<T>? other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(IDebounce<T>? other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public sealed override Int32 GetHashCode()
        {
            return Id.GetHashCode();
        }

        public sealed override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                T value => Equals(value, comparer),
                Debounce<T> value => Equals(value, comparer),
                MutableDebounce<T> value => Equals(value, comparer),
                IDebounce<T> value => Equals(value, comparer),
                _ => false
            };
        }

        public Boolean Equals(T? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(Debounce<T> other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(Debounce<T> other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(MutableDebounce<T>? other)
        {
            return other is not null && Equals(other._internal);
        }

        public Boolean Equals(MutableDebounce<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other._internal, comparer);
        }

        public Boolean Equals(IDebounce<T>? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(IDebounce<T>? other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }
        
        public virtual MutableDebounce<T> Clone()
        {
            return new MutableDebounce<T>(_internal);
        }

        Debounce<T> ICloneable<Debounce<T>>.Clone()
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

        public sealed override String? ToString()
        {
            return _internal.ToString();
        }

        public String ToString(String? format)
        {
            return _internal.ToString(format);
        }

        public String ToString(IFormatProvider? provider)
        {
            return _internal.ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return _internal.ToString(format, provider);
        }

        public String? GetString()
        {
            return _internal.GetString();
        }

        public String? GetString(EscapeType escape)
        {
            return _internal.GetString(escape);
        }

        public String? GetString(String? format)
        {
            return _internal.GetString(format);
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return _internal.GetString(escape, format);
        }

        public String? GetString(IFormatProvider? provider)
        {
            return _internal.GetString(provider);
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return _internal.GetString(escape, provider);
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return _internal.GetString(format, provider);
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return _internal.GetString(escape, format, provider);
        }
    }
    
    [Serializable]
    public class DebounceException : InvalidOperationException
    {
        public DebounceException()
        {
        }
        
        public DebounceException(String? message)
            : base(message)
        {
        }
        
        public DebounceException(String? message, Exception? exception)
            : base(message, exception)
        {
        }
        
        protected DebounceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}