using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Interfaces.Notify;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads.Debouce
{
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
    
    public class MutableDebounce<T> : IDebounce<T>, IDebounceEquatable<T, Debounce<T>>, IDebounceEquatable<T, MutableDebounce<T>>, IDebounceComparable<T, Debounce<T>>, IDebounceComparable<T, MutableDebounce<T>>, ICloneable<Debounce<T>>, ICloneable<MutableDebounce<T>>, INotifyProperty
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
                return Internal.Value;
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
                return Internal.Time;
            }
        }
        
        public DateTime SetTime
        {
            get
            {
                return Internal.SetTime;
            }
        }
        
        public TimeSpan Delay
        {
            get
            {
                return Internal.Delay;
            }
            init
            {
                Internal = Internal.With(value);
            }
        }
        
        public Boolean IsDebounce
        {
            get
            {
                return Internal.IsDebounce;
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
            if (!Internal.Set(value, out Debounce<T> result))
            {
                return false;
            }
            
            Internal = result;
            return true;
        }
        
        Boolean IDebounce<T>.Set(T value, [MaybeNullWhen(false)] out IDebounce<T> result)
        {
            if (Set(value))
            {
                result = this;
                return true;
            }
            
            result = default;
            return false;
        }
        
        public Boolean Set(T value, out TimeSpan delta)
        {
            if (!Internal.Set(value, out delta, out Debounce<T> result))
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
            if (!Internal.Set(value, out time, out Debounce<T> result))
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
            
            result = default;
            return false;
        }
        
        public Int32 CompareTo(T? other)
        {
            return Internal.CompareTo(other);
        }
        
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(Debounce<T> other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(Debounce<T> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(MutableDebounce<T>? other)
        {
            return other is not null ? Internal.CompareTo(other.Internal) : 1;
        }

        public Int32 CompareTo(MutableDebounce<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? Internal.CompareTo(other.Internal, comparer) : 1;
        }

        public Int32 CompareTo(IDebounce<T>? other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(IDebounce<T>? other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public sealed override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
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
            return Internal.Equals(other);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(Debounce<T> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Debounce<T> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(MutableDebounce<T>? other)
        {
            return other is not null && Equals(other.Internal);
        }

        public Boolean Equals(MutableDebounce<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Internal, comparer);
        }

        public Boolean Equals(IDebounce<T>? other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(IDebounce<T>? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }
        
        public virtual MutableDebounce<T> Clone()
        {
            return new MutableDebounce<T>(Internal);
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
        
        Debounce<T> ICloneable<Debounce<T>>.Clone()
        {
            return Clone();
        }
        
        public sealed override String? ToString()
        {
            return Internal.ToString();
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