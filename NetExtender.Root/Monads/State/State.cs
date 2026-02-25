// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NetExtender.Interfaces;
using NetExtender.Newtonsoft.Types.Monads;
using NetExtender.Exceptions;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    public enum StateEquality : Byte
    {
        Current,
        Value
    }

    [Serializable]
    [JsonConverter(typeof(StateJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.StateJsonConverter<>))]
    public readonly struct State<T> : IEqualityStruct<State<T>>, IState<T>, IStateEquality<T, State<T>>, ICloneable<State<T>>, ISerializable
    {
        public static implicit operator T(State<T> value)
        {
            return value.Current;
        }

        public static implicit operator State<T>(T value)
        {
            return new State<T>(value);
        }

        public static implicit operator Task<State<T>>(State<T> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<State<T>>.Default;
        }

        public static implicit operator ValueTask<State<T>>(State<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<State<T>>.Default;
        }

        public static Boolean operator ==(T? first, State<T> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, State<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(State<T> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(State<T> first, T? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(State<T> first, State<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(State<T> first, State<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator <(T? first, State<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, State<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(T? first, State<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, State<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(State<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(State<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(State<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(State<T> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(State<T> first, State<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(State<T> first, State<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(State<T> first, State<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(State<T> first, State<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        private readonly Maybe<T> _value;
        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value.Internal;
            }
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Next.Unwrap(out T? next) ? next : _value.Internal;
            }
        }

        public Maybe<T> Next { get; init; }

        public Boolean HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Next.HasValue;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value.IsEmpty && Next.IsEmpty;
            }
        }

        public State(T value)
        {
            _value = value;
            Next = default;
        }

        public State(T value, T next)
        {
            _value = value;
            Next = next;
        }

        public State(T value, Maybe<T> next)
        {
            _value = value;
            Next = next;
        }

        private State(SerializationInfo info, StreamingContext context)
        {
            _value = info.GetValue<T>(nameof(Value));
            Next = info.GetBoolean(nameof(HasNext)) ? info.GetValue<T>(nameof(Next)) : default(Maybe<T>);
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (IsEmpty)
            {
                value = null;
                return false;
            }

            value = Current;
            return true;
        }

        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            if (IsEmpty)
            {
                value = default;
                return false;
            }

            value = Current;
            return true;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), Value);

            if (Next.Unwrap(out T? next))
            {
                info.AddValue(nameof(HasNext), true);
                info.AddValue(nameof(Next), next);
                return;
            }

            info.AddValue(nameof(HasNext), false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean HasDifference()
        {
            return HasDifference(null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean HasDifference(IEqualityComparer<T>? comparer)
        {
            return HasNext && !Equals(Value, StateEquality.Current, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get()
        {
            return Get(default);
        }

        public T Get(StateEquality equality)
        {
            return equality switch
            {
                StateEquality.Current => Current,
                StateEquality.Value => Value,
                _ => throw new EnumUndefinedOrNotSupportedException<StateEquality>(equality, nameof(equality), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Set(T value)
        {
            return new State<T>(value) { Next = Next };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Set(T value)
        {
            return Set(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Save()
        {
            return Next ? new State<T>(Next.Value) : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState IState.Save()
        {
            return Save();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Save()
        {
            return Save();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Save(T value)
        {
            return new State<T>(Current) { Next = value };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Save(T value)
        {
            return Save(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Update(T value)
        {
            return new State<T>(Value) { Next = value };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Update(T value)
        {
            return Update(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Update(Maybe<T> value)
        {
            return new State<T>(Value) { Next = value };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Update(Maybe<T> value)
        {
            return Update(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Difference()
        {
            return Difference(out State<T> result) is not null ? result : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean? Difference(out State<T> result)
        {
            return Difference(null, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Difference()
        {
            return Difference();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Difference(IEqualityComparer<T>? comparer)
        {
            return Difference(comparer, out State<T> result) is not null ? result : this;
        }

        public Boolean? Difference(IEqualityComparer<T>? comparer, out State<T> result)
        {
            if (!HasNext)
            {
                result = this;
                return null;
            }

            if (Next.Equals(Value, comparer))
            {
                result = Reset();
                return false;
            }

            result = this;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Difference(IEqualityComparer<T>? comparer)
        {
            return Difference(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Difference(T value)
        {
            return Difference(value, out State<T> result) is not null ? result : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean? Difference(T value, out State<T> result)
        {
            return Difference(value, null, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Difference(T value)
        {
            return Difference(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Difference(T value, IEqualityComparer<T>? comparer)
        {
            return Difference(value, comparer, out State<T> result) is not null ? result : this;
        }

        public Boolean? Difference(T value, IEqualityComparer<T>? comparer, out State<T> result)
        {
            if (!Next.Equals(value, comparer))
            {
                result = Update(value);
                return true;
            }

            if (HasNext)
            {
                result = Reset();
                return false;
            }

            result = this;
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Difference(T value, IEqualityComparer<T>? comparer)
        {
            return Difference(value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Difference(Maybe<T> value)
        {
            return Difference(value, out State<T> result) is not null ? result : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean? Difference(Maybe<T> value, out State<T> result)
        {
            return Difference(value, null, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Difference(Maybe<T> value)
        {
            return Difference(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Difference(Maybe<T> value, IEqualityComparer<T>? comparer)
        {
            return Difference(value, comparer, out State<T> result) is not null ? result : this;
        }

        public Boolean? Difference(Maybe<T> value, IEqualityComparer<T>? comparer, out State<T> result)
        {
            if (!Next.Equals(value, comparer))
            {
                result = Update(value);
                return true;
            }

            if (HasNext)
            {
                result = Reset();
                return false;
            }

            result = this;
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Difference(Maybe<T> value, IEqualityComparer<T>? comparer)
        {
            return Difference(value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Swap()
        {
            return Next ? new State<T>(Next.Value) { Next = Value } : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState IState.Swap()
        {
            return Swap();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Swap()
        {
            return Swap();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Reset()
        {
            return new State<T>(Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState IState.Reset()
        {
            return Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Reset()
        {
            return Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, StateEquality equality)
        {
            return CompareTo(other, equality, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return CompareTo(other, default, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, StateEquality equality, IComparer<T>? comparer)
        {
            return comparer.SafeCompare(Get(equality), other) ?? 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(State<T> other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(State<T> other, StateEquality equality)
        {
            return CompareTo(other, equality, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(State<T> other, IComparer<T>? comparer)
        {
            return CompareTo(other, default, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(State<T> other, StateEquality equality, IComparer<T>? comparer)
        {
            return CompareTo(other.Get(equality), equality, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IState<T>? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IState<T>? other, StateEquality equality)
        {
            return CompareTo(other, equality, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IState<T>? other, IComparer<T>? comparer)
        {
            return CompareTo(other, default, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IState<T>? other, StateEquality equality, IComparer<T>? comparer)
        {
            return other is not null ? CompareTo(other.Get(equality), equality, comparer) : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return Current?.GetHashCode() ?? 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(T? other)
        {
            return ReferenceEquals(other, default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(T? other, StateEquality equality)
        {
            return ReferenceEquals(Get(equality), other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(State<T> other)
        {
            return ReferenceEquals(other, default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(State<T> other, StateEquality equality)
        {
            return ReferenceEquals(other.Get(equality), equality);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IState<T>? other)
        {
            return ReferenceEquals(other, default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IState<T>? other, StateEquality equality)
        {
            return other is not null && ReferenceEquals(other.Get(equality), equality);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Object? other, StateEquality equality)
        {
            return Equals(other, equality, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return Equals(other, default, comparer);
        }

        public Boolean Equals(Object? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                T value => Equals(value, equality, comparer),
                State<T> value => Equals(value, equality, comparer),
                IState<T> value => Equals(value, equality, comparer),
                _ => false
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other, StateEquality equality)
        {
            return Equals(other, equality, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            return Equals(other, default, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Get(equality), other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(State<T> other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(State<T> other, StateEquality equality)
        {
            return Equals(other, equality, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(State<T> other, IEqualityComparer<T>? comparer)
        {
            return Equals(other, default, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(State<T> other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return Equals(other.Get(equality), equality, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IState<T>? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IState<T>? other, StateEquality equality)
        {
            return Equals(other, equality, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IState<T>? other, IEqualityComparer<T>? comparer)
        {
            return Equals(other, default, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IState<T>? other, StateEquality equality, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Get(equality), equality, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State<T> Clone()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> IState<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState<T> ICloneable<IState<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState IState.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IState ICloneable<IState>.Clone()
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
            return ToString(default(StateEquality));
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
            return ToString(default, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? ToString(StateEquality equality)
        {
            return Get(equality) is { } value ? value.ToString() : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(StateEquality equality, String? format)
        {
            return ToString(equality, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(StateEquality equality, IFormatProvider? provider)
        {
            return ToString(equality, null, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(StateEquality equality, String? format, IFormatProvider? provider)
        {
            return Get(equality) is { } value ? StringUtilities.ToString(in value, format, provider) : String.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString()
        {
            return GetString(default(StateEquality));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape)
        {
            return GetString(default, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format)
        {
            return GetString(default(StateEquality), format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format)
        {
            return GetString(default, escape, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(IFormatProvider? provider)
        {
            return GetString(default(StateEquality), provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return GetString(default, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format, IFormatProvider? provider)
        {
            return GetString(default(StateEquality), format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return GetString(default, escape, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(StateEquality equality)
        {
            return IStringProvider.Default.GetString(Get(equality));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(StateEquality equality, EscapeType escape)
        {
            return IStringProvider.Default.GetString(Get(equality), escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(StateEquality equality, String? format)
        {
            return IStringProvider.Default.GetString(Get(equality), format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(StateEquality equality, EscapeType escape, String? format)
        {
            return IStringProvider.Default.GetString(Get(equality), escape, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(StateEquality equality, IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(Get(equality), provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(StateEquality equality, EscapeType escape, IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(Get(equality), escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(StateEquality equality, String? format, IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(Get(equality), format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(StateEquality equality, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return IStringProvider.Default.GetString(Get(equality), escape, format, provider);
        }
    }
}