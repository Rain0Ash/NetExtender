// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IMaybe<T> : IMaybe, IMaybeEquatable<T, T>, IMaybeEquatable<T, IMaybe<T>>, IMaybeEquatable<T, INullMaybe<T>>, IMaybeComparable<T, T>, IMaybeComparable<T, IMaybe<T>>, IMaybeComparable<T, INullMaybe<T>>
    {
        public new T Value { get; }
        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer);
    }

    public interface IMaybeEquatable<out T, TMaybe> : IMaybeEquatable<TMaybe>
    {
        public Boolean Equals(TMaybe? other, IEqualityComparer<T>? comparer);
    }

    public interface IMaybeEquatable<T> : IEquatable<T>
    {
    }
    
    public interface IMaybeComparable<out T, in TMaybe> : IMaybeComparable<TMaybe>
    {
        public Int32 CompareTo(TMaybe? other, IComparer<T>? comparer);
    }

    public interface IMaybeComparable<in T> : IComparable<T>
    {
    }
    
    public interface IMaybe
    {
        public Boolean HasValue { get; }
        public Object? Value { get; }
    }
}