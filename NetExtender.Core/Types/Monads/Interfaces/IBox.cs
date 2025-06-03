using System;
using NetExtender.Interfaces;
using NetExtender.Interfaces.Notify;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface INotifyBox<T> : INotifyBox, IBox<T>, ICloneable<INotifyBox<T>>
    {
        public new INotifyBox<T> Clone();
    }
    
    public interface IBox<T> : IBox, IMonad<T>, IBoxEquality<T, T>, IBoxEquality<T, IBox<T>>, ICloneable<IBox<T>>
    {
        public new T Value { get; }

        public Boolean Set(T value);
        public new IBox<T> Clone();
    }
    
    public interface INotifyBox : IBox, ICloneable<INotifyBox>, INotifyProperty
    {
        public new INotifyBox Clone();
    }
    
    public interface IBox : IMonad, ICloneable<IBox>
    {
        public Guid Id { get; }
        public Object? Value { get; }
        public Boolean IsReadOnly { get; }
        
        public new IBox Clone();
    }
    
    public interface IBoxEquality<out T, TDefault> : IBoxEquality<TDefault>, IBoxComparable<T, TDefault>, IBoxEquatable<T, TDefault>, IMonadEquality<T, TDefault>
    {
    }
    
    public interface IBoxEquality<T> : IBoxComparable<T>, IBoxEquatable<T>, IMonadEquality<T>
    {
    }
    
    public interface IBoxEquatable<out T, TDefault> : IBoxEquatable<TDefault>, IMonadEquatable<T, TDefault>
    {
    }
    
    public interface IBoxEquatable<T> : IMonadEquatable<T>
    {
    }
    
    public interface IBoxComparable<out T, in TDefault> : IBoxComparable<TDefault>, IMonadComparable<T, TDefault>
    {
    }
    
    public interface IBoxComparable<in T> : IMonadComparable<T>
    {
    }
}