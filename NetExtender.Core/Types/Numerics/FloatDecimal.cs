using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Numerics
{
    [Serializable]
    public readonly struct FloatDecimal : IEqualityStruct<FloatDecimal>, IEquality<Decimal>, IComparable, IConvertible, IFormattable, ISpanFormattable, ISerializable
    {
        public static implicit operator FloatDecimal(Char value)
        {
            return new Decimal((UInt32) value);
        }

        public static implicit operator FloatDecimal(SByte value)
        {
            return new Decimal(value);
        }

        public static implicit operator FloatDecimal(Byte value)
        {
            return new Decimal((UInt32) value);
        }

        public static implicit operator FloatDecimal(Int16 value)
        {
            return new Decimal(value);
        }

        public static implicit operator FloatDecimal(UInt16 value)
        {
            return new Decimal((UInt32) value);
        }

        public static implicit operator FloatDecimal(Int32 value)
        {
            return new Decimal(value);
        }

        public static implicit operator FloatDecimal(UInt32 value)
        {
            return new Decimal(value);
        }

        public static implicit operator FloatDecimal(Int64 value)
        {
            return new Decimal(value);
        }

        public static implicit operator FloatDecimal(UInt64 value)
        {
            return new Decimal(value);
        }

        public static explicit operator FloatDecimal(System.Single value)
        {
            return new Decimal(value);
        }

        public static explicit operator FloatDecimal(System.Double value)
        {
            return new Decimal(value);
        }
        
        public static implicit operator FloatDecimal(Decimal value)
        {
            return new FloatDecimal(value);
        }

        public static explicit operator Char(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? (Char) result : (Char) value._value;
        }

        public static explicit operator SByte(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? (SByte) result : (SByte) value._value;
        }

        public static explicit operator Byte(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? (Byte) result : (Byte) value._value;
        }

        public static explicit operator Int16(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? (Int16) result : (Int16) value._value;
        }

        public static explicit operator UInt16(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? (UInt16) result : (UInt16) value._value;
        }

        public static explicit operator Int32(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? (Int32) result : (Int32) value._value;
        }

        public static explicit operator UInt32(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? (UInt32) result : (UInt32) value._value;
        }

        public static explicit operator Int64(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? (Int64) result : (Int64) value._value;
        }

        public static explicit operator UInt64(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? (UInt64) result : (UInt64) value._value;
        }

        public static explicit operator System.Single(FloatDecimal value)
        {
            return value.AsSpecial(out System.Single result) ? result : (System.Single) value._value;
        }

        public static explicit operator System.Double(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? result : (System.Double) value._value;
        }

        public static explicit operator Decimal(FloatDecimal value)
        {
            return value.AsSpecial(out System.Double result) ? (Decimal) result : value._value;
        }

        public static Boolean operator ==(FloatDecimal first, FloatDecimal second)
        {
            
        }

        public static Boolean operator !=(FloatDecimal first, FloatDecimal second)
        {
            
        }

        public static Boolean operator <(FloatDecimal first, FloatDecimal second)
        {
            
        }

        public static Boolean operator <=(FloatDecimal first, FloatDecimal second)
        {
            
        }

        public static Boolean operator >(FloatDecimal first, FloatDecimal second)
        {
            
        }

        public static Boolean operator >=(FloatDecimal first, FloatDecimal second)
        {
            
        }

        public static FloatDecimal operator ++(FloatDecimal value)
        {
            try
            {
                return value._state switch
                {
                    State.Finite => value._value + Decimal.One,
                    State.NegativeZero => One,
                    State.PositiveInfinity => PositiveInfinity,
                    State.NegativeInfinity => NegativeInfinity,
                    State.NaN => NaN,
                    _ => throw new EnumUndefinedOrNotSupportedException<State>(value._state, nameof(State), null)
                };
            }
            catch (OverflowException)
            {
                return PositiveInfinity;
            }
        }

        public static FloatDecimal operator --(FloatDecimal value)
        {
            try
            {
                return value._state switch
                {
                    State.Finite => value._value - Decimal.One,
                    State.NegativeZero => MinusOne,
                    State.PositiveInfinity => PositiveInfinity,
                    State.NegativeInfinity => NegativeInfinity,
                    State.NaN => NaN,
                    _ => throw new EnumUndefinedOrNotSupportedException<State>(value._state, nameof(State), null)
                };
            }
            catch (OverflowException)
            {
                return NegativeInfinity;
            }
        }

        public static FloatDecimal operator +(FloatDecimal value)
        {
            return value._state switch
            {
                State.Finite => value._value,
                State.NegativeZero => Zero,
                State.PositiveInfinity => PositiveInfinity,
                State.NegativeInfinity => NegativeInfinity,
                State.NaN => NaN,
                _ => throw new EnumUndefinedOrNotSupportedException<State>(value._state, nameof(State), null)
            };
        }

        public static FloatDecimal operator -(FloatDecimal value)
        {
            try
            {
                return value._state switch
                {
                    State.Finite => -value._value,
                    State.NegativeZero => Zero,
                    State.PositiveInfinity => NegativeInfinity,
                    State.NegativeInfinity => PositiveInfinity,
                    State.NaN => NaN,
                    _ => throw new EnumUndefinedOrNotSupportedException<State>(value._state, nameof(State), null)
                };
            }
            catch (OverflowException)
            {
                return value._value > 0 ? NegativeInfinity : PositiveInfinity;
            }
        }

        public static FloatDecimal operator +(FloatDecimal first, FloatDecimal second)
        {
            try
            {
                return first._state switch
                {
                    State.Finite => second._state switch
                    {
                        State.Finite => new FloatDecimal(first._value + second._value),
                        State.NegativeZero => first,
                        State.PositiveInfinity => PositiveInfinity,
                        State.NegativeInfinity => NegativeInfinity,
                        State.NaN => NaN,
                        _ => throw new EnumUndefinedOrNotSupportedException<State>(second._state, nameof(State), null)
                    },
                    State.NegativeZero => second._state switch
                    {
                        State.Finite => second,
                        State.NegativeZero => NegativeZero,
                        State.PositiveInfinity => PositiveInfinity,
                        State.NegativeInfinity => NegativeInfinity,
                        State.NaN => NaN,
                        _ => throw new EnumUndefinedOrNotSupportedException<State>(second._state, nameof(State), null)
                    },
                    State.PositiveInfinity => second._state switch
                    {
                        State.Finite => PositiveInfinity,
                        State.NegativeZero => PositiveInfinity,
                        State.PositiveInfinity => PositiveInfinity,
                        State.NegativeInfinity => NaN,
                        State.NaN => NaN,
                        _ => throw new EnumUndefinedOrNotSupportedException<State>(second._state, nameof(State), null)
                    },
                    State.NegativeInfinity => second._state switch
                    {
                        State.Finite => NegativeInfinity,
                        State.NegativeZero => NegativeInfinity,
                        State.PositiveInfinity => NaN,
                        State.NegativeInfinity => NegativeInfinity,
                        State.NaN => NaN,
                        _ => throw new EnumUndefinedOrNotSupportedException<State>(second._state, nameof(State), null)
                    },
                    State.NaN => NaN,
                    _ => throw new EnumUndefinedOrNotSupportedException<State>(first._state, nameof(State), null)
                };
            }
            catch (OverflowException)
            {
                return (first._value > 0, second._value > 0) switch
                {
                    (true, true) => PositiveInfinity,
                    (false, false) => NegativeInfinity,
                    _ => NaN
                };
            }
        }

        public static FloatDecimal operator -(FloatDecimal first, FloatDecimal second)
        {
            try
            {
                return first._state switch
                {
                    State.Finite => second._state switch
                    {
                        State.Finite => new FloatDecimal(first._value - second._value),
                        State.NegativeZero => first,
                        State.PositiveInfinity => NegativeInfinity,
                        State.NegativeInfinity => PositiveInfinity,
                        State.NaN => NaN,
                        _ => throw new EnumUndefinedOrNotSupportedException<State>(second._state, nameof(State), null)
                    },
                    State.NegativeZero => second._state switch
                    {
                        State.Finite => new FloatDecimal(-second._value),
                        State.NegativeZero => NegativeZero,
                        State.PositiveInfinity => NegativeInfinity,
                        State.NegativeInfinity => PositiveInfinity,
                        State.NaN => NaN,
                        _ => throw new EnumUndefinedOrNotSupportedException<State>(second._state, nameof(State), null)
                    },
                    State.PositiveInfinity => second._state switch
                    {
                        State.Finite => PositiveInfinity,
                        State.NegativeZero => PositiveInfinity,
                        State.PositiveInfinity => NaN,
                        State.NegativeInfinity => PositiveInfinity,
                        State.NaN => NaN,
                        _ => throw new EnumUndefinedOrNotSupportedException<State>(second._state, nameof(State), null)
                    },
                    State.NegativeInfinity => second._state switch
                    {
                        State.Finite => NegativeInfinity,
                        State.NegativeZero => NegativeInfinity,
                        State.PositiveInfinity => NegativeInfinity,
                        State.NegativeInfinity => NaN,
                        State.NaN => NaN,
                        _ => throw new EnumUndefinedOrNotSupportedException<State>(second._state, nameof(State), null)
                    },
                    State.NaN => NaN,
                    _ => throw new EnumUndefinedOrNotSupportedException<State>(first._state, nameof(State), null)
                };
            }
            catch (OverflowException)
            {
                return (first._value > 0, second._value > 0) switch
                {
                    (true, false) => PositiveInfinity,
                    (false, true) => NegativeInfinity,
                    _ => NaN
                };
            }
        }

        public static FloatDecimal operator *(FloatDecimal first, FloatDecimal second)
        {
            try
            {
                return (first._state, second._state) switch
                {
                    (State.Finite, State.Finite) when first._value == Decimal.Zero || second._value == Decimal.Zero => HandleZeroMultiplication(first, second),
                    (State.Finite, State.Finite) => new FloatDecimal(first._value * second._value),
                    (State.NegativeZero, _) => HandleNegativeZeroMultiplication(first, second),
                    (_, State.NegativeZero) => HandleNegativeZeroMultiplication(second, first),

                    (State.PositiveInfinity, State.PositiveInfinity) => PositiveInfinity,
                    (State.PositiveInfinity, State.NegativeInfinity) => NegativeInfinity,
                    (State.NegativeInfinity, State.PositiveInfinity) => NegativeInfinity,
                    (State.NegativeInfinity, State.NegativeInfinity) => PositiveInfinity,

                    (State.Finite, State.PositiveInfinity) => first._value > Decimal.Zero ? PositiveInfinity : NegativeInfinity,
                    (State.Finite, State.NegativeInfinity) => first._value > Decimal.Zero ? NegativeInfinity : PositiveInfinity,
                    (State.PositiveInfinity, State.Finite) => second._value > Decimal.Zero ? PositiveInfinity : NegativeInfinity,
                    (State.NegativeInfinity, State.Finite) => second._value > Decimal.Zero ? NegativeInfinity : PositiveInfinity,

                    _ => NaN
                };
            }
            catch (OverflowException)
            {
                return first._value > Decimal.Zero == second._value > Decimal.Zero ? PositiveInfinity : NegativeInfinity;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FloatDecimal HandleZeroMultiplication(FloatDecimal first, FloatDecimal second)
        {
            Boolean negative = (first._state is State.NegativeZero || second._state is State.NegativeZero) && !(first._state is State.NegativeZero && second._state is State.NegativeZero);
            return negative ? NegativeZero : Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FloatDecimal HandleNegativeZeroMultiplication(FloatDecimal zero, FloatDecimal other)
        {
            return other._state switch
            {
                State.PositiveInfinity => NaN,
                State.NegativeInfinity => NaN,
                State.Finite when other._value == Decimal.Zero => NegativeZero,
                State.Finite => other._value > Decimal.Zero ? NegativeZero : Zero,
                _ => NaN
            };
        }

        public static FloatDecimal operator /(FloatDecimal first, FloatDecimal second)
        {
            
        }

        public static FloatDecimal operator %(FloatDecimal first, FloatDecimal second)
        {
            
        }

        public static FloatDecimal Zero
        {
            get
            {
                return Decimal.Zero;
            }
        }

        public static FloatDecimal NegativeZero
        {
            get
            {
                return new FloatDecimal(State.NegativeZero);
            }
        }

        public static FloatDecimal One
        {
            get
            {
                return Decimal.One;
            }
        }

        public static FloatDecimal MinusOne
        {
            get
            {
                return Decimal.MinusOne;
            }
        }
        
        public static FloatDecimal MaxValue
        {
            get
            {
                return Decimal.MaxValue;
            }
        }
        
        public static FloatDecimal MinValue
        {
            get
            {
                return Decimal.MinValue;
            }
        }
        
        public static FloatDecimal PositiveInfinity
        {
            get
            {
                return new FloatDecimal(State.PositiveInfinity);
            }
        }
        
        public static FloatDecimal NegativeInfinity
        {
            get
            {
                return new FloatDecimal(State.NegativeInfinity);
            }
        }
        
        public static FloatDecimal NaN
        {
            get
            {
                return new FloatDecimal(State.NaN);
            }
        }
        
        private readonly State _state;
        private readonly Decimal _value;

        Boolean IStruct.IsEmpty
        {
            get
            {
                return false;
            }
        }
        
        public FloatDecimal(Int32 value)
        {
            _state = State.Finite;
            _value = new Decimal(value);
        }

        public FloatDecimal(UInt32 value)
        {
            _state = State.Finite;
            _value = new Decimal(value);
        }

        public FloatDecimal(Int64 value)
        {
            _state = State.Finite;
            _value = new Decimal(value);
            
        }

        public FloatDecimal(UInt64 value)
        {
            _state = State.Finite;
            _value = new Decimal(value);
        }

        public FloatDecimal(System.Single value)
        {
            switch (value)
            {
                case Single.NegativeZero:
                    _state = State.NegativeZero;
                    _value = Decimal.Zero;
                    return;
                case Single.PositiveInfinity:
                    _state = State.PositiveInfinity;
                    _value = Decimal.Zero;
                    return;
                case Single.NegativeInfinity:
                    _state = State.NegativeInfinity;
                    _value = Decimal.Zero;
                    return;
                case Single.NaN:
                    _state = State.NaN;
                    _value = Decimal.Zero;
                    return;
                default:
                    _state = State.Finite;
                    _value = new Decimal(value);
                    return;
            }
        }

        public FloatDecimal(System.Double value)
        {
            switch (value)
            {
                case Double.NegativeZero:
                    _state = State.NegativeZero;
                    _value = Decimal.Zero;
                    return;
                case Double.PositiveInfinity:
                    _state = State.PositiveInfinity;
                    _value = Decimal.Zero;
                    return;
                case Double.NegativeInfinity:
                    _state = State.NegativeInfinity;
                    _value = Decimal.Zero;
                    return;
                case Double.NaN:
                    _state = State.NaN;
                    _value = Decimal.Zero;
                    return;
                default:
                    _state = State.Finite;
                    _value = new Decimal(value);
                    return;
            }
        }

        private FloatDecimal(State state)
        {
            _state = state;
            _value = Decimal.Zero;
        }

        public FloatDecimal(Decimal value)
        {
            _state = State.Finite;
            _value = value;
        }
        
        public FloatDecimal(ReadOnlySpan<Int32> bits)
        {
            switch (bits.Length)
            {
                case 1 when bits[0] is var state && state is > (Int32) State.Finite and <= (Int32) State.NaN:
                    _state = (State) state;
                    _value = Decimal.Zero;
                    return;
                default:
                    _state = State.Finite;
                    _value = new Decimal(bits);
                    return;
            }
        }
        
        public FloatDecimal(Int32 low, Int32 middle, Int32 high, Boolean negative, Byte scale)
        {
            _state = State.Finite;
            _value = new Decimal(low, middle, high, negative, scale);
        }

        private FloatDecimal(SerializationInfo info, StreamingContext context)
        {
            if (info.TryGetValue(nameof(State), out State state))
            {
                _state = EnumUtilities.ContainsValue(state) ? state : throw new SerializationException();
                _value = Decimal.Zero;
                return;
            }

            _state = State.Finite;
            _value = ReflectionUtilities.New<Decimal, SerializationInfo, StreamingContext>().Invoke(info, context);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            switch (_state)
            {
                case State.Finite:
                    SerializationUtilities.GetObjectData(_value, info, context);
                    return;
                case State.NegativeZero:
                case State.PositiveInfinity:
                case State.NegativeInfinity:
                case State.NaN:
                    info.AddValue(nameof(State), (Byte) _state);
                    return;
                default:
                    throw new EnumUndefinedOrNotSupportedException<State>(_state, nameof(State), null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Boolean AsSpecial(out System.Single result)
        {
            switch (_state)
            {
                case State.Finite:
                    result = 0;
                    return false;
                case State.NegativeZero:
                    result = Single.NegativeZero;
                    return true;
                case State.PositiveInfinity:
                    result = Single.PositiveInfinity;
                    return true;
                case State.NegativeInfinity:
                    result = Single.NegativeInfinity;
                    return true;
                case State.NaN:
                    result = Single.NaN;
                    return true;
                default:
                    throw new EnumUndefinedOrNotSupportedException<State>(_state, nameof(State), null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Boolean AsSpecial(out System.Double result)
        {
            switch (_state)
            {
                case State.Finite:
                    result = 0;
                    return false;
                case State.NegativeZero:
                    result = Double.NegativeZero;
                    return true;
                case State.PositiveInfinity:
                    result = Double.PositiveInfinity;
                    return true;
                case State.NegativeInfinity:
                    result = Double.NegativeInfinity;
                    return true;
                case State.NaN:
                    result = Double.NaN;
                    return true;
                default:
                    throw new EnumUndefinedOrNotSupportedException<State>(_state, nameof(State), null);
            }
        }

        public static Decimal FromOACurrency(Int64 cy)
        {
            UInt64 absoluteCy; // has to be ulong to accommodate the case where cy == long.MinValue.
            Boolean isNegative = false;
            if (cy < 0)
            {
                isNegative = true;
                absoluteCy = (UInt64)(-cy);
            }
            else
            {
                absoluteCy = (UInt64)cy;
            }

            // In most cases, FromOACurrency() produces a Decimal with Scale set to 4. Unless, that is, some of the trailing digits past the decimal point are zero,
            // in which case, for compatibility with .Net, we reduce the Scale by the number of zeros. While the result is still numerically equivalent, the scale does
            // affect the ToString() value. In particular, it prevents a converted currency value of $12.95 from printing uglily as "12.9500".
            Int32 scale = 4;
            if (absoluteCy != 0)  // For compatibility, a currency of 0 emits the Decimal "0.0000" (scale set to 4).
            {
                while (scale != 0 && ((absoluteCy % 10) == 0))
                {
                    scale--;
                    absoluteCy /= 10;
                }
            }

            return new Decimal((Int32)absoluteCy, (Int32)(absoluteCy >> 32), 0, isNegative, (Byte)scale);
        }

        public static Int64 ToOACurrency(Decimal value)
        {
            return DecCalc.VarCyFromDec(ref AsMutable(ref value));
        }
        
        public static Decimal Parse(String s)
        {
            if (s == null) ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
            return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo);
        }

        public static Decimal Parse(String s, NumberStyles style)
        {
            NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
            if (s == null) ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
            return Number.ParseDecimal(s, style, NumberFormatInfo.CurrentInfo);
        }

        public static Decimal Parse(String s, IFormatProvider? provider)
        {
            if (s == null) ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
            return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.GetInstance(provider));
        }

        public static Decimal Parse(String s, NumberStyles style, IFormatProvider? provider)
        {
            NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
            if (s == null) ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
            return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
        }

        public static Decimal Parse(ReadOnlySpan<Char> s, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = null)
        {
            NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
            return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
        }

        public static Boolean TryParse([NotNullWhen(true)] String? s, out Decimal result)
        {
            if (s == null)
            {
                result = 0;
                return false;
            }

            return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
        }

        public static Boolean TryParse(ReadOnlySpan<Char> s, out Decimal result)
        {
            return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
        }

        public static Boolean TryParse([NotNullWhen(true)] String? s, NumberStyles style, IFormatProvider? provider, out Decimal result)
        {
            NumberFormatInfo.ValidateParseStyleFloatingPoint(style);

            if (s == null)
            {
                result = 0;
                return false;
            }

            return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
        }

        public static Boolean TryParse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider, out Decimal result)
        {
            NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
            return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
        }

        // Returns a binary representation of a Decimal. The return value is an
        // integer array with four elements. Elements 0, 1, and 2 contain the low,
        // middle, and high 32 bits of the 96-bit integer part of the Decimal.
        // Element 3 contains the scale factor and sign of the Decimal: bits 0-15
        // (the lower word) are unused; bits 16-23 contain a value between 0 and
        // 28, indicating the power of 10 to divide the 96-bit integer part by to
        // produce the Decimal value; bits 24-30 are unused; and finally bit 31
        // indicates the sign of the Decimal value, 0 meaning positive and 1
        // meaning negative.
        //
        public static Int32[] GetBits(Decimal d)
        {
            return new Int32[] { (Int32)d.Low, (Int32)d.Mid, (Int32)d.High, d._flags };
        }

        /// <summary>
        /// Converts the value of a specified instance of <see cref="decimal"/> to its equivalent binary representation.
        /// </summary>
        /// <param name="d">The value to convert.</param>
        /// <param name="destination">The span into which to store the four-integer binary representation.</param>
        /// <returns>Four, the number of integers in the binary representation.</returns>
        /// <exception cref="ArgumentException">The destination span was not long enough to store the binary representation.</exception>
        public static Int32 GetBits(Decimal d, Span<Int32> destination)
        {
            if ((UInt32)destination.Length <= 3)
            {
                ThrowHelper.ThrowArgumentException_DestinationTooShort();
            }

            destination[0] = (Int32)d.Low;
            destination[1] = (Int32)d.Mid;
            destination[2] = (Int32)d.High;
            destination[3] = d._flags;
            return 4;
        }

        /// <summary>
        /// Tries to convert the value of a specified instance of <see cref="decimal"/> to its equivalent binary representation.
        /// </summary>
        /// <param name="d">The value to convert.</param>
        /// <param name="destination">The span into which to store the binary representation.</param>
        /// <param name="valuesWritten">The number of integers written to the destination.</param>
        /// <returns>true if the decimal's binary representation was written to the destination; false if the destination wasn't long enough.</returns>
        public static Boolean TryGetBits(Decimal d, Span<Int32> destination, out Int32 valuesWritten)
        {
            if ((UInt32)destination.Length <= 3)
            {
                valuesWritten = 0;
                return false;
            }

            destination[0] = (Int32)d.Low;
            destination[1] = (Int32)d.Mid;
            destination[2] = (Int32)d.High;
            destination[3] = d._flags;
            valuesWritten = 4;
            return true;
        }

        internal static void GetBytes(in Decimal d, Span<Byte> buffer)
        {
            Debug.Assert(buffer.Length >= 16, "buffer.Length >= 16");

            BinaryPrimitives.WriteInt32LittleEndian(buffer, (Int32)d.Low);
            BinaryPrimitives.WriteInt32LittleEndian(buffer.Slice(4), (Int32)d.Mid);
            BinaryPrimitives.WriteInt32LittleEndian(buffer.Slice(8), (Int32)d.High);
            BinaryPrimitives.WriteInt32LittleEndian(buffer.Slice(12), d._flags);
        }

        internal static Decimal ToDecimal(ReadOnlySpan<Byte> span)
        {
            Debug.Assert(span.Length >= 16, "span.Length >= 16");
            Int32 lo = BinaryPrimitives.ReadInt32LittleEndian(span);
            Int32 mid = BinaryPrimitives.ReadInt32LittleEndian(span.Slice(4));
            Int32 hi = BinaryPrimitives.ReadInt32LittleEndian(span.Slice(8));
            Int32 flags = BinaryPrimitives.ReadInt32LittleEndian(span.Slice(12));
            return new Decimal(lo, mid, hi, flags);
        }

        // Returns the larger of two Decimal values.
        //
        internal static ref readonly Decimal Max(in Decimal first, in Decimal second)
        {
            return ref DecCalc.VarDecCmp(in first, in second) >= 0 ? ref first : ref second;
        }

        // Returns the smaller of two Decimal values.
        //
        internal static ref readonly Decimal Min(in Decimal first, in Decimal second)
        {
            return ref DecCalc.VarDecCmp(in first, in second) < 0 ? ref first : ref second;
        }

        public static Decimal Remainder(Decimal first, Decimal second)
        {
            DecCalc.VarDecMod(ref AsMutable(ref first), ref AsMutable(ref second));
            return first;
        }

        // Multiplies two Decimal values.
        //
        public static Decimal Multiply(Decimal first, Decimal second)
        {
            DecCalc.VarDecMul(ref AsMutable(ref first), ref AsMutable(ref second));
            return first;
        }

        // Returns the negated value of the given Decimal. If d is non-zero,
        // the result is -d. If d is zero, the result is zero.
        //
        public static Decimal Negate(Decimal d)
        {
            return new Decimal(in d, d._flags ^ SignMask);
        }

        // Rounds a Decimal value to a given number of decimal places. The value
        // given by d is rounded to the number of decimal places given by
        // decimals. The decimals argument must be an integer between
        // 0 and 28 inclusive.
        //
        // By default a mid-point value is rounded to the nearest even number. If the mode is
        // passed in, it can also round away from zero.

        public static Decimal Round(Decimal d)
        {
            return Round(ref d, 0, MidpointRounding.ToEven);
        }

        public static Decimal Round(Decimal d, Int32 decimals)
        {
            return Round(ref d, decimals, MidpointRounding.ToEven);
        }

        public static Decimal Round(Decimal d, MidpointRounding mode)
        {
            return Round(ref d, 0, mode);
        }

        public static Decimal Round(Decimal d, Int32 decimals, MidpointRounding mode)
        {
            return Round(ref d, decimals, mode);
        }

        private static Decimal Round(ref Decimal d, Int32 decimals, MidpointRounding mode)
        {
            if ((UInt32)decimals > 28)
                throw new ArgumentOutOfRangeException(nameof(decimals), SR.ArgumentOutOfRange_DecimalRound);
            if ((UInt32)mode > (UInt32)MidpointRounding.ToPositiveInfinity)
                throw new ArgumentException(SR.Format(SR.Argument_InvalidEnumValue, mode, nameof(MidpointRounding)), nameof(mode));

            Int32 scale = d.Scale - decimals;
            if (scale > 0)
                DecCalc.InternalRound(ref AsMutable(ref d), (UInt32)scale, mode);
            return d;
        }

        internal static Int32 Sign(in Decimal d)
        {
            return (d.Low64 | d.High) == 0 ? 0 : (d._flags >> 31) | 1;
        }

        // Subtracts two Decimal values.
        //
        public static Decimal Subtract(Decimal first, Decimal second)
        {
            DecCalc.DecAddSub(ref AsMutable(ref first), ref AsMutable(ref second), true);
            return first;
        }

        // Converts a Decimal to an unsigned byte. The Decimal value is rounded
        // towards zero to the nearest integer value, and the result of this
        // operation is returned as a byte.
        //
        public static Byte ToByte(Decimal value)
        {
            UInt32 temp;
            try
            {
                temp = ToUInt32(value);
            }
            catch (OverflowException)
            {
                Number.ThrowOverflowException(TypeCode.Byte);
                throw;
            }
            if (temp != (Byte)temp) Number.ThrowOverflowException(TypeCode.Byte);
            return (Byte)temp;
        }

        // Converts a Decimal to a signed byte. The Decimal value is rounded
        // towards zero to the nearest integer value, and the result of this
        // operation is returned as a byte.
        //
        public static SByte ToSByte(Decimal value)
        {
            Int32 temp;
            try
            {
                temp = ToInt32(value);
            }
            catch (OverflowException)
            {
                Number.ThrowOverflowException(TypeCode.SByte);
                throw;
            }
            if (temp != (SByte)temp) Number.ThrowOverflowException(TypeCode.SByte);
            return (SByte)temp;
        }

        // Converts a Decimal to a short. The Decimal value is
        // rounded towards zero to the nearest integer value, and the result of
        // this operation is returned as a short.
        //
        public static Int16 ToInt16(Decimal value)
        {
            Int32 temp;
            try
            {
                temp = ToInt32(value);
            }
            catch (OverflowException)
            {
                Number.ThrowOverflowException(TypeCode.Int16);
                throw;
            }
            if (temp != (Int16)temp) Number.ThrowOverflowException(TypeCode.Int16);
            return (Int16)temp;
        }

        // Converts a Decimal to a double. Since a double has fewer significant
        // digits than a Decimal, this operation may produce round-off errors.
        //
        public static System.Double ToDouble(Decimal d)
        {
            return DecCalc.VarR8FromDec(in d);
        }

        // Converts a Decimal to an integer. The Decimal value is rounded towards
        // zero to the nearest integer value, and the result of this operation is
        // returned as an integer.
        //
        public static Int32 ToInt32(Decimal d)
        {
            Truncate(ref d);
            if ((d.High | d.Mid) == 0)
            {
                Int32 i = (Int32)d.Low;
                if (!d.IsNegative)
                {
                    if (i >= 0) return i;
                }
                else
                {
                    i = -i;
                    if (i <= 0) return i;
                }
            }
            throw new OverflowException(SR.Overflow_Int32);
        }

        // Converts a Decimal to a long. The Decimal value is rounded towards zero
        // to the nearest integer value, and the result of this operation is
        // returned as a long.
        //
        public static Int64 ToInt64(Decimal d)
        {
            Truncate(ref d);
            if (d.High == 0)
            {
                Int64 l = (Int64)d.Low64;
                if (!d.IsNegative)
                {
                    if (l >= 0) return l;
                }
                else
                {
                    l = -l;
                    if (l <= 0) return l;
                }
            }
            throw new OverflowException(SR.Overflow_Int64);
        }

        // Converts a Decimal to an ushort. The Decimal
        // value is rounded towards zero to the nearest integer value, and the
        // result of this operation is returned as an ushort.
        //
        public static UInt16 ToUInt16(Decimal value)
        {
            UInt32 temp;
            try
            {
                temp = ToUInt32(value);
            }
            catch (OverflowException)
            {
                Number.ThrowOverflowException(TypeCode.UInt16);
                throw;
            }
            if (temp != (UInt16)temp) Number.ThrowOverflowException(TypeCode.UInt16);
            return (UInt16)temp;
        }

        // Converts a Decimal to an unsigned integer. The Decimal
        // value is rounded towards zero to the nearest integer value, and the
        // result of this operation is returned as an unsigned integer.
        //
        public static UInt32 ToUInt32(Decimal d)
        {
            Truncate(ref d);
            if ((d.High| d.Mid) == 0)
            {
                UInt32 i = d.Low;
                if (!d.IsNegative || i == 0)
                    return i;
            }
            throw new OverflowException(SR.Overflow_UInt32);
        }

        // Converts a Decimal to an unsigned long. The Decimal
        // value is rounded towards zero to the nearest integer value, and the
        // result of this operation is returned as a long.
        //
        public static UInt64 ToUInt64(Decimal d)
        {
            Truncate(ref d);
            if (d.High == 0)
            {
                UInt64 l = d.Low64;
                if (!d.IsNegative || l == 0)
                    return l;
            }
            throw new OverflowException(SR.Overflow_UInt64);
        }

        // Converts a Decimal to a float. Since a float has fewer significant
        // digits than a Decimal, this operation may produce round-off errors.
        //
        public static Single ToSingle(Decimal d)
        {
            return DecCalc.VarR4FromDec(in d);
        }

        // Truncates a Decimal to an integer value. The Decimal argument is rounded
        // towards zero to the nearest integer value, corresponding to removing all
        // digits after the decimal point.
        //
        public static Decimal Truncate(Decimal d)
        {
            Truncate(ref d);
            return d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Truncate(ref Decimal d)
        {
            Int32 flags = d._flags;
            if ((flags & ScaleMask) != 0)
                DecCalc.InternalRound(ref AsMutable(ref d), (Byte)(flags >> ScaleShift), MidpointRounding.ToZero);
        }

        public TypeCode GetTypeCode()
        {
            return _value.GetTypeCode();
        }

        Boolean IConvertible.ToBoolean(IFormatProvider? provider)
        {
            
        }

        Char IConvertible.ToChar(IFormatProvider? provider)
        {
            
        }

        SByte IConvertible.ToSByte(IFormatProvider? provider)
        {
            
        }

        Byte IConvertible.ToByte(IFormatProvider? provider)
        {
            
        }

        Int16 IConvertible.ToInt16(IFormatProvider? provider)
        {
            
        }

        UInt16 IConvertible.ToUInt16(IFormatProvider? provider)
        {
            
        }

        Int32 IConvertible.ToInt32(IFormatProvider? provider)
        {
            
        }

        UInt32 IConvertible.ToUInt32(IFormatProvider? provider)
        {
            
        }

        Int64 IConvertible.ToInt64(IFormatProvider? provider)
        {
            
        }

        UInt64 IConvertible.ToUInt64(IFormatProvider? provider)
        {
            
        }

        System.Single IConvertible.ToSingle(IFormatProvider? provider)
        {
            
        }

        System.Double IConvertible.ToDouble(IFormatProvider? provider)
        {
            
        }

        Decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            
        }

        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        {
            
        }

        Object IConvertible.ToType(Type type, IFormatProvider? provider)
        {
            
        }

        public Boolean TryFormat(Span<Char> destination, out Int32 written, ReadOnlySpan<Char> format, IFormatProvider? provider)
        {
            return AsSpecial(out System.Double result) ? result.TryFormat(destination, out written, format, provider) : _value.TryFormat(destination, out written, format, provider);
        }

        public override Int32 GetHashCode()
        {
            return AsSpecial(out System.Double result) ? result.GetHashCode() : _value.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            
        }

        public override String ToString()
        {
            return AsSpecial(out System.Double result) ? result.ToString() : _value.ToString();
        }

        public String ToString(String? format)
        {
            return AsSpecial(out System.Double result) ? result.ToString(format) : _value.ToString(format);
        }

        public String ToString(IFormatProvider? provider)
        {
            return AsSpecial(out System.Double result) ? result.ToString(provider) : _value.ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return AsSpecial(out System.Double result) ? result.ToString(format, provider) : _value.ToString(format, provider);
        }

        private enum State : Byte
        {
            Finite,
            NegativeZero,
            PositiveInfinity,
            NegativeInfinity,
            NaN
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class Single
        {
#if NET7_0_OR_GREATER
            public const System.Single NegativeZero = System.Single.NegativeZero;
#else
            public const System.Single NegativeZero = -0.0F;
#endif
            public const System.Single PositiveInfinity = System.Single.PositiveInfinity;
            public const System.Single NegativeInfinity = System.Single.NegativeInfinity;
            public const System.Single NaN = System.Single.NaN;
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class Double
        {
#if NET7_0_OR_GREATER
            public const System.Double NegativeZero = System.Double.NegativeZero;
#else
            public const System.Double NegativeZero = -0.0;
#endif
            public const System.Double PositiveInfinity = System.Double.PositiveInfinity;
            public const System.Double NegativeInfinity = System.Double.NegativeInfinity;
            public const System.Double NaN = System.Double.NaN;
        }
    }
}