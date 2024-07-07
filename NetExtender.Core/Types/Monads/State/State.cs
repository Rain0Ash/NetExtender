using System;
using System.Collections.Generic;
using NetExtender.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Types.Monads
{
    public enum StateEquality : Byte
    {
        Current,
        Value
    }
    
    public readonly struct State<T> : IState<T>, IStateEquatable<T, State<T>>, IStateComparable<T, State<T>>, ICloneable<State<T>>
    {
        public static implicit operator T(State<T> value)
        {
            return value.Current;
        }

        public static implicit operator State<T>(T value)
        {
            return new State<T>(value);
        }
        
        public static Boolean operator ==(T? first, State<T> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, State<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(State<T> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(State<T> first, T? second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(State<T> first, State<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(State<T> first, State<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator >(T? first, State<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, State<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(T? first, State<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, State<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(State<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(State<T> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(State<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(State<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(State<T> first, State<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(State<T> first, State<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(State<T> first, State<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(State<T> first, State<T> second)
        {
            return first.CompareTo(second) <= 0;
        }
        
        public T Value { get; }

        public T Current
        {
            get
            {
                return Next ? Next.Value : Value;
            }
        }
        
        public Maybe<T> Next { get; init; }

        public Boolean HasValue
        {
            get
            {
                return Next;
            }
        }

        public State(T value)
        {
            Value = value;
            Next = default;
        }

        public Boolean HasDifference()
        {
            return HasDifference(null);
        }

        public Boolean HasDifference(IEqualityComparer<T>? comparer)
        {
            return HasValue && !Equals(Value, StateEquality.Current, comparer);
        }

        public T Get()
        {
            return Get(default);
        }

        public T Get(StateEquality equality)
        {
            return equality switch
            {
                StateEquality.Current => Current,
                StateEquality.Value => Value,
                _ => throw new EnumUndefinedOrNotSupportedException<StateEquality>(equality, nameof(equality), null)
            };
        }

        public State<T> Set(T value)
        {
            return new State<T>(value) { Next = Next };
        }
        
        IState<T> IState<T>.Set(T value)
        {
            return Set(value);
        }

        public State<T> Save()
        {
            return Next ? new State<T>(Next.Value) : this;
        }

        IState IState.Save()
        {
            return Save();
        }

        IState<T> IState<T>.Save()
        {
            return Save();
        }

        public State<T> Save(T value)
        {
            return new State<T>(Current) { Next = value };
        }
        
        IState<T> IState<T>.Save(T value)
        {
            return Save(value);
        }

        public State<T> Update(T value)
        {
            return new State<T>(Value) { Next = value };
        }
        
        IState<T> IState<T>.Update(T value)
        {
            return Update(value);
        }

        public State<T> Update(Maybe<T> value)
        {
            return new State<T>(Value) { Next = value };
        }
        
        IState<T> IState<T>.Update(Maybe<T> value)
        {
            return Update(value);
        }

        public State<T> Difference()
        {
            return Difference(out State<T> result) is not null ? result : this;
        }

        public Boolean? Difference(out State<T> result)
        {
            return Difference(null, out result);
        }

        IState<T> IState<T>.Difference()
        {
            return Difference();
        }

        public State<T> Difference(IEqualityComparer<T>? comparer)
        {
            return Difference(comparer, out State<T> result) is not null ? result : this;
        }

        public Boolean? Difference(IEqualityComparer<T>? comparer, out State<T> result)
        {
            if (!HasValue)
            {
                result = this;
                return null;
            }

            if (Next.Equals(Value, comparer))
            {
                result = Reset();
                return false;
            }

            result = this;
            return true;
        }

        IState<T> IState<T>.Difference(IEqualityComparer<T>? comparer)
        {
            return Difference(comparer);
        }

        public State<T> Difference(T value)
        {
            return Difference(value, out State<T> result) is not null ? result : this;
        }

        public Boolean? Difference(T value, out State<T> result)
        {
            return Difference(value, null, out result);
        }

        IState<T> IState<T>.Difference(T value)
        {
            return Difference(value);
        }

        public State<T> Difference(T value, IEqualityComparer<T>? comparer)
        {
            return Difference(value, comparer, out State<T> result) is not null ? result : this;
        }

        public Boolean? Difference(T value, IEqualityComparer<T>? comparer, out State<T> result)
        {
            if (!Next.Equals(value, comparer))
            {
                result = Update(value);
                return true;
            }

            if (HasValue)
            {
                result = Reset();
                return false;
            }

            result = this;
            return null;
        }

        IState<T> IState<T>.Difference(T value, IEqualityComparer<T>? comparer)
        {
            return Difference(value, comparer);
        }

        public State<T> Difference(Maybe<T> value)
        {
            return Difference(value, out State<T> result) is not null ? result : this;
        }

        public Boolean? Difference(Maybe<T> value, out State<T> result)
        {
            return Difference(value, null, out result);
        }

        IState<T> IState<T>.Difference(Maybe<T> value)
        {
            return Difference(value);
        }

        public State<T> Difference(Maybe<T> value, IEqualityComparer<T>? comparer)
        {
            return Difference(value, comparer, out State<T> result) is not null ? result : this;
        }

        public Boolean? Difference(Maybe<T> value, IEqualityComparer<T>? comparer, out State<T> result)
        {
            if (!Next.Equals(value, comparer))
            {
                result = Update(value);
                return true;
            }

            if (HasValue)
            {
                result = Reset();
                return false;
            }

            result = this;
            return null;
        }

        IState<T> IState<T>.Difference(Maybe<T> value, IEqualityComparer<T>? comparer)
        {
            return Difference(value, comparer);
        }

        public State<T> Swap()
        {
            return Next ? new State<T>(Next.Value) { Next = Value } : this;
        }

        IState IState.Swap()
        {
            return Swap();
        }

        IState<T> IState<T>.Swap()
        {
            return Swap();
        }

        public State<T> Reset()
        {
            return new State<T>(Value);
        }

        IState IState.Reset()
        {
            return Reset();
        }

        IState<T> IState<T>.Reset()
        {
            return Reset();
        }

        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(T? other, StateEquality equality)
        {
            return CompareTo(other, equality, null);
        }

        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return CompareTo(other, default, comparer);
        }

        public Int32 CompareTo(T? other, StateEquality equality, IComparer<T>? comparer)
        {
            try
            {
                comparer ??= Comparer<T>.Default;
                return comparer.Compare(Get(equality), other);
            }
            catch (ArgumentException)
            {
                return 0;
            }
        }

        public Int32 CompareTo(State<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(State<T> other, StateEquality equality)
        {
            return CompareTo(other, equality, null);
        }

        public Int32 CompareTo(State<T> other, IComparer<T>? comparer)
        {
            return CompareTo(other, default, comparer);
        }

        public Int32 CompareTo(State<T> other, StateEquality equality, IComparer<T>? comparer)
        {
            return CompareTo(other.Get(equality), equality, comparer);
        }

        public Int32 CompareTo(IState<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(IState<T>? other, StateEquality equality)
        {
            return CompareTo(other, equality, null);
        }

        public Int32 CompareTo(IState<T>? other, IComparer<T>? comparer)
        {
            return CompareTo(other, default, comparer);
        }

        public Int32 CompareTo(IState<T>? other, StateEquality equality, IComparer<T>? comparer)
        {
            return other is not null ? CompareTo(other.Get(equality), equality, comparer) : 1;
        }

        public override Int32 GetHashCode()
        {
            return Current?.GetHashCode() ?? 0;
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, StateEquality equality)
        {
            return Equals(other, equality, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return Equals(other, default, comparer);
        }

        public Boolean Equals(Object? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                T value => Equals(value, equality, comparer),
                State<T> value => Equals(value, equality, comparer),
                IState<T> value => Equals(value, equality, comparer),
                _ => false
            };
        }

        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(T? other, StateEquality equality)
        {
            return Equals(other, equality, null);
        }
        
        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            return Equals(other, default, comparer);
        }

        public Boolean Equals(T? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Get(equality), other);
        }

        public Boolean Equals(State<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(State<T> other, StateEquality equality)
        {
            return Equals(other, equality, null);
        }
        
        public Boolean Equals(State<T> other, IEqualityComparer<T>? comparer)
        {
            return Equals(other, default, comparer);
        }

        public Boolean Equals(State<T> other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return Equals(other.Get(equality), equality, comparer);
        }

        public Boolean Equals(IState<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IState<T>? other, StateEquality equality)
        {
            return Equals(other, equality, null);
        }

        public Boolean Equals(IState<T>? other, IEqualityComparer<T>? comparer)
        {
            return Equals(other, default, comparer);
        }

        public Boolean Equals(IState<T>? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Get(equality), equality, comparer);
        }
        
        public State<T> Clone()
        {
            return this;
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
        
        IState<T> ICloneable<IState<T>>.Clone()
        {
            return Clone();
        }
        
        public override String? ToString()
        {
            return ToString(default);
        }

        public String? ToString(StateEquality equality)
        {
            return Get(equality)?.ToString();
        }
    }
}