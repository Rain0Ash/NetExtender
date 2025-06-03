// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Interfaces;
using NetExtender.Interfaces.Notify;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface INotifyState<T> : INotifyState, IState<T>, ICloneable<INotifyState<T>>
    {
        public new INotifyState<T> Clone();
    }
    
    public interface IState<T> : IState, IMonad<T>, IStateEquality<T, T>, IStateEquality<T, IState<T>>, ICloneable<IState<T>>
    {
        public T Value { get; }
        public T Current { get; }
        public Maybe<T> Next { get; }
        
        public Boolean HasDifference(IEqualityComparer<T>? comparer);
        public T Get();
        public T Get(StateEquality equality);
        public IState<T> Set(T value);
        public new IState<T> Save();
        public IState<T> Save(T value);
        public IState<T> Update(T value);
        public IState<T> Update(Maybe<T> value);
        public IState<T> Difference();
        public IState<T> Difference(T value);
        public IState<T> Difference(IEqualityComparer<T>? comparer);
        public IState<T> Difference(T value, IEqualityComparer<T>? comparer);
        public IState<T> Difference(Maybe<T> value);
        public IState<T> Difference(Maybe<T> value, IEqualityComparer<T>? comparer);
        public new IState<T> Swap();
        public new IState<T> Reset();
        public new IState<T> Clone();
        public Boolean Equals(Object? other, StateEquality equality, IEqualityComparer<T>? comparer);
    }
    
    public interface INotifyState : IState, ICloneable<INotifyState>, INotifyProperty
    {
        public new INotifyState Clone();
    }
    
    public interface IState : IMonad, ICloneable<IState>
    {
        public Boolean HasNext { get; }
        
        public Boolean HasDifference();
        public IState Save();
        public IState Swap();
        public IState Reset();
        public new IState Clone();
        public Boolean Equals(Object? other, StateEquality equality);
        public String? ToString(StateEquality equality);
        public String ToString(StateEquality equality, String? format);
        public String ToString(StateEquality equality, IFormatProvider? provider);
        public String? ToString(StateEquality equality, String? format, IFormatProvider? provider);
        public String? GetString(StateEquality equality);
        public String? GetString(StateEquality equality, EscapeType escape);
        public String? GetString(StateEquality equality, String? format);
        public String? GetString(StateEquality equality, EscapeType escape, String? format);
        public String? GetString(StateEquality equality, IFormatProvider? provider);
        public String? GetString(StateEquality equality, EscapeType escape, IFormatProvider? provider);
        public String? GetString(StateEquality equality, String? format, IFormatProvider? provider);
        public String? GetString(StateEquality equality, EscapeType escape, String? format, IFormatProvider? provider);
    }
    
    public interface IStateEquality<out T, TState> : IStateEquality<TState>, IStateComparable<T, TState>, IStateEquatable<T, TState>, IMonadEquality<T, TState>
    {
    }
    
    public interface IStateEquality<T> : IStateComparable<T>, IStateEquatable<T>, IMonadEquality<T>
    {
    }
    
    public interface IStateEquatable<out T, TState> : IStateEquatable<TState>, IMonadEquatable<T, TState>
    {
        public Boolean Equals(TState? other, StateEquality equality, IEqualityComparer<T>? comparer);
    }
    
    public interface IStateEquatable<T> : IMonadEquatable<T>
    {
        public Boolean Equals(T? other, StateEquality equality);
    }
    
    public interface IStateComparable<out T, in TState> : IStateComparable<TState>, IMonadComparable<T, TState>
    {
        public Int32 CompareTo(TState? other, StateEquality equality, IComparer<T>? comparer);
    }
    
    public interface IStateComparable<in T> : IMonadComparable<T>
    {
        public Int32 CompareTo(T? other, StateEquality equality);
    }
}