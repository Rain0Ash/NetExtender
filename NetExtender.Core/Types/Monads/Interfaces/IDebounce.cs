using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IDebounce<T> : IDebounce, IDebounceEquatable<T, T>, IDebounceEquatable<T, IDebounce<T>>, IDebounceComparable<T, T>, IDebounceComparable<T, IDebounce<T>>, ICloneable<IDebounce<T>>
    {
        public T Value { get; }
        
        public Boolean Set(T value);
        public Boolean Set(T value, [MaybeNullWhen(false)] out IDebounce<T> result);
        public Boolean Set(T value, out TimeSpan delta);
        public Boolean Set(T value, out TimeSpan delta, [MaybeNullWhen(false)] out IDebounce<T> result);
        public Boolean Set(T value, out DateTime time);
        public Boolean Set(T value, out DateTime time, [MaybeNullWhen(false)] out IDebounce<T> result);
        
        public new IDebounce<T> Clone();
        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer);
    }
    
    public interface IDebounce : ICloneable, ICloneable<IDebounce>
    {
        public TimeSpan Time { get; }
        public DateTime SetTime { get; }
        public TimeSpan Delay { get; }
        public Boolean IsDebounce { get; }
        
        public new IDebounce Clone();
    }
    
    public interface IDebounceEquatable<out T, TDebounce> : IDebounceEquatable<TDebounce>
    {
        public Boolean Equals(TDebounce? other, IEqualityComparer<T>? comparer);
    }
    
    public interface IDebounceEquatable<T> : IEquatable<T>
    {
    }
    
    public interface IDebounceComparable<out T, in TDebounce> : IDebounceComparable<TDebounce>
    {
        public Int32 CompareTo(TDebounce? other, IComparer<T>? comparer);
    }
    
    public interface IDebounceComparable<in T> : IComparable<T>
    {
    }
}