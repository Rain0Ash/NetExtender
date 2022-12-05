// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Validate
{
    public readonly struct ValidateState : IEquatable<ValidateState>, IEquatable<Boolean>, IEquatable<Boolean?>
    {
        private enum State
        {
            Unknown,
            Successful,
            Invalid
        }

        public static ValidateState Unknown { get; } = new ValidateState(State.Unknown);
        public static ValidateState Successful { get; } = new ValidateState(State.Successful);
        public static ValidateState Invalid { get; } = new ValidateState(State.Invalid);

        public static implicit operator Boolean(ValidateState value)
        {
            return value.Value switch
            {
                State.Unknown => true,
                State.Successful => true,
                State.Invalid => false,
                _ => throw new EnumUndefinedOrNotSupportedException<State>(value.Value, nameof(value), null)
            };
        }

        public static implicit operator ValidateState(Boolean value)
        {
            return value ? Successful : Invalid;
        }

        public static implicit operator Boolean?(ValidateState value)
        {
            return value.Value switch
            {
                State.Unknown => null,
                State.Successful => true,
                State.Invalid => false,
                _ => throw new EnumUndefinedOrNotSupportedException<State>(value.Value, nameof(value), null)
            };
        }

        public static implicit operator ValidateState(Boolean? value)
        {
            return value is not null ? value.Value ? Successful : Invalid : Unknown;
        }

        public static Boolean operator ==(ValidateState first, ValidateState second)
        {
            return first.Value == second.Value;
        }

        public static Boolean operator !=(ValidateState first, ValidateState second)
        {
            return first.Value != second.Value;
        }

        public static Boolean operator ==(ValidateState first, Boolean second)
        {
            return (Boolean) first == second;
        }

        public static Boolean operator !=(ValidateState first, Boolean second)
        {
            return (Boolean) first != second;
        }

        public static Boolean operator ==(ValidateState first, Boolean? second)
        {
            return (Boolean?) first == second;
        }

        public static Boolean operator !=(ValidateState first, Boolean? second)
        {
            return (Boolean?) first != second;
        }

        public static Boolean operator ==(Boolean first, ValidateState second)
        {
            return second == first;
        }

        public static Boolean operator !=(Boolean first, ValidateState second)
        {
            return second != first;
        }

        public static Boolean operator ==(Boolean first, ValidateState? second)
        {
            return second == first;
        }

        public static Boolean operator !=(Boolean first, ValidateState? second)
        {
            return second != first;
        }

        public static Boolean operator true(ValidateState value)
        {
            return value.IsSuccessful;
        }

        public static Boolean operator false(ValidateState value)
        {
            return value.IsInvalid;
        }

        public static Boolean operator !(ValidateState value)
        {
            Boolean result = value;
            return !result;
        }

        public static ValidateState operator &(ValidateState first, ValidateState second)
        {
            return first.Value switch
            {
                State.Unknown => new ValidateState(second.Value),
                State.Successful => second.Value switch
                {
                    State.Unknown => new ValidateState(State.Successful),
                    State.Successful => new ValidateState(State.Successful),
                    State.Invalid => new ValidateState(State.Invalid),
                    _ => throw new EnumUndefinedOrNotSupportedException<State>(second.Value, nameof(second), null)
                },
                State.Invalid => new ValidateState(State.Invalid),
                _ => throw new EnumUndefinedOrNotSupportedException<State>(first.Value, nameof(first), null)
            };
        }

        public static ValidateState operator &(ValidateState first, Boolean second)
        {
            return first.Value switch
            {
                State.Unknown => second,
                State.Successful => second,
                State.Invalid => false,
                _ => throw new EnumUndefinedOrNotSupportedException<State>(first.Value, nameof(first), null)
            };
        }

        public static ValidateState operator &(ValidateState first, Boolean? second)
        {
            return first.Value switch
            {
                State.Unknown => second,
                State.Successful => second != false,
                State.Invalid => false,
                _ => throw new EnumUndefinedOrNotSupportedException<State>(first.Value, nameof(first), null)
            };
        }

        public static ValidateState operator &(Boolean first, ValidateState second)
        {
            return second & first;
        }

        public static ValidateState operator &(Boolean? first, ValidateState second)
        {
            return second & first;
        }

        public static ValidateState operator |(ValidateState first, ValidateState second)
        {
            return first.Value switch
            {
                State.Unknown => new ValidateState(second.Value),
                State.Successful => new ValidateState(State.Successful),
                State.Invalid => second.Value switch
                {
                    State.Unknown => new ValidateState(State.Invalid),
                    State.Successful => new ValidateState(State.Successful),
                    State.Invalid => new ValidateState(State.Invalid),
                    _ => throw new EnumUndefinedOrNotSupportedException<State>(second.Value, nameof(second), null)
                },
                _ => throw new EnumUndefinedOrNotSupportedException<State>(first.Value, nameof(first), null)
            };
        }

        public static ValidateState operator |(ValidateState first, Boolean second)
        {
            return first.Value switch
            {
                State.Unknown => second,
                State.Successful => true,
                State.Invalid => second,
                _ => throw new EnumUndefinedOrNotSupportedException<State>(first.Value, nameof(first), null)
            };
        }

        public static ValidateState operator |(ValidateState first, Boolean? second)
        {
            return first.Value switch
            {
                State.Unknown => second,
                State.Successful => true,
                State.Invalid => second != false,
                _ => throw new EnumUndefinedOrNotSupportedException<State>(first.Value, nameof(first), null)
            };
        }

        public static ValidateState operator |(Boolean first, ValidateState second)
        {
            return second | first;
        }

        public static ValidateState operator |(Boolean? first, ValidateState second)
        {
            return second | first;
        }

        private State Value { get; }

        public Boolean IsUnknown
        {
            get
            {
                return Value == State.Unknown;
            }
        }

        public Boolean IsSuccessful
        {
            get
            {
                return Value != State.Invalid;
            }
        }

        public Boolean IsInvalid
        {
            get
            {
                return Value == State.Invalid;
            }
        }

        private ValidateState(State value)
        {
            Value = value;
        }

        public override Int32 GetHashCode()
        {
            return (Int32) Value;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                State value => Equals(value),
                ValidateState value => Equals(value),
                Boolean value => Equals(value),
                null => Equals((Boolean?) null),
                _ => false
            };
        }

        private Boolean Equals(State other)
        {
            return Value == other;
        }

        public Boolean Equals(ValidateState other)
        {
            return this == other;
        }

        public Boolean Equals(Boolean other)
        {
            return this == other;
        }

        public Boolean Equals(Boolean? other)
        {
            return this == other;
        }

        public override String ToString()
        {
            return Value switch
            {
                State.Unknown => nameof(Unknown),
                State.Successful => nameof(Successful),
                State.Invalid => nameof(Invalid),
                _ => throw new EnumUndefinedOrNotSupportedException<State>(Value, nameof(Value), null)
            };
        }
    }
}