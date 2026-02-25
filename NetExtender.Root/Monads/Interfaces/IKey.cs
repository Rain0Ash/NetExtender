// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces;
using NetExtender.Interfaces.Notify;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface INotifyKey<TKey, TValue> : INotifyKey, IKey<TKey, TValue>, ICloneable<INotifyKey<TKey, TValue>>
    {
        public new INotifyKey<TKey, TValue> With(TValue value);
        public new INotifyKey<TKey, TValue> With(TValue value, out Boolean successful);
        public new INotifyKey<TKey, TValue> With(TKey key, TValue value);
        public new INotifyKey<TKey, TValue> With(TKey key, TValue value, out Boolean successful);
        public new INotifyKey<TKey, TValue> Clear();
        public new INotifyKey<TKey, TValue> Clear(out Boolean successful);

        public new INotifyKey<TKey, TValue> Clone();
    }

    public interface IKey<TKey, TValue> : IKey, IMonad<TValue>, IKeyEquality<TValue, TValue>, IKeyEquality<TValue, IKey<TKey, TValue>>, ICloneable<IKey<TKey, TValue>>
    {
        public new TKey Key { get; }
        public new Maybe<TKey> SafeKey { get; }

        public new TValue Value { get; }
        public new Maybe<TValue> SafeValue { get; }

        public Boolean Unwrap([MaybeNullWhen(false)] out TKey key);

        public Boolean Set(TValue value);
        public Boolean Set(TKey key, TValue value);

        public IKey<TKey, TValue> With(TValue value);
        public IKey<TKey, TValue> With(TValue value, out Boolean successful);
        public IKey<TKey, TValue> With(TKey key, TValue value);
        public IKey<TKey, TValue> With(TKey key, TValue value, out Boolean successful);
        public IKey<TKey, TValue> Clear();
        public IKey<TKey, TValue> Clear(out Boolean successful);

        public new IKey<TKey, TValue> Clone();
    }

    public interface INotifyKey : IKey, ICloneable<INotifyKey>, INotifyProperty
    {
        public new INotifyKey Clone();
    }

    public interface IKey : IMonad, ICloneable<IKey>
    {
        public Object? Key { get; }
        public Maybe<Object?> SafeKey { get; }
        public Object? Value { get; }
        public Maybe<Object?> SafeValue { get; }
        public Boolean IsKeyed { get; }
        public Boolean IsReadOnly { get; }

        public Boolean Reset();

        public new IKey Clone();
    }

    public interface IKeyEquality<out TValue, TBox> : IKeyEquality<TBox>, IKeyComparable<TValue, TBox>, IKeyEquatable<TValue, TBox>, IMonadEquality<TValue, TBox>
    {
    }

    public interface IKeyEquality<TValue> : IKeyComparable<TValue>, IKeyEquatable<TValue>, IMonadEquality<TValue>
    {
    }

    public interface IKeyEquatable<out TValue, TBox> : IKeyEquatable<TBox>, IMonadEquatable<TValue, TBox>
    {
    }

    public interface IKeyEquatable<TValue> : IMonadEquatable<TValue>
    {
    }

    public interface IKeyComparable<out TValue, in TBox> : IKeyComparable<TBox>, IMonadComparable<TValue, TBox>
    {
    }

    public interface IKeyComparable<in TValue> : IMonadComparable<TValue>
    {
    }
}