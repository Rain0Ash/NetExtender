using System;
using System.Collections.Generic;
using NetExtender.Interfaces;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IState<T> : IStateEquatable<T, T>, IStateEquatable<T, IState<T>>, IStateComparable<T, T>, IStateComparable<T, IState<T>>, ICloneable, ICloneable<IState<T>>
    {
        public T Value { get; }
        public T Current { get; }
        public Maybe<T> Next { get; }
        public Boolean HasValue { get; }
        
        public Boolean HasDifference();
        public Boolean HasDifference(IEqualityComparer<T>? comparer);
        public T Get();
        public T Get(StateEquality equality);
        public IState<T> Set(T value);
        public IState<T> Save();
        public IState<T> Save(T value);
        public IState<T> Update(T value);
        public IState<T> Update(Maybe<T> value);
        public IState<T> Difference();
        public IState<T> Difference(T value);
        public IState<T> Difference(IEqualityComparer<T>? comparer);
        public IState<T> Difference(T value, IEqualityComparer<T>? comparer);
        public IState<T> Difference(Maybe<T> value);
        public IState<T> Difference(Maybe<T> value, IEqualityComparer<T>? comparer);
        public IState<T> Swap();
        public IState<T> Reset();
        public new IState<T> Clone();
        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer);
        public Boolean Equals(Object? other, StateEquality equality);
        public Boolean Equals(Object? other, StateEquality equality, IEqualityComparer<T>? comparer);
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