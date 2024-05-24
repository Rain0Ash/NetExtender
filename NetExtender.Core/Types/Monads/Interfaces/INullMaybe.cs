// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface INullMaybe<T> : INullMaybe, IMaybeEquatable<T>, IMaybeEquatable<IMaybe<T>>, IMaybeEquatable<INullMaybe<T>>, IMaybeComparable<T>, IMaybeComparable<IMaybe<T>>, IMaybeComparable<INullMaybe<T>>
    {
        public new T Value { get; }
        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer);
    }
    
    public interface INullMaybe
    {
        public Object? Value { get; }
    }
}