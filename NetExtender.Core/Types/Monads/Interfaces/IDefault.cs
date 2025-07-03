// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IDefault<T> : IDefault, IMonad<T>, IDefaultEquality<T, T>, IDefaultEquality<T, IDefault<T>>, ICloneable<IDefault<T>>
    {
        public T Default { get; }
        public T Value { get; }

        public IDefault<T> Set(T value);
        public Boolean Set(T value, [MaybeNullWhen(false)] out IDefault<T> result);
        public new IDefault<T> Swap();
        public new IDefault<T> Reset();
        public new IDefault<T> Clone();
    }
    
    public interface IDefault : IMonad, ICloneable<IDefault>
    {
        public Boolean HasValue { get; }
        public Boolean IsDefault { get; }
        
        public IDefault Swap();
        public IDefault Reset();
        public new IDefault Clone();
    }
    
    public interface IDefaultEquality<out T, TDefault> : IDefaultEquality<TDefault>, IDefaultComparable<T, TDefault>, IDefaultEquatable<T, TDefault>, IMonadEquality<T, TDefault>
    {
    }
    
    public interface IDefaultEquality<T> : IDefaultComparable<T>, IDefaultEquatable<T>, IMonadEquality<T>
    {
    }
    
    public interface IDefaultEquatable<out T, TDefault> : IDefaultEquatable<TDefault>, IMonadEquatable<T, TDefault>
    {
    }
    
    public interface IDefaultEquatable<T> : IMonadEquatable<T>
    {
    }
    
    public interface IDefaultComparable<out T, in TDefault> : IDefaultComparable<TDefault>, IMonadComparable<T, TDefault>
    {
    }
    
    public interface IDefaultComparable<in T> : IMonadComparable<T>
    {
    }
}