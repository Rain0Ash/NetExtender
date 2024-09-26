using System;
using System.Collections.Generic;
using NetExtender.Interfaces;
using NetExtender.Interfaces.Notify;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface INotifyState<T> : IState<T>, INotifyState
    {
    }
    
    public interface IState<T> : IState, IStateEquatable<T, T>, IStateEquatable<T, IState<T>>, IStateComparable<T, T>, IStateComparable<T, IState<T>>, ICloneable<IState<T>>
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
        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer);
        public Boolean Equals(Object? other, StateEquality equality, IEqualityComparer<T>? comparer);
    }
    
    public interface INotifyState : IState, INotifyProperty
    {
    }
    
    public interface IState : ICloneable, ICloneable<IState>
    {
        public Boolean HasValue { get; }
        
        public Boolean HasDifference();
        public IState Save();
        public IState Swap();
        public IState Reset();
        public new IState Clone();
        public Boolean Equals(Object? other, StateEquality equality);
        public String? ToString(StateEquality equality);
    }
    
    public interface IStateEquatable<out T, TState> : IStateEquatable<TState>
    {
        public Boolean Equals(TState? other, IEqualityComparer<T>? comparer);
        public Boolean Equals(TState? other, StateEquality equality, IEqualityComparer<T>? comparer);
    }
    
    public interface IStateEquatable<T> : IEquatable<T>
    {
        public Boolean Equals(T? other, StateEquality equality);
    }
    
    public interface IStateComparable<out T, in TState> : IStateComparable<TState>
    {
        public Int32 CompareTo(TState? other, IComparer<T>? comparer);
        public Int32 CompareTo(TState? other, StateEquality equality, IComparer<T>? comparer);
    }
    
    public interface IStateComparable<in T> : IComparable<T>
    {
        public Int32 CompareTo(T? other, StateEquality equality);
    }
}