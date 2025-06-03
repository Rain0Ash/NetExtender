// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Types.Entities.Interfaces;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public sealed class Box<T> : INotifyBox<T>, IEntityId<Guid>, IBoxEquality<T, Box<T>>, ISerializable
    {
        public static implicit operator Box<T>(T value)
        {
            return new Box<T>(value);
        }

        [return: NotNullIfNotNull("box")]
        public static implicit operator T?(Box<T>? box)
        {
            return box is not null ? box.Value : default;
        }

        public static Boolean operator ==(Box<T>? first, Box<T>? second)
        {
            return EqualityComparer<Box<T>>.Default.Equals(first, second);
        }

        public static Boolean operator !=(Box<T>? first, Box<T>? second)
        {
            return !(first == second);
        }

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        public Guid Id { get; }

        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
        }
        Object? IBox.Value
        {
            get
            {
                return Value;
            }
        }

        private readonly State _state;
        public Boolean IsReadOnly
        {
            get
            {
                return _state is not State.Mutable;
            }
        }

        Boolean IMonad.IsEmpty
        {
            get
            {
                return _state is State.None;
            }
        }

        public Box(T value)
            : this(value, State.Immutable)
        {
        }

        public Box(T value, Boolean mutable)
            : this(value, mutable ? State.Mutable : State.Immutable)
        {
        }

        private Box(T value, State state)
            : this(NewGuid(state), value, state)
        {
        }

        private Box(Guid id, T value, State state)
        {
            Id = id;
            _value = value;
            _state = state;
        }

        private Box(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetValue<Guid>(nameof(Id));
            _value = info.GetValue<T>(nameof(Value));
            _state = (State) info.GetValueOrDefault<Byte>(nameof(State)) switch
            {
                State.None => State.Immutable,
                State.Mutable => State.Mutable,
                State.Immutable => State.Immutable,
                var state => throw new SerializationException($"Box state '{state}' is not supported.")
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static Guid NewGuid(State state)
        {
            return DateTimeOffset.UtcNow.NewGuid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Box<T> Mutable(T value)
        {
            return new Box<T>(value, State.Mutable);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Value), _value);

            if (_state is not State.None and State.Mutable)
            {
                info.AddValue(nameof(State), (Byte) _state);
            }
        }

        Guid IEntity<Guid>.Get()
        {
            return Id;
        }

        public Boolean Set(T value)
        {
            if (IsReadOnly)
            {
                return false;
            }

            this.RaiseAndSetIfChanged(ref _value, value, nameof(Value));
            return true;
        }

        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return comparer.SafeCompare(_value, other) ?? 0;
        }

        public Int32 CompareTo(Box<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(Box<T>? other, IComparer<T>? comparer)
        {
            return ReferenceEquals(this, other) ? 0 : other is not null ? CompareTo(other._value, comparer) : 1;
        }

        public Int32 CompareTo(IBox<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(IBox<T>? other, IComparer<T>? comparer)
        {
            return ReferenceEquals(this, other) ? 0 : other is not null ? CompareTo(other.Value, comparer) : 1;
        }

        public override Int32 GetHashCode()
        {
            return IsReadOnly ? _value is not null ? _value.GetHashCode() : 0 : Id.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                null => _value is null,
                T value => Equals(value, comparer),
                Box<T> value => Equals(value, comparer),
                _ => _value is not null && _value.Equals(other)
            };
        }

        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(_value, other);
        }

        public Boolean Equals(Box<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Box<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other._value, comparer);
        }

        public Boolean Equals(IBox<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IBox<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Value, comparer);
        }

        public Box<T> Clone()
        {
            return new Box<T>(_value, _state);
        }

        INotifyBox<T> INotifyBox<T>.Clone()
        {
            return Clone();
        }

        INotifyBox<T> ICloneable<INotifyBox<T>>.Clone()
        {
            return Clone();
        }

        INotifyBox INotifyBox.Clone()
        {
            return Clone();
        }

        INotifyBox ICloneable<INotifyBox>.Clone()
        {
            return Clone();
        }

        IBox<T> IBox<T>.Clone()
        {
            return Clone();
        }

        IBox<T> ICloneable<IBox<T>>.Clone()
        {
            return Clone();
        }

        IBox IBox.Clone()
        {
            return Clone();
        }

        IBox ICloneable<IBox>.Clone()
        {
            return Clone();
        }

        IMonad<T> IMonad<T>.Clone()
        {
            return Clone();
        }

        IMonad<T> ICloneable<IMonad<T>>.Clone()
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

        public override String? ToString()
        {
            return _value is not null ? _value.ToString() : null;
        }

        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        public String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return _value is not null ? StringUtilities.ToString(in _value, format, provider) : String.Empty;
        }

        public String? GetString()
        {
            return _value.GetString();
        }

        public String? GetString(EscapeType escape)
        {
            return _value.GetString(escape);
        }

        public String? GetString(String? format)
        {
            return _value.GetString(format);
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return _value.GetString(escape, format);
        }

        public String? GetString(IFormatProvider? provider)
        {
            return _value.GetString(provider);
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return _value.GetString(escape, provider);
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return _value.GetString(format, provider);
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return _value.GetString(escape, format, provider);
        }

        private enum State : Byte
        {
            None,
            Mutable,
            Immutable
        }
    }
}