// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IDebounce<T> : IDebounce, IMonad<T>, IDebounceEquality<T, T>, IDebounceEquality<T, IDebounce<T>>, ICloneable<IDebounce<T>>
    {
        public T Value { get; }
        
        public Boolean Set(T value);
        public Boolean Set(T value, [MaybeNullWhen(false)] out IDebounce<T> result);
        public Boolean Set(T value, out TimeSpan delta);
        public Boolean Set(T value, out TimeSpan delta, [MaybeNullWhen(false)] out IDebounce<T> result);
        public Boolean Set(T value, out DateTime time);
        public Boolean Set(T value, out DateTime time, [MaybeNullWhen(false)] out IDebounce<T> result);
        
        public new IDebounce<T> Clone();
    }
    
    public interface IDebounce : IMonad, ICloneable<IDebounce>
    {
        public TimeSpan Time { get; }
        public DateTime SetTime { get; }
        public DateTimeKind TimeKind { get; }
        public TimeSpan Delay { get; }
        public Boolean IsDebounce { get; }

        public new IDebounce Clone();
    }
    
    public interface IDebounceEquality<out T, TDebounce> : IDebounceEquality<TDebounce>, IDebounceComparable<T, TDebounce>, IDebounceEquatable<T, TDebounce>, IMonadEquality<T, TDebounce>
    {
    }
    
    public interface IDebounceEquality<T> : IDebounceComparable<T>, IDebounceEquatable<T>, IMonadEquality<T>
    {
    }

    public interface IDebounceEquatable<out T, TDebounce> : IDebounceEquatable<TDebounce>, IMonadEquatable<T, TDebounce>
    {
    }
    
    public interface IDebounceEquatable<T> : IMonadEquatable<T>
    {
    }
    
    public interface IDebounceComparable<out T, in TDebounce> : IDebounceComparable<TDebounce>, IMonadComparable<T, TDebounce>
    {
    }
    
    public interface IDebounceComparable<in T> : IMonadComparable<T>
    {
    }
}