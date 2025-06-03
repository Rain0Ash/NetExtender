using System;
using System.Collections.Generic;
using NetExtender.Interfaces;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IMonad<T> : IMonad, IMonadEquality<T>, ICloneable<IMonad<T>>
    {
        public new IMonad<T> Clone();
        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer);
    }
    
    public interface IMonad : ICloneable<IMonad>, ICloneable, IStringable
    {
        public Boolean IsEmpty { get; }
        
        public new IMonad Clone();
    }
    
    public interface IMonadEquality<out T, TMonad> : IMonadEquality<TMonad>, IMonadComparable<T, TMonad>, IMonadEquatable<T, TMonad>
    {
    }
    
    public interface IMonadEquality<T> : IMonadComparable<T>, IMonadEquatable<T>, IEquality<T>
    {
    }
    
    public interface IMonadEquatable<out T, TMonad> : IMonadEquatable<TMonad>
    {
        public Boolean Equals(TMonad? other, IEqualityComparer<T>? comparer);
    }
    
    public interface IMonadEquatable<T> : IEquatable<T>
    {
    }
    
    public interface IMonadComparable<out T, in TMonad> : IMonadComparable<TMonad>
    {
        public Int32 CompareTo(TMonad? other, IComparer<T>? comparer);
    }
    
    public interface IMonadComparable<in T> : IComparable<T>
    {
    }
}