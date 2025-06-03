using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using NetExtender.Types.Numerics.Exceptions.Interfaces;

namespace NetExtender.Types.Numerics.Exceptions
{
    [Serializable]
    public sealed class NumericArgumentException : NumericInfinityException
    {
        public override Boolean IsArgument
        {
            get
            {
                return true;
            }
        }

        public override Boolean IsOverflow
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsInfinity
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsPositiveInfinity
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsNegativeInfinity
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsNaN
        {
            get
            {
                return false;
            }
        }
        
        public NumericArgumentException()
        {
        }

        public NumericArgumentException(Exception? exception)
            : base(null, exception)
        {
        }

        public NumericArgumentException(String? message)
            : base(message)
        {
        }

        public NumericArgumentException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public NumericArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override Int32 GetHashCode()
        {
            return 0;
        }

        public override Boolean Equals(Object? other)
        {
            return other is ArgumentException or NumericArgumentException;
        }

        public override String ToString()
        {
            return "⌀";
        }
    }
    
    [Serializable]
    public sealed class NaNException : NumericException
    {
        public override Boolean IsArgument
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsOverflow
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsInfinity
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsPositiveInfinity
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsNegativeInfinity
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsNaN
        {
            get
            {
                return true;
            }
        }
        
        public NaNException()
        {
        }

        public NaNException(Exception? exception)
            : base(null, exception)
        {
        }

        public NaNException(String? message)
            : base(message)
        {
        }

        public NaNException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public NaNException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override Int32 GetHashCode()
        {
            return Double.NaN.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other is NaNException;
        }

        public override String ToString()
        {
            return "NaN";
        }
    }
    
    [Serializable]
    public sealed class PositiveInfinityException : NumericInfinityException
    {
        public override Boolean IsArgument
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsOverflow
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsInfinity
        {
            get
            {
                return true;
            }
        }

        public override Boolean IsPositiveInfinity
        {
            get
            {
                return true;
            }
        }

        public override Boolean IsNegativeInfinity
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsNaN
        {
            get
            {
                return false;
            }
        }
        
        public PositiveInfinityException()
        {
        }

        public PositiveInfinityException(Exception? exception)
            : base(null, exception)
        {
        }

        public PositiveInfinityException(String? message)
            : base(message)
        {
        }

        public PositiveInfinityException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public PositiveInfinityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override Int32 GetHashCode()
        {
            return Double.PositiveInfinity.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other is PositiveInfinityException;
        }

        public override String ToString()
        {
            return "∞";
        }
    }

    [Serializable]
    public sealed class NegativeInfinityException : NumericInfinityException
    {
        public override Boolean IsArgument
        {
            get
            {
                return false;
            }
        }
        
        public override Boolean IsOverflow
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsInfinity
        {
            get
            {
                return true;
            }
        }

        public override Boolean IsPositiveInfinity
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsNegativeInfinity
        {
            get
            {
                return true;
            }
        }

        public override Boolean IsNaN
        {
            get
            {
                return false;
            }
        }

        public NegativeInfinityException()
        {
        }

        public NegativeInfinityException(Exception? exception)
            : base(null, exception)
        {
        }

        public NegativeInfinityException(String? message)
            : base(message)
        {
        }

        public NegativeInfinityException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public NegativeInfinityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override Int32 GetHashCode()
        {
            return Double.NegativeInfinity.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other is NegativeInfinityException;
        }

        public override String ToString()
        {
            return "-∞";
        }
    }
    
    [Serializable]
    public class NumericInfinityException : NumericException
    {
        public Boolean FromZeroDivision { get; init; }

        public override Boolean IsArgument
        {
            get
            {
                return false;
            }
        }
        
        public override Boolean IsOverflow
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsInfinity
        {
            get
            {
                return true;
            }
        }

        public override Boolean IsPositiveInfinity
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsNegativeInfinity
        {
            get
            {
                return false;
            }
        }

        public override Boolean IsNaN
        {
            get
            {
                return false;
            }
        }
        
        public NumericInfinityException()
        {
        }

        public NumericInfinityException(Exception? exception)
            : base(null, exception)
        {
        }

        public NumericInfinityException(String? message)
            : base(message)
        {
        }

        public NumericInfinityException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        protected NumericInfinityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override Int32 GetHashCode()
        {
            return Double.PositiveInfinity.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other is DivideByZeroException or NumericInfinityException;
        }

        public override String ToString()
        {
            return "±∞";
        }
    }

    [Serializable]
    public abstract class NumericException : OverflowException, INumericException
    {
        public virtual Boolean IsArgument
        {
            get
            {
                return false;
            }
        }
        
        public virtual Boolean IsOverflow
        {
            get
            {
                return false;
            }
        }

        public virtual Boolean IsInfinity
        {
            get
            {
                return this is NumericInfinityException;
            }
        }

        public virtual Boolean IsPositiveInfinity
        {
            get
            {
                return this is PositiveInfinityException;
            }
        }

        public virtual Boolean IsNegativeInfinity
        {
            get
            {
                return this is NegativeInfinityException;
            }
        }

        public virtual Boolean IsNaN
        {
            get
            {
                return this is NaNException;
            }
        }
        
        protected NumericException()
        {
        }

        protected NumericException(Exception? exception)
            : base(null, exception)
        {
        }

        protected NumericException(String? message)
            : base(message)
        {
        }

        protected NumericException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        protected NumericException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [return: NotNullIfNotNull("exception")]
        public static OverflowException? From(Exception? exception)
        {
            return exception switch
            {
                null => null,
                ArgumentException value => From(value),
                DivideByZeroException value => From(value),
                OverflowException value => value,
                { } value => new NumericArgumentException(value)
            };
        }

        [return: NotNullIfNotNull("exception")]
        public static OverflowException? From(ArgumentException? exception)
        {
            return exception is not null ? new NumericArgumentException(exception) : null;
        }

        [return: NotNullIfNotNull("exception")]
        public static OverflowException? From(DivideByZeroException? exception)
        {
            return From(exception, null);
        }

        [return: NotNullIfNotNull("exception")]
        public static OverflowException? From(DivideByZeroException? exception, Boolean? sign)
        {
            if (exception is null)
            {
                return null;
            }

            return sign switch
            {
                null => new NumericInfinityException(exception) { FromZeroDivision = true },
                true => new PositiveInfinityException(exception) { FromZeroDivision = true },
                false => new NegativeInfinityException(exception) { FromZeroDivision = true }
            };
        }

        public abstract override Int32 GetHashCode();
        public abstract override Boolean Equals(Object? other);
        public abstract override String ToString();
    }
}