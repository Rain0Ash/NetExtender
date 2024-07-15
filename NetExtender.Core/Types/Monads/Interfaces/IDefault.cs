using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IDefault<T> : IDefault, IDefaultEquatable<T, T>, IDefaultEquatable<T, IDefault<T>>, IDefaultComparable<T, T>, IDefaultComparable<T, IDefault<T>>
    {
        public T Value { get; }

        public IDefault<T> Set(T value);
        public Boolean Set(T value, [MaybeNullWhen(false)] out IDefault<T> result);
        public new IDefault<T> Swap();
        public new IDefault<T> Reset();
        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer);
    }
    
    public interface IDefault
    {
        public Boolean IsDefault { get; }
        
        public IDefault Swap();
        public IDefault Reset();
    }
    
    public interface IDefaultEquatable<out T, TDefault> : IDefaultEquatable<TDefault>
    {
        public Boolean Equals(TDefault? other, IEqualityComparer<T>? comparer);
    }
    
    public interface IDefaultEquatable<T> : IEquatable<T>
    {
    }
    
    public interface IDefaultComparable<out T, in TDefault> : IDefaultComparable<TDefault>
    {
        public Int32 CompareTo(TDefault? other, IComparer<T>? comparer);
    }
    
    public interface IDefaultComparable<in T> : IComparable<T>
    {
    }
}