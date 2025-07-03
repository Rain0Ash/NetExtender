// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Newtonsoft.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [JsonConverter(typeof(NotifyStateJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.NotifyStateJsonConverter<>))]
    public class NotifyState<T> : MutableState<T>, INotifyState<T>, ICloneable<NotifyState<T>>
    {
        public static implicit operator NotifyState<T>(T value)
        {
            return new NotifyState<T>(value);
        }

        public static implicit operator NotifyState<T>(State<T> value)
        {
            return new NotifyState<T>(value);
        }

        public new static NotifyState<T?> New
        {
            get
            {
                return new NotifyState<T?>(default(T));
            }
        }
        
        protected internal override State<T> Internal
        {
            get
            {
                return base.Internal;
            }
            set
            {
                Boolean @internal = !Internal.Equals(value, StateEquality.Value);
                Boolean current = !Internal.Equals(value, StateEquality.Current);
                Boolean next = !Internal.Next.Equals(value.Next);

                if (@internal)
                {
                    OnPropertyChanging(nameof(Value));
                }

                if (next)
                {
                    OnPropertyChanging(nameof(Next));
                    OnPropertyChanging(nameof(HasNext));
                }

                if (current)
                {
                    OnPropertyChanging(nameof(Current));
                }

                base.Internal = value;
                
                if (@internal)
                {
                    OnPropertyChanged(nameof(Value));
                }

                if (next)
                {
                    OnPropertyChanged(nameof(Next));
                    OnPropertyChanged(nameof(HasNext));
                }

                if (current)
                {
                    OnPropertyChanged(nameof(Current));
                }
            }
        }

        public NotifyState(T value)
            : base(value)
        {
        }

        public NotifyState(State<T> value)
            : base(value)
        {
        }

        protected NotifyState(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override NotifyState<T> Clone()
        {
            return new NotifyState<T>(Internal);
        }

        INotifyState<T> INotifyState<T>.Clone()
        {
            return Clone();
        }

        INotifyState<T> ICloneable<INotifyState<T>>.Clone()
        {
            return Clone();
        }

        INotifyState INotifyState.Clone()
        {
            return Clone();
        }

        INotifyState ICloneable<INotifyState>.Clone()
        {
            return Clone();
        }
    }
    
    [Serializable]
    [JsonConverter(typeof(MutableStateJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.MutableStateJsonConverter<>))]
    public class MutableState<T> : IState<T>, IStateEquality<T, State<T>>, IStateEquality<T, MutableState<T>>, ICloneable<State<T>>, ICloneable<MutableState<T>>, ISerializable
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(MutableState<T>? value)
        {
            return value is not null ? value.Current : default;
        }
        
        public static implicit operator State<T>(MutableState<T>? value)
        {
            return value?.Internal ?? default;
        }

        public static implicit operator MutableState<T>(T value)
        {
            return new MutableState<T>(value);
        }

        public static implicit operator MutableState<T>(State<T> value)
        {
            return new MutableState<T>(value);
        }
        
        public static Boolean operator ==(T? first, MutableState<T>? second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, MutableState<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(MutableState<T>? first, T? second)
        {
            return first is not null && first.Equals(second);
        }

        public static Boolean operator !=(MutableState<T>? first, T? second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(State<T> first, MutableState<T>? second)
        {
            return second == first;
        }

        public static Boolean operator !=(State<T> first, MutableState<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(MutableState<T>? first, State<T> second)
        {
            return first is not null && first.Equals(second);
        }

        public static Boolean operator !=(MutableState<T>? first, State<T> second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(MutableState<T>? first, MutableState<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && first.Equals(second);
        }

        public static Boolean operator !=(MutableState<T>? first, MutableState<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator >(T? first, MutableState<T>? second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, MutableState<T>? second)
        {
            return second <= first;
        }

        public static Boolean operator <(T? first, MutableState<T>? second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, MutableState<T>? second)
        {
            return second >= first;
        }

        public static Boolean operator >(MutableState<T>? first, T? second)
        {
            return first is not null && first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(MutableState<T>? first, T? second)
        {
            return first is null && second is null || first is not null && first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(MutableState<T>? first, T? second)
        {
            return first is not null && first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(MutableState<T>? first, T? second)
        {
            return first is null && second is null || first is not null && first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(State<T> first, MutableState<T>? second)
        {
            return second < first;
        }

        public static Boolean operator >=(State<T> first, MutableState<T>? second)
        {
            return second <= first;
        }

        public static Boolean operator <(State<T> first, MutableState<T>? second)
        {
            return second > first;
        }

        public static Boolean operator <=(State<T> first, MutableState<T>? second)
        {
            return second >= first;
        }

        public static Boolean operator >(MutableState<T>? first, State<T> second)
        {
            return first is not null && first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(MutableState<T>? first, State<T> second)
        {
            return first is not null && first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(MutableState<T>? first, State<T> second)
        {
            return first is not null && first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(MutableState<T>? first, State<T> second)
        {
            return first is not null && first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(MutableState<T>? first, MutableState<T>? second)
        {
            return first is not null && (second is null || first.CompareTo(second) > 0);
        }

        public static Boolean operator >=(MutableState<T>? first, MutableState<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && (second is null || first.CompareTo(second) >= 0);
        }

        public static Boolean operator <(MutableState<T>? first, MutableState<T>? second)
        {
            return first is null && second is not null || first is not null && first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(MutableState<T>? first, MutableState<T>? second)
        {
            return ReferenceEquals(first, second) || first is null && second is not null || first is not null && first.CompareTo(second) <= 0;
        }

        public static MutableState<T?> New
        {
            get
            {
                return new MutableState<T?>(default(T));
            }
        }
        
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public Guid Id { get; } = Guid.NewGuid();

        private State<T> _internal;
        protected internal virtual State<T> Internal
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
                Set(value);
            }
        }

        public T Current
        {
            get
            {
                return _internal.Current;
            }
            set
            {
                Update(value);
            }
        }

        public Maybe<T> Next
        {
            get
            {
                return _internal.Next;
            }
            set
            {
                Update(value);
            }
        }

        public Boolean HasNext
        {
            get
            {
                return _internal.HasNext;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            get
            {
                return _internal.IsEmpty;
            }
        }

        public MutableState()
        {
            _internal = default;
        }

        public MutableState(T value)
        {
            _internal = new State<T>(value);
        }

        public MutableState(T value, T next)
        {
            _internal = new State<T>(value, next);
        }

        public MutableState(T value, Maybe<T> next)
        {
            _internal = new State<T>(value, next);
        }

        public MutableState(State<T> value)
        {
            _internal = value;
        }

        protected MutableState(SerializationInfo info, StreamingContext context)
        {
            _internal = new State<T>(info.GetValue<T>(nameof(Value)), info.GetBoolean(nameof(HasNext)) ? info.GetValue<T>(nameof(Next)) : default(Maybe<T>));
        }
        
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), _internal.Value);
            info.AddValue(nameof(HasNext), _internal.Next.HasValue);

            if (_internal.HasNext)
            {
                info.AddValue(nameof(Next), _internal.Next.Value);
            }
        }

        protected void OnPropertyChanging([CallerMemberName] String? property = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChanging(property));
        }

        protected void OnPropertyChanged([CallerMemberName] String? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChanged(property));
        }

        public Boolean HasDifference()
        {
            return _internal.HasDifference();
        }

        public Boolean HasDifference(IEqualityComparer<T>? comparer)
        {
            return _internal.HasDifference(comparer);
        }

        public T Get()
        {
            return _internal.Get();
        }

        public T Get(StateEquality equality)
        {
            return _internal.Get(equality);
        }

        public virtual void Set(T value)
        {
            Internal = _internal.Set(value);
        }

        IState<T> IState<T>.Set(T value)
        {
            Set(value);
            return this;
        }
        
        public virtual void Save()
        {
            Internal = _internal.Save();
        }

        IState IState.Save()
        {
            Save();
            return this;
        }

        IState<T> IState<T>.Save()
        {
            Save();
            return this;
        }

        public virtual void Save(T value)
        {
            Internal = _internal.Save(value);
        }

        IState<T> IState<T>.Save(T value)
        {
            Save(value);
            return this;
        }

        public virtual void Update(T value)
        {
            Internal = _internal.Update(value);
        }

        IState<T> IState<T>.Update(T value)
        {
            Update(value);
            return this;
        }

        public virtual void Update(Maybe<T> value)
        {
            Internal = _internal.Update(value);
        }

        IState<T> IState<T>.Update(Maybe<T> value)
        {
            Update(value);
            return this;
        }

        public virtual void Difference()
        {
            Difference(null);
        }

        IState<T> IState<T>.Difference()
        {
            Difference();
            return this;
        }
        
        public virtual void Difference(IEqualityComparer<T>? comparer)
        {
            if (_internal.Difference(comparer, out State<T> result) is false)
            {
                Internal = result;
            }
        }

        IState<T> IState<T>.Difference(IEqualityComparer<T>? comparer)
        {
            Difference(comparer);
            return this;
        }

        public virtual void Difference(T value)
        {
            Difference(value, null);
        }

        IState<T> IState<T>.Difference(T value)
        {
            Difference(value);
            return this;
        }

        public virtual void Difference(T value, IEqualityComparer<T>? comparer)
        {
            if (_internal.Difference(value, comparer, out State<T> result) is not null)
            {
                Internal = result;
            }
        }

        IState<T> IState<T>.Difference(T value, IEqualityComparer<T>? comparer)
        {
            Difference(value, comparer);
            return this;
        }

        public virtual void Difference(Maybe<T> value)
        {
            Difference(value, null);
        }

        IState<T> IState<T>.Difference(Maybe<T> value)
        {
            Difference(value);
            return this;
        }
        
        public virtual void Difference(Maybe<T> value, IEqualityComparer<T>? comparer)
        {
            if (_internal.Difference(value, comparer, out State<T> result) is not null)
            {
                Internal = result;
            }
        }

        IState<T> IState<T>.Difference(Maybe<T> value, IEqualityComparer<T>? comparer)
        {
            Difference(value, comparer);
            return this;
        }

        public virtual void Swap()
        {
            if (HasNext)
            {
                Internal = _internal.Swap();
            }
        }

        IState IState.Swap()
        {
            Swap();
            return this;
        }

        IState<T> IState<T>.Swap()
        {
            Swap();
            return this;
        }

        public virtual void Reset()
        {
            Internal = _internal.Reset();
        }

        IState IState.Reset()
        {
            Reset();
            return this;
        }

        IState<T> IState<T>.Reset()
        {
            Reset();
            return this;
        }

        public Int32 CompareTo(T? other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(T? other, StateEquality equality)
        {
            return _internal.CompareTo(other, equality);
        }
        
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(T? other, StateEquality equality, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, equality, comparer);
        }

        public Int32 CompareTo(State<T> other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(State<T> other, StateEquality equality)
        {
            return _internal.CompareTo(other, equality);
        }

        public Int32 CompareTo(State<T> other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(State<T> other, StateEquality equality, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, equality, comparer);
        }

        public Int32 CompareTo(MutableState<T>? other)
        {
            return other is not null ? _internal.CompareTo(other._internal) : 1;
        }

        public Int32 CompareTo(MutableState<T>? other, StateEquality equality)
        {
            return other is not null ? _internal.CompareTo(other._internal, equality) : 1;
        }

        public Int32 CompareTo(MutableState<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? _internal.CompareTo(other._internal, comparer) : 1;
        }

        public Int32 CompareTo(MutableState<T>? other, StateEquality equality, IComparer<T>? comparer)
        {
            return other is not null ? _internal.CompareTo(other._internal, equality, comparer) : 1;
        }

        public Int32 CompareTo(IState<T>? other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(IState<T>? other, StateEquality equality)
        {
            return _internal.CompareTo(other, equality);
        }

        public Int32 CompareTo(IState<T>? other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(IState<T>? other, StateEquality equality, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, equality, comparer);
        }

        public sealed override Int32 GetHashCode()
        {
            return Id.GetHashCode();
        }

        public sealed override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, StateEquality equality)
        {
            return Equals(other, equality, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return Equals(other, default, null);
        }

        public Boolean Equals(Object? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                T value => Equals(value, equality, comparer),
                State<T> value => Equals(value, equality, comparer),
                MutableState<T> value => Equals(value, equality, comparer),
                IState<T> value => Equals(value, equality, comparer),
                _ => false
            };
        }

        public Boolean Equals(T? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(T? other, StateEquality equality)
        {
            return _internal.Equals(other, equality);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(T? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, equality, comparer);
        }

        public Boolean Equals(State<T> other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(State<T> other, StateEquality equality)
        {
            return _internal.Equals(other, equality);
        }

        public Boolean Equals(State<T> other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(State<T> other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, equality, comparer);
        }

        public Boolean Equals(MutableState<T>? other)
        {
            return other is not null && Equals(other._internal);
        }

        public Boolean Equals(MutableState<T>? other, StateEquality equality)
        {
            return other is not null && Equals(other._internal, equality);
        }

        public Boolean Equals(MutableState<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other._internal, comparer);
        }

        public Boolean Equals(MutableState<T>? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other._internal, equality, comparer);
        }

        public Boolean Equals(IState<T>? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(IState<T>? other, StateEquality equality)
        {
            return _internal.Equals(other, equality);
        }

        public Boolean Equals(IState<T>? other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(IState<T>? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, equality, comparer);
        }
        
        public virtual MutableState<T> Clone()
        {
            return new MutableState<T>(_internal);
        }

        State<T> ICloneable<State<T>>.Clone()
        {
            return Clone();
        }

        IState<T> IState<T>.Clone()
        {
            return Clone();
        }

        IState<T> ICloneable<IState<T>>.Clone()
        {
            return Clone();
        }

        IState IState.Clone()
        {
            return Clone();
        }

        IState ICloneable<IState>.Clone()
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
        
        public String? ToString(StateEquality equality)
        {
            return _internal.ToString(equality);
        }

        public String ToString(StateEquality equality, String? format)
        {
            return _internal.ToString(equality, format);
        }

        public String ToString(StateEquality equality, IFormatProvider? provider)
        {
            return _internal.ToString(equality, provider);
        }

        public String ToString(StateEquality equality, String? format, IFormatProvider? provider)
        {
            return _internal.ToString(equality, format, provider);
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

        public String? GetString(StateEquality equality)
        {
            return _internal.GetString(equality);
        }

        public String? GetString(StateEquality equality, EscapeType escape)
        {
            return _internal.GetString(equality, escape);
        }

        public String? GetString(StateEquality equality, String? format)
        {
            return _internal.GetString(equality, format);
        }

        public String? GetString(StateEquality equality, EscapeType escape, String? format)
        {
            return _internal.GetString(equality, escape, format);
        }

        public String? GetString(StateEquality equality, IFormatProvider? provider)
        {
            return _internal.GetString(equality, provider);
        }

        public String? GetString(StateEquality equality, EscapeType escape, IFormatProvider? provider)
        {
            return _internal.GetString(equality, escape, provider);
        }

        public String? GetString(StateEquality equality, String? format, IFormatProvider? provider)
        {
            return _internal.GetString(equality, format, provider);
        }

        public String? GetString(StateEquality equality, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return _internal.GetString(equality, escape, format, provider);
        }
    }
}