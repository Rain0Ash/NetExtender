// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NetExtender.Interfaces;
using NetExtender.Newtonsoft.Types.Monads;
using NetExtender.Types.Entities.Interfaces;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [JsonConverter(typeof(BoxJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.BoxJsonConverter<>))]
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

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
        public static implicit operator Task<Box<T>>(Box<T> value)
        {
            return value is not null ? Task.FromResult(value) : TaskUtilities<Box<T>?>.Default!;
        }

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
        public static implicit operator ValueTask<Box<T>>(Box<T> value)
        {
            return value is not null ? ValueTask.FromResult(value) : ValueTaskUtilities<Box<T>?>.Default!;
        }

        public static Boolean operator ==(Box<T>? first, Box<T>? second)
        {
            return EqualityComparer<Box<T>>.Default.Equals(first, second);
        }

        public static Boolean operator !=(Box<T>? first, Box<T>? second)
        {
            return !(first == second);
        }

#pragma warning disable CS0067
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067

        public Guid Id { get; private set; }

        private T _value;
        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value;
            }
            private set
            {
                this.RaiseAndSetIfChanged(ref _value, value);
            }
        }
        Object? IBox.Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Value;
            }
        }

        private readonly State _state;
        public Boolean IsReadOnly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _state is not State.Mutable;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        internal Box(Guid id, T value)
            : this(id, value, State.Immutable)
        {
        }

        internal Box(Guid id, T value, Boolean mutable)
            : this(id, value, mutable ? State.Mutable : State.Immutable)
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
        public static Guid NewGuid()
        {
            return DateTimeOffset.UtcNow.NewGuid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static Guid NewGuid(State state)
        {
            return NewGuid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Box<T> Mutable(T value)
        {
            return new Box<T>(value, State.Mutable);
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (IsEmpty)
            {
                value = null;
                return false;
            }

            value = _value;
            return true;
        }

        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            if (IsEmpty)
            {
                value = default;
                return false;
            }

            value = _value;
            return true;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Box<T> Initialize()
        {
            if (Id == Guid.Empty)
            {
                Id = NewGuid(_state);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Guid IEntity<Guid>.Get()
        {
            return Id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Set(T value)
        {
            if (IsReadOnly)
            {
                return false;
            }

            Value = value;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return comparer.SafeCompare(_value, other) ?? 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Box<T>? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Box<T>? other, IComparer<T>? comparer)
        {
            return ReferenceEquals(this, other) ? 0 : other is not null ? CompareTo(other._value, comparer) : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IBox<T>? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IBox<T>? other, IComparer<T>? comparer)
        {
            return ReferenceEquals(this, other) ? 0 : other is not null ? CompareTo(other.Value, comparer) : 1;
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return IsReadOnly ? _value is not null ? _value.GetHashCode() : 0 : Id.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(T? other)
        {
            return default(T) is null ? ReferenceEquals(_value, other) : Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(Box<T>? other)
        {
            return ReferenceEquals(this, other) || other is not null && ReferenceEquals(other._value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IBox<T>? other)
        {
            return ReferenceEquals(this, other) || other is not null && ReferenceEquals(other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(_value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Box<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Box<T>? other, IEqualityComparer<T>? comparer)
        {
            return ReferenceEquals(this, other) || other is not null && Equals(other._value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IBox<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IBox<T>? other, IEqualityComparer<T>? comparer)
        {
            return ReferenceEquals(this, other) || other is not null && Equals(other.Value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Box<T> Clone()
        {
            return new Box<T>(_value, _state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        INotifyBox<T> INotifyBox<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        INotifyBox<T> ICloneable<INotifyBox<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        INotifyBox INotifyBox.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        INotifyBox ICloneable<INotifyBox>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBox<T> IBox<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBox<T> ICloneable<IBox<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBox IBox.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBox ICloneable<IBox>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad<T> IMonad<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad<T> ICloneable<IMonad<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad IMonad.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Object ICloneable.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override String? ToString()
        {
            return _value is not null ? _value.ToString() : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return _value is not null ? StringUtilities.ToString(in _value, format, provider) : String.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString()
        {
            return IStringProvider.Default.GetString(_value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape)
        {
            return IStringProvider.Default.GetString(_value, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format)
        {
            return IStringProvider.Default.GetString(_value, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format)
        {
            return IStringProvider.Default.GetString(_value, escape, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(_value, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(_value, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format, IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(_value, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(_value, escape, format, provider);
        }

        private enum State : Byte
        {
            None,
            Mutable,
            Immutable
        }
    }
}