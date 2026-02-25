using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using NetExtender.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Monads
{
    public abstract class Key<TKey, TValue> : Key.Base<TKey, TValue>, INotifyKey<TKey, TValue>
    {
        public static implicit operator Maybe<TKey>(Key<TKey, TValue>? value)
        {
            return value?.SafeKey ?? default;
        }

        public static implicit operator Maybe<TValue>(Key<TKey, TValue>? value)
        {
            return value?.SafeValue ?? default;
        }

        public abstract event PropertyChangingEventHandler? PropertyChanging;
        public abstract event PropertyChangedEventHandler? PropertyChanged;

        Object? IKey.Key
        {
            get
            {
                return Key;
            }
        }

        Maybe<Object?> IKey.SafeKey
        {
            get
            {
                return SafeKey.Unwrap(out TKey? key) ? key : default(Maybe<Object?>);
            }
        }

        Object? IKey.Value
        {
            get
            {
                return Value;
            }
        }

        Maybe<Object?> IKey.SafeValue
        {
            get
            {
                return SafeValue.Unwrap(out TValue? value) ? value : default(Maybe<Object?>);
            }
        }

        protected Key(Boolean mutable)
            : base(mutable)
        {
        }

        protected Key(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (Unwrap(out TValue? result))
            {
                value = result;
                return true;
            }

            value = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Unwrap([MaybeNullWhen(false)] out TKey key)
        {
            return SafeKey.Unwrap(out key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Unwrap([MaybeNullWhen(false)] out TValue value)
        {
            return SafeValue.Unwrap(out value);
        }

        public abstract Boolean Set(TValue value);
        public abstract Boolean Set(TKey key, TValue value);

        public virtual Key<TKey, TValue> With(TValue value)
        {
            return With(value, out _);
        }

        INotifyKey<TKey, TValue> INotifyKey<TKey, TValue>.With(TValue value)
        {
            return With(value);
        }

        IKey<TKey, TValue> IKey<TKey, TValue>.With(TValue value)
        {
            return With(value);
        }

        public virtual Key<TKey, TValue> With(TValue value, out Boolean successful)
        {
            successful = Set(value);
            return this;
        }

        INotifyKey<TKey, TValue> INotifyKey<TKey, TValue>.With(TValue value, out Boolean successful)
        {
            return With(value, out successful);
        }

        IKey<TKey, TValue> IKey<TKey, TValue>.With(TValue value, out Boolean successful)
        {
            return With(value, out successful);
        }

        public virtual Key<TKey, TValue> With(TKey key, TValue value)
        {
            return With(key, value, out _);
        }

        INotifyKey<TKey, TValue> INotifyKey<TKey, TValue>.With(TKey key, TValue value)
        {
            return With(key, value);
        }

        IKey<TKey, TValue> IKey<TKey, TValue>.With(TKey key, TValue value)
        {
            return With(key, value);
        }

        public virtual Key<TKey, TValue> With(TKey key, TValue value, out Boolean successful)
        {
            successful = Set(key, value);
            return this;
        }

        INotifyKey<TKey, TValue> INotifyKey<TKey, TValue>.With(TKey key, TValue value, out Boolean successful)
        {
            return With(key, value, out successful);
        }

        IKey<TKey, TValue> IKey<TKey, TValue>.With(TKey key, TValue value, out Boolean successful)
        {
            return With(key, value, out successful);
        }

        public abstract override Boolean Reset();

        public virtual Key<TKey, TValue> Clear()
        {
            return Clear(out _);
        }

        INotifyKey<TKey, TValue> INotifyKey<TKey, TValue>.Clear()
        {
            return Clear();
        }

        IKey<TKey, TValue> IKey<TKey, TValue>.Clear()
        {
            return Clear();
        }

        public virtual Key<TKey, TValue> Clear(out Boolean successful)
        {
            successful = Reset();
            return this;
        }

        INotifyKey<TKey, TValue> INotifyKey<TKey, TValue>.Clear(out Boolean successful)
        {
            return Clear(out successful);
        }

        IKey<TKey, TValue> IKey<TKey, TValue>.Clear(out Boolean successful)
        {
            return Clear(out successful);
        }

        public abstract Int32 CompareTo(TKey? other);
        public abstract Int32 CompareTo(TKey? other, IComparer<TKey>? comparer);
        public abstract Int32 CompareTo(TValue? other);
        public abstract Int32 CompareTo(TValue? other, IComparer<TValue>? comparer);
        public abstract Int32 CompareTo(Key<TKey, TValue>? other);
        public abstract Int32 CompareTo(Key<TKey, TValue>? other, IComparer<TValue>? comparer);
        public abstract Int32 CompareTo(IKey<TKey, TValue>? other);
        public abstract Int32 CompareTo(IKey<TKey, TValue>? other, IComparer<TValue>? comparer);

        public abstract override Int32 GetHashCode();

        public abstract Boolean ReferenceEquals(TKey? other);
        public abstract Boolean ReferenceEquals(TValue? other);
        public abstract Boolean ReferenceEquals(Key<TKey, TValue>? other);
        public abstract Boolean ReferenceEquals(IKey<TKey, TValue>? other);
        public abstract override Boolean Equals(Object? other);
        public abstract Boolean Equals(Object? other, IEqualityComparer<TValue>? comparer);
        public abstract Boolean Equals(TKey? other);
        public abstract Boolean Equals(TKey? other, IEqualityComparer<TKey>? comparer);
        public abstract Boolean Equals(TValue? other);
        public abstract Boolean Equals(TValue? other, IEqualityComparer<TValue>? comparer);
        public abstract Boolean Equals(Key<TKey, TValue>? other);
        public abstract Boolean Equals(Key<TKey, TValue>? other, IEqualityComparer<TValue>? comparer);
        public abstract Boolean Equals(IKey<TKey, TValue>? other);
        public abstract Boolean Equals(IKey<TKey, TValue>? other, IEqualityComparer<TValue>? comparer);

        public abstract override Key<TKey, TValue> Clone();

        INotifyKey<TKey, TValue> INotifyKey<TKey, TValue>.Clone()
        {
            return Clone();
        }

        INotifyKey<TKey, TValue> ICloneable<INotifyKey<TKey, TValue>>.Clone()
        {
            return Clone();
        }

        INotifyKey INotifyKey.Clone()
        {
            return Clone();
        }

        INotifyKey ICloneable<INotifyKey>.Clone()
        {
            return Clone();
        }

        IKey<TKey, TValue> IKey<TKey, TValue>.Clone()
        {
            return Clone();
        }

        IKey<TKey, TValue> ICloneable<IKey<TKey, TValue>>.Clone()
        {
            return Clone();
        }

        IKey IKey.Clone()
        {
            return Clone();
        }

        IKey ICloneable<IKey>.Clone()
        {
            return Clone();
        }

        IMonad<TValue> IMonad<TValue>.Clone()
        {
            return Clone();
        }

        IMonad<TValue> ICloneable<IMonad<TValue>>.Clone()
        {
            return Clone();
        }

        IMonad IMonad.Clone()
        {
            return Clone();
        }

        IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        public abstract override String? ToString();

        public abstract String ToString(String? format);
        public abstract String ToString(IFormatProvider? provider);
        public abstract String ToString(String? format, IFormatProvider? provider);
        public abstract String? GetString();
        public abstract String? GetString(EscapeType escape);
        public abstract String? GetString(String? format);
        public abstract String? GetString(EscapeType escape, String? format);
        public abstract String? GetString(IFormatProvider? provider);
        public abstract String? GetString(EscapeType escape, IFormatProvider? provider);
        public abstract String? GetString(String? format, IFormatProvider? provider);
        public abstract String? GetString(EscapeType escape, String? format, IFormatProvider? provider);
    }

    [Serializable]
    public abstract class Key : IDisposable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Search<TKey, TValue>(TKey key)
        {
            SealKey<TKey, TValue> node = SealKey<TKey, TValue>.Node;
            node.Set(key);
            return node;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Search<TKey, TValue>(TKey key, TValue value)
        {
            SealKey<TKey, TValue> node = SealKey<TKey, TValue>.Node;
            node.Set(key, value);
            return node;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Key<TKey, TValue> Search<TKey, TValue>(Func<TValue, TKey> handler, TValue value)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            SealKey<TKey, TValue> node = SealKey<TKey, TValue>.Node;
            node.Set(handler(value), value);
            return node;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Key<TKey, TValue> Search<TKey, TValue>(TryParseHandler<TValue, TKey> handler, TValue value)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (!handler(value, out TKey? result))
            {
                throw new ArgumentException($"Key of '{value}' is not valid.", nameof(value));
            }

            SealKey<TKey, TValue> node = SealKey<TKey, TValue>.Node;
            node.Set(result, value);
            return node;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value)
        {
            return Create(key, value, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value, Boolean notify)
        {
            return Create(key, value, notify, notify);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value, Boolean mutable, Boolean notify)
        {
            return notify ? new NotifySeal<TKey, TValue>(key, value, mutable) : new Seal<TKey, TValue>(key, value, mutable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TryParseHandler<TValue, TKey> handler, TValue value)
        {
            return Create(handler, value, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TryParseHandler<TValue, TKey> handler, TValue value, Boolean notify)
        {
            return Create(handler, value, notify, notify);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TryParseHandler<TValue, TKey> handler, TValue value, Boolean mutable, Boolean notify)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return notify ? new NotifyDynamic<TKey, TValue>(handler, value, mutable) : new Dynamic<TKey, TValue>(handler, value, mutable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TryParseHandler<TValue, TKey> handler, WeakMaybe<TValue> value) where TValue : class?
        {
            return Create(handler, value, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TryParseHandler<TValue, TKey> handler, WeakMaybe<TValue> value, Boolean notify) where TValue : class?
        {
            return Create(handler, value, notify, notify);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TryParseHandler<TValue, TKey> handler, WeakMaybe<TValue> value, Boolean mutable, Boolean notify) where TValue : class?
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return notify ? new NotifyWeak<TKey, TValue>(handler, value, mutable) : new Weak<TKey, TValue>(handler, value, mutable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TryParseHandler<TValue, TKey> handler, Action<Key<TKey, TValue>>? notify) where TValue : class?
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return new SmartWeak<TKey, TValue>(handler, notify);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TryParseHandler<TValue, TKey> handler, TValue value, Action<Key<TKey, TValue>>? notify) where TValue : class?
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return new SmartWeak<TKey, TValue>(handler, value, notify);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key<TKey, TValue> Create<TKey, TValue>(TryParseHandler<TValue, TKey> handler, WeakMaybe<TValue> value, Action<Key<TKey, TValue>>? notify) where TValue : class?
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return new SmartWeak<TKey, TValue>(handler, value, notify);
        }

        public static implicit operator Boolean(Key? value)
        {
            return value?.IsEmpty is false;
        }

        private readonly State _state;
        public Boolean IsReadOnly
        {
            get
            {
                return _state is not State.Mutable;
            }
        }

        public virtual Boolean IsKeyed
        {
            get
            {
                return false;
            }
        }

        public virtual Boolean IsEmpty
        {
            get
            {
                return _state is State.None;
            }
        }

        protected Key(Boolean mutable)
            : this(mutable ? State.Mutable : State.Immutable)
        {
        }

        private Key(State state)
        {
            _state = state;
        }

        protected Key(SerializationInfo info, StreamingContext context)
        {
            _state = (State) info.GetValueOrDefault<Byte>(nameof(State)) switch
            {
                State.None => State.Immutable,
                State.Mutable => State.Mutable,
                State.Immutable => State.Immutable,
                var state => throw new SerializationException($"Key state '{state}' is not supported.")
            };
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (_state is not State.None and State.Mutable)
            {
                info.AddValue(nameof(State), (Byte) _state);
            }
        }

        public abstract Boolean Reset();
        public abstract Key Clone();
        public abstract void Dispose();

        [Serializable]
        public abstract class Base<TKey, TValue> : Key
        {
            protected Base(Boolean mutable)
                : base(mutable)
            {
            }

            protected Base(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }

            public abstract TKey Key { get; }
            public abstract Maybe<TKey> SafeKey { get; }
            public abstract TValue Value { get; }
            public abstract Maybe<TValue> SafeValue { get; }
        }

        private sealed class SealKey<TKey, TValue> : SealBase<TKey, TValue>
        {
            [ThreadStatic]
            private static SealKey<TKey, TValue>? _node;

            internal static SealKey<TKey, TValue> Node
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _node ??= new SealKey<TKey, TValue>();
                }
            }

            public override event PropertyChangingEventHandler? PropertyChanging
            {
                add
                {
                }
                remove
                {
                }
            }

            public override event PropertyChangedEventHandler? PropertyChanged
            {
                add
                {
                }
                remove
                {
                }
            }

            public override Boolean IsKeyed
            {
                get
                {
                    return _value.IsEmpty;
                }
            }

            private SealKey()
                : this(default)
            {
            }

            private SealKey(NullMaybe<TKey> key)
                : base(key, default, true)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Set(NullMaybe<TKey> key)
            {
                Set(key, default);
            }

            public override Key<TKey, TValue> Clone()
            {
                return new SealKey<TKey, TValue>(_key);
            }
        }

        [Serializable]
        private sealed class NotifySeal<TKey, TValue> : SealBase<TKey, TValue>
        {
            public override event PropertyChangingEventHandler? PropertyChanging;
            public override event PropertyChangedEventHandler? PropertyChanged;

            public NotifySeal(TKey key, TValue value)
                : this(key, value, true)
            {
            }

            public NotifySeal(TKey key, TValue value, Boolean mutable)
                : base(key, value, mutable)
            {
            }

            private NotifySeal(NullMaybe<TKey> key, NullMaybe<TValue> value, Boolean mutable)
                : base(key, value, mutable)
            {
            }

            private NotifySeal(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            public override Boolean Set(TValue value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                (Boolean Value, Boolean IsEmpty) current = (!_value.ReferenceEquals(value), IsEmpty);
                (PropertyChangingEventHandler? changing, PropertyChangedEventHandler? changed) = (PropertyChanging, PropertyChanged);

                if (changing is not null)
                {
                    if (current.Value)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.Value);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeValue);
                    }

                    if (current.IsEmpty)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.IsEmpty);
                    }
                }

                _value = value;

                if (changed is not null)
                {
                    if (current.Value)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.Value);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeValue);
                    }

                    if (current.IsEmpty)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.IsEmpty);
                    }
                }

                return true;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            protected override Boolean Set(NullMaybe<TKey> key, NullMaybe<TValue> value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                (Boolean Key, Boolean Value, Boolean IsEmpty) current = (!_key.ReferenceEquals(key), !_value.ReferenceEquals(value), IsEmpty);
                (PropertyChangingEventHandler? changing, PropertyChangedEventHandler? changed) = (PropertyChanging, PropertyChanged);

                if (changing is not null)
                {
                    if (current.Key)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.Key);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeKey);
                    }

                    if (current.Value)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.Value);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeValue);
                    }

                    if (current.IsEmpty && (!key.IsEmpty || !value.IsEmpty) || !current.IsEmpty && key.IsEmpty && value.IsEmpty)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.IsEmpty);
                    }
                }

                _key = key;
                _value = value;

                if (changed is not null)
                {
                    if (current.Key)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.Key);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeKey);
                    }

                    if (current.Value)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.Value);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeValue);
                    }

                    if (current.IsEmpty && (!key.IsEmpty || !value.IsEmpty) || !current.IsEmpty && key.IsEmpty && value.IsEmpty)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.IsEmpty);
                    }
                }

                return true;
            }

            public override Key<TKey, TValue> Clone()
            {
                return new NotifySeal<TKey, TValue>(_key, _value, !IsReadOnly);
            }

            public override void Dispose()
            {
                base.Dispose();
                PropertyChanging = null;
                PropertyChanged = null;
            }
        }

        [Serializable]
        private sealed class Seal<TKey, TValue> : SealBase<TKey, TValue>
        {
            public override event PropertyChangingEventHandler? PropertyChanging
            {
                add
                {
                }
                remove
                {
                }
            }

            public override event PropertyChangedEventHandler? PropertyChanged
            {
                add
                {
                }
                remove
                {
                }
            }

            public Seal(TKey key, TValue value, Boolean mutable)
                : base(key, value, mutable)
            {
            }

            private Seal(NullMaybe<TKey> key, NullMaybe<TValue> value, Boolean mutable)
                : base(key, value, mutable)
            {
            }

            private Seal(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }

            public override Key<TKey, TValue> Clone()
            {
                return new Seal<TKey, TValue>(_key, _value, !IsReadOnly);
            }
        }

        [Serializable]
        private abstract class SealBase<TKey, TValue> : Key<TKey, TValue>
        {
            private protected NullMaybe<TKey> _key;
            public sealed override TKey Key
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _key.Value;
                }
            }

            public sealed override Maybe<TKey> SafeKey
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _key.Value;
                }
            }

            private protected NullMaybe<TValue> _value;
            public sealed override TValue Value
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _value.Value;
                }
            }

            public sealed override Maybe<TValue> SafeValue
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _value.Value;
                }
            }

            public sealed override Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _key.IsEmpty && (IsKeyed || _value.IsEmpty);
                }
            }

            protected SealBase(TKey key, TValue value, Boolean mutable)
                : this((NullMaybe<TKey>) key, (NullMaybe<TValue>) value, mutable)
            {
            }

            protected SealBase(NullMaybe<TKey> key, NullMaybe<TValue> value, Boolean mutable)
                : base(mutable)
            {
                _key = key;
                _value = value;
            }

            protected SealBase(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                _key = info.GetValueOrDefault<NullMaybe<TKey>>(nameof(Key));
                _value = info.GetValueOrDefault<NullMaybe<TValue>>(nameof(Value));
            }

            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);

                if (!_key.IsEmpty)
                {
                    info.AddValue(nameof(Key), _key.Value);
                }

                if (!_value.IsEmpty)
                {
                    info.AddValue(nameof(Value), _value.Value);
                }
            }

            public override Boolean Set(TValue value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                _value = value;
                return true;
            }

            [SuppressMessage("ReSharper", "RedundantCast")]
            public sealed override Boolean Set(TKey key, TValue value)
            {
                return Set((NullMaybe<TKey>) key, (NullMaybe<TValue>) value);
            }

            protected virtual Boolean Set(NullMaybe<TKey> key, NullMaybe<TValue> value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                _key = key;
                _value = value;
                return true;
            }

            public sealed override Boolean Reset()
            {
                return Set(default, default);
            }

            public override void Dispose()
            {
                Reset();
            }

            public override Int32 CompareTo(TKey? other)
            {
                return _key.CompareTo(other);
            }

            public override Int32 CompareTo(TKey? other, IComparer<TKey>? comparer)
            {
                return _key.CompareTo(other, comparer);
            }

            public override Int32 CompareTo(TValue? other)
            {
                if (_value.IsEmpty)
                {
                    return other is null ? _key.IsEmpty ? 0 : 1 : -1;
                }

                return _value.CompareTo(other);
            }

            public override Int32 CompareTo(TValue? other, IComparer<TValue>? comparer)
            {
                if (_value.IsEmpty)
                {
                    return other is null ? _key.IsEmpty ? 0 : 1 : -1;
                }

                return _value.CompareTo(other, comparer);
            }

            public override Int32 CompareTo(Key<TKey, TValue>? other)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = _key.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : _value.CompareTo(other.SafeValue);
            }

            public override Int32 CompareTo(Key<TKey, TValue>? other, IComparer<TValue>? comparer)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = _key.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : _value.CompareTo(other.SafeValue, comparer);
            }

            public override Int32 CompareTo(IKey<TKey, TValue>? other)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = _key.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : _value.CompareTo(other.SafeValue);
            }

            public override Int32 CompareTo(IKey<TKey, TValue>? other, IComparer<TValue>? comparer)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = _key.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : _value.CompareTo(other.SafeValue, comparer);
            }

            [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
            public sealed override Int32 GetHashCode()
            {
                return _key.GetHashCode();
            }

            public override Boolean ReferenceEquals(TKey? other)
            {
                return _key.ReferenceEquals(other);
            }

            public override Boolean ReferenceEquals(TValue? other)
            {
                return _value.ReferenceEquals(other);
            }

            public override Boolean ReferenceEquals(Key<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && _key.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.ReferenceEquals(other.SafeValue));
            }

            public override Boolean ReferenceEquals(IKey<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && _key.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.ReferenceEquals(other.SafeValue));
            }

            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    null => IsEmpty,
                    TKey value when IsKeyed => Equals(value),
                    TValue value => Equals(value),
                    TKey value => Equals(value),
                    Key<TKey, TValue> value => Equals(value),
                    IKey<TKey, TValue> value => Equals(value),
                    _ => false
                };
            }

            public override Boolean Equals(Object? other, IEqualityComparer<TValue>? comparer)
            {
                return other switch
                {
                    null => IsEmpty,
                    TKey value when IsKeyed => Equals(value),
                    TValue value => Equals(value, comparer),
                    TKey value => Equals(value),
                    Key<TKey, TValue> value => Equals(value, comparer),
                    IKey<TKey, TValue> value => Equals(value, comparer),
                    _ => false
                };
            }

            public override Boolean Equals(TKey? other)
            {
                return _key.Equals(other);
            }

            public override Boolean Equals(TKey? other, IEqualityComparer<TKey>? comparer)
            {
                return _key.Equals(other, comparer);
            }

            public override Boolean Equals(TValue? other)
            {
                return _value.Equals(other);
            }

            public override Boolean Equals(TValue? other, IEqualityComparer<TValue>? comparer)
            {
                return _value.Equals(other, comparer);
            }

            public override Boolean Equals(Key<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && _key.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.Equals(other.SafeValue));
            }

            public override Boolean Equals(Key<TKey, TValue>? other, IEqualityComparer<TValue>? comparer)
            {
                return ReferenceEquals(this, other) || other is not null && _key.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.Equals(other.SafeValue, comparer));
            }

            public override Boolean Equals(IKey<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && _key.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.Equals(other.SafeValue));
            }

            public override Boolean Equals(IKey<TKey, TValue>? other, IEqualityComparer<TValue>? comparer)
            {
                return ReferenceEquals(this, other) || other is not null && _key.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.Equals(other.SafeValue, comparer));
            }

            public override String? ToString()
            {
                return IsKeyed || _value.IsEmpty ? _key.ToString() : _value.ToString();
            }

            public override String ToString(String? format)
            {
                return IsKeyed || _value.IsEmpty ? _key.ToString((String?) null) : _value.ToString(format);
            }

            public override String ToString(IFormatProvider? provider)
            {
                return IsKeyed || _value.IsEmpty ? _key.ToString(provider) : _value.ToString(provider);
            }

            public override String ToString(String? format, IFormatProvider? provider)
            {
                return IsKeyed || _value.IsEmpty ? _key.ToString(null, provider) : _value.ToString(format, provider);
            }

            public override String? GetString()
            {
                return IsKeyed || _value.IsEmpty ? _key.GetString() : _value.GetString();
            }

            public override String? GetString(EscapeType escape)
            {
                return IsKeyed || _value.IsEmpty ? _key.GetString(escape) : _value.GetString(escape);
            }

            public override String? GetString(String? format)
            {
                return IsKeyed || _value.IsEmpty ? _key.GetString((String?) null) : _value.GetString(format);
            }

            public override String? GetString(EscapeType escape, String? format)
            {
                return IsKeyed || _value.IsEmpty ? _key.GetString(escape, (String?) null) : _value.GetString(escape, format);
            }

            public override String? GetString(IFormatProvider? provider)
            {
                return IsKeyed || _value.IsEmpty ? _key.GetString(provider) : _value.GetString(provider);
            }

            public override String? GetString(EscapeType escape, IFormatProvider? provider)
            {
                return IsKeyed || _value.IsEmpty ? _key.GetString(escape, provider) : _value.GetString(escape, provider);
            }

            public override String? GetString(String? format, IFormatProvider? provider)
            {
                return IsKeyed || _value.IsEmpty ? _key.GetString(null, provider) : _value.GetString(format, provider);
            }

            public override String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
            {
                return IsKeyed || _value.IsEmpty ? _key.GetString(escape, null, provider) : _value.GetString(escape, format, provider);
            }
        }

        private sealed class NotifyDynamic<TKey, TValue> : DynamicBase<TKey, TValue>
        {
            public override event PropertyChangingEventHandler? PropertyChanging;
            public override event PropertyChangedEventHandler? PropertyChanged;

            public NotifyDynamic(TryParseHandler<TValue, TKey> handler, TValue value)
                : this(handler, value, true)
            {
            }

            public NotifyDynamic(TryParseHandler<TValue, TKey> handler, TValue value, Boolean mutable)
                : base(handler, value, mutable)
            {
            }

            private NotifyDynamic(TryParseHandler<TValue, TKey> handler, NullMaybe<TValue> value, Boolean mutable)
                : base(handler, value, mutable)
            {
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            public override Boolean Set(TValue value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                (Boolean Value, Boolean IsEmpty) current = (!_value.ReferenceEquals(value), IsEmpty);
                (PropertyChangingEventHandler? changing, PropertyChangedEventHandler? changed) = (PropertyChanging, PropertyChanged);

                if (changing is not null)
                {
                    if (current.Value)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.Key);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeKey);
                        changing.Invoke(this, NotifyUtilities.Changing.Value);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeValue);
                    }

                    if (current.IsEmpty)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.IsEmpty);
                    }
                }

                _value = value;
                _hash = SafeKey.GetHashCode();

                if (changed is not null)
                {
                    if (current.Value)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.Key);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeKey);
                        changed.Invoke(this, NotifyUtilities.Changed.Value);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeValue);
                    }

                    if (current.IsEmpty)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.IsEmpty);
                    }
                }

                return true;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            protected override Boolean Set(Maybe<TKey> key, NullMaybe<TValue> value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                if (!key.IsEmpty && !value.IsEmpty && Handler(value, out TKey? result) && !key.Equals(result))
                {
                    return false;
                }

                (Boolean Value, Boolean IsEmpty) current = (!_value.ReferenceEquals(value), IsEmpty);
                (PropertyChangingEventHandler? changing, PropertyChangedEventHandler? changed) = (PropertyChanging, PropertyChanged);

                if (changing is not null)
                {
                    if (current.Value)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.Key);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeKey);
                        changing.Invoke(this, NotifyUtilities.Changing.Value);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeValue);
                    }

                    if (current.IsEmpty && !value.IsEmpty || !current.IsEmpty && value.IsEmpty)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.IsEmpty);
                    }
                }

                _value = value;
                _hash = SafeKey.GetHashCode();

                if (changed is not null)
                {
                    if (current.Value)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.Key);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeKey);
                        changed.Invoke(this, NotifyUtilities.Changed.Value);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeValue);
                    }

                    if (current.IsEmpty && !value.IsEmpty || !current.IsEmpty && value.IsEmpty)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.IsEmpty);
                    }
                }

                return true;
            }

            public override Key<TKey, TValue> Clone()
            {
                return new NotifyDynamic<TKey, TValue>(Handler, _value, !IsReadOnly);
            }

            public override void Dispose()
            {
                base.Dispose();
                PropertyChanging = null;
                PropertyChanged = null;
            }
        }

        private sealed class Dynamic<TKey, TValue> : DynamicBase<TKey, TValue>
        {
            public override event PropertyChangingEventHandler? PropertyChanging
            {
                add
                {
                }
                remove
                {
                }
            }

            public override event PropertyChangedEventHandler? PropertyChanged
            {
                add
                {
                }
                remove
                {
                }
            }

            public Dynamic(TryParseHandler<TValue, TKey> handler, TValue value, Boolean mutable)
                : base(handler, value, mutable)
            {
            }

            private Dynamic(TryParseHandler<TValue, TKey> handler, NullMaybe<TValue> value, Boolean mutable)
                : base(handler, value, mutable)
            {
            }

            public override Key<TKey, TValue> Clone()
            {
                return new Dynamic<TKey, TValue>(Handler, _value, !IsReadOnly);
            }
        }

        private abstract class DynamicBase<TKey, TValue> : Key<TKey, TValue>
        {
            private protected readonly TryParseHandler<TValue, TKey> Handler;

            public sealed override TKey Key
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return SafeKey.Value;
                }
            }

            public sealed override Maybe<TKey> SafeKey
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Handler(Value, out TKey? key) ? key : default(Maybe<TKey>);
                }
            }

            private protected NullMaybe<TValue> _value;
            public sealed override TValue Value
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _value.Value;
                }
            }

            public sealed override Maybe<TValue> SafeValue
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _value.Value;
                }
            }

            public sealed override Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _value.IsEmpty;
                }
            }

            private protected Int32 _hash;

            protected DynamicBase(TryParseHandler<TValue, TKey> handler, TValue value, Boolean mutable)
                : this(handler, (NullMaybe<TValue>) value, mutable)
            {
            }

            protected DynamicBase(TryParseHandler<TValue, TKey> handler, NullMaybe<TValue> value, Boolean mutable)
                : base(mutable)
            {
                Handler = handler;
                _value = value;
                _hash = SafeKey.GetHashCode();
            }

            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);

                if (SafeKey.Unwrap(out TKey? key))
                {
                    info.AddValue(nameof(Key), key);
                }

                if (!_value.IsEmpty)
                {
                    info.AddValue(nameof(Value), _value.Value);
                }
            }

            public override Boolean Set(TValue value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                _value = value;
                _hash = SafeKey.GetHashCode();
                return true;
            }

            [SuppressMessage("ReSharper", "RedundantCast")]
            public sealed override Boolean Set(TKey key, TValue value)
            {
                return Set((Maybe<TKey>) key, (NullMaybe<TValue>) value);
            }

            protected virtual Boolean Set(Maybe<TKey> key, NullMaybe<TValue> value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                if (value.IsEmpty)
                {
                    _value = value;
                    _hash = SafeKey.GetHashCode();
                    return true;
                }

                if (!key.IsEmpty && Handler(value, out TKey? result) && !key.Equals(result))
                {
                    return false;
                }

                _value = value;
                _hash = SafeKey.GetHashCode();
                return true;
            }

            public sealed override Boolean Reset()
            {
                return Set(default, default);
            }

            public override void Dispose()
            {
                Reset();
            }

            public override Int32 CompareTo(TKey? other)
            {
                return SafeKey.CompareTo(other);
            }

            public override Int32 CompareTo(TKey? other, IComparer<TKey>? comparer)
            {
                return SafeKey.CompareTo(other, comparer);
            }

            public override Int32 CompareTo(TValue? other)
            {
                if (_value.IsEmpty)
                {
                    return other is null ? 0 : -1;
                }

                return _value.CompareTo(other);
            }

            public override Int32 CompareTo(TValue? other, IComparer<TValue>? comparer)
            {
                if (_value.IsEmpty)
                {
                    return other is null ? 0 : -1;
                }

                return _value.CompareTo(other, comparer);
            }

            public override Int32 CompareTo(Key<TKey, TValue>? other)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = SafeKey.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : _value.CompareTo(other.SafeValue);
            }

            public override Int32 CompareTo(Key<TKey, TValue>? other, IComparer<TValue>? comparer)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = SafeKey.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : _value.CompareTo(other.SafeValue, comparer);
            }

            public override Int32 CompareTo(IKey<TKey, TValue>? other)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = SafeKey.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : _value.CompareTo(other.SafeValue);
            }

            public override Int32 CompareTo(IKey<TKey, TValue>? other, IComparer<TValue>? comparer)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = SafeKey.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : _value.CompareTo(other.SafeValue, comparer);
            }

            [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
            public sealed override Int32 GetHashCode()
            {
                Maybe<TKey> key = SafeKey;
                return key.IsEmpty ? _hash : key.GetHashCode();
            }

            public override Boolean ReferenceEquals(TKey? other)
            {
                return SafeKey.ReferenceEquals(other);
            }

            public override Boolean ReferenceEquals(TValue? other)
            {
                return _value.ReferenceEquals(other);
            }

            public override Boolean ReferenceEquals(Key<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.ReferenceEquals(other.SafeValue));
            }

            public override Boolean ReferenceEquals(IKey<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.ReferenceEquals(other.SafeValue));
            }

            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    null => IsEmpty,
                    TKey value when IsKeyed => Equals(value),
                    TValue value => Equals(value),
                    TKey value => Equals(value),
                    Key<TKey, TValue> value => Equals(value),
                    IKey<TKey, TValue> value => Equals(value),
                    _ => false
                };
            }

            public override Boolean Equals(Object? other, IEqualityComparer<TValue>? comparer)
            {
                return other switch
                {
                    null => IsEmpty,
                    TKey value when IsKeyed => Equals(value),
                    TValue value => Equals(value, comparer),
                    TKey value => Equals(value),
                    Key<TKey, TValue> value => Equals(value, comparer),
                    IKey<TKey, TValue> value => Equals(value, comparer),
                    _ => false
                };
            }

            public override Boolean Equals(TKey? other)
            {
                return SafeKey.Equals(other);
            }

            public override Boolean Equals(TKey? other, IEqualityComparer<TKey>? comparer)
            {
                return SafeKey.Equals(other, comparer);
            }

            public override Boolean Equals(TValue? other)
            {
                return _value.Equals(other);
            }

            public override Boolean Equals(TValue? other, IEqualityComparer<TValue>? comparer)
            {
                return _value.Equals(other, comparer);
            }

            public override Boolean Equals(Key<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.Equals(other.SafeValue));
            }

            public override Boolean Equals(Key<TKey, TValue>? other, IEqualityComparer<TValue>? comparer)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.Equals(other.SafeValue, comparer));
            }

            public override Boolean Equals(IKey<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.Equals(other.SafeValue));
            }

            public override Boolean Equals(IKey<TKey, TValue>? other, IEqualityComparer<TValue>? comparer)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || _value.Equals(other.SafeValue, comparer));
            }

            public override String? ToString()
            {
                return _value.IsEmpty ? null : IsKeyed ? SafeKey.ToString() : _value.ToString();
            }

            public override String ToString(String? format)
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.ToString((String?) null) : _value.ToString(format);
            }

            public override String ToString(IFormatProvider? provider)
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.ToString(provider) : _value.ToString(provider);
            }

            public override String ToString(String? format, IFormatProvider? provider)
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.ToString(provider) : _value.ToString(format, provider);
            }

            public override String? GetString()
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString() : _value.GetString();
            }

            public override String? GetString(EscapeType escape)
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(escape) : _value.GetString(escape);
            }

            public override String? GetString(String? format)
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString() : _value.GetString(format);
            }

            public override String? GetString(EscapeType escape, String? format)
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(escape) : _value.GetString(escape, format);
            }

            public override String? GetString(IFormatProvider? provider)
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(provider) : _value.GetString(provider);
            }

            public override String? GetString(EscapeType escape, IFormatProvider? provider)
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(escape, provider) : _value.GetString(escape, provider);
            }

            public override String? GetString(String? format, IFormatProvider? provider)
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(provider) : _value.GetString(format, provider);
            }

            public override String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
            {
                return _value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(escape, provider) : _value.GetString(escape, format, provider);
            }
        }

        private sealed class NotifyWeak<TKey, TValue> : NotifyWeakBase<TKey, TValue> where TValue : class?
        {
            public NotifyWeak(TryParseHandler<TValue, TKey> handler, TValue value)
                : this(handler, value, true)
            {
            }

            public NotifyWeak(TryParseHandler<TValue, TKey> handler, TValue value, Boolean mutable)
                : base(handler, value, mutable)
            {
            }

            private NotifyWeak(TryParseHandler<TValue, TKey> handler, WeakMaybe<TValue> value, Boolean mutable)
                : base(handler, value, mutable)
            {
            }
        }

        private abstract class NotifyWeakBase<TKey, TValue> : WeakBase<TKey, TValue> where TValue : class?
        {
            public override event PropertyChangingEventHandler? PropertyChanging;
            public override event PropertyChangedEventHandler? PropertyChanged;

            protected NotifyWeakBase(TryParseHandler<TValue, TKey> handler, TValue value)
                : this(handler, value, true)
            {
            }

            protected NotifyWeakBase(TryParseHandler<TValue, TKey> handler, TValue value, Boolean mutable)
                : base(handler, value, mutable)
            {
            }

            protected NotifyWeakBase(TryParseHandler<TValue, TKey> handler, WeakMaybe<TValue> value, Boolean mutable)
                : base(handler, value, mutable)
            {
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            public override Boolean Set(TValue value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                (Boolean Value, Boolean IsEmpty) current = (!_value.ReferenceEquals(value), IsEmpty);
                (PropertyChangingEventHandler? changing, PropertyChangedEventHandler? changed) = (PropertyChanging, PropertyChanged);

                if (changing is not null)
                {
                    if (current.Value)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.Key);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeKey);
                        changing.Invoke(this, NotifyUtilities.Changing.Value);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeValue);
                    }

                    if (current.IsEmpty)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.IsEmpty);
                    }
                }

                _value = value;
                _hash = SafeKey.GetHashCode();

                if (changed is not null)
                {
                    if (current.Value)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.Key);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeKey);
                        changed.Invoke(this, NotifyUtilities.Changed.Value);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeValue);
                    }

                    if (current.IsEmpty)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.IsEmpty);
                    }
                }

                return true;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            protected override Boolean Set(Maybe<TKey> key, WeakMaybe<TValue> value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                Maybe<TValue> maybe = value.Maybe;
                if (!key.IsEmpty && maybe.Unwrap(out TValue? @new) && Handler(@new, out TKey? result) && !key.Equals(result))
                {
                    return false;
                }

                (Boolean Value, Boolean IsEmpty) current = (!_value.ReferenceEquals(value), IsEmpty);
                (PropertyChangingEventHandler? changing, PropertyChangedEventHandler? changed) = (PropertyChanging, PropertyChanged);

                if (changing is not null)
                {
                    if (current.Value)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.Key);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeKey);
                        changing.Invoke(this, NotifyUtilities.Changing.Value);
                        changing.Invoke(this, NotifyUtilities.Changing.SafeValue);
                    }

                    if (current.IsEmpty && !maybe.IsEmpty || !current.IsEmpty && maybe.IsEmpty)
                    {
                        changing.Invoke(this, NotifyUtilities.Changing.IsEmpty);
                    }
                }

                _value = value;
                _hash = SafeKey.GetHashCode();

                if (changed is not null)
                {
                    if (current.Value)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.Key);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeKey);
                        changed.Invoke(this, NotifyUtilities.Changed.Value);
                        changed.Invoke(this, NotifyUtilities.Changed.SafeValue);
                    }

                    if (current.IsEmpty && !maybe.IsEmpty || !current.IsEmpty && maybe.IsEmpty)
                    {
                        changed.Invoke(this, NotifyUtilities.Changed.IsEmpty);
                    }
                }

                return true;
            }

            public override Key<TKey, TValue> Clone()
            {
                return new NotifyWeak<TKey, TValue>(Handler, _value, !IsReadOnly);
            }

            public override void Dispose()
            {
                base.Dispose();
                PropertyChanging = null;
                PropertyChanged = null;
            }
        }

        private sealed class Weak<TKey, TValue> : WeakBase<TKey, TValue> where TValue : class?
        {
            public override event PropertyChangingEventHandler? PropertyChanging
            {
                add
                {
                }
                remove
                {
                }
            }

            public override event PropertyChangedEventHandler? PropertyChanged
            {
                add
                {
                }
                remove
                {
                }
            }

            public Weak(TryParseHandler<TValue, TKey> handler, TValue value, Boolean mutable)
                : base(handler, value, mutable)
            {
            }

            public Weak(TryParseHandler<TValue, TKey> handler, WeakMaybe<TValue> value, Boolean mutable)
                : base(handler, value, mutable)
            {
            }

            public override Key<TKey, TValue> Clone()
            {
                return new Weak<TKey, TValue>(Handler, _value, !IsReadOnly);
            }
        }

        private abstract class WeakBase<TKey, TValue> : Key<TKey, TValue> where TValue : class?
        {
            private protected readonly TryParseHandler<TValue, TKey> Handler;

            public sealed override TKey Key
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return SafeKey.Value;
                }
            }

            public sealed override Maybe<TKey> SafeKey
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return SafeValue.Unwrap(out TValue? value) && Handler(value, out TKey? key) ? key : default(Maybe<TKey>);
                }
            }

            private protected WeakMaybe<TValue> _value;
            public sealed override TValue Value
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _value.Value;
                }
            }

            public sealed override Maybe<TValue> SafeValue
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _value.Maybe;
                }
            }

            public sealed override Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _value.IsEmpty;
                }
            }

            private protected Int32 _hash;

            protected WeakBase(TryParseHandler<TValue, TKey> handler, TValue value, Boolean mutable)
                : this(handler, (WeakMaybe<TValue>) value, mutable)
            {
            }

            protected WeakBase(TryParseHandler<TValue, TKey> handler, WeakMaybe<TValue> value, Boolean mutable)
                : base(mutable)
            {
                Handler = handler;
                _value = value;
                _hash = SafeKey.GetHashCode();
            }

            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);

                if (SafeKey.Unwrap(out TKey? key))
                {
                    info.AddValue(nameof(Key), key);
                }

                if (SafeValue.Unwrap(out TValue? value))
                {
                    info.AddValue(nameof(Value), value);
                }
            }

            public override Boolean Set(TValue value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                _value = value;
                _hash = SafeKey.GetHashCode();
                return true;
            }

            [SuppressMessage("ReSharper", "RedundantCast")]
            public sealed override Boolean Set(TKey key, TValue value)
            {
                return Set((Maybe<TKey>) key, (WeakMaybe<TValue>) value);
            }

            protected virtual Boolean Set(Maybe<TKey> key, WeakMaybe<TValue> value)
            {
                if (IsReadOnly)
                {
                    return false;
                }

                if (value.IsEmpty)
                {
                    _value = value;
                    _hash = SafeKey.GetHashCode();
                    return true;
                }

                if (!key.IsEmpty && Handler(value, out TKey? result) && !key.Equals(result))
                {
                    return false;
                }

                _value = value;
                _hash = SafeKey.GetHashCode();
                return true;
            }

            public sealed override Boolean Reset()
            {
                return Set(default, default);
            }

            public override void Dispose()
            {
                Reset();
            }

            public override Int32 CompareTo(TKey? other)
            {
                return SafeKey.CompareTo(other);
            }

            public override Int32 CompareTo(TKey? other, IComparer<TKey>? comparer)
            {
                return SafeKey.CompareTo(other, comparer);
            }

            public override Int32 CompareTo(TValue? other)
            {
                Maybe<TValue> value = SafeValue;

                if (value.IsEmpty)
                {
                    return other is null ? 0 : -1;
                }

                return value.CompareTo(other);
            }

            public override Int32 CompareTo(TValue? other, IComparer<TValue>? comparer)
            {
                Maybe<TValue> value = SafeValue;

                if (value.IsEmpty)
                {
                    return other is null ? 0 : -1;
                }

                return value.CompareTo(other, comparer);
            }

            public override Int32 CompareTo(Key<TKey, TValue>? other)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = SafeKey.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : SafeValue.CompareTo(other.SafeValue);
            }

            public override Int32 CompareTo(Key<TKey, TValue>? other, IComparer<TValue>? comparer)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = SafeKey.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : SafeValue.CompareTo(other.SafeValue, comparer);
            }

            public override Int32 CompareTo(IKey<TKey, TValue>? other)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = SafeKey.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : SafeValue.CompareTo(other.SafeValue);
            }

            public override Int32 CompareTo(IKey<TKey, TValue>? other, IComparer<TValue>? comparer)
            {
                if (other is null)
                {
                    return 1;
                }

                Int32 compare = SafeKey.CompareTo(other.SafeKey);
                return compare != 0 || IsKeyed || other.IsKeyed ? compare : SafeValue.CompareTo(other.SafeValue, comparer);
            }

            [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
            public sealed override Int32 GetHashCode()
            {
                Maybe<TKey> key = SafeKey;
                return key.IsEmpty ? _hash : key.GetHashCode();
            }

            public override Boolean ReferenceEquals(TKey? other)
            {
                return SafeKey.ReferenceEquals(other);
            }

            public override Boolean ReferenceEquals(TValue? other)
            {
                return SafeValue.ReferenceEquals(other);
            }

            public override Boolean ReferenceEquals(Key<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || SafeValue.ReferenceEquals(other.SafeValue));
            }

            public override Boolean ReferenceEquals(IKey<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || SafeValue.ReferenceEquals(other.SafeValue));
            }

            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    null => IsEmpty,
                    TKey value when IsKeyed => Equals(value),
                    TValue value => Equals(value),
                    TKey value => Equals(value),
                    Key<TKey, TValue> value => Equals(value),
                    IKey<TKey, TValue> value => Equals(value),
                    _ => false
                };
            }

            public override Boolean Equals(Object? other, IEqualityComparer<TValue>? comparer)
            {
                return other switch
                {
                    null => IsEmpty,
                    TKey value when IsKeyed => Equals(value),
                    TValue value => Equals(value, comparer),
                    TKey value => Equals(value),
                    Key<TKey, TValue> value => Equals(value, comparer),
                    IKey<TKey, TValue> value => Equals(value, comparer),
                    _ => false
                };
            }

            public override Boolean Equals(TKey? other)
            {
                return SafeKey.Equals(other);
            }

            public override Boolean Equals(TKey? other, IEqualityComparer<TKey>? comparer)
            {
                return SafeKey.Equals(other, comparer);
            }

            public override Boolean Equals(TValue? other)
            {
                return SafeValue.Equals(other);
            }

            public override Boolean Equals(TValue? other, IEqualityComparer<TValue>? comparer)
            {
                return SafeValue.Equals(other, comparer);
            }

            public override Boolean Equals(Key<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || SafeValue.Equals(other.SafeValue));
            }

            public override Boolean Equals(Key<TKey, TValue>? other, IEqualityComparer<TValue>? comparer)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || SafeValue.Equals(other.SafeValue, comparer));
            }

            public override Boolean Equals(IKey<TKey, TValue>? other)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || SafeValue.Equals(other.SafeValue));
            }

            public override Boolean Equals(IKey<TKey, TValue>? other, IEqualityComparer<TValue>? comparer)
            {
                return ReferenceEquals(this, other) || other is not null && SafeKey.Equals(other.SafeKey) && (IsKeyed || other.IsKeyed || SafeValue.Equals(other.SafeValue, comparer));
            }

            public override String? ToString()
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? null : IsKeyed ? SafeKey.ToString() : value.ToString();
            }

            public override String ToString(String? format)
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.ToString((String?) null) : value.ToString(format);
            }

            public override String ToString(IFormatProvider? provider)
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.ToString(provider) : value.ToString(provider);
            }

            public override String ToString(String? format, IFormatProvider? provider)
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.ToString(provider) : value.ToString(format, provider);
            }

            public override String? GetString()
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString() : value.GetString();
            }

            public override String? GetString(EscapeType escape)
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(escape) : value.GetString(escape);
            }

            public override String? GetString(String? format)
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString() : value.GetString(format);
            }

            public override String? GetString(EscapeType escape, String? format)
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(escape) : value.GetString(escape, format);
            }

            public override String? GetString(IFormatProvider? provider)
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(provider) : value.GetString(provider);
            }

            public override String? GetString(EscapeType escape, IFormatProvider? provider)
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(escape, provider) : value.GetString(escape, provider);
            }

            public override String? GetString(String? format, IFormatProvider? provider)
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(provider) : value.GetString(format, provider);
            }

            public override String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
            {
                Maybe<TValue> value = SafeValue;
                return value.IsEmpty ? String.Empty : IsKeyed ? SafeKey.GetString(escape, provider) : value.GetString(escape, format, provider);
            }
        }

        private sealed class SmartWeak<TKey, TValue> : NotifyWeakBase<TKey, TValue> where TValue : class?
        {
#pragma warning disable CS8634
            private static ConditionalWeakTable<TValue, Notifier> Storage { get; } = new ConditionalWeakTable<TValue, Notifier>();
#pragma warning restore CS8634

            private Action<Key<TKey, TValue>>? Notify { get; }
            private SynchronizationContext? Context;

            public SmartWeak(TryParseHandler<TValue, TKey> handler, Action<Key<TKey, TValue>>? notify)
                : base(handler, default(WeakMaybe<TValue>), true)
            {
                Notify = notify;
            }

            public SmartWeak(TryParseHandler<TValue, TKey> handler, TValue value, Action<Key<TKey, TValue>>? notify)
                : base(handler, value, true)
            {
                Notify = notify;

                Notifier notifier = Storage.GetOrCreateValue(value);
                lock (notifier)
                {
                    notifier.Add(this);
                }
            }

            public SmartWeak(TryParseHandler<TValue, TKey> handler, WeakMaybe<TValue> value, Action<Key<TKey, TValue>>? notify)
                : base(handler, value, true)
            {
                Notify = notify;

                if (!value.Unwrap(out TValue? result))
                {
                    return;
                }

                Notifier notifier = Storage.GetOrCreateValue(result);
                lock (notifier)
                {
                    notifier.Add(this);
                }
            }

            public override Boolean Set(TValue value)
            {
                Maybe<TValue> previous = SafeValue;

                if (!base.Set(value))
                {
                    return false;
                }

                Interlocked.Exchange(ref Context, SynchronizationContext.Current);

                if (previous.Unwrap(out TValue? result) && Storage.TryGetValue(result, out Notifier? notifier))
                {
                    lock (notifier)
                    {
                        notifier.Remove(this);
                    }
                }

                notifier = Storage.GetOrCreateValue(value);

                lock (notifier)
                {
                    notifier.Add(this);
                }

                return true;
            }

            protected override Boolean Set(Maybe<TKey> key, WeakMaybe<TValue> value)
            {
                Maybe<TValue> previous = SafeValue;

                if (!base.Set(key, value))
                {
                    return false;
                }

                Interlocked.Exchange(ref Context, SynchronizationContext.Current);

                if (previous.Unwrap(out TValue? result) && Storage.TryGetValue(result, out Notifier? notifier))
                {
                    lock (notifier)
                    {
                        notifier.Remove(this);
                    }
                }

                if (!value.Unwrap(out result))
                {
                    return true;
                }

                notifier = Storage.GetOrCreateValue(result);

                lock (notifier)
                {
                    notifier.Add(this);
                }

                return true;
            }

            private void Reset(Unit _)
            {
                if (Context is null)
                {
                    Reset();

                    try
                    {
                        Notify?.Invoke(this);
                    }
                    catch (Exception)
                    {
                    }

                    return;
                }

                try
                {
                    Context.Post(static state =>
                    {
                        SmartWeak<TKey, TValue> @this = (SmartWeak<TKey, TValue>) state!;

                        @this.Reset();

                        try
                        {
                            @this.Notify?.Invoke(@this);
                        }
                        catch (Exception)
                        {
                        }
                    }, this);
                }
                catch (ObjectDisposedException)
                {
                    Context = null;
                    Reset();

                    try
                    {
                        Notify?.Invoke(this);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            private sealed class Notifier : IDisposable
            {
                private readonly List<WeakReference<SmartWeak<TKey, TValue>>> Weak;

                public Notifier()
                {
                    Weak = new List<WeakReference<SmartWeak<TKey, TValue>>>(1);
                }

                public Notifier(SmartWeak<TKey, TValue> weak)
                {
                    Weak = new List<WeakReference<SmartWeak<TKey, TValue>>>(1);
                    Add(weak);
                }

                public void Add(SmartWeak<TKey, TValue> weak)
                {
                    Weak.Add(new WeakReference<SmartWeak<TKey, TValue>>(weak));
                }

                public void Remove(SmartWeak<TKey, TValue> weak)
                {
                    Weak.RemoveAll(reference => !reference.TryGetTarget(out SmartWeak<TKey, TValue>? target) || ReferenceEquals(target, weak));
                }

                public void Dispose()
                {
                    foreach (WeakReference<SmartWeak<TKey, TValue>> weak in Weak)
                    {
                        if (weak.TryGetTarget(out SmartWeak<TKey, TValue>? target))
                        {
                            target.Reset(Unit.Default);
                        }
                    }
                }

                ~Notifier()
                {
                    Dispose();
                }
            }
        }

        private enum State : Byte
        {
            None,
            Mutable,
            Immutable
        }
    }
}