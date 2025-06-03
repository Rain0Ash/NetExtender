// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Interfaces;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IMaybe<T> : IMaybe, IMonad<T>, IMaybeEquality<T, T>, IMaybeEquality<T, IMaybe<T>>, IMaybeEquality<T, INullMaybe<T>>, ICloneable<IMaybe<T>>
    {
        public new T Value { get; }

        public new IMaybe<T> Clone();
    }
    
    public interface IMaybe : IMonad, ICloneable<IMaybe>
    {
        public Boolean HasValue { get; }
        public Object? Value { get; }

        public new IMaybe Clone();
    }
    
    public interface IMaybeEquality<out T, TMaybe> : IMaybeEquality<TMaybe>, IMaybeComparable<T, TMaybe>, IMaybeEquatable<T, TMaybe>, IMonadEquality<T, TMaybe>
    {
    }
    
    public interface IMaybeEquality<T> : IMaybeComparable<T>, IMaybeEquatable<T>, IMonadEquality<T>
    {
    }
    
    public interface IMaybeEquatable<out T, TMaybe> : IMaybeEquatable<TMaybe>, IMonadEquatable<T, TMaybe>
    {
    }
    
    public interface IMaybeEquatable<T> : IMonadEquatable<T>
    {
    }
    
    public interface IMaybeComparable<out T, in TMaybe> : IMaybeComparable<TMaybe>, IMonadComparable<T, TMaybe>
    {
    }
    
    public interface IMaybeComparable<in T> : IMonadComparable<T>
    {
    }
}