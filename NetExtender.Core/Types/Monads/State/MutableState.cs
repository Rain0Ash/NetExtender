using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Interfaces;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads
{
    public class NotifyState<T> : MutableState<T>
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
        
        protected override State<T> Internal
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
                    OnPropertyChanging(nameof(HasValue));
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
                    OnPropertyChanged(nameof(HasValue));
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

        public override NotifyState<T> Clone()
        {
            return new NotifyState<T>(Internal);
        }
    }
    
    public class MutableState<T> : INotifyState<T>, IStateEquatable<T, State<T>>, IStateEquatable<T, MutableState<T>>, IStateComparable<T, State<T>>, IStateComparable<T, MutableState<T>>, ICloneable<State<T>>, ICloneable<MutableState<T>>
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

        private State<T> _internal;
        protected virtual State<T> Internal
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
                Set(value);
            }
        }

        public T Current
        {
            get
            {
                return Internal.Current;
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
                return Internal.Next;
            }
            set
            {
                Update(value);
            }
        }

        public Boolean HasValue
        {
            get
            {
                return Internal.HasValue;
            }
        }

        public MutableState()
        {
            _internal = default;
        }

        public MutableState(T value)
        {
            _internal = value;
        }

        public MutableState(State<T> value)
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

        public Boolean HasDifference()
        {
            return Internal.HasDifference();
        }

        public Boolean HasDifference(IEqualityComparer<T>? comparer)
        {
            return Internal.HasDifference(comparer);
        }

        public T Get()
        {
            return Internal.Get();
        }

        public T Get(StateEquality equality)
        {
            return Internal.Get(equality);
        }

        public virtual void Set(T value)
        {
            Internal = Internal.Set(value);
        }

        IState<T> IState<T>.Set(T value)
        {
            Set(value);
            return this;
        }
        
        public virtual void Save()
        {
            Internal = Internal.Save();
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
            Internal = Internal.Save(value);
        }

        IState<T> IState<T>.Save(T value)
        {
            Save(value);
            return this;
        }

        public virtual void Update(T value)
        {
            Internal = Internal.Update(value);
        }

        IState<T> IState<T>.Update(T value)
        {
            Update(value);
            return this;
        }

        public virtual void Update(Maybe<T> value)
        {
            Internal = Internal.Update(value);
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
            if (Internal.Difference(comparer, out State<T> result) is false)
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
            if (Internal.Difference(value, comparer, out State<T> result) is not null)
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
            if (Internal.Difference(value, comparer, out State<T> result) is not null)
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
            if (HasValue)
            {
                Internal = Internal.Swap();
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
            Internal = Internal.Reset();
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
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(T? other, StateEquality equality)
        {
            return Internal.CompareTo(other, equality);
        }
        
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(T? other, StateEquality equality, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, equality, comparer);
        }

        public Int32 CompareTo(State<T> other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(State<T> other, StateEquality equality)
        {
            return Internal.CompareTo(other, equality);
        }

        public Int32 CompareTo(State<T> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(State<T> other, StateEquality equality, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, equality, comparer);
        }

        public Int32 CompareTo(MutableState<T>? other)
        {
            return other is not null ? Internal.CompareTo(other.Internal) : 1;
        }

        public Int32 CompareTo(MutableState<T>? other, StateEquality equality)
        {
            return other is not null ? Internal.CompareTo(other.Internal, equality) : 1;
        }

        public Int32 CompareTo(MutableState<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? Internal.CompareTo(other.Internal, comparer) : 1;
        }

        public Int32 CompareTo(MutableState<T>? other, StateEquality equality, IComparer<T>? comparer)
        {
            return other is not null ? Internal.CompareTo(other.Internal, equality, comparer) : 1;
        }

        public Int32 CompareTo(IState<T>? other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(IState<T>? other, StateEquality equality)
        {
            return Internal.CompareTo(other, equality);
        }

        public Int32 CompareTo(IState<T>? other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(IState<T>? other, StateEquality equality, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, equality, comparer);
        }

        public sealed override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
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
            return Internal.Equals(other);
        }

        public Boolean Equals(T? other, StateEquality equality)
        {
            return Internal.Equals(other, equality);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(T? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, equality, comparer);
        }

        public Boolean Equals(State<T> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(State<T> other, StateEquality equality)
        {
            return Internal.Equals(other, equality);
        }

        public Boolean Equals(State<T> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(State<T> other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, equality, comparer);
        }

        public Boolean Equals(MutableState<T>? other)
        {
            return other is not null && Equals(other.Internal);
        }

        public Boolean Equals(MutableState<T>? other, StateEquality equality)
        {
            return other is not null && Equals(other.Internal, equality);
        }

        public Boolean Equals(MutableState<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Internal, comparer);
        }

        public Boolean Equals(MutableState<T>? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Internal, equality, comparer);
        }

        public Boolean Equals(IState<T>? other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(IState<T>? other, StateEquality equality)
        {
            return Internal.Equals(other, equality);
        }

        public Boolean Equals(IState<T>? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(IState<T>? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, equality, comparer);
        }
        
        public virtual MutableState<T> Clone()
        {
            return new MutableState<T>(Internal);
        }
        
        Object ICloneable.Clone()
        {
            return Clone();
        }
        
        IState IState.Clone()
        {
            return Clone();
        }
        
        IState<T> IState<T>.Clone()
        {
            return Clone();
        }
        
        State<T> ICloneable<State<T>>.Clone()
        {
            return Clone();
        }
        
        public sealed override String? ToString()
        {
            return Internal.ToString();
        }
        
        public String? ToString(StateEquality equality)
        {
            return Internal.ToString(equality);
        }
    }
}